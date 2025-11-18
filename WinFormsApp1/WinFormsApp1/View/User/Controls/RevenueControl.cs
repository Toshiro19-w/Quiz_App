using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.ViewModels;

namespace WinFormsApp1.View.User.Controls
{
    public partial class RevenueControl : UserControl
    {
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalRecords = 0;
        private List<CourseRevenueViewModel> _allRevenues = new List<CourseRevenueViewModel>();
        private RevenueOverviewViewModel _overview;

        public RevenueControl()
        {
            InitializeComponent();
            cmbPageSize.SelectedIndex = 0;
            LoadRevenueData();
        }

        private async void LoadRevenueData()
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

                // Load revenue overview
                var overviewQuery = await context.CoursePurchases
                    .Include(cp => cp.Course)
                    .Where(cp => cp.Course.OwnerId == userId.Value && cp.Status == "Paid")
                    .GroupBy(cp => 1)
                    .Select(g => new RevenueOverviewViewModel
                    {
                        TotalPurchases = g.Count(),
                        TotalGrossRevenue = g.Sum(cp => cp.PricePaid),
                        TotalInstructorRevenue = g.Sum(cp => cp.PricePaid * 0.6m),
                        TotalPlatformFee = g.Sum(cp => cp.PricePaid * 0.4m)
                    })
                    .FirstOrDefaultAsync();

                _overview = overviewQuery ?? new RevenueOverviewViewModel();

                // Load detailed revenue by course
                _allRevenues = await context.Courses
                    .Where(c => c.OwnerId == userId.Value)
                    .Select(c => new CourseRevenueViewModel
                    {
                        CourseId = c.CourseId,
                        CourseTitle = c.Title,
                        CoursePrice = c.Price,
                        TotalPurchases = c.CoursePurchases.Count(cp => cp.Status == "Paid"),
                        GrossRevenue = c.CoursePurchases
                            .Where(cp => cp.Status == "Paid")
                            .Sum(cp => (decimal?)cp.PricePaid) ?? 0,
                        InstructorRevenue = c.CoursePurchases
                            .Where(cp => cp.Status == "Paid")
                            .Sum(cp => (decimal?)(cp.PricePaid * 0.6m)) ?? 0,
                        PlatformFee = c.CoursePurchases
                            .Where(cp => cp.Status == "Paid")
                            .Sum(cp => (decimal?)(cp.PricePaid * 0.4m)) ?? 0
                    })
                    .OrderByDescending(r => r.InstructorRevenue)
                    .ToListAsync();

                _totalRecords = _allRevenues.Count;

                UpdateOverviewCards();
                ApplyFiltersAndLoadPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateOverviewCards()
        {
            // Update summary cards
            lblTotalPurchases.Text = _overview.TotalPurchases.ToString();
            lblTotalGrossRevenue.Text = $"{_overview.TotalGrossRevenue:N0} VNĐ";
            lblInstructorRevenue.Text = $"{_overview.TotalInstructorRevenue:N0} VNĐ";
            lblPlatformFee.Text = $"{_overview.TotalPlatformFee:N0} VNĐ";
        }

        private void ApplyFiltersAndLoadPage()
        {
            var filteredRevenues = _allRevenues.AsEnumerable();

            // Apply search filter
            string searchText = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredRevenues = filteredRevenues.Where(r =>
                    r.CourseTitle.ToLower().Contains(searchText));
            }

            _totalRecords = filteredRevenues.Count();

            // Calculate pagination
            int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            if (_currentPage > totalPages && totalPages > 0)
                _currentPage = totalPages;

            var pagedRevenues = filteredRevenues
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            LoadDataToGrid(pagedRevenues);
            UpdatePaginationUI(totalPages);
        }

        private void LoadDataToGrid(List<CourseRevenueViewModel> revenues)
        {
            flowRevenues.Controls.Clear();

            if (revenues.Count == 0)
            {
                var lblEmpty = new Label
                {
                    Text = "Chưa có doanh thu nào",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    ForeColor = ColorPalette.TextSecondary,
                    AutoSize = true,
                    Location = new Point(400, 200)
                };
                flowRevenues.Controls.Add(lblEmpty);
                return;
            }

            foreach (var revenue in revenues)
            {
                var row = CreateRevenueRow(revenue);
                flowRevenues.Controls.Add(row);
            }
        }

        private Panel CreateRevenueRow(CourseRevenueViewModel revenue)
        {
            var row = new Panel
            {
                Width = flowRevenues.Width - 25,
                Height = 70,
                BackColor = Color.White,
                Margin = new Padding(0, 1, 0, 0),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Course Title
            var lblTitle = new Label
            {
                Text = revenue.CourseTitle,
                Location = new Point(20, 10),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary,
                AutoEllipsis = true
            };

            var lblId = new Label
            {
                Text = $"ID: {revenue.CourseId}",
                Location = new Point(20, 35),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = ColorPalette.TextSecondary
            };

            // Price
            var lblPrice = new Label
            {
                Text = $"{revenue.CoursePrice:N0} VNĐ",
                Location = new Point(664, 25),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextPrimary,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Total Purchases Badge
            var lblPurchases = new Label
            {
                Text = revenue.TotalPurchases.ToString(),
                Location = new Point(857, 25),
                Size = new Size(60, 30),
                BackColor = Color.FromArgb(52, 144, 220),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Gross Revenue
            var lblGrossRevenue = new Label
            {
                Text = $"{revenue.GrossRevenue:N0} VNĐ",
                Location = new Point(1043, 25),
                Size = new Size(180, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(23, 162, 184),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Instructor Revenue (60%)
            var lblInstructorRev = new Label
            {
                Text = $"{revenue.InstructorRevenue:N0} VNĐ",
                Location = new Point(1357, 25),
                Size = new Size(180, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 167, 69),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Platform Fee (40%)
            var lblPlatformFee = new Label
            {
                Text = $"{revenue.PlatformFee:N0} VNĐ",
                Location = new Point(1629, 25),
                Size = new Size(180, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(255, 193, 7),
                TextAlign = ContentAlignment.MiddleCenter
            };

            row.Controls.AddRange(new Control[] {
                lblTitle, lblId, lblPrice, lblPurchases,
                lblGrossRevenue, lblInstructorRev, lblPlatformFee
            });

            // Hover effect
            row.MouseEnter += (s, e) => row.BackColor = ColorPalette.Background;
            row.MouseLeave += (s, e) => row.BackColor = Color.White;

            return row;
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
    }
}
