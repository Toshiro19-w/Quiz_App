using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;

namespace WinFormsApp1.View.User.Controls.ProfileTabs
{
    public partial class EditProfileTab : UserControl
    {
        private TextBox txtFullName;
        private TextBox txtUsername;
        private Label lblUsernameNote;
        private TextBox txtPhone;
        private Button btnSave;

        public EditProfileTab()
        {
            InitializeComponent();
            LoadProfileData();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.White;
            this.Size = new Size(760, 550);

            int yPos = 30;

            // Full Name
            var lblFullName = new Label
            {
                Text = "Họ và tên",
                Location = new Point(30, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            this.Controls.Add(lblFullName);
            yPos += 35;

            txtFullName = new TextBox
            {
                Location = new Point(30, yPos),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txtFullName);
            yPos += 60;

            // Username
            var lblUsername = new Label
            {
                Text = "Tên người dùng",
                Location = new Point(30, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            this.Controls.Add(lblUsername);
            yPos += 35;

            txtUsername = new TextBox
            {
                Location = new Point(30, yPos),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true,
                BackColor = Color.FromArgb(245, 245, 245)
            };
            this.Controls.Add(txtUsername);
            yPos += 40;

            lblUsernameNote = new Label
            {
                Text = "Tên người dùng không thể thay đổi",
                Location = new Point(450, yPos - 35),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            this.Controls.Add(lblUsernameNote);
            yPos += 40;

            // Phone
            var lblPhone = new Label
            {
                Text = "Số điện thoại",
                Location = new Point(30, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            this.Controls.Add(lblPhone);
            yPos += 35;

            txtPhone = new TextBox
            {
                Location = new Point(30, yPos),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txtPhone);
            yPos += 60;

            // Save button
            btnSave = new Button
            {
                Text = "lưu thay đổi",
                Location = new Point(30, yPos),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click;
            this.Controls.Add(btnSave);
        }

        private void LoadProfileData()
        {
            if (AuthHelper.CurrentUser != null)
            {
                txtFullName.Text = AuthHelper.CurrentUser.FullName;
                txtUsername.Text = AuthHelper.CurrentUser.Username;
                txtPhone.Text = AuthHelper.CurrentUser.Phone ?? "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                ToastHelper.Show(this, "Vui lòng nhập họ tên!");
                return;
            }

            try
            {
                using (var context = new LearningPlatformContext())
                {
                    var user = AuthHelper.CurrentUser;
                    if (user != null)
                    {
                        var dbUser = context.Users.Find(user.UserId);
                        if (dbUser != null)
                        {
                            dbUser.FullName = txtFullName.Text.Trim();
                            dbUser.Phone = txtPhone.Text.Trim();
                            context.SaveChanges();

                            ToastHelper.Show(this, "Cập nhật thông tin thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this, $"Lỗi khi lưu thông tin: {ex.Message}");
            }
        }
    }
}
