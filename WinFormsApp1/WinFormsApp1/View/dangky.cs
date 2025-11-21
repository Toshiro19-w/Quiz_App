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
            txtPhone.TextChanged += ValidatePhone;
        }

        private void ValidateFullName(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = ValidationHelper.IsValidFullName(textBox.Text);
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblFullNameError.Text = isValid ? "" : "Họ tên phải có ít nhất 2 ký tự";
            lblFullNameError.Visible = !isValid;
            if (!isValid) lblFullNameError.BringToFront();
        }

        private void ValidateUsername(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = ValidationHelper.IsValidUsername(textBox.Text);
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblUsernameError.Text = isValid ? "" : "Username phải có ít nhất 3 ký tự và chỉ chứa chữ, số, _";
            lblUsernameError.Visible = !isValid;
            if (!isValid) lblUsernameError.BringToFront();

            // Clear duplicate message when editing
            if (!string.IsNullOrWhiteSpace(lblUsernameError.Text) && isValid)
            {
                lblUsernameError.Text = "";
                lblUsernameError.Visible = false;
            }
        }

        private void ValidateEmail(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = ValidationHelper.IsValidEmail(textBox.Text);
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblEmailError.Text = isValid ? "" : "Email không hợp lệ";
            lblEmailError.Visible = !isValid;
            if (!isValid) lblEmailError.BringToFront();

            if (!string.IsNullOrWhiteSpace(lblEmailError.Text) && isValid)
            {
                lblEmailError.Text = "";
                lblEmailError.Visible = false;
            }
        }

        private void ValidatePassword(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = ValidationHelper.IsValidPassword(textBox.Text);
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblPasswordError.Text = isValid ? "" : "Mật khẩu tối thiểu 6 ký tự, phải có chữ hoa, chữ thường, số và ký tự đặc biệt";
            lblPasswordError.Visible = !isValid;
            if (!isValid) lblPasswordError.BringToFront();
        }

        private void ValidateConfirmPassword(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = textBox.Text == txtPassword.Text && ValidationHelper.IsValidPassword(textBox.Text);
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblConfirmPasswordError.Text = isValid ? "" : "Mật khẩu xác nhận không khớp hoặc không đủ mạnh";
            lblConfirmPasswordError.Visible = !isValid;
            if (!isValid) lblConfirmPasswordError.BringToFront();
        }

        private void ValidatePhone(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            bool isValid = ValidationHelper.IsValidPhone(textBox.Text);
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblPhoneError.Text = isValid ? "" : "Số điện thoại phải có đúng 10 chữ số";
            lblPhoneError.Visible = !isValid;
            if (!isValid)
            {
                lblPhoneError.BringToFront();
            }
            else
            {
                // clear duplicate phone label when corrected
                lblPhoneError.Text = "";
                lblPhoneError.Visible = false;
            }
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
                    // Determine reason (duplicate email, username or phone) and show label errors
                    using var ctx = new LearningPlatformContext();
                    var emailExists = ctx.Users.Any(u => u.Email == txtEmail.Text.Trim());
                    var usernameExists = ctx.Users.Any(u => u.Username == txtUsername.Text.Trim());
                    var phoneExists = !string.IsNullOrWhiteSpace(txtPhone.Text) && ctx.Users.Any(u => u.Phone == txtPhone.Text.Trim());

                    if (emailExists)
                    {
                        lblEmailError.Text = "Email đã được sử dụng";
                        lblEmailError.Visible = true;
                        lblEmailError.BringToFront();
                    }
                    if (usernameExists)
                    {
                        lblUsernameError.Text = "Tên đăng nhập đã tồn tại";
                        lblUsernameError.Visible = true;
                        lblUsernameError.BringToFront();
                    }
                    if (phoneExists)
                    {
                        lblPhoneError.Text = "Số điện thoại đã được sử dụng";
                        lblPhoneError.Visible = true;
                        lblPhoneError.BringToFront();
                    }

                    // If no specific reason, show generic message
                    if (!emailExists && !usernameExists && !phoneExists)
                    {
                        ToastHelper.Show(this, "Đăng ký thất bại! Vui lòng thử lại.");
                    }
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
            // Clear previous duplicate labels
            lblEmailError.Text = "";
            lblEmailError.Visible = false;
            lblUsernameError.Text = "";
            lblUsernameError.Visible = false;
            lblPhoneError.Text = "";
            lblPhoneError.Visible = false;

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

            if (string.IsNullOrWhiteSpace(txtPassword.Text) || !ValidationHelper.IsValidPassword(txtPassword.Text))
            {
                ToastHelper.Show(this, "Mật khẩu phải tối thiểu 6 ký tự và bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt!");
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                ToastHelper.Show(this, "Mật khẩu xác nhận không khớp!");
                txtConfirmPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text) || !ValidationHelper.IsValidPhone(txtPhone.Text))
            {
                lblPhoneError.Text = "Số điện thoại phải có đúng 10 chữ số!";
                lblPhoneError.Visible = true;
                lblPhoneError.BringToFront();
                txtPhone.Focus();
                return false;
            }

            // Check duplicates in database
            using (var ctx = new LearningPlatformContext())
            {
                if (ctx.Users.Any(u => u.Username == txtUsername.Text.Trim()))
                {
                    lblUsernameError.Text = "Tên đăng nhập đã tồn tại";
                    lblUsernameError.Visible = true;
                    lblUsernameError.BringToFront();
                    txtUsername.Focus();
                    return false;
                }

                if (ctx.Users.Any(u => u.Email == txtEmail.Text.Trim()))
                {
                    lblEmailError.Text = "Email đã được sử dụng";
                    lblEmailError.Visible = true;
                    lblEmailError.BringToFront();
                    txtEmail.Focus();
                    return false;
                }

                if (ctx.Users.Any(u => u.Phone == txtPhone.Text.Trim()))
                {
                    lblPhoneError.Text = "Số điện thoại đã được sử dụng";
                    lblPhoneError.Visible = true;
                    lblPhoneError.BringToFront();
                    txtPhone.Focus();
                    return false;
                }
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