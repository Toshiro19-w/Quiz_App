using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.Charts.WinForms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;
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
            InitializeFilterControls();
            LoadData();
        }

        private async void InitializeFilterControls()
        {
            // ✅ Set default dates using helper
            DateRangeValidationHelper.InitializeDatePickers(startDatePicker, endDatePicker, 30);

            // ✅ Setup date range validation
            DateRangeValidationHelper.SetupDateRangeValidation(
                startDatePicker,
                endDatePicker,
                applyButton
            );

            // Load categories
            categoryCombo.Items.Clear();
            categoryCombo.Items.Add("Tất cả");
            try
            {
                var categories = await _controller.GetCategoriesAsync();
                foreach (var cat in categories)
                {
                    categoryCombo.Items.Add(cat.Name);
                }
            }
            catch { }
            categoryCombo.SelectedIndex = 0;

            // Wire up events
            applyButton.Click += (s, e) => LoadData();
            resetButton.Click += (s, e) =>
            {
                DateRangeValidationHelper.InitializeDatePickers(startDatePicker, endDatePicker, 30);
                categoryCombo.SelectedIndex = 0;
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
                var category = categoryCombo.SelectedItem?.ToString();

                var learningStats = await _controller.GetLearningAnalyticsAsync(startDate, endDate, category);
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
            statsFlowPanel.Controls.Clear();
            
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
                statsFlowPanel.Controls.Add(card);
            }
        }

        private void CreateLearningCharts(LearningAnalytics stats)
        {
            chartsFlowPanel.Controls.Clear();

            var topCoursesPanel = CreateResponsiveChartPanel("🏆 Top khóa học phổ biến", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            topCoursesPanel.Margin = new Padding(0, 0, 20, 0);
            var topCoursesChart = CreateTopCoursesChart(topCoursesPanel, stats.TopCourses);
            topCoursesPanel.Controls.Add(topCoursesChart);
            chartsFlowPanel.Controls.Add(topCoursesPanel);

            var testsPanel = CreateResponsiveChartPanel("📝 Bài kiểm tra theo tháng", new Point(0, 0), new Size(540, 350), AnchorStyles.None);
            var testsChart = CreateTestsChart(testsPanel, stats.TestsByMonth);
            testsPanel.Controls.Add(testsChart);
            chartsFlowPanel.Controls.Add(testsPanel);
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
            string[] months = { "Tháng 01", "Tháng 02", "Tháng 03", "Tháng 04", "Tháng 05", "Tháng 06", "Tháng 07", "Tháng 08", "Tháng 09", "Tháng 10", "Tháng 11", "Tháng 12" };
            
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
