using System;
using System.Drawing;
using System.Windows.Forms;
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
            LoadData();
        }

        private async void LoadData()
        {
            Controls.Clear();

            var titleLabel = new Label
            {
                Text = "⚙️ Giám sát hệ thống",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            Controls.Add(titleLabel);

            try
            {
                var systemStats = await _controller.GetSystemAnalyticsAsync();
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
            var flowPanel = CreateResponsiveCardContainer(this, 80);
            flowPanel.Name = "flowPanel";

            var cards = new[]
            {
                new { Title = "📧 Thông báo", Value = stats.TotalNotifications.ToString(), Color = Color.FromArgb(14, 165, 233) },
                new { Title = "📝 Nhật ký", Value = stats.TotalAuditLogs.ToString(), Color = Color.FromArgb(34, 197, 94) },
                new { Title = "❌ Lỗi", Value = stats.TotalErrors.ToString(), Color = Color.FromArgb(239, 68, 68) },
                new { Title = "🔄 Yêu cầu hôm nay", Value = stats.RequestsToday.ToString(), Color = Color.FromArgb(251, 191, 36) }
            };

            foreach (var cardData in cards)
            {
                var card = CreateStatsCard(cardData.Title, cardData.Value, cardData.Color, new Point(0, 0), new Size(320, 130));
                card.Margin = new Padding(0, 0, 15, 15);
                flowPanel.Controls.Add(card);
            }

            Controls.Add(flowPanel);
        }

        private void CreateSystemCharts(SystemAnalytics stats)
        {
            var flowPanel = Controls.Find("flowPanel", false).FirstOrDefault();
            int yPos = flowPanel != null ? flowPanel.Bottom + 20 : 220;

            var chartFlow = new FlowLayoutPanel
            {
                Location = new Point(20, yPos),
                Width = Width - 40,
                AutoSize = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                Name = "chartFlow"
            };

            var notifPanel = CreateResponsiveChartPanel("📧 Thông báo", new Point(0, 0), new Size(540, 300), AnchorStyles.None);
            notifPanel.Margin = new Padding(0, 0, 20, 20);
            var notifInfo = new Label
            {
                Text = $"Đã gửi: {stats.NotificationsSent}\nChờ gửi: {stats.NotificationsPending}",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 50),
                AutoSize = true
            };
            notifPanel.Controls.Add(notifInfo);
            chartFlow.Controls.Add(notifPanel);

            var logPanel = CreateResponsiveChartPanel("📝 Nhật ký hoạt động", new Point(0, 0), new Size(540, 300), AnchorStyles.None);
            logPanel.Margin = new Padding(0, 0, 0, 20);
            var logInfo = new Label
            {
                Text = $"Tổng: {stats.TotalAuditLogs}\nHôm nay: {stats.AuditLogsToday}",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 50),
                AutoSize = true
            };
            logPanel.Controls.Add(logInfo);
            chartFlow.Controls.Add(logPanel);

            Controls.Add(chartFlow);
            Resize += (s, e) => chartFlow.Width = Width - 40;

            var errorPanel = CreateResponsiveChartPanel("❌ Lỗi hệ thống", new Point(20, chartFlow.Bottom + 20), new Size(Width - 40, 250), AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            var errorInfo = new Label
            {
                Text = $"Lỗi hôm nay: {stats.ErrorsToday}\nLỗi tuần này: {stats.ErrorsThisWeek}",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 50),
                AutoSize = true
            };
            errorPanel.Controls.Add(errorInfo);
            Controls.Add(errorPanel);
        }




    }
}
