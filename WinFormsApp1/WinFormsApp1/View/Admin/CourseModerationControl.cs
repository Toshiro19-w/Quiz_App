using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Services;

namespace WinFormsApp1.View.Admin
{
    public partial class CourseModerationControl : AdminBaseControl
    {
        private List<Course> _pendingCourses = new List<Course>();
        private Course _selectedCourse;
        
        // New UI components
        private Panel _previewPanel;
        private Panel _statsPanel;
        private TreeView _courseTreeView;
        private ProgressBar _scoreProgressBar;
        private Label _lblPreviewTitle;
        private Label _lblPreviewInstructor;
        private Label _lblPreviewCategory;
        private Label _lblPreviewPrice;
        private Label _lblPreviewScore;
        private Label _lblPreviewStatus;
        private Panel _autoCheckPanel;

        public CourseModerationControl() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shorthand for LanguageHelper.GetString
        /// </summary>
        private static string Lang(string key) => LanguageHelper.GetString(key);
        private static string Lang(string key, params object[] args) => LanguageHelper.GetString(key, args);

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Create main components
            dataGridView = CreateModernDataGridView();
            dataGridView.MultiSelect = true; // Enable multi-select for batch actions
            
            // Setup layout FIRST
            SetupLayout(Lang("CourseModeration"), dataGridView);
            
            // Create statistics panel
            CreateStatsPanel();
            
            // Create preview panel
            CreatePreviewPanel();
            
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
        /// Create statistics panel showing moderation stats
        /// </summary>
        private void CreateStatsPanel()
        {
            _statsPanel = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = Color.White,
                Padding = new Padding(0, 5, 20, 5)
            };

            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            // Stats cards will be populated in UpdateStats()
            flowPanel.Name = "statsFlowPanel";
            _statsPanel.Controls.Add(flowPanel);
            
            // Insert after top panel
            var topPanel = this.Controls.OfType<Panel>().FirstOrDefault(p => p.Dock == DockStyle.Top);
            if (topPanel != null)
            {
                var index = this.Controls.GetChildIndex(topPanel);
                this.Controls.Add(_statsPanel);
                this.Controls.SetChildIndex(_statsPanel, index + 1);
            }
        }

