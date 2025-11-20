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
            chkCorrect.Location = new Point(8, 10);
            chkCorrect.Name = "chkCorrect";
            chkCorrect.Size = new Size(22, 21);
            chkCorrect.TabIndex = 0;
            chkCorrect.UseVisualStyleBackColor = true;
            // 
            // txtAnswer
            // 
            txtAnswer.Location = new Point(36, 6);
            txtAnswer.Name = "txtAnswer";
            txtAnswer.PlaceholderText = "Nội dung đáp án";
            txtAnswer.Size = new Size(480, 31);
            txtAnswer.TabIndex = 1;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(158, 158, 158);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(522, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(40, 30);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "🗑";
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
            Size = new Size(570, 42);
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private CheckBox chkCorrect;
        private TextBox txtAnswer;
        private Button btnDelete;
    }
}
