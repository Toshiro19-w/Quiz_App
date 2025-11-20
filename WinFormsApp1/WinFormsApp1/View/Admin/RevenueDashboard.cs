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
    public partial class RevenueDashboard : AdminBaseControl
    {
        public RevenueDashboard() : base()
        {
            InitializeComponent();
        }

        private void RevenueDashboard_Load(object sender, EventArgs e)
        {
            SetupLayout();
            LoadData();
        }

        private void SetupLayout()
        {
            var topPanel = CreateTopPanel("Phân tích doanh thu");
            this.Controls.Add(topPanel);
        }

        private async void LoadData()
        {
            Controls.Clear();

            var titleLabel = new Label
            {
                Text = "💰 Phân tích doanh thu",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            Controls.Add(titleLabel);

            try
            {
                var revenueStats = await _adminController.GetRevenueAnalyticsAsync();
                var monthlyRevenue = await _adminController.GetMonthlyRevenueAsync();
                CreateRevenueStatsCards(revenueStats);
                CreateRevenueCharts(revenueStats, monthlyRevenue);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void CreateRevenueStatsCards(RevenueAnalytics stats)
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
                new { Title = "Tổng doanh thu", Value = $"{stats.TotalRevenue:N0} VND", Color = Color.FromArgb(34, 197, 94) },
                new { Title = "Doanh thu tháng này", Value = $"{stats.RevenueThisMonth:N0} VND", Color = Color.FromArgb(14, 165, 233) },
                new { Title = "Đã thanh toán", Value = $"{stats.PaidAmount:N0} VND", Color = Color.FromArgb(56, 178, 172) },
                new { Title = "Chờ thanh toán", Value = $"{stats.PendingAmount:N0} VND", Color = Color.FromArgb(251, 191, 36) }
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

        private void CreateRevenueCharts(RevenueAnalytics stats, Dictionary<int, decimal> monthlyRevenue)
        {
            var flowPanel = Controls.Find("flowPanel", false).FirstOrDefault();
            int yPos = flowPanel != null ? flowPanel.Bottom + 20 : 245;

            var monthlyPanel = CreateResponsiveChartPanel("📈 Doanh thu 12 tháng", new Point(20, yPos), new Size(Width - 40, 400), AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            var monthlyChart = CreateBarChart(monthlyPanel, monthlyRevenue);
            monthlyPanel.Controls.Add(monthlyChart);
            Controls.Add(monthlyPanel);

            var chartFlow = new FlowLayoutPanel
            {
                Location = new Point(20, monthlyPanel.Bottom + 20),
                Width = Width - 40,
                AutoSize = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight
            };

            var statusPanel = CreateResponsiveChartPanel("💳 Trạng thái thanh toán", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            statusPanel.Margin = new Padding(0, 0, 20, 0);
            var statusChart = CreateDoughnutChart(statusPanel, new[] { 
                ("Hoàn thành", stats.PaidCount, Color.FromArgb(34, 197, 94)),
                ("Chờ", stats.PendingCount, Color.FromArgb(251, 191, 36)),
                ("Hoàn tiền", stats.RefundedCount, Color.FromArgb(239, 68, 68))
            });
            statusPanel.Controls.Add(statusChart);
            chartFlow.Controls.Add(statusPanel);

            var providerPanel = CreateResponsiveChartPanel("🏦 Nhà cung cấp thanh toán", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            var providerChart = CreateDoughnutChart(providerPanel, new[] {
                ("VNPay", stats.VNPayCount, Color.FromArgb(14, 165, 233)),
                ("Stripe", stats.StripeCount, Color.FromArgb(139, 92, 246)),
                ("Khác", stats.OtherPaymentCount, Color.FromArgb(156, 163, 175))
            });
            providerPanel.Controls.Add(providerChart);
            chartFlow.Controls.Add(providerPanel);

            Controls.Add(chartFlow);
            Resize += (s, e) => chartFlow.Width = Width - 40;
        }

        private GunaChart CreateBarChart(Panel parent, Dictionary<int, decimal> monthlyRevenue)
        {
            var chart = new GunaChart
            {
                Location = new Point(10, 50),
                Size = new Size(parent.Width - 20, parent.Height - 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            var dataset = new GunaBarDataset
            {
                Label = "Doanh thu"
            };
            dataset.FillColors.Add(Color.FromArgb(14, 165, 233));

            for (int i = 1; i <= 12; i++)
            {
                dataset.DataPoints.Add("T" + i, (double)monthlyRevenue[i]);
            }

            chart.Datasets.Add(dataset);
            return chart;
        }

        private GunaChart CreateDoughnutChart(Panel parent, (string Label, int Value, Color Color)[] data)
        {
            var chart = new GunaChart
            {
                Location = new Point(10, 50),
                Size = new Size(parent.Width - 20, parent.Height - 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            var dataset = new GunaDoughnutDataset();
            
            foreach (var item in data)
            {
                dataset.DataPoints.Add(item.Label, item.Value);
                dataset.FillColors.Add(item.Color);
            }

            chart.Datasets.Add(dataset);
            chart.Legend.Display = true;
            chart.XAxes.Display = false;
            chart.YAxes.Display = false;
            return chart;
        }




    }
}
