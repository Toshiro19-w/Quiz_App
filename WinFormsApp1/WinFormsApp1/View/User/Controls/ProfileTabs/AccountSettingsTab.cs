using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;

namespace WinFormsApp1.View.User.Controls.ProfileTabs
{
    public partial class AccountSettingsTab : UserControl
    {
        private Panel cardPanel;
        private Panel emailContainer;
        private Panel passwordContainer;
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
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(30);

            // Main card panel
            cardPanel = new Panel
            {
                Location = new Point(30, 30),
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Padding = new Padding(40)
            };
            cardPanel.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(230, 230, 230), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, cardPanel.Width - 1, cardPanel.Height - 1);
            };

            int yPos = 40;

            // Section title
            var lblSectionTitle = new Label
            {
                Text = "🔐 Bảo mật tài khoản",
                Location = new Point(40, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            cardPanel.Controls.Add(lblSectionTitle);
            yPos += 50;

            // Email section
            lblEmail = new Label
            {
                Text = "Địa chỉ Email",
                Location = new Point(40, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            cardPanel.Controls.Add(lblEmail);
            yPos += 30;

            emailContainer = new Panel
            {
                Location = new Point(40, yPos),
                Height = 55,
                BackColor = Color.FromArgb(248, 249, 250)
            };
            emailContainer.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(220, 220, 220), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, emailContainer.Width - 1, emailContainer.Height - 1);
            };

            txtEmail = new TextBox
            {
                Location = new Point(20, 15),
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11),
                ReadOnly = true,
                BackColor = Color.FromArgb(248, 249, 250)
            };
            emailContainer.Controls.Add(txtEmail);

            btnEditEmail = new Button
            {
                Text = "Thay đổi",
                Size = new Size(100, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnEditEmail.FlatAppearance.BorderSize = 0;
            btnEditEmail.Click += btnEditEmail_Click;
            emailContainer.Controls.Add(btnEditEmail);

            cardPanel.Controls.Add(emailContainer);
            yPos += 80;

            // Password section
            lblPassword = new Label
            {
                Text = "Mật khẩu",
                Location = new Point(40, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            cardPanel.Controls.Add(lblPassword);
            yPos += 30;

            passwordContainer = new Panel
            {
                Location = new Point(40, yPos),
                Height = 55,
                BackColor = Color.FromArgb(248, 249, 250)
            };
            passwordContainer.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(220, 220, 220), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, passwordContainer.Width - 1, passwordContainer.Height - 1);
            };

            txtPassword = new TextBox
            {
                Location = new Point(20, 15),
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11),
                ReadOnly = true,
                Text = "••••••••••••",
                BackColor = Color.FromArgb(248, 249, 250),
                UseSystemPasswordChar = false
            };
            passwordContainer.Controls.Add(txtPassword);

            btnEditPassword = new Button
            {
                Text = "Đổi mật khẩu",
                Size = new Size(120, 35),  // Increased width
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnEditPassword.FlatAppearance.BorderSize = 0;
            btnEditPassword.Click += btnEditPassword_Click;
            passwordContainer.Controls.Add(btnEditPassword);

            cardPanel.Controls.Add(passwordContainer);
            yPos += 80;

            // Info note
            var lblNote = new Label
            {
                Text = "💡 Để bảo mật tài khoản, bạn nên sử dụng mật khẩu mạnh và thay đổi định kỳ.",
                Location = new Point(40, yPos),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(108, 117, 125),
                AutoSize = true
            };
            cardPanel.Controls.Add(lblNote);

            cardPanel.Height = yPos + 50;
            this.Controls.Add(cardPanel);

            // Handle resize
            this.Resize += AccountSettingsTab_Resize;
            this.Load += (s, e) => AccountSettingsTab_Resize(s, e);
        }

        private void AccountSettingsTab_Resize(object? sender, EventArgs e)
        {
            cardPanel.Width = this.Width - 60;
            
            // Update containers
            emailContainer.Width = cardPanel.Width - 80;
            passwordContainer.Width = cardPanel.Width - 80;
            
            // Update textboxes
            txtEmail.Width = emailContainer.Width - 140;
            txtPassword.Width = passwordContainer.Width - 160;
            
            // Position buttons at right
            btnEditEmail.Location = new Point(emailContainer.Width - btnEditEmail.Width - 10, 10);
            btnEditPassword.Location = new Point(passwordContainer.Width - btnEditPassword.Width - 10, 10);
        }

        private void LoadAccountData()
        {
            if (AuthHelper.CurrentUser != null)
            {
                txtEmail.Text = AuthHelper.CurrentUser.Email;
            }
        }

        private void btnEditEmail_Click(object? sender, EventArgs e)
        {
            using (var dialog = new ChangeEmailDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadAccountData();
                    ToastHelper.Show(this, "✓ Email đã được cập nhật thành công!");
                }
            }
        }

        private void btnEditPassword_Click(object? sender, EventArgs e)
        {
            using (var dialog = new ChangePasswordDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ToastHelper.Show(this, "✓ Mật khẩu đã được thay đổi thành công!");
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
            this.Size = new Size(480, 320);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var lblTitle = new Label
            {
                Text = "📧 Thay đổi địa chỉ Email",
                Location = new Point(30, 25),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };
            this.Controls.Add(lblTitle);

            var lblNewEmail = new Label
            {
                Text = "Email mới:",
                Location = new Point(30, 75),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblNewEmail);

            txtNewEmail = new TextBox
            {
                Location = new Point(30, 100),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 11)
            };
            this.Controls.Add(txtNewEmail);

            var lblPassword = new Label
            {
                Text = "Mật khẩu hiện tại:",
                Location = new Point(30, 145),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblPassword);

            txtPassword = new TextBox
            {
                Location = new Point(30, 170),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 11),
                UseSystemPasswordChar = true
            };
            this.Controls.Add(txtPassword);

            btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(230, 225),
                Size = new Size(95, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(248, 249, 250),
                Font = new Font("Segoe UI", 10),
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
            this.Controls.Add(btnCancel);

            btnSave = new Button
            {
                Text = "Lưu thay đổi",
                Location = new Point(335, 225),
                Size = new Size(95, 40),
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click;
            this.Controls.Add(btnSave);

            this.CancelButton = btnCancel;
        }

        private void btnSave_Click(object? sender, EventArgs e)
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

            using (var context = new LearningPlatformContext())
            {
                var user = AuthHelper.CurrentUser;
                if (user != null)
                {
                    if (!PasswordHelper.VerifyPassword(txtPassword.Text, user.PasswordHash))
                    {
                        MessageBox.Show("Mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var dbUser = context.Users.Find(user.UserId);
                    if (dbUser != null)
                    {
                        dbUser.Email = txtNewEmail.Text;
                        context.SaveChanges();
                        AuthHelper.CurrentUser.Email = dbUser.Email;
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
            this.Size = new Size(480, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var lblTitle = new Label
            {
                Text = "🔒 Thay đổi mật khẩu",
                Location = new Point(30, 25),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };
            this.Controls.Add(lblTitle);

            int yPos = 70;

            var lblCurrent = new Label
            {
                Text = "Mật khẩu hiện tại:",
                Location = new Point(30, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblCurrent);
            yPos += 25;

            txtCurrentPassword = new TextBox
            {
                Location = new Point(30, yPos),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 11),
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
            yPos += 25;

            txtNewPassword = new TextBox
            {
                Location = new Point(30, yPos),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 11),
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
            yPos += 25;

            txtConfirmPassword = new TextBox
            {
                Location = new Point(30, yPos),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 11),
                UseSystemPasswordChar = true
            };
            this.Controls.Add(txtConfirmPassword);
            yPos += 55;

            btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(230, yPos),
                Size = new Size(95, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(248, 249, 250),
                Font = new Font("Segoe UI", 10),
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
            this.Controls.Add(btnCancel);

            btnSave = new Button
            {
                Text = "Lưu thay đổi",
                Location = new Point(335, yPos),
                Size = new Size(95, 40),
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click;
            this.Controls.Add(btnSave);

            this.CancelButton = btnCancel;
        }

        private void btnSave_Click(object? sender, EventArgs e)
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
                    if (!PasswordHelper.VerifyPassword(txtCurrentPassword.Text, user.PasswordHash))
                    {
                        MessageBox.Show("Mật khẩu hiện tại không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

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
