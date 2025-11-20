using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Service.PaymentService;

namespace WinFormsApp1.View.Dialogs
{
    public class PaymentQRDialog : Form
    {
        // Import DLL bo tròn
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private Label lblCourseTitle;
        private Label lblAmount;
        private PictureBox picQRCode;
        private Label lblTimer;
        private Button btnConfirmPayment;
        private Button btnCancel;
        private System.Windows.Forms.Timer countdownTimer;
        private int timeRemaining = 300; // 5 phút

        public decimal Amount { get; set; }
        public string CourseTitle { get; set; }
        public int CourseId { get; set; }
        private bool _isCartPayment;

        public PaymentQRDialog(string courseTitle, decimal amount, int courseId, bool isCartPayment = false)
        {
            CourseTitle = courseTitle;
            Amount = amount;
            CourseId = courseId;
            _isCartPayment = isCartPayment;
            InitializeComponent();
            SetupUI();
            GenerateQRCode();
            StartTimer();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // PaymentQRDialog
            // 
            ClientSize = new Size(815, 1000);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PaymentQRDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thanh toán VietQR";
            ResumeLayout(false);
        }

        private void SetupUI()
        {
            this.BackColor = Color.White;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 25, 25));

            // Header
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.FromArgb(88, 56, 255)
            };

            Label lblTitle = new Label
            {
                Text = "QUÉT MÃ QR ĐỂ THANH TOÁN",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            // Footer
            Panel pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 160,
                BackColor = Color.White,
                Padding = new Padding(0, 15, 0, 0)
            };
            this.Controls.Add(pnlFooter);

            int centerX = this.Width / 2;

            // Confirm button
            btnConfirmPayment = CreateStyledButton("ĐÃ THANH TOÁN",
                Color.FromArgb(40, 167, 69), Color.White);
            btnConfirmPayment.Size = new Size(380, 60);
            btnConfirmPayment.Location = new Point(centerX - 190, 10);
            btnConfirmPayment.Click += BtnConfirmPayment_Click;
            pnlFooter.Controls.Add(btnConfirmPayment);

            // Cancel button
            btnCancel = new Button
            {
                Text = "Hủy bỏ",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DimGray,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 30),
                Location = new Point(centerX - 75, 80),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            pnlFooter.Controls.Add(btnCancel);

            // Body panel
            Panel pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true
            };
            this.Controls.Add(pnlContent);
            pnlContent.BringToFront();

            // ===== FIXED: đẩy xuống tránh che Header =====
            int currentY = 80;

            // Timer
            lblTimer = new Label
            {
                Text = "Hết hạn trong: 05:00",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.Red,
                Size = new Size(pnlContent.Width - 40, 35),
                Location = new Point(20, currentY),
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlContent.Controls.Add(lblTimer);

            currentY += 50;

            // Course Title
            lblCourseTitle = new Label
            {
                Text = CourseTitle,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Size = new Size(pnlContent.Width - 40, 45),
                Location = new Point(20, currentY),
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlContent.Controls.Add(lblCourseTitle);

            currentY += 60;

            // Amount
            lblAmount = new Label
            {
                Text = $"Số tiền: {Amount:N0} VNĐ",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.FromArgb(88, 56, 255),
                Size = new Size(pnlContent.Width - 40, 70),
                Location = new Point(20, currentY),
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlContent.Controls.Add(lblAmount);

            currentY += 90;

            // ===== FIXED: Căn giữa QR theo panel content =====
            int qrSize = 340;
            Panel pnlQRBorder = new Panel
            {
                Size = new Size(qrSize + 2, qrSize + 2),
                Location = new Point((pnlContent.Width - qrSize) / 2 - 10, currentY),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top
            };

            picQRCode = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            pnlQRBorder.Controls.Add(picQRCode);
            pnlContent.Controls.Add(pnlQRBorder);

            currentY += qrSize + 40;

            // Bank info
            Label lblBankInfo = new Label
            {
                Text = $"Ngân hàng: {VietQRConfig.BankName}\nChủ TK: {VietQRConfig.AccountName}\nSTK: {VietQRConfig.AccountNumber}",
                Font = new Font("Segoe UI", 15, FontStyle.Regular),
                ForeColor = Color.DimGray,
                Size = new Size(pnlContent.Width - 40, 100),
                Location = new Point(20, currentY),
                TextAlign = ContentAlignment.TopCenter,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlContent.Controls.Add(lblBankInfo);
        }


        private void StartTimer()
        {
            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += (s, e) =>
            {
                timeRemaining--;
                int minutes = timeRemaining / 60;
                int seconds = timeRemaining % 60;
                lblTimer.Text = $"Hết hạn trong: {minutes:D2}:{seconds:D2}";

                if (timeRemaining <= 60) lblTimer.ForeColor = Color.Red;
                else lblTimer.ForeColor = Color.Black;

                if (timeRemaining <= 0)
                {
                    countdownTimer.Stop();
                    MessageBox.Show("Hết thời gian thanh toán!", "Thông báo");
                    this.DialogResult = DialogResult.Abort;
                    this.Close();
                }
            };
            countdownTimer.Start();
        }

        private void GenerateQRCode()
        {
            try
            {
                int qrCourseId = _isCartPayment ? 0 : CourseId;
                var qrImage = VietQRService.GeneratePaymentQRCode(Amount, CourseTitle, qrCourseId);
                picQRCode.Image = qrImage;
            }
            catch
            {
                Label lblErr = new Label { Text = "Lỗi tạo QR", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter };
                picQRCode.Controls.Add(lblErr);
            }
        }

        private void BtnConfirmPayment_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn đã chuyển khoản thành công chưa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
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
                Font = new Font("Segoe UI", 16, FontStyle.Bold), // Font to hơn
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Paint += (s, e) =>
            {
                Button b = (Button)s;
                IntPtr ptr = CreateRoundRectRgn(0, 0, b.Width, b.Height, 30, 30); // Bo tròn nhiều hơn
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                countdownTimer?.Stop();
                countdownTimer?.Dispose();
                picQRCode?.Image?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}