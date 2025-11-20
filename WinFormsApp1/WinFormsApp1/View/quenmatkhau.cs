using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;


namespace WinFormsApp1.View
{
    public partial class quenmatkhau : Form
    {
        public quenmatkhau()
        {
            InitializeComponent();
            SetupModernUI();
            SetupRealTimeValidation();
        }

        private void SetupRealTimeValidation()
        {
            txtEmail.TextChanged += ValidateEmail;
            txtNewPassword.TextChanged += ValidateNewPassword;
            txtConfirmNewPassword.TextChanged += ValidateConfirmPassword;
        }

        private void ValidateEmail(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = ValidationHelper.IsValidEmail(textBox.Text);
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblEmailError.Text = isValid ? "" : "Email không hợp lệ";
        }

        private void ValidateNewPassword(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = textBox.Text.Length >= 6;
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblPasswordError.Text = isValid ? "" : "Mật khẩu phải có ít nhất 6 ký tự";
        }

        private void ValidateConfirmPassword(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = textBox.Text == txtNewPassword.Text && textBox.Text.Length >= 6;
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblConfirmPasswordError.Text = isValid ? "" : "Mật khẩu xác nhận không khớp";
        }

        private void SetupModernUI()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private async void btnSendReset_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email) || !ValidationHelper.IsValidEmail(email))
            {
                ToastHelper.Show(this, "Vui lòng nhập email hợp lệ!");
                return;
            }

            var btn = sender as Button;
            if (btn != null) btn.Enabled = false;

            try
            {
                bool success = await Task.Run(() => SendResetToken(email));

                if (success)
                {
                    ToastHelper.Show(this, "Mã đặt lại mật khẩu đã được tạo! Vui lòng nhập mã để tiếp tục.");
                    pnlResetToken.Visible = true;
                    txtResetToken.Focus();
                }
                else
                {
                    ToastHelper.Show(this, "Email không tồn tại trong hệ thống!");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this, "Lỗi: " + ex.Message);
            }
            finally
            {
                if (btn != null) btn.Enabled = true;
            }
        }

        private async void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (!ValidateResetInput()) return;

            var btn = sender as Button;
            if (btn != null) btn.Enabled = false;

            try
            {
                bool success = await Task.Run(() => ResetPassword());

                if (success)
                {
                    ToastHelper.Show(this, "Đặt lại mật khẩu thành công! Vui lòng đăng nhập.");
                    this.Close();
                }
                else
                {
                    ToastHelper.Show(this, "Mã xác nhận không đúng hoặc đã hết hạn!");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this, "Lỗi: " + ex.Message);
            }
            finally
            {
                if (btn != null) btn.Enabled = true;
            }
        }

        private bool ValidateResetInput()
        {
            if (string.IsNullOrWhiteSpace(txtResetToken.Text))
            {
                ToastHelper.Show(this, "Vui lòng nhập mã xác nhận!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text) || txtNewPassword.Text.Length < 6)
            {
                ToastHelper.Show(this, "Mật khẩu mới phải có ít nhất 6 ký tự!");
                return false;
            }

            if (txtNewPassword.Text != txtConfirmNewPassword.Text)
            {
                ToastHelper.Show(this, "Mật khẩu xác nhận không khớp!");
                return false;
            }

            return true;
        }

        private bool SendResetToken(string email)
        {
            using var context = new LearningPlatformContext();
            // Kiểm tra email có tồn tại và đang hoạt động không
            var user = context.Users.FirstOrDefault(u => u.Email == email && u.Status == 1);

            if (user == null) return false;

            // 1. Tạo token ngẫu nhiên
            string token = new Random().Next(100000, 999999).ToString();

            // 2. Lưu token vào Database
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiry = DateTime.Now.AddMinutes(15); // Hết hạn sau 15 phút
            context.SaveChanges();

            // 3. Gửi Email qua Gmail SMTP
            try
            {
                var fromAddress = new MailAddress("daotiendat192005@gmail.com", "LearningPlatform");
                var toAddress = new MailAddress(email);
                // Đây là mật khẩu ứng dụng 16 ký tự bạn lấy ở Bước 1
                const string fromPassword = "qvdp yczq lldb mher";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Mã xác nhận đặt lại mật khẩu - YMEDU",
                    Body = $@"
                <h2>Yêu cầu đặt lại mật khẩu</h2>
                <p>Xin chào <b>{user.FullName}</b>,</p>
                <p>Bạn vừa yêu cầu đặt lại mật khẩu cho tài khoản YMEDU.</p>
                <p>Mã xác nhận của bạn là: <strong style='font-size: 20px; color: blue;'>{token}</strong></p>
                <p>Mã này sẽ hết hạn sau 15 phút.</p>
                <p>Nếu bạn không yêu cầu, vui lòng bỏ qua email này.</p>
                <br>
                <p>Trân trọng,<br>Đội ngũ YMEDU</p>",
                    IsBodyHtml = true // Cho phép dùng HTML để format mail đẹp hơn
                })
                {
                    smtp.Send(message);
                }

                // 4. Ẩn phần hiển thị token trên màn hình (vì đã gửi qua mail rồi)
                // lblTokenDisplay.Text = ...; // Xóa dòng cũ này đi

                return true;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần thiết
                MessageBox.Show("Lỗi gửi mail: " + ex.Message);
                return false;
            }
        }

        private bool ResetPassword()
        {
            using var context = new LearningPlatformContext();
            var user = context.Users.FirstOrDefault(u =>
                u.Email == txtEmail.Text.Trim() &&
                u.PasswordResetToken == txtResetToken.Text.Trim() &&
                u.PasswordResetTokenExpiry > DateTime.Now &&
                u.Status == 1);

            if (user == null) return false;

            user.PasswordHash = PasswordHelper.HashPassword(txtNewPassword.Text);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;

            context.SaveChanges();
            return true;
        }
        private async void btnConfirmReset_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra dữ liệu đầu vào (Validation)
            string token = txtResetToken.Text.Trim();
            string newPass = txtNewPassword.Text.Trim();
            string confirmPass = txtConfirmNewPassword.Text.Trim();

            if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Vui lòng nhập mã xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Khóa nút để tránh spam
            btnConfirmReset.Enabled = false;
            btnConfirmReset.Text = "Đang xử lý...";

            try
            {
                // 3. Xử lý Database
                bool isSuccess = await Task.Run(() =>
                {
                    using (var context = new LearningPlatformContext())
                    {
                        // Tìm user có Email khớp VÀ Token khớp VÀ Token chưa hết hạn
                        var user = context.Users.FirstOrDefault(u =>
                            u.Email == txtEmail.Text.Trim() &&
                            u.PasswordResetToken == token &&
                            u.PasswordResetTokenExpiry > DateTime.Now
                        );

                        if (user != null)
                        {
                            // Cập nhật mật khẩu mới
                            user.PasswordHash = PasswordHelper.HashPassword(newPass); // Nhớ dùng hàm băm mật khẩu của bạn

                            // Xóa token để không dùng lại được nữa
                            user.PasswordResetToken = null;
                            user.PasswordResetTokenExpiry = null;

                            context.SaveChanges();
                            return true;
                        }
                        return false;
                    }
                });

                // 4. Thông báo kết quả
                if (isSuccess)
                {
                    MessageBox.Show("Đổi mật khẩu thành công! Vui lòng đăng nhập lại.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form quên mật khẩu
                }
                else
                {
                    MessageBox.Show("Mã xác nhận không đúng hoặc đã hết hạn!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
            finally
            {
                // Mở lại nút
                btnConfirmReset.Enabled = true;
                btnConfirmReset.Text = "Xác nhận đổi mật khẩu";
            }
        }
        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}