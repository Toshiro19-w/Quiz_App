using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.Charts.WinForms;
using WinFormsApp1.Controllers;
using WinFormsApp1.ViewModels;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;
using static WinFormsApp1.Helpers.ResponsiveLayoutHelper;
using static WinFormsApp1.Helpers.UIComponentHelper;

namespace WinFormsApp1.View.Admin
{
    public partial class ExecutiveReportControl : UserControl
    {
        private readonly AdminController _adminController;
        private Panel topPanel;
        private Panel contentPanel;
        private FlowLayoutPanel statsFlowPanel;
        private FlowLayoutPanel chartsFlowPanel;
        private ComboBox filterCombo;
        private DateTimePicker startDatePicker;
        private DateTimePicker endDatePicker;
        private Button applyButton;
        private Button exportButton;

        /// <summary>
        /// Shorthand for LanguageHelper.GetString
        /// </summary>
        private static string Lang(string key) => LanguageHelper.GetString(key);
        private static string Lang(string key, params object[] args) => LanguageHelper.GetString(key, args);

        public ExecutiveReportControl()
        {
            _adminController = new AdminController();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1200, 800);
            this.BackColor = Color.FromArgb(248, 249, 250);

            // Top Panel (Filters)
            topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };

            var filterLabel = new Label
            {
                Text = $"{Lang("TimePeriod")}:",
                AutoSize = true,
                Location = new Point(20, 20),
                Font = new Font("Segoe UI", 10)
            };

            filterCombo = new ComboBox
            {
                Location = new Point(100, 18),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            filterCombo.Items.AddRange(new object[] { Lang("Today"), Lang("ThisWeek"), Lang("ThisMonth"), Lang("Custom") });
            filterCombo.SelectedIndex = 2; // Default to Month
            filterCombo.SelectedIndexChanged += FilterCombo_SelectedIndexChanged;

            // ✅ DateTimePicker with format based on language
            startDatePicker = new DateTimePicker
            {
                Location = new Point(270, 18),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = LanguageHelper.DateFormatPattern,
                Width = 120,
                Visible = false
            };

            endDatePicker = new DateTimePicker
            {
                Location = new Point(400, 18),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = LanguageHelper.DateFormatPattern,
                Width = 120,
                Visible = false
            };

            applyButton = new Button
            {
                Text = Lang("Apply"),
                Location = new Point(530, 17),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(14, 165, 233),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };
            applyButton.FlatAppearance.BorderSize = 0;
            applyButton.Click += (s, e) => ApplyFilter();

            exportButton = new Button
            {
                Text = $"📥 {Lang("ExportReport")}",
                Size = new Size(150, 35),
                BackColor = Color.FromArgb(34, 197, 94),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            exportButton.Location = new Point(topPanel.Width - 170, 12);
            exportButton.FlatAppearance.BorderSize = 0;
            exportButton.Click += ExportButton_Click;

            topPanel.Controls.AddRange(new Control[] { filterLabel, filterCombo, startDatePicker, endDatePicker, applyButton, exportButton });
            topPanel.Resize += (s, e) => { exportButton.Location = new Point(topPanel.Width - 170, 12); };

            // Content Panel (Scrollable)
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20)
            };

