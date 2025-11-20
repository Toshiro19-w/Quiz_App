using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Service.PaymentService;
using WinFormsApp1.View.Dialogs;

namespace WinFormsApp1.View.User.Forms
{
    public partial class PaymentForm : Form
    {
        private Course _course;
        private decimal _totalAmount;
        private Button btnPayWithQR;
        private Button btnCancel;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public PaymentForm(Course course)
        {
            _course = course;
            _totalAmount = course.Price;
            InitializeComponent();
            SetupUI_1200x700();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PaymentForm
            // 
            this.ClientSize = new System.Drawing.Size(1200, 700); // Kích thước mới
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thanh toán";
            this.ResumeLayout(false);
        }

        private void SetupUI_1200x700()
        {
            this.BackColor = Color.White;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 25, 25));

            this.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen p = new Pen(Color.LightGray, 2))
                {
                    e.Graphics.DrawPath(p, GetRoundedPath(this.ClientRectangle, 25));
                }
            };

            // --- HEADER ---
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100, // Giảm nhẹ chiều cao header (120 -> 100)
                BackColor = Color.FromArgb(88, 56, 255)
            };

            Label lblTitle = new Label
            {
                Text = "XÁC NHẬN THANH TOÁN",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            // --- CONTENT ---
            Panel pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            this.Controls.Add(pnlContent);
            pnlContent.BringToFront();

            int centerX = this.Width / 2;
            int currentY = 40; // Bắt đầu nội dung cách header 40px

            // 1. Panel Thông tin khóa học
            int cardWidth = 900; // Mở rộng chiều ngang
            int cardHeight = 180; // Giảm chiều cao card chút xíu

            Panel pnlCourseInfo = new Panel
            {
                Location = new Point(centerX - (cardWidth / 2), currentY),
                Size = new Size(cardWidth, cardHeight),
                BackColor = Color.FromArgb(248, 249, 250)
            };

            pnlCourseInfo.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen p = new Pen(Color.FromArgb(220, 220, 220), 1))
                using (var path = GetRoundedPath(pnlCourseInfo.ClientRectangle, 20))
                {
                    e.Graphics.FillPath(new SolidBrush(Color.FromArgb(248, 249, 250)), path);
                    e.Graphics.DrawPath(p, path);
                }
            };

            Label lblCourseName = new Label
            {
                Text = _course.Title.ToUpper(),
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(20, 25),
                Size = new Size(cardWidth - 40, 80),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblInstructor = new Label
            {
                Text = $"Giảng viên: {_course.Owner?.FullName ?? "N/A"}",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = Color.Gray,
                Location = new Point(20, 110),
                Size = new Size(cardWidth - 40, 30),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlCourseInfo.Controls.Add(lblCourseName);
            pnlCourseInfo.Controls.Add(lblInstructor);
            pnlContent.Controls.Add(pnlCourseInfo);

            // Cập nhật Y: Khoảng cách từ Card xuống Giá tiền
            currentY += cardHeight + 40;

            // 2. Label "Tổng số tiền"
            Label lblTotalLabel = new Label
            {
                Text = "Tổng số tiền thanh toán",
                Font = new Font("Segoe UI", 13, FontStyle.Regular),
                ForeColor = Color.Gray,
                Location = new Point(0, currentY),
                Size = new Size(this.Width, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlContent.Controls.Add(lblTotalLabel);

            currentY += 35;

            // 3. GIÁ TIỀN (Sửa lỗi bị che)
            Label lblPrice = new Label
            {
                Text = $"{_totalAmount:N0} VNĐ",
                Font = new Font("Segoe UI", 50, FontStyle.Bold),
                ForeColor = Color.FromArgb(88, 56, 255),
                Location = new Point(0, currentY),
                Size = new Size(this.Width, 110), // TĂNG CHIỀU CAO LÊN 110px ĐỂ KHÔNG BỊ CẮT CHỮ
                TextAlign = ContentAlignment.MiddleCenter,
                // AutoSize = false (Đã set false mặc định khi set Size)
            };
            pnlContent.Controls.Add(lblPrice);

            currentY += 120; // Khoảng cách xuống nút

            // 4. Nút Thanh toán
            int btnWidth = 450;
            int btnHeight = 70;

            btnPayWithQR = CreateStyledButton("THANH TOÁN QUA VIETQR", Color.FromArgb(40, 167, 69), Color.White);
            btnPayWithQR.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            btnPayWithQR.Location = new Point(centerX - (btnWidth / 2), currentY);
            btnPayWithQR.Size = new Size(btnWidth, btnHeight);
            btnPayWithQR.Click += BtnPayWithQR_Click;
            pnlContent.Controls.Add(btnPayWithQR);

            currentY += btnHeight + 20;

            // 5. Nút Quay lại
            btnCancel = new Button
            {
                Text = "Quay lại",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Gray,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(centerX - 100, currentY),
                Size = new Size(200, 35),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnCancel.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            pnlContent.Controls.Add(btnCancel);
        }

        // --- LOGIC XỬ LÝ (GIỮ NGUYÊN) ---
        private async void BtnPayWithQR_Click(object sender, EventArgs e)
        {
            try
            {
                var currentUser = AuthHelper.CurrentUser;
                if (currentUser == null)
                {
                    MessageBox.Show("Vui lòng đăng nhập để thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Hide();

                using (var qrDialog = new PaymentQRDialog(_course.Title, _totalAmount, _course.CourseId))
                {
                    var result = qrDialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        this.Show();
                        btnPayWithQR.Enabled = false;
                        btnPayWithQR.Text = "Đang xử lý...";
                        btnPayWithQR.BackColor = Color.Gray;

                        try
                        {
                            await SimplePaymentService.ProcessCoursePaymentAsync(
                                _course.CourseId,
                                currentUser.UserId,
                                _totalAmount
                            );

                            MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            btnPayWithQR.Enabled = true;
                            btnPayWithQR.Text = "THANH TOÁN QUA VIETQR";
                            btnPayWithQR.BackColor = Color.FromArgb(40, 167, 69);
                        }
                    }
                    else
                    {
                        this.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Show();
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- HELPER UI ---
        private Button CreateStyledButton(string text, Color backColor, Color foreColor)
        {
            var btn = new Button
            {
                Text = text,
                BackColor = backColor,
                ForeColor = foreColor,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Paint += (s, e) =>
            {
                Button b = (Button)s;
                IntPtr ptr = CreateRoundRectRgn(0, 0, b.Width, b.Height, 20, 20);
                b.Region = Region.FromHrgn(ptr);
            };
            return btn;
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}