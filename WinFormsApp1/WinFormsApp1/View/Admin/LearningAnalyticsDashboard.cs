using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;
using static WinFormsApp1.Helpers.ResponsiveLayoutHelper;
using static WinFormsApp1.Helpers.UIComponentHelper;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.Admin
{
    public class LearningAnalyticsDashboard : UserControl
    {
        private AdminController _controller;

        public LearningAnalyticsDashboard()
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
                Text = "🎓 Phân tích học tập",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            Controls.Add(titleLabel);

            try
            {
                var learningStats = await _controller.GetLearningAnalyticsAsync();
                CreateLearningStatsCards(learningStats);
                CreateLearningCharts(learningStats);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void CreateLearningStatsCards(LearningAnalytics stats)
        {
            var flowPanel = CreateResponsiveCardContainer(this, 80);

            var cards = new[]
            {
                new { Title = "📚 Khóa học", Value = stats.TotalCourses.ToString(), Color = Color.FromArgb(34, 197, 94) },
                new { Title = "🏫 Lớp học", Value = stats.TotalClasses.ToString(), Color = Color.FromArgb(168, 85, 247) },
                new { Title = "👥 Học viên", Value = stats.TotalEnrollments.ToString(), Color = Color.FromArgb(14, 165, 233) },
                new { Title = "✅ Tỷ lệ hoàn thành", Value = $"{stats.CompletionRate:F1}%", Color = Color.FromArgb(251, 191, 36) }
            };

            foreach (var cardData in cards)
            {
                var card = CreateStatsCard(cardData.Title, cardData.Value, cardData.Color, new Point(0, 0), new Size(280, 110));
                card.Margin = new Padding(0, 0, 20, 20);
                flowPanel.Controls.Add(card);
            }

            Controls.Add(flowPanel);
        }

        private void CreateLearningCharts(LearningAnalytics stats)
        {
            int yPos = 220;
            int halfWidth = (Width - 60) / 2;

            var completionPanel = CreateResponsiveChartPanel(
                "✅ Tỷ lệ hoàn thành khóa học", 
                new Point(20, yPos), 
                new Size(halfWidth, 300),
                AnchorStyles.Top | AnchorStyles.Left);
            var completionInfo = new Label
            {
                Text = $"Hoàn thành: {stats.CompletionRate:F1}%\nChưa hoàn thành: {100 - stats.CompletionRate:F1}%",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 50),
                AutoSize = true
            };
            var progressBar = new ProgressBar
            {
                Location = new Point(10, 120),
                Size = new Size(520, 30),
                Value = (int)stats.CompletionRate,
                Style = ProgressBarStyle.Continuous
            };
            completionPanel.Controls.Add(completionInfo);
            completionPanel.Controls.Add(progressBar);
            Controls.Add(completionPanel);

            var testPanel = CreateResponsiveChartPanel(
                "📝 Thống kê bài kiểm tra", 
                new Point(halfWidth + 40, yPos), 
                new Size(halfWidth, 300),
                AnchorStyles.Top | AnchorStyles.Right);
            var testInfo = new Label
            {
                Text = $"Tổng bài thi: {stats.TotalTests}\nBài thi tháng này: {stats.TestsThisMonth}",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 50),
                AutoSize = true
            };
            testPanel.Controls.Add(testInfo);
            Controls.Add(testPanel);

            yPos += 320;
            var activityPanel = CreateResponsiveChartPanel(
                "📊 Hoạt động giảng dạy & học tập", 
                new Point(20, yPos), 
                new Size(Width - 60, 250),
                AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            var activityInfo = new Label
            {
                Text = $"Giáo viên tích cực: {stats.ActiveTeachers}\nHọc viên tích cực: {stats.ActiveStudents}",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 50),
                AutoSize = true
            };
            activityPanel.Controls.Add(activityInfo);
            Controls.Add(activityPanel);
        }
    }
}
