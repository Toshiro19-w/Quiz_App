using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using static WinFormsApp1.Helpers.ResponsiveLayoutHelper;
using static WinFormsApp1.Helpers.UIComponentHelper;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.Admin
{
    public class SystemMonitoringDashboard : UserControl
    {
        private AdminController _controller;

        public SystemMonitoringDashboard()
        {
            _controller = new AdminController();
            InitializeControl();
            LoadData();
        }

        private void InitializeControl()
        {
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(248, 249, 250);
            AutoScroll = true;
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

            var cards = new[]
            {
                new { Title = "📧 Thông báo", Value = stats.TotalNotifications.ToString(), Color = Color.FromArgb(14, 165, 233) },
                new { Title = "📝 Nhật ký", Value = stats.TotalAuditLogs.ToString(), Color = Color.FromArgb(34, 197, 94) },
                new { Title = "❌ Lỗi", Value = stats.TotalErrors.ToString(), Color = Color.FromArgb(239, 68, 68) },
                new { Title = "🔄 Yêu cầu hôm nay", Value = stats.RequestsToday.ToString(), Color = Color.FromArgb(251, 191, 36) }
            };

            foreach (var cardData in cards)
            {
                var card = CreateStatsCard(cardData.Title, cardData.Value, cardData.Color, new Point(0, 0), new Size(280, 110));
                card.Margin = new Padding(0, 0, 20, 20);
                flowPanel.Controls.Add(card);
            }

            Controls.Add(flowPanel);
        }

        private void CreateSystemCharts(SystemAnalytics stats)
        {
            int yPos = 220;

            var notifPanel = CreateResponsiveChartPanel("📧 Thông báo", new Point(20, yPos), new Size(540, 300), AnchorStyles.Top | AnchorStyles.Left);
            var notifInfo = new Label
            {
                Text = $"Đã gửi: {stats.NotificationsSent}\nChờ gửi: {stats.NotificationsPending}",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 50),
                AutoSize = true
            };
            notifPanel.Controls.Add(notifInfo);
            Controls.Add(notifPanel);

            var logPanel = CreateResponsiveChartPanel("📝 Nhật ký hoạt động", new Point(580, yPos), new Size(540, 300), AnchorStyles.Top | AnchorStyles.Right);
            var logInfo = new Label
            {
                Text = $"Tổng: {stats.TotalAuditLogs}\nHôm nay: {stats.AuditLogsToday}",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 50),
                AutoSize = true
            };
            logPanel.Controls.Add(logInfo);
            Controls.Add(logPanel);

            yPos += 320;
            var errorPanel = CreateResponsiveChartPanel("❌ Lỗi hệ thống", new Point(20, yPos), new Size(1100, 250), AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
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
