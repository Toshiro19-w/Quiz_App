using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.View.User.Controls.CourseControls;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Helpers;
using static WinFormsApp1.Helpers.ColorPalette;

namespace WinFormsApp1.View.User.Controls
{
    public partial class CourseCardControl : UserControl
    {
        public int CourseId { get; private set; }

        public CourseCardControl()
        {
            InitializeComponent();
            btnView.Click += BtnView_Click;
            btnAddToCart.Click += BtnAddToCart_Click;

            // Card click should navigate, but avoid overriding button clicks
            pnlCard.Click += (s, e) => BtnView_Click(s, e);
            // Attach click to non-button child controls so clicking image/text navigates
            foreach (Control c in pnlCard.Controls)
            {
                if (!(c is Button)) c.Click += (s, e) => BtnView_Click(s, e);
            }
        }

        public void Bind(Course course)
        {
            if (course == null) return;

            CourseId = course.CourseId;

            // Only set data properties — do not modify layout defined in Designer
            lblTitle.Text = course.Title;
            lblInstructor.Text = course.Owner?.FullName ?? "Trần Minh Khoa";
            lblRating.Text = course.TotalReviews > 0 ? $"{course.AverageRating:0.0} ⭐ ({course.TotalReviews})" : "Chưa có đánh giá";
            lblPrice.Text = course.Price > 0 ? $"{course.Price:N0} đ" : "Miễn phí";
            btnView.Tag = course.CourseId;

            // By default show the AddToCart button; we'll hide it for owners or purchasers
            btnAddToCart.Visible = true;

            // If current user exists, hide the button if they are the owner
            var userId = AuthHelper.CurrentUser?.UserId;
            if (userId.HasValue)
            {
                if (course.OwnerId == userId.Value)
                {
                    btnAddToCart.Visible = false;
                }
                else
                {
                    // Check asynchronously whether the current user already purchased this course
                    _ = UpdateAddToCartVisibilityAsync(course.CourseId, userId.Value);
                }
            }

            // Load cover image if available; otherwise keep Designer placeholder
            picCover.Controls.Clear();
            picCover.Image = null;
            if (!string.IsNullOrEmpty(course.CoverUrl))
            {
                try
                {
                    var path = course.CoverUrl.Replace('/', Path.DirectorySeparatorChar);
                    if (!Path.IsPathRooted(path))
                        path = Path.Combine(Application.StartupPath, path.TrimStart('\\', '/'));

                    if (System.IO.File.Exists(path))
                    {
                        picCover.Image = Image.FromFile(path);
                    }
                }
                catch { /* ignore image load errors */ }
            }

            // If no image assigned, add a simple placeholder label
            if (picCover.Image == null)
            {
                var lblImagePlaceholder = new Label
                {
                    Text = "📚",
                    Font = new Font("Segoe UI", 48),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(200, 200, 200)
                };
                picCover.Controls.Add(lblImagePlaceholder);
            }
        }

        private async System.Threading.Tasks.Task UpdateAddToCartVisibilityAsync(int courseId, int userId)
        {
            try
            {
                using var context = new LearningPlatformContext();
                bool purchased = await context.CoursePurchases.AnyAsync(cp => cp.CourseId == courseId && cp.BuyerId == userId);

                // Update UI on the UI thread
                if (this.IsHandleCreated && !this.Disposing && !this.IsDisposed)
                {
                    this.Invoke(() => btnAddToCart.Visible = !purchased);
                }
                else
                {
                    btnAddToCart.Visible = !purchased;
                }
            }
            catch
            {
                // ignore DB errors and leave button visible
            }
        }

        private void PnlCard_MouseEnter(object sender, EventArgs e)
        {
            pnlCard.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void PnlCard_MouseLeave(object sender, EventArgs e)
        {
            pnlCard.BackColor = CardBackground;
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            if (CourseId <= 0) return;

            var form = this.FindForm();
            if (form == null) return;

            // Try to find MainContainer and call its navigation helper if available
            if (form is MainContainer mainContainer)
            {
                try
                {
                    mainContainer.NavigateToCourseDetail(CourseId);
                    return;
                }
                catch { /* fallback below */ }
            }

            // Fallback: place CourseDetailControl into mainContentPanel
            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;
            if (mainPanel == null) mainPanel = this.Parent as Panel;
            if (mainPanel == null) return;

            mainPanel.Controls.Clear();
            var detail = new CourseDetailControl(CourseId);
            detail.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(detail);
        }

        private async void BtnAddToCart_Click(object sender, EventArgs e)
        {
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue)
            {
                ToastHelper.Show(this.FindForm(), "Vui lòng đăng nhập để thêm vào giỏ hàng!");
                return;
            }

            if (CourseId <= 0)
            {
                ToastHelper.Show(this.FindForm(), "Không có khóa học để thêm.");
                return;
            }

            try
            {
                using var context = new LearningPlatformContext();
                var cart = await context.ShoppingCarts.FirstOrDefaultAsync(c => c.UserId == userId.Value);
                if (cart == null)
                {
                    cart = new ShoppingCart { UserId = userId.Value, CreatedAt = DateTime.Now };
                    context.ShoppingCarts.Add(cart);
                    await context.SaveChangesAsync();
                }

                var existing = await context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.CourseId == CourseId);
                if (existing == null)
                {
                    var item = new CartItem { CartId = cart.CartId, CourseId = CourseId, AddedAt = DateTime.Now };
                    context.CartItems.Add(item);
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

        private Control FindControlRecursive(Control parent, string name)
        {
            if (parent == null) return null;
            foreach (Control c in parent.Controls)
            {
                if (string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase)) return c;
                var found = FindControlRecursive(c, name);
                if (found != null) return found;
            }
            return null;
        }
    }
}

