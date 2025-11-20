using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.Charts.WinForms;
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
                var monthlyRevenue = await _adminController.GetMonthlyRevenueAsync();
                CreateKPICards(stats);
                CreateTrendChart(monthlyRevenue);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void CreateKPICards(ViewModels.DashboardStats stats)
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
                new { Title = "Người dùng", Value = stats.TotalUsers.ToString(), Color = Color.FromArgb(56, 178, 172) },
                new { Title = "Khóa học", Value = stats.TotalCourses.ToString(), Color = Color.FromArgb(34, 197, 94) },
                new { Title = "Bài kiểm tra", Value = stats.TotalTests.ToString(), Color = Color.FromArgb(251, 191, 36) },
                new { Title = "Doanh thu", Value = $"{stats.TotalRevenue:N0} VND", Color = Color.FromArgb(14, 165, 233) }
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

        private void CreateTrendChart(Dictionary<int, decimal> monthlyRevenue)
        {
            var flowPanel = Controls.Find("flowPanel", false).FirstOrDefault();
            int yPos = flowPanel != null ? flowPanel.Bottom + 20 : 245;
            
            var panel = CreateResponsiveChartPanel(
                "📈 Xu hướng doanh thu 12 tháng",
                new Point(20, yPos),
                new Size(Width - 60, 400),
                AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            );

            var chart = new GunaChart
            {
                Location = new Point(10, 50),
                Size = new Size(panel.Width - 20, panel.Height - 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            var dataset = new GunaLineDataset
            {
                Label = "Doanh thu",
                BorderColor = Color.FromArgb(14, 165, 233),
                PointRadius = 5,
                PointStyle = PointStyle.Circle
            };

            for (int i = 1; i <= 12; i++)
            {
                dataset.DataPoints.Add("T" + i, (double)monthlyRevenue[i]);
            }

            chart.Datasets.Add(dataset);
            panel.Controls.Add(chart);
            Controls.Add(panel);
        }
    }
}
