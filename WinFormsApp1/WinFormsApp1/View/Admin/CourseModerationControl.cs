using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Services;

namespace WinFormsApp1.View.Admin
{
    public partial class CourseModerationControl : AdminBaseControl
    {
        private List<Course> _pendingCourses = new List<Course>();
        private Course _selectedCourse;

        public CourseModerationControl() : base()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Create main components
            dataGridView = CreateModernDataGridView();
            
            // Setup layout FIRST
            SetupLayout("Kiểm duyệt khóa học", dataGridView);
            
            // Wire events
            WireCrudEvents();
            SetupCustomButtons();
            SetupDataGridViewEvents();
            
            // Setup custom filters AFTER layout
            SetupCustomFilters();
            
            this.ResumeLayout();
            
            // Load data
            _ = LoadPendingCoursesAsync();
        }

        /// <summary>
        /// Setup custom filters for Course Moderation
        /// </summary>
        private void SetupCustomFilters()
        {
            // Moderation Status Filter
            var statusCombo = new ComboBox
            {
                Name = "cboModerationStatus",
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            statusCombo.Items.AddRange(new object[] { "Tất cả", "Chờ duyệt", "Đã duyệt", "Từ chối", "Cần sửa" });
            statusCombo.SelectedIndex = 1; // Default: Chờ duyệt
            statusCombo.SelectedIndexChanged += async (s, e) => await LoadPendingCoursesAsync();

            // Add filter using the new helper method
            AddCustomFilter("Trạng thái:", statusCombo);
            
            // Adjust search box width to avoid overlap
            if (searchBox != null)
            {
                searchBox.Width = 200;
            }
        }

        private void SetupCustomButtons()
        {
            // Hide default Add button
            var addBtn = this.Controls.Find("btnAdd", true).FirstOrDefault() as Button;
            if (addBtn != null) addBtn.Visible = false;

            var buttonPanel = this.Controls.Find("btnAdd", true).FirstOrDefault()?.Parent as Panel;
            if (buttonPanel == null) return;

            // Clear all existing buttons to rearrange
            var existingButtons = buttonPanel.Controls.OfType<Button>().ToList();
            foreach (var btn in existingButtons)
            {
                buttonPanel.Controls.Remove(btn);
            }

            int xPos = 20;
            int spacing = 10;

            // 1. Xem chi tiết (Edit button)
            var btnViewDetail = new Button
            {
                Text = "Xem chi tiết",
                Size = new Size(110, 35),
                Location = new Point(xPos, 12),
                BackColor = Color.FromArgb(52, 144, 220),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Name = "btnEdit"
            };
            btnViewDetail.FlatAppearance.BorderSize = 0;
            btnViewDetail.Click += OnEditButtonClick;
            buttonPanel.Controls.Add(btnViewDetail);
            xPos += btnViewDetail.Width + spacing;

            // 2. Phê duyệt
            var btnApprove = new Button
            {
                Text = "Phê duyệt",
                Size = new Size(100, 35),
                Location = new Point(xPos, 12),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Name = "btnApprove"
            };
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.Click += BtnApprove_Click;
            buttonPanel.Controls.Add(btnApprove);
            xPos += btnApprove.Width + spacing;

            // 3. Yêu cầu sửa
            var btnRequestRevision = new Button
            {
                Text = "Yêu cầu sửa",
                Size = new Size(110, 35),
                Location = new Point(xPos, 12),
                BackColor = Color.FromArgb(255, 193, 7),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Name = "btnRequestRevision"
            };
            btnRequestRevision.FlatAppearance.BorderSize = 0;
            btnRequestRevision.Click += BtnRequestRevision_Click;
            buttonPanel.Controls.Add(btnRequestRevision);
            xPos += btnRequestRevision.Width + spacing;

            // 4. Từ chối
            var btnReject = new Button
            {
                Text = "Từ chối",
                Size = new Size(80, 35),
                Location = new Point(xPos, 12),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Name = "btnDelete"
            };
            btnReject.FlatAppearance.BorderSize = 0;
            btnReject.Click += OnDeleteButtonClick;
            buttonPanel.Controls.Add(btnReject);
            xPos += btnReject.Width + spacing;

            // 5. Làm mới
            var btnRefresh = new Button
            {
                Text = "Làm mới",
                Size = new Size(90, 35),
                Location = new Point(xPos, 12),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Name = "btnRefresh"
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += OnRefreshButtonClick;
            buttonPanel.Controls.Add(btnRefresh);
        }

        private void SetupDataGridViewEvents()
        {
            dataGridView.SelectionChanged += (s, e) =>
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    var courseId = (int)dataGridView.SelectedRows[0].Cells["CourseId"].Value;
                    _selectedCourse = _pendingCourses.FirstOrDefault(c => c.CourseId == courseId);
                }
            };

            dataGridView.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    ShowCourseDetailDialog();
                }
            };
        }

        private async Task LoadPendingCoursesAsync()
        {
            try
            {
                // Tìm ComboBox với tên mới
                var statusCombo = this.Controls.Find("cboModerationStatus", true).FirstOrDefault() as ComboBox;
                var selectedStatus = statusCombo?.SelectedItem?.ToString() ?? "Chờ duyệt";

                using var context = new LearningPlatformContext();

                var query = context.Courses
                    .Include(c => c.Owner)
                    .Include(c => c.Category)
                    .Include(c => c.CourseChapters)
                        .ThenInclude(ch => ch.Lessons)
                            .ThenInclude(l => l.LessonContents)
                    .AsQueryable();

                // Filter by status
                query = selectedStatus switch
                {
                    "Chờ duyệt" => query.Where(c => c.ModerationStatus == "Pending"),
                    "Đã duyệt" => query.Where(c => c.ModerationStatus == "Approved"),
                    "Từ chối" => query.Where(c => c.ModerationStatus == "Rejected"),
                    "Cần sửa" => query.Where(c => c.ModerationStatus == "NeedsRevision"),
                    _ => query
                };

                _pendingCourses = await query
                    .OrderBy(c => c.SubmittedForReviewAt)
                    .ToListAsync();

                DisplayCourses();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayCourses()
        {
            var displayData = _pendingCourses.Select(c =>
            {
                var autoCheckResults = new List<CourseModerationService.AutoCheckResult>();
                if (!string.IsNullOrEmpty(c.AutoCheckResults))
                {
                    try
                    {
                        autoCheckResults = JsonSerializer.Deserialize<List<CourseModerationService.AutoCheckResult>>(c.AutoCheckResults);
                    }
                    catch { }
                }

                var autoScore = CourseModerationService.CalculateAutoScore(autoCheckResults);
                var lessonCount = c.CourseChapters.Sum(ch => ch.Lessons.Count);

                return new
                {
                    c.CourseId,
                    c.Title,
                    GiảngViên = c.Owner.FullName,
                    DanhMục = c.Category?.Name ?? "Chưa phân loại",
                    SốChương = c.CourseChapters.Count,
                    SốBàiHọc = lessonCount,
                    Giá = c.Price,
                    ĐiểmTựĐộng = autoScore,
                    TrạngThái = GetStatusText(c.ModerationStatus),
                    NgàyGửi = c.SubmittedForReviewAt?.ToString("dd/MM/yyyy HH:mm") ?? "N/A"
                };
            }).ToList();

            dataGridView.DataSource = displayData;

            // Update column headers
            UpdateDataGridHeaders(dataGridView, new Dictionary<string, string>
            {
                { "CourseId", "ID" },
                { "Title", "Tên khóa học" },
                { "GiảngViên", "Giảng viên" },
                { "DanhMục", "Danh mục" },
                { "SốChương", "Số chương" },
                { "SốBàiHọc", "Số bài học" },
                { "Giá", "Giá" },
                { "ĐiểmTựĐộng", "Điểm tự động" },
                { "TrạngThái", "Trạng thái" },
                { "NgàyGửi", "Ngày gửi" }
            });

            // Format price column
            if (dataGridView.Columns["Giá"] != null)
            {
                dataGridView.Columns["Giá"].DefaultCellStyle.Format = "N0";
            }

            // Color code auto score
            if (dataGridView.Columns["ĐiểmTựĐộng"] != null)
            {
                dataGridView.CellFormatting += (s, e) =>
                {
                    if (e.ColumnIndex == dataGridView.Columns["ĐiểmTựĐộng"].Index && e.Value != null)
                    {
                        var score = (int)e.Value;
                        if (score >= 80)
                            e.CellStyle.BackColor = Color.FromArgb(200, 255, 200); // Green
                        else if (score >= 60)
                            e.CellStyle.BackColor = Color.FromArgb(255, 255, 200); // Yellow
                        else
                            e.CellStyle.BackColor = Color.FromArgb(255, 200, 200); // Red
                    }
                };
            }
        }

        private string GetStatusText(string status)
        {
            return status switch
            {
                "Pending" => "Chờ duyệt",
                "Approved" => "Đã duyệt",
                "Rejected" => "Từ chối",
                "NeedsRevision" => "Cần sửa",
                _ => status
            };
        }

        protected override void OnEditButtonClick(object sender, EventArgs e)
        {
            ShowCourseDetailDialog();
        }

        protected override void OnDeleteButtonClick(object sender, EventArgs e)
        {
            RejectCourse();
        }

        protected override async void OnRefreshButtonClick(object sender, EventArgs e)
        {
            await LoadPendingCoursesAsync();
        }

        private void BtnApprove_Click(object sender, EventArgs e)
        {
            ApproveCourse();
        }

        private void BtnRequestRevision_Click(object sender, EventArgs e)
        {
            RequestRevision();
        }

        private void ShowCourseDetailDialog()
        {
            if (_selectedCourse == null)
            {
                MessageBox.Show("Vui lòng chọn khóa học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var form = new Form
            {
                Text = "Chi tiết khóa học - Kiểm duyệt",
                Size = new Size(900, 700),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.Sizable
            };

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20)
            };

            int yPos = 20;

            // Course info
            panel.Controls.Add(CreateLabel("Thông tin khóa học", new Point(20, yPos), new Font("Segoe UI", 14, FontStyle.Bold)));
            yPos += 40;

            panel.Controls.Add(CreateLabel($"Tên: {_selectedCourse.Title}", new Point(20, yPos)));
            yPos += 30;

            panel.Controls.Add(CreateLabel($"Giảng viên: {_selectedCourse.Owner.FullName}", new Point(20, yPos)));
            yPos += 30;

            panel.Controls.Add(CreateLabel($"Danh mục: {_selectedCourse.Category?.Name ?? "Chưa phân loại"}", new Point(20, yPos)));
            yPos += 30;

            panel.Controls.Add(CreateLabel($"Giá: {_selectedCourse.Price:N0} VNĐ", new Point(20, yPos)));
            yPos += 30;

            panel.Controls.Add(CreateLabel($"Số chương: {_selectedCourse.CourseChapters.Count}", new Point(20, yPos)));
            yPos += 30;

            var lessonCount = _selectedCourse.CourseChapters.Sum(ch => ch.Lessons.Count);
            panel.Controls.Add(CreateLabel($"Số bài học: {lessonCount}", new Point(20, yPos)));
            yPos += 40;

            // Description
            panel.Controls.Add(CreateLabel("Mô tả:", new Point(20, yPos), new Font("Segoe UI", 11, FontStyle.Bold)));
            yPos += 30;

            var txtDesc = new TextBox
            {
                Text = _selectedCourse.Summary ?? "Chưa có mô tả",
                Location = new Point(20, yPos),
                Size = new Size(820, 80),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 10)
            };
            panel.Controls.Add(txtDesc);
            yPos += 100;

            // Auto check results
            panel.Controls.Add(CreateLabel("Kết quả kiểm tra tự động", new Point(20, yPos), new Font("Segoe UI", 14, FontStyle.Bold)));
            yPos += 40;

            var autoCheckResults = new List<CourseModerationService.AutoCheckResult>();
            if (!string.IsNullOrEmpty(_selectedCourse.AutoCheckResults))
            {
                try
                {
                    autoCheckResults = JsonSerializer.Deserialize<List<CourseModerationService.AutoCheckResult>>(_selectedCourse.AutoCheckResults);
                }
                catch { }
            }

            var autoScore = CourseModerationService.CalculateAutoScore(autoCheckResults);
            var scoreLabel = CreateLabel($"Điểm tổng: {autoScore}/100", new Point(20, yPos), new Font("Segoe UI", 12, FontStyle.Bold));
            scoreLabel.ForeColor = autoScore >= 80 ? Color.Green : autoScore >= 60 ? Color.Orange : Color.Red;
            panel.Controls.Add(scoreLabel);
            yPos += 40;

            foreach (var result in autoCheckResults)
            {
                var icon = result.Passed ? "✓" : "✗";
                var color = result.Severity == "Error" ? Color.Red : result.Severity == "Warning" ? Color.Orange : Color.Green;

                var resultLabel = CreateLabel($"{icon} {result.CheckName}: {result.Message}", new Point(40, yPos));
                resultLabel.ForeColor = color;
                panel.Controls.Add(resultLabel);
                yPos += 30;
            }

            yPos += 20;

            // Action buttons
            var btnApprove = new Button
            {
                Text = "Phê duyệt",
                Size = new Size(120, 40),
                Location = new Point(20, yPos),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.Click += (s, e) => { form.DialogResult = DialogResult.OK; ApproveCourse(); form.Close(); };
            panel.Controls.Add(btnApprove);

            var btnRequestRevision = new Button
            {
                Text = "Yêu cầu sửa",
                Size = new Size(120, 40),
                Location = new Point(150, yPos),
                BackColor = Color.FromArgb(255, 193, 7),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnRequestRevision.FlatAppearance.BorderSize = 0;
            btnRequestRevision.Click += (s, e) => { form.DialogResult = DialogResult.OK; RequestRevision(); form.Close(); };
            panel.Controls.Add(btnRequestRevision);

            var btnReject = new Button
            {
                Text = "Từ chối",
                Size = new Size(120, 40),
                Location = new Point(280, yPos),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnReject.FlatAppearance.BorderSize = 0;
            btnReject.Click += (s, e) => { form.DialogResult = DialogResult.OK; RejectCourse(); form.Close(); };
            panel.Controls.Add(btnReject);

            var btnClose = new Button
            {
                Text = "Đóng",
                Size = new Size(100, 40),
                Location = new Point(720, yPos),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => form.Close();
            panel.Controls.Add(btnClose);

            form.Controls.Add(panel);
            form.ShowDialog();
        }

        private Label CreateLabel(string text, Point location, Font font = null)
        {
            return new Label
            {
                Text = text,
                Location = location,
                AutoSize = true,
                Font = font ?? new Font("Segoe UI", 10)
            };
        }

        private async void ApproveCourse()
        {
            if (_selectedCourse == null)
            {
                MessageBox.Show("Vui lòng chọn khóa học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn phê duyệt khóa học '{_selectedCourse.Title}'?",
                "Xác nhận phê duyệt",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var adminUserId = AuthHelper.CurrentUser?.UserId ?? 0;
                    using var context = new LearningPlatformContext();

                    if (CourseModerationService.ApproveCourse(_selectedCourse.CourseId, adminUserId, context))
                    {
                        await LogAdminActionAsync("Approve", "Course", _selectedCourse.CourseId, $"Approved course: {_selectedCourse.Title}");
                        MessageBox.Show("Đã phê duyệt khóa học!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadPendingCoursesAsync();
                    }
                    else
                    {
                        MessageBox.Show("Không thể phê duyệt khóa học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void RejectCourse()
        {
            if (_selectedCourse == null)
            {
                MessageBox.Show("Vui lòng chọn khóa học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var reason = ShowReasonDialog("Nhập lý do từ chối:");
            if (string.IsNullOrWhiteSpace(reason)) return;

            try
            {
                var adminUserId = AuthHelper.CurrentUser?.UserId ?? 0;
                using var context = new LearningPlatformContext();

                if (CourseModerationService.RejectCourse(_selectedCourse.CourseId, adminUserId, reason, context))
                {
                    await LogAdminActionAsync("Reject", "Course", _selectedCourse.CourseId, $"Rejected course: {_selectedCourse.Title}. Reason: {reason}");
                    MessageBox.Show("Đã từ chối khóa học!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPendingCoursesAsync();
                }
                else
                {
                    MessageBox.Show("Không thể từ chối khóa học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void RequestRevision()
        {
            if (_selectedCourse == null)
            {
                MessageBox.Show("Vui lòng chọn khóa học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var reason = ShowReasonDialog("Nhập yêu cầu sửa đổi:");
            if (string.IsNullOrWhiteSpace(reason)) return;

            try
            {
                var adminUserId = AuthHelper.CurrentUser?.UserId ?? 0;
                using var context = new LearningPlatformContext();

                if (CourseModerationService.RequestRevision(_selectedCourse.CourseId, adminUserId, reason, context))
                {
                    await LogAdminActionAsync("RequestRevision", "Course", _selectedCourse.CourseId, $"Requested revision for course: {_selectedCourse.Title}. Reason: {reason}");
                    MessageBox.Show("Đã gửi yêu cầu sửa đổi!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPendingCoursesAsync();
                }
                else
                {
                    MessageBox.Show("Không thể gửi yêu cầu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ShowReasonDialog(string prompt)
        {
            using var form = new Form
            {
                Text = "Nhập lý do",
                Size = new Size(500, 300),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };

            var label = new Label
            {
                Text = prompt,
                Location = new Point(20, 20),
                Size = new Size(440, 20),
                Font = new Font("Segoe UI", 10)
            };

            var textBox = new TextBox
            {
                Location = new Point(20, 50),
                Size = new Size(440, 120),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 10)
            };

            var btnOK = new Button
            {
                Text = "OK",
                Size = new Size(80, 35),
                Location = new Point(300, 190),
                BackColor = ColorPalette.Primary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            btnOK.FlatAppearance.BorderSize = 0;

            var btnCancel = new Button
            {
                Text = "Hủy",
                Size = new Size(80, 35),
                Location = new Point(390, 190),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            form.Controls.AddRange(new Control[] { label, textBox, btnOK, btnCancel });

            return form.ShowDialog() == DialogResult.OK ? textBox.Text : null;
        }
    }
}
