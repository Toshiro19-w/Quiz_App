using Guna.UI2.AnimatorNS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.View.User.Forms;

namespace WinFormsApp1.View.User.Controls
{
    public partial class MyCoursesControl : UserControl
    {
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalRecords = 0;
        private List<Course> _allCourses = new List<Course>();
        private string _searchFilter = "";
		
        public MyCoursesControl()
        {
            InitializeComponent();
            LocalizeUI();
            cmbPageSize.SelectedIndex = 0;
            cbbSearch.SelectedIndex = 0;
            LoadCourses();
            
            flowCourses.Resize += (s, e) => RefreshRowWidths();
        }

        private void LocalizeUI()
        {
            // Header
            lblTitle.Text = LanguageHelper.GetString("MyCourses");
            
            // Action buttons
            btnCreateCourse.Text = "➕ " + LanguageHelper.GetString("CreateCourse");
            btnRevenue.Text = "📊 " + LanguageHelper.GetString("Revenue");
            btnFlashcards.Text = "🗂️ " + LanguageHelper.GetString("MyFlashcards");
            
            // Filter labels
            lblShowLabel.Text = LanguageHelper.GetString("Show");
            lblEntriesLabel.Text = LanguageHelper.GetString("Entries");
            lblSearchLabel.Text = LanguageHelper.GetString("Search") + ":";
            
            // Search filter combobox
            cbbSearch.Items.Clear();
            cbbSearch.Items.AddRange(new object[] {
                LanguageHelper.GetString("FilterAll"),
                LanguageHelper.GetString("FilterTitle"),
                LanguageHelper.GetString("FilterCategory"),
                LanguageHelper.GetString("FilterPrice"),
                LanguageHelper.GetString("FilterCreatedAt"),
                LanguageHelper.GetString("FilterStatus"),
                LanguageHelper.GetString("FilterPublished")
            });
            
            // Table headers
            lblHeaderCover.Text = LanguageHelper.GetString("Image");
            lblHeaderTitle.Text = LanguageHelper.GetString("Title");
            lblHeaderCategory.Text = LanguageHelper.GetString("Category");
            lblHeaderPrice.Text = LanguageHelper.GetString("Price");
            lblHeaderDate.Text = LanguageHelper.GetString("CreatedAt");
            lblHeaderModeration.Text = LanguageHelper.GetString("Status");
            lblHeaderStatus.Text = LanguageHelper.GetString("Published");
            lblHeaderActions.Text = LanguageHelper.GetString("Actions");
            
            // Pagination buttons
            btnFirstPage.Text = LanguageHelper.GetString("First");
            btnPrevPage.Text = LanguageHelper.GetString("Previous");
            btnNextPage.Text = LanguageHelper.GetString("Next");
            btnLastPage.Text = LanguageHelper.GetString("Last");
        }
        
        private void RefreshRowWidths()
        {
            foreach (Control control in flowCourses.Controls)
            {
                if (control is CourseRowControl row)
                {
                    row.Width = flowCourses.ClientSize.Width - 2;
                }
            }
        }

