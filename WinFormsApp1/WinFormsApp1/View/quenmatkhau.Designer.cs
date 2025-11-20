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
			btnConfirmReset = new Button();
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
			pictureBox1 = new PictureBox();
			groupBox1.SuspendLayout();
			pnlResetToken.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
			groupBox1.FlatStyle = FlatStyle.Flat;
			groupBox1.Location = new Point(553, 0);
			groupBox1.Margin = new Padding(4);
			groupBox1.Name = "groupBox1";
			groupBox1.Padding = new Padding(4);
			groupBox1.Size = new Size(625, 796);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			// 
			// pnlResetToken
			// 
			pnlResetToken.Controls.Add(btnConfirmReset);
			pnlResetToken.Controls.Add(btnResetPassword);
			pnlResetToken.Controls.Add(lblConfirmPasswordError);
			pnlResetToken.Controls.Add(lblPasswordError);
			pnlResetToken.Controls.Add(txtConfirmNewPassword);
			pnlResetToken.Controls.Add(txtNewPassword);
			pnlResetToken.Controls.Add(txtResetToken);
			pnlResetToken.Controls.Add(lblConfirmNewPassword);
			pnlResetToken.Controls.Add(lblNewPassword);
			pnlResetToken.Controls.Add(lblResetToken);
			pnlResetToken.Location = new Point(76, 181);
			pnlResetToken.Margin = new Padding(4);
			pnlResetToken.Name = "pnlResetToken";
			pnlResetToken.Size = new Size(500, 432);
			pnlResetToken.TabIndex = 7;
			pnlResetToken.Visible = false;
			// 
			// btnConfirmReset
			// 
			btnConfirmReset.BackColor = Color.FromArgb(40, 167, 69);
			btnConfirmReset.Cursor = Cursors.Hand;
			btnConfirmReset.FlatAppearance.BorderSize = 0;
			btnConfirmReset.FlatStyle = FlatStyle.Flat;
			btnConfirmReset.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnConfirmReset.ForeColor = Color.White;
			btnConfirmReset.Location = new Point(62, 369);
			btnConfirmReset.Name = "btnConfirmReset";
			btnConfirmReset.Size = new Size(400, 45);
			btnConfirmReset.TabIndex = 6;
			btnConfirmReset.Text = "Xác nhận đổi mật khẩu";
			btnConfirmReset.UseVisualStyleBackColor = false;
			btnConfirmReset.Click += btnConfirmReset_Click;
			// 
			// btnResetPassword
			// 
			btnResetPassword.BackColor = Color.FromArgb(214, 188, 132);
			btnResetPassword.Cursor = Cursors.Hand;
			btnResetPassword.FlatAppearance.BorderSize = 0;
			btnResetPassword.FlatStyle = FlatStyle.Flat;
			btnResetPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnResetPassword.ForeColor = Color.White;
			btnResetPassword.Location = new Point(312, 312);
			btnResetPassword.Margin = new Padding(4);
			btnResetPassword.Name = "btnResetPassword";
			btnResetPassword.Size = new Size(188, 50);
			btnResetPassword.TabIndex = 6;
			btnResetPassword.Text = "Đặt lại mật khẩu";
			btnResetPassword.UseVisualStyleBackColor = false;
			btnResetPassword.Click += btnResetPassword_Click;
			// 
			// lblConfirmPasswordError
			// 
			lblConfirmPasswordError.AutoSize = true;
			lblConfirmPasswordError.Font = new Font("Segoe UI", 9F);
			lblConfirmPasswordError.ForeColor = Color.Red;
			lblConfirmPasswordError.Location = new Point(0, 294);
			lblConfirmPasswordError.Margin = new Padding(4, 0, 4, 0);
			lblConfirmPasswordError.Name = "lblConfirmPasswordError";
			lblConfirmPasswordError.Size = new Size(0, 25);
			lblConfirmPasswordError.TabIndex = 8;
			// 
			// lblPasswordError
			// 
			lblPasswordError.AutoSize = true;
			lblPasswordError.Font = new Font("Segoe UI", 9F);
			lblPasswordError.ForeColor = Color.Red;
			lblPasswordError.Location = new Point(0, 194);
			lblPasswordError.Margin = new Padding(4, 0, 4, 0);
			lblPasswordError.Name = "lblPasswordError";
			lblPasswordError.Size = new Size(0, 25);
			lblPasswordError.TabIndex = 7;
			// 
			// txtConfirmNewPassword
			// 
			txtConfirmNewPassword.BorderStyle = BorderStyle.FixedSingle;
			txtConfirmNewPassword.Font = new Font("Segoe UI", 11F);
			txtConfirmNewPassword.Location = new Point(0, 250);
			txtConfirmNewPassword.Margin = new Padding(4);
			txtConfirmNewPassword.Name = "txtConfirmNewPassword";
			txtConfirmNewPassword.Size = new Size(500, 37);
			txtConfirmNewPassword.TabIndex = 5;
			txtConfirmNewPassword.UseSystemPasswordChar = true;
			// 
			// txtNewPassword
			// 
			txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
			txtNewPassword.Font = new Font("Segoe UI", 11F);
			txtNewPassword.Location = new Point(0, 150);
			txtNewPassword.Margin = new Padding(4);
			txtNewPassword.Name = "txtNewPassword";
			txtNewPassword.Size = new Size(500, 37);
			txtNewPassword.TabIndex = 3;
			txtNewPassword.UseSystemPasswordChar = true;
			// 
			// txtResetToken
			// 
			txtResetToken.BorderStyle = BorderStyle.FixedSingle;
			txtResetToken.Font = new Font("Segoe UI", 11F);
			txtResetToken.Location = new Point(0, 50);
			txtResetToken.Margin = new Padding(4);
			txtResetToken.Name = "txtResetToken";
			txtResetToken.Size = new Size(500, 37);
			txtResetToken.TabIndex = 1;
			// 
			// lblConfirmNewPassword
			// 
			lblConfirmNewPassword.AutoSize = true;
			lblConfirmNewPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblConfirmNewPassword.ForeColor = Color.FromArgb(64, 64, 64);
			lblConfirmNewPassword.Location = new Point(0, 212);
			lblConfirmNewPassword.Margin = new Padding(4, 0, 4, 0);
			lblConfirmNewPassword.Name = "lblConfirmNewPassword";
			lblConfirmNewPassword.Size = new Size(237, 28);
			lblConfirmNewPassword.TabIndex = 4;
			lblConfirmNewPassword.Text = "Xác nhận mật khẩu mới";
			// 
			// lblNewPassword
			// 
			lblNewPassword.AutoSize = true;
			lblNewPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblNewPassword.ForeColor = Color.FromArgb(64, 64, 64);
			lblNewPassword.Location = new Point(0, 112);
			lblNewPassword.Margin = new Padding(4, 0, 4, 0);
			lblNewPassword.Name = "lblNewPassword";
			lblNewPassword.Size = new Size(145, 28);
			lblNewPassword.TabIndex = 2;
			lblNewPassword.Text = "Mật khẩu mới";
			// 
			// lblResetToken
			// 
			lblResetToken.AutoSize = true;
			lblResetToken.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblResetToken.ForeColor = Color.FromArgb(64, 64, 64);
			lblResetToken.Location = new Point(0, 12);
			lblResetToken.Margin = new Padding(4, 0, 4, 0);
			lblResetToken.Name = "lblResetToken";
			lblResetToken.Size = new Size(133, 28);
			lblResetToken.TabIndex = 0;
			lblResetToken.Text = "Mã xác nhận";
			// 
			// lblTokenDisplay
			// 
			lblTokenDisplay.AutoSize = true;
			lblTokenDisplay.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblTokenDisplay.ForeColor = Color.FromArgb(132, 214, 147);
			lblTokenDisplay.Location = new Point(76, 199);
			lblTokenDisplay.Margin = new Padding(4, 0, 4, 0);
			lblTokenDisplay.Name = "lblTokenDisplay";
			lblTokenDisplay.Size = new Size(0, 32);
			lblTokenDisplay.TabIndex = 6;
			lblTokenDisplay.Visible = false;
			// 
			// lblEmailError
			// 
			lblEmailError.AutoSize = true;
			lblEmailError.Font = new Font("Segoe UI", 9F);
			lblEmailError.ForeColor = Color.Red;
			lblEmailError.Location = new Point(76, 180);
			lblEmailError.Margin = new Padding(4, 0, 4, 0);
			lblEmailError.Name = "lblEmailError";
			lblEmailError.Size = new Size(0, 25);
			lblEmailError.TabIndex = 8;
			// 
			// btnSendReset
			// 
			btnSendReset.BackColor = Color.FromArgb(132, 214, 147);
			btnSendReset.Cursor = Cursors.Hand;
			btnSendReset.FlatAppearance.BorderSize = 0;
			btnSendReset.FlatAppearance.MouseOverBackColor = Color.FromArgb(112, 194, 127);
			btnSendReset.FlatStyle = FlatStyle.Flat;
			btnSendReset.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnSendReset.ForeColor = Color.White;
			btnSendReset.Location = new Point(334, 633);
			btnSendReset.Margin = new Padding(4);
			btnSendReset.Name = "btnSendReset";
			btnSendReset.Size = new Size(238, 62);
			btnSendReset.TabIndex = 5;
			btnSendReset.Text = "Gửi mã xác nhận";
			btnSendReset.UseVisualStyleBackColor = false;
			btnSendReset.Click += btnSendReset_Click;
			// 
			// btnBackToLogin
			// 
			btnBackToLogin.BackColor = Color.Transparent;
			btnBackToLogin.Cursor = Cursors.Hand;
			btnBackToLogin.FlatAppearance.BorderColor = Color.FromArgb(214, 188, 132);
			btnBackToLogin.FlatStyle = FlatStyle.Flat;
			btnBackToLogin.Font = new Font("Segoe UI", 11F);
			btnBackToLogin.ForeColor = Color.FromArgb(214, 188, 132);
			btnBackToLogin.Location = new Point(76, 633);
			btnBackToLogin.Margin = new Padding(4);
			btnBackToLogin.Name = "btnBackToLogin";
			btnBackToLogin.Size = new Size(238, 62);
			btnBackToLogin.TabIndex = 4;
			btnBackToLogin.Text = "Quay lại đăng nhập";
			btnBackToLogin.UseVisualStyleBackColor = false;
			btnBackToLogin.Click += btnBackToLogin_Click;
			// 
			// txtEmail
			// 
			txtEmail.BorderStyle = BorderStyle.FixedSingle;
			txtEmail.Font = new Font("Segoe UI", 11F);
			txtEmail.Location = new Point(76, 136);
			txtEmail.Margin = new Padding(4);
			txtEmail.Name = "txtEmail";
			txtEmail.Size = new Size(500, 37);
			txtEmail.TabIndex = 3;
			txtEmail.TextChanged += txtEmail_TextChanged;
			// 
			// lblEmail
			// 
			lblEmail.AutoSize = true;
			lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblEmail.ForeColor = Color.FromArgb(64, 64, 64);
			lblEmail.Location = new Point(76, 99);
			lblEmail.Margin = new Padding(4, 0, 4, 0);
			lblEmail.Name = "lblEmail";
			lblEmail.Size = new Size(64, 28);
			lblEmail.TabIndex = 2;
			lblEmail.Text = "Email";
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.BackColor = Color.White;
			lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
			lblTitle.ForeColor = Color.Teal;
			lblTitle.Location = new Point(90, 160);
			lblTitle.Margin = new Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(374, 65);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Quên mật khẩu";
			// 
			// lblSubtitle
			// 
			lblSubtitle.AutoSize = true;
			lblSubtitle.BackColor = Color.White;
			lblSubtitle.Font = new Font("Segoe UI", 10F);
			lblSubtitle.ForeColor = Color.Gray;
			lblSubtitle.Location = new Point(100, 557);
			lblSubtitle.Margin = new Padding(4, 0, 4, 0);
			lblSubtitle.Name = "lblSubtitle";
			lblSubtitle.Size = new Size(364, 28);
			lblSubtitle.TabIndex = 1;
			lblSubtitle.Text = "Nhập email để nhận mã đặt lại mật khẩu";
			// 
			// pictureBox1
			// 
			pictureBox1.BackColor = Color.White;
			pictureBox1.Dock = DockStyle.Left;
			pictureBox1.Image = Properties.Resources.logo;
			pictureBox1.Location = new Point(0, 0);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(560, 774);
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox1.TabIndex = 2;
			pictureBox1.TabStop = false;
			// 
			// quenmatkhau
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(240, 242, 245);
			ClientSize = new Size(1178, 774);
			Controls.Add(groupBox1);
			Controls.Add(lblTitle);
			Controls.Add(lblSubtitle);
			Controls.Add(pictureBox1);
			Margin = new Padding(4);
			Name = "quenmatkhau";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Quên mật khẩu - YMEDU Learning Platform";
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			pnlResetToken.ResumeLayout(false);
			pnlResetToken.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
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
        private System.Windows.Forms.Button btnConfirmReset;
		private PictureBox pictureBox1;
	}
}