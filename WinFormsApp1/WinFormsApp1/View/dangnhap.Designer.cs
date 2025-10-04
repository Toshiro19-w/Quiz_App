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
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(textTK);
            groupBox1.Controls.Add(LableMK);
            groupBox1.Controls.Add(LableTK);
            groupBox1.Location = new Point(395, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(403, 447);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Đăng nhập";
            // 
            // button2
            // 
            button2.Location = new Point(152, 380);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 5;
            button2.Text = "Đăng nhập";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.BackgroundImageLayout = ImageLayout.None;
            button1.Location = new Point(219, 330);
            button1.Name = "button1";
            button1.Size = new Size(161, 34);
            button1.TabIndex = 4;
            button1.Text = "Quên mật khẩu";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(29, 278);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(351, 46);
            textBox2.TabIndex = 3;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // textTK
            // 
            textTK.Location = new Point(29, 201);
            textTK.Multiline = true;
            textTK.Name = "textTK";
            textTK.Size = new Size(351, 46);
            textTK.TabIndex = 2;
            textTK.TextChanged += textBox1_TextChanged;
            // 
            // LableMK
            // 
            LableMK.AutoSize = true;
            LableMK.Location = new Point(45, 250);
            LableMK.Name = "LableMK";
            LableMK.Size = new Size(161, 25);
            LableMK.TabIndex = 1;
            LableMK.Text = "Email và mật khẩu ";
            // 
            // LableTK
            // 
            LableTK.AutoSize = true;
            LableTK.Location = new Point(45, 169);
            LableTK.Name = "LableTK";
            LableTK.Size = new Size(161, 25);
            LableTK.TabIndex = 0;
            LableTK.Text = "Email và mật khẩu ";
            // 
            // dangnhap
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Name = "dangnhap";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
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
    }
}