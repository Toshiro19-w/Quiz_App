using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;

namespace WinFormsApp1.View.Admin.Controls.ProfileTabs
{
    public partial class AdminEditProfileTab : UserControl
    {
        private Panel cardPanel;
        private PictureBox avatarBox;
        private TextBox txtFullName;
        private TextBox txtUsername;
        private TextBox txtPhone;
        private TextBox txtBio;
        private Button btnSave;

        public AdminEditProfileTab()
        {
            InitializeComponent();
            LoadProfileData();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(30);

            // Main card panel - anchor to expand
            cardPanel = new Panel
            {
                Location = new Point(30, 30),
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Padding = new Padding(40),
                AutoScroll = true
            };
            cardPanel.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(230, 230, 230), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, cardPanel.Width - 1, cardPanel.Height - 1);
            };

            int yPos = 30;

            // Section title
            var lblSectionTitle = new Label
            {
                Text = "👤 Thông tin cá nhân Admin",
                Location = new Point(40, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            cardPanel.Controls.Add(lblSectionTitle);
            yPos += 50;

            // Avatar section
            var avatarPanel = new Panel
            {
                Location = new Point(40, yPos),
                Height = 100,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(248, 249, 250)
            };
            avatarPanel.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(220, 220, 220), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, avatarPanel.Width - 1, avatarPanel.Height - 1);
            };

