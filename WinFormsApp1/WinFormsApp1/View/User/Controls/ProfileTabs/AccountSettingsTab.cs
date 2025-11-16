using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;

namespace WinFormsApp1.View.User.Controls.ProfileTabs
{
    public partial class AccountSettingsTab : UserControl
    {
        private Label lblEmail;
        private TextBox txtEmail;
        private Button btnEditEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnEditPassword;

        public AccountSettingsTab()
        {
            InitializeComponent();
            LoadAccountData();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.White;
            this.Size = new Size(760, 550);

            int yPos = 30;

            // Email section
            lblEmail = new Label
            {
                Text = "Email",
                Location = new Point(30, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            this.Controls.Add(lblEmail);
            yPos += 35;

            var emailPanel = new Panel
            {
                Location = new Point(30, yPos),
                Size = new Size(700, 50),
                BorderStyle = BorderStyle.FixedSingle
            };

            txtEmail = new TextBox
            {
                Location = new Point(15, 12),
                Size = new Size(600, 30),
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.White
            };
            emailPanel.Controls.Add(txtEmail);

            btnEditEmail = new Button
            {
                Text = "✏️",
                Location = new Point(640, 8),
                Size = new Size(40, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(245, 245, 245),
                Cursor = Cursors.Hand
            };
            btnEditEmail.FlatAppearance.BorderSize = 0;
            btnEditEmail.Click += btnEditEmail_Click;
            emailPanel.Controls.Add(btnEditEmail);

            this.Controls.Add(emailPanel);
            yPos += 80;

            // Password section
            lblPassword = new Label
            {
                Text = "Mật khẩu",
                Location = new Point(30, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            this.Controls.Add(lblPassword);
            yPos += 35;

            var passwordPanel = new Panel
            {
                Location = new Point(30, yPos),
                Size = new Size(700, 50),
                BorderStyle = BorderStyle.FixedSingle
            };

            txtPassword = new TextBox
            {
                Location = new Point(15, 12),
                Size = new Size(600, 30),
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                Text = "••••••••",
                BackColor = Color.White,
                UseSystemPasswordChar = false
            };
            passwordPanel.Controls.Add(txtPassword);

            btnEditPassword = new Button
            {
                Text = "✏️",
                Location = new Point(640, 8),
                Size = new Size(40, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(245, 245, 245),
                Cursor = Cursors.Hand
            };
            btnEditPassword.FlatAppearance.BorderSize = 0;
            btnEditPassword.Click += btnEditPassword_Click;
            passwordPanel.Controls.Add(btnEditPassword);

            this.Controls.Add(passwordPanel);
        }

        private void LoadAccountData()
        {
            if (AuthHelper.CurrentUser != null)
            {
                txtEmail.Text = AuthHelper.CurrentUser.Email;
            }
        }

        private void btnEditEmail_Click(object sender, EventArgs e)
        {
            using (var dialog = new ChangeEmailDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadAccountData();
                    MessageBox.Show("Email đã được cập nhật thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEditPassword_Click(object sender, EventArgs e)
        {
            using (var dialog = new ChangePasswordDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Mật khẩu đã được thay đổi thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }

    // Dialog đổi email
    public class ChangeEmailDialog : Form
    {
        private TextBox txtNewEmail;
        private TextBox txtPassword;
        private Button btnSave;
        private Button btnCancel;

        public ChangeEmailDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Thay đổi Email";
            this.Size = new Size(450, 280);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblNewEmail = new Label
            {
                Text = "Email mới:",
                Location = new Point(30, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblNewEmail);

            txtNewEmail = new TextBox
            {
                Location = new Point(30, 60),
                Size = new Size(380, 30),
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(txtNewEmail);

            var lblPassword = new Label
            {
                Text = "Mật khẩu hiện tại:",
                Location = new Point(30, 110),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblPassword);

            txtPassword = new TextBox
            {
                Location = new Point(30, 140),
                Size = new Size(380, 30),
                Font = new Font("Segoe UI", 10),
                UseSystemPasswordChar = true
            };
            this.Controls.Add(txtPassword);

            btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(220, 190),
                Size = new Size(90, 35),
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(btnCancel);

            btnSave = new Button
            {
                Text = "Lưu",
                Location = new Point(320, 190),
                Size = new Size(90, 35),
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click;
            this.Controls.Add(btnSave);

            this.CancelButton = btnCancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email mới!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // TODO: Verify password and update email in database
            using (var context = new LearningPlatformContext())
            {
                var user = AuthHelper.CurrentUser;
                if (user != null)
                {
                    // Verify password
                    if (!PasswordHelper.VerifyPassword(txtPassword.Text, user.PasswordHash))
                    {
                        MessageBox.Show("Mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update email
                    var dbUser = context.Users.Find(user.UserId);
                    if (dbUser != null)
                    {
                        dbUser.Email = txtNewEmail.Text;
                        context.SaveChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }
    }

    // Dialog đổi mật khẩu
    public class ChangePasswordDialog : Form
    {
        private TextBox txtCurrentPassword;
        private TextBox txtNewPassword;
        private TextBox txtConfirmPassword;
        private Button btnSave;
        private Button btnCancel;

        public ChangePasswordDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Thay đổi Mật khẩu";
            this.Size = new Size(450, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int yPos = 30;

            var lblCurrent = new Label
            {
                Text = "Mật khẩu hiện tại:",
                Location = new Point(30, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblCurrent);
            yPos += 30;

            txtCurrentPassword = new TextBox
            {
                Location = new Point(30, yPos),
                Size = new Size(380, 30),
                Font = new Font("Segoe UI", 10),
                UseSystemPasswordChar = true
            };
            this.Controls.Add(txtCurrentPassword);
            yPos += 50;

            var lblNew = new Label
            {
                Text = "Mật khẩu mới:",
                Location = new Point(30, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblNew);
            yPos += 30;

            txtNewPassword = new TextBox
            {
                Location = new Point(30, yPos),
                Size = new Size(380, 30),
                Font = new Font("Segoe UI", 10),
                UseSystemPasswordChar = true
            };
            this.Controls.Add(txtNewPassword);
            yPos += 50;

            var lblConfirm = new Label
            {
                Text = "Xác nhận mật khẩu mới:",
                Location = new Point(30, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblConfirm);
            yPos += 30;

            txtConfirmPassword = new TextBox
            {
                Location = new Point(30, yPos),
                Size = new Size(380, 30),
                Font = new Font("Segoe UI", 10),
                UseSystemPasswordChar = true
            };
            this.Controls.Add(txtConfirmPassword);
            yPos += 50;

            btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(220, yPos),
                Size = new Size(90, 35),
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(btnCancel);

            btnSave = new Button
            {
                Text = "Lưu",
                Location = new Point(320, yPos),
                Size = new Size(90, 35),
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click;
            this.Controls.Add(btnSave);

            this.CancelButton = btnCancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text) ||
                string.IsNullOrWhiteSpace(txtNewPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Mật khẩu mới không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new LearningPlatformContext())
            {
                var user = AuthHelper.CurrentUser;
                if (user != null)
                {
                    // Verify current password
                    if (!PasswordHelper.VerifyPassword(txtCurrentPassword.Text, user.PasswordHash))
                    {
                        MessageBox.Show("Mật khẩu hiện tại không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update password
                    var dbUser = context.Users.Find(user.UserId);
                    if (dbUser != null)
                    {
                        dbUser.PasswordHash = PasswordHelper.HashPassword(txtNewPassword.Text);
                        context.SaveChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }
    }
}
