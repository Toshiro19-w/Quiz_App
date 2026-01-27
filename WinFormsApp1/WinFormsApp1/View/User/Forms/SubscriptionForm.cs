using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.User.Forms
{
    public partial class SubscriptionForm : Form
    {
        private int _selectedMonths = 1;
        private decimal _pricePerMonth = 99000m;
        private bool _isRenewal = false;
        private DateTime? _currentExpiryDate = null;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public SubscriptionForm()
        {
            InitializeComponent();
            CheckIfRenewal();
        }

        private void SubscriptionForm_Load(object sender, EventArgs e)
        {
            // Apply rounded corners
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));

            // Apply border paint
            this.Paint += (s, ev) =>
            {
                ev.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen p = new Pen(Color.LightGray, 2))
                {
                    ev.Graphics.DrawPath(p, GetRoundedPath(this.ClientRectangle, 20));
                }
            };

            // Setup benefits list
            SetupBenefitsList();

            // Setup subscription buttons
            SetupSubscriptionButtons();

            // Hiển thị thông tin gia hạn nếu có
            if (_isRenewal && _currentExpiryDate.HasValue)
            {
                ShowRenewalInfo();
            }

            // Apply rounded region to payment button
            btnPayment.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnPayment.Width, btnPayment.Height, 15, 15));
            
            // Wire up payment button click event
            btnPayment.Click += BtnPayment_Click;
        }

        private void CheckIfRenewal()
        {
            var daysRemaining = AuthHelper.GetSubscriptionDaysRemaining();
            if (daysRemaining.HasValue && daysRemaining.Value > 0)
            {
                _isRenewal = true;
                
                // Lấy ngày hết hạn hiện tại
                using var context = new Models.EF.LearningPlatformContext();
                var subscription = context.UserSubscriptions
                    .Where(s => s.UserId == AuthHelper.CurrentUser.UserId 
                        && s.Status == "Active" 
                        && s.ExpiresAt > DateTime.UtcNow)
                    .OrderByDescending(s => s.ExpiresAt)
                    .FirstOrDefault();
                
                if (subscription != null)
                {
                    _currentExpiryDate = subscription.ExpiresAt;
                }
            }
        }

        private void ShowRenewalInfo()
        {
            // Tạo panel thông báo gia hạn - đặt dưới pnlBenefits
            Panel renewalPanel = new Panel
            {
                Name = "renewalPanel",
                Size = new Size(913, 80),
                Location = new Point(57, 270), // Dưới pnlBenefits (30 + 220 + khoảng cách 20)
                BackColor = Color.FromArgb(232, 245, 233),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Icon
            Label iconLabel = new Label
            {
                Text = "🔄",
                Font = new Font("Segoe UI", 18F),
                AutoSize = true,
                Location = new Point(10, 20)
            };

            // Title
            Label titleLabel = new Label
            {
                Text = "GIA HẠN SUBSCRIPTION",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 125, 50),
                AutoSize = true,
                Location = new Point(65, 15)
            };

            // Info
            var daysRemaining = AuthHelper.GetSubscriptionDaysRemaining();
            Label infoLabel = new Label
            {
                Text = $"Gói Premium hiện tại còn {daysRemaining} ngày (đến {_currentExpiryDate:dd/MM/yyyy})",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(46, 125, 50),
                AutoSize = true,
                Location = new Point(65, 45)
            };

            renewalPanel.Controls.Add(iconLabel);
            renewalPanel.Controls.Add(titleLabel);
            renewalPanel.Controls.Add(infoLabel);
            
            pnlContent.Controls.Add(renewalPanel);
            renewalPanel.BringToFront();

            // Cập nhật text của button thanh toán
            if (btnPayment != null)
            {
                btnPayment.Text = "💳 GIA HẠN NGAY";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetupBenefitsList()
        {
            string[] benefits = new string[]
            {
                "✅ Truy cập KHÔNG GIỚI HẠN tất cả khóa học trên hệ thống",
                "✅ Học mọi lúc, mọi nơi trên mọi thiết bị",
                "✅ Cập nhật nội dung mới liên tục",
                "✅ Tải tài liệu và xem offline",
                "✅ Hỗ trợ 24/7 từ đội ngũ giảng viên"
            };

            int yPos = 55;
            foreach (var benefit in benefits)
            {
                Label lblBenefit = new Label
                {
                    Text = benefit,
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.FromArgb(50, 50, 50),
                    AutoSize = true,
                    Location = new Point(40, yPos)
                };
                pnlBenefits.Controls.Add(lblBenefit);
                yPos += 22;
            }
        }

        private void SetupSubscriptionButtons()
        {
            // Thông tin các gói - cập nhật theo bảng SubscriptionPlans
            var plans = new[]
            {
                new { Months = 1, Title = "1 THÁNG", Price = 249000m, SavePercent = 0, Popular = false },
                new { Months = 6, Title = "6 THÁNG", Price = 1349000m, SavePercent = 10, Popular = true },
                new { Months = 12, Title = "1 NĂM", Price = 2390000m, SavePercent = 20, Popular = false }
            };

            // Cập nhật nội dung cho btnMonth1 (đã có trong Designer)
            UpdatePlanButtonContent(btnMonth1, plans[0].Months, plans[0].Title, plans[0].Price, plans[0].SavePercent);
            
            // Cập nhật nội dung cho btnMonth6 (đã có trong Designer)
            UpdatePlanButtonContent(btnMonth6, plans[1].Months, plans[1].Title, plans[1].Price, plans[1].SavePercent);
            
            // Cập nhật nội dung cho btnYear1 (đã có trong Designer)
            UpdatePlanButtonContent(btnYear1, plans[2].Months, plans[2].Title, plans[2].Price, plans[2].SavePercent);

            // Chọn mặc định gói 1 tháng
            SelectPlan(btnMonth1, 1);
        }

        private void UpdatePlanButtonContent(Button btn, int months, string title, decimal price, int savePercent)
        {
            // Clear existing controls
            btn.Controls.Clear();
            
            // Tag để lưu số tháng
            btn.Tag = months;

            // Tiêu đề gói
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 153, 51),
                AutoSize = false,
                Size = new Size(220, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 25),
                BackColor = Color.Transparent
            };
            btn.Controls.Add(lblTitle);

            // Giá
            Label lblPrice = new Label
            {
                Text = $"{price:N0}đ",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                AutoSize = false,
                Size = new Size(220, 35),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 60),
                BackColor = Color.Transparent
            };
            btn.Controls.Add(lblPrice);

            // Giá trung bình/tháng
            decimal avgMonthPrice = price / months;
            Label lblAvgPrice = new Label
            {
                Text = $"~{avgMonthPrice:N0}đ/tháng",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = false,
                Size = new Size(220, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 95),
                BackColor = Color.Transparent
            };
            btn.Controls.Add(lblAvgPrice);

            // Label tiết kiệm
            if (savePercent > 0)
            {
                Label lblSave = new Label
                {
                    Text = $"🎉 Tiết kiệm {savePercent}%",
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.FromArgb(40, 167, 69),
                    AutoSize = false,
                    Size = new Size(220, 25),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(10, 120),
                    BackColor = Color.Transparent
                };
                btn.Controls.Add(lblSave);
            }

            btn.Click += (s, e) => SelectPlan(btn, months);
        }

        private void SelectPlan(Button selectedButton, int months)
        {
            _selectedMonths = months;

            // Reset tất cả buttons
            ResetButtonStyle(btnMonth1);
            ResetButtonStyle(btnMonth6);
            ResetButtonStyle(btnYear1);

            // Highlight button được chọn
            selectedButton.BackColor = Color.FromArgb(255, 248, 240);
            selectedButton.FlatAppearance.BorderColor = Color.FromArgb(255, 153, 51);
            selectedButton.FlatAppearance.BorderSize = 3;

            // Hiển thị thông tin ngày hết hạn mới nếu là gia hạn
            if (_isRenewal && _currentExpiryDate.HasValue)
            {
                ShowNewExpiryDatePreview(months);
            }
        }

        private void ShowNewExpiryDatePreview(int months)
        {
            // Xóa preview cũ nếu có
            var oldPreview = pnlContent.Controls.Find("previewPanel", false);
            foreach (Control ctrl in oldPreview)
            {
                pnlContent.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            // Tính ngày hết hạn mới
            DateTime newExpiryDate = _currentExpiryDate.Value.AddMonths(months);

            // Tạo panel preview - đặt dưới các button plans
            Panel previewPanel = new Panel
            {
                Name = "previewPanel",
                Size = new Size(770, 50),
                Location = new Point(70, 540), // Dưới buttons (360 + 160 + khoảng cách 20)
                BackColor = Color.FromArgb(255, 248, 225),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblPreview = new Label
            {
                Text = "📅 Sau khi gia hạn, gói Premium của bạn sẽ có hiệu lực đến:",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(117, 117, 117),
                AutoSize = true,
                Location = new Point(15, 8)
            };

            Label lblNewDate = new Label
            {
                Text = newExpiryDate.ToString("dd/MM/yyyy HH:mm"),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 153, 51),
                AutoSize = true,
                Location = new Point(15, 26)
            };

            previewPanel.Controls.Add(lblPreview);
            previewPanel.Controls.Add(lblNewDate);
            
            pnlContent.Controls.Add(previewPanel);
            previewPanel.BringToFront();

            // Đảm bảo btnPayment không bị che
            btnPayment.Location = new Point(314, 610);
        }

        private void ResetButtonStyle(Button btn)
        {
            if (btn != null)
            {
                btn.BackColor = Color.White;
                btn.FlatAppearance.BorderColor = Color.LightGray;
                btn.FlatAppearance.BorderSize = 2;
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float r2 = radius / 2f;
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        public int SelectedMonths => _selectedMonths;
        public decimal TotalAmount => _selectedMonths switch
        {
            1 => 249000m,
            6 => 1349000m,
            12 => 2390000m,
            _ => 249000m
        };

        private async void BtnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                var currentUser = AuthHelper.CurrentUser;
                if (currentUser == null)
                {
                    MessageBox.Show("Vui lòng đăng nhập để đăng ký gói Premium", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var amount = TotalAmount;
                var planName = _selectedMonths switch
                {
                    1 => "1 tháng",
                    6 => "6 tháng",
                    12 => "1 năm",
                    _ => $"{_selectedMonths} tháng"
                };

                // Xác nhận thanh toán với thông báo phù hợp
                string confirmMessage = _isRenewal 
                    ? $"gói Premium {planName} (Gia hạn)"
                    : $"gói Premium {planName}";
                
                if (!MoMoPaymentHelper.ConfirmPayment(amount, confirmMessage))
                    return;

                // Gọi helper để thanh toán (sẽ mở form và browser)
                var success = await MoMoPaymentHelper.PaySubscriptionAsync(
                    currentUser.UserId, 
                    _selectedMonths, 
                    this
                );

                if (success)
                {
                    // Thông báo thành công với nội dung phù hợp
                    string successMessage;
                    if (_isRenewal && _currentExpiryDate.HasValue)
                    {
                        DateTime newExpiryDate = _currentExpiryDate.Value.AddMonths(_selectedMonths);
                        successMessage = $"Gia hạn Premium thành công!\n\n" +
                                       $"Gói Premium của bạn đã được gia hạn {planName}.\n" +
                                       $"Hiệu lực mới đến: {newExpiryDate:dd/MM/yyyy HH:mm}\n\n" +
                                       $"Bạn tiếp tục có quyền truy cập không giới hạn tất cả khóa học.";
                    }
                    else
                    {
                        successMessage = "Đăng ký Premium thành công!\n\n" +
                                       "Bạn đã có quyền truy cập không giới hạn tất cả khóa học.";
                    }

                    MessageBox.Show(successMessage, "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thanh toán chưa hoàn tất hoặc đã bị hủy.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
