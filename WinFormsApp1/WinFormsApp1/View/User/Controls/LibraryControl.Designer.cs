namespace WinFormsApp1.View.User.Controls
{
    partial class LibraryControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            headerPanel = new Panel();
            tabPanel = new Panel();
            tabUnderline = new Panel();
            btnFlashcards = new Button();
            btnAllCourses = new Button();
            lblTitle = new Label();
            coursesPanel = new FlowLayoutPanel();
            headerPanel.SuspendLayout();
            tabPanel.SuspendLayout();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(88, 56, 255);
            headerPanel.Controls.Add(tabPanel);
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(1476, 150);
            headerPanel.TabIndex = 0;
            headerPanel.Paint += HeaderPanel_Paint;
            // 
            // tabPanel
            // 
            tabPanel.BackColor = Color.Transparent;
            tabPanel.Controls.Add(tabUnderline);
            tabPanel.Controls.Add(btnFlashcards);
            tabPanel.Controls.Add(btnAllCourses);
            tabPanel.Location = new Point(100, 90);
            tabPanel.Name = "tabPanel";
            tabPanel.Size = new Size(400, 60);
            tabPanel.TabIndex = 1;
            // 
            // tabUnderline
            // 
            tabUnderline.BackColor = Color.White;
            tabUnderline.Location = new Point(0, 50);
            tabUnderline.Name = "tabUnderline";
            tabUnderline.Size = new Size(200, 4);
            tabUnderline.TabIndex = 2;
            // 
            // btnFlashcards
            // 
            btnFlashcards.BackColor = Color.Transparent;
            btnFlashcards.Cursor = Cursors.Hand;
            btnFlashcards.FlatAppearance.BorderSize = 0;
            btnFlashcards.FlatStyle = FlatStyle.Flat;
            btnFlashcards.Font = new Font("Segoe UI", 12F);
            btnFlashcards.ForeColor = Color.FromArgb(200, 200, 255);
            btnFlashcards.Location = new Point(200, 0);
            btnFlashcards.Name = "btnFlashcards";
            btnFlashcards.Size = new Size(200, 50);
            btnFlashcards.TabIndex = 1;
            btnFlashcards.Text = "📇 Flashcards";
            btnFlashcards.UseVisualStyleBackColor = false;
            btnFlashcards.Click += BtnFlashcards_Click;
            // 
            // btnAllCourses
            // 
            btnAllCourses.BackColor = Color.Transparent;
            btnAllCourses.Cursor = Cursors.Hand;
            btnAllCourses.FlatAppearance.BorderSize = 0;
            btnAllCourses.FlatStyle = FlatStyle.Flat;
            btnAllCourses.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAllCourses.ForeColor = Color.White;
            btnAllCourses.Location = new Point(0, 0);
            btnAllCourses.Name = "btnAllCourses";
            btnAllCourses.Size = new Size(200, 50);
            btnAllCourses.TabIndex = 0;
            btnAllCourses.Text = "📚 Tất cả khóa học";
            btnAllCourses.UseVisualStyleBackColor = false;
            btnAllCourses.Click += BtnAllCourses_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(100, 13);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(218, 74);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Library";
            // 
            // coursesPanel
            // 
            coursesPanel.AutoScroll = true;
            coursesPanel.BackColor = Color.Transparent;
            coursesPanel.Location = new Point(100, 180);
            coursesPanel.Name = "coursesPanel";
            coursesPanel.Padding = new Padding(0, 20, 0, 20);
            coursesPanel.Size = new Size(1276, 601);
            coursesPanel.TabIndex = 1;
            // 
            // LibraryControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(248, 249, 250);
            Controls.Add(coursesPanel);
            Controls.Add(headerPanel);
            Name = "LibraryControl";
            Size = new Size(1476, 801);
            Resize += LibraryControl_Resize;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            tabPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel tabPanel;
        private System.Windows.Forms.Button btnAllCourses;
        private System.Windows.Forms.Button btnFlashcards;
        private System.Windows.Forms.Panel tabUnderline;
        private System.Windows.Forms.FlowLayoutPanel coursesPanel;
    }
}
