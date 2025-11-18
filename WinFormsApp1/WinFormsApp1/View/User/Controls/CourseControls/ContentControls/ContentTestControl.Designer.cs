namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    partial class ContentTestControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

		#region Component Designer generated code
		private void InitializeComponent()
		{
			cboContentType = new ComboBox();
			lblTitle = new Label();
			txtTitle = new TextBox();
			lblDesc = new Label();
			txtDesc = new TextBox();
			pnlQuestions = new Panel();
			SuspendLayout();
			// 
			// cboContentType
			// 
			cboContentType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboContentType.FormattingEnabled = true;
			cboContentType.Items.AddRange(new object[] { "Theory", "Video", "FlashcardSet", "Test" });
			cboContentType.Location = new Point(8, 8);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(314, 33);
			cboContentType.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Location = new Point(8, 44);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(73, 25);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Tiêu đề:";
			// 
			// txtTitle
			// 
			txtTitle.Location = new Point(80, 40);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(714, 31);
			txtTitle.TabIndex = 2;
			// 
			// lblDesc
			// 
			lblDesc.AutoSize = true;
			lblDesc.Location = new Point(8, 72);
			lblDesc.Name = "lblDesc";
			lblDesc.Size = new Size(63, 25);
			lblDesc.TabIndex = 3;
			lblDesc.Text = "Mô tả:";
			// 
			// txtDesc
			// 
			txtDesc.Location = new Point(8, 92);
			txtDesc.Multiline = true;
			txtDesc.Name = "txtDesc";
			txtDesc.ScrollBars = ScrollBars.Vertical;
			txtDesc.Size = new Size(786, 60);
			txtDesc.TabIndex = 4;
			// 
			// pnlQuestions
			// 
			pnlQuestions.Location = new Point(8, 160);
			pnlQuestions.Name = "pnlQuestions";
			pnlQuestions.Size = new Size(786, 120);
			pnlQuestions.TabIndex = 5;
			// 
			// ContentTestControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(pnlQuestions);
			Controls.Add(txtDesc);
			Controls.Add(lblDesc);
			Controls.Add(txtTitle);
			Controls.Add(lblTitle);
			Controls.Add(cboContentType);
			Name = "ContentTestControl";
			Size = new Size(835, 399);
			ResumeLayout(false);
			PerformLayout();
		}
		#endregion

		private System.Windows.Forms.ComboBox cboContentType;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Panel pnlQuestions;
    }
}