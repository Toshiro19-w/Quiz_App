using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.Charts.WinForms;
using WinFormsApp1.Controllers;
using static WinFormsApp1.Helpers.ResponsiveLayoutHelper;
using static WinFormsApp1.Helpers.UIComponentHelper;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.Admin
{
    public partial class UserAnalyticsDashboard : UserControl
    {
        private AdminController _controller;

        public UserAnalyticsDashboard()
        {
            _controller = new AdminController();
            InitializeComponent();
        }

        private void UserAnalyticsDashboard_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private async void LoadData()
        {
            Controls.Clear();

            var titleLabel = new Label
            {
                Text = "👥 Phân tích người dùng",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            Controls.Add(titleLabel);

            try
            {
                var userStats = await _controller.GetUserAnalyticsAsync();
                CreateUserStatsCards(userStats);
                CreateUserCharts(userStats);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void CreateUserStatsCards(UserAnalytics stats)
        {
            var flowPanel = new FlowLayoutPanel
            {
                Location = new Point(20, 80),
                Width = Width - 40,
                Height = 145,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                Name = "flowPanel"
            };

            var cards = new[]
            {
                new { Title = "Admin", Value = stats.AdminCount.ToString(), Color = Color.FromArgb(239, 68, 68) },
                new { Title = "Giáo viên", Value = stats.TeacherCount.ToString(), Color = Color.FromArgb(34, 197, 94) },
                new { Title = "Học sinh", Value = stats.StudentCount.ToString(), Color = Color.FromArgb(14, 165, 233) },
                new { Title = "Người dùng mới", Value = stats.NewUsersThisMonth.ToString(), Color = Color.FromArgb(251, 191, 36) }
            };

            int cardWidth = (Width - 85) / 4;
            foreach (var cardData in cards)
            {
                var card = CreateStatsCard(cardData.Title, cardData.Value, cardData.Color, new Point(0, 0), new Size(cardWidth, 130));
                card.Margin = new Padding(0, 0, 15, 0);
                flowPanel.Controls.Add(card);
            }

            Controls.Add(flowPanel);
            Resize += (s, e) => {
                flowPanel.Width = Width - 40;
                cardWidth = (Width - 85) / 4;
                foreach (Control card in flowPanel.Controls)
                    card.Width = cardWidth;
            };
        }

        private void CreateUserCharts(UserAnalytics stats)
        {
            var flowPanel = Controls.Find("flowPanel", false).FirstOrDefault();
            int yPos = flowPanel != null ? flowPanel.Bottom + 20 : 245;

            var chartFlow = new FlowLayoutPanel
            {
                Location = new Point(20, yPos),
                Width = Width - 40,
                AutoSize = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight
            };

            var growthPanel = CreateResponsiveChartPanel("📈 Tăng trưởng người dùng mới theo tháng", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            growthPanel.Margin = new Padding(0, 0, 20, 0);
            var growthChart = CreateLineChart(growthPanel, stats.NewUsersByMonth);
            growthPanel.Controls.Add(growthChart);
            chartFlow.Controls.Add(growthPanel);

            var activePanel = CreateResponsiveChartPanel("👥 Người dùng hoạt động gần đây", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            var activeList = CreateActiveUsersList(activePanel, stats.RecentActiveUsers);
            activePanel.Controls.Add(activeList);
            chartFlow.Controls.Add(activePanel);

            Controls.Add(chartFlow);
            Resize += (s, e) => chartFlow.Width = Width - 40;
        }

        private GunaChart CreateLineChart(Panel parent, Dictionary<int, int> monthlyData)
        {
            var chart = new GunaChart
            {
                Location = new Point(10, 50),
                Size = new Size(parent.Width - 20, parent.Height - 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            var dataset = new GunaLineDataset { Label = "Người dùng mới" };
            string[] months = { "T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10", "T11", "T12" };
            
            for (int i = 1; i <= 12; i++)
            {
                dataset.DataPoints.Add(months[i-1], monthlyData[i]);
            }
            
            dataset.BorderColor = Color.FromArgb(14, 165, 233);
            chart.Datasets.Add(dataset);
            chart.Legend.Display = true;
            return chart;
        }

        private Panel CreateActiveUsersList(Panel parent, List<(string Username, DateTime? LastLogin)> users)
        {
            var listPanel = new Panel
            {
                Location = new Point(10, 50),
                Size = new Size(parent.Width - 20, parent.Height - 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoScroll = true,
                BackColor = Color.White
            };

            int yPos = 10;
            foreach (var user in users)
            {
                var userLabel = new Label
                {
                    Text = $"{user.Username} - {user.LastLogin?.ToString("dd/MM/yyyy HH:mm") ?? "Chưa đăng nhập"}",
                    Location = new Point(10, yPos),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(45, 55, 72)
                };
                listPanel.Controls.Add(userLabel);
                yPos += 25;
            }

            return listPanel;
        }
    }
}
