using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using static WinFormsApp1.Helpers.ResponsiveLayoutHelper;
using static WinFormsApp1.Helpers.UIComponentHelper;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.Admin
{
    public class UserAnalyticsDashboard : UserControl
    {
        private AdminController _controller;

        public UserAnalyticsDashboard()
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
                Text = "👥 Phân tích người dùng",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            Controls.Add(titleLabel);

            try
            {
                var userStats = await _controller.GetUserAnalyticsAsync();
                CreateUserStatsCards(userStats);
                CreateUserCharts(userStats);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void CreateUserStatsCards(UserAnalytics stats)
        {
            var flowPanel = CreateResponsiveCardContainer(this, 80);

            var cards = new[]
            {
                new { Title = "👨💼 Admin", Value = stats.AdminCount.ToString(), Color = Color.FromArgb(239, 68, 68) },
                new { Title = "👨🏫 Giáo viên", Value = stats.TeacherCount.ToString(), Color = Color.FromArgb(34, 197, 94) },
                new { Title = "👨🎓 Học sinh", Value = stats.StudentCount.ToString(), Color = Color.FromArgb(14, 165, 233) },
                new { Title = "🆕 Người dùng mới", Value = stats.NewUsersThisMonth.ToString(), Color = Color.FromArgb(251, 191, 36) }
            };

            foreach (var cardData in cards)
            {
                var card = CreateStatsCard(cardData.Title, cardData.Value, cardData.Color, new Point(0, 0), new Size(280, 110));
                card.Margin = new Padding(0, 0, 20, 20);
                flowPanel.Controls.Add(card);
            }

            Controls.Add(flowPanel);
        }

        private void CreateUserCharts(UserAnalytics stats)
        {
            int yPos = 220;

            var rolePanel = CreateResponsiveChartPanel("📊 Phân bố vai trò", new Point(20, yPos), new Size(540, 300), AnchorStyles.Top | AnchorStyles.Left);
            var roleInfo = new Label
            {
                Text = $"Admin: {stats.AdminCount}\nGiáo viên: {stats.TeacherCount}\nHọc sinh: {stats.StudentCount}",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 50),
                AutoSize = true
            };
            rolePanel.Controls.Add(roleInfo);
            Controls.Add(rolePanel);

            var genderPanel = CreateResponsiveChartPanel("⚧ Phân bố giới tính", new Point(580, yPos), new Size(540, 300), AnchorStyles.Top | AnchorStyles.Right);
            var genderInfo = new Label
            {
                Text = $"Nam: {stats.MaleCount}\nNữ: {stats.FemaleCount}\nKhác: {stats.OtherCount}",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 50),
                AutoSize = true
            };
            genderPanel.Controls.Add(genderInfo);
            Controls.Add(genderPanel);

            yPos += 320;
            var activePanel = CreateResponsiveChartPanel("🔥 Người dùng hoạt động", new Point(20, yPos), new Size(1100, 250), AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            var activeInfo = new Label
            {
                Text = $"Hoạt động hôm nay: {stats.ActiveToday}\nHoạt động tuần này: {stats.ActiveThisWeek}",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 50),
                AutoSize = true
            };
            activePanel.Controls.Add(activeInfo);
            Controls.Add(activePanel);
        }
    }
}
