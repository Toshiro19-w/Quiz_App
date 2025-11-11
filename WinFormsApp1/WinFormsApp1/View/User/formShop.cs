using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;
using static WinFormsApp1.Helpers.ColorPalette;

namespace WinFormsApp1.View.User
{
    public partial class formShop : Form
    {
        public formShop()
        {
            InitializeComponent();
        }

        private async void formShop_Load(object sender, EventArgs e)
        {
            FormLayoutHelper.SetupShopCompleteLayout(
                this, panel1, searchTB, searchButton, label1, tagPanel, coursesPanel
            );
            
            await LoadCategories();
            await LoadCourses();
        }

        private async System.Threading.Tasks.Task LoadCategories()
        {
            using var context = new LearningPlatformContext();
            var categories = await context.CourseCategories.Take(9).ToListAsync();

            tagPanel.Controls.Clear();
            foreach (var category in categories)
            {
                var btn = new Button
                {
                    Text = category.Name,
                    Size = new Size(120, 35),
                    BackColor = ButtonPrimary,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Tag = category.CategoryId
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = ButtonHover;
                btn.Click += CategoryButton_Click;
                tagPanel.Controls.Add(btn);
            }
        }

        private async void CategoryButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int categoryId = (int)btn.Tag;
            await LoadCoursesByCategory(categoryId);
        }

        private async System.Threading.Tasks.Task LoadCourses()
        {
            using var context = new LearningPlatformContext();
            var courses = await context.Courses
                .OrderByDescending(c => c.TotalReviews)
                .Take(8)
                .ToListAsync();

            DisplayCourses(courses);
        }

        private async System.Threading.Tasks.Task LoadCoursesByCategory(int categoryId)
        {
            using var context = new LearningPlatformContext();
            var courses = await context.Courses
                .Where(c => c.CategoryId == categoryId)
                .Take(8)
                .ToListAsync();

            DisplayCourses(courses);
        }

        private void DisplayCourses(System.Collections.Generic.List<Models.Entities.Course> courses)
        {
            coursesPanel.Controls.Clear();

            foreach (var course in courses)
            {
                var courseCard = CreateCourseCard(course);
                coursesPanel.Controls.Add(courseCard);
            }
        }

        private Panel CreateCourseCard(Models.Entities.Course course)
        {
            var card = new Panel
            {
                Size = new Size(273, 250),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5)
            };

            var picBox = new PictureBox
            {
                Size = new Size(267, 120),
                Location = new Point(3, 3),
                BackColor = Color.LightGray,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            var lblTitle = new Label
            {
                Text = course.Title,
                Location = new Point(5, 130),
                Size = new Size(260, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Black
            };

            var lblPrice = new Label
            {
                Text = course.Price > 0 ? $"{course.Price:N0} VNĐ" : "Miễn phí",
                Location = new Point(5, 175),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ButtonPrimary
            };

            var btnAddCart = new Button
            {
                Text = "Thêm vào giỏ",
                Location = new Point(5, 210),
                Size = new Size(130, 30),
                BackColor = ButtonSecondary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = course.CourseId
            };
            btnAddCart.FlatAppearance.BorderSize = 0;
            btnAddCart.FlatAppearance.MouseOverBackColor = SecondaryDark;
            btnAddCart.Click += BtnAddCart_Click;

            var btnView = new Button
            {
                Text = "Xem",
                Location = new Point(140, 210),
                Size = new Size(130, 30),
                BackColor = Color.White,
                ForeColor = ButtonPrimary,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = course.CourseId
            };
            btnView.FlatAppearance.BorderColor = ButtonPrimary;
            btnView.Click += BtnView_Click;

            card.Controls.AddRange(new Control[] { picBox, lblTitle, lblPrice, btnAddCart, btnView });
            return card;
        }

        private async void BtnAddCart_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int courseId = (int)btn.Tag;
            var userId = AuthHelper.CurrentUser?.UserId;

            if (!userId.HasValue)
            {
                MessageBox.Show("Vui lòng đăng nhập!");
                return;
            }

            using var context = new LearningPlatformContext();
            var cart = await context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.UserId == userId.Value);

            if (cart == null)
            {
                cart = new Models.Entities.ShoppingCart
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
                var cartItem = new Models.Entities.CartItem
                {
                    CartId = cart.CartId,
                    CourseId = courseId,
                    AddedAt = DateTime.Now
                };
                context.CartItems.Add(cartItem);
                await context.SaveChangesAsync();
                MessageBox.Show("Đã thêm vào giỏ hàng!");
            }
            else
            {
                MessageBox.Show("Khóa học đã có trong giỏ hàng!");
            }
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int courseId = (int)btn.Tag;
            var lessonForm = new formLesson();
            lessonForm.Show();
        }
    }
}
