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
            lblFullNameError = new Label();
            lblUsernameError = new Label();
            lblEmailError = new Label();
            lblPasswordError = new Label();
            lblConfirmPasswordError = new Label();
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
            groupBox1.SuspendLayout();
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
            groupBox1.Controls.Add(lblTitle);
            groupBox1.Controls.Add(lblSubtitle);
            groupBox1.Location = new Point(150, 30);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(500, 720);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.FlatStyle = FlatStyle.Flat;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = WinFormsApp1.Helpers.ColorPalette.ButtonPrimary;
            lblTitle.Location = new Point(170, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(160, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Đăng ký";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.Gray;
            lblSubtitle.Location = new Point(130, 90);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(240, 23);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Tạo tài khoản mới để bắt đầu học tập";
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFullName.ForeColor = Color.FromArgb(64, 64, 64);
            lblFullName.Location = new Point(50, 130);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(69, 23);
            lblFullName.TabIndex = 2;
            lblFullName.Text = "Họ tên";
            // 
            // txtFullName
            // 
            txtFullName.BorderStyle = BorderStyle.FixedSingle;
            txtFullName.Font = new Font("Segoe UI", 11F);
            txtFullName.Location = new Point(50, 160);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(400, 32);
            txtFullName.TabIndex = 3;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(64, 64, 64);
            lblUsername.Location = new Point(50, 210);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(130, 23);
            lblUsername.TabIndex = 4;
            lblUsername.Text = "Tên đăng nhập";
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.Location = new Point(50, 240);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(400, 32);
            txtUsername.TabIndex = 5;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(64, 64, 64);
            lblEmail.Location = new Point(50, 290);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(54, 23);
            lblEmail.TabIndex = 6;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.Location = new Point(50, 320);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(400, 32);
            txtEmail.TabIndex = 7;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPhone.ForeColor = Color.FromArgb(64, 64, 64);
            lblPhone.Location = new Point(50, 370);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(115, 23);
            lblPhone.TabIndex = 8;
            lblPhone.Text = "Số điện thoại";
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Font = new Font("Segoe UI", 11F);
            txtPhone.Location = new Point(50, 400);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(400, 32);
            txtPhone.TabIndex = 9;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(64, 64, 64);
            lblPassword.Location = new Point(50, 450);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(87, 23);
            lblPassword.TabIndex = 10;
            lblPassword.Text = "Mật khẩu";
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(50, 480);
            txtPassword.Name = "txtPassword";
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.Size = new Size(400, 32);
            txtPassword.TabIndex = 11;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblConfirmPassword.ForeColor = Color.FromArgb(64, 64, 64);
            lblConfirmPassword.Location = new Point(50, 530);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(154, 23);
            lblConfirmPassword.TabIndex = 12;
            lblConfirmPassword.Text = "Xác nhận mật khẩu";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.Font = new Font("Segoe UI", 11F);
            txtConfirmPassword.Location = new Point(50, 560);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.Size = new Size(400, 32);
            txtConfirmPassword.TabIndex = 13;
            // 
            // btnBackToLogin
            // 
            btnBackToLogin.BackColor = Color.Transparent;
            btnBackToLogin.FlatStyle = FlatStyle.Flat;
            btnBackToLogin.FlatAppearance.BorderSize = 1;
            btnBackToLogin.FlatAppearance.BorderColor = WinFormsApp1.Helpers.ColorPalette.ButtonPrimary;
            btnBackToLogin.Font = new Font("Segoe UI", 11F);
            btnBackToLogin.ForeColor = WinFormsApp1.Helpers.ColorPalette.ButtonPrimary;
            btnBackToLogin.Location = new Point(50, 650);
            btnBackToLogin.Name = "btnBackToLogin";
            btnBackToLogin.Size = new Size(190, 50);
            btnBackToLogin.TabIndex = 14;
            btnBackToLogin.Text = "Quay lại đăng nhập";
            btnBackToLogin.UseVisualStyleBackColor = false;
            btnBackToLogin.Cursor = Cursors.Hand;
            btnBackToLogin.Click += btnBackToLogin_Click;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = WinFormsApp1.Helpers.ColorPalette.ButtonSecondary;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.FlatAppearance.MouseOverBackColor = WinFormsApp1.Helpers.ColorPalette.SecondaryDark;
            btnRegister.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(260, 650);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(190, 50);
            btnRegister.TabIndex = 15;
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.Click += btnRegister_Click;
            // 
            // lblFullNameError
            // 
            lblFullNameError.AutoSize = true;
            lblFullNameError.Font = new Font("Segoe UI", 9F);
            lblFullNameError.ForeColor = Color.Red;
            lblFullNameError.Location = new Point(50, 195);
            lblFullNameError.Name = "lblFullNameError";
            lblFullNameError.Size = new Size(0, 20);
            lblFullNameError.TabIndex = 16;
            // 
            // lblUsernameError
            // 
            lblUsernameError.AutoSize = true;
            lblUsernameError.Font = new Font("Segoe UI", 9F);
            lblUsernameError.ForeColor = Color.Red;
            lblUsernameError.Location = new Point(50, 275);
            lblUsernameError.Name = "lblUsernameError";
            lblUsernameError.Size = new Size(0, 20);
            lblUsernameError.TabIndex = 17;
            // 
            // lblEmailError
            // 
            lblEmailError.AutoSize = true;
            lblEmailError.Font = new Font("Segoe UI", 9F);
            lblEmailError.ForeColor = Color.Red;
            lblEmailError.Location = new Point(50, 355);
            lblEmailError.Name = "lblEmailError";
            lblEmailError.Size = new Size(0, 20);
            lblEmailError.TabIndex = 18;
            // 
            // lblPasswordError
            // 
            lblPasswordError.AutoSize = true;
            lblPasswordError.Font = new Font("Segoe UI", 9F);
            lblPasswordError.ForeColor = Color.Red;
            lblPasswordError.Location = new Point(50, 515);
            lblPasswordError.Name = "lblPasswordError";
            lblPasswordError.Size = new Size(0, 20);
            lblPasswordError.TabIndex = 19;
            // 
            // lblConfirmPasswordError
            // 
            lblConfirmPasswordError.AutoSize = true;
            lblConfirmPasswordError.Font = new Font("Segoe UI", 9F);
            lblConfirmPasswordError.ForeColor = Color.Red;
            lblConfirmPasswordError.Location = new Point(50, 595);
            lblConfirmPasswordError.Name = "lblConfirmPasswordError";
            lblConfirmPasswordError.Size = new Size(0, 20);
            lblConfirmPasswordError.TabIndex = 20;
            // 
            // dangky
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(800, 780);
            Controls.Add(groupBox1);
            Name = "dangky";
            Text = "Đăng ký - YMEDU Learning Platform";
            StartPosition = FormStartPosition.CenterScreen;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
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
    }
}