using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Controls
{
    public partial class MyFlashcardsControl : UserControl
    {
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalRecords = 0;
        private List<FlashcardSet> _allFlashcardSets = new List<FlashcardSet>();

        public MyFlashcardsControl()
        {
            InitializeComponent();
            cmbPageSize.SelectedIndex = 0; // Set default to 10
            LoadFlashcardSets();
        }

        private async void LoadFlashcardSets()
        {
            try
            {
                var userId = AuthHelper.CurrentUser?.UserId;
                if (!userId.HasValue)
                {
                    MessageBox.Show("Vui lòng đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var context = new LearningPlatformContext();
                _allFlashcardSets = await context.FlashcardSets
                    .Include(fs => fs.Flashcards)
                    .Where(fs => fs.OwnerId == userId.Value && !fs.IsDeleted)
                    .OrderByDescending(fs => fs.CreatedAt)
                    .ToListAsync();

                _totalRecords = _allFlashcardSets.Count;
                ApplyFiltersAndLoadPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFiltersAndLoadPage()
        {
            var filteredSets = _allFlashcardSets.AsEnumerable();

            // Apply search filter
            string searchText = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredSets = filteredSets.Where(fs =>
                    fs.Title.ToLower().Contains(searchText) ||
                    (fs.Description != null && fs.Description.ToLower().Contains(searchText)));
            }

            _totalRecords = filteredSets.Count();

            // Calculate pagination
            int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            if (_currentPage > totalPages && totalPages > 0)
                _currentPage = totalPages;

            var pagedSets = filteredSets
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            LoadDataToGrid(pagedSets);
            UpdatePaginationUI(totalPages);
        }

        private void LoadDataToGrid(List<FlashcardSet> flashcardSets)
        {
            flowFlashcards.Controls.Clear();

            if (flashcardSets.Count == 0)
            {
                var lblEmpty = new Label
                {
                    Text = "Chưa có bộ flashcard nào",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    ForeColor = ColorPalette.TextSecondary,
                    AutoSize = true,
                    Location = new Point(400, 200)
                };
                flowFlashcards.Controls.Add(lblEmpty);
                return;
            }

            int rowIndex = (_currentPage - 1) * _pageSize + 1;
            foreach (var flashcardSet in flashcardSets)
            {
                var row = CreateFlashcardRow(flashcardSet, rowIndex++);
                flowFlashcards.Controls.Add(row);
            }
        }

        private Panel CreateFlashcardRow(FlashcardSet flashcardSet, int index)
        {
            var row = new Panel
            {
                Width = flowFlashcards.Width - 25,
                Height = 60,
                BackColor = Color.White,
                Margin = new Padding(0, 1, 0, 0),
                BorderStyle = BorderStyle.FixedSingle
            };

            // ID Column
            var lblId = new Label
            {
                Text = index.ToString(),
                Location = new Point(20, 20),
                Size = new Size(40, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextPrimary,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Title Column
            var lblTitle = new Label
            {
                Text = flashcardSet.Title,
                Location = new Point(175, 15),
                Size = new Size(450, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary,
                AutoEllipsis = true
            };

            // Card Count Badge
            var lblCardCount = new Label
            {
                Text = $"{flashcardSet.Flashcards.Count} thẻ",
                Location = new Point(629, 20),
                Size = new Size(80, 24),
                BackColor = Color.FromArgb(23, 162, 184), // Cyan
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Visibility Badge
            var lblVisibility = new Label
            {
                Text = flashcardSet.Visibility == "Public" ? "Công khai" : "Riêng tư",
                Location = new Point(765, 20),
                Size = new Size(100, 20),
                BackColor = flashcardSet.Visibility == "Public" 
                    ? Color.FromArgb(40, 167, 69)  // Green
                    : Color.FromArgb(108, 117, 125), // Gray
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Language
            var lblLanguage = new Label
            {
                Text = string.IsNullOrEmpty(flashcardSet.Language) ? "vi" : flashcardSet.Language,
                Location = new Point(899, 20),
                Size = new Size(80, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextPrimary,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Created Date
            var lblDate = new Label
            {
                Text = flashcardSet.CreatedAt.ToString("dd/MM/yyyy"),
                Location = new Point(1077, 20),
                Size = new Size(155, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextPrimary,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Action Buttons
            var btnView = CreateActionButton("👁️", ColorPalette.Primary, 1434, "Xem");
            btnView.Click += (s, e) => ViewFlashcardSet(flashcardSet);

            var btnStudy = CreateActionButton("▶️", Color.FromArgb(52, 144, 220), 1484, "Học");
            btnStudy.Click += (s, e) => StudyFlashcardSet(flashcardSet);

            var btnEdit = CreateActionButton("✏️", Color.FromArgb(255, 193, 7), 1534, "Sửa");
            btnEdit.Click += (s, e) => EditFlashcardSet(flashcardSet);

            var btnDelete = CreateActionButton("🗑️", Color.FromArgb(220, 53, 69), 1584, "Xóa");
            btnDelete.Click += (s, e) => DeleteFlashcardSet(flashcardSet);

            row.Controls.AddRange(new Control[] {
                lblId, lblTitle, lblCardCount, lblVisibility, lblLanguage, lblDate,
                btnView, btnStudy, btnEdit, btnDelete
            });

            // Hover effect
            row.MouseEnter += (s, e) => row.BackColor = ColorPalette.Background;
            row.MouseLeave += (s, e) => row.BackColor = Color.White;

            return row;
        }

        private Button CreateActionButton(string icon, Color color, int x, string tooltip)
        {
            var btn = new Button
            {
                Text = icon,
                Location = new Point(x, 13),
                Size = new Size(36, 34),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btn.FlatAppearance.BorderSize = 0;

            var toolTip = new ToolTip();
            toolTip.SetToolTip(btn, tooltip);

            return btn;
        }

        private void ViewFlashcardSet(FlashcardSet flashcardSet)
        {
            MessageBox.Show($"Xem bộ flashcard: {flashcardSet.Title}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // TODO: Navigate to flashcard set detail view
        }

        private void StudyFlashcardSet(FlashcardSet flashcardSet)
        {
            MessageBox.Show($"Học bộ flashcard: {flashcardSet.Title}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // TODO: Open flashcard study mode
        }

        private void EditFlashcardSet(FlashcardSet flashcardSet)
        {
            MessageBox.Show($"Chỉnh sửa bộ flashcard: {flashcardSet.Title}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // TODO: Open edit flashcard dialog/form
        }

        private async void DeleteFlashcardSet(FlashcardSet flashcardSet)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa bộ flashcard '{flashcardSet.Title}'?\n\nHành động này không thể hoàn tác.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using var context = new LearningPlatformContext();
                    var setToDelete = await context.FlashcardSets.FindAsync(flashcardSet.SetId);
                    if (setToDelete != null)
                    {
                        // Soft delete
                        setToDelete.IsDeleted = true;
                        await context.SaveChangesAsync();

                        ToastHelper.Show(this.FindForm(), "Đã xóa bộ flashcard thành công!");
                        LoadFlashcardSets();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa bộ flashcard: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdatePaginationUI(int totalPages)
        {
            lblPageInfo.Text = $"Hiển thị {(_currentPage - 1) * _pageSize + 1} tới {Math.Min(_currentPage * _pageSize, _totalRecords)} của {_totalRecords} dữ liệu";

            btnFirstPage.Enabled = _currentPage > 1;
            btnPrevPage.Enabled = _currentPage > 1;
            btnNextPage.Enabled = _currentPage < totalPages;
            btnLastPage.Enabled = _currentPage < totalPages;

            lblCurrentPage.Text = _currentPage.ToString();
        }

        private void BtnCreateFlashcard_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng tạo bộ flashcard mới đang được phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // TODO: Open create flashcard dialog
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            // Navigate back to MyCoursesControl
            var form = this.FindForm();
            if (form is MainContainer mainContainer)
            {
                var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
                if (mainPanel != null)
                {
                    mainPanel.Controls.Clear();
                    var myCoursesControl = new MyCoursesControl();
                    myCoursesControl.Dock = DockStyle.Fill;
                    mainPanel.Controls.Add(myCoursesControl);
                }
            }
        }

        private Control FindControlRecursive(Control parent, string name)
        {
            foreach (Control c in parent.Controls)
            {
                if (string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase)) return c;
                var found = FindControlRecursive(c, name);
                if (found != null) return found;
            }
            return null;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            ApplyFiltersAndLoadPage();
        }

        private void CmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPageSize.SelectedItem != null)
            {
                _pageSize = int.Parse(cmbPageSize.SelectedItem.ToString());
                _currentPage = 1;
                ApplyFiltersAndLoadPage();
            }
        }

        private void BtnFirstPage_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            ApplyFiltersAndLoadPage();
        }

        private void BtnPrevPage_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                ApplyFiltersAndLoadPage();
            }
        }

        private void BtnNextPage_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            if (_currentPage < totalPages)
            {
                _currentPage++;
                ApplyFiltersAndLoadPage();
            }
        }

        private void BtnLastPage_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            _currentPage = totalPages;
            ApplyFiltersAndLoadPage();
        }

        private void flowFlashcards_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
