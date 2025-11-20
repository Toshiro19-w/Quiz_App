namespace WinFormsApp1.View
{
    partial class dangky
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
			btnRegister = new Button();
			btnBackToLogin = new Button();
			lblConfirmPasswordError = new Label();
			lblPasswordError = new Label();
			lblEmailError = new Label();
			lblUsernameError = new Label();
			lblFullNameError = new Label();
			txtConfirmPassword = new TextBox();
			txtPassword = new TextBox();
			txtPhone = new TextBox();
			txtEmail = new TextBox();
			txtUsername = new TextBox();
			txtFullName = new TextBox();
			lblConfirmPassword = new Label();
			lblPassword = new Label();
			lblPhone = new Label();
			lblEmail = new Label();
			lblUsername = new Label();
			lblFullName = new Label();
			lblTitle = new Label();
			lblSubtitle = new Label();
			pictureBox1 = new PictureBox();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// groupBox1
			// 
			groupBox1.BackColor = Color.White;
			groupBox1.Controls.Add(btnRegister);
			groupBox1.Controls.Add(btnBackToLogin);
			groupBox1.Controls.Add(lblConfirmPasswordError);
			groupBox1.Controls.Add(lblPasswordError);
			groupBox1.Controls.Add(lblEmailError);
			groupBox1.Controls.Add(lblUsernameError);
			groupBox1.Controls.Add(lblFullNameError);
			groupBox1.Controls.Add(txtConfirmPassword);
			groupBox1.Controls.Add(txtPassword);
			groupBox1.Controls.Add(txtPhone);
			groupBox1.Controls.Add(txtEmail);
			groupBox1.Controls.Add(txtUsername);
			groupBox1.Controls.Add(txtFullName);
			groupBox1.Controls.Add(lblConfirmPassword);
			groupBox1.Controls.Add(lblPassword);
			groupBox1.Controls.Add(lblPhone);
			groupBox1.Controls.Add(lblEmail);
			groupBox1.Controls.Add(lblUsername);
			groupBox1.Controls.Add(lblFullName);
			groupBox1.FlatStyle = FlatStyle.Flat;
			groupBox1.Location = new Point(558, 0);
			groupBox1.Margin = new Padding(4);
			groupBox1.Name = "groupBox1";
			groupBox1.Padding = new Padding(4);
			groupBox1.Size = new Size(654, 900);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			// 
			// btnRegister
			// 
			btnRegister.BackColor = Color.FromArgb(132, 214, 147);
			btnRegister.Cursor = Cursors.Hand;
			btnRegister.FlatAppearance.BorderSize = 0;
			btnRegister.FlatAppearance.MouseOverBackColor = Color.FromArgb(112, 194, 127);
			btnRegister.FlatStyle = FlatStyle.Flat;
			btnRegister.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnRegister.ForeColor = Color.White;
			btnRegister.Location = new Point(333, 697);
			btnRegister.Margin = new Padding(4);
			btnRegister.Name = "btnRegister";
			btnRegister.Size = new Size(238, 62);
			btnRegister.TabIndex = 15;
			btnRegister.Text = "Đăng ký";
			btnRegister.UseVisualStyleBackColor = false;
			btnRegister.Click += btnRegister_Click;
			// 
			// btnBackToLogin
			// 
			btnBackToLogin.BackColor = Color.Transparent;
			btnBackToLogin.Cursor = Cursors.Hand;
			btnBackToLogin.FlatAppearance.BorderColor = Color.FromArgb(214, 188, 132);
			btnBackToLogin.FlatStyle = FlatStyle.Flat;
			btnBackToLogin.Font = new Font("Segoe UI", 11F);
			btnBackToLogin.ForeColor = Color.FromArgb(214, 188, 132);
			btnBackToLogin.Location = new Point(70, 697);
			btnBackToLogin.Margin = new Padding(4);
			btnBackToLogin.Name = "btnBackToLogin";
			btnBackToLogin.Size = new Size(238, 62);
			btnBackToLogin.TabIndex = 14;
			btnBackToLogin.Text = "Quay lại đăng nhập";
			btnBackToLogin.UseVisualStyleBackColor = false;
			btnBackToLogin.Click += btnBackToLogin_Click;
			// 
			// lblConfirmPasswordError
			// 
			lblConfirmPasswordError.AutoSize = true;
			lblConfirmPasswordError.Font = new Font("Segoe UI", 9F);
			lblConfirmPasswordError.ForeColor = Color.Red;
			lblConfirmPasswordError.Location = new Point(71, 637);
			lblConfirmPasswordError.Margin = new Padding(4, 0, 4, 0);
			lblConfirmPasswordError.Name = "lblConfirmPasswordError";
			lblConfirmPasswordError.Size = new Size(0, 25);
			lblConfirmPasswordError.TabIndex = 20;
			// 
			// lblPasswordError
			// 
			lblPasswordError.AutoSize = true;
			lblPasswordError.Font = new Font("Segoe UI", 9F);
			lblPasswordError.ForeColor = Color.Red;
			lblPasswordError.Location = new Point(71, 537);
			lblPasswordError.Margin = new Padding(4, 0, 4, 0);
			lblPasswordError.Name = "lblPasswordError";
			lblPasswordError.Size = new Size(0, 25);
			lblPasswordError.TabIndex = 19;
			// 
			// lblEmailError
			// 
			lblEmailError.AutoSize = true;
			lblEmailError.Font = new Font("Segoe UI", 9F);
			lblEmailError.ForeColor = Color.Red;
			lblEmailError.Location = new Point(71, 337);
			lblEmailError.Margin = new Padding(4, 0, 4, 0);
			lblEmailError.Name = "lblEmailError";
			lblEmailError.Size = new Size(0, 25);
			lblEmailError.TabIndex = 18;
			// 
			// lblUsernameError
			// 
			lblUsernameError.AutoSize = true;
			lblUsernameError.Font = new Font("Segoe UI", 9F);
			lblUsernameError.ForeColor = Color.Red;
			lblUsernameError.Location = new Point(71, 237);
			lblUsernameError.Margin = new Padding(4, 0, 4, 0);
			lblUsernameError.Name = "lblUsernameError";
			lblUsernameError.Size = new Size(0, 25);
			lblUsernameError.TabIndex = 17;
			// 
			// lblFullNameError
			// 
			lblFullNameError.AutoSize = true;
			lblFullNameError.Font = new Font("Segoe UI", 9F);
			lblFullNameError.ForeColor = Color.Red;
			lblFullNameError.Location = new Point(71, 137);
			lblFullNameError.Margin = new Padding(4, 0, 4, 0);
			lblFullNameError.Name = "lblFullNameError";
			lblFullNameError.Size = new Size(0, 25);
			lblFullNameError.TabIndex = 16;
			// 
			// txtConfirmPassword
			// 
			txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
			txtConfirmPassword.Font = new Font("Segoe UI", 11F);
			txtConfirmPassword.Location = new Point(71, 593);
			txtConfirmPassword.Margin = new Padding(4);
			txtConfirmPassword.Name = "txtConfirmPassword";
			txtConfirmPassword.Size = new Size(500, 37);
			txtConfirmPassword.TabIndex = 13;
			txtConfirmPassword.UseSystemPasswordChar = true;
			// 
			// txtPassword
			// 
			txtPassword.BorderStyle = BorderStyle.FixedSingle;
			txtPassword.Font = new Font("Segoe UI", 11F);
			txtPassword.Location = new Point(71, 493);
			txtPassword.Margin = new Padding(4);
			txtPassword.Name = "txtPassword";
			txtPassword.Size = new Size(500, 37);
			txtPassword.TabIndex = 11;
			txtPassword.UseSystemPasswordChar = true;
			// 
			// txtPhone
			// 
			txtPhone.BorderStyle = BorderStyle.FixedSingle;
			txtPhone.Font = new Font("Segoe UI", 11F);
			txtPhone.Location = new Point(71, 393);
			txtPhone.Margin = new Padding(4);
			txtPhone.Name = "txtPhone";
			txtPhone.Size = new Size(500, 37);
			txtPhone.TabIndex = 9;
			// 
			// txtEmail
			// 
			txtEmail.BorderStyle = BorderStyle.FixedSingle;
			txtEmail.Font = new Font("Segoe UI", 11F);
			txtEmail.Location = new Point(71, 293);
			txtEmail.Margin = new Padding(4);
			txtEmail.Name = "txtEmail";
			txtEmail.Size = new Size(500, 37);
			txtEmail.TabIndex = 7;
			// 
			// txtUsername
			// 
			txtUsername.BorderStyle = BorderStyle.FixedSingle;
			txtUsername.Font = new Font("Segoe UI", 11F);
			txtUsername.Location = new Point(71, 193);
			txtUsername.Margin = new Padding(4);
			txtUsername.Name = "txtUsername";
			txtUsername.Size = new Size(500, 37);
			txtUsername.TabIndex = 5;
			// 
			// txtFullName
			// 
			txtFullName.BorderStyle = BorderStyle.FixedSingle;
			txtFullName.Font = new Font("Segoe UI", 11F);
			txtFullName.Location = new Point(71, 93);
			txtFullName.Margin = new Padding(4);
			txtFullName.Name = "txtFullName";
			txtFullName.Size = new Size(500, 37);
			txtFullName.TabIndex = 3;
			// 
			// lblConfirmPassword
			// 
			lblConfirmPassword.AutoSize = true;
			lblConfirmPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblConfirmPassword.ForeColor = Color.FromArgb(64, 64, 64);
			lblConfirmPassword.Location = new Point(71, 555);
			lblConfirmPassword.Margin = new Padding(4, 0, 4, 0);
			lblConfirmPassword.Name = "lblConfirmPassword";
			lblConfirmPassword.Size = new Size(194, 28);
			lblConfirmPassword.TabIndex = 12;
			lblConfirmPassword.Text = "Xác nhận mật khẩu";
			// 
			// lblPassword
			// 
			lblPassword.AutoSize = true;
			lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblPassword.ForeColor = Color.FromArgb(64, 64, 64);
			lblPassword.Location = new Point(71, 455);
			lblPassword.Margin = new Padding(4, 0, 4, 0);
			lblPassword.Name = "lblPassword";
			lblPassword.Size = new Size(102, 28);
			lblPassword.TabIndex = 10;
			lblPassword.Text = "Mật khẩu";
			// 
			// lblPhone
			// 
			lblPhone.AutoSize = true;
			lblPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblPhone.ForeColor = Color.FromArgb(64, 64, 64);
			lblPhone.Location = new Point(71, 355);
			lblPhone.Margin = new Padding(4, 0, 4, 0);
			lblPhone.Name = "lblPhone";
			lblPhone.Size = new Size(138, 28);
			lblPhone.TabIndex = 8;
			lblPhone.Text = "Số điện thoại";
			// 
			// lblEmail
			// 
			lblEmail.AutoSize = true;
			lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblEmail.ForeColor = Color.FromArgb(64, 64, 64);
			lblEmail.Location = new Point(71, 255);
			lblEmail.Margin = new Padding(4, 0, 4, 0);
			lblEmail.Name = "lblEmail";
			lblEmail.Size = new Size(64, 28);
			lblEmail.TabIndex = 6;
			lblEmail.Text = "Email";
			// 
			// lblUsername
			// 
			lblUsername.AutoSize = true;
			lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblUsername.ForeColor = Color.FromArgb(64, 64, 64);
			lblUsername.Location = new Point(71, 155);
			lblUsername.Margin = new Padding(4, 0, 4, 0);
			lblUsername.Name = "lblUsername";
			lblUsername.Size = new Size(152, 28);
			lblUsername.TabIndex = 4;
			lblUsername.Text = "Tên đăng nhập";
			// 
			// lblFullName
			// 
			lblFullName.AutoSize = true;
			lblFullName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblFullName.ForeColor = Color.FromArgb(64, 64, 64);
			lblFullName.Location = new Point(71, 55);
			lblFullName.Margin = new Padding(4, 0, 4, 0);
			lblFullName.Name = "lblFullName";
			lblFullName.Size = new Size(76, 28);
			lblFullName.TabIndex = 2;
			lblFullName.Text = "Họ tên";
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.BackColor = Color.White;
			lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
			lblTitle.ForeColor = Color.Teal;
			lblTitle.Location = new Point(172, 137);
			lblTitle.Margin = new Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(214, 65);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Đăng ký";
			// 
			// lblSubtitle
			// 
			lblSubtitle.AutoSize = true;
			lblSubtitle.BackColor = Color.White;
			lblSubtitle.Font = new Font("Segoe UI", 10F);
			lblSubtitle.ForeColor = Color.Gray;
			lblSubtitle.Location = new Point(116, 555);
			lblSubtitle.Margin = new Padding(4, 0, 4, 0);
			lblSubtitle.Name = "lblSubtitle";
			lblSubtitle.Size = new Size(339, 28);
			lblSubtitle.TabIndex = 1;
			lblSubtitle.Text = "Tạo tài khoản mới để bắt đầu học tập";
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
			pictureBox1.TabIndex = 1;
			pictureBox1.TabStop = false;
			// 
			// dangky
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(240, 242, 245);
			ClientSize = new Size(1178, 774);
			Controls.Add(lblSubtitle);
			Controls.Add(groupBox1);
			Controls.Add(lblTitle);
			Controls.Add(pictureBox1);
			Margin = new Padding(4);
			Name = "dangky";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Đăng ký - YMEDU Learning Platform";
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private GroupBox groupBox1;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Button btnBackToLogin;
        private Button btnRegister;
        private Label lblFullNameError;
        private Label lblUsernameError;
        private Label lblEmailError;
        private Label lblPasswordError;
        private Label lblConfirmPasswordError;
		private PictureBox pictureBox1;
	}
}