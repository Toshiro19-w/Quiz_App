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
                    
                    // ✅ Update button states based on course status
                    UpdateButtonStates();
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

        /// <summary>
        /// Update button states based on selected course status
        /// </summary>
        private void UpdateButtonStates()
        {
            var btnApprove = this.Controls.Find("btnApprove", true).FirstOrDefault() as Button;
            var btnReject = this.Controls.Find("btnDelete", true).FirstOrDefault() as Button;
            var btnRequestRevision = this.Controls.Find("btnRequestRevision", true).FirstOrDefault() as Button;

            if (_selectedCourse == null)
            {
                // No course selected - disable all action buttons
                if (btnApprove != null) btnApprove.Enabled = false;
                if (btnReject != null) btnReject.Enabled = false;
                if (btnRequestRevision != null) btnRequestRevision.Enabled = false;
                return;
            }

            // Enable/disable based on current status
            switch (_selectedCourse.ModerationStatus)
            {
                case "Pending":
                    // Chờ duyệt - cho phép tất cả actions
                    if (btnApprove != null) btnApprove.Enabled = true;
                    if (btnReject != null) btnReject.Enabled = true;
                    if (btnRequestRevision != null) btnRequestRevision.Enabled = true;
                    break;

                case "Approved":
                    // Đã duyệt - chỉ cho phép từ chối hoặc yêu cầu sửa (re-review)
                    if (btnApprove != null) btnApprove.Enabled = false;
                    if (btnReject != null) btnReject.Enabled = true;
                    if (btnRequestRevision != null) btnRequestRevision.Enabled = true;
                    break;

                case "Rejected":
                    // Đã từ chối - không cho phép từ chối lại, nhưng có thể phê duyệt hoặc yêu cầu sửa
                    if (btnApprove != null) btnApprove.Enabled = true;
                    if (btnReject != null) btnReject.Enabled = false;
                    if (btnRequestRevision != null) btnRequestRevision.Enabled = true;
                    break;

                case "NeedsRevision":
                    // Cần sửa - user cần fix trước, admin không thể action cho đến khi user submit lại
                    if (btnApprove != null) btnApprove.Enabled = true;
                    if (btnReject != null) btnReject.Enabled = true;
                    if (btnRequestRevision != null) btnRequestRevision.Enabled = false;
                    break;

                default:
                    // Unknown status - enable all
                    if (btnApprove != null) btnApprove.Enabled = true;
                    if (btnReject != null) btnReject.Enabled = true;
                    if (btnRequestRevision != null) btnRequestRevision.Enabled = true;
                    break;
            }
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
                    // ✅ Chỉ load khóa học đã xuất bản (bỏ qua nháp)
                    .Where(c => c.IsPublished == true)
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
                
                // ✅ Update button states after loading
                UpdateButtonStates();
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

            // ✅ Create responsive dialog form
            using var form = new Form
            {
                Text = "Chi tiết khóa học - Kiểm duyệt",
                Size = new Size(1000, 750),
                MinimumSize = new Size(800, 600),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.Sizable, // ✅ Allow resize
                MaximizeBox = true, // ✅ Allow maximize
                MinimizeBox = false
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
                Size = new Size(panel.Width - 60, 80),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right // ✅ Responsive
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

            // ✅ Responsive button panel at bottom
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20, 15, 20, 15)
            };

            var btnApprove = new Button
            {
                Text = "Phê duyệt",
                Size = new Size(120, 40),
                Location = new Point(20, 15),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.Click += (s, e) => { form.DialogResult = DialogResult.OK; ApproveCourse(); form.Close(); };
            buttonPanel.Controls.Add(btnApprove);

            var btnRequestRevision = new Button
            {
                Text = "Yêu cầu sửa",
                Size = new Size(130, 40),
                Location = new Point(150, 15),
                BackColor = Color.FromArgb(255, 193, 7),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            btnRequestRevision.FlatAppearance.BorderSize = 0;
            btnRequestRevision.Click += (s, e) => { form.DialogResult = DialogResult.OK; RequestRevision(); form.Close(); };
            buttonPanel.Controls.Add(btnRequestRevision);

            var btnReject = new Button
            {
                Text = "Từ chối",
                Size = new Size(100, 40),
                Location = new Point(290, 15),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            btnReject.FlatAppearance.BorderSize = 0;
            btnReject.Click += (s, e) => { form.DialogResult = DialogResult.OK; RejectCourse(); form.Close(); };
            buttonPanel.Controls.Add(btnReject);

            var btnClose = new Button
            {
                Text = "Đóng",
                Size = new Size(100, 40),
                Location = new Point(form.Width - 140, 15),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => form.Close();
            
            // ✅ Adjust close button position when form resizes
            form.Resize += (s, e) => {
                btnClose.Left = form.Width - 140;
            };
            
            buttonPanel.Controls.Add(btnClose);

            form.Controls.Add(buttonPanel);
            form.Controls.Add(panel);
            
            // ✅ Center on screen if no parent
            if (this.FindForm() == null)
            {
                form.StartPosition = FormStartPosition.CenterScreen;
            }
            
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

            // ✅ Validation: Check if already approved
            if (_selectedCourse.ModerationStatus == "Approved")
            {
                MessageBox.Show("Khóa học này đã được phê duyệt rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ✅ Validation: Check auto score
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
            var hasErrors = autoCheckResults.Any(r => r.Severity == "Error" && !r.Passed);

            // ✅ Warning if score is low
            if (autoScore < 60)
            {
                var confirmLowScore = MessageBox.Show(
                    $"Cảnh báo: Điểm tự động chỉ {autoScore}/100 (thấp).\n\n" +
                    $"Khóa học có thể chưa đạt chất lượng tốt.\n\n" +
                    $"Bạn có chắc chắn muốn phê duyệt?",
                    "Xác nhận phê duyệt với điểm thấp",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmLowScore != DialogResult.Yes) return;
            }

            // ✅ Error if has critical errors
            if (hasErrors)
            {
                var errorList = string.Join("\n", autoCheckResults
                    .Where(r => r.Severity == "Error" && !r.Passed)
                    .Select(r => $"• {r.Message}"));

                var confirmWithErrors = MessageBox.Show(
                    $"Cảnh báo: Khóa học có lỗi nghiêm trọng:\n\n{errorList}\n\n" +
                    $"Bạn vẫn muốn phê duyệt?",
                    "Xác nhận phê duyệt với lỗi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmWithErrors != DialogResult.Yes) return;
            }

            // ✅ Final confirmation
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn phê duyệt khóa học:\n\n" +
                $"'{_selectedCourse.Title}'\n\n" +
                $"Giảng viên: {_selectedCourse.Owner.FullName}\n" +
                $"Điểm tự động: {autoScore}/100\n\n" +
                $"Khóa học sẽ được công khai sau khi phê duyệt.",
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
                        await LogAdminActionAsync("Approve", "Course", _selectedCourse.CourseId, 
                            $"Approved course: {_selectedCourse.Title} (Score: {autoScore}/100)");
                        
                        ToastHelper.Show(this.FindForm(), "✅ Đã phê duyệt khóa học!");
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

            // ✅ Validation: Check if already rejected
            if (_selectedCourse.ModerationStatus == "Rejected")
            {
                MessageBox.Show("Khóa học này đã bị từ chối rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ✅ Validation: Require reason
            var reason = ShowReasonDialog("Nhập lý do từ chối:");
            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Vui lòng nhập lý do từ chối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validation: Reason must be at least 20 characters
            if (reason.Length < 20)
            {
                MessageBox.Show("Lý do từ chối phải có ít nhất 20 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Final confirmation
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn từ chối khóa học:\n\n" +
                $"'{_selectedCourse.Title}'\n\n" +
                $"Giảng viên: {_selectedCourse.Owner.FullName}\n\n" +
                $"Lý do: {reason}\n\n" +
                $"Khóa học sẽ không được công khai và giảng viên sẽ nhận được thông báo.",
                "Xác nhận từ chối",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                var adminUserId = AuthHelper.CurrentUser?.UserId ?? 0;
                using var context = new LearningPlatformContext();

                if (CourseModerationService.RejectCourse(_selectedCourse.CourseId, adminUserId, reason, context))
                {
                    await LogAdminActionAsync("Reject", "Course", _selectedCourse.CourseId, 
                        $"Rejected course: {_selectedCourse.Title}. Reason: {reason}");
                    
                    ToastHelper.Show(this.FindForm(), "✅ Đã từ chối khóa học!");
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

            // ✅ Validation: Check if already in NeedsRevision status
            if (_selectedCourse.ModerationStatus == "NeedsRevision")
            {
                MessageBox.Show("Khóa học này đã được yêu cầu sửa đổi rồi!\n\nVui lòng chờ giảng viên cập nhật và gửi lại.", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ✅ Validation: Require reason
            var reason = ShowReasonDialog("Nhập yêu cầu sửa đổi chi tiết:");
            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Vui lòng nhập yêu cầu sửa đổi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validation: Reason must be at least 30 characters (more detailed than reject)
            if (reason.Length < 30)
            {
                MessageBox.Show("Yêu cầu sửa đổi phải có ít nhất 30 ký tự để giảng viên hiểu rõ!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Show suggestion for common issues
            var autoCheckResults = new List<CourseModerationService.AutoCheckResult>();
            if (!string.IsNullOrEmpty(_selectedCourse.AutoCheckResults))
            {
                try
                {
                    autoCheckResults = JsonSerializer.Deserialize<List<CourseModerationService.AutoCheckResult>>(_selectedCourse.AutoCheckResults);
                }
                catch { }
            }

            var issues = autoCheckResults.Where(r => !r.Passed && r.Severity != "Info").ToList();
            if (issues.Any())
            {
                var issueList = string.Join("\n", issues.Select(r => $"• {r.Message}"));
                var showIssues = MessageBox.Show(
                    $"Các vấn đề được phát hiện tự động:\n\n{issueList}\n\n" +
                    $"Bạn có muốn tiếp tục với yêu cầu sửa đổi đã nhập không?",
                    "Gợi ý vấn đề",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (showIssues != DialogResult.Yes) return;
            }

            // ✅ Final confirmation
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn yêu cầu sửa đổi:\n\n" +
                $"'{_selectedCourse.Title}'\n\n" +
                $"Giảng viên: {_selectedCourse.Owner.FullName}\n\n" +
                $"Yêu cầu: {reason}\n\n" +
                $"Giảng viên sẽ nhận được thông báo và cần cập nhật khóa học.",
                "Xác nhận yêu cầu sửa đổi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                var adminUserId = AuthHelper.CurrentUser?.UserId ?? 0;
                using var context = new LearningPlatformContext();

                if (CourseModerationService.RequestRevision(_selectedCourse.CourseId, adminUserId, reason, context))
                {
                    await LogAdminActionAsync("RequestRevision", "Course", _selectedCourse.CourseId, 
                        $"Requested revision for course: {_selectedCourse.Title}. Reason: {reason}");
                    
                    ToastHelper.Show(this.FindForm(), "✅ Đã gửi yêu cầu sửa đổi!");
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
                Size = new Size(600, 350),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var label = new Label
            {
                Text = prompt,
                Location = new Point(20, 20),
                Size = new Size(540, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            var hintLabel = new Label
            {
                Text = "Ghi chú: Lý do phải rõ ràng, cụ thể để giảng viên hiểu và cải thiện.",
                Location = new Point(20, 45),
                Size = new Size(540, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray
            };

            var textBox = new TextBox
            {
                Location = new Point(20, 70),
                Size = new Size(540, 130),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 10),
                Name = "txtReason"
            };

            // ✅ Character count label
            var charCountLabel = new Label
            {
                Text = "0 ký tự (tối thiểu 20)",
                Location = new Point(20, 205),
                Size = new Size(540, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Name = "lblCharCount"
            };

            // ✅ Update character count on text change
            textBox.TextChanged += (s, e) =>
            {
                var length = textBox.Text.Length;
                charCountLabel.Text = $"{length} ký tự (tối thiểu 20)";
                charCountLabel.ForeColor = length >= 20 ? Color.Green : Color.Red;
            };

            var btnOK = new Button
            {
                Text = "OK",
                Size = new Size(100, 40),
                Location = new Point(370, 240),
                BackColor = ColorPalette.Primary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Name = "btnOK"
            };
            btnOK.FlatAppearance.BorderSize = 0;
            
            // ✅ Validate before accepting
            btnOK.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    MessageBox.Show("Vui lòng nhập lý do!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (textBox.Text.Trim().Length < 20)
                {
                    MessageBox.Show("Lý do phải có ít nhất 20 ký tự!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                form.DialogResult = DialogResult.OK;
                form.Close();
            };

            var btnCancel = new Button
            {
                Text = "Hủy",
                Size = new Size(100, 40),
                Location = new Point(480, 240),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            form.Controls.AddRange(new Control[] { 
                label, hintLabel, textBox, charCountLabel, btnOK, btnCancel 
            });

            // ✅ Set Accept/Cancel buttons for Enter/Esc keys
            form.AcceptButton = btnOK;
            form.CancelButton = btnCancel;

            return form.ShowDialog() == DialogResult.OK ? textBox.Text.Trim() : null;
        }
    }
}
