using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.View.User.Components;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.View.User.Forms
{
    public partial class frmCheckout : Form
    {
        public frmCheckout()
        {
            InitializeComponent();
            LoadCartItems();
        }

        private void LoadCartItems()
        {
            panelCartItems.Controls.Clear();

            try
            {
                using (var context = new LearningPlatformContext())
                {
                    var user = AuthHelper.CurrentUser;
                    if (user == null)
                    {
                        MessageBox.Show("Vui lòng đăng nhập để tiếp tục", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        return;
                    }

                    var cart = context.ShoppingCarts
                        .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Course)
                        .ThenInclude(c => c.Owner)
                        .FirstOrDefault(c => c.UserId == user.UserId);

                    if (cart == null || !cart.CartItems.Any())
                    {
                        ShowEmptyCart();
                        return;
                    }

                    int yPos = 0;
                    decimal total = 0;

                    foreach (var item in cart.CartItems)
                    {
                        if (item.Course != null)
                        {
                            var cartItem = new CheckoutCartItem(item.Course);
                            cartItem.Location = new Point(0, yPos);
                            cartItem.OnRemoveClick += (s, courseId) => RemoveCartItem(item.CartItemId);
                            panelCartItems.Controls.Add(cartItem);
                            yPos += 135;

                            total += item.Course.Price;
                        }
                    }

                    UpdateSummary(cart.CartItems.Count, total);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải giỏ hàng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowEmptyCart()
        {
            var emptyPanel = new Panel
            {
                Location = new Point(250, 150),
                Size = new Size(400, 200),
                BackColor = Color.White
            };

            var iconLabel = new Label
            {
                Text = "🛒",
                Font = new Font("Segoe UI", 72),
                Location = new Point(150, 20),
                AutoSize = true
            };
            emptyPanel.Controls.Add(iconLabel);

            var messageLabel = new Label
            {
                Text = "Giỏ hàng trống",
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.Gray,
                Location = new Point(100, 130),
                AutoSize = true
            };
            emptyPanel.Controls.Add(messageLabel);

            panelCartItems.Controls.Add(emptyPanel);
            UpdateSummary(0, 0);
        }

        private void RemoveCartItem(int cartItemId)
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa khóa học này?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (var context = new LearningPlatformContext())
                    {
                        var cartItem = context.CartItems.Find(cartItemId);
                        if (cartItem != null)
                        {
                            context.CartItems.Remove(cartItem);
                            context.SaveChanges();
                            LoadCartItems();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummary(int count, decimal total)
        {
            lblSoKhoaHocValue.Text = count.ToString();
            lblTamTinhValue.Text = $"{total:N0} VND";
            lblTongCongValue.Text = $"{total:N0} VND";
        }

        private void btnThanhToanMoMo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng thanh toán MoMo đang được phát triển", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTheTinDung_Click(object sender, EventArgs e)
        {
            try
            {
                var user = AuthHelper.CurrentUser;
                if (user == null) return;

                using (var context = new LearningPlatformContext())
                {
                    var cart = context.ShoppingCarts
                        .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Course)
                        .FirstOrDefault(c => c.UserId == user.UserId);

                    if (cart == null || !cart.CartItems.Any())
                    {
                        MessageBox.Show("Giỏ hàng trống", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    decimal total = cart.CartItems.Sum(ci => ci.Course.Price);
                    var tempCourse = new Models.Entities.Course
                    {
                        Title = $"Thanh toán {cart.CartItems.Count} khóa học",
                        Price = total,
                        CourseId = 0
                    };

                    var paymentForm = new PaymentForm(tempCourse, isCartPayment: true);
                    if (paymentForm.ShowDialog() == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
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
