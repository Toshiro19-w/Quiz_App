using Guna.UI2.AnimatorNS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.View.User.Forms;

namespace WinFormsApp1.View.User.Controls
{
    public partial class MyCoursesControl : UserControl
    {
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalRecords = 0;
        private List<Course> _allCourses = new List<Course>();
		public MyCoursesControl()
        {
            InitializeComponent();
            cmbPageSize.SelectedIndex = 0; // Set default to 10
            LoadCourses();
        }

        private async void LoadCourses()
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
                _allCourses = await context.Courses
                    .Where(c => c.OwnerId == userId.Value)
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync();

                _totalRecords = _allCourses.Count;
                ApplyFiltersAndLoadPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFiltersAndLoadPage()
        {
            var filteredCourses = _allCourses.AsEnumerable();

            // Apply search filter
            string searchText = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredCourses = filteredCourses.Where(c =>
                    c.Title.ToLower().Contains(searchText) ||
                    (c.Slug != null && c.Slug.ToLower().Contains(searchText)));
            }

            _totalRecords = filteredCourses.Count();

            // Calculate pagination
            int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            if (_currentPage > totalPages && totalPages > 0)
                _currentPage = totalPages;

            var pagedCourses = filteredCourses
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            LoadDataToGrid(pagedCourses);
            UpdatePaginationUI(totalPages);
        }

        private void LoadDataToGrid(List<Course> courses)
        {
            flowCourses.Controls.Clear();

            if (courses.Count == 0)
            {
                var lblEmpty = new Label
                {
                    Text = "Chưa có khóa học nào",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    ForeColor = ColorPalette.TextSecondary,
                    AutoSize = true,
                    Location = new Point(400, 200)
                };
                flowCourses.Controls.Add(lblEmpty);
                return;
            }

            int rowIndex = (_currentPage - 1) * _pageSize + 1;
            foreach (var course in courses)
            {
                var row = CreateCourseRow(course, rowIndex++);
                flowCourses.Controls.Add(row);
            }
        }

