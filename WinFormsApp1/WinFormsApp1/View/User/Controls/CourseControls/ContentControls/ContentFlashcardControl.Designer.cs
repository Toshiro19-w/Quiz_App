namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    partial class ContentFlashcardControl
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
			pnlFlashcards = new Panel();
			btnAddFlashcard = new Button();
			label1 = new Label();
			SuspendLayout();
			// 
			// cboContentType
			// 
			cboContentType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboContentType.FormattingEnabled = true;
			cboContentType.Items.AddRange(new object[] { "Theory", "Video", "FlashcardSet", "Test" });
			cboContentType.Location = new Point(25, 39);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(200, 33);
			cboContentType.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Location = new Point(25, 84);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(73, 25);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Tiêu đề:";
			// 
			// txtTitle
			// 
			txtTitle.Location = new Point(25, 115);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(670, 31);
			txtTitle.TabIndex = 2;
			// 
			// lblDesc
			// 
			lblDesc.AutoSize = true;
			lblDesc.Location = new Point(23, 149);
			lblDesc.Name = "lblDesc";
			lblDesc.Size = new Size(63, 25);
			lblDesc.TabIndex = 3;
			lblDesc.Text = "Mô tả:";
			// 
			// txtDesc
			// 
			txtDesc.Location = new Point(26, 173);
			txtDesc.Multiline = true;
			txtDesc.Name = "txtDesc";
			txtDesc.ScrollBars = ScrollBars.Vertical;
			txtDesc.Size = new Size(782, 60);
			txtDesc.TabIndex = 4;
			// 
			// pnlFlashcards
			// 
			pnlFlashcards.AutoScroll = true;
			pnlFlashcards.Location = new Point(26, 241);
			pnlFlashcards.Name = "pnlFlashcards";
			pnlFlashcards.Size = new Size(782, 170);
			pnlFlashcards.TabIndex = 5;
			// 
			// btnAddFlashcard
			// 
			btnAddFlashcard.Location = new Point(26, 417);
			btnAddFlashcard.Name = "btnAddFlashcard";
			btnAddFlashcard.Size = new Size(180, 32);
			btnAddFlashcard.TabIndex = 6;
			btnAddFlashcard.Text = "+ Thêm thẻ";
			btnAddFlashcard.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(25, 11);
			label1.Name = "label1";
			label1.Size = new Size(125, 25);
			label1.TabIndex = 7;
			label1.Text = "Loại nội dung:";
			// 
			// ContentFlashcardControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(label1);
			Controls.Add(btnAddFlashcard);
			Controls.Add(pnlFlashcards);
			Controls.Add(txtDesc);
			Controls.Add(lblDesc);
			Controls.Add(txtTitle);
			Controls.Add(lblTitle);
			Controls.Add(cboContentType);
			Name = "ContentFlashcardControl";
			Size = new Size(830, 462);
			ResumeLayout(false);
			PerformLayout();
		}
		#endregion

		private System.Windows.Forms.ComboBox cboContentType;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Panel pnlFlashcards;
        private System.Windows.Forms.Button btnAddFlashcard;
		private Label label1;
	}
}