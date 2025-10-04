namespace WinFormsApp1
{
    partial class TrangChu
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrangChu));
            panel1 = new Panel();
            Dashbroad = new Label();
            LOGO = new PictureBox();
            SideBar = new Panel();
            MenuContainer = new FlowLayoutPanel();
            panel8 = new Panel();
            pictureBox5 = new PictureBox();
            LessonButton = new Button();
            panel4 = new Panel();
            pictureBox1 = new PictureBox();
            LessonAssignedButton = new Button();
            panel9 = new Panel();
            pictureBox6 = new PictureBox();
            LessonCompletedButton = new Button();
            panel10 = new Panel();
            pictureBox7 = new PictureBox();
            LessonOverdueButton = new Button();
            MenuContainer2 = new FlowLayoutPanel();
            panel7 = new Panel();
            pictureBox4 = new PictureBox();
            TestButton = new Button();
            panel11 = new Panel();
            pictureBox8 = new PictureBox();
            TestAssigned = new Button();
            panel12 = new Panel();
            pictureBox9 = new PictureBox();
            TestCompletedButton = new Button();
            panel13 = new Panel();
            pictureBox10 = new PictureBox();
            TestOverdueButton = new Button();
            panel3 = new Panel();
            PicHome = new PictureBox();
            HomeButton = new Button();
            panel5 = new Panel();
            pictureBox2 = new PictureBox();
            LibraryButton = new Button();
            panel6 = new Panel();
            pictureBox3 = new PictureBox();
            ShopButton = new Button();
            MenuTransition = new System.Windows.Forms.Timer(components);
            MenuTransition2 = new System.Windows.Forms.Timer(components);
            SidebarTransition = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LOGO).BeginInit();
            SideBar.SuspendLayout();
            MenuContainer.SuspendLayout();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            MenuContainer2.SuspendLayout();
            panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PicHome).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(Dashbroad);
            panel1.Controls.Add(LOGO);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1257, 60);
            panel1.TabIndex = 0;
            // 
            // Dashbroad
            // 
            Dashbroad.AutoSize = true;
            Dashbroad.Font = new Font("Century Gothic", 14F);
            Dashbroad.Location = new Point(107, 37);
            Dashbroad.Name = "Dashbroad";
            Dashbroad.Size = new Size(164, 34);
            Dashbroad.TabIndex = 1;
            Dashbroad.Text = "dashbroad";
            // 
            // LOGO
            // 
            LOGO.Image = (Image)resources.GetObject("LOGO.Image");
            LOGO.Location = new Point(12, 12);
            LOGO.Name = "LOGO";
            LOGO.Size = new Size(33, 34);
            LOGO.SizeMode = PictureBoxSizeMode.Zoom;
            LOGO.TabIndex = 1;
            LOGO.TabStop = false;
            LOGO.Click += LOGO_Click;
            // 
            // SideBar
            // 
            SideBar.BackColor = Color.Black;
            SideBar.Controls.Add(MenuContainer);
            SideBar.Controls.Add(MenuContainer2);
            SideBar.Controls.Add(panel3);
            SideBar.Controls.Add(panel5);
            SideBar.Controls.Add(panel6);
            SideBar.Dock = DockStyle.Left;
            SideBar.Location = new Point(0, 60);
            SideBar.Name = "SideBar";
            SideBar.Size = new Size(332, 818);
            SideBar.TabIndex = 1;
            SideBar.Paint += panel2_Paint;
            // 
            // MenuContainer
            // 
            MenuContainer.BackColor = Color.FromArgb(23, 24, 29);
            MenuContainer.Controls.Add(panel8);
            MenuContainer.Controls.Add(panel4);
            MenuContainer.Controls.Add(panel9);
            MenuContainer.Controls.Add(panel10);
            MenuContainer.Location = new Point(3, 359);
            MenuContainer.Margin = new Padding(0);
            MenuContainer.Name = "MenuContainer";
            MenuContainer.Size = new Size(329, 84);
            MenuContainer.TabIndex = 10;
            // 
            // panel8
            // 
            panel8.Controls.Add(pictureBox5);
            panel8.Controls.Add(LessonButton);
            panel8.Location = new Point(0, 0);
            panel8.Margin = new Padding(0);
            panel8.Name = "panel8";
            panel8.Padding = new Padding(25, 0, 0, 0);
            panel8.Size = new Size(329, 84);
            panel8.TabIndex = 9;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Black;
            pictureBox5.BackgroundImageLayout = ImageLayout.None;
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(14, 19);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(45, 45);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 7;
            pictureBox5.TabStop = false;
            // 
            // LessonButton
            // 
            LessonButton.BackColor = Color.Black;
            LessonButton.FlatStyle = FlatStyle.Flat;
            LessonButton.ForeColor = Color.Transparent;
            LessonButton.ImageAlign = ContentAlignment.MiddleLeft;
            LessonButton.Location = new Point(-22, -36);
            LessonButton.Margin = new Padding(0);
            LessonButton.Name = "LessonButton";
            LessonButton.Size = new Size(366, 154);
            LessonButton.TabIndex = 3;
            LessonButton.Text = "Lesson";
            LessonButton.UseVisualStyleBackColor = false;
            LessonButton.Click += LessonButton_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(pictureBox1);
            panel4.Controls.Add(LessonAssignedButton);
            panel4.Location = new Point(0, 84);
            panel4.Margin = new Padding(0);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(25, 0, 0, 0);
            panel4.Size = new Size(329, 84);
            panel4.TabIndex = 8;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Black;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(14, 19);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(45, 45);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // LessonAssignedButton
            // 
            LessonAssignedButton.BackColor = Color.FromArgb(23, 24, 29);
            LessonAssignedButton.FlatStyle = FlatStyle.Flat;
            LessonAssignedButton.ForeColor = Color.Transparent;
            LessonAssignedButton.ImageAlign = ContentAlignment.MiddleLeft;
            LessonAssignedButton.Location = new Point(-22, -36);
            LessonAssignedButton.Margin = new Padding(0);
            LessonAssignedButton.Name = "LessonAssignedButton";
            LessonAssignedButton.Size = new Size(366, 154);
            LessonAssignedButton.TabIndex = 3;
            LessonAssignedButton.Text = "Assigned";
            LessonAssignedButton.UseVisualStyleBackColor = false;
            // 
            // panel9
            // 
            panel9.Controls.Add(pictureBox6);
            panel9.Controls.Add(LessonCompletedButton);
            panel9.Location = new Point(0, 168);
            panel9.Margin = new Padding(0);
            panel9.Name = "panel9";
            panel9.Padding = new Padding(25, 0, 0, 0);
            panel9.Size = new Size(329, 84);
            panel9.TabIndex = 9;
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.Black;
            pictureBox6.BackgroundImageLayout = ImageLayout.None;
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(14, 19);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(45, 45);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 7;
            pictureBox6.TabStop = false;
            // 
            // LessonCompletedButton
            // 
            LessonCompletedButton.BackColor = Color.FromArgb(23, 24, 29);
            LessonCompletedButton.FlatStyle = FlatStyle.Flat;
            LessonCompletedButton.ForeColor = Color.Transparent;
            LessonCompletedButton.ImageAlign = ContentAlignment.MiddleLeft;
            LessonCompletedButton.Location = new Point(-22, -36);
            LessonCompletedButton.Margin = new Padding(0);
            LessonCompletedButton.Name = "LessonCompletedButton";
            LessonCompletedButton.Size = new Size(366, 154);
            LessonCompletedButton.TabIndex = 3;
            LessonCompletedButton.Text = "Completed";
            LessonCompletedButton.UseVisualStyleBackColor = false;
            // 
            // panel10
            // 
            panel10.Controls.Add(pictureBox7);
            panel10.Controls.Add(LessonOverdueButton);
            panel10.Location = new Point(0, 252);
            panel10.Margin = new Padding(0);
            panel10.Name = "panel10";
            panel10.Padding = new Padding(25, 0, 0, 0);
            panel10.Size = new Size(329, 84);
            panel10.TabIndex = 10;
            // 
            // pictureBox7
            // 
            pictureBox7.BackColor = Color.Black;
            pictureBox7.BackgroundImageLayout = ImageLayout.None;
            pictureBox7.Image = (Image)resources.GetObject("pictureBox7.Image");
            pictureBox7.Location = new Point(14, 19);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(45, 45);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 7;
            pictureBox7.TabStop = false;
            // 
            // LessonOverdueButton
            // 
            LessonOverdueButton.BackColor = Color.FromArgb(23, 24, 29);
            LessonOverdueButton.FlatStyle = FlatStyle.Flat;
            LessonOverdueButton.ForeColor = Color.Transparent;
            LessonOverdueButton.ImageAlign = ContentAlignment.MiddleLeft;
            LessonOverdueButton.Location = new Point(-22, -36);
            LessonOverdueButton.Margin = new Padding(0);
            LessonOverdueButton.Name = "LessonOverdueButton";
            LessonOverdueButton.Size = new Size(366, 154);
            LessonOverdueButton.TabIndex = 3;
            LessonOverdueButton.Text = "Overdue";
            LessonOverdueButton.UseVisualStyleBackColor = false;
            // 
            // MenuContainer2
            // 
            MenuContainer2.BackColor = Color.FromArgb(23, 24, 29);
            MenuContainer2.Controls.Add(panel7);
            MenuContainer2.Controls.Add(panel11);
            MenuContainer2.Controls.Add(panel12);
            MenuContainer2.Controls.Add(panel13);
            MenuContainer2.Location = new Point(3, 443);
            MenuContainer2.Margin = new Padding(0);
            MenuContainer2.Name = "MenuContainer2";
            MenuContainer2.Size = new Size(329, 83);
            MenuContainer2.TabIndex = 11;
            MenuContainer2.Paint += MenuContainer2_Paint;
            // 
            // panel7
            // 
            panel7.Controls.Add(pictureBox4);
            panel7.Controls.Add(TestButton);
            panel7.Location = new Point(0, 0);
            panel7.Margin = new Padding(0);
            panel7.Name = "panel7";
            panel7.Padding = new Padding(25, 0, 0, 0);
            panel7.Size = new Size(329, 84);
            panel7.TabIndex = 9;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Black;
            pictureBox4.BackgroundImageLayout = ImageLayout.None;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(14, 19);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(45, 45);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 7;
            pictureBox4.TabStop = false;
            // 
            // TestButton
            // 
            TestButton.BackColor = Color.Black;
            TestButton.FlatStyle = FlatStyle.Flat;
            TestButton.ForeColor = Color.Transparent;
            TestButton.ImageAlign = ContentAlignment.MiddleLeft;
            TestButton.Location = new Point(-22, -36);
            TestButton.Margin = new Padding(0);
            TestButton.Name = "TestButton";
            TestButton.Size = new Size(366, 154);
            TestButton.TabIndex = 3;
            TestButton.Text = "Test";
            TestButton.UseVisualStyleBackColor = false;
            TestButton.Click += TestButton_Click;
            // 
            // panel11
            // 
            panel11.Controls.Add(pictureBox8);
            panel11.Controls.Add(TestAssigned);
            panel11.Location = new Point(0, 84);
            panel11.Margin = new Padding(0);
            panel11.Name = "panel11";
            panel11.Padding = new Padding(25, 0, 0, 0);
            panel11.Size = new Size(329, 84);
            panel11.TabIndex = 8;
            // 
            // pictureBox8
            // 
            pictureBox8.BackColor = Color.Black;
            pictureBox8.BackgroundImageLayout = ImageLayout.None;
            pictureBox8.Image = (Image)resources.GetObject("pictureBox8.Image");
            pictureBox8.Location = new Point(14, 19);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(45, 45);
            pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox8.TabIndex = 7;
            pictureBox8.TabStop = false;
            // 
            // TestAssigned
            // 
            TestAssigned.BackColor = Color.FromArgb(23, 24, 29);
            TestAssigned.FlatStyle = FlatStyle.Flat;
            TestAssigned.ForeColor = Color.Transparent;
            TestAssigned.ImageAlign = ContentAlignment.MiddleLeft;
            TestAssigned.Location = new Point(-22, -36);
            TestAssigned.Margin = new Padding(0);
            TestAssigned.Name = "TestAssigned";
            TestAssigned.Size = new Size(366, 154);
            TestAssigned.TabIndex = 3;
            TestAssigned.Text = "Assigned";
            TestAssigned.UseVisualStyleBackColor = false;
            // 
            // panel12
            // 
            panel12.Controls.Add(pictureBox9);
            panel12.Controls.Add(TestCompletedButton);
            panel12.Location = new Point(0, 168);
            panel12.Margin = new Padding(0);
            panel12.Name = "panel12";
            panel12.Padding = new Padding(25, 0, 0, 0);
            panel12.Size = new Size(329, 84);
            panel12.TabIndex = 9;
            // 
            // pictureBox9
            // 
            pictureBox9.BackColor = Color.Black;
            pictureBox9.BackgroundImageLayout = ImageLayout.None;
            pictureBox9.Image = (Image)resources.GetObject("pictureBox9.Image");
            pictureBox9.Location = new Point(14, 19);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(45, 45);
            pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox9.TabIndex = 7;
            pictureBox9.TabStop = false;
            // 
            // TestCompletedButton
            // 
            TestCompletedButton.BackColor = Color.FromArgb(23, 24, 29);
            TestCompletedButton.FlatStyle = FlatStyle.Flat;
            TestCompletedButton.ForeColor = Color.Transparent;
            TestCompletedButton.ImageAlign = ContentAlignment.MiddleLeft;
            TestCompletedButton.Location = new Point(-22, -36);
            TestCompletedButton.Margin = new Padding(0);
            TestCompletedButton.Name = "TestCompletedButton";
            TestCompletedButton.Size = new Size(366, 154);
            TestCompletedButton.TabIndex = 3;
            TestCompletedButton.Text = "Completed";
            TestCompletedButton.UseVisualStyleBackColor = false;
            TestCompletedButton.Click += TestCompletedButton_Click;
            // 
            // panel13
            // 
            panel13.Controls.Add(pictureBox10);
            panel13.Controls.Add(TestOverdueButton);
            panel13.Location = new Point(0, 252);
            panel13.Margin = new Padding(0);
            panel13.Name = "panel13";
            panel13.Padding = new Padding(25, 0, 0, 0);
            panel13.Size = new Size(329, 84);
            panel13.TabIndex = 10;
            // 
            // pictureBox10
            // 
            pictureBox10.BackColor = Color.Black;
            pictureBox10.BackgroundImageLayout = ImageLayout.None;
            pictureBox10.Image = (Image)resources.GetObject("pictureBox10.Image");
            pictureBox10.Location = new Point(14, 19);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(45, 45);
            pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox10.TabIndex = 7;
            pictureBox10.TabStop = false;
            // 
            // TestOverdueButton
            // 
            TestOverdueButton.BackColor = Color.FromArgb(23, 24, 29);
            TestOverdueButton.FlatStyle = FlatStyle.Flat;
            TestOverdueButton.ForeColor = Color.Transparent;
            TestOverdueButton.ImageAlign = ContentAlignment.MiddleLeft;
            TestOverdueButton.Location = new Point(-22, -36);
            TestOverdueButton.Margin = new Padding(0);
            TestOverdueButton.Name = "TestOverdueButton";
            TestOverdueButton.Size = new Size(366, 154);
            TestOverdueButton.TabIndex = 3;
            TestOverdueButton.Text = "Overdue";
            TestOverdueButton.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(PicHome);
            panel3.Controls.Add(HomeButton);
            panel3.Location = new Point(3, 70);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(25, 0, 0, 0);
            panel3.Size = new Size(329, 84);
            panel3.TabIndex = 6;
            panel3.Paint += panel3_Paint;
            // 
            // PicHome
            // 
            PicHome.BackColor = Color.Black;
            PicHome.BackgroundImageLayout = ImageLayout.None;
            PicHome.Image = (Image)resources.GetObject("PicHome.Image");
            PicHome.Location = new Point(14, 19);
            PicHome.Name = "PicHome";
            PicHome.Size = new Size(45, 45);
            PicHome.SizeMode = PictureBoxSizeMode.Zoom;
            PicHome.TabIndex = 7;
            PicHome.TabStop = false;
            // 
            // HomeButton
            // 
            HomeButton.BackColor = Color.Black;
            HomeButton.FlatStyle = FlatStyle.Flat;
            HomeButton.ForeColor = Color.Transparent;
            HomeButton.ImageAlign = ContentAlignment.MiddleLeft;
            HomeButton.Location = new Point(-22, -36);
            HomeButton.Name = "HomeButton";
            HomeButton.Size = new Size(366, 154);
            HomeButton.TabIndex = 3;
            HomeButton.Text = "Home";
            HomeButton.UseVisualStyleBackColor = false;
            HomeButton.Click += Home_Click;
            // 
            // panel5
            // 
            panel5.Controls.Add(pictureBox2);
            panel5.Controls.Add(LibraryButton);
            panel5.Location = new Point(3, 154);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(25, 0, 0, 0);
            panel5.Size = new Size(329, 84);
            panel5.TabIndex = 7;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Black;
            pictureBox2.BackgroundImageLayout = ImageLayout.None;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(14, 19);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(45, 45);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // LibraryButton
            // 
            LibraryButton.BackColor = Color.Black;
            LibraryButton.FlatStyle = FlatStyle.Flat;
            LibraryButton.ForeColor = Color.Transparent;
            LibraryButton.ImageAlign = ContentAlignment.MiddleLeft;
            LibraryButton.Location = new Point(-22, -36);
            LibraryButton.Name = "LibraryButton";
            LibraryButton.Size = new Size(366, 154);
            LibraryButton.TabIndex = 3;
            LibraryButton.Text = "Library";
            LibraryButton.UseVisualStyleBackColor = false;
            // 
            // panel6
            // 
            panel6.Controls.Add(pictureBox3);
            panel6.Controls.Add(ShopButton);
            panel6.Location = new Point(3, 238);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(25, 0, 0, 0);
            panel6.Size = new Size(329, 84);
            panel6.TabIndex = 8;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Black;
            pictureBox3.BackgroundImageLayout = ImageLayout.None;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(14, 19);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(45, 45);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // ShopButton
            // 
            ShopButton.BackColor = Color.Black;
            ShopButton.FlatStyle = FlatStyle.Flat;
            ShopButton.ForeColor = Color.Transparent;
            ShopButton.ImageAlign = ContentAlignment.MiddleLeft;
            ShopButton.Location = new Point(-22, -36);
            ShopButton.Name = "ShopButton";
            ShopButton.Size = new Size(366, 154);
            ShopButton.TabIndex = 3;
            ShopButton.Text = "Shop";
            ShopButton.UseVisualStyleBackColor = false;
            // 
            // MenuTransition
            // 
            MenuTransition.Interval = 10;
            MenuTransition.Tick += MenuTransition_Tick;
            // 
            // MenuTransition2
            // 
            MenuTransition2.Interval = 10;
            MenuTransition2.Tick += MenuTransition2_Tick;
            // 
            // SidebarTransition
            // 
            SidebarTransition.Tick += SidebarTransition_Tick;
            // 
            // TrangChu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1257, 878);
            Controls.Add(SideBar);
            Controls.Add(panel1);
            ForeColor = Color.White;
            Name = "TrangChu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TrangChu";
            Load += TrangChu_Load_1;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LOGO).EndInit();
            SideBar.ResumeLayout(false);
            MenuContainer.ResumeLayout(false);
            panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            MenuContainer2.ResumeLayout(false);
            panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PicHome).EndInit();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox LOGO;
        private Label Dashbroad;
        private Panel SideBar;
        private Button HomeButton;
        private Panel panel3;
        private PictureBox PicHome;
        private Panel panel4;
        private PictureBox pictureBox1;
        private Button LessonAssignedButton;
        private Panel panel5;
        private PictureBox pictureBox2;
        private Button LibraryButton;
        private Panel panel6;
        private PictureBox pictureBox3;
        private Button ShopButton;
        private FlowLayoutPanel MenuContainer;
        private Panel panel8;
        private PictureBox pictureBox5;
        private Button LessonButton;
        private Panel panel9;
        private PictureBox pictureBox6;
        private Button LessonCompletedButton;
        private Panel panel10;
        private PictureBox pictureBox7;
        private Button LessonOverdueButton;
        private FlowLayoutPanel MenuContainer2;
        private Panel panel7;
        private PictureBox pictureBox4;
        private Button TestButton;
        private Panel panel11;
        private PictureBox pictureBox8;
        private Button TestAssigned;
        private Panel panel12;
        private PictureBox pictureBox9;
        private Button TestCompletedButton;
        private Panel panel13;
        private PictureBox pictureBox10;
        private Button TestOverdueButton;
        private System.Windows.Forms.Timer MenuTransition;
        private System.Windows.Forms.Timer MenuTransition2;
        private System.Windows.Forms.Timer SidebarTransition;
    }
}