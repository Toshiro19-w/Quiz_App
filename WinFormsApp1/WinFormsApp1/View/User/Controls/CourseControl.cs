using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static WinFormsApp1.Helpers.ColorPalette;
using WinFormsApp1.View.User.Controls.CourseControls;
using WinFormsApp1.View.User;

namespace WinFormsApp1.View.User.Controls
{
    public partial class CourseControl : UserControl
    {
        private string? _categoryFilterSlug = null;
        private string? _searchQuery = null;

        public CourseControl()
        {
            InitializeComponent();
            cmbSort.SelectedIndex = 0;
            LoadCourses();
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

        private async void FilterChanged(object sender, EventArgs e)
        {
            await ApplyFiltersAndLoad();
        }

        private async void SortChanged(object sender, EventArgs e)
        {
            await ApplyFiltersAndLoad();
        }

        private async System.Threading.Tasks.Task ApplyFiltersAndLoad()
        {
            using var context = new LearningPlatformContext();
            var query = context.Courses
                .Include(c => c.Owner)
                .Include(c => c.Category)
                .Where(c => c.IsPublished)
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

            // Apply rating filters
            bool hasRatingFilter = false;
            var ratingQuery = context.Courses.Where(c => false).AsQueryable();

            if (chkRating4Plus.Checked)
            {
                if (!hasRatingFilter)
                {
                    ratingQuery = query.Where(c => c.AverageRating >= 4.0m && c.AverageRating <= 5.0m);
                    hasRatingFilter = true;
                }
                else
                {
                    ratingQuery = ratingQuery.Union(query.Where(c => c.AverageRating >= 4.0m && c.AverageRating <= 5.0m));
                }
            }

            if (chkRating3To4.Checked)
            {
                if (!hasRatingFilter)
                {
                    ratingQuery = query.Where(c => c.AverageRating >= 3.0m && c.AverageRating < 4.0m);
                    hasRatingFilter = true;
                }
                else
                {
                    ratingQuery = ratingQuery.Union(query.Where(c => c.AverageRating >= 3.0m && c.AverageRating < 4.0m));
                }
            }

            if (chkRating2To3.Checked)
            {
                if (!hasRatingFilter)
                {
                    ratingQuery = query.Where(c => c.AverageRating >= 2.0m && c.AverageRating < 3.0m);
                    hasRatingFilter = true;
                }
                else
                {
                    ratingQuery = ratingQuery.Union(query.Where(c => c.AverageRating >= 2.0m && c.AverageRating < 3.0m));
                }
            }

            if (chkRating1To2.Checked)
            {
                if (!hasRatingFilter)
                {
                    ratingQuery = query.Where(c => c.AverageRating >= 1.0m && c.AverageRating < 2.0m);
                    hasRatingFilter = true;
                }
                else
                {
                    ratingQuery = ratingQuery.Union(query.Where(c => c.AverageRating >= 1.0m && c.AverageRating < 2.0m));
                }
            }

            if (hasRatingFilter)
            {
                query = ratingQuery;
            }

            // Apply price filter
            bool freeChecked = chkFree.Checked;
            bool paidChecked = chkPaid.Checked;

            if (freeChecked && !paidChecked)
            {
                query = query.Where(c => c.Price == 0);
            }
            else if (paidChecked && !freeChecked)
            {
                query = query.Where(c => c.Price > 0);
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
                var courses = await query.Take(20).ToListAsync();

                // Update course count
                lblCourseCount.Text = $"{courses.Count} khóa học";

                // Display courses
                DisplayCourses(courses);
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), $"Lỗi: {ex.Message}");
            }
        }

        private void DisplayCourses(System.Collections.Generic.List<Course> courses)
        {
            coursesPanel.Controls.Clear();

            foreach (var course in courses)
            {
                // Use the reusable CourseCardControl so design matches everywhere
                var control = new CourseCardControl();
                control.Bind(course);
                control.Margin = new Padding(10);
                // Ensure fixed size so FlowLayoutPanel doesn't stretch it
                control.Size = new Size(330, 380);
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
                ToastHelper.Show(this.FindForm(), "Không thể điều hướng đến trang chi tiết");
            }
        }

        private async void BtnAddCart_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int courseId = (int)btn.Tag;
            var userId = AuthHelper.CurrentUser?.UserId;

            if (!userId.HasValue)
            {
                ToastHelper.Show(this.FindForm(), "Vui lòng đăng nhập để thêm vào giỏ hàng!");
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
                    
                    ToastHelper.Show(this.FindForm(), "Đã thêm khóa học vào giỏ hàng!");
                }
                else
                {
                    ToastHelper.Show(this.FindForm(), "Khóa học đã có trong giỏ hàng!");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), $"Lỗi: {ex.Message}");
            }
        }
    }
}
