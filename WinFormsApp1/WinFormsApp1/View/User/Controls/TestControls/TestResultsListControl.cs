using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using ClosedXML.Excel;

namespace WinFormsApp1.View.User.Controls.TestControls
{
    public partial class TestResultsListControl : UserControl
    {
        private int _testId;
        private Models.Entities.Test _test;
        private Control _previousControl;
        private List<StudentResultViewModel> _allResults;
        private List<StudentResultViewModel> _filteredResults;
        
        // Pagination
        private int _currentPage = 1;
        private int _pageSize = 20;
        private int _totalPages = 1;

        public TestResultsListControl()
        {
            InitializeComponent();
            InitializeEvents();
            ConfigureDataGridView();
        }

        private void InitializeEvents()
        {
            btnBack.Click += BtnBack_Click;
            btnExport.Click += BtnExport_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            cboSortBy.SelectedIndexChanged += CboSortBy_SelectedIndexChanged;
            dgvResults.CellDoubleClick += DgvResults_CellDoubleClick;
            dgvResults.CellClick += DgvResults_CellClick; // Thêm event cho button click
        }

        private void ConfigureDataGridView()
        {
            // Configure DataGridView appearance
            dgvResults.AutoGenerateColumns = false;
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.ReadOnly = true;
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.MultiSelect = false;
            dgvResults.RowHeadersVisible = false;
            dgvResults.BackgroundColor = Color.White;
            dgvResults.BorderStyle = BorderStyle.None;
            dgvResults.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvResults.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvResults.EnableHeadersVisualStyles = false;
            dgvResults.RowTemplate.Height = 40;

            // Scroll settings
            dgvResults.Dock = DockStyle.Fill;
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResults.ScrollBars = ScrollBars.Vertical;
            // BỎ: pnlContent.Padding - để Design file control

            // Header style
            dgvResults.ColumnHeadersDefaultCellStyle.BackColor = ColorPalette.Primary;
            dgvResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvResults.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvResults.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgvResults.ColumnHeadersHeight = 45;

            // Row style
            dgvResults.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 239, 254);
            dgvResults.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvResults.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvResults.GridColor = Color.FromArgb(230, 230, 230);
            dgvResults.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Setup sort combo
            cboSortBy.Items.AddRange(new object[]
            {
                "Tên (A-Z)",
                "Tên (Z-A)",
                "Điểm cao nhất",
                "Điểm thấp nhất",
                "Thời gian mới nhất",
                "Thời gian cũ nhất"
            });
            cboSortBy.SelectedIndex = 0;
        }

