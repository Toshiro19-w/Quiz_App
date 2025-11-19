namespace WinFormsApp1.View
{
    partial class quenmatkhau
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            pnlResetToken = new Panel();
            btnResetPassword = new Button();
            lblConfirmPasswordError = new Label();
            lblPasswordError = new Label();
            txtConfirmNewPassword = new TextBox();
            txtNewPassword = new TextBox();
            txtResetToken = new TextBox();
            lblConfirmNewPassword = new Label();
            lblNewPassword = new Label();
            lblResetToken = new Label();
            lblTokenDisplay = new Label();
            lblEmailError = new Label();
            btnSendReset = new Button();
            btnBackToLogin = new Button();
            txtEmail = new TextBox();
            lblEmail = new Label();
            lblTitle = new Label();
            lblSubtitle = new Label();
            groupBox1.SuspendLayout();
            pnlResetToken.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(pnlResetToken);
            groupBox1.Controls.Add(lblTokenDisplay);
            groupBox1.Controls.Add(lblEmailError);
            groupBox1.Controls.Add(btnSendReset);
            groupBox1.Controls.Add(btnBackToLogin);
            groupBox1.Controls.Add(txtEmail);
            groupBox1.Controls.Add(lblEmail);
            groupBox1.Controls.Add(lblTitle);
            groupBox1.Controls.Add(lblSubtitle);
            groupBox1.Location = new Point(150, 50);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(500, 620);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.FlatStyle = FlatStyle.Flat;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = WinFormsApp1.Helpers.ColorPalette.ButtonPrimary;
            lblTitle.Location = new Point(120, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(260, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Quên mật khẩu";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.Gray;
            lblSubtitle.Location = new Point(80, 90);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(340, 23);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Nhập email để nhận mã đặt lại mật khẩu";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(64, 64, 64);
            lblEmail.Location = new Point(50, 140);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(54, 23);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.Location = new Point(50, 170);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(400, 32);
            txtEmail.TabIndex = 3;
            // 
            // btnBackToLogin
            // 
            btnBackToLogin.BackColor = Color.Transparent;
            btnBackToLogin.FlatStyle = FlatStyle.Flat;
            btnBackToLogin.FlatAppearance.BorderSize = 1;
            btnBackToLogin.FlatAppearance.BorderColor = WinFormsApp1.Helpers.ColorPalette.ButtonPrimary;
            btnBackToLogin.Font = new Font("Segoe UI", 11F);
            btnBackToLogin.ForeColor = WinFormsApp1.Helpers.ColorPalette.ButtonPrimary;
            btnBackToLogin.Location = new Point(50, 550);
            btnBackToLogin.Name = "btnBackToLogin";
            btnBackToLogin.Size = new Size(190, 50);
            btnBackToLogin.TabIndex = 4;
            btnBackToLogin.Text = "Quay lại đăng nhập";
            btnBackToLogin.UseVisualStyleBackColor = false;
            btnBackToLogin.Cursor = Cursors.Hand;
            btnBackToLogin.Click += btnBackToLogin_Click;
            // 
            // btnSendReset
            // 
            btnSendReset.BackColor = WinFormsApp1.Helpers.ColorPalette.ButtonSecondary;
            btnSendReset.FlatStyle = FlatStyle.Flat;
            btnSendReset.FlatAppearance.BorderSize = 0;
            btnSendReset.FlatAppearance.MouseOverBackColor = WinFormsApp1.Helpers.ColorPalette.SecondaryDark;
            btnSendReset.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSendReset.ForeColor = Color.White;
            btnSendReset.Location = new Point(260, 550);
            btnSendReset.Name = "btnSendReset";
            btnSendReset.Size = new Size(190, 50);
            btnSendReset.TabIndex = 5;
            btnSendReset.Text = "Gửi mã xác nhận";
            btnSendReset.UseVisualStyleBackColor = false;
            btnSendReset.Cursor = Cursors.Hand;
            btnSendReset.Click += btnSendReset_Click;
            // 
            // lblTokenDisplay
            // 
            lblTokenDisplay.AutoSize = true;
            lblTokenDisplay.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTokenDisplay.ForeColor = WinFormsApp1.Helpers.ColorPalette.ButtonSecondary;
            lblTokenDisplay.Location = new Point(50, 220);
            lblTokenDisplay.Name = "lblTokenDisplay";
            lblTokenDisplay.Size = new Size(0, 28);
            lblTokenDisplay.TabIndex = 6;
            lblTokenDisplay.Visible = false;
            // 
            // pnlResetToken
            // 
            pnlResetToken.Controls.Add(btnResetPassword);
            pnlResetToken.Controls.Add(lblConfirmPasswordError);
            pnlResetToken.Controls.Add(lblPasswordError);
            pnlResetToken.Controls.Add(txtConfirmNewPassword);
            pnlResetToken.Controls.Add(txtNewPassword);
            pnlResetToken.Controls.Add(txtResetToken);
            pnlResetToken.Controls.Add(lblConfirmNewPassword);
            pnlResetToken.Controls.Add(lblNewPassword);
            pnlResetToken.Controls.Add(lblResetToken);
            pnlResetToken.Location = new Point(50, 260);
            pnlResetToken.Name = "pnlResetToken";
            pnlResetToken.Size = new Size(400, 250);
            pnlResetToken.TabIndex = 7;
            pnlResetToken.Visible = false;
            // 
            // lblResetToken
            // 
            lblResetToken.AutoSize = true;
            lblResetToken.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblResetToken.ForeColor = Color.FromArgb(64, 64, 64);
            lblResetToken.Location = new Point(0, 10);
            lblResetToken.Name = "lblResetToken";
            lblResetToken.Size = new Size(120, 23);
            lblResetToken.TabIndex = 0;
            lblResetToken.Text = "Mã xác nhận";
            // 
            // txtResetToken
            // 
            txtResetToken.BorderStyle = BorderStyle.FixedSingle;
            txtResetToken.Font = new Font("Segoe UI", 11F);
            txtResetToken.Location = new Point(0, 40);
            txtResetToken.Name = "txtResetToken";
            txtResetToken.Size = new Size(400, 32);
            txtResetToken.TabIndex = 1;
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNewPassword.ForeColor = Color.FromArgb(64, 64, 64);
            lblNewPassword.Location = new Point(0, 90);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(118, 23);
            lblNewPassword.TabIndex = 2;
            lblNewPassword.Text = "Mật khẩu mới";
            // 
            // txtNewPassword
            // 
            txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
            txtNewPassword.Font = new Font("Segoe UI", 11F);
            txtNewPassword.Location = new Point(0, 120);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.UseSystemPasswordChar = true;
            txtNewPassword.Size = new Size(400, 32);
            txtNewPassword.TabIndex = 3;
            // 
            // lblConfirmNewPassword
            // 
            lblConfirmNewPassword.AutoSize = true;
            lblConfirmNewPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblConfirmNewPassword.ForeColor = Color.FromArgb(64, 64, 64);
            lblConfirmNewPassword.Location = new Point(0, 170);
            lblConfirmNewPassword.Name = "lblConfirmNewPassword";
            lblConfirmNewPassword.Size = new Size(185, 23);
            lblConfirmNewPassword.TabIndex = 4;
            lblConfirmNewPassword.Text = "Xác nhận mật khẩu mới";
            // 
            // txtConfirmNewPassword
            // 
            txtConfirmNewPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmNewPassword.Font = new Font("Segoe UI", 11F);
            txtConfirmNewPassword.Location = new Point(0, 200);
            txtConfirmNewPassword.Name = "txtConfirmNewPassword";
            txtConfirmNewPassword.UseSystemPasswordChar = true;
            txtConfirmNewPassword.Size = new Size(400, 32);
            txtConfirmNewPassword.TabIndex = 5;
            // 
            // btnResetPassword
            // 
            btnResetPassword.BackColor = WinFormsApp1.Helpers.ColorPalette.ButtonPrimary;
            btnResetPassword.FlatStyle = FlatStyle.Flat;
            btnResetPassword.FlatAppearance.BorderSize = 0;
            btnResetPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnResetPassword.ForeColor = Color.White;
            btnResetPassword.Location = new Point(250, 250);
            btnResetPassword.Name = "btnResetPassword";
            btnResetPassword.Size = new Size(150, 40);
            btnResetPassword.TabIndex = 6;
            btnResetPassword.Text = "Đặt lại mật khẩu";
            btnResetPassword.UseVisualStyleBackColor = false;
            btnResetPassword.Cursor = Cursors.Hand;
            btnResetPassword.Click += btnResetPassword_Click;
            // 
            // lblEmailError
            // 
            lblEmailError.AutoSize = true;
            lblEmailError.Font = new Font("Segoe UI", 9F);
            lblEmailError.ForeColor = Color.Red;
            lblEmailError.Location = new Point(50, 205);
            lblEmailError.Name = "lblEmailError";
            lblEmailError.Size = new Size(0, 20);
            lblEmailError.TabIndex = 8;
            // 
            // lblPasswordError
            // 
            lblPasswordError.AutoSize = true;
            lblPasswordError.Font = new Font("Segoe UI", 9F);
            lblPasswordError.ForeColor = Color.Red;
            lblPasswordError.Location = new Point(0, 155);
            lblPasswordError.Name = "lblPasswordError";
            lblPasswordError.Size = new Size(0, 20);
            lblPasswordError.TabIndex = 7;
            // 
            // lblConfirmPasswordError
            // 
            lblConfirmPasswordError.AutoSize = true;
            lblConfirmPasswordError.Font = new Font("Segoe UI", 9F);
            lblConfirmPasswordError.ForeColor = Color.Red;
            lblConfirmPasswordError.Location = new Point(0, 235);
            lblConfirmPasswordError.Name = "lblConfirmPasswordError";
            lblConfirmPasswordError.Size = new Size(0, 20);
            lblConfirmPasswordError.TabIndex = 8;
            // 
            // quenmatkhau
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(800, 720);
            Controls.Add(groupBox1);
            Name = "quenmatkhau";
            Text = "Quên mật khẩu - YMEDU Learning Platform";
            StartPosition = FormStartPosition.CenterScreen;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            pnlResetToken.ResumeLayout(false);
            pnlResetToken.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblEmail;
        private TextBox txtEmail;
        private Button btnBackToLogin;
        private Button btnSendReset;
        private Label lblTokenDisplay;
        private Panel pnlResetToken;
        private Label lblResetToken;
        private TextBox txtResetToken;
        private Label lblNewPassword;
        private TextBox txtNewPassword;
        private Label lblConfirmNewPassword;
        private TextBox txtConfirmNewPassword;
        private Button btnResetPassword;
        private Label lblEmailError;
        private Label lblPasswordError;
        private Label lblConfirmPasswordError;
    }
}