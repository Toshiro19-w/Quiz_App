using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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

        // UI Components
        private Button btnPayWithQR;
        private Button btnCancel;

        // Import DLL để bo tròn nút/panel
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public PaymentForm(Course course)
        {
            _course = course;
            _totalAmount = course.Price;
            InitializeComponent();
            SetupModernUI(); // Gọi hàm UI mới
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PaymentForm
            // 
            this.ClientSize = new System.Drawing.Size(500, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; // Tắt viền mặc định của Windows để tự vẽ
            this.Name = "PaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thanh toán";
            this.Load += new System.EventHandler(this.PaymentForm_Load);
            this.ResumeLayout(false);
        }

        private void SetupModernUI()
        {
            // 1. Cấu hình Form chính
            this.BackColor = Color.White;
            // Bo tròn Form
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));

            // Viền mỏng xung quanh form (để tách biệt với nền desktop)
            this.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen p = new Pen(Color.LightGray, 1))
                {
                    e.Graphics.DrawPath(p, GetRoundedPath(this.ClientRectangle, 20));
                }
            };

            // --- HEADER ---
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(88, 56, 255) // Màu tím thương hiệu
            };

            Label lblTitle = new Label
            {
                Text = "XÁC NHẬN THANH TOÁN",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            // --- CONTENT PANEL (Chứa thông tin) ---
            Panel pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30) // Lề rộng cho thoáng
            };
            this.Controls.Add(pnlContent);
            pnlContent.BringToFront(); // Đảm bảo nằm dưới Header

            int yPos = 20;

            // 1. Thông tin khóa học (Card)
            Panel pnlCourseInfo = new Panel
            {
                Location = new Point(30, yPos),
                Size = new Size(440, 100),
                BackColor = Color.FromArgb(248, 249, 250) // Màu xám rất nhạt
            };
            // Bo tròn panel thông tin
            pnlCourseInfo.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen p = new Pen(Color.FromArgb(230, 230, 230), 1))
                {
                    using (var path = GetRoundedPath(pnlCourseInfo.ClientRectangle, 15))
                    {
                        e.Graphics.FillPath(new SolidBrush(Color.FromArgb(248, 249, 250)), path);
                        e.Graphics.DrawPath(p, path);
                    }
                }
            };

            // Tên khóa học
            Label lblCourseName = new Label
            {
                Text = _course.Title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(15, 15),
                Size = new Size(410, 50), // Cho phép xuống dòng nếu tên dài
                BackColor = Color.Transparent
            };

            // Giảng viên
            Label lblInstructor = new Label
            {
                Text = $"Giảng viên: {_course.Owner?.FullName ?? "N/A"}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.Gray,
                Location = new Point(15, 65),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            pnlCourseInfo.Controls.Add(lblCourseName);
            pnlCourseInfo.Controls.Add(lblInstructor);
            pnlContent.Controls.Add(pnlCourseInfo);

            yPos += 120; // Dịch xuống

            // 2. Phần giá tiền (Nổi bật nhất)
            Label lblTotalLabel = new Label
            {
                Text = "Tổng thanh toán",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.Gray,
                Location = new Point(0, yPos),
                Size = new Size(500, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlContent.Controls.Add(lblTotalLabel);

            yPos += 25;

            Label lblPrice = new Label
            {
                Text = $"{_totalAmount:N0} đ",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.FromArgb(88, 56, 255), // Màu tím
                Location = new Point(0, yPos),
                Size = new Size(500, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlContent.Controls.Add(lblPrice);

            // --- FOOTER BUTTONS ---

            // Nút Thanh toán
            btnPayWithQR = CreateStyledButton("Thanh toán qua VietQR", Color.FromArgb(40, 167, 69), Color.White);
            btnPayWithQR.Location = new Point(50, 300);
            btnPayWithQR.Size = new Size(400, 50);
            btnPayWithQR.Click += BtnPayWithQR_Click;
            this.Controls.Add(btnPayWithQR);
            btnPayWithQR.BringToFront();

            // Nút Hủy
            btnCancel = new Button
            {
                Text = "Quay lại",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Gray,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(150, 360),
                Size = new Size(200, 30),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnCancel.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancel);
            btnCancel.BringToFront();
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

                // Ẩn form hiện tại đi một chút hoặc hiện loading
                this.Hide();

                using (var qrDialog = new PaymentQRDialog(_course.Title, _totalAmount, _course.CourseId))
                {
                    var result = qrDialog.ShowDialog(); // Show dialog độc lập

                    if (result == DialogResult.OK)
                    {
                        // Show lại form để hiển thị trạng thái xử lý (nếu cần)
                        this.Show();
                        btnPayWithQR.Enabled = false;
                        btnPayWithQR.Text = "Đang xử lý giao dịch...";
                        btnPayWithQR.BackColor = Color.Gray;

                        try
                        {
                            await SimplePaymentService.ProcessCoursePaymentAsync(
                                _course.CourseId,
                                currentUser.UserId,
                                _totalAmount
                            );

                            MessageBox.Show(
                                "Thanh toán thành công! Bạn đã có thể truy cập khóa học.",
                                "Thanh toán thành công",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi xử lý thanh toán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            btnPayWithQR.Enabled = true;
                            btnPayWithQR.Text = "Thanh toán qua VietQR";
                            btnPayWithQR.BackColor = Color.FromArgb(40, 167, 69);
                        }
                    }
                    else
                    {
                        // Nếu hủy ở màn hình QR thì hiện lại form này
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

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            // Animation hoặc logic load nếu cần
        }

        // --- CÁC HÀM HỖ TRỢ UI ---

        private Button CreateStyledButton(string text, Color backColor, Color foreColor)
        {
            var btn = new Button
            {
                Text = text,
                BackColor = backColor,
                ForeColor = foreColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            // Bo tròn nút
            btn.Paint += (s, e) =>
            {
                Button b = (Button)s;
                IntPtr ptr = CreateRoundRectRgn(0, 0, b.Width, b.Height, 15, 15);
                b.Region = Region.FromHrgn(ptr);
                // DeleteObject(ptr); // Trong C# managed code thường ko cần thiết phải gọi delete object gdi thủ công cho region nhỏ
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