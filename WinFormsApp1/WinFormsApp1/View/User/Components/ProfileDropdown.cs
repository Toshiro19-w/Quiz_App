using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.User.Components
{
    public class ProfileDropdown : Panel
    {
        private Panel headerPanel;
        private PictureBox avatarBox;
        private Label lblName;
        private Label lblEmail;
        private Panel menuPanel;
        private System.Windows.Forms.Timer fadeTimer;
        private int targetOpacity = 100;
        private int currentOpacity = 0;
        
        public event EventHandler? OnHocTapClick;
        public event EventHandler? OnGioHangClick;
        public event EventHandler? OnBangDieuKhienClick;
        public event EventHandler? OnCaiDatClick;
        public event EventHandler? OnChinhSuaClick;
        public event EventHandler? OnLichSuMuaHangClick;
        public event EventHandler? OnDangXuatClick;

        // Thêm event để chuyển tab
        public event EventHandler<int>? OnProfileTabClick;

        public ProfileDropdown()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Width = 300;
            this.Height = 520;
            this.BackColor = Color.White;
            this.Visible = false;

            // Create border panel for shadow effect
            this.Paint += ProfileDropdown_Paint;

            // Header Panel
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            // Avatar
            avatarBox = new PictureBox
            {
                Width = 60,
                Height = 60,
                Location = new Point(15, 20),
                BackColor = Color.FromArgb(64, 64, 64),
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            MakeCircular(avatarBox);

            // Name Label
            lblName = new Label
            {
                Text = "Tên User",
                Location = new Point(85, 25),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };

            // Email Label
            lblEmail = new Label
            {
                Text = "email@example.com",
                Location = new Point(85, 50),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextSecondary
            };

            headerPanel.Controls.Add(avatarBox);
            headerPanel.Controls.Add(lblName);
            headerPanel.Controls.Add(lblEmail);

            // Menu Panel
            menuPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(0, 10, 0, 0),
                AutoScroll = false
            };

            int yPos = 0;

            // Menu Items - create buttons then wire to events so subscribers added later are called
            var btnHocTap = AddMenuItem("Học tập", ref yPos);
            btnHocTap.Click += (s, e) => OnHocTapClick?.Invoke(s, e);

            var btnGioHang = AddMenuItem("Giỏ hàng của tôi", ref yPos);
            btnGioHang.Click += (s, e) => OnGioHangClick?.Invoke(s, e);
            
            // Separator for teacher/admin
            if (AuthHelper.CurrentUser != null && !AuthHelper.IsUser())
            {
                AddSeparator(ref yPos);
                var btnBangDieuKhien = AddMenuItem("Bảng điều khiển của giảng viên", ref yPos);
                btnBangDieuKhien.Click += (s, e) => OnBangDieuKhienClick?.Invoke(s, e);
            }

            AddSeparator(ref yPos);
            
            // Cập nhật các menu item này để trigger event với tab index
            var btnCaiDat = AddMenuItem("Cài đặt tài khoản", ref yPos);
            btnCaiDat.Click += (s, e) => {
                OnProfileTabClick?.Invoke(this, 0); // Tab index 0
                OnCaiDatClick?.Invoke(s, e);
            };

            var btnChinhSua = AddMenuItem("Chỉnh sửa hồ sơ", ref yPos);
            btnChinhSua.Click += (s, e) => {
                OnProfileTabClick?.Invoke(this, 1); // Tab index 1
                OnChinhSuaClick?.Invoke(s, e);
            };

            var btnLichSu = AddMenuItem("Lịch sử mua hàng", ref yPos);
            btnLichSu.Click += (s, e) => {
                OnProfileTabClick?.Invoke(this, 2); // Tab index 2
                OnLichSuMuaHangClick?.Invoke(s, e);
            };

            AddSeparator(ref yPos);
            
            // Language selector
            AddLanguageSelector(ref yPos);

            AddSeparator(ref yPos);
            
            // Logout - ensure event invoked when button clicked even if subscribers added after construction
            var btnLogout = AddMenuItem("Đăng xuất", ref yPos);
            btnLogout.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLogout.Click += (s, e) => OnDangXuatClick?.Invoke(s, e);

            this.Controls.Add(menuPanel);
            this.Controls.Add(headerPanel);

            // Setup fade timer
            fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 10;
            fadeTimer.Tick += FadeTimer_Tick;

            LoadUserInfo();
        }

        private void ProfileDropdown_Paint(object sender, PaintEventArgs e)
        {
            // Draw shadow effect
            using (Pen shadowPen = new Pen(Color.FromArgb(30, 0, 0, 0), 1))
            {
                e.Graphics.DrawRectangle(shadowPen, 0, 0, this.Width - 1, this.Height - 1);
            }

            // Draw subtle shadow around
            Rectangle rect = new Rectangle(1, 1, this.Width - 3, this.Height - 3);
            using (Pen borderPen = new Pen(ColorPalette.Border, 1))
            {
                e.Graphics.DrawRectangle(borderPen, rect);
            }
        }

        private void FadeTimer_Tick(object? sender, EventArgs e)
        {
            if (currentOpacity < targetOpacity)
            {
                currentOpacity += 10;
                if (currentOpacity >= targetOpacity)
                {
                    currentOpacity = targetOpacity;
                    fadeTimer.Stop();
                }
            }
            else if (currentOpacity > targetOpacity)
            {
                currentOpacity -= 10;
                if (currentOpacity <= targetOpacity)
                {
                    currentOpacity = targetOpacity;
                    fadeTimer.Stop();
                    if (currentOpacity == 0)
                    {
                        this.Visible = false;
                    }
                }
            }

            // Update opacity (note: this is a simplified version)
            this.Invalidate();
        }

        private void MakeCircular(PictureBox pictureBox)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);
            pictureBox.Region = new Region(path);
        }

        private Button AddMenuItem(string text, ref int yPos)
        {
            var btn = new Button
            {
                Text = text,
                Width = menuPanel.Width,
                Height = 50,
                Location = new Point(0, yPos),
                BackColor = Color.White,
                ForeColor = ColorPalette.TextPrimary,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ColorPalette.Background;

            menuPanel.Controls.Add(btn);
            yPos += 50;

            return btn;
        }

        private void AddSeparator(ref int yPos)
        {
            var separator = new Panel
            {
                Width = menuPanel.Width - 20,
                Height = 1,
                Location = new Point(10, yPos + 5),
                BackColor = ColorPalette.Border
            };

            menuPanel.Controls.Add(separator);
            yPos += 10;
        }

        private void AddLanguageSelector(ref int yPos)
        {
            var langPanel = new Panel
            {
                Width = menuPanel.Width,
                Height = 50,
                Location = new Point(0, yPos),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };

            var lblNgonNgu = new Label
            {
                Text = "Ngôn ngữ",
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = ColorPalette.TextPrimary
            };

            var lblTiengViet = new Label
            {
                Text = "🌐 Tiếng Việt",
                Location = new Point(200, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextSecondary,
                Cursor = Cursors.Hand
            };

            langPanel.Controls.Add(lblNgonNgu);
            langPanel.Controls.Add(lblTiengViet);

            langPanel.MouseEnter += (s, e) => langPanel.BackColor = ColorPalette.Background;
            langPanel.MouseLeave += (s, e) => langPanel.BackColor = Color.White;
            lblNgonNgu.MouseEnter += (s, e) => langPanel.BackColor = ColorPalette.Background;
            lblTiengViet.MouseEnter += (s, e) => langPanel.BackColor = ColorPalette.Background;

            menuPanel.Controls.Add(langPanel);
            yPos += 50;
        }

        public void LoadUserInfo()
        {
            if (AuthHelper.CurrentUser != null)
            {
                lblName.Text = AuthHelper.CurrentUser.FullName;
                lblEmail.Text = AuthHelper.CurrentUser.Email;

                // Draw initials on avatar
                DrawInitialsOnAvatar(GetInitials(AuthHelper.CurrentUser.FullName));
            }
        }

        private string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return "U";

            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
                return $"{parts[0][0]}{parts[parts.Length - 1][0]}".ToUpper();
            else if (parts.Length == 1)
                return parts[0][0].ToString().ToUpper();

            return "U";
        }

        private void DrawInitialsOnAvatar(string initials)
        {
            Bitmap bmp = new Bitmap(avatarBox.Width, avatarBox.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.FromArgb(64, 64, 64));

                using (Font font = new Font("Segoe UI", 20, FontStyle.Bold))
                {
                    SizeF textSize = g.MeasureString(initials, font);
                    float x = (bmp.Width - textSize.Width) / 2;
                    float y = (bmp.Height - textSize.Height) / 2;

                    g.DrawString(initials, font, Brushes.White, x, y);
                }
            }

            avatarBox.Image = bmp;
        }

        public void ShowDropdown(Control parent)
        {
            this.Visible = true;
            this.BringToFront();
            
            // Position below the profile button
            Point location = parent.PointToScreen(Point.Empty);
            Point formLocation = parent.FindForm().PointToScreen(Point.Empty);
            
            this.Location = new Point(
                location.X - formLocation.X - this.Width + parent.Width,
                location.Y - formLocation.Y + parent.Height + 5
            );

            // Start fade in animation
            currentOpacity = 0;
            targetOpacity = 100;
            fadeTimer.Start();
        }

        public void HideDropdown()
        {
            // Start fade out animation
            targetOpacity = 0;
            fadeTimer.Start();
        }
    }
}
