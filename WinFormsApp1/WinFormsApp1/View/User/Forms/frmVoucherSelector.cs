using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Service;
using WinFormsApp1.View.User.Controls;

namespace WinFormsApp1.View.User.Forms
{
    public partial class frmVoucherSelector : Form
    {
        private List<Discount> _vouchers = new();
        private Discount? _selectedVoucher;
        private decimal _orderAmount;
        private int _userId;

        public Discount? SelectedVoucher => _selectedVoucher;

        public frmVoucherSelector(decimal orderAmount, int userId)
        {
            InitializeComponent();
            _orderAmount = orderAmount;
            _userId = userId;
            LoadVouchersAsync();
            SetupEvents();
        }

        private void SetupEvents()
        {
            this.Load += async (s, e) =>
            {
                this.Opacity = 0;
                await Task.Delay(50);
                for (double i = 0; i <= 1; i += 0.1)
                {
                    this.Opacity = i;
                    await Task.Delay(20);
                }
            };
        }

        private async void LoadVouchersAsync()
        {
            try
            {
                flowLayoutVouchers.Controls.Clear();

                // Load active vouchers
                _vouchers = await DiscountService.GetActiveDiscountsAsync();

                if (_vouchers.Count == 0)
                {
                    var lblNoVoucher = new Label
                    {
                        Text = "Không có voucher khả dụng",
                        Font = new Font("Segoe UI", 11F),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Padding = new Padding(20)
                    };
                    flowLayoutVouchers.Controls.Add(lblNoVoucher);
                    return;
                }

                // Tạo danh sách VoucherItemControl
                var voucherControls = new List<VoucherItemControl>();
                
                foreach (var voucher in _vouchers)
                {
                    var voucherItem = new VoucherItemControl
                    {
                        Discount = voucher,
                        OrderAmount = _orderAmount,
                        Width = flowLayoutVouchers.Width - 30,
                        Margin = new Padding(5, 5, 5, 5)
                    };

                    voucherItem.VoucherSelected += VoucherItem_VoucherSelected;
                    voucherControls.Add(voucherItem);
                }

                // Sắp xếp: Voucher dùng được trước, không dùng được sau
                var sortedVouchers = voucherControls
                    .OrderByDescending(v => v.CanUse)  // true trước, false sau
                    .ThenByDescending(v => v.Discount?.DiscountValue ?? 0)  // Giá trị giảm cao trước
                    .ToList();

                // Add vào flowlayout theo thứ tự đã sắp xếp
                foreach (var voucherItem in sortedVouchers)
                {
                    flowLayoutVouchers.Controls.Add(voucherItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách voucher: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VoucherItem_VoucherSelected(object? sender, Discount voucher)
        {
            // Uncheck all other vouchers
            foreach (VoucherItemControl item in flowLayoutVouchers.Controls.OfType<VoucherItemControl>())
            {
                if (item != sender)
                {
                    item.IsSelected = false;
                }
            }

            _selectedVoucher = voucher;
        }

        private async void btnApplyCode_Click(object sender, EventArgs e)
        {
            var code = txtVoucherCode.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Vui lòng nhập mã voucher", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var result = await DiscountService.ValidateDiscountAsync(code, _userId, _orderAmount);

                if (result.IsValid)
                {
                    _selectedVoucher = result.Discount;

                    // Select the voucher in list if exists
                    var existingItem = flowLayoutVouchers.Controls
                        .OfType<VoucherItemControl>()
                        .FirstOrDefault(v => v.Discount?.DiscountId == result.Discount?.DiscountId);

                    if (existingItem != null)
                    {
                        // Uncheck all
                        foreach (VoucherItemControl item in flowLayoutVouchers.Controls.OfType<VoucherItemControl>())
                        {
                            item.IsSelected = false;
                        }
                        existingItem.IsSelected = true;
                    }
                    else
                    {
                        // Add to list if not exists
                        var voucherItem = new VoucherItemControl
                        {
                            Discount = result.Discount,
                            OrderAmount = _orderAmount,
                            Width = flowLayoutVouchers.Width - 30,
                            Margin = new Padding(5, 5, 5, 5),
                            IsSelected = true
                        };
                        voucherItem.VoucherSelected += VoucherItem_VoucherSelected;
                        flowLayoutVouchers.Controls.Add(voucherItem);
                        flowLayoutVouchers.Controls.SetChildIndex(voucherItem, 0);
                    }

                    MessageBox.Show(result.Message, "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(result.Message, "Không thể áp dụng",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _selectedVoucher = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            btnCancel_Click(sender, e);
        }
    }
}