        private async void LoadCourses()
        {
            try
            {
                var userId = AuthHelper.CurrentUser?.UserId;
                if (!userId.HasValue)
                {
                    MessageBox.Show(LanguageHelper.GetString("PleaseLoginMessage"), LanguageHelper.GetString("Notification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var context = new LearningPlatformContext();
                _allCourses = await context.Courses
                    .Include(c => c.Category)
                    .Where(c => c.OwnerId == userId.Value)
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync();

                _totalRecords = _allCourses.Count;
                ApplyFiltersAndLoadPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageHelper.GetString("DataLoadError", ex.Message), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFiltersAndLoadPage()
        {
            var filteredCourses = _allCourses.AsEnumerable();

            // Apply search filter based on selected criteria
            string searchText = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                var filterTitle = LanguageHelper.GetString("FilterTitle");
                var filterCategory = LanguageHelper.GetString("FilterCategory");
                var filterPrice = LanguageHelper.GetString("FilterPrice");
                var filterCreatedAt = LanguageHelper.GetString("FilterCreatedAt");
                var filterStatus = LanguageHelper.GetString("FilterStatus");
                var filterPublished = LanguageHelper.GetString("FilterPublished");
                var statusPublished = LanguageHelper.GetString("StatusPublished");
                var statusDraft = LanguageHelper.GetString("StatusDraft");

                filteredCourses = _searchFilter switch
                {
                    var f when f == filterTitle => filteredCourses.Where(c => 
                        c.Title.ToLower().Contains(searchText)),
                    
                    var f when f == filterCategory => filteredCourses.Where(c => 
                        c.Category?.Name?.ToLower().Contains(searchText) == true),
                    
                    var f when f == filterPrice => filteredCourses.Where(c => 
                        c.Price.ToString().Contains(searchText)),
                    
                    var f when f == filterCreatedAt => filteredCourses.Where(c => 
                        c.CreatedAt.ToString("dd/MM/yyyy").Contains(searchText) ||
                        c.CreatedAt.ToString("dd-MM-yyyy").Contains(searchText) ||
                        c.CreatedAt.ToString("yyyy").Contains(searchText)),
                    
                    var f when f == filterStatus => filteredCourses.Where(c =>
                    {
                        var status = GetModerationStatusText(c.ModerationStatus);
                        return status.ToLower().Contains(searchText);
                    }),
                    
                    var f when f == filterPublished => filteredCourses.Where(c =>
                    {
                        var publishStatus = c.IsPublished ? statusPublished : statusDraft;
                        return publishStatus.Contains(searchText);
                    }),
                    
                    _ => filteredCourses.Where(c =>
                        c.Title.ToLower().Contains(searchText) ||
                        (c.Slug != null && c.Slug.ToLower().Contains(searchText)) ||
                        (c.Category?.Name?.ToLower().Contains(searchText) == true) ||
                        c.Price.ToString().Contains(searchText) ||
                        c.CreatedAt.ToString("dd/MM/yyyy").Contains(searchText) ||
                        GetModerationStatusText(c.ModerationStatus).ToLower().Contains(searchText) ||
                        (c.IsPublished ? statusPublished : statusDraft).Contains(searchText))
                };
            }

            _totalRecords = filteredCourses.Count();

            // Calculate pagination
            int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            if (_currentPage > totalPages && totalPages > 0)
                _currentPage = totalPages;

            var pagedCourses = filteredCourses
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            LoadDataToGrid(pagedCourses);
            UpdatePaginationUI(totalPages);
        }

        private void LoadDataToGrid(List<Course> courses)
        {
            flowCourses.Controls.Clear();

            if (courses.Count == 0)
            {
                var lblEmpty = new Label
                {
                    Text = LanguageHelper.GetString("NoCourseYet"),
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    ForeColor = ColorPalette.TextSecondary,
                    AutoSize = true,
                    Location = new Point(400, 200)
                };
                flowCourses.Controls.Add(lblEmpty);
                return;
            }

            int rowIndex = (_currentPage - 1) * _pageSize + 1;
            foreach (var course in courses)
            {
                var row = CreateCourseRow(course, rowIndex++);
                flowCourses.Controls.Add(row);
            }
        }

        private CourseRowControl CreateCourseRow(Course course, int index)
        {
            var row = new CourseRowControl
            {
                Width = flowCourses.ClientSize.Width - 2
            };

            row.SetData(course, index);

            row.SubmitClicked += (s, c) => SubmitForReview(c);
            row.ViewClicked += (s, c) => ViewCourse(c);
            row.EditClicked += (s, c) => EditCourse(c);
            row.DeleteClicked += (s, c) => DeleteCourse(c);

            return row;
        }

        private async void SubmitForReview(Course course)
        {
            try
            {
                using var context = new LearningPlatformContext();
                
                var fullCourse = await context.Courses
                    .Include(c => c.CourseChapters)
                        .ThenInclude(ch => ch.Lessons)
                            .ThenInclude(l => l.LessonContents)
                    .FirstOrDefaultAsync(c => c.CourseId == course.CourseId);
                
                if (fullCourse == null)
                {
                    MessageBox.Show(LanguageHelper.GetString("CourseNotFound"), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                var autoCheckResults = Services.CourseModerationService.RunAutoChecks(fullCourse, context);
                var canPublish = Services.CourseModerationService.CanPublish(autoCheckResults);
                var autoScore = Services.CourseModerationService.CalculateAutoScore(autoCheckResults);
                
                var errorCount = autoCheckResults.Count(r => r.Severity == "Error" && !r.Passed);
                var warningCount = autoCheckResults.Count(r => r.Severity == "Warning" && !r.Passed);
                
                var message = LanguageHelper.GetString("AutoCheckResult") + "\n\n";
                message += LanguageHelper.GetString("Score") + $": {autoScore}/100\n";
                message += LanguageHelper.GetString("Errors") + $": {errorCount}\n";
                message += LanguageHelper.GetString("Warnings") + $": {warningCount}\n\n";
                
                if (!canPublish)
                {
                    message += "❌ " + LanguageHelper.GetString("CourseNotEligible") + "\n\n";
                    message += LanguageHelper.GetString("ErrorsToFix") + ":\n";
                    foreach (var result in autoCheckResults.Where(r => r.Severity == "Error" && !r.Passed))
                    {
                        message += $"• {result.Message}\n";
                    }
                    
                    MessageBox.Show(message, LanguageHelper.GetString("CannotSubmit"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (warningCount > 0)
                {
                    message += "⚠️ " + LanguageHelper.GetString("HasWarnings") + "\n\n";
                    message += LanguageHelper.GetString("Warnings") + ":\n";
                    foreach (var checkResult in autoCheckResults.Where(r => r.Severity == "Warning" && !r.Passed))
                    {
                        message += $"• {checkResult.Message}\n";
                    }
                    
                    var confirmResult = MessageBox.Show(message, LanguageHelper.GetString("ConfirmSubmit"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmResult != DialogResult.Yes) return;
                }
                else
                {
                    message += "✅ " + LanguageHelper.GetString("CourseReady") + "\n\n" + LanguageHelper.GetString("ConfirmSubmitCourse");
                    var confirmResult = MessageBox.Show(message, LanguageHelper.GetString("ConfirmSubmit"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmResult != DialogResult.Yes) return;
                }
                
                if (Services.CourseModerationService.SubmitForReview(course.CourseId, context))
                {
                    MessageBox.Show(LanguageHelper.GetString("CourseSubmitted"), 
                        LanguageHelper.GetString("Success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCourses();
                }
                else
                {
                    MessageBox.Show(LanguageHelper.GetString("CannotSubmitCourse"), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageHelper.GetString("GenericError", ex.Message), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		//private async void ViewCourse(Course course)
		//{
		//    try
		//    {
		//        using var context = new LearningPlatformContext();

		//        var courseWithDetails = await context.Courses
		//            .Include(c => c.CourseChapters)
		//                .ThenInclude(ch => ch.Lessons)
		//            .FirstOrDefaultAsync(c => c.CourseId == course.CourseId);

		//        if (courseWithDetails == null)
		//        {
		//            MessageBox.Show("Không tìm thấy khóa học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//            return;
		//        }

		//        var firstLesson = courseWithDetails.CourseChapters
		//            .OrderBy(ch => ch.OrderIndex)
		//            .SelectMany(ch => ch.Lessons.OrderBy(l => l.OrderIndex))
		//            .FirstOrDefault();

		//        if (firstLesson == null)
		//        {
		//            MessageBox.Show("Khóa học chưa có bài học nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
		//            return;
		//        }

		//        var form = this.FindForm();
		//        if (form is MainContainer mainContainer)
		//        {
		//            var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
		//            if (mainPanel != null)
		//            {
		//                mainPanel.Controls.Clear();

		//                var lessonDetailControl = new LessonDetailControl();
		//                lessonDetailControl.Dock = DockStyle.Fill;
		//                mainPanel.Controls.Add(lessonDetailControl);

		//                await lessonDetailControl.LoadLessonAsync(courseWithDetails.Slug, firstLesson.LessonId);
		//            }
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        MessageBox.Show($"Lỗi khi mở khóa học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//    }
		//}
		private async void ViewCourse(Course course)
		{
			try
			{
				// SỬA LỖI: Chỉ lấy lesson đầu tiên, không load toàn bộ course structure
				using var context = new LearningPlatformContext();

				var firstLesson = await context.Lessons
					.AsNoTracking() // QUAN TRỌNG: Không tracking
					.Include(l => l.Chapter)
					.Where(l => l.Chapter.CourseId == course.CourseId)
					.OrderBy(l => l.Chapter.OrderIndex)
					.ThenBy(l => l.OrderIndex)
					.FirstOrDefaultAsync();

				if (firstLesson == null)
				{
					MessageBox.Show(LanguageHelper.GetString("NoLessonsYet"), LanguageHelper.GetString("Notification"), MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				var form = this.FindForm();
				if (form is MainContainer mainContainer)
				{
					var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
					if (mainPanel != null)
					{
						mainPanel.Controls.Clear();

						var lessonDetailControl = new LessonDetailControl();
						lessonDetailControl.Dock = DockStyle.Fill;
						mainPanel.Controls.Add(lessonDetailControl);

						// Truyền course.Slug (có sẵn từ tham số)
						await lessonDetailControl.LoadLessonAsync(course.Slug, firstLesson.LessonId);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(LanguageHelper.GetString("CourseOpenError", ex.Message), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private async void EditCourse(Course course)
        {
            try
            {
                var builderCtrl = new CourseBuilderController();
                var vm = await builderCtrl.LoadCourseAsync(course.CourseId);
                
                if (vm == null)
                {
                    MessageBox.Show(LanguageHelper.GetString("CannotLoadCourseData"), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                using var form = new CourseBuilderForm(vm, course.CourseId);
                form.StartPosition = FormStartPosition.CenterParent;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadCourses();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageHelper.GetString("EditorOpenError", ex.Message), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DeleteCourse(Course course)
        {
            var result = MessageBox.Show(
                LanguageHelper.GetString("DeleteCourseConfirm", course.Title),
                LanguageHelper.GetString("ConfirmDelete"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using var context = new LearningPlatformContext();
                    var courseToDelete = await context.Courses.FindAsync(course.CourseId);
                    if (courseToDelete != null)
                    {
                        context.Courses.Remove(courseToDelete);
                        await context.SaveChangesAsync();

                        ToastHelper.Show(this.FindForm(), LanguageHelper.GetString("CourseDeletedSuccess"));
                        LoadCourses();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LanguageHelper.GetString("CourseDeleteError", ex.Message), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdatePaginationUI(int totalPages)
        {
            lblPageInfo.Text = LanguageHelper.GetString("ShowingEntries", 
                (_currentPage - 1) * _pageSize + 1, 
                Math.Min(_currentPage * _pageSize, _totalRecords), 
                _totalRecords);

            btnFirstPage.Enabled = _currentPage > 1;
            btnPrevPage.Enabled = _currentPage > 1;
            btnNextPage.Enabled = _currentPage < totalPages;
            btnLastPage.Enabled = _currentPage < totalPages;

            lblCurrentPage.Text = _currentPage.ToString();
        }

        private void BtnCreateCourse_Click(object sender, EventArgs e)
        {
            using var builder = new CourseBuilderForm();
            builder.StartPosition = FormStartPosition.CenterParent;
            var owner = this.FindForm();
            if (owner != null)
            {
                builder.ShowDialog(owner);
            }
            else
            {
                builder.ShowDialog();
            }

            if (builder.DialogResult == DialogResult.OK)
            {
                LoadCourses();
            }
        }

        private void BtnRevenue_Click(object sender, EventArgs e)
        {
            var form = this.FindForm();
            if (form is MainContainer mainContainer)
            {
                var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
                if (mainPanel != null)
                {
                    mainPanel.Controls.Clear();
                    var revenueControl = new RevenueControl();
                    revenueControl.Dock = DockStyle.Fill;
                    mainPanel.Controls.Add(revenueControl);
                }
            }
        }

        private void BtnFlashcards_Click(object sender, EventArgs e)
        {
            var form = this.FindForm();
            if (form is MainContainer mainContainer)
            {
                var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
                if (mainPanel != null)
                {
                    mainPanel.Controls.Clear();
                    var myFlashcardsControl = new MyFlashcardsControl();
                    myFlashcardsControl.Dock = DockStyle.Fill;
                    mainPanel.Controls.Add(myFlashcardsControl);
                }
            }
        }

        private Control FindControlRecursive(Control parent, string name)
        {
            foreach (Control c in parent.Controls)
            {
                if (string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase)) return c;
                var found = FindControlRecursive(c, name);
                if (found != null) return found;
            }
            return null;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            ApplyFiltersAndLoadPage();
        }

        private void CbbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSearch.SelectedItem != null)
            {
                _searchFilter = cbbSearch.SelectedItem.ToString();
                _currentPage = 1;
                ApplyFiltersAndLoadPage();
            }
        }

        private void CmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPageSize.SelectedItem != null)
            {
                _pageSize = int.Parse(cmbPageSize.SelectedItem.ToString());
                _currentPage = 1;
                ApplyFiltersAndLoadPage();
            }
        }

        private void BtnFirstPage_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            ApplyFiltersAndLoadPage();
        }

        private void BtnPrevPage_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                ApplyFiltersAndLoadPage();
            }
        }

        private void BtnNextPage_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            if (_currentPage < totalPages)
            {
                _currentPage++;
                ApplyFiltersAndLoadPage();
            }
        }

        private void BtnLastPage_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            _currentPage = totalPages;
            ApplyFiltersAndLoadPage();
        }

        private void flowCourses_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private string GetModerationStatusText(string status)
        {
            return status switch
            {
                "Pending" => LanguageHelper.GetString("StatusPending"),
                "Approved" => LanguageHelper.GetString("StatusApproved"),
                "Rejected" => LanguageHelper.GetString("StatusRejected"),
                "NeedsRevision" => LanguageHelper.GetString("StatusNeedsRevision"),
                _ => LanguageHelper.GetString("StatusNotSubmitted")
            };
        }
    }
}
