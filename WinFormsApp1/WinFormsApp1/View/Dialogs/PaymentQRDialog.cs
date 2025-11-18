using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Service.PaymentService;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Dialogs
{
    public class PaymentQRDialog : Form
    {
        private Label lblCourseTitle;
        private Label lblAmount;
        private PictureBox picQRCode;
        private Label lblBankInfo;
        private Label lblAccountName;
        private Label lblAccountNumber;
        private Label lblDescription;
        private Button btnConfirmPayment;
        private Button btnCancel;
        private Label lblTimer;
        private System.Windows.Forms.Timer countdownTimer;
        private Panel panelMain;
        private Panel panelQR;

        public decimal Amount { get; set; }
        public string CourseTitle { get; set; }
        public int CourseId { get; set; }
        public DialogResult PaymentResult { get; set; }

        public PaymentQRDialog(string courseTitle, decimal amount, int courseId)
        {
            CourseTitle = courseTitle;
            Amount = amount;
            CourseId = courseId;
            InitializeComponent();
            SetupUI();
        }

        private void InitializeComponent()
        {
            this.Text = "Thanh toán bằng VietQR";
            this.Size = new Size(500, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9);

            // Setup countdown timer
            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;
        }

        private void SetupUI()
        {
            // Main panel
            panelMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White
            };
            this.Controls.Add(panelMain);

            int yPos = 0;

            // Title
            var lblTitle = new Label
            {
                Text = "Quét mã QR để thanh toán",
                Location = new Point(0, yPos),
                Size = new Size(440, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelMain.Controls.Add(lblTitle);
            yPos += 40;

            // Timer
            lblTimer = new Label
            {
                Text = "Hết hạn trong: 5:00",
                Location = new Point(0, yPos),
                Size = new Size(440, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Red,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelMain.Controls.Add(lblTimer);
            yPos += 35;

            // Course title
            lblCourseTitle = new Label
            {
                Text = CourseTitle,
                Location = new Point(0, yPos),
                Size = new Size(440, 40),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelMain.Controls.Add(lblCourseTitle);
            yPos += 50;

            // Amount
            lblAmount = new Label
            {
                Text = $"Số tiền: {Amount:N0} VNĐ",
                Location = new Point(0, yPos),
                Size = new Size(440, 30),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(88, 56, 255),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelMain.Controls.Add(lblAmount);
            yPos += 40;

            // QR Code panel
            panelQR = new Panel
            {
                Location = new Point(70, yPos),
                Size = new Size(300, 300),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            panelMain.Controls.Add(panelQR);

            // QR Code
            picQRCode = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };
            panelQR.Controls.Add(picQRCode);
            yPos += 320;

            // Bank info section
            var lblBankTitle = new Label
            {
                Text = "Thông tin tài khoản:",
                Location = new Point(0, yPos),
                Size = new Size(440, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelMain.Controls.Add(lblBankTitle);
            yPos += 30;

            // Bank name
            var lblBank = new Label
            {
                Text = $"Ngân hàng: {VietQRConfig.BankName}",
                Location = new Point(0, yPos),
                Size = new Size(440, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            panelMain.Controls.Add(lblBank);
            yPos += 25;

            // Account name
            lblAccountName = new Label
            {
                Text = $"Chủ tài khoản: {VietQRConfig.AccountName}",
                Location = new Point(0, yPos),
                Size = new Size(440, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            panelMain.Controls.Add(lblAccountName);
            yPos += 25;

            // Account number
            lblAccountNumber = new Label
            {
                Text = $"Số tài khoản: {VietQRConfig.AccountNumber}",
                Location = new Point(0, yPos),
                Size = new Size(440, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            panelMain.Controls.Add(lblAccountNumber);
            yPos += 25;

            // Description
            lblDescription = new Label
            {
                Text = $"Nội dung: Thanh toan khoa hoc {CourseId}",
                Location = new Point(0, yPos),
                Size = new Size(440, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            panelMain.Controls.Add(lblDescription);
            yPos += 40;

            // Buttons panel
            var panelButtons = new Panel
            {
                Location = new Point(0, yPos),
                Size = new Size(440, 50)
            };
            panelMain.Controls.Add(panelButtons);

            // Confirm button
            btnConfirmPayment = new Button
            {
                Text = "Đã thanh toán",
                Location = new Point(0, 0),
                Size = new Size(210, 40),
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnConfirmPayment.FlatAppearance.BorderSize = 0;
            btnConfirmPayment.Click += BtnConfirmPayment_Click;
            panelButtons.Controls.Add(btnConfirmPayment);

            // Cancel button
            btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(230, 0),
                Size = new Size(210, 40),
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            panelButtons.Controls.Add(btnCancel);

            // Generate QR code
            GenerateQRCode();

            // Start countdown timer (5 minutes)
            countdownTimer.Start();
        }

        private void GenerateQRCode()
        {
            try
            {
                var qrImage = VietQRService.GeneratePaymentQRCode(Amount, CourseTitle, CourseId);
                picQRCode.Image = qrImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo mã QR: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Show fallback info
                var lblError = new Label
                {
                    Text = "Không thể tạo mã QR\nVui lòng chuyển khoản thủ công",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Red,
                    Font = new Font("Segoe UI", 10)
                };
                panelQR.Controls.Add(lblError);
            }
        }

        private int timeRemaining = 300; // 5 minutes

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            timeRemaining--;

            int minutes = timeRemaining / 60;
            int seconds = timeRemaining % 60;
            lblTimer.Text = $"Hết hạn trong: {minutes}:{seconds:D2}";

            if (timeRemaining <= 60)
                lblTimer.ForeColor = Color.Red;

            if (timeRemaining <= 0)
            {
                countdownTimer.Stop();
                MessageBox.Show("Thời gian thanh toán hết hạn. Vui lòng thử lại.", "Hết hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
        }

        private void BtnConfirmPayment_Click(object sender, EventArgs e)
        {
            countdownTimer.Stop();

            var result = MessageBox.Show(
                "Bạn đã thanh toán thành công chưa?",
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                countdownTimer.Start();
            }
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