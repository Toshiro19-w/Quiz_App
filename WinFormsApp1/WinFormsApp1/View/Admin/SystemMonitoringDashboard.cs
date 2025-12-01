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
    public partial class SystemMonitoringDashboard : UserControl
    {
        private AdminController _controller;

        public SystemMonitoringDashboard()
        {
            _controller = new AdminController();
            InitializeComponent();
        }

        private void SystemMonitoringDashboard_Load(object sender, EventArgs e)
        {
            InitializeFilterControls();
            LoadData();
        }

        private void InitializeFilterControls()
        {
            // Set default dates
            startDatePicker.Value = DateTime.Now.AddMonths(-1);
            endDatePicker.Value = DateTime.Now;

            // Wire up events
            applyButton.Click += (s, e) => LoadData();
            resetButton.Click += (s, e) =>
            {
                startDatePicker.Value = DateTime.Now.AddMonths(-1);
                endDatePicker.Value = DateTime.Now;
                LoadData();
            };

            // Responsive layout
            Resize += (s, e) =>
            {
                if (statsFlowPanel != null)
                {
                    statsFlowPanel.Width = Width - 40;
                    int cardWidth = (Width - 65) / 3;
                    foreach (Control card in statsFlowPanel.Controls)
                        card.Width = cardWidth;
                }
                if (chartsFlowPanel != null)
                {
                    chartsFlowPanel.Width = Width - 40;
                }
            };
        }

        private async void LoadData()
        {
            try
            {
                var startDate = startDatePicker.Value;
                var endDate = endDatePicker.Value;

                var systemStats = await _controller.GetSystemAnalyticsAsync(startDate, endDate);
                CreateSystemStatsCards(systemStats);
                CreateSystemCharts(systemStats);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void CreateSystemStatsCards(SystemAnalytics stats)
        {
            statsFlowPanel.Controls.Clear();
            
            var cards = new[]
            {
                new { Title = "📧 Thông báo", Value = stats.TotalNotifications.ToString(), Color = Color.FromArgb(14, 165, 233) },
                new { Title = "📝 Nhật ký", Value = stats.TotalAuditLogs.ToString(), Color = Color.FromArgb(34, 197, 94) },
                new { Title = "🔄 Yêu cầu hôm nay", Value = stats.RequestsToday.ToString(), Color = Color.FromArgb(251, 191, 36) }
            };

            int cardWidth = (Width - 65) / 3;
            foreach (var cardData in cards)
            {
                var card = CreateStatsCard(cardData.Title, cardData.Value, cardData.Color, new Point(0, 0), new Size(cardWidth, 130));
                card.Margin = new Padding(0, 0, 15, 0);
                statsFlowPanel.Controls.Add(card);
            }
        }

        private void CreateSystemCharts(SystemAnalytics stats)
        {
            chartsFlowPanel.Controls.Clear();

            var notifPanel = CreateResponsiveChartPanel("📧 Số lượng thông báo gửi ra", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            notifPanel.Margin = new Padding(0, 0, 20, 0);
            var notifChart = CreateDoughnutChart(notifPanel, new[] {
                ("Đã gửi", stats.NotificationsSent, Color.FromArgb(34, 197, 94)),
                ("Chờ gửi", stats.NotificationsPending, Color.FromArgb(251, 191, 36))
            });
            notifPanel.Controls.Add(notifChart);
            chartsFlowPanel.Controls.Add(notifPanel);

            var auditPanel = CreateResponsiveChartPanel("📋 Nhật ký hoạt động người dùng", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            var auditList = CreateAuditLogsList(auditPanel, stats.RecentAuditLogs);
            auditPanel.Controls.Add(auditList);
            chartsFlowPanel.Controls.Add(auditPanel);
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

        private Panel CreateAuditLogsList(Panel parent, List<(string Action, string Username, DateTime CreatedAt)> logs)
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
            foreach (var log in logs)
            {
                var logLabel = new Label
                {
                    Text = $"{log.CreatedAt:dd/MM/yyyy HH:mm} - {log.Username}: {log.Action}",
                    Location = new Point(10, yPos),
                    Size = new Size(listPanel.Width - 30, 20),
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(45, 55, 72)
                };
                listPanel.Controls.Add(logLabel);
                yPos += 25;
            }

            return listPanel;
        }
    }
}
