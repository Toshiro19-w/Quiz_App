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
            SetupLayout(Lang("FlashcardManagement"), dataGridView);
            WireCrudEvents();
            
            // Add custom filters using new pattern
            SetupCustomFilters();
            
            SetupFilterEvents();
            
            // ❌ Don't load data immediately - wait for user interaction
            // _ = LoadFlashcardSetsAsync();
        }

        /// <summary>
        /// Setup custom filters for Flashcard Management
        /// </summary>
        private void SetupCustomFilters()
        {
            // Visibility Filter
            var visibilityCombo = new ComboBox
            {
                Name = "cboVisibility",
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            visibilityCombo.Items.AddRange(new object[] { Lang("All"), Lang("VisibilityPublic"), Lang("VisibilityPrivate"), Lang("VisibilityUnlisted") });
            visibilityCombo.SelectedIndex = 0;
            visibilityCombo.SelectedIndexChanged += (s, e) => FilterFlashcardsLocally();

            // Add filter using the new helper method
            AddCustomFilter(Lang("FilterVisibility"), visibilityCombo);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "FlashcardManagementControl";
            this.Size = new Size(1200, 800);
            
            // ✅ Load data after component is initialized
            this.Load += async (s, e) => await LoadFlashcardSetsAsync();
            
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
                    HeaderText = Lang("Title"),
                    DataPropertyName = "Title",
                    Width = 250,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "OwnerName",
                    HeaderText = Lang("Creator"),
                    Width = 150,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "CardCount",
                    HeaderText = Lang("CardCount"),
                    Width = 80,
                    ReadOnly = true,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Visibility",
                    HeaderText = Lang("Status"),
                    DataPropertyName = "Visibility",
                    Width = 100,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Language",
                    HeaderText = Lang("Language"),
                    DataPropertyName = "Language",
                    Width = 100,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "CreatedAt",
                    HeaderText = Lang("CreatedAt"),
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
        }

        private void FilterFlashcardsLocally()
        {
            var visibilityCombo = this.Controls.Find("cboVisibility", true).FirstOrDefault() as ComboBox;
            var selectedIndex = visibilityCombo?.SelectedIndex ?? 0;

            _filteredFlashcardSets = _allFlashcardSets.Where(f =>
            {
                // Index 0 = All, 1 = Public, 2 = Private, 3 = Unlisted
                bool matchVisibility = selectedIndex == 0 ||
                                     (selectedIndex == 1 && f.Visibility == "Public") ||
                                     (selectedIndex == 2 && f.Visibility == "Private") ||
                                     (selectedIndex == 3 && f.Visibility == "Unlisted");

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
                MessageBox.Show(Lang("FlashcardLoadError", ex.Message), Lang("Error"),
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
                MessageBox.Show(Lang("DisplayError", ex.Message), Lang("Error"),
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
                MessageBox.Show(Lang("PleaseSelectFlashcardToEdit"), Lang("Warning"),
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
                MessageBox.Show(Lang("PleaseSelectFlashcardToDelete"), Lang("Warning"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedSet = dataGridView.SelectedRows[0].Tag as FlashcardSet;
            if (selectedSet == null) return;

            var result = MessageBox.Show(
                $"{Lang("ConfirmDeleteFlashcard")}\n\n" +
                $"'{selectedSet.Title}'\n\n" +
                $"{Lang("CardsInSet", selectedSet.Flashcards?.Count ?? 0)}",
                Lang("Confirm"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    await _flashcardController.DeleteFlashcardSetAsync(selectedSet.SetId);
                    await LogAdminActionAsync("DELETE", "FlashcardSet", selectedSet.SetId,
                        $"Xóa flashcard set: {selectedSet.Title}");
                    
                    MessageBox.Show(Lang("FlashcardDeleteSuccess"), Lang("Success"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    await LoadFlashcardSetsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Lang("FlashcardDeleteFailed") + $": {ex.Message}", Lang("Error"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override async void OnRefreshButtonClick(object sender, EventArgs e)
        {
            await LoadFlashcardSetsAsync();
            MessageBox.Show(Lang("DataRefreshed"), Lang("Information"),
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Sử dụng form CreateFlashcardControl của User
        private void ShowUserCreateFlashcardForm()
        {
            // Tạo một form wrapper responsive để chứa CreateFlashcardControl
            var dialogForm = new Form
            {
                Text = Lang("CreateFlashcardSet"),
                Size = new Size(1000, 750),
                MinimumSize = new Size(800, 600),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.Sizable, // ✅ Allow resize
                MaximizeBox = true, // ✅ Allow maximize
                MinimizeBox = false
            };

            var createControl = new CreateFlashcardControl
            {
                Dock = DockStyle.Fill
            };

            dialogForm.Controls.Add(createControl);

            // ✅ Center on screen if no parent
            if (this.FindForm() == null)
            {
                dialogForm.StartPosition = FormStartPosition.CenterScreen;
            }

            // Hook vào sự kiện để đóng form sau khi tạo thành công
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
            // Tạo một form wrapper responsive để chứa EditFlashcardControl
            var dialogForm = new Form
            {
                Text = Lang("EditFlashcardSet"),
                Size = new Size(1000, 750),
                MinimumSize = new Size(800, 600),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.Sizable, // ✅ Allow resize
                MaximizeBox = true, // ✅ Allow maximize
                MinimizeBox = false
            };

            var editControl = new EditFlashcardControl(setId)
            {
                Dock = DockStyle.Fill
            };

            dialogForm.Controls.Add(editControl);

            // ✅ Center on screen if no parent
            if (this.FindForm() == null)
            {
                dialogForm.StartPosition = FormStartPosition.CenterScreen;
            }

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
                Text = Lang("FlashcardSetDetail"),
                Size = new Size(1200, 750),
                MinimumSize = new Size(900, 600),
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

            // ✅ Center on screen if no parent
            if (this.FindForm() == null)
            {
                dialogForm.StartPosition = FormStartPosition.CenterScreen;
            }

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
