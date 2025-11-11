using System;
using System.Drawing;
using WinFormsApp1.View.Admin;
using WinFormsApp1.View.User;
using WinFormsApp1.Helpers;
using WinFormsApp1.View;
using formHome = WinFormsApp1.View.User.formHome;

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
            MessageBox.Show("Chức năng quên mật khẩu đang được phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = textTK.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AuthHelper.Login(email, password))
            {
                this.Hide();
                
                if (AuthHelper.IsAdmin())
                {
                    var adminDashboard = new AdminDashboard();
                    adminDashboard.FormClosed += (s, args) => this.Close();
                    adminDashboard.Show();
                }
                else
                {
                    var userDashboard = new MainContainer();
                    userDashboard.FormClosed += (s, args) => this.Close();
                    userDashboard.Show();
                }
            }
            else
            {
                MessageBox.Show("Email hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