        public async Task LoadResultsAsync(int testId, Control previousControl = null)
        {
            _testId = testId;
            _previousControl = previousControl;

            try
            {
                using var context = new LearningPlatformContext();

                _test = await context.Tests
                    .Include(t => t.TestAttempts)
                        .ThenInclude(ta => ta.User)
                    .FirstOrDefaultAsync(t => t.TestId == testId);

                if (_test == null)
                {
                    MessageBox.Show("Không tìm thấy bài kiểm tra!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblTitle.Text = $"Danh sách kết quả: {_test.Title}";

                await LoadStudentResults();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadStudentResults()
        {
            try
            {
                using var context = new LearningPlatformContext();

                // Get all attempts with user info
                var attempts = await context.TestAttempts
                    .Include(ta => ta.User)
                    .Where(ta => ta.TestId == _testId)
                    .OrderByDescending(ta => ta.SubmittedAt ?? ta.StartedAt)
                    .ToListAsync();

                // Group by user and calculate statistics
                var studentResults = attempts
                    .GroupBy(ta => ta.UserId)
                    .Select(g => new StudentResultViewModel
                    {
                        UserId = g.Key,
                        StudentName = g.First().User.FullName,
                        Email = g.First().User.Email,
                        TotalAttempts = g.Count(),
                        HighestScore = g.Max(a => a.Score ?? 0),
                        LowestScore = g.Min(a => a.Score ?? 0),
                        AverageScore = g.Average(a => a.Score ?? 0),
                        MaxScore = g.First().MaxScore ?? 0,
                        LastAttemptDate = g.Max(a => a.SubmittedAt ?? a.StartedAt),
                        FirstAttemptDate = g.Min(a => a.StartedAt),
                        AverageTimeSpent = g.Average(a => a.TimeSpentSec ?? 0)
                    })
                    .ToList();

                _allResults = studentResults;
                _filteredResults = new List<StudentResultViewModel>(studentResults);

                // Check if no results
                if (_allResults == null || _allResults.Count == 0)
                {
                    _allResults = new List<StudentResultViewModel>();
                    _filteredResults = new List<StudentResultViewModel>();
                    lblStats.Text = "Chưa có học viên nào làm bài";
                    
                    // Clear grid and show empty message
                    dgvResults.Columns.Clear();
                    dgvResults.Rows.Clear();
                    return;
                }

                // Update statistics
                UpdateStatistics();

                // Apply initial sort
                ApplySort();

                // Load first page
                LoadPage(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatistics()
        {
            if (_allResults == null || _allResults.Count == 0)
            {
                lblStats.Text = "Chưa có học viên nào làm bài";
                return;
            }

            var totalStudents = _allResults.Count;
            var avgScore = _allResults.Average(r => r.AverageScore);
            var maxPossible = _allResults.First().MaxScore;
            
            // Tránh chia cho 0
            var passRate = _allResults.Count(r => r.MaxScore > 0 && (r.HighestScore / r.MaxScore) * 100 >= 60);
            var passPercentage = totalStudents > 0 ? (double)passRate / totalStudents * 100 : 0;

            lblStats.Text = $"Tổng: {totalStudents} học viên | " +
                           $"Điểm TB: {avgScore:F2}/{maxPossible:F2} | " +
                           $"Tỷ lệ đạt: {passPercentage:F1}% ({passRate}/{totalStudents})";
        }

        private void LoadPage(int pageNumber)
        {
            // Safety check
            if (_filteredResults == null)
            {
                _filteredResults = new List<StudentResultViewModel>();
            }

            _currentPage = pageNumber;
            _totalPages = (int)Math.Ceiling((double)_filteredResults.Count / _pageSize);

            if (_totalPages == 0) _totalPages = 1;
            if (_currentPage > _totalPages) _currentPage = _totalPages;
            if (_currentPage < 1) _currentPage = 1;

            var pagedResults = _filteredResults
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            DisplayResults(pagedResults);
            UpdatePaginationControls();
        }

        private void DisplayResults(List<StudentResultViewModel> results)
        {
            dgvResults.Columns.Clear();

            // Define columns - để Fill tự động chia đều
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "STT",
                HeaderText = "STT",
                ReadOnly = true,
                FillWeight = 50,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StudentName",
                HeaderText = "Họ và tên",
                ReadOnly = true,
                FillWeight = 150
            });

            dgvResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                ReadOnly = true,
                FillWeight = 200
            });

            dgvResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalAttempts",
                HeaderText = "Số lần làm",
                ReadOnly = true,
                FillWeight = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HighestScore",
                HeaderText = "Điểm cao nhất",
                ReadOnly = true,
                FillWeight = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AverageScore",
                HeaderText = "Điểm TB",
                ReadOnly = true,
                FillWeight = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Percentage",
                HeaderText = "Phần trăm",
                ReadOnly = true,
                FillWeight = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LastAttempt",
                HeaderText = "Lần làm cuối",
                ReadOnly = true,
                FillWeight = 130
            });

            dgvResults.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Action",
                HeaderText = "Thao tác",
                Text = "Xem chi tiết",
                UseColumnTextForButtonValue = true,
                FillWeight = 100
            });

            // Add rows
            dgvResults.Rows.Clear();
            int startIndex = (_currentPage - 1) * _pageSize;

