using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Controls
{
    public partial class MyFlashcardsControl : UserControl
    {
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalRecords = 0;
        private List<FlashcardSet> _allFlashcardSets = new List<FlashcardSet>();
        private string _searchFilter = "";

        public MyFlashcardsControl()
        {
            InitializeComponent();
            LocalizeUI();
            cmbPageSize.SelectedIndex = 0;
            cbbSearch.SelectedIndex = 0;
            LoadFlashcardSets();
            
            flowFlashcards.Resize += (s, e) => RefreshRowWidths();
        }

        private void LocalizeUI()
        {
            // Header
            lblTitle.Text = LanguageHelper.GetString("MyFlashcardSets");
            
            // Action buttons
            btnCreateFlashcard.Text = LanguageHelper.GetString("CreateFlashcardSet");
            btnMyCourse.Text = LanguageHelper.GetString("MyCourses");
            btnBack.Text = LanguageHelper.GetString("GoBack");
            
            // Filter labels
            lblShowLabel.Text = LanguageHelper.GetString("Show");
            lblEntriesLabel.Text = LanguageHelper.GetString("Entries");
            lblSearchLabel.Text = LanguageHelper.GetString("Search") + ":";
            txtSearch.PlaceholderText = LanguageHelper.GetString("EnterSearch");
            
            // Search filter combobox
            cbbSearch.Items.Clear();
            cbbSearch.Items.AddRange(new object[] {
                LanguageHelper.GetString("FilterAll"),
                LanguageHelper.GetString("FilterTitle"),
                LanguageHelper.GetString("FilterCardCount"),
                LanguageHelper.GetString("FilterVisibility"),
                LanguageHelper.GetString("FilterLanguage"),
                LanguageHelper.GetString("FilterCreatedAt")
            });
            
            // Table headers
            lblHeaderTitle.Text = LanguageHelper.GetString("Title");
            lblHeaderCardCount.Text = LanguageHelper.GetString("HeaderCardCount");
            lblHeaderVisibility.Text = LanguageHelper.GetString("HeaderVisibility");
            lblHeaderLanguage.Text = LanguageHelper.GetString("HeaderLanguage");
            lblHeaderDate.Text = LanguageHelper.GetString("CreatedAt");
            lblHeaderActions.Text = LanguageHelper.GetString("Actions");
            
            // Pagination buttons
            btnFirstPage.Text = LanguageHelper.GetString("First");
            btnPrevPage.Text = LanguageHelper.GetString("Previous");
            btnNextPage.Text = LanguageHelper.GetString("Next");
            btnLastPage.Text = LanguageHelper.GetString("Last");
        }

        private async void LoadFlashcardSets()
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
                _allFlashcardSets = await context.FlashcardSets
                    .Include(fs => fs.Flashcards)
                    .Where(fs => fs.OwnerId == userId.Value &&
                                !fs.IsDeleted &&
                                (fs.Visibility == "Public" || fs.Visibility == "Private"))
                    .OrderByDescending(fs => fs.CreatedAt)
                    .ToListAsync();

                _totalRecords = _allFlashcardSets.Count;
                ApplyFiltersAndLoadPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageHelper.GetString("DataLoadError", ex.Message), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFiltersAndLoadPage()
        {
            var filteredSets = _allFlashcardSets.AsEnumerable();

            // Apply search filter based on selected criteria
            string searchText = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                var filterTitle = LanguageHelper.GetString("FilterTitle");
                var filterCardCount = LanguageHelper.GetString("FilterCardCount");
                var filterVisibility = LanguageHelper.GetString("FilterVisibility");
                var filterLanguage = LanguageHelper.GetString("FilterLanguage");
                var filterCreatedAt = LanguageHelper.GetString("FilterCreatedAt");
                var visibilityPublic = LanguageHelper.GetString("VisibilityPublic");
                var visibilityPrivate = LanguageHelper.GetString("VisibilityPrivate");

                filteredSets = _searchFilter switch
                {
                    var f when f == filterTitle => filteredSets.Where(fs =>
                        fs.Title.ToLower().Contains(searchText)),
                    
                    var f when f == filterCardCount => filteredSets.Where(fs =>
                        fs.Flashcards.Count.ToString().Contains(searchText)),
                    
                    var f when f == filterVisibility => filteredSets.Where(fs =>
                    {
                        var visibility = fs.Visibility == "Public" ? visibilityPublic : visibilityPrivate;
                        return visibility.Contains(searchText);
                    }),
                    
                    var f when f == filterLanguage => filteredSets.Where(fs =>
                    {
                        var language = string.IsNullOrEmpty(fs.Language) ? "vi" : fs.Language;
                        return language.ToLower().Contains(searchText);
                    }),
                    
                    var f when f == filterCreatedAt => filteredSets.Where(fs =>
                                fs.CreatedAt.ToString("dd/MM/yyyy").Contains(searchText) ||
                                fs.CreatedAt.ToString("dd-MM-yyyy").Contains(searchText) ||
                                fs.CreatedAt.ToString("yyyy").Contains(searchText)),
                    
                            _ => filteredSets.Where(fs =>
                                fs.Title.ToLower().Contains(searchText) ||
                                (fs.Description != null && fs.Description.ToLower().Contains(searchText)) ||
                                fs.Flashcards.Count.ToString().Contains(searchText) ||
                                (fs.Visibility == "Public" ? visibilityPublic : visibilityPrivate).Contains(searchText) ||
                                (string.IsNullOrEmpty(fs.Language) ? "vi" : fs.Language).ToLower().Contains(searchText) ||
                                fs.CreatedAt.ToString("dd/MM/yyyy").Contains(searchText))
                        };
                    }

                    _totalRecords = filteredSets.Count();

            // Calculate pagination
            int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            if (_currentPage > totalPages && totalPages > 0)
                _currentPage = totalPages;

            var pagedSets = filteredSets
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            LoadDataToGrid(pagedSets);
            UpdatePaginationUI(totalPages);
        }

        private void LoadDataToGrid(List<FlashcardSet> flashcardSets)
        {
            flowFlashcards.Controls.Clear();

            if (flashcardSets.Count == 0)
            {
                var lblEmpty = new Label
                {
                    Text = LanguageHelper.GetString("NoFlashcardSetYet"),
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    ForeColor = ColorPalette.TextSecondary,
                    AutoSize = true,
                    Location = new Point(400, 200)
                };
                flowFlashcards.Controls.Add(lblEmpty);
                return;
            }

            int rowIndex = (_currentPage - 1) * _pageSize + 1;
            foreach (var flashcardSet in flashcardSets)
            {
                var row = CreateFlashcardRow(flashcardSet, rowIndex++);
                flowFlashcards.Controls.Add(row);
            }
        }

        private FlashcardRowControl CreateFlashcardRow(FlashcardSet flashcardSet, int index)
        {
            var row = new FlashcardRowControl
            {
                Width = flowFlashcards.ClientSize.Width - 2
            };

            row.SetData(flashcardSet, index);

            row.ViewClicked += (s, fs) => ViewFlashcardSet(fs);
            row.StudyClicked += (s, fs) => StudyFlashcardSet(fs);
            row.EditClicked += (s, fs) => EditFlashcardSet(fs);
            row.DeleteClicked += (s, fs) => DeleteFlashcardSet(fs);

            return row;
        }

        private void ViewFlashcardSet(FlashcardSet flashcardSet)
        {
            var form = this.FindForm();
            if (form == null) return;

            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

            if (mainPanel == null)
            {
                mainPanel = this.Parent as Panel;
            }

            if (mainPanel == null) return;

            mainPanel.Controls.Clear();

			var detail = new WinFormsApp1.View.User.Controls.FlashcardControls.FlashcardDetailControl(
				flashcardSet.SetId,
				WinFormsApp1.View.User.Controls.FlashcardControls.FlashcardDetailSource.MyFlashcards);
            detail.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(detail);
        }

        private void StudyFlashcardSet(FlashcardSet flashcardSet)
        {
            if (flashcardSet.Flashcards == null || flashcardSet.Flashcards.Count == 0)
            {
                MessageBox.Show(LanguageHelper.GetString("NoCardsToStudy"), LanguageHelper.GetString("Notification"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var studyForm = new WinFormsApp1.View.User.Controls.FlashcardControls.FlashcardStudyForm(flashcardSet.SetId);
            studyForm.ShowDialog();
        }

        private void EditFlashcardSet(FlashcardSet flashcardSet)
        {
            var form = this.FindForm();
            if (form == null) return;

            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

            if (mainPanel == null)
            {
                mainPanel = this.Parent as Panel;
            }

            if (mainPanel == null) return;

            mainPanel.Controls.Clear();

            var editControl = new WinFormsApp1.View.User.Controls.FlashcardControls.EditFlashcardControl(flashcardSet.SetId);
            editControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(editControl);
        }

        private async void DeleteFlashcardSet(FlashcardSet flashcardSet)
        {
            var result = MessageBox.Show(
                LanguageHelper.GetString("DeleteFlashcardConfirm", flashcardSet.Title),
                LanguageHelper.GetString("ConfirmDelete"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using var context = new LearningPlatformContext();
                    var setToDelete = await context.FlashcardSets.FindAsync(flashcardSet.SetId);
                    if (setToDelete != null)
                    {
                        // Soft delete
                        setToDelete.IsDeleted = true;
                        await context.SaveChangesAsync();

                        ToastHelper.Show(this.FindForm(), LanguageHelper.GetString("FlashcardDeletedSuccess"));
                        LoadFlashcardSets();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LanguageHelper.GetString("FlashcardDeleteError", ex.Message), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void BtnCreateFlashcard_Click(object sender, EventArgs e)
        {
            var form = this.FindForm();
            if (form == null) return;

            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

            if (mainPanel == null)
            {
                mainPanel = this.Parent as Panel;
            }

            if (mainPanel == null) return;

            mainPanel.Controls.Clear();

            var createControl = new WinFormsApp1.View.User.Controls.FlashcardControls.CreateFlashcardControl();
            createControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(createControl);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            // Navigate back to MyCoursesControl
            var form = this.FindForm();
            if (form is MainContainer mainContainer)
            {
                var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
                if (mainPanel != null)
                {
                    mainPanel.Controls.Clear();
                    var myCoursesControl = new MyCoursesControl();
                    myCoursesControl.Dock = DockStyle.Fill;
                    mainPanel.Controls.Add(myCoursesControl);
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

        private void flowFlashcards_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMyCourse_Click(object sender, EventArgs e)
        {
			var form = this.FindForm();
			if (form is MainContainer mainContainer)
			{
				var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
				if (mainPanel != null)
				{
					mainPanel.Controls.Clear();
					var myCoursesControl = new MyCoursesControl();
					myCoursesControl.Dock = DockStyle.Fill;
					mainPanel.Controls.Add(myCoursesControl);
				}
			}
		}

        private void RefreshRowWidths()
        {
            foreach (Control control in flowFlashcards.Controls)
            {
                if (control is FlashcardRowControl row)
                {
                    row.Width = flowFlashcards.ClientSize.Width - 2;
                }
            }
        }
    }
}
