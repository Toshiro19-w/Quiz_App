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
            ToastHelper.Show(this, "Chức năng quên mật khẩu đang được phát triển");
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
                    Hide();

                    if (AuthHelper.IsAdmin())
                    {
                        var adminDashboard = new AdminDashboard();
                        adminDashboard.FormClosed += (s, args) => Close();
                        adminDashboard.Show();
                    }
                    else
                    {
                        var userDashboard = new MainContainer();
                        userDashboard.FormClosed += (s, args) => Close();
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
