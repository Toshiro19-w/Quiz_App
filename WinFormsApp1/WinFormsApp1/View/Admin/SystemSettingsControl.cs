using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    public partial class SystemSettingsControl : AdminBaseControl
    {
        private TabControl tabControl;
        private TabPage generalTab, securityTab, emailTab, backupTab;

        public SystemSettingsControl() : base()
        {
            InitializeComponent();
            SetupTabs();
        }

        private void SetupTabs()
        {
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };

            // General Settings Tab
            generalTab = new TabPage("Cài đặt chung");
            SetupGeneralTab();

            // Security Settings Tab
            securityTab = new TabPage("Bảo mật");
            SetupSecurityTab();

            // Email Settings Tab
            emailTab = new TabPage("Email");
            SetupEmailTab();

            // Backup Settings Tab
            backupTab = new TabPage("Sao lưu");
            SetupBackupTab();

            tabControl.TabPages.AddRange(new TabPage[] { generalTab, securityTab, emailTab, backupTab });
            this.Controls.Add(tabControl);
        }

        private void SetupGeneralTab()
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            
            var lblSiteName = new Label { Text = "Tên trang web:", Location = new Point(0, 20), AutoSize = true };
            var txtSiteName = new TextBox { Location = new Point(0, 45), Size = new Size(300, 25), Text = "YMEDU Learning Platform" };
            
            var lblPageSize = new Label { Text = "Số mục mỗi trang:", Location = new Point(0, 80), AutoSize = true };
            var numPageSize = new NumericUpDown { Location = new Point(0, 105), Size = new Size(100, 25), Value = 50, Minimum = 10, Maximum = 200 };
            
            var btnSaveGeneral = new Button 
            { 
                Text = "💾 Lưu cài đặt", 
                Location = new Point(0, 150), 
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(52, 144, 220),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSaveGeneral.FlatAppearance.BorderSize = 0;
            btnSaveGeneral.Click += (s, e) => ToastHelper.Show(this.FindForm(), "✅ Cài đặt đã được lưu!");

            panel.Controls.AddRange(new Control[] { lblSiteName, txtSiteName, lblPageSize, numPageSize, btnSaveGeneral });
            generalTab.Controls.Add(panel);
        }

        private void SetupSecurityTab()
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            
            var chkTwoFactor = new CheckBox { Text = "Bật xác thực 2 yếu tố", Location = new Point(0, 20), AutoSize = true };
            var chkPasswordExpiry = new CheckBox { Text = "Mật khẩu hết hạn sau 90 ngày", Location = new Point(0, 50), AutoSize = true };
            var chkLoginAttempts = new CheckBox { Text = "Khóa tài khoản sau 5 lần đăng nhập sai", Location = new Point(0, 80), AutoSize = true, Checked = true };
            
            var lblSessionTimeout = new Label { Text = "Thời gian hết hạn phiên (phút):", Location = new Point(0, 120), AutoSize = true };
            var numSessionTimeout = new NumericUpDown { Location = new Point(0, 145), Size = new Size(100, 25), Value = 30, Minimum = 5, Maximum = 480 };
            
            var btnSaveSecurity = new Button 
            { 
                Text = "🔒 Lưu bảo mật", 
                Location = new Point(0, 190), 
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSaveSecurity.FlatAppearance.BorderSize = 0;
            btnSaveSecurity.Click += (s, e) => ToastHelper.Show(this.FindForm(), "🔒 Cài đặt bảo mật đã được lưu!");

            panel.Controls.AddRange(new Control[] { chkTwoFactor, chkPasswordExpiry, chkLoginAttempts, lblSessionTimeout, numSessionTimeout, btnSaveSecurity });
            securityTab.Controls.Add(panel);
        }

        private void SetupEmailTab()
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            
            var lblSmtpServer = new Label { Text = "SMTP Server:", Location = new Point(0, 20), AutoSize = true };
            var txtSmtpServer = new TextBox { Location = new Point(0, 45), Size = new Size(300, 25), Text = "smtp.gmail.com" };
            
            var lblSmtpPort = new Label { Text = "SMTP Port:", Location = new Point(0, 80), AutoSize = true };
            var numSmtpPort = new NumericUpDown { Location = new Point(0, 105), Size = new Size(100, 25), Value = 587, Minimum = 1, Maximum = 65535 };
            
            var lblEmailFrom = new Label { Text = "Email gửi từ:", Location = new Point(0, 140), AutoSize = true };
            var txtEmailFrom = new TextBox { Location = new Point(0, 165), Size = new Size(300, 25), Text = "noreply@ymedu.com" };
            
            var chkEnableSSL = new CheckBox { Text = "Bật SSL/TLS", Location = new Point(0, 200), AutoSize = true, Checked = true };
            
            var btnTestEmail = new Button 
            { 
                Text = "📧 Test Email", 
                Location = new Point(0, 240), 
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(255, 193, 7),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btnTestEmail.FlatAppearance.BorderSize = 0;
            btnTestEmail.Click += (s, e) => ToastHelper.Show(this.FindForm(), "📧 Email test đã được gửi!");

            var btnSaveEmail = new Button 
            { 
                Text = "💾 Lưu Email", 
                Location = new Point(110, 240), 
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSaveEmail.FlatAppearance.BorderSize = 0;
            btnSaveEmail.Click += (s, e) => ToastHelper.Show(this.FindForm(), "✅ Cài đặt email đã được lưu!");

            panel.Controls.AddRange(new Control[] { lblSmtpServer, txtSmtpServer, lblSmtpPort, numSmtpPort, lblEmailFrom, txtEmailFrom, chkEnableSSL, btnTestEmail, btnSaveEmail });
            emailTab.Controls.Add(panel);
        }

        private void SetupBackupTab()
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            
            var lblBackupPath = new Label { Text = "Thư mục sao lưu:", Location = new Point(0, 20), AutoSize = true };
            var txtBackupPath = new TextBox { Location = new Point(0, 45), Size = new Size(250, 25), Text = @"C:\Backups\YMEDU" };
            var btnBrowse = new Button { Text = "...", Location = new Point(255, 45), Size = new Size(30, 25) };
            
            var chkAutoBackup = new CheckBox { Text = "Tự động sao lưu hàng ngày", Location = new Point(0, 80), AutoSize = true, Checked = true };
            var lblBackupTime = new Label { Text = "Thời gian sao lưu:", Location = new Point(0, 110), AutoSize = true };
            var dtpBackupTime = new DateTimePicker { Location = new Point(0, 135), Size = new Size(100, 25), Format = DateTimePickerFormat.Time, ShowUpDown = true };
            
            var lblRetention = new Label { Text = "Giữ lại (ngày):", Location = new Point(0, 170), AutoSize = true };
            var numRetention = new NumericUpDown { Location = new Point(0, 195), Size = new Size(100, 25), Value = 30, Minimum = 1, Maximum = 365 };
            
            var btnBackupNow = new Button 
            { 
                Text = "🗄️ Sao lưu ngay", 
                Location = new Point(0, 240), 
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnBackupNow.FlatAppearance.BorderSize = 0;
            btnBackupNow.Click += (s, e) => 
            {
                var progressForm = new Form 
                { 
                    Text = "Đang sao lưu...", 
                    Size = new Size(300, 100), 
                    StartPosition = FormStartPosition.CenterParent 
                };
                var progressBar = new ProgressBar { Dock = DockStyle.Fill, Style = ProgressBarStyle.Marquee };
                progressForm.Controls.Add(progressBar);
                progressForm.Show(this.FindForm());
                
                var timer = new System.Windows.Forms.Timer { Interval = 3000 };
                timer.Tick += (sender, args) => 
                {
                    timer.Stop();
                    timer.Dispose();
                    progressForm.Close();
                    ToastHelper.Show(this.FindForm(), "✅ Sao lưu hoàn tất!");
                };
                timer.Start();
            };

            var btnRestore = new Button 
            { 
                Text = "📥 Khôi phục", 
                Location = new Point(130, 240), 
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(23, 162, 184),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRestore.FlatAppearance.BorderSize = 0;
            btnRestore.Click += (s, e) => 
            {
                var result = MessageBox.Show("Bạn có chắc muốn khôi phục dữ liệu? Thao tác này không thể hoàn tác!", 
                    "Xác nhận khôi phục", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    ToastHelper.Show(this.FindForm(), "📥 Khôi phục dữ liệu hoàn tất!");
            };

            panel.Controls.AddRange(new Control[] { lblBackupPath, txtBackupPath, btnBrowse, chkAutoBackup, lblBackupTime, dtpBackupTime, lblRetention, numRetention, btnBackupNow, btnRestore });
            backupTab.Controls.Add(panel);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "SystemSettingsControl";
            this.Size = new Size(800, 600);
            this.ResumeLayout(false);
        }
    }
}