            // Stats Flow Panel
            statsFlowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 150,
                AutoSize = false,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            // Charts Flow Panel
            chartsFlowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(0, 20, 0, 0)
            };

            contentPanel.Controls.Add(chartsFlowPanel);
            contentPanel.Controls.Add(statsFlowPanel);

            this.Controls.Add(contentPanel);
            this.Controls.Add(topPanel);

            // Initial Load
            this.Load += ExecutiveReportControl_Load;
            
            // Resize logic
            this.Resize += ExecutiveReportControl_Resize;
            
            // ✅ Setup date range validation
            InitializeDateValidation();
        }

        /// <summary>
        /// Initialize date validation for custom date range
        /// </summary>
        private void InitializeDateValidation()
        {
            // ✅ Initialize DatePickers with default range (last 30 days)
            DateRangeValidationHelper.InitializeDatePickers(startDatePicker, endDatePicker, 30);

            // ✅ Setup date range validation
            DateRangeValidationHelper.SetupDateRangeValidation(
                startDatePicker,
                endDatePicker,
                applyButton
            );
        }

        private void ExecutiveReportControl_Load(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ExecutiveReportControl_Resize(object sender, EventArgs e)
        {
            if (statsFlowPanel != null)
            {
                statsFlowPanel.Width = contentPanel.Width - 40;
                int cardWidth = (statsFlowPanel.Width - 60) / 4; // 4 cards with gaps
                foreach (Control card in statsFlowPanel.Controls)
                    card.Width = cardWidth;
            }
            if (chartsFlowPanel != null)
            {
                chartsFlowPanel.Width = contentPanel.Width - 40;
            }
        }

        private void FilterCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isCustom = filterCombo.SelectedIndex == 3;
            startDatePicker.Visible = isCustom;
            endDatePicker.Visible = isCustom;
            applyButton.Visible = isCustom;

            // ✅ Initialize date pickers when switching to custom mode
            if (isCustom)
            {
                // Set end date to today
                endDatePicker.Value = DateTime.Now;
                // Set start date to 1 month before end date
                startDatePicker.Value = endDatePicker.Value.AddMonths(-1);
                
                // Validate dates
                DateRangeValidationHelper.ValidateDateRange(
                    startDatePicker,
                    endDatePicker,
                    applyButton,
                    Color.FromArgb(14, 165, 233),
                    Color.Gray
                );
            }
            else
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
                    int diff = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7;
                    start = now.Date.AddDays(-1 * diff);
                    end = now.Date.AddDays(1).AddTicks(-1);
                    break;
                case 2: // This Month
                    start = new DateTime(now.Year, now.Month, 1);
                    end = now.Date.AddDays(1).AddTicks(-1);
                    break;
                case 3: // Custom
                    // ✅ Validate using helper with message box
                    if (!DateRangeValidationHelper.ValidateWithMessage(
                        startDatePicker,
                        endDatePicker,
                        applyButton,
                        this.FindForm()))
                    {
                        return;
                    }
                    start = startDatePicker.Value.Date;
                    end = endDatePicker.Value.Date.AddDays(1).AddTicks(-1);
                    break;
            }

            await LoadData(start, end);
        }

        private async Task LoadData(DateTime? start, DateTime? end)
        {
            try
            {
                // Parallel data fetching for performance
                var statsTask = _adminController.GetDashboardStatsAsync(start, end);
                var revenueTrendTask = _adminController.GetRevenueTrendAsync(start, end);
                var userAnalyticsTask = _adminController.GetUserAnalyticsAsync(start, end);
                var learningAnalyticsTask = _adminController.GetLearningAnalyticsAsync(start, end);

                await Task.WhenAll(statsTask, revenueTrendTask, userAnalyticsTask, learningAnalyticsTask);

                var stats = statsTask.Result;
                var revenueTrend = revenueTrendTask.Result;
                var userAnalytics = userAnalyticsTask.Result;
                var learningAnalytics = learningAnalyticsTask.Result;

                UpdateStatsCards(stats, userAnalytics, learningAnalytics);
                UpdateCharts(revenueTrend, userAnalytics, learningAnalytics);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Lang("DataLoadError", ex.Message), Lang("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatsCards(DashboardStats stats, UserAnalytics userStats, LearningAnalytics learningStats)
        {
            statsFlowPanel.Controls.Clear();

            var cards = new[]
            {
                new { Title = Lang("Revenue"), Value = LanguageHelper.FormatVND(stats.TotalRevenue), Color = Color.FromArgb(14, 165, 233), Icon = "💰" },
                new { Title = Lang("NewUsers"), Value = userStats.NewUsersThisMonth.ToString(), Color = Color.FromArgb(34, 197, 94), Icon = "👥" },
                new { Title = Lang("CoursesSold"), Value = learningStats.TotalEnrollments.ToString(), Color = Color.FromArgb(168, 85, 247), Icon = "📚" },
                new { Title = Lang("Tests"), Value = stats.TotalTestResults.ToString(), Color = Color.FromArgb(251, 191, 36), Icon = "📝" }
            };

            int cardWidth = (statsFlowPanel.Width - 60) / 4;
            foreach (var cardData in cards)
            {
                var card = CreateStatsCard(cardData.Title, cardData.Value, cardData.Color, new Point(0, 0), new Size(cardWidth, 130));
                card.Margin = new Padding(0, 0, 15, 0);
                statsFlowPanel.Controls.Add(card);
            }
        }

        private void UpdateCharts(Dictionary<string, decimal> revenueTrend, UserAnalytics userStats, LearningAnalytics learningStats)
        {
            chartsFlowPanel.Controls.Clear();

            // 1. Revenue Trend Chart (Large, Full Width)
            var revenuePanel = CreateResponsiveChartPanel($"📈 {Lang("RevenueTrend")}", new Point(0, 0), new Size(chartsFlowPanel.Width - 20, 400), AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            revenuePanel.Margin = new Padding(0, 0, 0, 20);
            var revenueChart = CreateLineChart(revenuePanel, revenueTrend, Lang("Revenue"), Color.FromArgb(14, 165, 233));
            revenuePanel.Controls.Add(revenueChart);
            chartsFlowPanel.Controls.Add(revenuePanel);

            // 2. User Growth (Half Width)
            var userPanel = CreateResponsiveChartPanel($"👥 {Lang("UserGrowth")}", new Point(0, 0), new Size((chartsFlowPanel.Width / 2) - 20, 350), AnchorStyles.None);
            userPanel.Margin = new Padding(0, 0, 20, 20);
            // Convert Dictionary<int, int> to Dictionary<string, decimal> for the generic chart helper if needed, or custom
            var userGrowthData = userStats.NewUsersByMonth.ToDictionary(k => $"{Lang("Month")} {k.Key}", v => (decimal)v.Value);
            var userChart = CreateBarChart(userPanel, userGrowthData, Lang("Users"), Color.FromArgb(34, 197, 94));
            userPanel.Controls.Add(userChart);
            chartsFlowPanel.Controls.Add(userPanel);

            // 3. Top Courses (Half Width)
            var coursePanel = CreateResponsiveChartPanel($"🏆 {Lang("TopPopularCourses")}", new Point(0, 0), new Size((chartsFlowPanel.Width / 2) - 20, 350), AnchorStyles.None);
            coursePanel.Margin = new Padding(0, 0, 0, 20);
            
            // Custom list for top courses - Padding top để không che title
            var courseListPanel = new Panel 
            { 
                Location = new Point(10, 45),  // Đặt dưới title label
                Size = new Size(coursePanel.Width - 20, coursePanel.Height - 55),
                Padding = new Padding(0), 
                AutoScroll = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            int y = 5;
            foreach(var course in learningStats.TopCourses)
            {
                var row = new Panel { Size = new Size(courseListPanel.Width - 20, 40), Location = new Point(0, y), BackColor = Color.White };
                var lblName = new Label { Text = course.CourseTitle, AutoSize = true, Location = new Point(5, 10), Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(64, 64, 64) };
                var lblCount = new Label { Text = $"{course.EnrollmentCount} {Lang("Students")}", AutoSize = true, Location = new Point(row.Width - 100, 10), Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(14, 165, 233), Anchor = AnchorStyles.Top | AnchorStyles.Right };
                
                row.Controls.Add(lblName);
                row.Controls.Add(lblCount);
                row.Paint += (s, e) => { ControlPaint.DrawBorder(e.Graphics, row.ClientRectangle, Color.FromArgb(229, 231, 235), ButtonBorderStyle.Solid); };
                
                courseListPanel.Controls.Add(row);
                y += 45;
            }
            coursePanel.Controls.Add(courseListPanel);
            chartsFlowPanel.Controls.Add(coursePanel);
        }

        private GunaChart CreateLineChart(Panel parent, Dictionary<string, decimal> data, string label, Color color)
        {
            var chart = new GunaChart
            {
                Location = new Point(10, 50),
                Size = new Size(parent.Width - 20, parent.Height - 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            var dataset = new GunaLineDataset
            {
                Label = label,
                BorderColor = color,
                PointRadius = 5,
                PointStyle = PointStyle.Circle
            };

            foreach (var item in data)
            {
                dataset.DataPoints.Add(item.Key, (double)item.Value);
            }

            chart.Datasets.Add(dataset);
            return chart;
        }

        private GunaChart CreateBarChart(Panel parent, Dictionary<string, decimal> data, string label, Color color)
        {
            var chart = new GunaChart
            {
                Location = new Point(10, 50),
                Size = new Size(parent.Width - 20, parent.Height - 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            var dataset = new GunaBarDataset
            {
                Label = label
            };
            dataset.FillColors.Add(color);

            foreach (var item in data)
            {
                dataset.DataPoints.Add(item.Key, (double)item.Value);
            }

            chart.Datasets.Add(dataset);
            return chart;
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Lang("ExportFeatureInDevelopment"), Lang("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
