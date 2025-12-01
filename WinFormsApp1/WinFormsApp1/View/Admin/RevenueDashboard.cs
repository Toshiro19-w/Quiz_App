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
    public partial class RevenueDashboard : UserControl
    {
        private readonly AdminController _adminController;

        public RevenueDashboard()
        {
            _adminController = new AdminController();
            InitializeComponent();
            InitializeFilterControls();
        }

        private void InitializeFilterControls()
        {
            // Initialize ComboBox items
            statusCombo.Items.AddRange(new object[] { "Tất cả", "Đã thanh toán", "Chờ thanh toán", "Hoàn tiền" });
            statusCombo.SelectedIndex = 0;

            providerCombo.Items.AddRange(new object[] { "Tất cả", "VNPay", "Stripe", "Khác" });
            providerCombo.SelectedIndex = 0;

            // Initialize DatePickers
            startDatePicker.Value = DateTime.Now.AddMonths(-1);
            endDatePicker.Value = DateTime.Now;

            // Wire up events
            applyButton.Click += (s, e) => LoadData();
            resetButton.Click += (s, e) => {
                startDatePicker.Value = DateTime.Now.AddMonths(-1);
                endDatePicker.Value = DateTime.Now;
                statusCombo.SelectedIndex = 0;
                providerCombo.SelectedIndex = 0;
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

        private void RevenueDashboard_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                var startDate = startDatePicker.Value;
                var endDate = endDatePicker.Value;
                var status = statusCombo.SelectedItem?.ToString();
                var provider = providerCombo.SelectedItem?.ToString();

                var revenueStats = await _adminController.GetRevenueAnalyticsAsync(startDate, endDate, status, provider);
                var revenueTrend = await _adminController.GetRevenueTrendAsync(startDate, endDate);
                CreateRevenueStatsCards(revenueStats);
                CreateRevenueCharts(revenueStats, revenueTrend);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void CreateRevenueStatsCards(RevenueAnalytics stats)
        {
            statsFlowPanel.Controls.Clear();

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
                statsFlowPanel.Controls.Add(card);
            }
        }

        private void CreateRevenueCharts(RevenueAnalytics stats, Dictionary<string, decimal> revenueTrend)
        {
            // Monthly Chart
            monthlyChartPanel.Controls.Clear();
            // We need to add the title label for the chart panel manually or use the helper
            // The helper 'CreateResponsiveChartPanel' creates a panel with a label. 
            // Since we already have the panel 'monthlyChartPanel', we can just add the chart to it.
            // But we need the title. Let's add a title label to monthlyChartPanel if it doesn't exist.
            
            if (monthlyChartPanel.Controls.Find("chartTitle", false).Length == 0)
            {
                var title = new Label
                {
                    Text = "📈 Xu hướng doanh thu",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Location = new Point(10, 10),
                    AutoSize = true,
                    Name = "chartTitle"
                };
                monthlyChartPanel.Controls.Add(title);
            }

            var monthlyChart = CreateBarChart(monthlyChartPanel, revenueTrend);
            monthlyChartPanel.Controls.Add(monthlyChart);


            // Other Charts
            chartsFlowPanel.Controls.Clear();

            var statusPanel = CreateResponsiveChartPanel("💳 Trạng thái thanh toán", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            statusPanel.Margin = new Padding(0, 0, 20, 0);
            var statusChart = CreateDoughnutChart(statusPanel, new[] { 
                ("Hoàn thành", stats.PaidCount, Color.FromArgb(34, 197, 94)),
                ("Chờ", stats.PendingCount, Color.FromArgb(251, 191, 36)),
                ("Hoàn tiền", stats.RefundedCount, Color.FromArgb(239, 68, 68))
            });
            statusPanel.Controls.Add(statusChart);
            chartsFlowPanel.Controls.Add(statusPanel);

            var providerPanel = CreateResponsiveChartPanel("🏦 Nhà cung cấp thanh toán", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            var providerChart = CreateDoughnutChart(providerPanel, new[] {
                ("VNPay", stats.VNPayCount, Color.FromArgb(14, 165, 233)),
                ("Stripe", stats.StripeCount, Color.FromArgb(139, 92, 246)),
                ("Khác", stats.OtherPaymentCount, Color.FromArgb(156, 163, 175))
            });
            providerPanel.Controls.Add(providerChart);
            chartsFlowPanel.Controls.Add(providerPanel);
        }

        private GunaChart CreateBarChart(Panel parent, Dictionary<string, decimal> revenueTrend)
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

            foreach (var item in revenueTrend)
            {
                dataset.DataPoints.Add(item.Key, (double)item.Value);
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
