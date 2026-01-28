using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static WinFormsApp1.Helpers.ColorPalette;
using WinFormsApp1.View.User.Controls.CourseControls;
using WinFormsApp1.View.User;
using System.Collections.Generic;

namespace WinFormsApp1.View.User.Controls
{
    public partial class CourseControl : UserControl
    {
        private string? _categoryFilterSlug = null;
        private string? _searchQuery = null;
        private List<Course> _allFilteredCourses = new List<Course>();

        public CourseControl()
        {
            InitializeComponent();
            cmbSort.SelectedIndex = 0;
            
            // Initialize pagination control
            paginationControl1.Initialize(12);
            paginationControl1.PageChanged += PaginationControl_PageChanged;
            
            ApplyLocalization();
            InitializeFilters();
            LoadCourses();
        }

        private void ApplyLocalization()
        {
            // Filter panel
            lblFilterHeader.Text = LanguageHelper.GetString("Filter");
            label1.Text = LanguageHelper.GetString("Category");
            lblRatingHeader.Text = LanguageHelper.GetString("Rating");
            lblPriceHeader.Text = LanguageHelper.GetString("Price");
            btnApply.Text = LanguageHelper.GetString("Apply");
            btnClear.Text = LanguageHelper.GetString("Reset");
            txtFilterFromPrice.PlaceholderText = LanguageHelper.GetString("FromPrice");
            txtFilterToPrice.PlaceholderText = LanguageHelper.GetString("ToPrice");
            
            // Header
            lblHeader.Text = LanguageHelper.GetString("AllCourses");
            lblSortLabel.Text = LanguageHelper.GetString("SortBy");
        }

        private void PaginationControl_PageChanged(object sender, int newPage)
        {
            DisplayCurrentPage();
        }

        private async void InitializeFilters()
        {
            // Load categories into combo box
            await LoadCategoriesAsync();
            
            // Setup rating filter
            cbbFilterRate.Items.Clear();
            cbbFilterRate.Items.Add(LanguageHelper.GetString("All"));
            cbbFilterRate.Items.Add(LanguageHelper.GetString("FiveStars"));
            cbbFilterRate.Items.Add(LanguageHelper.GetString("FourStarsAndUp"));
            cbbFilterRate.Items.Add(LanguageHelper.GetString("ThreeStarsAndUp"));
            cbbFilterRate.Items.Add(LanguageHelper.GetString("TwoStarsAndUp"));
            cbbFilterRate.Items.Add(LanguageHelper.GetString("OneStarAndUp"));
            cbbFilterRate.SelectedIndex = 0;
            
            // Only allow numbers in price textboxes
            txtFilterFromPrice.KeyPress += PriceTextBox_KeyPress;
            txtFilterToPrice.KeyPress += PriceTextBox_KeyPress;
            
            // Validate price range
            txtFilterFromPrice.Leave += ValidatePriceRange;
            txtFilterToPrice.Leave += ValidatePriceRange;
        }

        private void PriceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow digits, backspace, and decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            // Only allow one decimal point
            var textBox = sender as Guna.UI2.WinForms.Guna2TextBox;
            if (e.KeyChar == '.' && textBox.Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void ValidatePriceRange(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterFromPrice.Text) || 
                string.IsNullOrWhiteSpace(txtFilterToPrice.Text))
                return;

            if (decimal.TryParse(txtFilterFromPrice.Text, out decimal fromPrice) &&
                decimal.TryParse(txtFilterToPrice.Text, out decimal toPrice))
            {
                if (fromPrice > toPrice)
                {
                    MessageBox.Show(LanguageHelper.GetString("FromPriceGreaterThanToPrice"), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    // Swap values
                    var temp = txtFilterFromPrice.Text;
                    txtFilterFromPrice.Text = txtFilterToPrice.Text;
                    txtFilterToPrice.Text = temp;
                }
            }
        }

