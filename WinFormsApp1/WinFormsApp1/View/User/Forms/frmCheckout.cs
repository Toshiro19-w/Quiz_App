using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;
using WinFormsApp1.Models.EF;
using WinFormsApp1.View.User.Components;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Service;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Forms
{
    public partial class frmCheckout : Form
    {
        // Discount state
        private decimal _originalTotal = 0;
        private decimal _discountAmount = 0;
        private decimal _finalAmount = 0;
        private Discount? _appliedDiscount = null;
        private System.Collections.Generic.List<int> _courseIds = new();

        public frmCheckout()
        {
            InitializeComponent();
            LoadCartItems();
        }

        private void LoadCartItems()
        {
            panelCartItems.Controls.Clear();
            _courseIds.Clear();

            try
            {
                using (var context = new LearningPlatformContext())
                {
                    var user = AuthHelper.CurrentUser;
                    if (user == null)
                    {
                        MessageBox.Show(LanguageHelper.GetString("PleaseLoginToContinue"), LanguageHelper.GetString("Notification"),
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
                            yPos += 195; // Increased spacing for card separation

                            total += item.Course.Price;
                            _courseIds.Add(item.Course.CourseId);
                        }
                    }

                    _originalTotal = total;
                    _finalAmount = total;
                    _discountAmount = 0;
                    _appliedDiscount = null;

                    UpdateSummary(cart.CartItems.Count, total, 0, total);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageHelper.GetString("CartLoadError", ex.Message), LanguageHelper.GetString("Error"),
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
                Text = "",
                Font = new Font("Segoe UI", 72),
                Location = new Point(150, 20),
                AutoSize = true
            };
            emptyPanel.Controls.Add(iconLabel);

            var messageLabel = new Label
            {
                Text = LanguageHelper.GetString("CartEmpty"),
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.Gray,
                Location = new Point(100, 130),
                AutoSize = true
            };
            emptyPanel.Controls.Add(messageLabel);

            panelCartItems.Controls.Add(emptyPanel);
            UpdateSummary(0, 0, 0, 0);
        }

        private void RemoveCartItem(int cartItemId)
        {
            try
            {
                var result = MessageBox.Show(LanguageHelper.GetString("RemoveCourseConfirm"),
                    LanguageHelper.GetString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (var context = new LearningPlatformContext())
                    {
                        var cartItem = context.CartItems.Find(cartItemId);
                        if (cartItem != null)
                        {
                            context.CartItems.Remove(cartItem);
                            context.SaveChanges();
                            
                            // Reset discount when cart changes
                            _appliedDiscount = null;
                            lblSelectedVoucher.Text = "";
                            lblSelectedVoucher.Visible = false;
                            btnRemoveVoucher.Visible = false;
                            
                            LoadCartItems();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageHelper.GetString("DeleteError", ex.Message), LanguageHelper.GetString("Error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummary(int count, decimal subtotal, decimal discount, decimal total)
        {
            lblSoKhoaHocValue.Text = count.ToString();
            lblTamTinhValue.Text = $"{subtotal:N0} VND";
            
            // Show discount if applied
            if (discount > 0)
            {
                lblGiamGia.Visible = true;
                lblGiamGiaValue.Visible = true;
                lblGiamGiaValue.Text = $"-{discount:N0} VND";
                lblGiamGiaValue.ForeColor = Color.FromArgb(40, 167, 69);
            }
            else
            {
                lblGiamGia.Visible = false;
                lblGiamGiaValue.Visible = false;
            }
            
            lblTongCongValue.Text = $"{total:N0} VND";
        }

        private async void btnThanhToanMoMo_Click(object sender, EventArgs e)
        {
            try
            {
                var user = AuthHelper.CurrentUser;
                if (user == null)
                {
                    MessageBox.Show(LanguageHelper.GetString("PleaseLoginToPayment"), LanguageHelper.GetString("Notification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Truyền thông tin mã giảm giá vào MoMo payment
                var discountId = _appliedDiscount?.DiscountId;
                var success = await MoMoPaymentHelper.PayCartAsync(
                    user.UserId, 
                    this, 
                    discountId, 
                    _discountAmount
                );

                if (success)
                {
                    MessageBox.Show(LanguageHelper.GetString("MoMoPaymentSuccess"), LanguageHelper.GetString("Notification"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(LanguageHelper.GetString("MoMoPaymentIncomplete"), LanguageHelper.GetString("Notification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadCartItems();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageHelper.GetString("MoMoPaymentError", ex.Message), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        MessageBox.Show(LanguageHelper.GetString("CartEmpty"), LanguageHelper.GetString("Notification"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var tempCourse = new Course
                    {
                        Title = LanguageHelper.GetString("PaymentForCourses", cart.CartItems.Count),
                        Price = _finalAmount, // Use final amount with discount
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
                            MessageBox.Show($"Error: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
        }

        private async void btnSelectVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                var user = AuthHelper.CurrentUser;
                if (user == null)
                {
                    MessageBox.Show(LanguageHelper.GetString("PleaseLoginForVoucher"), LanguageHelper.GetString("Notification"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Open voucher selector
                var voucherForm = new frmVoucherSelector(_originalTotal, user.UserId);
                if (voucherForm.ShowDialog() == DialogResult.OK)
                {
                    var selectedVoucher = voucherForm.SelectedVoucher;
                    if (selectedVoucher != null)
                    {
                        // Validate and apply voucher
                        var result = await DiscountService.ValidateDiscountAsync(
                            selectedVoucher.Code, 
                            user.UserId, 
                            _originalTotal, 
                            _courseIds
                        );

                        if (result.IsValid && result.Discount != null)
                        {
                            _appliedDiscount = result.Discount;
                            _discountAmount = result.DiscountAmount;
                            _finalAmount = result.FinalAmount;

                            // Update UI
                            lblSelectedVoucher.Text = $"✓ {selectedVoucher.Code}: -{_discountAmount:N0} VND";
                            lblSelectedVoucher.Visible = true;
                            btnRemoveVoucher.Visible = true;
                            lblDiscountMessage.Visible = false;

                            // Update summary
                            UpdateSummary(_courseIds.Count, _originalTotal, _discountAmount, _finalAmount);

                            MessageBox.Show(result.Message, LanguageHelper.GetString("Success"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(result.Message, LanguageHelper.GetString("CannotApply"),
                                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error: {ex.Message}", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

        private void btnRemoveVoucher_Click(object sender, EventArgs e)
        {
            // Reset discount
            _appliedDiscount = null;
            _discountAmount = 0;
            _finalAmount = _originalTotal;

            // Update UI
            lblSelectedVoucher.Text = "";
            lblSelectedVoucher.Visible = false;
            btnRemoveVoucher.Visible = false;
            lblDiscountMessage.Visible = false;

            // Update summary
            UpdateSummary(_courseIds.Count, _originalTotal, 0, _originalTotal);
        }

        private void txtDiscountCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Apply discount on Enter key
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSelectVoucher_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
