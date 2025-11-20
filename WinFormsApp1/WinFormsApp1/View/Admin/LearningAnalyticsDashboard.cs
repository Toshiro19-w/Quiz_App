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
    public partial class LearningAnalyticsDashboard : UserControl
    {
        private AdminController _controller;

        public LearningAnalyticsDashboard()
        {
            _controller = new AdminController();
            InitializeComponent();
        }

        private void LearningAnalyticsDashboard_Load(object sender, EventArgs e)
        {
            LoadData();
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
                new { Title = "Khóa học", Value = stats.TotalCourses.ToString(), Color = Color.FromArgb(34, 197, 94) },
                new { Title = "Lớp học", Value = stats.TotalClasses.ToString(), Color = Color.FromArgb(168, 85, 247) },
                new { Title = "Học viên tham gia", Value = stats.TotalEnrollments.ToString(), Color = Color.FromArgb(14, 165, 233) }
            };

            int cardWidth = (Width - 65) / 3;
            foreach (var cardData in cards)
            {
                var card = CreateStatsCard(cardData.Title, cardData.Value, cardData.Color, new Point(0, 0), new Size(cardWidth, 130));
                card.Margin = new Padding(0, 0, 15, 0);
                flowPanel.Controls.Add(card);
            }

            Controls.Add(flowPanel);
            Resize += (s, e) => {
                flowPanel.Width = Width - 40;
                cardWidth = (Width - 65) / 3;
                foreach (Control card in flowPanel.Controls)
                    card.Width = cardWidth;
            };
        }

        private void CreateLearningCharts(LearningAnalytics stats)
        {
            var flowPanel = Controls.Find("flowPanel", false).FirstOrDefault();
            int yPos = flowPanel != null ? flowPanel.Bottom + 20 : 245;

            var chartFlow = new FlowLayoutPanel
            {
                Location = new Point(20, yPos),
                Width = Width - 40,
                AutoSize = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight
            };

            var topCoursesPanel = CreateResponsiveChartPanel("🏆 Top khóa học phổ biến", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            topCoursesPanel.Margin = new Padding(0, 0, 20, 0);
            var topCoursesChart = CreateTopCoursesChart(topCoursesPanel, stats.TopCourses);
            topCoursesPanel.Controls.Add(topCoursesChart);
            chartFlow.Controls.Add(topCoursesPanel);

            var testsPanel = CreateResponsiveChartPanel("📝 Bài kiểm tra theo tháng", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            var testsChart = CreateTestsChart(testsPanel, stats.TestsByMonth);
            testsPanel.Controls.Add(testsChart);
            chartFlow.Controls.Add(testsPanel);

            Controls.Add(chartFlow);
            Resize += (s, e) => chartFlow.Width = Width - 40;
        }

        private GunaChart CreateTopCoursesChart(Panel parent, List<(string CourseTitle, int EnrollmentCount)> topCourses)
        {
            var chart = new GunaChart
            {
                Location = new Point(10, 50),
                Size = new Size(parent.Width - 20, parent.Height - 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            var dataset = new GunaBarDataset { Label = "Học viên" };
            var colors = new[] {
                Color.FromArgb(34, 197, 94),
                Color.FromArgb(14, 165, 233),
                Color.FromArgb(251, 191, 36),
                Color.FromArgb(168, 85, 247),
                Color.FromArgb(239, 68, 68)
            };

            for (int i = 0; i < topCourses.Count && i < 5; i++)
            {
                var course = topCourses[i];
                var shortTitle = course.CourseTitle.Length > 20 ? course.CourseTitle.Substring(0, 20) + "..." : course.CourseTitle;
                dataset.DataPoints.Add(shortTitle, course.EnrollmentCount);
                dataset.FillColors.Add(colors[i]);
            }

            chart.Datasets.Add(dataset);
            chart.Legend.Display = false;
            return chart;
        }

        private GunaChart CreateTestsChart(Panel parent, Dictionary<int, int> testsByMonth)
        {
            var chart = new GunaChart
            {
                Location = new Point(10, 50),
                Size = new Size(parent.Width - 20, parent.Height - 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            var dataset = new GunaLineDataset { Label = "Bài kiểm tra" };
            string[] months = { "T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10", "T11", "T12" };
            
            for (int i = 1; i <= 12; i++)
            {
                dataset.DataPoints.Add(months[i-1], testsByMonth[i]);
            }
            
            dataset.BorderColor = Color.FromArgb(251, 191, 36);
            chart.Datasets.Add(dataset);
            chart.Legend.Display = true;
            return chart;
        }


    }
}
