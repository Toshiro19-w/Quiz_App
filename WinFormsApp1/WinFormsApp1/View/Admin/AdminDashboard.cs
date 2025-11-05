using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;

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
            Size = new Size(1400, 800);
            MinimumSize = new Size(1200, 700);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(248, 249, 250);

            // Top Panel
            topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(52, 144, 220)
            };

            var logoLabel = new Label
            {
                Text = "Valt",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true
            };

            var userLabel = new Label
            {
                Text = "Quản trị viên",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                Location = new Point(1250, 20),
                AutoSize = true
            };

            topPanel.Controls.AddRange(new Control[] { logoLabel, userLabel });

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

            // Dashboard submenu
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
                    btn.BackColor = Color.FromArgb(74, 85, 104);
                }
            }

            yPos += 10;

            // Other menu items
            var otherItems = new[]
            {
                new { Text = "👤 Người dùng", Tag = "user-management" },
                new { Text = "📚 Khóa học", Tag = "courses" },
                new { Text = "📝 Bài kiểm tra", Tag = "tests" },
                new { Text = "📊 Báo cáo", Tag = "reports" },
                new { Text = "🏠 Trở về trang chủ", Tag = "home" }
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
                default:
                    MessageBox.Show($"Chức năng {button?.Text} đang được phát triển", "Thông báo");
                    break;
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