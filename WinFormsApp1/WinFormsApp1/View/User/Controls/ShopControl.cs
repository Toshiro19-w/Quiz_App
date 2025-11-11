using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;
using static WinFormsApp1.Helpers.ColorPalette;

namespace WinFormsApp1.View.User.Controls
{
    public partial class ShopControl : UserControl
    {
        private FlowLayoutPanel coursesPanel;

        public ShopControl()
        {
            InitializeComponent();
            SetupUI();
            LoadCourses();
        }

        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ColorPalette.Background;
            this.Padding = new Padding(0, 70, 0, 0);

            var lblTitle = new Label { Text = "🛒 Cửa hàng khóa học", Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(20, 10), AutoSize = true };
            
            var searchTB = new TextBox { Location = new Point(20, 50), Size = new Size(400, 30), Font = new Font("Segoe UI", 11), PlaceholderText = "Tìm kiếm khóa học..." };
            var btnSearch = new Button { Text = "🔍 Tìm", Location = new Point(430, 50), Size = new Size(100, 30), BackColor = ColorPalette.ButtonSecondary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += async (s, e) => await SearchCourses(searchTB.Text.Trim());

            var lblFilter = new Label { Text = "Lọc theo:", Location = new Point(550, 55), AutoSize = true, Font = new Font("Segoe UI", 10) };
            var cmbFilter = new ComboBox { Location = new Point(620, 50), Size = new Size(150, 30), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbFilter.Items.AddRange(new[] { "Tất cả", "Miễn phí", "Trả phí", "Phổ biến nhất" });
            cmbFilter.SelectedIndex = 0;
            cmbFilter.SelectedIndexChanged += async (s, e) => await FilterCourses(cmbFilter.SelectedIndex);

            coursesPanel = new FlowLayoutPanel { Location = new Point(20, 100), Size = new Size(1150, 500), AutoScroll = true };

            this.Controls.AddRange(new Control[] { lblTitle, searchTB, btnSearch, lblFilter, cmbFilter, coursesPanel });
        }

        private async System.Threading.Tasks.Task SearchCourses(string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) { LoadCourses(); return; }
            using var context = new LearningPlatformContext();
            var courses = await context.Courses.Where(c => c.Title.Contains(searchText) || (c.Summary != null && c.Summary.Contains(searchText))).Take(12).ToListAsync();
            DisplayCourses(courses);
        }

        private async System.Threading.Tasks.Task FilterCourses(int filterIndex)
        {
            using var context = new LearningPlatformContext();
            var query = context.Courses.AsQueryable();
            query = filterIndex switch
            {
                1 => query.Where(c => c.Price == 0),
                2 => query.Where(c => c.Price > 0),
                3 => query.OrderByDescending(c => c.TotalReviews),
                _ => query.OrderByDescending(c => c.TotalReviews)
            };
            var courses = await query.Take(12).ToListAsync();
            DisplayCourses(courses);
        }

        private void DisplayCourses(System.Collections.Generic.List<Models.Entities.Course> courses)
        {
            coursesPanel.Controls.Clear();
            foreach (var course in courses)
            {
                var card = CreateCourseCard(course);
                coursesPanel.Controls.Add(card);
            }
        }

        private Panel CreateCourseCard(Models.Entities.Course course)
        {
            var card = new Panel { Size = new Size(270, 220), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(5) };
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(245, 245, 245);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            var lblTitle = new Label { Text = course.Title, Location = new Point(10, 10), Size = new Size(250, 60), Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            var lblReviews = new Label { Text = $"⭐ {course.TotalReviews} đánh giá", Location = new Point(10, 75), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = ColorPalette.TextSecondary };
            var lblPrice = new Label { Text = course.Price > 0 ? $"{course.Price:N0} VNĐ" : "Miễn phí", Location = new Point(10, 100), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = ColorPalette.ButtonPrimary };
            
            var btnAddCart = new Button { Text = "🛒 Thêm giỏ", Location = new Point(10, 140), Size = new Size(120, 30), BackColor = ColorPalette.ButtonPrimary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Tag = course.CourseId };
            btnAddCart.FlatAppearance.BorderSize = 0;
            btnAddCart.Click += BtnAddCart_Click;

            var btnView = new Button { Text = "Xem", Location = new Point(140, 140), Size = new Size(120, 30), BackColor = ColorPalette.ButtonSecondary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += (s, e) => NavigationHelper.NavigateTo("lesson");

            card.Controls.AddRange(new Control[] { lblTitle, lblReviews, lblPrice, btnAddCart, btnView });
            return card;
        }

        private async void BtnAddCart_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int courseId = (int)btn.Tag;
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) { MessageBox.Show("Vui lòng đăng nhập!"); return; }

            using var context = new LearningPlatformContext();
            var cart = await context.ShoppingCarts.FirstOrDefaultAsync(c => c.UserId == userId.Value);
            if (cart == null)
            {
                cart = new Models.Entities.ShoppingCart { UserId = userId.Value, CreatedAt = DateTime.Now };
                context.ShoppingCarts.Add(cart);
                await context.SaveChangesAsync();
            }

            var existingItem = await context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.CourseId == courseId);
            if (existingItem == null)
            {
                var cartItem = new Models.Entities.CartItem { CartId = cart.CartId, CourseId = courseId, AddedAt = DateTime.Now };
                context.CartItems.Add(cartItem);
                await context.SaveChangesAsync();
                MessageBox.Show("Đã thêm vào giỏ hàng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Khóa học đã có trong giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void LoadCourses()
        {
            using var context = new LearningPlatformContext();
            var courses = await context.Courses.OrderByDescending(c => c.TotalReviews).Take(12).ToListAsync();

            foreach (var course in courses)
            {
                var card = new Panel { Size = new Size(270, 200), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(5) };
                var lblTitle = new Label { Text = course.Title, Location = new Point(10, 10), Size = new Size(250, 60), Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                var lblPrice = new Label { Text = course.Price > 0 ? $"{course.Price:N0} VNĐ" : "Miễn phí", Location = new Point(10, 80), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = ButtonPrimary };
                var btnView = new Button { Text = "Xem", Location = new Point(10, 160), Size = new Size(250, 30), BackColor = ButtonSecondary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
                btnView.FlatAppearance.BorderSize = 0;
                btnView.Click += (s, e) => MessageBox.Show($"Xem khóa học: {course.Title}");
                card.Controls.AddRange(new Control[] { lblTitle, lblPrice, btnView });
                coursesPanel.Controls.Add(card);
            }
        }
    }
}
