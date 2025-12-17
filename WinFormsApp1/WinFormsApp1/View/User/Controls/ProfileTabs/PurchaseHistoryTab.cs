using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.View.User.Controls.ProfileTabs
{
    public partial class PurchaseHistoryTab : UserControl
    {
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblPurchaseCount;
        private Label lblTotalSpent;
        private Panel purchaseListPanel;

        public PurchaseHistoryTab()
        {
            InitializeComponent();
            LoadPurchaseHistory();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.Dock = DockStyle.Fill;
            this.AutoScroll = false;

            // Header stats panel
            headerPanel = new Panel
            {
                Location = new Point(30, 20),
                Height = 100,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White
            };
            headerPanel.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(230, 230, 230), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, headerPanel.Width - 1, headerPanel.Height - 1);
            };

            lblTitle = new Label
            {
                Text = "📋 Lịch sử đơn hàng",
                Location = new Point(25, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            headerPanel.Controls.Add(lblTitle);

            lblPurchaseCount = new Label
            {
                Location = new Point(25, 55),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray
            };
            headerPanel.Controls.Add(lblPurchaseCount);

            lblTotalSpent = new Label
            {
                Location = new Point(280, 55),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray
            };
            headerPanel.Controls.Add(lblTotalSpent);

            this.Controls.Add(headerPanel);

            purchaseListPanel = new Panel
            {
                Location = new Point(30, 135),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoScroll = true,
                BackColor = Color.FromArgb(248, 249, 250)
            };
            this.Controls.Add(purchaseListPanel);

            this.Resize += PurchaseHistoryTab_Resize;
            this.Load += (s, e) => PurchaseHistoryTab_Resize(s, e);
        }

        private void PurchaseHistoryTab_Resize(object? sender, EventArgs e)
        {
            headerPanel.Width = this.Width - 60;
            purchaseListPanel.Width = this.Width - 60;
            purchaseListPanel.Height = this.Height - 165;

            foreach (Control c in purchaseListPanel.Controls)
            {
                if (c is Panel card)
                {
                    int newWidth = purchaseListPanel.Width - 25;
                    card.Width = newWidth;
                    UpdateCardLayout(card, newWidth);
                }
            }
        }

        private void UpdateCardLayout(Panel card, int cardWidth)
        {
            Label? lblTotal = null;
            Label? lblOriginal = null;
            
            foreach (Control c in card.Controls)
            {
                if (c.Tag is string tag)
                {
                    switch (tag)
                    {
                        case "date":
                        case "coursePrice":
                        case "discountAmount":
                            c.Location = new Point(cardWidth - c.Width - 25, c.Location.Y);
                            break;
                        case "totalPrice":
                            lblTotal = (Label)c;
                            break;
                        case "originalPrice":
                            lblOriginal = (Label)c;
                            break;
                        case "separator":
                            ((Panel)c).Width = cardWidth - 40;
                            break;
                    }
                }
            }
            
            // Position total and original prices properly
            if (lblTotal != null)
            {
                lblTotal.Location = new Point(cardWidth - lblTotal.Width - 25, lblTotal.Location.Y);
                
                if (lblOriginal != null)
                {
                    // Original price goes to the left of total price with some spacing
                    lblOriginal.Location = new Point(lblTotal.Location.X - lblOriginal.Width - 15, lblOriginal.Location.Y);
                }
            }
        }

        private void LoadPurchaseHistory()
        {
            try
            {
                using (var context = new LearningPlatformContext())
                {
                    var user = AuthHelper.CurrentUser;
                    if (user == null) return;

                    var orders = context.Orders
                        .Include(o => o.OrderItems)
                            .ThenInclude(oi => oi.Course)
                        .Include(o => o.Discount)
                        .Include(o => o.Payments)
                        .Where(o => o.BuyerId == user.UserId && o.Status == "Paid")
                        .OrderByDescending(o => o.PaidAt ?? o.CreatedAt)
                        .ToList();

                    var totalSpent = orders.Sum(o => o.TotalAmount);
                    var totalDiscount = orders.Sum(o => o.DiscountAmount ?? 0);

                    lblPurchaseCount.Text = $"🛒 Tổng số đơn hàng: {orders.Count}";
                    lblTotalSpent.Text = $"💰 Đã chi tiêu: {totalSpent:N0} VND" + 
                        (totalDiscount > 0 ? $" (Tiết kiệm: {totalDiscount:N0} VND)" : "");

                    if (orders.Count == 0)
                    {
                        ShowEmptyState();
                        return;
                    }

                    int yPos = 0;
                    foreach (var order in orders)
                    {
                        var orderCard = CreateOrderCard(order);
                        orderCard.Location = new Point(0, yPos);
                        purchaseListPanel.Controls.Add(orderCard);
                        yPos += orderCard.Height + 15;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lịch sử mua hàng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowEmptyState()
        {
            var emptyPanel = new Panel
            {
                Location = new Point(0, 50),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Height = 300,
                BackColor = Color.White
            };
            emptyPanel.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(230, 230, 230), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, emptyPanel.Width - 1, emptyPanel.Height - 1);
            };

            var iconLabel = new Label
            {
                Text = "🛒",
                Font = new Font("Segoe UI", 56),
                AutoSize = true
            };
            emptyPanel.Controls.Add(iconLabel);

            var messageLabel = new Label
            {
                Text = "Bạn chưa có đơn hàng nào",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.Gray,
                AutoSize = true
            };
            emptyPanel.Controls.Add(messageLabel);

            var subLabel = new Label
            {
                Text = "Hãy khám phá các khóa học của chúng tôi!",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(150, 150, 150),
                AutoSize = true
            };
            emptyPanel.Controls.Add(subLabel);

            emptyPanel.Resize += (s, e) =>
            {
                iconLabel.Location = new Point((emptyPanel.Width - iconLabel.Width) / 2, 50);
                messageLabel.Location = new Point((emptyPanel.Width - messageLabel.Width) / 2, 150);
                subLabel.Location = new Point((emptyPanel.Width - subLabel.Width) / 2, 185);
            };

            purchaseListPanel.Controls.Add(emptyPanel);
            emptyPanel.Width = purchaseListPanel.Width - 20;
        }

        private Panel CreateOrderCard(Order order)
        {
            var courseCount = order.OrderItems?.Count ?? 0;
            var hasDiscount = order.DiscountAmount.HasValue && order.DiscountAmount.Value > 0;

            int baseHeight = 70;
            int courseListHeight = Math.Min(courseCount, 3) * 26;
            int discountHeight = hasDiscount ? 28 : 0;
            int summaryHeight = 45;
            int cardHeight = baseHeight + courseListHeight + discountHeight + summaryHeight;

            var card = new Panel
            {
                Height = cardHeight,
                BackColor = Color.White
            };
            card.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(230, 230, 230), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };

            int yPos = 15;

            // Order ID
            var lblOrderId = new Label
            {
                Text = $"🧾 Đơn hàng #{order.OrderId}",
                Location = new Point(20, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            card.Controls.Add(lblOrderId);

            // Date - positioned at right
            var lblDate = new Label
            {
                Text = $"{(order.PaidAt ?? order.CreatedAt):dd/MM/yyyy HH:mm}",
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Tag = "date"
            };
            lblDate.Location = new Point(20, yPos);
            card.Controls.Add(lblDate);

            yPos += 32;

            // Course list
            if (order.OrderItems != null && order.OrderItems.Any())
            {
                foreach (var item in order.OrderItems.Take(3))
                {
                    var courseName = item.Course?.Title ?? "Khóa học không xác định";
                    if (courseName.Length > 55) courseName = courseName.Substring(0, 52) + "...";

                    var lblCourse = new Label
                    {
                        Text = $"  📚 {courseName}",
                        Location = new Point(25, yPos),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9),
                        ForeColor = ColorPalette.TextSecondary
                    };
                    card.Controls.Add(lblCourse);

                    var lblCoursePrice = new Label
                    {
                        Text = $"{item.Price:N0} VND",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9),
                        ForeColor = Color.Gray,
                        Tag = "coursePrice"
                    };
                    lblCoursePrice.Location = new Point(20, yPos);
                    card.Controls.Add(lblCoursePrice);

                    yPos += 26;
                }

                if (courseCount > 3)
                {
                    var lblMore = new Label
                    {
                        Text = $"  ... và {courseCount - 3} khóa học khác",
                        Location = new Point(25, yPos),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9, FontStyle.Italic),
                        ForeColor = Color.FromArgb(100, 100, 100)
                    };
                    card.Controls.Add(lblMore);
                    yPos += 24;
                }
            }

            // Discount section
            if (hasDiscount)
            {
                yPos += 3;
                var discountCode = order.Discount?.Code ?? "";
                var lblDiscount = new Label
                {
                    Text = $"🏷️ Mã giảm giá: {discountCode}",
                    Location = new Point(25, yPos),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.FromArgb(40, 167, 69)
                };
                card.Controls.Add(lblDiscount);

                var lblDiscountAmount = new Label
                {
                    Text = $"-{order.DiscountAmount.Value:N0} VND",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.FromArgb(40, 167, 69),
                    Tag = "discountAmount"
                };
                lblDiscountAmount.Location = new Point(20, yPos);
                card.Controls.Add(lblDiscountAmount);

                yPos += 28;
            }

            // Separator
            var separator = new Panel
            {
                Location = new Point(20, yPos),
                Height = 1,
                BackColor = Color.FromArgb(230, 230, 230),
                Tag = "separator"
            };
            card.Controls.Add(separator);
            yPos += 10;

            int footerY = yPos;

            // Status badge
            var lblStatus = new Label
            {
                Text = " ✓ Đã thanh toán ",
                Location = new Point(25, footerY),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 167, 69),
                BackColor = Color.FromArgb(212, 237, 218)
            };
            card.Controls.Add(lblStatus);

            // Payment method
            var payment = order.Payments?.FirstOrDefault();
            if (payment != null)
            {
                var providerIcon = payment.Provider switch
                {
                    "MoMo" => "💳",
                    "VNPay" => "🏦",
                    _ => "💰"
                };
                var lblPayment = new Label
                {
                    Text = $"{providerIcon} {payment.Provider}",
                    Location = new Point(155, footerY + 1),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.Gray
                };
                card.Controls.Add(lblPayment);
            }

            // Total price (always shown)
            var lblTotal = new Label
            {
                Text = $"{order.TotalAmount:N0} VND",
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.Primary,
                Tag = "totalPrice"
            };
            lblTotal.Location = new Point(20, footerY - 2);
            card.Controls.Add(lblTotal);

            // Original price (strikethrough) if discounted - positioned BEFORE total
            if (hasDiscount && order.OriginalAmount.HasValue)
            {
                var lblOriginal = new Label
                {
                    Text = $"{order.OriginalAmount.Value:N0} VND",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Strikeout),
                    ForeColor = Color.Gray,
                    Tag = "originalPrice"
                };
                lblOriginal.Location = new Point(20, footerY);
                card.Controls.Add(lblOriginal);
            }

            card.Height = footerY + 30;

            // Initial positioning
            card.Resize += (s, e) => UpdateCardLayout(card, card.Width);

            return card;
        }
    }
}
