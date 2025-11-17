using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;
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
                Height = 120,
                BackColor = Primary
            };

            // Left logo area
            var logoPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 233,
                BackColor = Color.Transparent
            };

            var logoLabel = new Label
            {
                Text = "YMEDU",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
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
                Width = 383,
                BackColor = Color.Transparent
            };

            var userLabel = new Label
            {
                Text = AuthHelper.CurrentUser != null ? $"{AuthHelper.CurrentUser.FullName} ({AuthHelper.GetRoleName()})" : "Quản trị viên",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                Location = new Point(15, 40),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            var btnProfile = new Button
            {
                Text = AuthHelper.CurrentUser != null && !string.IsNullOrEmpty(AuthHelper.CurrentUser.FullName) ? AuthHelper.CurrentUser.FullName.Substring(0,1).ToUpper() : "A",
                Size = new Size(60, 60),
                BackColor = Color.FromArgb(64,64,64),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnProfile.FlatAppearance.BorderSize = 0;
            btnProfile.Click += (s, e) => { /* open profile */ };

            // Use layout within profilePanel
            profilePanel.Controls.Add(userLabel);
            profilePanel.Controls.Add(btnProfile);
            profilePanel.Padding = new Padding(10, 0, 20, 0);
            profilePanel.Resize += (s, e) =>
            {
                // keep profile button at right edge
                btnProfile.Location = new Point(profilePanel.ClientSize.Width - btnProfile.Width - 10, (profilePanel.ClientSize.Height - btnProfile.Height) / 2);
                userLabel.Location = new Point(15, (profilePanel.ClientSize.Height - userLabel.Height) / 2);
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

        private void CreateSidebarMenu()
        {
            int yPos = 20;

            // Dashboard header
            var dashboardHeader = new Label
            {
                Text = "📊 Dashboard",
                Size = new Size(230, 35),
                Location = new Point(10, yPos),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            sidebarPanel.Controls.Add(dashboardHeader);
            yPos += 40;

            var dashboardItems = new[]
            {
                new { Text = "   📋 Tổng quan", Tag = "overview" },
                new { Text = "   👥 Người dùng", Tag = "users" },
                new { Text = "   📖 Học tập", Tag = "learning" },
                new { Text = "   💰 Doanh thu", Tag = "revenue" },
                new { Text = "   ⚙️ Hệ thống", Tag = "system" }
            };

            foreach (var item in dashboardItems)
            {
                var btn = new Button
                {
                    Text = item.Text,
                    Size = new Size(230, 40),
                    Location = new Point(10, yPos),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(200, 200, 200),
                    Font = new Font("Segoe UI", 9),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Tag = item.Tag,
                    Cursor = Cursors.Hand
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(74, 85, 104);
                btn.Click += MenuButton_Click;
                sidebarPanel.Controls.Add(btn);
                yPos += 42;

                if (item.Tag == "overview")
                {
                    selectedButton = btn;
                    btn.BackColor = PrimaryDark;
                }
            }

            yPos += 10;

            var otherItems = new[]
            {
                new { Text = "👤 Người dùng", Tag = "user-management" },
                new { Text = "📚 Khóa học", Tag = "courses" },
                new { Text = "📝 Bài kiểm tra", Tag = "tests" },
                new { Text = "📊 Báo cáo", Tag = "reports" },
                new { Text = "🚪 Đăng xuất", Tag = "logout" }
            };

            foreach (var item in otherItems)
            {
                var btn = new Button
                {
                    Text = item.Text,
                    Size = new Size(230, 45),
                    Location = new Point(10, yPos),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.Transparent,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Tag = item.Tag,
                    Cursor = Cursors.Hand
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(74, 85, 104);
                btn.Click += MenuButton_Click;
                sidebarPanel.Controls.Add(btn);
                yPos += 50;
            }
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
                case "system":
                    LoadSystemStats();
                    break;
                case "user-management":
                    LoadUserManagement();
                    break;
                case "courses":
                    LoadCourseManagement();
                    break;
                case "tests":
                    LoadTestManagement();
                    break;
                case "logout":
                    Logout();
                    break;
                default:
                    ToastHelper.Show(this, $"Chức năng {button?.Text} đang được phát triển");
                    break;
            }
        }

        private void Logout()
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AuthHelper.Logout();
                this.Hide();
                var loginForm = new dangnhap();
                loginForm.FormClosed += (s, args) => this.Close();
                loginForm.Show();
            }
            else
            {
                ToastHelper.Show(this, "Hủy đăng xuất");
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

        private void LoadTestManagement()
        {
            contentPanel.Controls.Clear();
            var testControl = new TestManagementControl();
            testControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(testControl);
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

        private void LoadSystemStats()
        {
            contentPanel.Controls.Clear();
            var systemDashboard = new SystemMonitoringDashboard();
            systemDashboard.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(systemDashboard);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _adminController?.Dispose();
            base.OnFormClosed(e);
        }
    
    }

    public class DashboardStats
    {
        public int TotalUsers { get; set; }
        public int TotalCourses { get; set; }
        public int TotalClasses { get; set; }
        public int TotalTests { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalTestResults { get; set; }
    }
}