            for (int i = 0; i < results.Count; i++)
            {
                var result = results[i];
                var percentage = result.MaxScore > 0 ? (result.HighestScore / result.MaxScore) * 100 : 0;

                var rowIndex = dgvResults.Rows.Add();
                var row = dgvResults.Rows[rowIndex];

                row.Cells["STT"].Value = startIndex + i + 1;
                row.Cells["StudentName"].Value = result.StudentName;
                row.Cells["Email"].Value = result.Email;
                row.Cells["TotalAttempts"].Value = result.TotalAttempts;
                row.Cells["HighestScore"].Value = $"{result.HighestScore:F2}/{result.MaxScore:F2}";
                row.Cells["AverageScore"].Value = $"{result.AverageScore:F2}";
                row.Cells["Percentage"].Value = $"{percentage:F1}%";
                row.Cells["LastAttempt"].Value = result.LastAttemptDate.ToString("dd/MM/yyyy HH:mm");

                // Color code based on percentage
                var scoreCell = row.Cells["Percentage"];
                if (percentage >= 80)
                    scoreCell.Style.ForeColor = ColorPalette.Success;
                else if (percentage >= 60)
                    scoreCell.Style.ForeColor = Color.FromArgb(255, 193, 7);
                else
                    scoreCell.Style.ForeColor = ColorPalette.Error;

                // Make percentage cell bold
                scoreCell.Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                row.Tag = result;
            }
        }

        private void UpdatePaginationControls()
        {
            // Remove old pagination controls
            var oldPagination = pnlContent.Controls.OfType<Panel>()
                .FirstOrDefault(p => p.Name == "pnlPagination");
            if (oldPagination != null)
                pnlContent.Controls.Remove(oldPagination);

            // Create pagination panel
            var pnlPagination = new Panel
            {
                Name = "pnlPagination",
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblPageInfo = new Label
            {
                Text = $"Trang {_currentPage} / {_totalPages} (Tổng: {_filteredResults.Count} kết quả)",
                AutoSize = true,
                Location = new Point(20, 18),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.Primary
            };

            var btnFirst = new Button
            {
                Text = "⏮ Đầu",
                Size = new Size(90, 35),
                Location = new Point(400, 13),
                Enabled = _currentPage > 1,
                Cursor = Cursors.Hand,
                BackColor = _currentPage > 1 ? ColorPalette.Primary : Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnFirst.FlatAppearance.BorderSize = 0;
            btnFirst.Click += (s, e) => LoadPage(1);

            var btnPrev = new Button
            {
                Text = "◀ Trước",
                Size = new Size(90, 35),
                Location = new Point(500, 13),
                Enabled = _currentPage > 1,
                Cursor = Cursors.Hand,
                BackColor = _currentPage > 1 ? ColorPalette.Primary : Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnPrev.FlatAppearance.BorderSize = 0;
            btnPrev.Click += (s, e) => LoadPage(_currentPage - 1);

            var btnNext = new Button
            {
                Text = "Sau ▶",
                Size = new Size(90, 35),
                Location = new Point(600, 13),
                Enabled = _currentPage < _totalPages,
                Cursor = Cursors.Hand,
                BackColor = _currentPage < _totalPages ? ColorPalette.Primary : Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Click += (s, e) => LoadPage(_currentPage + 1);

            var btnLast = new Button
            {
                Text = "Cuối ⏭",
                Size = new Size(90, 35),
                Location = new Point(700, 13),
                Enabled = _currentPage < _totalPages,
                Cursor = Cursors.Hand,
                BackColor = _currentPage < _totalPages ? ColorPalette.Primary : Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnLast.FlatAppearance.BorderSize = 0;
            btnLast.Click += (s, e) => LoadPage(_totalPages);

            pnlPagination.Controls.AddRange(new Control[] { lblPageInfo, btnFirst, btnPrev, btnNext, btnLast });
            
            // SỬA: Đặt Dock của dgvResults để không bị overlap
            dgvResults.Dock = DockStyle.Fill;
            
            pnlContent.Controls.Add(pnlPagination);
            pnlPagination.BringToFront();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void CboSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplySort();
            LoadPage(1);
        }

        private void ApplyFilters()
        {
            // Safety check
            if (_allResults == null)
            {
                _allResults = new List<StudentResultViewModel>();
                _filteredResults = new List<StudentResultViewModel>();
                return;
            }

            var searchText = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                _filteredResults = new List<StudentResultViewModel>(_allResults);
            }
            else
            {
                _filteredResults = _allResults
                    .Where(r => r.StudentName.ToLower().Contains(searchText) ||
                               r.Email.ToLower().Contains(searchText))
                    .ToList();
            }

            ApplySort();
            LoadPage(1);
        }

        private void ApplySort()
        {
            if (_filteredResults == null || cboSortBy.SelectedIndex < 0) return;

            switch (cboSortBy.SelectedIndex)
            {
                case 0: // Tên A-Z
                    _filteredResults = _filteredResults.OrderBy(r => r.StudentName).ToList();
                    break;
                case 1: // Tên Z-A
                    _filteredResults = _filteredResults.OrderByDescending(r => r.StudentName).ToList();
                    break;
                case 2: // Điểm cao nhất
                    _filteredResults = _filteredResults.OrderByDescending(r => r.HighestScore).ToList();
                    break;
                case 3: // Điểm thấp nhất
                    _filteredResults = _filteredResults.OrderBy(r => r.HighestScore).ToList();
                    break;
                case 4: // Thời gian mới nhất
                    _filteredResults = _filteredResults.OrderByDescending(r => r.LastAttemptDate).ToList();
                    break;
                case 5: // Thời gian cũ nhất
                    _filteredResults = _filteredResults.OrderBy(r => r.FirstAttemptDate).ToList();
                    break;
            }
        }

        private void DgvResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var result = dgvResults.Rows[e.RowIndex].Tag as StudentResultViewModel;
            if (result != null)
            {
                ViewStudentDetails(result.UserId);
            }
        }

        private void DgvResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu click vào cột Action
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var column = dgvResults.Columns[e.ColumnIndex];
                if (column.Name == "Action")
                {
                    var result = dgvResults.Rows[e.RowIndex].Tag as StudentResultViewModel;
                    if (result != null)
                    {
                        ViewStudentDetails(result.UserId);
                    }
                }
            }
        }

        private void ViewStudentDetails(int userId)
        {
            try
            {
                var detailForm = new StudentTestDetailForm(_testId, userId);
                detailForm.ShowDialog(this.FindForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using var sfd = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    FileName = $"KetQua_{_test.Title}_{DateTime.Now:yyyyMMdd}.xlsx"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Kết quả");

                // Headers
                worksheet.Cell(1, 1).Value = "STT";
                worksheet.Cell(1, 2).Value = "Họ và tên";
                worksheet.Cell(1, 3).Value = "Email";
                worksheet.Cell(1, 4).Value = "Số lần làm";
                worksheet.Cell(1, 5).Value = "Điểm cao nhất";
                worksheet.Cell(1, 6).Value = "Điểm TB";
                worksheet.Cell(1, 7).Value = "Phần trăm";
                worksheet.Cell(1, 8).Value = "Lần làm cuối";

                // Style headers
                var headerRange = worksheet.Range(1, 1, 1, 8);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Data
                for (int i = 0; i < _filteredResults.Count; i++)
                {
                    var result = _filteredResults[i];
                    var percentage = result.MaxScore > 0 ? (result.HighestScore / result.MaxScore) * 100 : 0;
                    var row = i + 2;

                    worksheet.Cell(row, 1).Value = i + 1;
                    worksheet.Cell(row, 2).Value = result.StudentName;
                    worksheet.Cell(row, 3).Value = result.Email;
                    worksheet.Cell(row, 4).Value = result.TotalAttempts;
                    worksheet.Cell(row, 5).Value = $"{result.HighestScore:F2}/{result.MaxScore:F2}";
                    worksheet.Cell(row, 6).Value = result.AverageScore;
                    worksheet.Cell(row, 7).Value = $"{percentage:F1}%";
                    worksheet.Cell(row, 8).Value = result.LastAttemptDate.ToString("dd/MM/yyyy HH:mm");
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(sfd.FileName);

                MessageBox.Show("Xuất Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                var parentPanel = this.Parent as Panel;
                if (parentPanel == null) return;

                parentPanel.Controls.Clear();

                if (_previousControl != null)
                {
                    _previousControl.Dock = DockStyle.Fill;
                    parentPanel.Controls.Add(_previousControl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi quay lại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // ViewModel for student results
    public class StudentResultViewModel
    {
        public int UserId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public int TotalAttempts { get; set; }
        public decimal HighestScore { get; set; }
        public decimal LowestScore { get; set; }
        public decimal AverageScore { get; set; }
        public decimal MaxScore { get; set; }
        public DateTime LastAttemptDate { get; set; }
        public DateTime FirstAttemptDate { get; set; }
        public double AverageTimeSpent { get; set; }
    }
}
