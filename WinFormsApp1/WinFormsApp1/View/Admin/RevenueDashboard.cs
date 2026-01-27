using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.Charts.WinForms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;
using static WinFormsApp1.Helpers.ResponsiveLayoutHelper;
using static WinFormsApp1.Helpers.UIComponentHelper;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.Admin
{
    public partial class RevenueDashboard : UserControl
    {
        private readonly AdminController _adminController;

        /// <summary>
        /// Shorthand for LanguageHelper.GetString
        /// </summary>
        private static string Lang(string key) => LanguageHelper.GetString(key);
        private static string Lang(string key, params object[] args) => LanguageHelper.GetString(key, args);

        public RevenueDashboard()
        {
            _adminController = new AdminController();
            InitializeComponent();
            InitializeFilterControls();
        }

        private void InitializeFilterControls()
        {
            // Initialize ComboBox items
            statusCombo.Items.AddRange(new object[] { Lang("All"), Lang("Paid"), Lang("Pending"), Lang("Refunded") });
            statusCombo.SelectedIndex = 0;

            providerCombo.Items.AddRange(new object[] { Lang("All"), "VNPay", "Stripe", Lang("Other") });
            providerCombo.SelectedIndex = 0;

            // Initialize DatePickers
            DateRangeValidationHelper.InitializeDatePickers(startDatePicker, endDatePicker, 30);

            // ✅ Setup date range validation
            DateRangeValidationHelper.SetupDateRangeValidation(
                startDatePicker,
                endDatePicker,
                applyButton
            );

            // Wire up events
            applyButton.Click += (s, e) => LoadData();
            resetButton.Click += (s, e) => {
                DateRangeValidationHelper.InitializeDatePickers(startDatePicker, endDatePicker, 30);
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
                // ✅ Validate date range before loading
                if (!DateRangeValidationHelper.ValidateWithMessage(
                    startDatePicker,
                    endDatePicker,
                    applyButton,
                    this.FindForm()))
                {
                    return;
                }

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
                MessageBox.Show(Lang("DataLoadError", ex.Message), Lang("Error"));
            }
        }

        private void CreateRevenueStatsCards(RevenueAnalytics stats)
        {
            statsFlowPanel.Controls.Clear();

            var cards = new[]
            {
                new { Title = Lang("TotalRevenue"), Value = LanguageHelper.FormatVND(stats.TotalRevenue), Color = Color.FromArgb(34, 197, 94) },
                new { Title = Lang("RevenueThisMonth"), Value = LanguageHelper.FormatVND(stats.RevenueThisMonth), Color = Color.FromArgb(14, 165, 233) },
                new { Title = Lang("Paid"), Value = LanguageHelper.FormatVND(stats.PaidAmount), Color = Color.FromArgb(56, 178, 172) },
                new { Title = Lang("Pending"), Value = LanguageHelper.FormatVND(stats.PendingAmount), Color = Color.FromArgb(251, 191, 36) }
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
                    Text = $"📈 {Lang("RevenueTrend")}",
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

            var statusPanel = CreateResponsiveChartPanel($"💳 {Lang("PaymentStatus")}", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            statusPanel.Margin = new Padding(0, 0, 20, 0);
            var statusChart = CreateDoughnutChart(statusPanel, new[] { 
                (Lang("Completed"), stats.PaidCount, Color.FromArgb(34, 197, 94)),
                (Lang("Pending"), stats.PendingCount, Color.FromArgb(251, 191, 36)),
                (Lang("Refunded"), stats.RefundedCount, Color.FromArgb(239, 68, 68))
            });
            statusPanel.Controls.Add(statusChart);
            chartsFlowPanel.Controls.Add(statusPanel);

            var providerPanel = CreateResponsiveChartPanel($"🏦 {Lang("PaymentProvider")}", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            var providerChart = CreateDoughnutChart(providerPanel, new[] {
                ("VNPay", stats.VNPayCount, Color.FromArgb(14, 165, 233)),
                ("Stripe", stats.StripeCount, Color.FromArgb(139, 92, 246)),
                (Lang("Other"), stats.OtherPaymentCount, Color.FromArgb(156, 163, 175))
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
                Label = Lang("Revenue")
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
