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
			cboContentType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			cboContentType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboContentType.Font = new Font("Segoe UI", 12F);
			cboContentType.FormattingEnabled = true;
			cboContentType.Items.AddRange(new object[] { "Lý thuyết", "Video", "Bộ thẻ ghi nhớ", "Bài kiểm tra" });
			cboContentType.Location = new Point(20, 48);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(213, 40);
			cboContentType.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 12F);
			lblTitle.Location = new Point(21, 98);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(99, 32);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Tiêu đề:";
			// 
			// txtTitle
			// 
			txtTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtTitle.Font = new Font("Segoe UI", 12F);
			txtTitle.Location = new Point(20, 133);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(1285, 39);
			txtTitle.TabIndex = 2;
			// 
			// lblDesc
			// 
			lblDesc.AutoSize = true;
			lblDesc.Font = new Font("Segoe UI", 12F);
			lblDesc.Location = new Point(20, 183);
			lblDesc.Name = "lblDesc";
			lblDesc.Size = new Size(82, 32);
			lblDesc.TabIndex = 3;
			lblDesc.Text = "Mô tả:";
			// 
			// txtDesc
			// 
			txtDesc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtDesc.Font = new Font("Segoe UI", 12F);
			txtDesc.Location = new Point(20, 218);
			txtDesc.Multiline = true;
			txtDesc.Name = "txtDesc";
			txtDesc.ScrollBars = ScrollBars.Vertical;
			txtDesc.Size = new Size(1285, 60);
			txtDesc.TabIndex = 4;
			// 
			// pnlFlashcards
			// 
			pnlFlashcards.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			pnlFlashcards.AutoScroll = true;
			pnlFlashcards.Font = new Font("Segoe UI", 12F);
			pnlFlashcards.Location = new Point(21, 297);
			pnlFlashcards.Name = "pnlFlashcards";
			pnlFlashcards.Size = new Size(1284, 236);
			pnlFlashcards.TabIndex = 5;
			// 
			// btnAddFlashcard
			// 
			btnAddFlashcard.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnAddFlashcard.BackColor = Color.FromArgb(192, 255, 192);
			btnAddFlashcard.Font = new Font("Segoe UI", 10F);
			btnAddFlashcard.Location = new Point(21, 559);
			btnAddFlashcard.Name = "btnAddFlashcard";
			btnAddFlashcard.Size = new Size(170, 40);
			btnAddFlashcard.TabIndex = 6;
			btnAddFlashcard.Text = "+ Thêm thẻ";
			btnAddFlashcard.UseVisualStyleBackColor = false;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F);
			label1.Location = new Point(20, 13);
			label1.Name = "label1";
			label1.Size = new Size(166, 32);
			label1.TabIndex = 7;
			label1.Text = "Loại nội dung:";
			// 
			// ContentFlashcardControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			BackColor = Color.White;
			Controls.Add(label1);
			Controls.Add(btnAddFlashcard);
			Controls.Add(pnlFlashcards);
			Controls.Add(txtDesc);
			Controls.Add(lblDesc);
			Controls.Add(txtTitle);
			Controls.Add(lblTitle);
			Controls.Add(cboContentType);
			Name = "ContentFlashcardControl";
			Size = new Size(1323, 601);
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