        /// <summary>
        /// Create preview panel on the right side
        /// </summary>
        private void CreatePreviewPanel()
        {
            _previewPanel = new Panel
            {
                Width = 350,
                Dock = DockStyle.Right,
                BackColor = Color.White,
                Padding = new Padding(15),
                BorderStyle = BorderStyle.None
            };

            // Add left border
            var borderPanel = new Panel
            {
                Width = 1,
                Dock = DockStyle.Left,
                BackColor = Color.FromArgb(229, 231, 235)
            };
            _previewPanel.Controls.Add(borderPanel);

            var contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            int yPos = 10;

            // Title
            var lblTitle = new Label
            {
                Text = $"📋 {Lang("CoursePreview")}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            contentPanel.Controls.Add(lblTitle);
            yPos += 40;

            // Course Title
            _lblPreviewTitle = new Label
            {
                Text = Lang("NoCourseSelected"),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(10, yPos),
                Size = new Size(310, 50),
                ForeColor = Color.FromArgb(64, 64, 64)
            };
            contentPanel.Controls.Add(_lblPreviewTitle);
            yPos += 55;

            // Instructor
            _lblPreviewInstructor = new Label
            {
                Text = $"👤 {Lang("Instructor")}: -",
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            contentPanel.Controls.Add(_lblPreviewInstructor);
            yPos += 25;

            // Category
            _lblPreviewCategory = new Label
            {
                Text = $"📁 {Lang("Category")}: -",
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            contentPanel.Controls.Add(_lblPreviewCategory);
            yPos += 25;

            // Price
            _lblPreviewPrice = new Label
            {
                Text = $"💰 {Lang("Price")}: -",
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            contentPanel.Controls.Add(_lblPreviewPrice);
            yPos += 25;

            // Status
            _lblPreviewStatus = new Label
            {
                Text = $"📊 {Lang("Status")}: -",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            contentPanel.Controls.Add(_lblPreviewStatus);
            yPos += 35;

            // Score section
            var lblScoreTitle = new Label
            {
                Text = $"⚡ {Lang("AutoCheckScore")}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            contentPanel.Controls.Add(lblScoreTitle);
            yPos += 25;

            _scoreProgressBar = new ProgressBar
            {
                Location = new Point(10, yPos),
                Size = new Size(310, 20),
                Style = ProgressBarStyle.Continuous,
                Maximum = 100,
                Value = 0
            };
            contentPanel.Controls.Add(_scoreProgressBar);
            yPos += 25;

            _lblPreviewScore = new Label
            {
                Text = $"0/100 {Lang("Points")}",
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            contentPanel.Controls.Add(_lblPreviewScore);
            yPos += 35;

            // Auto check results panel
            var lblAutoCheck = new Label
            {
                Text = $"📝 {Lang("CheckDetails")}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            contentPanel.Controls.Add(lblAutoCheck);
            yPos += 25;

            _autoCheckPanel = new Panel
            {
                Location = new Point(10, yPos),
                Size = new Size(310, 150),
                AutoScroll = true,
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.FixedSingle
            };
            contentPanel.Controls.Add(_autoCheckPanel);
            yPos += 160;

            // Course structure tree
            var lblStructure = new Label
            {
                Text = $"📚 {Lang("CourseStructure")}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            contentPanel.Controls.Add(lblStructure);
            yPos += 25;

            _courseTreeView = new TreeView
            {
                Location = new Point(10, yPos),
                Size = new Size(310, 180),
                Font = new Font("Segoe UI", 9),
                BorderStyle = BorderStyle.FixedSingle,
                ShowLines = true,
                ShowPlusMinus = true,
                ShowRootLines = true
            };
            contentPanel.Controls.Add(_courseTreeView);

            _previewPanel.Controls.Add(contentPanel);
            this.Controls.Add(_previewPanel);
        }

        /// <summary>
        /// Update preview panel with selected course info
        /// </summary>
        private void UpdatePreviewPanel()
        {
            if (_selectedCourse == null)
            {
                _lblPreviewTitle.Text = Lang("NoCourseSelected");
                _lblPreviewInstructor.Text = $"👤 {Lang("Instructor")}: -";
                _lblPreviewCategory.Text = $"📁 {Lang("Category")}: -";
                _lblPreviewPrice.Text = $"💰 {Lang("Price")}: -";
                _lblPreviewStatus.Text = $"📊 {Lang("Status")}: -";
                _lblPreviewScore.Text = $"0/100 {Lang("Points")}";
                _scoreProgressBar.Value = 0;
                _autoCheckPanel.Controls.Clear();
                _courseTreeView.Nodes.Clear();
                return;
            }

            // Basic info
            _lblPreviewTitle.Text = _selectedCourse.Title;
            _lblPreviewInstructor.Text = $"👤 {Lang("Instructor")}: {_selectedCourse.Owner?.FullName ?? Lang("NA")}";
            _lblPreviewCategory.Text = $"📁 {Lang("Category")}: {_selectedCourse.Category?.Name ?? Lang("NotCategorized")}";
            _lblPreviewPrice.Text = $"💰 {Lang("Price")}: {LanguageHelper.FormatVND(_selectedCourse.Price)}";

            // Status with color
            var statusText = GetStatusText(_selectedCourse.ModerationStatus);
            _lblPreviewStatus.Text = $"📊 {Lang("Status")}: {statusText}";
            _lblPreviewStatus.ForeColor = _selectedCourse.ModerationStatus switch
            {
                "Approved" => Color.FromArgb(40, 167, 69),
                "Rejected" => Color.FromArgb(220, 53, 69),
                "NeedsRevision" => Color.FromArgb(255, 193, 7),
                _ => Color.FromArgb(52, 144, 220)
            };

            // Auto check results
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
            _scoreProgressBar.Value = autoScore;
            _lblPreviewScore.Text = $"{autoScore}/100 {Lang("Points")}";
            _lblPreviewScore.ForeColor = autoScore >= 80 ? Color.FromArgb(40, 167, 69) 
                : autoScore >= 60 ? Color.FromArgb(255, 193, 7) 
                : Color.FromArgb(220, 53, 69);

            // Update auto check panel
            _autoCheckPanel.Controls.Clear();
            int yPos = 5;
            foreach (var result in autoCheckResults)
            {
                var icon = result.Passed ? "✓" : "✗";
                var color = result.Severity == "Error" ? Color.FromArgb(220, 53, 69) 
                    : result.Severity == "Warning" ? Color.FromArgb(255, 193, 7) 
                    : Color.FromArgb(40, 167, 69);

                var lbl = new Label
                {
                    Text = $"{icon} {result.CheckName}",
                    Location = new Point(5, yPos),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 8),
                    ForeColor = color
                };
                _autoCheckPanel.Controls.Add(lbl);
                yPos += 18;
            }

            // Update course structure tree
            _courseTreeView.Nodes.Clear();
            var rootNode = new TreeNode($"📚 {_selectedCourse.Title}")
            {
                Tag = _selectedCourse
            };

            foreach (var chapter in _selectedCourse.CourseChapters.OrderBy(c => c.OrderIndex))
            {
                var chapterNode = new TreeNode($"📖 {chapter.Title} ({chapter.Lessons.Count} bài)")
                {
                    Tag = chapter
                };

                foreach (var lesson in chapter.Lessons.OrderBy(l => l.OrderIndex))
                {
                    var contentCount = lesson.LessonContents?.Count ?? 0;
                    var lessonIcon = contentCount > 0 ? "✅" : "⚠️";
                    var lessonNode = new TreeNode($"{lessonIcon} {lesson.Title}")
                    {
                        Tag = lesson
                    };
                    chapterNode.Nodes.Add(lessonNode);
                }

                rootNode.Nodes.Add(chapterNode);
            }

            _courseTreeView.Nodes.Add(rootNode);
            rootNode.Expand();
        }

        /// <summary>
        /// Update statistics panel
        /// </summary>
        private async Task UpdateStatsAsync()
        {
            try
            {
                using var context = new LearningPlatformContext();

                var pendingCount = await context.Courses.CountAsync(c => c.IsPublished && c.ModerationStatus == "Pending");
                var approvedToday = await context.Courses.CountAsync(c => 
                    c.ModerationStatus == "Approved" && 
                    c.ReviewedAt.HasValue && 
                    c.ReviewedAt.Value.Date == DateTime.Today);
                var rejectedToday = await context.Courses.CountAsync(c => 
                    c.ModerationStatus == "Rejected" && 
                    c.ReviewedAt.HasValue && 
                    c.ReviewedAt.Value.Date == DateTime.Today);
                var needsRevisionCount = await context.Courses.CountAsync(c => c.IsPublished && c.ModerationStatus == "NeedsRevision");

                var flowPanel = _statsPanel.Controls.Find("statsFlowPanel", true).FirstOrDefault() as FlowLayoutPanel;
                if (flowPanel == null) return;

                flowPanel.Controls.Clear();

                // Pending card
                flowPanel.Controls.Add(CreateStatCard($"⏳ {Lang("PendingReview")}", pendingCount.ToString(), Color.FromArgb(52, 144, 220)));
                
                // Approved today card
                flowPanel.Controls.Add(CreateStatCard($"✅ {Lang("ApprovedToday")}", approvedToday.ToString(), Color.FromArgb(40, 167, 69)));
                
                // Rejected today card
                flowPanel.Controls.Add(CreateStatCard($"❌ {Lang("RejectedToday")}", rejectedToday.ToString(), Color.FromArgb(220, 53, 69)));
                
                // Needs revision card
                flowPanel.Controls.Add(CreateStatCard($"🔧 {Lang("NeedsRevision")}", needsRevisionCount.ToString(), Color.FromArgb(255, 193, 7)));
            }
            catch { }
        }

        private Panel CreateStatCard(string title, string value, Color color)
        {
            var card = new Panel
            {
                Size = new Size(190, 70),
                BackColor = Color.FromArgb(248, 249, 250),
                Margin = new Padding(10, 10, 10, 10)
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(15, 10),
                AutoSize = true
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = color,
                Location = new Point(15, 32),
                AutoSize = true
            };

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);

            // Border
            card.Paint += (s, e) =>
            {
                using var pen = new Pen(color, 3);
                e.Graphics.DrawLine(pen, 0, 0, 0, card.Height);
            };

            return card;
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
            statusCombo.Items.AddRange(new object[] { 
                Lang("All"), 
                Lang("PendingReview"), 
                Lang("Approved"), 
                Lang("Rejected"), 
                Lang("NeedsRevision") 
            });
            statusCombo.SelectedIndex = 1; // Default: Pending
            statusCombo.SelectedIndexChanged += async (s, e) => await LoadPendingCoursesAsync();

            // Add filter using the new helper method
            AddCustomFilter(Lang("FilterByStatus"), statusCombo);
            
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
                Text = Lang("ViewDetails"),
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
                Text = $"✓ {Lang("Approve")}",
                Size = new Size(110, 35),
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
                Text = $"🔧 {Lang("RequestRevision")}",
                Size = new Size(130, 35),
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
                Text = $"✗ {Lang("Reject")}",
                Size = new Size(90, 35),
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

            // 5. Batch Approve
            var btnBatchApprove = new Button
            {
                Text = $"✓ {Lang("BatchApprove")}",
                Size = new Size(130, 35),
                Location = new Point(xPos, 12),
                BackColor = Color.FromArgb(23, 162, 184),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Name = "btnBatchApprove"
            };
            btnBatchApprove.FlatAppearance.BorderSize = 0;
            btnBatchApprove.Click += BtnBatchApprove_Click;
            buttonPanel.Controls.Add(btnBatchApprove);
            xPos += btnBatchApprove.Width + spacing;

            // 6. Làm mới
            var btnRefresh = new Button
            {
                Text = $"🔄 {Lang("Refresh")}",
                Size = new Size(100, 35),
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

        /// <summary>
        /// Batch approve multiple selected courses
        /// </summary>
        private async void BtnBatchApprove_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count < 2)
            {
                MessageBox.Show($"{Lang("SelectAtLeast2Courses")}\n\n{Lang("HoldCtrlToSelect")}", 
                    Lang("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedCourses = new List<Course>();
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                var courseId = (int)row.Cells["CourseId"].Value;
                var course = _pendingCourses.FirstOrDefault(c => c.CourseId == courseId);
                if (course != null && course.ModerationStatus == "Pending")
                {
                    selectedCourses.Add(course);
                }
            }

            if (!selectedCourses.Any())
            {
                MessageBox.Show(Lang("NoPendingCoursesSelected"), 
                    Lang("Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Show confirmation with list
            var courseList = string.Join("\n", selectedCourses.Select(c => $"• {c.Title} ({Lang("Instructor")}: {c.Owner?.FullName})"));
            var result = MessageBox.Show(
                $"{Lang("ConfirmBatchApprove", selectedCourses.Count)}\n\n{courseList}",
                Lang("Confirm"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                var adminUserId = AuthHelper.CurrentUser?.UserId ?? 0;
                using var context = new LearningPlatformContext();
                int successCount = 0;

                foreach (var course in selectedCourses)
                {
                    if (CourseModerationService.ApproveCourse(course.CourseId, adminUserId, context))
                    {
                        await LogAdminActionAsync("BatchApprove", "Course", course.CourseId, 
                            $"Batch approved course: {course.Title}");
                        successCount++;
                    }
                }

                ToastHelper.Show(this.FindForm(), $"✅ {Lang("BatchApproveSuccess", successCount, selectedCourses.Count)}");
                await LoadPendingCoursesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Lang("Error")}: {ex.Message}", Lang("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridViewEvents()
        {
            dataGridView.SelectionChanged += (s, e) =>
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    var courseId = (int)dataGridView.SelectedRows[0].Cells["CourseId"].Value;
                    _selectedCourse = _pendingCourses.FirstOrDefault(c => c.CourseId == courseId);
                    
                    // Update preview panel
                    UpdatePreviewPanel();
                    
                    // Update button states based on course status
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
                // Update stats panel
                await UpdateStatsAsync();

                // Tìm ComboBox với tên mới
                var statusCombo = this.Controls.Find("cboModerationStatus", true).FirstOrDefault() as ComboBox;
                var selectedIndex = statusCombo?.SelectedIndex ?? 1;

                using var context = new LearningPlatformContext();

                var query = context.Courses
                    .Include(c => c.Owner)
                    .Include(c => c.Category)
                    .Include(c => c.CourseChapters)
                        .ThenInclude(ch => ch.Lessons)
                            .ThenInclude(l => l.LessonContents)
                    // Chỉ load khóa học đã xuất bản (bỏ qua nháp)
                    .Where(c => c.IsPublished == true)
                    .AsQueryable();

                // Filter by status based on index
                query = selectedIndex switch
                {
                    1 => query.Where(c => c.ModerationStatus == "Pending"),      // Pending
                    2 => query.Where(c => c.ModerationStatus == "Approved"),     // Approved
                    3 => query.Where(c => c.ModerationStatus == "Rejected"),     // Rejected
                    4 => query.Where(c => c.ModerationStatus == "NeedsRevision"),// NeedsRevision
                    _ => query                                                    // All
                };

                _pendingCourses = await query
                    .OrderBy(c => c.SubmittedForReviewAt)
                    .ToListAsync();

                DisplayCourses();
                
                // Update button states and preview panel after loading
                UpdateButtonStates();
                UpdatePreviewPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Lang("DataLoadError", ex.Message), Lang("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Instructor = c.Owner.FullName,
                    Category = c.Category?.Name ?? Lang("NotCategorized"),
                    Chapters = c.CourseChapters.Count,
                    Lessons = lessonCount,
                    Price = LanguageHelper.FormatVND(c.Price),
                    AutoScore = autoScore,
                    Status = GetStatusText(c.ModerationStatus),
                    SubmitDate = c.SubmittedForReviewAt.HasValue 
                        ? LanguageHelper.FormatDateTime(c.SubmittedForReviewAt.Value) 
                        : Lang("NA")
                };
            }).ToList();

            dataGridView.DataSource = displayData;

            // Update column headers
            UpdateDataGridHeaders(dataGridView, new Dictionary<string, string>
            {
                { "CourseId", Lang("ID") },
                { "Title", Lang("CourseTitle") },
                { "Instructor", Lang("Instructor") },
                { "Category", Lang("Category") },
                { "Chapters", Lang("Chapters") },
                { "Lessons", Lang("Lessons") },
                { "Price", Lang("Price") },
                { "AutoScore", Lang("AutoScore") },
                { "Status", Lang("Status") },
                { "SubmitDate", Lang("SubmitDate") }
            });

            // Color code auto score
            if (dataGridView.Columns["AutoScore"] != null)
            {
                dataGridView.CellFormatting += (s, e) =>
                {
                    if (e.ColumnIndex == dataGridView.Columns["AutoScore"].Index && e.Value != null)
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
                "Pending" => Lang("PendingReview"),
                "Approved" => Lang("Approved"),
                "Rejected" => Lang("Rejected"),
                "NeedsRevision" => Lang("NeedsRevision"),
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
                MessageBox.Show(Lang("PleaseSelectCourse"), Lang("Information"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Create responsive dialog form
            using var form = new Form
            {
                Text = Lang("CourseDetailModeration"),
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
            panel.Controls.Add(CreateLabel(Lang("CourseInfo"), new Point(20, yPos), new Font("Segoe UI", 14, FontStyle.Bold)));
            yPos += 40;

            panel.Controls.Add(CreateLabel($"{Lang("CourseTitle")}: {_selectedCourse.Title}", new Point(20, yPos)));
            yPos += 30;

            panel.Controls.Add(CreateLabel($"{Lang("Instructor")}: {_selectedCourse.Owner.FullName}", new Point(20, yPos)));
            yPos += 30;

            panel.Controls.Add(CreateLabel($"{Lang("Category")}: {_selectedCourse.Category?.Name ?? Lang("NotCategorized")}", new Point(20, yPos)));
            yPos += 30;

            panel.Controls.Add(CreateLabel($"{Lang("Price")}: {LanguageHelper.FormatVND(_selectedCourse.Price)}", new Point(20, yPos)));
            yPos += 30;

            panel.Controls.Add(CreateLabel($"{Lang("Chapters")}: {_selectedCourse.CourseChapters.Count}", new Point(20, yPos)));
            yPos += 30;

            var lessonCount = _selectedCourse.CourseChapters.Sum(ch => ch.Lessons.Count);
            panel.Controls.Add(CreateLabel($"{Lang("Lessons")}: {lessonCount}", new Point(20, yPos)));
            yPos += 40;

            // Description
            panel.Controls.Add(CreateLabel($"{Lang("Description")}:", new Point(20, yPos), new Font("Segoe UI", 11, FontStyle.Bold)));
            yPos += 30;

            var txtDesc = new TextBox
            {
                Text = _selectedCourse.Summary ?? Lang("NoDescription"),
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
            panel.Controls.Add(CreateLabel(Lang("AutoCheckResults"), new Point(20, yPos), new Font("Segoe UI", 14, FontStyle.Bold)));
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
            var scoreLabel = CreateLabel($"{Lang("TotalScore")}: {autoScore}/100", new Point(20, yPos), new Font("Segoe UI", 12, FontStyle.Bold));
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
                Text = Lang("Approve"),
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
                Text = Lang("RequestRevision"),
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
                Text = Lang("Reject"),
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
                Text = Lang("Close"),
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
                MessageBox.Show(Lang("PleaseSelectCourse"), Lang("Information"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validation: Check if already approved
            if (_selectedCourse.ModerationStatus == "Approved")
            {
                MessageBox.Show(Lang("CourseAlreadyApproved"), Lang("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    $"{Lang("LowScoreWarning", autoScore)}\n\n" +
                    $"{Lang("CourseQualityWarning")}\n\n" +
                    $"{Lang("ConfirmApprove")}?",
                    Lang("ConfirmApproveWithLowScore"),
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
                    $"{Lang("CriticalErrorsWarning")}\n\n{errorList}\n\n" +
                    $"{Lang("ConfirmApprove")}?",
                    Lang("ConfirmApproveWithErrors"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmWithErrors != DialogResult.Yes) return;
            }

            // ✅ Final confirmation
            var result = MessageBox.Show(
                $"{Lang("ConfirmApprove")}:\n\n" +
                $"'{_selectedCourse.Title}'\n\n" +
                $"{Lang("Instructor")}: {_selectedCourse.Owner.FullName}\n" +
                $"{Lang("AutoScore")}: {autoScore}/100\n\n" +
                $"{Lang("CourseWillBePublic")}",
                Lang("ConfirmApprove"),
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
                        
                        ToastHelper.Show(this.FindForm(), $"✅ {Lang("ApproveSuccess")}");
                        await LoadPendingCoursesAsync();
                    }
                    else
                    {
                        MessageBox.Show(Lang("CannotApproveCourse"), Lang("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{Lang("Error")}: {ex.Message}", Lang("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void RejectCourse()
        {
            if (_selectedCourse == null)
            {
                MessageBox.Show(Lang("PleaseSelectCourse"), Lang("Information"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validation: Check if already rejected
            if (_selectedCourse.ModerationStatus == "Rejected")
            {
                MessageBox.Show(Lang("CourseAlreadyRejected"), Lang("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ✅ Validation: Require reason
            var reason = ShowReasonDialog(Lang("EnterRejectionReason"));
            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show(Lang("PleaseEnterReason"), Lang("Information"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validation: Reason must be at least 20 characters
            if (reason.Length < 20)
            {
                MessageBox.Show(Lang("ReasonTooShort", 20), Lang("Information"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Final confirmation
            var result = MessageBox.Show(
                $"{Lang("ConfirmReject")}:\n\n" +
                $"'{_selectedCourse.Title}'\n\n" +
                $"{Lang("Instructor")}: {_selectedCourse.Owner.FullName}\n\n" +
                $"{Lang("Reason")}: {reason}\n\n" +
                $"{Lang("CourseWillNotBePublic")}",
                Lang("ConfirmReject"),
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
                    
                    ToastHelper.Show(this.FindForm(), $"✅ {Lang("RejectSuccess")}");
                    await LoadPendingCoursesAsync();
                }
                else
                {
                    MessageBox.Show(Lang("CannotRejectCourse"), Lang("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Lang("Error")}: {ex.Message}", Lang("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void RequestRevision()
        {
            if (_selectedCourse == null)
            {
                MessageBox.Show(Lang("PleaseSelectCourse"), Lang("Information"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validation: Check if already in NeedsRevision status
            if (_selectedCourse.ModerationStatus == "NeedsRevision")
            {
                MessageBox.Show($"{Lang("CourseAlreadyNeedsRevision")}\n\n{Lang("WaitForInstructorUpdate")}", 
                    Lang("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ✅ Validation: Require reason
            var reason = ShowReasonDialog(Lang("EnterRevisionRequest"));
            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show(Lang("PleaseEnterReason"), Lang("Information"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validation: Reason must be at least 30 characters (more detailed than reject)
            if (reason.Length < 30)
            {
                MessageBox.Show(Lang("ReasonTooShort", 30), 
                    Lang("Information"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    $"{Lang("AutoDetectedIssues")}\n\n{issueList}\n\n" +
                    $"{Lang("ContinueWithRevisionRequest")}",
                    Lang("IssueSuggestions"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (showIssues != DialogResult.Yes) return;
            }

            // ✅ Final confirmation
            var result = MessageBox.Show(
                $"{Lang("ConfirmRequestRevision")}:\n\n" +
                $"'{_selectedCourse.Title}'\n\n" +
                $"{Lang("Instructor")}: {_selectedCourse.Owner.FullName}\n\n" +
                $"{Lang("Reason")}: {reason}\n\n" +
                $"{Lang("InstructorWillBeNotified")}",
                Lang("ConfirmRequestRevision"),
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
                    
                    ToastHelper.Show(this.FindForm(), $"✅ {Lang("RequestRevisionSuccess")}");
                    await LoadPendingCoursesAsync();
                }
                else
                {
                    MessageBox.Show(Lang("CannotRequestRevision"), Lang("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Lang("Error")}: {ex.Message}", Lang("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ShowReasonDialog(string prompt)
        {
            using var form = new Form
            {
                Text = Lang("EnterReason"),
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
                Text = Lang("ReasonHint"),
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
                Text = $"0 {Lang("Characters")} ({Lang("MinimumCharacters", 20)})",
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
                charCountLabel.Text = $"{length} {Lang("Characters")} ({Lang("MinimumCharacters", 20)})";
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
                    MessageBox.Show(Lang("PleaseEnterReason"), Lang("Information"), 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (textBox.Text.Trim().Length < 20)
                {
                    MessageBox.Show(Lang("ReasonTooShort", 20), Lang("Information"), 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                form.DialogResult = DialogResult.OK;
                form.Close();
            };

            var btnCancel = new Button
            {
                Text = Lang("Cancel"),
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
