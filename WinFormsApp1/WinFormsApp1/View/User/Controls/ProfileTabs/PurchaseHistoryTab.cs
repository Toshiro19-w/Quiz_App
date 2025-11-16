using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.View.User.Controls.ProfileTabs
{
    public partial class PurchaseHistoryTab : UserControl
    {
        private Label lblPurchaseCount;
        private Panel purchaseListPanel;

        public PurchaseHistoryTab()
        {
            InitializeComponent();
            LoadPurchaseHistory();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.White;
            this.Size = new Size(760, 550);
            this.AutoScroll = true;

            int yPos = 30;

            lblPurchaseCount = new Label
            {
                Location = new Point(30, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray
            };
            this.Controls.Add(lblPurchaseCount);
            yPos += 50;

            purchaseListPanel = new Panel
            {
                Location = new Point(0, yPos),
                Size = new Size(760, 470),
                AutoScroll = true,
                BackColor = Color.White
            };
            this.Controls.Add(purchaseListPanel);
        }

        private void LoadPurchaseHistory()
        {
            try
            {
                using (var context = new LearningPlatformContext())
                {
                    var user = AuthHelper.CurrentUser;
                    if (user == null) return;

                    var purchases = context.CoursePurchases
                        .Include(cp => cp.Course)
                        .Where(cp => cp.BuyerId == user.UserId && cp.Status == "Paid")
                        .OrderByDescending(cp => cp.PurchasedAt)
                        .ToList();

                    lblPurchaseCount.Text = $"Số lượng purchase: {purchases.Count}";

                    if (purchases.Count == 0)
                    {
                        ShowEmptyState();
                        return;
                    }

                    int yPos = 20;
                    foreach (var purchase in purchases)
                    {
                        var purchaseCard = CreatePurchaseCard(purchase);
                        purchaseCard.Location = new Point(30, yPos);
                        purchaseListPanel.Controls.Add(purchaseCard);
                        yPos += 120;
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
                Location = new Point(0, 100),
                Size = new Size(760, 200),
                BackColor = Color.White
            };

            var iconLabel = new Label
            {
                Text = "🛒",
                Font = new Font("Segoe UI", 48),
                Location = new Point(330, 30),
                AutoSize = true
            };
            emptyPanel.Controls.Add(iconLabel);

            var messageLabel = new Label
            {
                Text = "Bạn chưa có đơn hàng nào",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Location = new Point(270, 120),
                AutoSize = true
            };
            emptyPanel.Controls.Add(messageLabel);

            purchaseListPanel.Controls.Add(emptyPanel);
        }

        private Panel CreatePurchaseCard(Models.Entities.CoursePurchase purchase)
        {
            var card = new Panel
            {
                Size = new Size(700, 100),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Course title
            var lblTitle = new Label
            {
                Text = purchase.Course.Title,
                Location = new Point(15, 15),
                Size = new Size(500, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            card.Controls.Add(lblTitle);

            // Purchase date
            var lblDate = new Label
            {
                Text = $"Ngày mua: {purchase.PurchasedAt:dd/MM/yyyy HH:mm}",
                Location = new Point(15, 45),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblDate);

            // Price
            var lblPrice = new Label
            {
                Text = $"{purchase.PricePaid:N0} {purchase.Currency}",
                Location = new Point(550, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.Primary
            };
            card.Controls.Add(lblPrice);

            // Status badge
            var lblStatus = new Label
            {
                Text = "✓ Đã thanh toán",
                Location = new Point(550, 45),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Green
            };
            card.Controls.Add(lblStatus);

            return card;
        }
    }
}
