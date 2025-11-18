namespace WinFormsApp1.View.User.Controls.CourseControls
{
    partial class ChapterItemControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox lblTitle;
        private System.Windows.Forms.Button btnAddLesson;
        private System.Windows.Forms.Button btnRemoveChapter;
        private System.Windows.Forms.FlowLayoutPanel flpLessons;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		private void InitializeComponent()
		{
			lblTitle = new TextBox();
			btnAddLesson = new Button();
			btnRemoveChapter = new Button();
			flpLessons = new FlowLayoutPanel();
			SuspendLayout();
			// 
			// lblTitle
			// 
			lblTitle.BackColor = Color.White;
			lblTitle.BorderStyle = BorderStyle.None;
			lblTitle.Dock = DockStyle.Top;
			lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblTitle.Location = new Point(8, 8);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(812, 27);
			lblTitle.TabIndex = 2;
			// 
			// btnAddLesson
			// 
			btnAddLesson.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnAddLesson.BackColor = Color.FromArgb(88, 86, 233);
			btnAddLesson.FlatAppearance.BorderSize = 0;
			btnAddLesson.FlatStyle = FlatStyle.Flat;
			btnAddLesson.ForeColor = Color.White;
			btnAddLesson.Location = new Point(737, 11);
			btnAddLesson.Name = "btnAddLesson";
			btnAddLesson.Size = new Size(80, 28);
			btnAddLesson.TabIndex = 1;
			btnAddLesson.Text = "Thêm";
			btnAddLesson.UseVisualStyleBackColor = false;
			// 
			// btnRemoveChapter
			// 
			btnRemoveChapter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnRemoveChapter.BackColor = Color.FromArgb(220, 53, 69);
			btnRemoveChapter.FlatAppearance.BorderSize = 0;
			btnRemoveChapter.FlatStyle = FlatStyle.Flat;
			btnRemoveChapter.ForeColor = Color.White;
			btnRemoveChapter.Location = new Point(645, 11);
			btnRemoveChapter.Name = "btnRemoveChapter";
			btnRemoveChapter.Size = new Size(80, 28);
			btnRemoveChapter.TabIndex = 3;
			btnRemoveChapter.Text = "Xóa chương";
			btnRemoveChapter.UseVisualStyleBackColor = false;
			// 
			// flpLessons
			// 
			flpLessons.AutoScroll = true;
			flpLessons.FlowDirection = FlowDirection.TopDown;
			flpLessons.Location = new Point(8, 60);
			flpLessons.Name = "flpLessons";
			flpLessons.Padding = new Padding(4);
			flpLessons.Size = new Size(812, 112);
			flpLessons.TabIndex = 0;
			flpLessons.WrapContents = false;
			// 
			// ChapterItemControl
			// 
			BackColor = Color.White;
			Controls.Add(flpLessons);
			Controls.Add(btnAddLesson);
			Controls.Add(btnRemoveChapter);
			Controls.Add(lblTitle);
			Name = "ChapterItemControl";
			Padding = new Padding(8);
			Size = new Size(828, 183);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