        private Panel CreateCourseRow(Course course, int index)
        {
            var row = new Panel
            {
                Width = flowCourses.Width - 25,
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
            var pnlTitle = new Panel
            {
                Location = new Point(175, 10),
                Size = new Size(380, 54),
                BackColor = Color.Transparent
            };

            var lblTitle = new Label
            {
                Text = course.Title,
                Location = new Point(0, 0),
                Size = new Size(380, 42),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary,
                AutoEllipsis = true
            };
            
            pnlTitle.Controls.AddRange(new Control[] { lblTitle });

            // Moderation Status Column
            var moderationStatus = GetModerationStatusDisplay(course.ModerationStatus);
            var lblModeration = new Label
            {
                Text = moderationStatus.Text,
                Location = new Point(560, 15),
                Size = new Size(120, 24),
                BackColor = moderationStatus.Color,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Status Column
            var lblStatus = new Label
            {
                Text = course.IsPublished ? "Đã xuất bản" : "Nháp",
                Location = new Point(690, 15),
                Size = new Size(100, 24),
                BackColor = course.IsPublished ? Color.FromArgb(40, 167, 69) : Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Price Column
            var lblPrice = new Label
            {
                Text = $"{course.Price:N0} VNĐ",
                Location = new Point(800, 15),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextPrimary,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Created Date Column
            var lblDate = new Label
            {
                Text = course.CreatedAt.ToString("dd/MM/yyyy"),
                Location = new Point(930, 15),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextPrimary,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Action Buttons
            var btnSubmit = CreateActionButton("📤", Color.FromArgb(52, 144, 220), 1040, "Gửi duyệt");
            btnSubmit.Click += (s, e) => SubmitForReview(course);
            btnSubmit.Visible = (course.ModerationStatus != "Pending" && course.ModerationStatus != "Approved");

            var btnView = CreateActionButton("👁️", ColorPalette.Primary, 1086, "Xem");
            btnView.Click += (s, e) => ViewCourse(course);

            var btnEdit = CreateActionButton("✏️", Color.FromArgb(255, 193, 7), 1132, "Sửa");
            btnEdit.Click += (s, e) => EditCourse(course);

            var btnDelete = CreateActionButton("🗑️", Color.FromArgb(220, 53, 69), 1178, "Xóa");
            btnDelete.Click += (s, e) => DeleteCourse(course);

            row.Controls.AddRange(new Control[] {
                lblId, pnlTitle, lblModeration, lblStatus, lblPrice, lblDate,
                btnSubmit, btnView, btnEdit, btnDelete
            });

            // Hover effect
            row.MouseEnter += (s, e) => row.BackColor = ColorPalette.Background;
            row.MouseLeave += (s, e) => row.BackColor = Color.White;

            return row;
        }
        
        private (string Text, Color Color) GetModerationStatusDisplay(string status)
        {
            return status switch
            {
                "Pending" => ("Chờ duyệt", Color.FromArgb(255, 193, 7)),
                "Approved" => ("Đã duyệt", Color.FromArgb(40, 167, 69)),
                "Rejected" => ("Từ chối", Color.FromArgb(220, 53, 69)),
                "NeedsRevision" => ("Cần sửa", Color.FromArgb(255, 152, 0)),
                _ => ("Chưa gửi", Color.FromArgb(108, 117, 125))
            };
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

        private async void SubmitForReview(Course course)
        {
            try
            {
                using var context = new LearningPlatformContext();
                
                // Load course with full details for auto-check
                var fullCourse = await context.Courses
                    .Include(c => c.CourseChapters)
                        .ThenInclude(ch => ch.Lessons)
                            .ThenInclude(l => l.LessonContents)
                    .FirstOrDefaultAsync(c => c.CourseId == course.CourseId);
                
                if (fullCourse == null)
                {
                    MessageBox.Show("Không tìm thấy khóa học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Run auto-check
                var autoCheckResults = Services.CourseModerationService.RunAutoChecks(fullCourse, context);
                var canPublish = Services.CourseModerationService.CanPublish(autoCheckResults);
                var autoScore = Services.CourseModerationService.CalculateAutoScore(autoCheckResults);
                
                // Show auto-check results
                var errorCount = autoCheckResults.Count(r => r.Severity == "Error" && !r.Passed);
                var warningCount = autoCheckResults.Count(r => r.Severity == "Warning" && !r.Passed);
                
                var message = $"Kết quả kiểm tra tự động:\n\n";
                message += $"Điểm: {autoScore}/100\n";
                message += $"Lỗi: {errorCount}\n";
                message += $"Cảnh báo: {warningCount}\n\n";
                
                if (!canPublish)
                {
                    message += "❌ Khóa học chưa đủ điều kiện gửi duyệt.\n\n";
                    message += "Các lỗi cần sửa:\n";
                    foreach (var result in autoCheckResults.Where(r => r.Severity == "Error" && !r.Passed))
                    {
                        message += $"• {result.Message}\n";
                    }
                    
                    MessageBox.Show(message, "Không thể gửi duyệt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (warningCount > 0)
                {
                    message += "⚠️ Có một số cảnh báo. Bạn có muốn tiếp tục gửi duyệt?\n\n";
                    message += "Các cảnh báo:\n";
                    foreach (var checkResult in autoCheckResults.Where(r => r.Severity == "Warning" && !r.Passed))
                    {
                        message += $"• {checkResult.Message}\n";
                    }
                    
                    var confirmResult = MessageBox.Show(message, "Xác nhận gửi duyệt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmResult != DialogResult.Yes) return;
                }
                else
                {
                    message += "✅ Khóa học đã sẵn sàng để gửi duyệt.\n\nBạn có muốn gửi khóa học để admin kiểm duyệt?";
                    var confirmResult = MessageBox.Show(message, "Xác nhận gửi duyệt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmResult != DialogResult.Yes) return;
                }
                
                // Submit for review
                if (Services.CourseModerationService.SubmitForReview(course.CourseId, context))
                {
                    MessageBox.Show("Đã gửi khóa học để kiểm duyệt!\n\nAdmin sẽ xem xét và phản hồi trong thời gian sớm nhất.", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCourses();
                }
                else
                {
                    MessageBox.Show("Không thể gửi khóa học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ViewCourse(Course course)
        {
            try
            {
                using var context = new LearningPlatformContext();
                
                // Load course with chapters and lessons
                var courseWithDetails = await context.Courses
                    .Include(c => c.CourseChapters)
                        .ThenInclude(ch => ch.Lessons)
                    .FirstOrDefaultAsync(c => c.CourseId == course.CourseId);

                if (courseWithDetails == null)
                {
                    MessageBox.Show("Không tìm thấy khóa học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get first lesson
                var firstLesson = courseWithDetails.CourseChapters
                    .OrderBy(ch => ch.OrderIndex)
                    .SelectMany(ch => ch.Lessons.OrderBy(l => l.OrderIndex))
                    .FirstOrDefault();

                if (firstLesson == null)
                {
                    MessageBox.Show("Khóa học chưa có bài học nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Navigate to LessonDetailControl
                var form = this.FindForm();
                if (form is MainContainer mainContainer)
                {
                    var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
                    if (mainPanel != null)
                    {
                        mainPanel.Controls.Clear();

                        var lessonDetailControl = new LessonDetailControl();
                        lessonDetailControl.Dock = DockStyle.Fill;
                        mainPanel.Controls.Add(lessonDetailControl);

                        // Load lesson
                        await lessonDetailControl.LoadLessonAsync(courseWithDetails.Slug, firstLesson.LessonId);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở khóa học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void EditCourse(Course course)
        {
            try
            {
                var builderCtrl = new CourseBuilderController();
                var vm = await builderCtrl.LoadCourseAsync(course.CourseId);
                
                if (vm == null)
                {
                    MessageBox.Show("Không thể tải dữ liệu khóa học", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                using var form = new CourseBuilderForm(vm, course.CourseId);
                form.StartPosition = FormStartPosition.CenterParent;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadCourses();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở trình chỉnh sửa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DeleteCourse(Course course)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khóa học '{course.Title}'?\n\nHành động này không thể hoàn tác.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using var context = new LearningPlatformContext();
                    var courseToDelete = await context.Courses.FindAsync(course.CourseId);
                    if (courseToDelete != null)
                    {
                        context.Courses.Remove(courseToDelete);
                        await context.SaveChangesAsync();

                        ToastHelper.Show(this.FindForm(), "Đã xóa khóa học thành công!");
                        LoadCourses();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa khóa học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void BtnCreateCourse_Click(object sender, EventArgs e)
        {
            // Open CourseBuilderForm as dialog to create a new course
            using var builder = new CourseBuilderForm();
            builder.StartPosition = FormStartPosition.CenterParent;
            var owner = this.FindForm();
            if (owner != null)
            {
                builder.ShowDialog(owner);
            }
            else
            {
                builder.ShowDialog();
            }

            if (builder.DialogResult == DialogResult.OK)
            {
                // Reload courses after successful creation
                LoadCourses();
            }
        }

        private void BtnRevenue_Click(object sender, EventArgs e)
        {
            // Navigate to RevenueControl
            var form = this.FindForm();
            if (form is MainContainer mainContainer)
            {
                var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
                if (mainPanel != null)
                {
                    mainPanel.Controls.Clear();
                    var revenueControl = new RevenueControl();
                    revenueControl.Dock = DockStyle.Fill;
                    mainPanel.Controls.Add(revenueControl);
                }
            }
        }

        private void BtnFlashcards_Click(object sender, EventArgs e)
        {
            // Navigate to MyFlashcardsControl
            var form = this.FindForm();
            if (form is MainContainer mainContainer)
            {
                var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
                if (mainPanel != null)
                {
                    mainPanel.Controls.Clear();
                    var myFlashcardsControl = new MyFlashcardsControl();
                    myFlashcardsControl.Dock = DockStyle.Fill;
                    mainPanel.Controls.Add(myFlashcardsControl);
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

        private void flowCourses_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