        private async System.Threading.Tasks.Task LoadCategoriesAsync()
        {
            try
            {
                using var context = new LearningPlatformContext();
                var categories = await context.CourseCategories
                    .OrderBy(c => c.DisplayOrder)
                    .ThenBy(c => c.Name)
                    .ToListAsync();

                cbbFilterCategory.Items.Clear();
                cbbFilterCategory.Items.Add(LanguageHelper.GetString("All"));
                
                foreach (var category in categories)
                {
                    cbbFilterCategory.Items.Add(category.Name);
                    cbbFilterCategory.Tag = categories; // Store categories for later use
                }
                
                cbbFilterCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageHelper.GetString("CategoryLoadError", ex.Message), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async System.Threading.Tasks.Task FilterByCategory(string categorySlug)
        {
            _categoryFilterSlug = categorySlug;
            await ApplyFiltersAndLoad();
        }

        public async System.Threading.Tasks.Task SearchCourses(string searchQuery)
        {
            _searchQuery = searchQuery;
            await ApplyFiltersAndLoad();
        }

        private async void LoadCourses()
        {
            await ApplyFiltersAndLoad();
        }

        private async void SortChanged(object sender, EventArgs e)
        {
            await ApplyFiltersAndLoad();
        }

        private async void BtnApply_Click(object sender, EventArgs e)
        {
            // Validate price range before applying
            if (!string.IsNullOrWhiteSpace(txtFilterFromPrice.Text) && 
                !string.IsNullOrWhiteSpace(txtFilterToPrice.Text))
            {
                if (decimal.TryParse(txtFilterFromPrice.Text, out decimal fromPrice) &&
                    decimal.TryParse(txtFilterToPrice.Text, out decimal toPrice))
                {
                    if (fromPrice > toPrice)
                    {
                        MessageBox.Show(LanguageHelper.GetString("FromPriceGreaterThanToPrice"), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    if (fromPrice < 0 || toPrice < 0)
                    {
                        MessageBox.Show(LanguageHelper.GetString("PriceCannotBeNegative"), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            await ApplyFiltersAndLoad();
        }

        private async void BtnClear_Click(object sender, EventArgs e)
        {
            // Reset all filters to default
            cbbFilterCategory.SelectedIndex = 0;
            cbbFilterRate.SelectedIndex = 0;
            txtFilterFromPrice.Clear();
            txtFilterToPrice.Clear();
            
            // Clear external filters
            _categoryFilterSlug = null;
            _searchQuery = null;
            
            // Reload courses
            await ApplyFiltersAndLoad();
        }

        private async System.Threading.Tasks.Task ApplyFiltersAndLoad()
        {
            using var context = new LearningPlatformContext();
            var query = context.Courses
                .Include(c => c.Owner)
                .Include(c => c.Category)
                .Where(c => c.IsPublished && c.ModerationStatus == "Approved")
                .AsQueryable();

            // Apply search query
            if (!string.IsNullOrEmpty(_searchQuery))
            {
                query = query.Where(c => c.Title.Contains(_searchQuery) || 
                                        (c.Summary != null && c.Summary.Contains(_searchQuery)));
            }

            // Apply external category filter if provided
            if (!string.IsNullOrEmpty(_categoryFilterSlug))
            {
                query = query.Where(c => c.Category != null && c.Category.Slug == _categoryFilterSlug);
            }

            // Apply category filter from combo box
            if (cbbFilterCategory.SelectedIndex > 0 && cbbFilterCategory.Tag is System.Collections.Generic.List<CourseCategory> categories)
            {
                var selectedCategoryName = cbbFilterCategory.SelectedItem.ToString();
                var selectedCategory = categories.FirstOrDefault(c => c.Name == selectedCategoryName);
                if (selectedCategory != null)
                {
                    query = query.Where(c => c.CategoryId == selectedCategory.CategoryId);
                }
            }

            // Apply rating filter
            if (cbbFilterRate.SelectedIndex > 0)
            {
                decimal minRating = cbbFilterRate.SelectedIndex switch
                {
                    1 => 5.0m,    // 5 sao
                    2 => 4.0m,    // 4 sao trở lên
                    3 => 3.0m,    // 3 sao trở lên
                    4 => 2.0m,    // 2 sao trở lên
                    5 => 1.0m,    // 1 sao trở lên
                    _ => 0.0m
                };

                if (cbbFilterRate.SelectedIndex == 1)
                {
                    // Exactly 5 stars
                    query = query.Where(c => c.AverageRating == 5.0m);
                }
                else
                {
                    // Greater than or equal to
                    query = query.Where(c => c.AverageRating >= minRating);
                }
            }

            // Apply price range filter
            if (!string.IsNullOrWhiteSpace(txtFilterFromPrice.Text) && decimal.TryParse(txtFilterFromPrice.Text, out decimal fromPrice))
            {
                if (fromPrice >= 0)
                {
                    query = query.Where(c => c.Price >= fromPrice);
                }
            }

            if (!string.IsNullOrWhiteSpace(txtFilterToPrice.Text) && decimal.TryParse(txtFilterToPrice.Text, out decimal toPrice))
            {
                if (toPrice >= 0)
                {
                    query = query.Where(c => c.Price <= toPrice);
                }
            }

            // Apply sorting
            query = cmbSort.SelectedIndex switch
            {
                0 => query.OrderByDescending(c => c.TotalReviews),
                1 => query.OrderByDescending(c => c.AverageRating),
                2 => query.OrderByDescending(c => c.CreatedAt),
                3 => query.OrderBy(c => c.Price),
                4 => query.OrderByDescending(c => c.Price),
                _ => query.OrderByDescending(c => c.TotalReviews)
            };

            try
            {
                // Load all filtered courses
                _allFilteredCourses = await query.ToListAsync();

                // Update course count with total
                lblCourseCount.Text = LanguageHelper.GetString("CourseCount", _allFilteredCourses.Count);

                // Update pagination
                paginationControl1.UpdatePagination(_allFilteredCourses.Count);

                // Display first page
                DisplayCurrentPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageHelper.GetString("Error") + ": " + ex.Message, LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayCurrentPage()
        {
            coursesPanel.Controls.Clear();

            if (_allFilteredCourses.Count == 0)
            {
                var noResultLabel = new Label
                {
                    Text = LanguageHelper.GetString("NoCoursesFound"),
                    Font = new Font("Segoe UI", 14F),
                    ForeColor = Color.FromArgb(108, 117, 125),
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Size = new Size(800, 200),
                    Margin = new Padding(50)
                };
                coursesPanel.Controls.Add(noResultLabel);
                return;
            }

            // Get courses for current page using pagination control
            var pagedCourses = paginationControl1.GetPageData(_allFilteredCourses.ToArray());

            foreach (var course in pagedCourses)
            {
                // Use the reusable CourseCardControl so design matches everywhere
                var control = new CourseCardControl();
                control.Bind(course);
                control.Margin = new Padding(10);
                // Ensure fixed size so FlowLayoutPanel doesn't stretch it
                control.Size = new Size(330, 420);
                coursesPanel.Controls.Add(control);
            }
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int courseId = (int)btn.Tag;
            
            // Tìm MainContainer form
            var mainContainer = this.FindForm() as MainContainer;
            if (mainContainer != null)
            {
                // Gọi method NavigateToCourseDetail
                mainContainer.NavigateToCourseDetail(courseId);
            }
            else
            {
                MessageBox.Show(LanguageHelper.GetString("NavigationError"), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnAddCart_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int courseId = (int)btn.Tag;
            var userId = AuthHelper.CurrentUser?.UserId;

            if (!userId.HasValue)
            {
                MessageBox.Show("Vui lòng đăng nhập để thêm vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using var context = new LearningPlatformContext();
                
                var cart = await context.ShoppingCarts
                    .FirstOrDefaultAsync(c => c.UserId == userId.Value);

                if (cart == null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId.Value,
                        CreatedAt = DateTime.Now
                    };
                    context.ShoppingCarts.Add(cart);
                    await context.SaveChangesAsync();
                }

                var existingItem = await context.CartItems
                    .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.CourseId == courseId);

                if (existingItem == null)
                {
                    var cartItem = new CartItem
                    {
                        CartId = cart.CartId,
                        CourseId = courseId,
                        AddedAt = DateTime.Now
                    };
                    context.CartItems.Add(cartItem);
                    await context.SaveChangesAsync();
                    
                    MessageBox.Show("Đã thêm khóa học vào giỏ hàng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Khóa học đã có trong giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
