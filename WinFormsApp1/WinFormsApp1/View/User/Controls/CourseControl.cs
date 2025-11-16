using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static WinFormsApp1.Helpers.ColorPalette;

namespace WinFormsApp1.View.User.Controls
{
    public partial class CourseControl : UserControl
    {
        public CourseControl()
        {
            InitializeComponent();
            cmbSort.SelectedIndex = 0;
            LoadCourses();
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

            var courses = await query.Take(20).ToListAsync();

            // Update course count
            lblCourseCount.Text = $"{courses.Count} khóa học";

            // Display courses
            DisplayCourses(courses);
        }

        private void DisplayCourses(System.Collections.Generic.List<Course> courses)
        {
            coursesPanel.Controls.Clear();

            foreach (var course in courses)
            {
                var card = CreateCourseCard(course);
                coursesPanel.Controls.Add(card);
            }
        }

        private Panel CreateCourseCard(Course course)
        {
            var card = new Panel
            {
                Size = new Size(330, 380),
                BackColor = CardBackground,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Cursor = Cursors.Hand
            };

            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(250, 250, 250);
            card.MouseLeave += (s, e) => card.BackColor = CardBackground;

            // Course image
            var picBox = new PictureBox
            {
                Size = new Size(328, 180),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(240, 240, 240),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = null
            };

            // Add placeholder icon if no image
            var lblImagePlaceholder = new Label
            {
                Text = "📚",
                Font = new Font("Segoe UI", 48),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(200, 200, 200)
            };
            picBox.Controls.Add(lblImagePlaceholder);
            card.Controls.Add(picBox);

            // Course title
            var lblTitle = new Label
            {
                Text = course.Title,
                Location = new Point(10, 190),
                Size = new Size(310, 45),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = TextPrimary
            };
            card.Controls.Add(lblTitle);

            // Author name
            var lblAuthor = new Label
            {
                Text = course.Owner?.FullName ?? "Trần Minh Khoa",
                Location = new Point(10, 240),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = TextSecondary
            };
            card.Controls.Add(lblAuthor);

            // Rating
            var lblRating = new Label
            {
                Text = course.TotalReviews > 0 
                    ? $"{course.AverageRating:0.0} ⭐ ({course.TotalReviews})" 
                    : "Chưa có đánh giá",
                Location = new Point(10, 265),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Warning
            };
            card.Controls.Add(lblRating);

            // Price
            var lblPrice = new Label
            {
                Text = course.Price > 0 ? $"{course.Price:N0} đ" : "Miễn phí",
                Location = new Point(10, 290),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = ButtonPrimary
            };
            card.Controls.Add(lblPrice);

            // Buttons panel
            var btnPanel = new Panel
            {
                Location = new Point(10, 330),
                Size = new Size(310, 40),
                BackColor = Color.Transparent
            };

            // View details button
            var btnView = new Button
            {
                Text = "Xem chi tiết",
                Location = new Point(0, 0),
                Size = new Size(230, 35),
                BackColor = Color.White,
                ForeColor = ButtonPrimary,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Tag = course.CourseId
            };
            btnView.FlatAppearance.BorderColor = Border;
            btnView.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);
            btnView.Click += BtnView_Click;
            btnPanel.Controls.Add(btnView);

            // Add to cart button
            var btnCart = new Button
            {
                Text = "🛒",
                Location = new Point(240, 0),
                Size = new Size(60, 35),
                BackColor = ButtonPrimary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 14),
                Cursor = Cursors.Hand,
                Tag = course.CourseId
            };
            btnCart.FlatAppearance.BorderSize = 0;
            btnCart.FlatAppearance.MouseOverBackColor = ButtonHover;
            btnCart.Click += BtnAddCart_Click;
            btnPanel.Controls.Add(btnCart);

            card.Controls.Add(btnPanel);

            return card;
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int courseId = (int)btn.Tag;
            
            MessageBox.Show($"Xem chi tiết khóa học ID: {courseId}", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void BtnAddCart_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int courseId = (int)btn.Tag;
            var userId = AuthHelper.CurrentUser?.UserId;

            if (!userId.HasValue)
            {
                MessageBox.Show("Vui lòng đăng nhập để thêm vào giỏ hàng!", "Yêu cầu đăng nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    
                    MessageBox.Show("Đã thêm khóa học vào giỏ hàng!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Khóa học đã có trong giỏ hàng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
