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
			lblTitle.BackColor = Color.WhiteSmoke;
			lblTitle.BorderStyle = BorderStyle.None;
			lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblTitle.Location = new Point(46, 8);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(1346, 27);
			lblTitle.TabIndex = 2;
			// 
			// btnAddLesson
			// 
			btnAddLesson.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnAddLesson.BackColor = Color.FromArgb(88, 86, 233);
			btnAddLesson.FlatAppearance.BorderSize = 0;
			btnAddLesson.FlatStyle = FlatStyle.Flat;
			btnAddLesson.ForeColor = Color.White;
			btnAddLesson.Location = new Point(1308, 4);
			btnAddLesson.Name = "btnAddLesson";
			btnAddLesson.Size = new Size(131, 35);
			btnAddLesson.TabIndex = 1;
			btnAddLesson.Text = "Thêm bài học";
			btnAddLesson.UseVisualStyleBackColor = false;
			// 
			// btnRemoveChapter
			// 
			btnRemoveChapter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnRemoveChapter.BackColor = Color.FromArgb(220, 53, 69);
			btnRemoveChapter.FlatAppearance.BorderSize = 0;
			btnRemoveChapter.FlatStyle = FlatStyle.Flat;
			btnRemoveChapter.ForeColor = Color.White;
			btnRemoveChapter.Location = new Point(1148, 4);
			btnRemoveChapter.Name = "btnRemoveChapter";
			btnRemoveChapter.Size = new Size(134, 35);
			btnRemoveChapter.TabIndex = 3;
			btnRemoveChapter.Text = "Xóa chương";
			btnRemoveChapter.UseVisualStyleBackColor = false;
			// 
			// flpLessons
			// 
			flpLessons.AutoScroll = true;
			flpLessons.FlowDirection = FlowDirection.TopDown;
			flpLessons.Location = new Point(46, 45);
			flpLessons.Name = "flpLessons";
			flpLessons.Padding = new Padding(4);
			flpLessons.Size = new Size(1393, 161);
			flpLessons.TabIndex = 0;
			flpLessons.WrapContents = false;
			// 
			// ChapterItemControl
			// 
			BackColor = Color.WhiteSmoke;
			Controls.Add(flpLessons);
			Controls.Add(btnAddLesson);
			Controls.Add(btnRemoveChapter);
			Controls.Add(lblTitle);
			Name = "ChapterItemControl";
			Padding = new Padding(8);
			Size = new Size(1450, 217);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
