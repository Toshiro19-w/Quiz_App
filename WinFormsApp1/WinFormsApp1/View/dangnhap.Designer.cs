namespace WinFormsApp1
{
    partial class dangnhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			groupBox1 = new GroupBox();
			button2 = new Button();
			button1 = new Button();
			btnRegister = new Button();
			chkRememberMe = new CheckBox();
			lblPasswordError = new Label();
			lblEmailError = new Label();
			textBox2 = new TextBox();
			textTK = new TextBox();
			LableMK = new Label();
			LableTK = new Label();
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
			groupBox1.Controls.Add(button2);
			groupBox1.Controls.Add(button1);
			groupBox1.Controls.Add(btnRegister);
			groupBox1.Controls.Add(chkRememberMe);
			groupBox1.Controls.Add(lblPasswordError);
			groupBox1.Controls.Add(lblEmailError);
			groupBox1.Controls.Add(textBox2);
			groupBox1.Controls.Add(textTK);
			groupBox1.Controls.Add(LableMK);
			groupBox1.Controls.Add(LableTK);
			groupBox1.FlatStyle = FlatStyle.Flat;
			groupBox1.Location = new Point(548, 2);
			groupBox1.Margin = new Padding(4);
			groupBox1.Name = "groupBox1";
			groupBox1.Padding = new Padding(4);
			groupBox1.Size = new Size(686, 772);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			// 
			// button2
			// 
			button2.BackColor = Color.FromArgb(132, 214, 147);
			button2.Cursor = Cursors.Hand;
			button2.FlatAppearance.BorderSize = 0;
			button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(112, 194, 127);
			button2.FlatStyle = FlatStyle.Flat;
			button2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			button2.ForeColor = Color.White;
			button2.Location = new Point(105, 500);
			button2.Margin = new Padding(4);
			button2.Name = "button2";
			button2.Size = new Size(438, 62);
			button2.TabIndex = 5;
			button2.Text = "Đăng nhập";
			button2.UseVisualStyleBackColor = false;
			button2.Click += btnLogin_Click;
			// 
			// button1
			// 
			button1.BackColor = Color.Transparent;
			button1.Cursor = Cursors.Hand;
			button1.FlatAppearance.BorderSize = 0;
			button1.FlatStyle = FlatStyle.Flat;
			button1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
			button1.ForeColor = Color.Teal;
			button1.Location = new Point(356, 421);
			button1.Margin = new Padding(4);
			button1.Name = "button1";
			button1.Size = new Size(188, 44);
			button1.TabIndex = 4;
			button1.Text = "Quên mật khẩu?";
			button1.UseVisualStyleBackColor = false;
			button1.Click += button1_Click;
			// 
			// btnRegister
			// 
			btnRegister.BackColor = Color.Transparent;
			btnRegister.Cursor = Cursors.Hand;
			btnRegister.FlatAppearance.BorderColor = Color.FromArgb(214, 188, 132);
			btnRegister.FlatStyle = FlatStyle.Flat;
			btnRegister.Font = new Font("Segoe UI", 10F);
			btnRegister.ForeColor = Color.Teal;
			btnRegister.Location = new Point(105, 581);
			btnRegister.Margin = new Padding(4);
			btnRegister.Name = "btnRegister";
			btnRegister.Size = new Size(438, 50);
			btnRegister.TabIndex = 6;
			btnRegister.Text = "Chưa có tài khoản? Đăng ký ngay";
			btnRegister.UseVisualStyleBackColor = false;
			btnRegister.Click += btnRegister_Click;
			// 
			// chkRememberMe
			// 
			chkRememberMe.AutoSize = true;
			chkRememberMe.Font = new Font("Segoe UI", 9F);
			chkRememberMe.ForeColor = Color.FromArgb(64, 64, 64);
			chkRememberMe.Location = new Point(106, 430);
			chkRememberMe.Margin = new Padding(4);
			chkRememberMe.Name = "chkRememberMe";
			chkRememberMe.Size = new Size(191, 29);
			chkRememberMe.TabIndex = 7;
			chkRememberMe.Text = "Ghi nhớ đăng nhập";
			chkRememberMe.UseVisualStyleBackColor = true;
			// 
			// lblPasswordError
			// 
			lblPasswordError.AutoSize = true;
			lblPasswordError.Font = new Font("Segoe UI", 9F);
			lblPasswordError.ForeColor = Color.Red;
			lblPasswordError.Location = new Point(106, 390);
			lblPasswordError.Margin = new Padding(4, 0, 4, 0);
			lblPasswordError.Name = "lblPasswordError";
			lblPasswordError.Size = new Size(0, 25);
			lblPasswordError.TabIndex = 9;
			// 
			// lblEmailError
			// 
			lblEmailError.AutoSize = true;
			lblEmailError.Font = new Font("Segoe UI", 9F);
			lblEmailError.ForeColor = Color.Red;
			lblEmailError.Location = new Point(106, 264);
			lblEmailError.Margin = new Padding(4, 0, 4, 0);
			lblEmailError.Name = "lblEmailError";
			lblEmailError.Size = new Size(0, 25);
			lblEmailError.TabIndex = 8;
			// 
			// textBox2
			// 
			textBox2.BorderStyle = BorderStyle.FixedSingle;
			textBox2.Font = new Font("Segoe UI", 11F);
			textBox2.Location = new Point(106, 343);
			textBox2.Margin = new Padding(4);
			textBox2.Name = "textBox2";
			textBox2.Size = new Size(437, 37);
			textBox2.TabIndex = 3;
			textBox2.UseSystemPasswordChar = true;
			textBox2.TextChanged += textBox2_TextChanged;
			// 
			// textTK
			// 
			textTK.BorderStyle = BorderStyle.FixedSingle;
			textTK.Font = new Font("Segoe UI", 11F);
			textTK.Location = new Point(106, 217);
			textTK.Margin = new Padding(4);
			textTK.Name = "textTK";
			textTK.Size = new Size(437, 37);
			textTK.TabIndex = 2;
			textTK.TextChanged += textBox1_TextChanged;
			// 
			// LableMK
			// 
			LableMK.AutoSize = true;
			LableMK.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			LableMK.ForeColor = Color.FromArgb(64, 64, 64);
			LableMK.Location = new Point(106, 305);
			LableMK.Margin = new Padding(4, 0, 4, 0);
			LableMK.Name = "LableMK";
			LableMK.Size = new Size(102, 28);
			LableMK.TabIndex = 1;
			LableMK.Text = "Mật khẩu";
			// 
			// LableTK
			// 
			LableTK.AutoSize = true;
			LableTK.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			LableTK.ForeColor = Color.FromArgb(64, 64, 64);
			LableTK.Location = new Point(106, 180);
			LableTK.Margin = new Padding(4, 0, 4, 0);
			LableTK.Name = "LableTK";
			LableTK.Size = new Size(64, 28);
			LableTK.TabIndex = 0;
			LableTK.Text = "Email";
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.BackColor = Color.White;
			lblTitle.Font = new Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblTitle.ForeColor = Color.DarkSlateGray;
			lblTitle.Location = new Point(113, 149);
			lblTitle.Margin = new Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(313, 70);
			lblTitle.TabIndex = 6;
			lblTitle.Text = "Chào mừng";
			// 
			// lblSubtitle
			// 
			lblSubtitle.AutoSize = true;
			lblSubtitle.BackColor = Color.White;
			lblSubtitle.Font = new Font("Segoe UI", 10F);
			lblSubtitle.ForeColor = Color.Gray;
			lblSubtitle.Location = new Point(95, 556);
			lblSubtitle.Margin = new Padding(4, 0, 4, 0);
			lblSubtitle.Name = "lblSubtitle";
			lblSubtitle.Size = new Size(366, 28);
			lblSubtitle.TabIndex = 7;
			lblSubtitle.Text = "Đăng nhập để tiếp tục sử dụng hệ thống";
			// 
			// pictureBox1
			// 
			pictureBox1.BackColor = Color.White;
			pictureBox1.Dock = DockStyle.Left;
			pictureBox1.Image = Properties.Resources.logo;
			pictureBox1.Location = new Point(0, 0);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(551, 774);
			pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
			pictureBox1.TabIndex = 1;
			pictureBox1.TabStop = false;
			// 
			// dangnhap
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(0, 192, 192);
			ClientSize = new Size(1178, 774);
			Controls.Add(lblTitle);
			Controls.Add(lblSubtitle);
			Controls.Add(pictureBox1);
			Controls.Add(groupBox1);
			Margin = new Padding(4);
			Name = "dangnhap";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Đăng nhập - YMEDU Learning Platform";
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private GroupBox groupBox1;
        private TextBox textBox2;
        private TextBox textTK;
        private Label LableMK;
        private Label LableTK;
        private Button button1;
        private Button button2;
        private Button btnRegister;
        private CheckBox chkRememberMe;
        private Label lblEmailError;
        private Label lblPasswordError;
        private Label lblTitle;
        private Label lblSubtitle;
		private PictureBox pictureBox1;
	}
}