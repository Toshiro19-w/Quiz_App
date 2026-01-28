using System;
using System.Windows.Forms;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Controls
{
    public partial class VoucherItemControl : UserControl
    {
        private Discount? _discount;
        private bool _isSelected;
        private decimal _orderAmount;
        private bool _canUse = true;

        public event EventHandler<Discount>? VoucherSelected;

        public Discount? Discount
        {
            get => _discount;
            set
            {
                _discount = value;
                UpdateUI();
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                rbSelect.Checked = value;
                UpdateSelection();
            }
        }

        public decimal OrderAmount
        {
            get => _orderAmount;
            set
            {
                _orderAmount = value;
                UpdateUI();
            }
        }

        // Property để sort voucher
        public bool CanUse => _canUse;

        public VoucherItemControl()
        {
            InitializeComponent();
            SetupStyle();
        }

        private void SetupStyle()
        {
            // Click vào panel cũng chọn được voucher
            panelMain.Click += panelMain_Click;
            panelLeft.Click += panelMain_Click;
            panelRight.Click += panelMain_Click;
            lblDiscount.Click += panelMain_Click;
            lblMinOrder.Click += panelMain_Click;
            lblExpiry.Click += panelMain_Click;
            lblExpiryInfo.Click += panelMain_Click;
            lblFreeShip.Click += panelMain_Click;
            
            panelMain.MouseEnter += (s, e) =>
            {
                if (_canUse && !_isSelected)
                    panelMain.BackColor = Color.FromArgb(250, 250, 250);
            };

            panelMain.MouseLeave += (s, e) =>
            {
                if (_canUse && !_isSelected)
                    panelMain.BackColor = Color.White;
                else if (!_canUse)
                    panelMain.BackColor = Color.FromArgb(240, 240, 240);
            };
        }

        private void UpdateUI()
        {
            if (_discount == null) return;

            // Cập nhật thông tin giảm giá
            if (_discount.DiscountType == "Percentage")
            {
                lblDiscount.Text = $"Max discount {_discount.DiscountValue}%";
                if (_discount.MaxDiscountAmount.HasValue)
                {
                    lblDiscount.Text = $"Discount {_discount.DiscountValue}% up to {FormatMoney(_discount.MaxDiscountAmount.Value)}";
                }
            }
            else
            {
                lblDiscount.Text = $"Discount {FormatMoney(_discount.DiscountValue)}";
            }

            // Đơn tối thiểu
            if (_discount.MinOrderAmount.HasValue && _discount.MinOrderAmount.Value > 0)
            {
                lblMinOrder.Text = $"Min Order {FormatMoney(_discount.MinOrderAmount.Value)}";
            }
            else
            {
                lblMinOrder.Text = "Min Order 0₫";
            }

            // Hạn sử dụng
            lblExpiry.Text = $"HSD: {_discount.EndDate:dd/MM/yyyy}";

            // Số lượng còn lại
            if (_discount.UsageLimit.HasValue)
            {
                var remaining = _discount.UsageLimit.Value - _discount.UsageCount;
                lblQuantity.Text = $"x {remaining}";
                lblQuantity.Visible = true;
            }
            else
            {
                lblQuantity.Visible = false;
            }

            // Thông tin đã dùng
            if (_discount.UsageLimit.HasValue)
            {
                var percentage = (_discount.UsageCount * 100.0) / _discount.UsageLimit.Value;
                lblExpiryInfo.Text = $"Used {percentage:F0}%";
            }
            else
            {
                lblExpiryInfo.Text = $"Used {_discount.UsageCount} times";
            }

            // Kiểm tra điều kiện
            UpdateConditionMessage();

            // Update panel màu
            UpdatePanelColor();
        }

        private void UpdateConditionMessage()
        {
            if (_discount == null) return;

            _canUse = true;
            var message = "";

            // Kiểm tra đơn tối thiểu
            if (_discount.MinOrderAmount.HasValue && _orderAmount < _discount.MinOrderAmount.Value)
            {
                _canUse = false;
                message = $"⚠️ Minimum order {FormatMoney(_discount.MinOrderAmount.Value)} to apply this Voucher";
            }

            // Kiểm tra hết lượt
            if (_discount.UsageLimit.HasValue && _discount.UsageCount >= _discount.UsageLimit.Value)
            {
                _canUse = false;
                message = "⚠️ Voucher has run out of uses";
            }

            // Kiểm tra hết hạn
            if (DateTime.Now > _discount.EndDate)
            {
                _canUse = false;
                message = "⚠️ Voucher has expired";
            }

            if (_canUse)
            {
                lblConditions.Visible = false;
                rbSelect.Enabled = true;
                panelMain.Cursor = Cursors.Hand;
                
                // Màu bình thường
                panelMain.BackColor = Color.White;
                lblDiscount.ForeColor = Color.Black;
                lblMinOrder.ForeColor = Color.FromArgb(100, 100, 100);
                lblExpiry.ForeColor = Color.FromArgb(100, 100, 100);
                lblExpiryInfo.ForeColor = Color.FromArgb(100, 100, 100);
            }
            else
            {
                lblConditions.Text = message;
                lblConditions.Visible = true;
                lblConditions.ForeColor = Color.FromArgb(238, 77, 45);
                rbSelect.Enabled = false;
                panelMain.Cursor = Cursors.Default;
                
                // Màu xám cho voucher không dùng được
                panelMain.BackColor = Color.FromArgb(240, 240, 240);
                lblDiscount.ForeColor = Color.FromArgb(150, 150, 150);
                lblMinOrder.ForeColor = Color.FromArgb(170, 170, 170);
                lblExpiry.ForeColor = Color.FromArgb(170, 170, 170);
                lblExpiryInfo.ForeColor = Color.FromArgb(170, 170, 170);
                lblFreeShip.ForeColor = Color.FromArgb(180, 180, 180);
                
                // Panel bên trái cũng mờ đi
                panelLeft.BackColor = Color.FromArgb(200, 200, 200);
            }
        }

        private void UpdatePanelColor()
        {
            if (_discount == null) return;

            if (_canUse)
            {
                // Thay đổi màu panel bên trái theo loại voucher (nếu có thể dùng)
                if (_discount.DiscountType == "Percentage")
                {
                    panelLeft.BackColor = Color.FromArgb(0, 174, 173); // Xanh lam
                    lblFreeShip.Text = $"{_discount.DiscountValue}%\r";
                    lblFreeShip.ForeColor = Color.White;
                }
                else
                {
                    panelLeft.BackColor = Color.FromArgb(238, 77, 45); // Đỏ cam
                    var value = _discount.DiscountValue / 1000;
                    lblFreeShip.Text = $"{value}K\r";
                    lblFreeShip.ForeColor = Color.White;
                }
            }
            else
            {
                // Màu xám nếu không dùng được
                panelLeft.BackColor = Color.FromArgb(200, 200, 200);
                lblFreeShip.ForeColor = Color.FromArgb(150, 150, 150);
                
                if (_discount.DiscountType == "Percentage")
                {
                    lblFreeShip.Text = $"{_discount.DiscountValue}%\r";
                }
                else
                {
                    var value = _discount.DiscountValue / 1000;
                    lblFreeShip.Text = $"{value}K\r";
                }
            }
        }

        private void UpdateSelection()
        {
            if (_isSelected && _canUse)
            {
                panelMain.BackColor = Color.FromArgb(255, 244, 229);
                panelMain.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (_canUse)
            {
                panelMain.BackColor = Color.White;
                panelMain.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private string FormatMoney(decimal amount)
        {
            if (amount >= 1000000)
            {
                return $"{amount / 1000000:F1}tr";
            }
            else if (amount >= 1000)
            {
                return $"{amount / 1000:F0}k";
            }
            return $"{amount:F0}₫";
        }

        private void panelMain_Click(object sender, EventArgs e)
        {
            if (rbSelect.Enabled && _canUse)
            {
                rbSelect.Checked = true;
            }
        }

        private void rbSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSelect.Checked && _canUse)
            {
                IsSelected = true;
                VoucherSelected?.Invoke(this, _discount!);
            }
        }
    }
}
