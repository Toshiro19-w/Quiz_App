using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.View.Admin;
using WinFormsApp1.View.User;
using WinFormsApp1.Helpers;
using WinFormsApp1.View;

namespace WinFormsApp1
{
    public partial class dangnhap : Form
    {
        public dangnhap()
        {
            InitializeComponent();
            SetupModernUI();
            CheckExistingSession();
            SetupRealTimeValidation();
        }

        private void SetupRealTimeValidation()
        {
            textTK.TextChanged += ValidateEmail;
            textBox2.TextChanged += ValidatePassword;
        }

        private void ValidateEmail(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.BackColor = Color.White;
                lblEmailError.Text = "";
                return;
            }
            
            bool isValid = ValidationHelper.IsValidEmail(textBox.Text);
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblEmailError.Text = isValid ? "" : "Email không hợp lệ";
        }

        private void ValidatePassword(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.BackColor = Color.White;
                lblPasswordError.Text = "";
                return;
            }
            
            bool isValid = textBox.Text.Length >= 6;
            textBox.BackColor = isValid ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
            lblPasswordError.Text = isValid ? "" : "Mật khẩu phải có ít nhất 6 ký tự";
        }

        private void CheckExistingSession()
        {
            var session = SessionHelper.LoadSession();
            if (session != null)
            {
                // Tự động điền thông tin đăng nhập
                textTK.Text = session.Email;
                chkRememberMe.Checked = session.RememberMe;
                ToastHelper.Show(this, $"Chào mừng trở lại, {session.FullName}!");
            }
        }

        private void SetupModernUI()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var forgotPasswordForm = new quenmatkhau();
            forgotPasswordForm.FormClosed += (s, args) => this.Show();
            forgotPasswordForm.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            var registerForm = new dangky();
            registerForm.FormClosed += (s, args) => this.Show();
            registerForm.Show();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string email = textTK.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ToastHelper.Show(this, "Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            var btn = sender as Button;
            if (btn != null) btn.Enabled = false;

            try
            {
                bool success = await Task.Run(() => AuthHelper.Login(email, password));

                if (success)
                {
                    // Lưu session nếu có chọn ghi nhớ
                    if (chkRememberMe.Checked)
                    {
                        SessionHelper.SaveSession(AuthHelper.CurrentUser, true);
                    }

                    Hide();

                    if (AuthHelper.IsAdmin())
                    {
                        var adminDashboard = new AdminDashboard();
                        adminDashboard.FormClosed += (s, args) => {
                            SessionHelper.ClearSession();
                            Close();
                        };
                        adminDashboard.Show();
                    }
                    else
                    {
                        var userDashboard = new MainContainer();
                        userDashboard.FormClosed += (s, args) => {
                            SessionHelper.ClearSession();
                            Close();
                        };
                        userDashboard.Show();
                    }
                }
                else
                {
                    textBox2.Clear();
                    textBox2.Focus();
                    ToastHelper.Show(this, "Email hoặc mật khẩu không chính xác!");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this, "Lỗi khi đăng nhập: " + ex.Message);
            }
            finally
            {
                if (btn != null) btn.Enabled = true;
            }
        }
    }
}
