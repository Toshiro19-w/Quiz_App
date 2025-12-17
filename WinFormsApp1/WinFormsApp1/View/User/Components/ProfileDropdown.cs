using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;

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
        
        // Language selector controls
        private Label lblCurrentLanguage;
        
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
            
            // Subscribe to language change event
            LanguageHelper.LanguageChanged += OnLanguageChanged;
        }

        private void OnLanguageChanged(object? sender, EventArgs e)
        {
            // Update UI when language changes
            UpdateLanguageDisplay();
        }

        private void UpdateLanguageDisplay()
        {
            if (lblCurrentLanguage != null)
            {
                lblCurrentLanguage.Text = $"{LanguageHelper.CurrentLanguageName} ▼";
            }
        }

        private void InitializeComponent()
        {
            this.Width = 280;
            this.BackColor = Color.White;
            this.Visible = false;
            this.AutoSize = false;

            // Create border panel for shadow effect
            this.Paint += ProfileDropdown_Paint;

            // Header Panel
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            // Avatar
            avatarBox = new PictureBox
            {
                Width = 50,
                Height = 50,
                Location = new Point(15, 20),
                BackColor = Color.FromArgb(64, 64, 64),
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            MakeCircular(avatarBox);

            // Name Label
            lblName = new Label
            {
                Text = "Tên User",
                Location = new Point(75, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary,
                MaximumSize = new Size(180, 0)
            };

            // Email Label
            lblEmail = new Label
            {
                Text = "email@example.com",
                Location = new Point(75, 45),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextSecondary,
                MaximumSize = new Size(180, 0)
            };

            headerPanel.Controls.Add(avatarBox);
            headerPanel.Controls.Add(lblName);
            headerPanel.Controls.Add(lblEmail);

            // Menu Panel
            menuPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(0, 5, 0, 0),
                AutoScroll = false
            };

            int yPos = 0;

            // Menu Items - create buttons then wire to events so subscribers added later are called
            var btnHocTap = AddMenuItem($"📚 {LanguageHelper.GetString("Learning")}", ref yPos);
            btnHocTap.Click += (s, e) => OnHocTapClick?.Invoke(s, e);

            var btnGioHang = AddMenuItem($"🛒 {LanguageHelper.GetString("MyCart")}", ref yPos);
            btnGioHang.Click += (s, e) => OnGioHangClick?.Invoke(s, e);
            
            // Separator for teacher/admin
            if (AuthHelper.CurrentUser != null && !AuthHelper.IsUser())
            {
                AddSeparator(ref yPos);
                var btnBangDieuKhien = AddMenuItem($"📊 {LanguageHelper.GetString("Dashboard")}", ref yPos);
                btnBangDieuKhien.ForeColor = Color.FromArgb(88, 56, 255);
                btnBangDieuKhien.Click += (s, e) => OnBangDieuKhienClick?.Invoke(s, e);
            }

            AddSeparator(ref yPos);
            
            // Cập nhật các menu item này để trigger event với tab index
            var btnCaiDat = AddMenuItem($"⚙️ {LanguageHelper.GetString("AccountSettings")}", ref yPos);
            btnCaiDat.Click += (s, e) => {
                OnProfileTabClick?.Invoke(this, 0); // Tab index 0
                OnCaiDatClick?.Invoke(s, e);
            };

            var btnChinhSua = AddMenuItem($"✏️ {LanguageHelper.GetString("EditProfile")}", ref yPos);
            btnChinhSua.Click += (s, e) => {
                OnProfileTabClick?.Invoke(this, 1); // Tab index 1
                OnChinhSuaClick?.Invoke(s, e);
            };

            var btnLichSu = AddMenuItem($"📜 {LanguageHelper.GetString("PurchaseHistory")}", ref yPos);
            btnLichSu.Click += (s, e) => {
                OnProfileTabClick?.Invoke(this, 2); // Tab index 2
                OnLichSuMuaHangClick?.Invoke(s, e);
            };

            AddSeparator(ref yPos);
            
            // Language selector
            AddLanguageSelector(ref yPos);

            AddSeparator(ref yPos);
            
            // Logout - ensure event invoked when button clicked even if subscribers added after construction
            var btnLogout = AddMenuItem($"🚪 {LanguageHelper.GetString("Logout")}", ref yPos);
            btnLogout.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLogout.ForeColor = Color.FromArgb(220, 53, 69);
            btnLogout.Click += (s, e) => OnDangXuatClick?.Invoke(s, e);

            // Calculate total height
            int totalHeight = 90 + yPos + 10; // header + menu items + padding
            this.Height = totalHeight;

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
                Dock = DockStyle.None,
                Width = this.Width,
                Height = 45,
                Location = new Point(0, yPos),
                BackColor = Color.White,
                ForeColor = ColorPalette.TextPrimary,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ColorPalette.Background;

            menuPanel.Controls.Add(btn);
            yPos += 45;

            return btn;
        }

        private void AddSeparator(ref int yPos)
        {
            var separator = new Panel
            {
                Width = this.Width - 30,
                Height = 1,
                Location = new Point(15, yPos + 5),
                BackColor = ColorPalette.Border
            };

            menuPanel.Controls.Add(separator);
            yPos += 12;
        }

        private void AddLanguageSelector(ref int yPos)
        {
            var langPanel = new Panel
            {
                Width = this.Width,
                Height = 45,
                Location = new Point(0, yPos),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };

            var lblNgonNgu = new Label
            {
                Text = $"🌐 {LanguageHelper.GetString("Language")}",
                Location = new Point(15, 12),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = ColorPalette.TextPrimary
            };

            lblCurrentLanguage = new Label
            {
                Text = $"{LanguageHelper.CurrentLanguageName} ▼",
                Location = new Point(165, 12),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextSecondary,
                Cursor = Cursors.Hand
            };

            langPanel.Controls.Add(lblNgonNgu);
            langPanel.Controls.Add(lblCurrentLanguage);

            // Hover effects
            langPanel.MouseEnter += (s, e) => langPanel.BackColor = ColorPalette.Background;
            langPanel.MouseLeave += (s, e) => langPanel.BackColor = Color.White;
            lblNgonNgu.MouseEnter += (s, e) => langPanel.BackColor = ColorPalette.Background;
            lblCurrentLanguage.MouseEnter += (s, e) => langPanel.BackColor = ColorPalette.Background;

            // Click to open language selector dialog
            void OpenLanguageDialog(object? s, EventArgs e)
            {
                HideDropdown();
                var dialog = new LanguageSelectorDialog();
                dialog.ShowDialog(this.FindForm());
            }

            langPanel.Click += OpenLanguageDialog;
            lblNgonNgu.Click += OpenLanguageDialog;
            lblCurrentLanguage.Click += OpenLanguageDialog;

            menuPanel.Controls.Add(langPanel);
            yPos += 45;
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

                using (Font font = new Font("Segoe UI", 18, FontStyle.Bold))
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                LanguageHelper.LanguageChanged -= OnLanguageChanged;
            }
            base.Dispose(disposing);
        }
    }
}
