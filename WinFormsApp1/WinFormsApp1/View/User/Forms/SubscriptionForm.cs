using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.User.Forms
{
    public partial class SubscriptionForm : Form
    {
        private int _selectedMonths = 1;
        private decimal _pricePerMonth = 99000m;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public SubscriptionForm()
        {
            InitializeComponent();
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

            // Apply rounded region to payment button
            btnPayment.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnPayment.Width, btnPayment.Height, 15, 15));
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

            int buttonWidth = 240;
            int buttonHeight = 160;
            int spacing = 20;
            int totalWidth = (buttonWidth * 3) + (spacing * 2);
            int centerX = pnlContent.Width / 2;
            int startX = centerX - (totalWidth / 2);
            
            // Đặt các button dưới label "CHỌN GÓI ĐĂNG KÝ" (label ở y=320, cao 37px)
            int startY = 380; // 320 + 37 + khoảng cách 23px

            btnMonth1 = CreatePlanButton(plans[0].Months, plans[0].Title, plans[0].Price, plans[0].SavePercent, plans[0].Popular);
            btnMonth1.Location = new Point(startX, startY);
            pnlContent.Controls.Add(btnMonth1);

            btnMonth6 = CreatePlanButton(plans[1].Months, plans[1].Title, plans[1].Price, plans[1].SavePercent, plans[1].Popular);
            btnMonth6.Location = new Point(startX + buttonWidth + spacing, startY);
            pnlContent.Controls.Add(btnMonth6);

            btnYear1 = CreatePlanButton(plans[2].Months, plans[2].Title, plans[2].Price, plans[2].SavePercent, plans[2].Popular);
            btnYear1.Location = new Point(startX + (buttonWidth + spacing) * 2, startY);
            pnlContent.Controls.Add(btnYear1);

            // Chọn mặc định gói 1 tháng
            SelectPlan(btnMonth1, 1);
        }

        private Button CreatePlanButton(int months, string title, decimal price, int savePercent, bool isPopular)
        {
            // Label "PHỔ BIẾN" nếu cần
            if (isPopular)
            {
                Label lblPopular = new Label
                {
                    Text = "⭐ PHỔ BIẾN",
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(255, 193, 7),
                    AutoSize = false,
                    Size = new Size(100, 20),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(0, 0) // Sẽ điều chỉnh sau khi button được đặt
                };
                // Note: Label này cần được thêm vào pnlContent với tính toán vị trí chính xác
            }

            Button btn = new Button
            {
                Size = new Size(240, 160),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = months
            };
            btn.FlatAppearance.BorderColor = Color.LightGray;
            btn.FlatAppearance.BorderSize = 2;

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

            return btn;
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
    }
}
