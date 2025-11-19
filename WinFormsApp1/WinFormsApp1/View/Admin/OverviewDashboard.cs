using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.ViewModels;
using static WinFormsApp1.Helpers.ResponsiveLayoutHelper;
using static WinFormsApp1.Helpers.UIComponentHelper;

namespace WinFormsApp1.View.Admin
{
    public partial class OverviewDashboard : AdminBaseControl
    {
        public OverviewDashboard() : base()
        {
            InitializeComponent();
        }

        private void OverviewDashboard_Load(object sender, EventArgs e)
        {
            SetupLayout();
            LoadData();
        }

        private void SetupLayout()
        {
            var topPanel = CreateTopPanel("Tổng quan hệ thống");
            this.Controls.Add(topPanel);
        }

        private async void LoadData()
        {
            Controls.Clear();

            var titleLabel = new Label
            {
                Text = "📊 Tổng quan hệ thống",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            Controls.Add(titleLabel);

            try
            {
                var stats = await _adminController.GetDashboardStatsAsync();
                CreateKPICards(stats);
                CreateTrendChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void CreateKPICards(ViewModels.DashboardStats stats)
        {
            var flowPanel = CreateResponsiveCardContainer(this, 80);
            flowPanel.Name = "flowPanel";

            var cards = new[]
            {
                new { Title = "👥 Người dùng", Value = stats.TotalUsers.ToString(), Color = Color.FromArgb(56, 178, 172) },
                new { Title = "📚 Khóa học", Value = stats.TotalCourses.ToString(), Color = Color.FromArgb(34, 197, 94) },
                new { Title = "📝 Bài kiểm tra", Value = stats.TotalTests.ToString(), Color = Color.FromArgb(251, 191, 36) },
                new { Title = "💰 Doanh thu", Value = $"${stats.TotalRevenue:N0}", Color = Color.FromArgb(14, 165, 233) }
            };

            foreach (var cardData in cards)
            {
                var card = CreateStatsCard(
                    cardData.Title,
                    cardData.Value,
                    cardData.Color,
                    new Point(0, 0),
                    new Size(320, 130)
                );
                card.Margin = new Padding(0, 0, 15, 15);
                flowPanel.Controls.Add(card);
            }

            Controls.Add(flowPanel);
        }

        private void CreateTrendChart()
        {
            var flowPanel = Controls.Find("flowPanel", false).FirstOrDefault();
            int yPos = flowPanel != null ? flowPanel.Bottom + 20 : 350;
            
            var panel = CreateResponsiveChartPanel(
                "📈 Xu hướng doanh thu 12 tháng",
                new Point(20, yPos),
                new Size(Width - 60, 300),
                AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            );

            var infoLabel = new Label
            {
                Text = "Biểu đồ xu hướng doanh thu theo tháng",
                Font = new Font("Segoe UI", 10),
                Location = new Point(10, 50),
                ForeColor = Color.Gray,
                AutoSize = true
            };
            panel.Controls.Add(infoLabel);

            Controls.Add(panel);
        }
    }
}
