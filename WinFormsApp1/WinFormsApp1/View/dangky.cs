using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View
{
    public partial class dangky : Form
    {
        public dangky()
        {
            InitializeComponent();
            SetupModernUI();
            SetupRealTimeValidation();
        }

        private void SetupRealTimeValidation()
        {
            txtFullName.TextChanged += ValidateFullName;
            txtUsername.TextChanged += ValidateUsername;
            txtEmail.TextChanged += ValidateEmail;
            txtPassword.TextChanged += ValidatePassword;
            txtConfirmPassword.TextChanged += ValidateConfirmPassword;
        }

        private void ValidateFullName(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = ValidationHelper.IsValidFullName(textBox.Text);
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblFullNameError.Text = isValid ? "" : "Họ tên phải có ít nhất 2 ký tự";
        }

        private void ValidateUsername(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = ValidationHelper.IsValidUsername(textBox.Text);
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblUsernameError.Text = isValid ? "" : "Username phải có ít nhất 3 ký tự và chỉ chứa chữ, số, _";
        }

        private void ValidateEmail(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = ValidationHelper.IsValidEmail(textBox.Text);
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblEmailError.Text = isValid ? "" : "Email không hợp lệ";
        }

        private void ValidatePassword(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = textBox.Text.Length >= 6;
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblPasswordError.Text = isValid ? "" : "Mật khẩu phải có ít nhất 6 ký tự";
        }

        private void ValidateConfirmPassword(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = textBox.Text == txtPassword.Text && textBox.Text.Length >= 6;
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

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            var btn = sender as Button;
            if (btn != null) btn.Enabled = false;

            try
            {
                bool success = await Task.Run(() => RegisterUser());
                
                if (success)
                {
                    ToastHelper.Show(this, "Đăng ký thành công! Vui lòng đăng nhập.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ToastHelper.Show(this, "Đăng ký thất bại! Vui lòng thử lại.");
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

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                ToastHelper.Show(this, "Vui lòng nhập họ tên!");
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                ToastHelper.Show(this, "Vui lòng nhập tên đăng nhập!");
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !ValidationHelper.IsValidEmail(txtEmail.Text))
            {
                ToastHelper.Show(this, "Vui lòng nhập email hợp lệ!");
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length < 6)
            {
                ToastHelper.Show(this, "Mật khẩu phải có ít nhất 6 ký tự!");
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                ToastHelper.Show(this, "Mật khẩu xác nhận không khớp!");
                txtConfirmPassword.Focus();
                return false;
            }

            return true;
        }

        private bool RegisterUser()
        {
            return AuthHelper.Register(
                txtUsername.Text.Trim(),
                txtEmail.Text.Trim(),
                txtFullName.Text.Trim(),
                txtPassword.Text,
                txtPhone.Text.Trim()
            );
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}