            avatarBox = new PictureBox
            {
                Location = new Point(20, 15),
                Size = new Size(70, 70),
                BackColor = Color.FromArgb(45, 55, 72),
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            MakeCircular(avatarBox);
            avatarPanel.Controls.Add(avatarBox);

            var lblAvatarInfo = new Label
            {
                Text = "Ảnh đại diện",
                Location = new Point(110, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            avatarPanel.Controls.Add(lblAvatarInfo);

            var lblAvatarNote = new Label
            {
                Text = "Ảnh đại diện được tạo tự động từ tên của bạn",
                Location = new Point(110, 45),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            avatarPanel.Controls.Add(lblAvatarNote);

            cardPanel.Controls.Add(avatarPanel);
            yPos += 120;

            // Full Name and Username in a row
            var lblFullName = new Label
            {
                Text = "Họ và tên",
                Location = new Point(40, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            cardPanel.Controls.Add(lblFullName);

            var lblUsername = new Label
            {
                Text = "Tên người dùng",
                Location = new Point(400, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            cardPanel.Controls.Add(lblUsername);
            yPos += 28;

            txtFullName = new TextBox
            {
                Location = new Point(40, yPos),
                Size = new Size(320, 40),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle
            };
            cardPanel.Controls.Add(txtFullName);

            txtUsername = new TextBox
            {
                Location = new Point(400, yPos),
                Size = new Size(320, 40),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true,
                BackColor = Color.FromArgb(245, 245, 245)
            };
            cardPanel.Controls.Add(txtUsername);

            var lblUsernameNote = new Label
            {
                Text = "🔒 Không thể thay đổi",
                Location = new Point(400, yPos + 42),
                AutoSize = true,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray
            };
            cardPanel.Controls.Add(lblUsernameNote);
            yPos += 85;

            // Phone
            var lblPhone = new Label
            {
                Text = "Số điện thoại",
                Location = new Point(40, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            cardPanel.Controls.Add(lblPhone);
            yPos += 28;

            txtPhone = new TextBox
            {
                Location = new Point(40, yPos),
                Size = new Size(320, 40),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Nhập số điện thoại..."
            };
            cardPanel.Controls.Add(txtPhone);
            yPos += 70;

            // Bio
            var lblBio = new Label
            {
                Text = "Giới thiệu bản thân",
                Location = new Point(40, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            cardPanel.Controls.Add(lblBio);
            yPos += 28;

            txtBio = new TextBox
            {
                Location = new Point(40, yPos),
                Size = new Size(680, 80),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true,
                PlaceholderText = "Viết vài dòng giới thiệu về bản thân bạn..."
            };
            cardPanel.Controls.Add(txtBio);
            yPos += 100;

            // Save button
            btnSave = new Button
            {
                Text = "💾 Lưu thay đổi",
                Location = new Point(40, yPos),
                Size = new Size(175, 45),
                BackColor = Color.FromArgb(45, 55, 72),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click;
            cardPanel.Controls.Add(btnSave);

            this.Controls.Add(cardPanel);

            // Handle resize
            this.Resize += AdminEditProfileTab_Resize;
        }

        private void AdminEditProfileTab_Resize(object? sender, EventArgs e)
        {
            cardPanel.Width = this.Width - 60;
            cardPanel.Height = this.Height - 60;

            // Update avatar panel width
            foreach (Control c in cardPanel.Controls)
            {
                if (c is Panel p && p.Height == 100)
                {
                    p.Width = cardPanel.Width - 80;
                }
            }

            // Update bio width
            txtBio.Width = cardPanel.Width - 80;
        }

        private void MakeCircular(PictureBox pictureBox)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);
            pictureBox.Region = new Region(path);
        }

        private void LoadProfileData()
        {
            if (AuthHelper.CurrentUser != null)
            {
                txtFullName.Text = AuthHelper.CurrentUser.FullName;
                txtUsername.Text = AuthHelper.CurrentUser.Username;
                txtPhone.Text = AuthHelper.CurrentUser.Phone ?? "";

                DrawInitialsOnAvatar(GetInitials(AuthHelper.CurrentUser.FullName));

                try
                {
                    using var context = new LearningPlatformContext();
                    var profile = context.UserProfiles.FirstOrDefault(p => p.UserId == AuthHelper.CurrentUser.UserId);
                    if (profile != null)
                    {
                        txtBio.Text = profile.Bio ?? "";
                    }
                }
                catch { }
            }
        }

        private string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return "A";
            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
                return $"{parts[0][0]}{parts[parts.Length - 1][0]}".ToUpper();
            else if (parts.Length == 1)
                return parts[0][0].ToString().ToUpper();
            return "A";
        }

        private void DrawInitialsOnAvatar(string initials)
        {
            Bitmap bmp = new Bitmap(avatarBox.Width, avatarBox.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.FromArgb(45, 55, 72)); // Admin color theme

                using (Font font = new Font("Segoe UI", 22, FontStyle.Bold))
                {
                    SizeF textSize = g.MeasureString(initials, font);
                    float x = (bmp.Width - textSize.Width) / 2;
                    float y = (bmp.Height - textSize.Height) / 2;
                    g.DrawString(initials, font, Brushes.White, x, y);
                }
            }
            avatarBox.Image = bmp;
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                ToastHelper.Show(this, "❌ Vui lòng nhập họ tên!");
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

                            var profile = context.UserProfiles.FirstOrDefault(p => p.UserId == user.UserId);
                            if (profile != null)
                            {
                                profile.Bio = txtBio.Text.Trim();
                            }
                            else
                            {
                                context.UserProfiles.Add(new Models.Entities.UserProfile
                                {
                                    UserId = user.UserId,
                                    Bio = txtBio.Text.Trim()
                                });
                            }

                            context.SaveChanges();

                            AuthHelper.CurrentUser.FullName = dbUser.FullName;
                            AuthHelper.CurrentUser.Phone = dbUser.Phone;

                            DrawInitialsOnAvatar(GetInitials(dbUser.FullName));
                            UpdateAdminDashboardUI();

                            ToastHelper.Show(this, "✓ Cập nhật thông tin thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this, $"❌ Lỗi: {ex.Message}");
            }
        }

        private void UpdateAdminDashboardUI()
        {
            // Update the AdminDashboard top panel user label if needed
            var adminDashboard = this.FindForm() as AdminDashboard;
            if (adminDashboard != null)
            {
                // The AdminDashboard will be updated when re-loaded
                // For now, just refresh current display
            }
        }
    }
}
