using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.View.User.Controls.FlashcardControls;

namespace WinFormsApp1.View.Admin
{
    public partial class FlashcardManagementControl : AdminBaseControl
    {
        private readonly FlashcardController _flashcardController;
        private List<FlashcardSet> _allFlashcardSets = new List<FlashcardSet>();
        private List<FlashcardSet> _filteredFlashcardSets = new List<FlashcardSet>();
        private int _currentEditingSetId = 0;
        private bool _isPaginationInitialized = false; // Thêm flag để track pagination

        public FlashcardManagementControl() : base()
        {
            InitializeComponent();
            _flashcardController = new FlashcardController();
            
            dataGridView = CreateModernDataGridView();
            SetupDataGridColumns();
            SetupLayout("Quản lý Flashcard", dataGridView);
            WireCrudEvents();
            
            SetupFilterEvents();
            _ = LoadFlashcardSetsAsync();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "FlashcardManagementControl";
            this.Size = new Size(1200, 800);
            this.ResumeLayout(false);
        }

        private void SetupDataGridColumns()
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.Columns.Clear();

            dataGridView.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    Name = "SetId",
                    HeaderText = "ID",
                    DataPropertyName = "SetId",
                    Width = 60,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Title",
                    HeaderText = "Tiêu đề",
                    DataPropertyName = "Title",
                    Width = 250,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "OwnerName",
                    HeaderText = "Người tạo",
                    Width = 150,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "CardCount",
                    HeaderText = "Số thẻ",
                    Width = 80,
                    ReadOnly = true,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Visibility",
                    HeaderText = "Trạng thái",
                    DataPropertyName = "Visibility",
                    Width = 100,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Language",
                    HeaderText = "Ngôn ngữ",
                    DataPropertyName = "Language",
                    Width = 100,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "CreatedAt",
                    HeaderText = "Ngày tạo",
                    DataPropertyName = "CreatedAt",
                    Width = 130,
                    ReadOnly = true,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
                }
            });

            // Add double-click event
            dataGridView.CellDoubleClick += DataGridView_CellDoubleClick;
        }

        private void SetupFilterEvents()
        {
            // Setup search
            SetupSearchFunctionality(dataGridView, "Title", "OwnerName", "Language");

            // Find and wire visibility filter if exists
            var visibilityCombo = this.Controls.Find("cboVisibility", true).FirstOrDefault() as ComboBox;
            if (visibilityCombo != null)
            {
                visibilityCombo.SelectedIndexChanged += (s, e) => FilterFlashcardsLocally();
            }
        }

        protected override Panel CreateFilterPanel()
        {
            var panel = base.CreateFilterPanel();

            // Visibility Filter
            var visibilityCombo = new ComboBox
            {
                Items = { "Tất cả", "Public", "Private", "Unlisted" },
                SelectedIndex = 0,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                Size = new Size(120, 25),
                Name = "cboVisibility"
            };
            visibilityCombo.SelectedIndexChanged += (s, e) => FilterFlashcardsLocally();

            var visibilityLabel = new Label
            {
                Text = "Trạng thái:",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(visibilityCombo.Left - 75, 15),
                Tag = visibilityCombo.Left - 75
            };

            panel.Controls.Add(visibilityLabel);
            AddFilterControl(visibilityCombo);

            // Adjust labels position after controls are added
            panel.Resize += (s, e) =>
            {
                var combo = panel.Controls.Find("cboVisibility", false).FirstOrDefault();
                if (combo != null && visibilityLabel != null)
                {
                    visibilityLabel.Left = combo.Left - 75;
                }
            };

            return panel;
        }

        private void FilterFlashcardsLocally()
        {
            var visibilityCombo = this.Controls.Find("cboVisibility", true).FirstOrDefault() as ComboBox;
            var selectedVisibility = visibilityCombo?.SelectedItem?.ToString();

            _filteredFlashcardSets = _allFlashcardSets.Where(f =>
            {
                bool matchVisibility = string.IsNullOrEmpty(selectedVisibility) ||
                                     selectedVisibility == "Tất cả" ||
                                     f.Visibility == selectedVisibility;

                return matchVisibility;
            }).ToList();

            DisplayFlashcardSets(_filteredFlashcardSets);
        }

        private async Task LoadFlashcardSetsAsync()
        {
            try
            {
                // Load all flashcard sets (admin can see all)
                _allFlashcardSets = await _flashcardController.GetAllPublicFlashcardSetsAsync();
                
                _filteredFlashcardSets = _allFlashcardSets;
                DisplayFlashcardSets(_filteredFlashcardSets);

                // Chỉ tạo pagination panel một lần duy nhất
                if (!_isPaginationInitialized)
                {
                    var paginationPanel = paginationHelper.CreatePaginationPanel((page) => DisplayCurrentPage());
                    this.Controls.Add(paginationPanel);
                    paginationPanel.BringToFront();
                    _isPaginationInitialized = true;
                }
                
                UpdatePagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải flashcard sets: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayCurrentPage()
        {
            var pagedData = paginationHelper.GetPagedData(_filteredFlashcardSets);
            DisplayFlashcardSets(pagedData.ToList());
        }

        private void UpdatePagination()
        {
            paginationHelper.UpdatePagination(_filteredFlashcardSets.Count);
            DisplayCurrentPage();
        }

        private void DisplayFlashcardSets(List<FlashcardSet> flashcardSets)
        {
            try
            {
                dataGridView.Rows.Clear();

                foreach (var set in flashcardSets)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dataGridView);
                    row.Tag = set;

                    row.Cells[0].Value = set.SetId;
                    row.Cells[1].Value = set.Title;
                    row.Cells[2].Value = set.Owner?.FullName ?? "N/A";
                    row.Cells[3].Value = set.Flashcards?.Count ?? 0;
                    row.Cells[4].Value = set.Visibility;
                    row.Cells[5].Value = set.Language ?? "N/A";
                    row.Cells[6].Value = set.CreatedAt;

                    // Color code by visibility
                    if (set.Visibility == "Private")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    }
                    else if (set.Visibility == "Unlisted")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(230, 240, 255);
                    }

                    dataGridView.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnAddButtonClick(object sender, EventArgs e)
        {
            // Sử dụng form CreateFlashcardControl của User
            ShowUserCreateFlashcardForm();
        }

        protected override void OnEditButtonClick(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một flashcard set để chỉnh sửa!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedSet = dataGridView.SelectedRows[0].Tag as FlashcardSet;
            if (selectedSet != null)
            {
                // Sử dụng form EditFlashcardControl của User
                ShowUserEditFlashcardForm(selectedSet.SetId);
            }
        }

        protected override async void OnDeleteButtonClick(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một flashcard set để xóa!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedSet = dataGridView.SelectedRows[0].Tag as FlashcardSet;
            if (selectedSet == null) return;

            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa flashcard set:\n\n" +
                $"'{selectedSet.Title}'\n\n" +
                $"Có {selectedSet.Flashcards?.Count ?? 0} thẻ trong bộ này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    await _flashcardController.DeleteFlashcardSetAsync(selectedSet.SetId);
                    await LogAdminActionAsync("DELETE", "FlashcardSet", selectedSet.SetId,
                        $"Xóa flashcard set: {selectedSet.Title}");
                    
                    MessageBox.Show("Xóa flashcard set thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    await LoadFlashcardSetsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa flashcard set: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override async void OnRefreshButtonClick(object sender, EventArgs e)
        {
            await LoadFlashcardSetsAsync();
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Sử dụng form CreateFlashcardControl của User
        private void ShowUserCreateFlashcardForm()
        {
            // Tạo một form wrapper để chứa CreateFlashcardControl
            var dialogForm = new Form
            {
                Text = "Tạo Flashcard Set mới",
                Size = new Size(950, 700),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var createControl = new CreateFlashcardControl
            {
                Dock = DockStyle.Fill
            };

            dialogForm.Controls.Add(createControl);

            // Hook vào sự kiện để đóng form sau khi tạo thành công
            // (Bạn có thể cần thêm event vào CreateFlashcardControl nếu chưa có)
            dialogForm.FormClosing += async (s, e) =>
            {
                // Reload data khi đóng form
                await LoadFlashcardSetsAsync();
            };

            dialogForm.ShowDialog(this.FindForm());
        }

        // Sử dụng form EditFlashcardControl của User
        private void ShowUserEditFlashcardForm(int setId)
        {
            // Tạo một form wrapper để chứa EditFlashcardControl
            var dialogForm = new Form
            {
                Text = "Chỉnh sửa Flashcard Set",
                Size = new Size(950, 700),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var editControl = new EditFlashcardControl(setId)
            {
                Dock = DockStyle.Fill
            };

            dialogForm.Controls.Add(editControl);

            // Hook vào sự kiện để đóng form sau khi lưu thành công
            dialogForm.FormClosing += async (s, e) =>
            {
                // Reload data khi đóng form
                await LoadFlashcardSetsAsync();
            };

            dialogForm.ShowDialog(this.FindForm());
        }

        // Xem chi tiết - sử dụng FlashcardDetailControl của User
        private void ShowFlashcardDetailView(int setId)
        {
            var dialogForm = new Form
            {
                Text = "Chi tiết Flashcard Set",
                Size = new Size(1200, 700),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.Sizable,
                MaximizeBox = true,
                MinimizeBox = false
            };

            var detailControl = new FlashcardDetailControl(setId)
            {
                Dock = DockStyle.Fill
            };

            dialogForm.Controls.Add(detailControl);
            dialogForm.ShowDialog(this.FindForm());
        }

        // Handle double-click on row to view details
        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedSet = dataGridView.Rows[e.RowIndex].Tag as FlashcardSet;
                if (selectedSet != null)
                {
                    ShowFlashcardDetailView(selectedSet.SetId);
                }
            }
        }
    }
}
