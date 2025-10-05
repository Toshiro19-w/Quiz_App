namespace WinFormsApp1
{
    partial class formMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMenu));
            panel1 = new Panel();
            button1 = new Button();
            panel2 = new Panel();
            MenuContainer = new FlowLayoutPanel();
            panel8 = new Panel();
            pictureBox5 = new PictureBox();
            LessonButton = new Button();
            panel4 = new Panel();
            pictureBox1 = new PictureBox();
            LessonCreationButton = new Button();
            panel9 = new Panel();
            pictureBox6 = new PictureBox();
            LessonInProgressButton = new Button();
            panel3 = new Panel();
            PicHome = new PictureBox();
            HomeButton = new Button();
            panel5 = new Panel();
            pictureBox2 = new PictureBox();
            LibraryButton = new Button();
            panel6 = new Panel();
            pictureBox3 = new PictureBox();
            ShopButton = new Button();
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
            MenuTransition = new System.Windows.Forms.Timer(components);
            MenuTransition2 = new System.Windows.Forms.Timer(components);
            SidebarTransition = new System.Windows.Forms.Timer(components);
            panelShow = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            MenuContainer.SuspendLayout();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PicHome).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            MenuContainer2.SuspendLayout();
            panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            panelShow.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1257, 60);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // button1
            // 
            button1.Location = new Point(17, 12);
            button1.Name = "button1";
            button1.Size = new Size(45, 45);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.Controls.Add(MenuContainer);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(panel6);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 60);
            panel2.Name = "panel2";
            panel2.Size = new Size(332, 818);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // MenuContainer
            // 
            MenuContainer.BackColor = Color.FromArgb(23, 24, 29);
            MenuContainer.Controls.Add(panel8);
            MenuContainer.Controls.Add(panel4);
            MenuContainer.Controls.Add(panel9);
            MenuContainer.Location = new Point(3, 343);
            MenuContainer.Margin = new Padding(0);
            MenuContainer.Name = "MenuContainer";
            MenuContainer.Size = new Size(329, 262);
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
            panel4.Controls.Add(LessonCreationButton);
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
            // LessonCreationButton
            // 
            LessonCreationButton.BackColor = Color.FromArgb(23, 24, 29);
            LessonCreationButton.FlatStyle = FlatStyle.Flat;
            LessonCreationButton.ForeColor = Color.Transparent;
            LessonCreationButton.ImageAlign = ContentAlignment.MiddleLeft;
            LessonCreationButton.Location = new Point(-22, -36);
            LessonCreationButton.Margin = new Padding(0);
            LessonCreationButton.Name = "LessonCreationButton";
            LessonCreationButton.Size = new Size(366, 154);
            LessonCreationButton.TabIndex = 3;
            LessonCreationButton.Text = "Create a new lesson";
            LessonCreationButton.UseVisualStyleBackColor = false;
            LessonCreationButton.Click += LessonCreationButton_Click;
            // 
            // panel9
            // 
            panel9.Controls.Add(pictureBox6);
            panel9.Controls.Add(LessonInProgressButton);
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
            // LessonInProgressButton
            // 
            LessonInProgressButton.BackColor = Color.FromArgb(23, 24, 29);
            LessonInProgressButton.FlatStyle = FlatStyle.Flat;
            LessonInProgressButton.ForeColor = Color.Transparent;
            LessonInProgressButton.ImageAlign = ContentAlignment.MiddleLeft;
            LessonInProgressButton.Location = new Point(-22, -36);
            LessonInProgressButton.Margin = new Padding(0);
            LessonInProgressButton.Name = "LessonInProgressButton";
            LessonInProgressButton.Size = new Size(366, 154);
            LessonInProgressButton.TabIndex = 3;
            LessonInProgressButton.Text = "In progress";
            LessonInProgressButton.UseVisualStyleBackColor = false;
            LessonInProgressButton.Click += LessonInProgressButton_Click;
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
            panel5.Location = new Point(3, 166);
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
            LibraryButton.Click += LibraryButton_Click;
            // 
            // panel6
            // 
            panel6.Controls.Add(pictureBox3);
            panel6.Controls.Add(ShopButton);
            panel6.Location = new Point(3, 256);
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
            ShopButton.Click += ShopButton_Click;
            // 
            // MenuContainer2
            // 
            MenuContainer2.BackColor = Color.FromArgb(23, 24, 29);
            MenuContainer2.Controls.Add(panel7);
            MenuContainer2.Controls.Add(panel11);
            MenuContainer2.Controls.Add(panel12);
            MenuContainer2.Controls.Add(panel13);
            MenuContainer2.Location = new Point(257, 305);
            MenuContainer2.Margin = new Padding(0);
            MenuContainer2.Name = "MenuContainer2";
            MenuContainer2.Size = new Size(329, 337);
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
            TestAssigned.Click += TestAssigned_Click;
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
            TestOverdueButton.Click += TestOverdueButton_Click;
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
            SidebarTransition.Interval = 10;
            SidebarTransition.Tick += SidebarTransition_Tick;
            // 
            // panelShow
            // 
            panelShow.Controls.Add(MenuContainer2);
            panelShow.Location = new Point(335, 62);
            panelShow.Name = "panelShow";
            panelShow.Size = new Size(922, 816);
            panelShow.TabIndex = 3;
            panelShow.Paint += panel10_Paint;
            // 
            // formMenu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1257, 878);
            Controls.Add(panelShow);
            Controls.Add(panel2);
            Controls.Add(panel1);
            ForeColor = Color.White;
            IsMdiContainer = true;
            Name = "formMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TrangChu";
            Load += TrangChu_Load_1;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            MenuContainer.ResumeLayout(false);
            panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PicHome).EndInit();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            MenuContainer2.ResumeLayout(false);
            panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            panelShow.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Button HomeButton;
        private Panel panel3;
        private PictureBox PicHome;
        private Panel panel4;
        private PictureBox pictureBox1;
        private Button LessonCreationButton;
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
        private Button LessonInProgressButton;
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
        private Button button1;
        private Panel panelShow;
    }
}