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
			cboContentType.Font = new Font("Segoe UI", 10F);
			cboContentType.FormattingEnabled = true;
			cboContentType.Items.AddRange(new object[] { "Theory", "Video", "FlashcardSet", "Test" });
			cboContentType.Location = new Point(25, 43);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(699, 36);
			cboContentType.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 10F);
			lblTitle.Location = new Point(25, 90);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(79, 28);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Tiêu đề:";
			// 
			// txtTitle
			// 
			txtTitle.Font = new Font("Segoe UI", 10F);
			txtTitle.Location = new Point(25, 124);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(1270, 34);
			txtTitle.TabIndex = 2;
			// 
			// lblDesc
			// 
			lblDesc.AutoSize = true;
			lblDesc.Font = new Font("Segoe UI", 10F);
			lblDesc.Location = new Point(25, 166);
			lblDesc.Name = "lblDesc";
			lblDesc.Size = new Size(68, 28);
			lblDesc.TabIndex = 3;
			lblDesc.Text = "Mô tả:";
			// 
			// txtDesc
			// 
			txtDesc.Font = new Font("Segoe UI", 10F);
			txtDesc.Location = new Point(25, 204);
			txtDesc.Multiline = true;
			txtDesc.Name = "txtDesc";
			txtDesc.ScrollBars = ScrollBars.Vertical;
			txtDesc.Size = new Size(1382, 60);
			txtDesc.TabIndex = 4;
			// 
			// pnlFlashcards
			// 
			pnlFlashcards.AutoScroll = true;
			pnlFlashcards.Font = new Font("Segoe UI", 10F);
			pnlFlashcards.Location = new Point(26, 285);
			pnlFlashcards.Name = "pnlFlashcards";
			pnlFlashcards.Size = new Size(1382, 194);
			pnlFlashcards.TabIndex = 5;
			// 
			// btnAddFlashcard
			// 
			btnAddFlashcard.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnAddFlashcard.BackColor = Color.FromArgb(192, 255, 192);
			btnAddFlashcard.Font = new Font("Segoe UI", 10F);
			btnAddFlashcard.Location = new Point(26, 485);
			btnAddFlashcard.Name = "btnAddFlashcard";
			btnAddFlashcard.Size = new Size(180, 40);
			btnAddFlashcard.TabIndex = 6;
			btnAddFlashcard.Text = "+ Thêm thẻ";
			btnAddFlashcard.UseVisualStyleBackColor = false;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 10F);
			label1.Location = new Point(25, 11);
			label1.Name = "label1";
			label1.Size = new Size(136, 28);
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
			Size = new Size(1450, 539);
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