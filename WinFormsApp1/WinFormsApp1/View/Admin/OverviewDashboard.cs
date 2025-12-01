using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.Charts.WinForms;
using WinFormsApp1.Controllers;
using WinFormsApp1.ViewModels;
using static WinFormsApp1.Helpers.ResponsiveLayoutHelper;
using static WinFormsApp1.Helpers.UIComponentHelper;

namespace WinFormsApp1.View.Admin
{
    public partial class OverviewDashboard : UserControl
    {
        private readonly AdminController _adminController;

        public OverviewDashboard()
        {
            _adminController = new AdminController();
            InitializeComponent();
        }

        private void OverviewDashboard_Load(object sender, EventArgs e)
        {
            // Default to "This Month"
            filterCombo.SelectedIndex = 2; 
            
            // Wire up events
            filterCombo.SelectedIndexChanged += FilterCombo_SelectedIndexChanged;
            applyButton.Click += (s, ev) => ApplyFilter();

            // Responsive layout
            Resize += (s, ev) =>
            {
                if (statsFlowPanel != null)
                {
                    statsFlowPanel.Width = Width - 40;
                    int cardWidth = (Width - 85) / 4;
                    foreach (Control card in statsFlowPanel.Controls)
                        card.Width = cardWidth;
                }
                if (chartPanel != null)
                {
                    chartPanel.Width = Width - 40;
                }
            };
        }

        private void FilterCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isCustom = filterCombo.SelectedIndex == 3;
            startDatePicker.Visible = isCustom;
            endDatePicker.Visible = isCustom;
            applyButton.Visible = isCustom;

            if (!isCustom)
            {
                ApplyFilter();
            }
        }

        private async void ApplyFilter()
        {
            DateTime? start = null;
            DateTime? end = null;
            var now = DateTime.Now;

            switch (filterCombo.SelectedIndex)
            {
                case 0: // Today
                    start = now.Date;
                    end = now.Date.AddDays(1).AddTicks(-1);
                    break;
                case 1: // This Week
                    // Assuming Monday is start of week
                    int diff = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7;
                    start = now.Date.AddDays(-1 * diff);
                    end = now.Date.AddDays(1).AddTicks(-1); // Until end of today
                    break;
                case 2: // This Month
                    start = new DateTime(now.Year, now.Month, 1);
                    end = now.Date.AddDays(1).AddTicks(-1);
                    break;
                case 3: // Custom
                    start = startDatePicker.Value.Date;
                    end = endDatePicker.Value.Date.AddDays(1).AddTicks(-1);
                    break;
            }

            await LoadData(start, end);
        }

        private async System.Threading.Tasks.Task LoadData(DateTime? start, DateTime? end)
        {
            try
            {
                var stats = await _adminController.GetDashboardStatsAsync(start, end);
                var revenueTrend = await _adminController.GetRevenueTrendAsync(start, end);
                CreateKPICards(stats);
                CreateTrendChart(revenueTrend);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void CreateKPICards(ViewModels.DashboardStats stats)
        {
            statsFlowPanel.Controls.Clear();
            
            var cards = new[]
            {
                new { Title = "Người dùng mới", Value = stats.TotalUsers.ToString(), Color = Color.FromArgb(56, 178, 172) },
                new { Title = "Khóa học mới", Value = stats.TotalCourses.ToString(), Color = Color.FromArgb(34, 197, 94) },
                new { Title = "Bài kiểm tra mới", Value = stats.TotalTests.ToString(), Color = Color.FromArgb(251, 191, 36) },
                new { Title = "Doanh thu", Value = $"{stats.TotalRevenue:N0} VND", Color = Color.FromArgb(14, 165, 233) }
            };

            int cardWidth = (Width - 85) / 4;
            foreach (var cardData in cards)
            {
                var card = CreateStatsCard(cardData.Title, cardData.Value, cardData.Color, new Point(0, 0), new Size(cardWidth, 130));
                card.Margin = new Padding(0, 0, 15, 0);
                statsFlowPanel.Controls.Add(card);
            }
        }

        private void CreateTrendChart(Dictionary<string, decimal> revenueTrend)
        {
            chartPanel.Controls.Clear();
            
            var panel = CreateResponsiveChartPanel(
                "📈 Xu hướng doanh thu",
                new Point(0, 0),
                new Size(chartPanel.Width, 400),
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

            foreach (var item in revenueTrend)
            {
                dataset.DataPoints.Add(item.Key, (double)item.Value);
            }

            chart.Datasets.Add(dataset);
            panel.Controls.Add(chart);
            chartPanel.Controls.Add(panel);
        }
    }
}
