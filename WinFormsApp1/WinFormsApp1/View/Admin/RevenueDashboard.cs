using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using static WinFormsApp1.Helpers.ResponsiveLayoutHelper;
using static WinFormsApp1.Helpers.UIComponentHelper;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.Admin
{
    public class RevenueDashboard : UserControl
    {
        private AdminController _controller;

        public RevenueDashboard()
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
                Text = "💰 Phân tích doanh thu",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            Controls.Add(titleLabel);

            try
            {
                var revenueStats = await _controller.GetRevenueAnalyticsAsync();
                CreateRevenueStatsCards(revenueStats);
                CreateRevenueCharts(revenueStats);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void CreateRevenueStatsCards(RevenueAnalytics stats)
        {
            var flowPanel = CreateResponsiveCardContainer(this, 80);

            var cards = new[]
            {
                new { Title = "💵 Tổng doanh thu", Value = $"${stats.TotalRevenue:N0}", Color = Color.FromArgb(34, 197, 94) },
                new { Title = "📅 Doanh thu tháng này", Value = $"${stats.RevenueThisMonth:N0}", Color = Color.FromArgb(14, 165, 233) },
                new { Title = "✅ Đã thanh toán", Value = $"${stats.PaidAmount:N0}", Color = Color.FromArgb(56, 178, 172) },
                new { Title = "⏳ Chờ thanh toán", Value = $"${stats.PendingAmount:N0}", Color = Color.FromArgb(251, 191, 36) }
            };

            foreach (var cardData in cards)
            {
                var card = CreateStatsCard(cardData.Title, cardData.Value, cardData.Color, new Point(0, 0), new Size(280, 110));
                card.Margin = new Padding(0, 0, 20, 20);
                flowPanel.Controls.Add(card);
            }

            Controls.Add(flowPanel);
        }

        private void CreateRevenueCharts(RevenueAnalytics stats)
        {
            int yPos = 220;

            var monthlyPanel = CreateResponsiveChartPanel("📈 Doanh thu 12 tháng", new Point(20, yPos), new Size(1100, 300), AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            var monthlyInfo = new Label
            {
                Text = "Biểu đồ doanh thu theo tháng",
                Font = new Font("Segoe UI", 10),
                Location = new Point(10, 50),
                ForeColor = Color.Gray,
                AutoSize = true
            };
            monthlyPanel.Controls.Add(monthlyInfo);
            Controls.Add(monthlyPanel);

            yPos += 320;
            var statusPanel = CreateResponsiveChartPanel("💳 Trạng thái thanh toán", new Point(20, yPos), new Size(540, 300), AnchorStyles.Top | AnchorStyles.Left);
            var statusInfo = new Label
            {
                Text = $"Hoàn thành: {stats.PaidCount}\nChờ: {stats.PendingCount}\nHoàn tiền: {stats.RefundedCount}",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 50),
                AutoSize = true
            };
            statusPanel.Controls.Add(statusInfo);
            Controls.Add(statusPanel);

            var providerPanel = CreateResponsiveChartPanel("🏦 Nhà cung cấp thanh toán", new Point(580, yPos), new Size(540, 300), AnchorStyles.Top | AnchorStyles.Right);
            var providerInfo = new Label
            {
                Text = $"VNPay: {stats.VNPayCount}\nStripe: {stats.StripeCount}\nKhác: {stats.OtherPaymentCount}",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 50),
                AutoSize = true
            };
            providerPanel.Controls.Add(providerInfo);
            Controls.Add(providerPanel);
        }




    }
}
