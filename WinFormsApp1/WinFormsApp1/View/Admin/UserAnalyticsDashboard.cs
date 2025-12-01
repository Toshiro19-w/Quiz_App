using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            InitializeFilterControls();
        }

        private void InitializeFilterControls()
        {
            // Initialize DatePickers
            startDatePicker.Value = DateTime.Now.AddMonths(-1);
            endDatePicker.Value = DateTime.Now;

            // Wire up events
            applyButton.Click += (s, e) => LoadData();
            resetButton.Click += (s, e) => {
                startDatePicker.Value = DateTime.Now.AddMonths(-1);
                endDatePicker.Value = DateTime.Now;
                LoadData();
            };

            // Resize handlers
            Resize += (s, e) => {
                if (statsFlowPanel != null)
                {
                    statsFlowPanel.Width = Width - 40;
                    int cardWidth = (Width - 85) / 4;
                    foreach (Control card in statsFlowPanel.Controls)
                        card.Width = cardWidth;
                }
                if (chartsFlowPanel != null)
                {
                    chartsFlowPanel.Width = Width - 40;
                }
            };
        }

        private void UserAnalyticsDashboard_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                var startDate = startDatePicker.Value;
                var endDate = endDatePicker.Value;

                var userStats = await _controller.GetUserAnalyticsAsync(startDate, endDate);
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
            statsFlowPanel.Controls.Clear();

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
                statsFlowPanel.Controls.Add(card);
            }
        }

        private void CreateUserCharts(UserAnalytics stats)
        {
            chartsFlowPanel.Controls.Clear();

            var growthPanel = CreateResponsiveChartPanel("📈 Tăng trưởng người dùng mới theo tháng", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            growthPanel.Margin = new Padding(0, 0, 20, 0);
            var growthChart = CreateLineChart(growthPanel, stats.NewUsersByMonth);
            growthPanel.Controls.Add(growthChart);
            chartsFlowPanel.Controls.Add(growthPanel);

            var activePanel = CreateResponsiveChartPanel("👥 Người dùng hoạt động gần đây", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            var activeList = CreateActiveUsersList(activePanel, stats.RecentActiveUsers);
            activePanel.Controls.Add(activeList);
            chartsFlowPanel.Controls.Add(activePanel);
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
            string[] months = { "Tháng 01", "Tháng 02", "Tháng 03", "Tháng 04", "Tháng 05", "Tháng 06", "Tháng 07", "Tháng 08", "Tháng 09", "Tháng 10", "Tháng 11", "Tháng 12" };
            
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
