using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;
using WinFormsApp1.View.User;
using WinFormsApp1.ViewModels;
using static WinFormsApp1.Helpers.ColorPalette;

namespace WinFormsApp1.View.Admin
{
    public partial class AdminDashboard : Form
    {
        private AdminController _adminController;
        private Panel sidebarPanel;
        private Panel mainPanel;
        private Panel topPanel;
        private Panel contentPanel;
        private Button selectedButton;
        private bool isSidebarCollapsed = false;
        private Dictionary<string, bool> _menuSectionStates = new Dictionary<string, bool>
        {
            { "Dashboard", true },
            { "Management", true },
            { "Reports", true }
        };

        public AdminDashboard()
        {
            InitializeComponent();
            _adminController = new AdminController();
            SetupLayout();
            LoadDashboard();
        }

        private void SetupLayout()
        {
            Text = "Tổng quan hệ thống - Quiz Web Admin Panel";
            Size = new Size(1898, 1024);
            MinimumSize = new Size(1200, 700);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(248, 249, 250);
            WindowState = FormWindowState.Maximized;

            // Top Panel
            topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Primary
            };

            // Left logo area
            var logoPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 200,
                BackColor = Color.Transparent
            };

            var logoLabel = new Label
            {
                Text = "YMEDU",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(214, 188, 132),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0)
            };
            logoLabel.Click += (s, e) => { /* optional click */ };
            logoPanel.Controls.Add(logoLabel);

            // Right profile area
            var profilePanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 350,
                BackColor = Color.Transparent
            };

            // Avatar button (circular)
            var avatarButton = new Button
            {
                Size = new Size(40, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(74, 85, 104),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Text = GetInitials(AuthHelper.CurrentUser?.FullName ?? "A"),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.None
            };
            avatarButton.FlatAppearance.BorderSize = 0;
            avatarButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(94, 105, 124);
            MakeCircular(avatarButton);
            avatarButton.Click += (s, e) => LoadAdminProfile();

            // User info label (clickable)
            var userLabel = new Label
            {
                Text = AuthHelper.CurrentUser != null ? $"{AuthHelper.CurrentUser.FullName}" : "Quản trị viên",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Cursor = Cursors.Hand
            };
            userLabel.Click += (s, e) => LoadAdminProfile();

            // Role label
            var roleLabel = new Label
            {
                Text = AuthHelper.GetRoleName(),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(200, 200, 200),
                AutoSize = true
            };

            // Notification bell button (thay vì profile icon)
            var notificationBtn = new Button
            {
                Text = "🔔",
                Size = new Size(35, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14),
                Cursor = Cursors.Hand
            };
            notificationBtn.FlatAppearance.BorderSize = 0;
            notificationBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(74, 85, 104);
            notificationBtn.Click += (s, e) => ShowNotifications();

            // Add tooltip
            var tooltip = new ToolTip();
            tooltip.SetToolTip(avatarButton, "Hồ sơ cá nhân");
            tooltip.SetToolTip(userLabel, "Nhấn để xem hồ sơ cá nhân");
            tooltip.SetToolTip(notificationBtn, "Thông báo");

            // Add controls to profile panel
            profilePanel.Controls.AddRange(new Control[] { avatarButton, userLabel, roleLabel, notificationBtn });
            
            // Position controls on resize
            profilePanel.Resize += (s, e) =>
            {
                int centerY = profilePanel.ClientSize.Height / 2;
                avatarButton.Location = new Point(15, centerY - avatarButton.Height / 2);
                userLabel.Location = new Point(65, centerY - 15);
                roleLabel.Location = new Point(65, centerY + 5);
                notificationBtn.Location = new Point(profilePanel.Width - 55, centerY - notificationBtn.Height / 2);
            };

            topPanel.Controls.AddRange(new Control[] { logoPanel, profilePanel });

            // Sidebar Panel
            sidebarPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = Color.FromArgb(45, 55, 72)
            };

            CreateSidebarMenu();

            // Main Panel
            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 249, 250)
            };

            // Content Panel
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(248, 249, 250)
            };

            mainPanel.Controls.Add(contentPanel);

            Controls.AddRange(new Control[] { mainPanel, sidebarPanel, topPanel });
        }

        private string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return "A";
            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
                return $"{parts[0][0]}{parts[parts.Length - 1][0]}".ToUpper();
            else if (parts.Length == 1)
                return parts[0][0].ToString().ToUpper();
            return "A";
        }

        private void MakeCircular(Control control)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, control.Width, control.Height);
            control.Region = new Region(path);
        }

        private void CreateSidebarMenu()
        {
            sidebarPanel.Controls.Clear();
            int yPos = 10;

            // Toggle Button
            var toggleBtn = new Button
            {
                Text = isSidebarCollapsed ? "▶" : "◀",
                Size = new Size(40, 40),
                Location = new Point(isSidebarCollapsed ? 10 : 200, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            toggleBtn.FlatAppearance.BorderSize = 0;
            toggleBtn.Click += (s, e) => ToggleSidebar();
            sidebarPanel.Controls.Add(toggleBtn);

            yPos += 50;

            // Helper to create collapsible sections
            void CreateSection(string title, string key, Action createButtons)
            {
                if (isSidebarCollapsed)
                {
                    createButtons();
                    return;
                }

                bool isExpanded = _menuSectionStates.ContainsKey(key) ? _menuSectionStates[key] : true;
                string arrow = isExpanded ? "▼" : "▶";

                var headerBtn = new Button
                {
                    Text = $"{arrow} {title}",
                    Size = new Size(230, 35),
                    Location = new Point(10, yPos),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.Transparent,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Cursor = Cursors.Hand
                };
                headerBtn.FlatAppearance.BorderSize = 0;
                headerBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 65, 81);
                headerBtn.Click += (s, e) =>
                {
                    if (_menuSectionStates.ContainsKey(key))
                        _menuSectionStates[key] = !isExpanded;
                    else
                        _menuSectionStates[key] = false;
                        
                    CreateSidebarMenu();
                };

                sidebarPanel.Controls.Add(headerBtn);
                yPos += 40;

                if (isExpanded)
                {
                    createButtons();
                }
            }

            // Helper to create buttons
            void CreateButton(string text, string tag, string icon)
            {
                var btn = new Button
                {
                    Text = isSidebarCollapsed ? icon : $"   {icon} {text}",
                    Size = new Size(isSidebarCollapsed ? 40 : 230, 40),
                    Location = new Point(10, yPos),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(200, 200, 200),
                    Font = new Font("Segoe UI", 9),
                    TextAlign = isSidebarCollapsed ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft,
                    Tag = tag,
                    Cursor = Cursors.Hand
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(74, 85, 104);
                btn.Click += MenuButton_Click;
                
                // Tooltip for collapsed mode
                if (isSidebarCollapsed)
                {
                    var tip = new ToolTip();
                    tip.SetToolTip(btn, text);
                }

                sidebarPanel.Controls.Add(btn);
                yPos += 42;

                if (selectedButton != null && selectedButton.Tag?.ToString() == tag)
                {
                    btn.BackColor = Color.FromArgb(74, 85, 104);
                    selectedButton = btn; // Update reference
                }
                else if (selectedButton == null && tag == "overview")
                {
                    selectedButton = btn;
                    btn.BackColor = Color.FromArgb(74, 85, 104); // PrimaryDark
                }
            }

            // Dashboard Section
            CreateSection("📊 Dashboard", "Dashboard", () => {
                CreateButton("Tổng quan", "overview", "📋");
                CreateButton("Người dùng", "users", "👥");
                CreateButton("Học tập", "learning", "📖");
                CreateButton("Doanh thu", "revenue", "💰");
            });

            yPos += 10;

            // Management Section
            CreateSection("📋 Quản lý", "Management", () => {
                CreateButton("Người dùng", "user-management", "👤");
                CreateButton("Khóa học", "courses", "📚");
                CreateButton("Kiểm duyệt", "course-moderation", "✅");
                CreateButton("Danh mục", "categories", "📁");
                CreateButton("Flashcard", "flashcards", "🗂️");
                CreateButton("Lịch sử hoạt động", "audit-logs", "📜");
                // Thêm item "Mã giảm giá" vào sidebar
                CreateButton("Mã giảm giá", "discounts", "🏷️");
            });

            yPos += 10;

            // Reports Section
            CreateSection("📊 Báo cáo", "Reports", () => {
                CreateButton("Tổng hợp", "report-executive", "📈");
                CreateButton("Báo cáo người dùng", "report-users", "📄");
                CreateButton("Báo cáo khóa học", "report-courses", "📚");
                CreateButton("Báo cáo doanh thu", "report-revenue", "💰");
            });
            
            // Home Button only (Profile moved to header)
            yPos += 20;
            CreateButton("Trang chủ", "home", "🏠");
        }

        private void ToggleSidebar()
        {
            isSidebarCollapsed = !isSidebarCollapsed;
            
            if (isSidebarCollapsed)
            {
                sidebarPanel.Width = 60;
            }
            else
            {
                sidebarPanel.Width = 250;
            }
            
            // Recreate menu to adjust text/icons
            CreateSidebarMenu();
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var tag = button?.Tag?.ToString();

            if (selectedButton != null)
                selectedButton.BackColor = Color.Transparent;

            if (button != null)
            {
                button.BackColor = Color.FromArgb(74, 85, 104);
                selectedButton = button;
            }

            switch (tag)
            {
                case "overview":
                    LoadDashboard();
                    break;
                case "users":
                    LoadUserStats();
                    break;
                case "learning":
                    LoadLearningStats();
                    break;
                case "revenue":
                    LoadRevenueReport();
                    break;
                case "user-management":
                    LoadUserManagement();
                    break;
                case "courses":
                    LoadCourseManagement();
                    break;
                case "course-moderation":
                    LoadCourseModeration();
                    break;
                case "categories":
                    LoadCategoryManagement();
                    break;
                case "flashcards":
                    LoadFlashcardManagement();
                    break;
                case "audit-logs":
                    LoadAuditLogManagement();
                    break;
                case "system-settings":
                    LoadSystemSettings();
                    break;
                case "report-executive":
                    LoadExecutiveReport();
                    break;
                case "report-users":
                    LoadUserReport();
                    break;
                case "report-courses":
                    LoadCourseReport();
                    break;
                case "report-revenue":
                    LoadRevenueReportDetail();
                    break;
                case "home":
                    GoToHomePage();
                    break;
                case "discounts":
                    LoadDiscountManagementControl();
                    break;
                default:
                    ToastHelper.Show(this, $"Chức năng {button?.Text} đang được phát triển");
                    break;
            }
        }

        private void LoadAdminProfile()
        {
            contentPanel.Controls.Clear();
            var adminProfile = new AdminProfile();
            adminProfile.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(adminProfile);
        }

        private void ShowNotifications()
        {
            // TODO: Implement notification dropdown or panel
            ToastHelper.Show(this, "🔔 Không có thông báo mới");
        }

        private void LoadDiscountManagementControl()
        {
            contentPanel.Controls.Clear();
            var courseControl = new DiscountManagementControl();
            courseControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(courseControl);
        }

        private void GoToHomePage()
        {
            var result = MessageBox.Show("Bạn có muốn chuyển về trang chủ?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                var mainForm = new MainContainer();
                mainForm.FormClosed += (s, args) => this.Close();
                mainForm.Show();
            }
        }

        private void LoadDashboard()
        {
            contentPanel.Controls.Clear();
            var overviewDashboard = new OverviewDashboard();
            overviewDashboard.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(overviewDashboard);
        }

        private void CreateStatsCards(DashboardStats stats)
        {
            var cards = new[]
            {
                new { Title = "Tổng người dùng", Value = stats.TotalUsers.ToString(), Color = Color.FromArgb(56, 178, 172) },
                new { Title = "Tổng khóa học", Value = stats.TotalCourses.ToString(), Color = Color.FromArgb(34, 197, 94) },
                new { Title = "Tổng bài kiểm tra", Value = stats.TotalTests.ToString(), Color = Color.FromArgb(251, 191, 36) },
                new { Title = "Tổng doanh thu", Value = $"${stats.TotalRevenue:N0}", Color = Color.FromArgb(14, 165, 233) }
            };

            int xPos = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                var card = FormLayoutHelper.CreateStatsCard(
                    cards[i].Title,
                    cards[i].Value,
                    cards[i].Color,
                    new Point(xPos, 60),
                    new Size(300, 120)
                );

                contentPanel.Controls.Add(card);
                xPos += 320;
            }
        }

        private void LoadUserManagement()
        {
            contentPanel.Controls.Clear();
            var userControl = new UserManagementControl();
            userControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(userControl);
        }

        private void LoadCourseManagement()
        {
            contentPanel.Controls.Clear();
            var courseControl = new CourseManagementControl();
            courseControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(courseControl);
        }
        
        private void LoadCourseModeration()
        {
            contentPanel.Controls.Clear();
            var moderationControl = new CourseModerationControl();
            moderationControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(moderationControl);
        }

        private void LoadUserStats()
        {
            contentPanel.Controls.Clear();
            var userDashboard = new UserAnalyticsDashboard();
            userDashboard.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(userDashboard);
        }

        private void LoadLearningStats()
        {
            contentPanel.Controls.Clear();
            var learningDashboard = new LearningAnalyticsDashboard();
            learningDashboard.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(learningDashboard);
        }

        private void LoadRevenueReport()
        {
            contentPanel.Controls.Clear();
            var revenueDashboard = new RevenueDashboard();
            revenueDashboard.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(revenueDashboard);
        }

        private void LoadCategoryManagement()
        {
            contentPanel.Controls.Clear();
            var categoryControl = new CategoryManagementControl();
            categoryControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(categoryControl);
        }

        private void LoadFlashcardManagement()
        {
            contentPanel.Controls.Clear();
            var flashcardControl = new FlashcardManagementControl();
            flashcardControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(flashcardControl);
        }

        private void LoadSystemSettings()
        {
            contentPanel.Controls.Clear();
            var systemSettings = new SystemSettingsControl();
            systemSettings.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(systemSettings);
        }

        private void LoadExecutiveReport()
        {
            contentPanel.Controls.Clear();
            var executiveReport = new ExecutiveReportControl();
            executiveReport.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(executiveReport);
        }

        private async void LoadUserReport()
        {
            try
            {
                var users = await _adminController.GetUsersAsync();
                var reportForm = new ReportViewerForm();
                ReportHelper.GenerateUserReport(reportForm.GetReportViewer(), users);
                reportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this, $"Lỗi tạo báo cáo: {ex.Message}");
            }
        }

        private async void LoadCourseReport()
        {
            try
            {
                var courses = await _adminController.GetCoursesAsync();
                var reportForm = new ReportViewerForm();
                ReportHelper.GenerateCourseReport(reportForm.GetReportViewer(), courses);
                reportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this, $"Lỗi tạo báo cáo: {ex.Message}");
            }
        }

        private async void LoadRevenueReportDetail()
        {
            try
            {
                var revenue = await _adminController.GetRevenueAnalyticsAsync();
                var reportForm = new ReportViewerForm();
                ReportHelper.GenerateRevenueReport(reportForm.GetReportViewer(), revenue);
                reportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this, $"Lỗi tạo báo cáo: {ex.Message}");
            }
        }

        private void LoadAuditLogManagement()
        {
            contentPanel.Controls.Clear();
            var auditLogControl = new AuditLogManagementControl();
            auditLogControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(auditLogControl);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _adminController?.Dispose();
            base.OnFormClosed(e);
        }
    }
}