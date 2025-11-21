using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.View.User.Components
{
    public class CartDropdown : Panel
    {
        private Label lblTitle;
        private Panel cartItemsPanel;
        private Panel footerPanel;
        private Label lblTotal;
        private Label lblTotalAmount;
        private Button btnCheckout;
        private System.Windows.Forms.Timer fadeTimer;
        private int targetOpacity = 100;
        private int currentOpacity = 0;

        public event EventHandler? OnCheckoutClick;

        public CartDropdown()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Width = 550;
            this.Height = 500;
            this.BackColor = Color.White;
            this.Visible = false;

            // Border effect
            this.Paint += CartDropdown_Paint;

            // Title
            lblTitle = new Label
            {
                Text = "Giỏ hàng của bạn",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            this.Controls.Add(lblTitle);

            // Cart Items Panel (scrollable)
            cartItemsPanel = new Panel
            {
                Location = new Point(0, 70),
                Size = new Size(550, 320),
                BackColor = Color.White,
                AutoScroll = true
            };
            this.Controls.Add(cartItemsPanel);

            // Footer Panel
            footerPanel = new Panel
            {
                Location = new Point(0, 390),
                Size = new Size(550, 110),
                BackColor = Color.White
            };

            // Total label
            lblTotal = new Label
            {
                Text = "Tổng:",
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            footerPanel.Controls.Add(lblTotal);

            // Total amount
            lblTotalAmount = new Label
            {
                Text = "0 ₫",
                Location = new Point(400, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(88, 56, 255)
            };
            footerPanel.Controls.Add(lblTotalAmount);

            // Checkout button
            btnCheckout = new Button
            {
                Text = "Chuyển đến giỏ hàng",
                Location = new Point(20, 55),
                Size = new Size(510, 45),
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.Click += (s, e) =>
            {
                OnCheckoutClick?.Invoke(this, EventArgs.Empty);
            };
            footerPanel.Controls.Add(btnCheckout);

            this.Controls.Add(footerPanel);

            // Setup fade timer
            fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 10;
            fadeTimer.Tick += FadeTimer_Tick;

            LoadCartItems();
        }

        private void CartDropdown_Paint(object sender, PaintEventArgs e)
        {
            // Draw shadow effect
            using (Pen shadowPen = new Pen(Color.FromArgb(30, 0, 0, 0), 1))
            {
                e.Graphics.DrawRectangle(shadowPen, 0, 0, this.Width - 1, this.Height - 1);
            }

            // Draw border
            Rectangle rect = new Rectangle(1, 1, this.Width - 3, this.Height - 3);
            using (Pen borderPen = new Pen(ColorPalette.Border, 1))
            {
                e.Graphics.DrawRectangle(borderPen, rect);
            }
        }

        private void FadeTimer_Tick(object? sender, EventArgs e)
        {
            if (currentOpacity < targetOpacity)
            {
                currentOpacity += 10;
                if (currentOpacity >= targetOpacity)
                {
                    currentOpacity = targetOpacity;
                    fadeTimer.Stop();
                }
            }
            else if (currentOpacity > targetOpacity)
            {
                currentOpacity -= 10;
                if (currentOpacity <= targetOpacity)
                {
                    currentOpacity = targetOpacity;
                    fadeTimer.Stop();
                    if (currentOpacity == 0)
                    {
                        this.Visible = false;
                    }
                }
            }

            this.Invalidate();
        }

        public void LoadCartItems()
        {
            cartItemsPanel.Controls.Clear();

            try
            {
                using (var context = new LearningPlatformContext())
                {
                    var user = AuthHelper.CurrentUser;
                    if (user == null) return;

                    // Get or create cart
                    var cart = context.ShoppingCarts
                        .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Course)
                        .ThenInclude(course => course.Owner)
                        .FirstOrDefault(c => c.UserId == user.UserId);

                    if (cart == null || !cart.CartItems.Any())
                    {
                        ShowEmptyCart();
                        return;
                    }

                    int yPos = 10;
                    decimal total = 0;

                    foreach (var item in cart.CartItems)
                    {
                        if (item.Course != null)
                        {
                            var itemControl = CreateCartItemControl(item);
                            itemControl.Location = new Point(10, yPos);
                            cartItemsPanel.Controls.Add(itemControl);
                            yPos += 110;

                            total += item.Course.Price;
                        }
                    }

                    // Update total
                    lblTotalAmount.Text = $"{total:N0} ₫";
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
                Location = new Point(0, 80),
                Size = new Size(550, 200),
                BackColor = Color.White
            };

            var iconLabel = new Label
            {
                Font = new Font("Segoe UI", 48),
                Location = new Point(235, 30),
                AutoSize = true
            };
            emptyPanel.Controls.Add(iconLabel);

            var messageLabel = new Label
            {
                Text = "Giỏ hàng trống",
                Font = new Font("Segoe UI", 13),
                ForeColor = Color.Gray,
                Location = new Point(195, 120),
                AutoSize = true
            };
            emptyPanel.Controls.Add(messageLabel);

            cartItemsPanel.Controls.Add(emptyPanel);

            lblTotalAmount.Text = "0 ₫";
        }

        private Panel CreateCartItemControl(Models.Entities.CartItem item)
        {
            var panel = new Panel
            {
                Size = new Size(530, 100),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Course label
            var lblCourse = new Label
            {
                Text = "Course",
                Location = new Point(15, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            panel.Controls.Add(lblCourse);

            // Course title
            var lblTitle = new Label
            {
                Text = item.Course.Title,
                Location = new Point(15, 35),
                Size = new Size(350, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            panel.Controls.Add(lblTitle);

            // Instructor
            var lblInstructor = new Label
            {
                Text = $"👤 {item.Course.Owner?.FullName ?? "N/A"}",
                Location = new Point(15, 62),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            panel.Controls.Add(lblInstructor);

            // Price
            var lblPrice = new Label
            {
                Text = $"{item.Course.Price:N0} ₫",
                Location = new Point(380, 35),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            panel.Controls.Add(lblPrice);

            // Remove button
            var btnRemove = new LinkLabel
            {
                Text = "Xóa",
                Location = new Point(480, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                LinkColor = Color.FromArgb(88, 56, 255)
            };
            btnRemove.Click += (s, e) => RemoveCartItem(item.CartItemId);
            panel.Controls.Add(btnRemove);

            return panel;
        }

        private void RemoveCartItem(int cartItemId)
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa khóa học này khỏi giỏ hàng?", 
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

        public void ShowDropdown(Control parent)
        {
            this.Visible = true;
            this.BringToFront();

            // Position below the cart button
            Point location = parent.PointToScreen(Point.Empty);
            Point formLocation = parent.FindForm().PointToScreen(Point.Empty);

            this.Location = new Point(
                location.X - formLocation.X - this.Width + parent.Width,
                location.Y - formLocation.Y + parent.Height + 5
            );

            // Reload items
            LoadCartItems();

            // Start fade in animation
            currentOpacity = 0;
            targetOpacity = 100;
            fadeTimer.Start();
        }

        public void HideDropdown()
        {
            // Start fade out animation
            targetOpacity = 0;
            fadeTimer.Start();
        }
    }
}
