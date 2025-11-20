using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.CourseControls
{
    partial class LessonItemControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

		private void InitializeComponent()
		{
			txtTitle = new TextBox();
			btnDelete = new Button();
			SuspendLayout();
			// 
			// txtTitle
			// 
			txtTitle.BackColor = Color.WhiteSmoke;
			txtTitle.BorderStyle = BorderStyle.None;
			txtTitle.Font = new Font("Segoe UI", 9F);
			txtTitle.Location = new Point(93, 6);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(500, 24);
			txtTitle.TabIndex = 0;
			// 
			// btnDelete
			// 
			btnDelete.BackColor = Color.FromArgb(242, 75, 75);
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.FlatStyle = FlatStyle.Flat;
			btnDelete.ForeColor = Color.White;
			btnDelete.Location = new Point(1204, 3);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(122, 30);
			btnDelete.TabIndex = 1;
			btnDelete.Text = "Xóa bài học";
			btnDelete.UseVisualStyleBackColor = false;
			// 
			// LessonItemControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			BackColor = Color.Transparent;
			Controls.Add(btnDelete);
			Controls.Add(txtTitle);
			Margin = new Padding(0, 0, 0, 6);
			Name = "LessonItemControl";
			Size = new Size(1349, 36);
			ResumeLayout(false);
			PerformLayout();
		}

		private TextBox txtTitle;
        private Button btnDelete;
    }
}
