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
            textBox2 = new TextBox();
            textTK = new TextBox();
            LableMK = new Label();
            LableTK = new Label();
            lblTitle = new Label();
            lblSubtitle = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(textTK);
            groupBox1.Controls.Add(LableMK);
            groupBox1.Controls.Add(LableTK);
            groupBox1.Controls.Add(lblTitle);
            groupBox1.Controls.Add(lblSubtitle);
            groupBox1.Location = new Point(200, 50);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(450, 550);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.FlatStyle = FlatStyle.Flat;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = WinFormsApp1.Helpers.ColorPalette.ButtonPrimary;
            lblTitle.Location = new Point(120, 40);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(210, 54);
            lblTitle.TabIndex = 6;
            lblTitle.Text = "Chào mừng";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.Gray;
            lblSubtitle.Location = new Point(100, 100);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(250, 23);
            lblSubtitle.TabIndex = 7;
            lblSubtitle.Text = "Đăng nhập để tiếp tục sử dụng hệ thống";
            // 
            // button2
            // 
            button2.BackColor = WinFormsApp1.Helpers.ColorPalette.ButtonSecondary;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseOverBackColor = WinFormsApp1.Helpers.ColorPalette.SecondaryDark;
            button2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            button2.ForeColor = Color.White;
            button2.Location = new Point(50, 450);
            button2.Name = "button2";
            button2.Size = new Size(350, 50);
            button2.TabIndex = 5;
            button2.Text = "Đăng nhập";
            button2.UseVisualStyleBackColor = false;
            button2.Cursor = Cursors.Hand;
            button2.Click += btnLogin_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Font = new Font("Segoe UI", 9F);
            button1.ForeColor = WinFormsApp1.Helpers.ColorPalette.ButtonPrimary;
            button1.Location = new Point(150, 400);
            button1.Name = "button1";
            button1.Size = new Size(150, 35);
            button1.TabIndex = 4;
            button1.Text = "Quên mật khẩu?";
            button1.UseVisualStyleBackColor = false;
            button1.Cursor = Cursors.Hand;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Font = new Font("Segoe UI", 11F);
            textBox2.Location = new Point(50, 310);
            textBox2.Name = "textBox2";
            textBox2.UseSystemPasswordChar = true;
            textBox2.Size = new Size(350, 32);
            textBox2.TabIndex = 3;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // textTK
            // 
            textTK.BorderStyle = BorderStyle.FixedSingle;
            textTK.Font = new Font("Segoe UI", 11F);
            textTK.Location = new Point(50, 210);
            textTK.Name = "textTK";
            textTK.Size = new Size(350, 32);
            textTK.TabIndex = 2;
            textTK.TextChanged += textBox1_TextChanged;
            // 
            // LableMK
            // 
            LableMK.AutoSize = true;
            LableMK.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            LableMK.ForeColor = Color.FromArgb(64, 64, 64);
            LableMK.Location = new Point(50, 280);
            LableMK.Name = "LableMK";
            LableMK.Size = new Size(87, 23);
            LableMK.TabIndex = 1;
            LableMK.Text = "Mật khẩu";
            // 
            // LableTK
            // 
            LableTK.AutoSize = true;
            LableTK.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            LableTK.ForeColor = Color.FromArgb(64, 64, 64);
            LableTK.Location = new Point(50, 180);
            LableTK.Name = "LableTK";
            LableTK.Size = new Size(54, 23);
            LableTK.TabIndex = 0;
            LableTK.Text = "Email";
            // 
            // dangnhap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(850, 650);
            Controls.Add(groupBox1);
            Name = "dangnhap";
            Text = "Đăng nhập - Valt Learning Platform";
            StartPosition = FormStartPosition.CenterScreen;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox textBox2;
        private TextBox textTK;
        private Label LableMK;
        private Label LableTK;
        private Button button1;
        private Button button2;
        private Label lblTitle;
        private Label lblSubtitle;
    }
}