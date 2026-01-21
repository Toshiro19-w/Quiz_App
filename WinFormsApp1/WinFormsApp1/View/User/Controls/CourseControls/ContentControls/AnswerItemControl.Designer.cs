using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    partial class AnswerItemControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

		#region Component Designer generated code
		private void InitializeComponent()
		{
			chkCorrect = new CheckBox();
			txtAnswer = new TextBox();
			btnDelete = new Button();
			SuspendLayout();
			// 
			// chkCorrect
			// 
			chkCorrect.AutoSize = true;
			chkCorrect.Location = new Point(8, 18);
			chkCorrect.Name = "chkCorrect";
			chkCorrect.Size = new Size(22, 21);
			chkCorrect.TabIndex = 0;
			chkCorrect.UseVisualStyleBackColor = true;
			// 
			// txtAnswer
			// 
			txtAnswer.Font = new Font("Segoe UI", 12F);
			txtAnswer.Location = new Point(36, 6);
			txtAnswer.Name = "txtAnswer";
			txtAnswer.PlaceholderText = "Nội dung đáp án";
			txtAnswer.Size = new Size(534, 39);
			txtAnswer.TabIndex = 1;
			// 
			// btnDelete
			// 
			btnDelete.BackColor = Color.White;
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.FlatStyle = FlatStyle.Flat;
			btnDelete.ForeColor = Color.White;
			btnDelete.Image = Properties.Resources.delete;
			btnDelete.Location = new Point(588, 6);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(43, 39);
			btnDelete.TabIndex = 2;
			btnDelete.UseVisualStyleBackColor = false;
			// 
			// AnswerItemControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			BackColor = Color.White;
			Controls.Add(btnDelete);
			Controls.Add(txtAnswer);
			Controls.Add(chkCorrect);
			Name = "AnswerItemControl";
			Size = new Size(644, 51);
			ResumeLayout(false);
			PerformLayout();
		}
		#endregion

		private CheckBox chkCorrect;
        private TextBox txtAnswer;
        private Button btnDelete;
    }
}
