using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using System.Linq;

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
            var user = context.Users.FirstOrDefault(u => u.Email == email && u.Status == 1);
            
            if (user == null) return false;

            // Tạo token đơn giản (trong thực tế nên dùng GUID hoặc mã hóa phức tạp hơn)
            string token = new Random().Next(100000, 999999).ToString();
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiry = DateTime.Now.AddMinutes(15); // Token hết hạn sau 15 phút

            context.SaveChanges();

            // Hiển thị token cho demo (trong thực tế sẽ gửi qua email)
            this.Invoke(() => {
                lblTokenDisplay.Text = $"Mã xác nhận của bạn: {token}";
                lblTokenDisplay.Visible = true;
            });

            return true;
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

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}