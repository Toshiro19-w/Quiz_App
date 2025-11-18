using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Service.PaymentService;
using WinFormsApp1.View.Dialogs;
using WinFormsApp1.Helpers;
using System.Threading.Tasks;

namespace WinFormsApp1.View.User.Forms
{
    public partial class PaymentForm : Form
    {
        private Course _course;
        private decimal _totalAmount;
        
        private Label lblCourseTitle;
        private Label lblCoursePrice;
        private Label lblTotalAmount;
        private Button btnPayWithQR;
        private Button btnCancel;
        private Panel panelCourseInfo;
        private Panel panelPayment;

        public PaymentForm(Course course)
        {
            _course = course;
            _totalAmount = course.Price;
            InitializeComponent();
            SetupUI();
        }

        private void InitializeComponent()
        {
            this.Text = "Thanh toán khóa học";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9);
        }

        private void SetupUI()
        {
            // Main container
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White
            };
            this.Controls.Add(mainPanel);

            int yPos = 0;

            // Title
            var lblTitle = new Label
            {
                Text = "Xác nhận thanh toán",
                Location = new Point(0, yPos),
                Size = new Size(440, 30),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary,
                TextAlign = ContentAlignment.MiddleCenter
            };
            mainPanel.Controls.Add(lblTitle);
            yPos += 50;

            // Course info panel
            panelCourseInfo = new Panel
            {
                Location = new Point(0, yPos),
                Size = new Size(440, 120),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(15)
            };
            mainPanel.Controls.Add(panelCourseInfo);

            // Course title
            lblCourseTitle = new Label
            {
                Text = _course.Title,
                Location = new Point(0, 0),
                Size = new Size(410, 40),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            panelCourseInfo.Controls.Add(lblCourseTitle);

            // Course price
            lblCoursePrice = new Label
            {
                Text = $"Giá: {_course.Price:N0} VNĐ",
                Location = new Point(0, 50),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray
            };
            panelCourseInfo.Controls.Add(lblCoursePrice);

            // Instructor
            var lblInstructor = new Label
            {
                Text = $"Giảng viên: {_course.Owner?.FullName ?? "N/A"}",
                Location = new Point(0, 75),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            panelCourseInfo.Controls.Add(lblInstructor);

            yPos += 140;

            // Payment summary
            var lblPaymentTitle = new Label
            {
                Text = "Tổng kết thanh toán",
                Location = new Point(0, yPos),
                Size = new Size(440, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            mainPanel.Controls.Add(lblPaymentTitle);
            yPos += 35;

            // Total amount
            lblTotalAmount = new Label
            {
                Text = $"Tổng cộng: {_totalAmount:N0} VNĐ",
                Location = new Point(0, yPos),
                Size = new Size(440, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(88, 56, 255),
                TextAlign = ContentAlignment.MiddleRight
            };
            mainPanel.Controls.Add(lblTotalAmount);
            yPos += 50;

            // Payment buttons
            var panelButtons = new Panel
            {
                Location = new Point(0, yPos),
                Size = new Size(440, 50)
            };
            mainPanel.Controls.Add(panelButtons);

            // Pay with QR button
            btnPayWithQR = new Button
            {
                Text = "Thanh toán bằng VietQR",
                Location = new Point(0, 0),
                Size = new Size(210, 40),
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnPayWithQR.FlatAppearance.BorderSize = 0;
            btnPayWithQR.Click += BtnPayWithQR_Click;
            panelButtons.Controls.Add(btnPayWithQR);

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
        }

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

                using (var qrDialog = new PaymentQRDialog(_course.Title, _totalAmount, _course.CourseId))
                {
                    var result = qrDialog.ShowDialog(this);
                    
                    if (result == DialogResult.OK)
                    {
                        // Process payment
                        btnPayWithQR.Enabled = false;
                        btnPayWithQR.Text = "Đang xử lý...";
                        
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
                            MessageBox.Show(
                                $"Lỗi khi xử lý thanh toán: {ex.Message}",
                                "Lỗi thanh toán",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                            
                            btnPayWithQR.Enabled = true;
                            btnPayWithQR.Text = "Thanh toán bằng VietQR";
                        }
                    }
                    else if (result == DialogResult.Abort)
                    {
                        // Payment timeout
                        MessageBox.Show(
                            "Thời gian thanh toán đã hết hạn. Vui lòng thử lại.",
                            "Hết thời gian",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                    // DialogResult.Cancel means user cancelled, do nothing
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi mở thanh toán: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}