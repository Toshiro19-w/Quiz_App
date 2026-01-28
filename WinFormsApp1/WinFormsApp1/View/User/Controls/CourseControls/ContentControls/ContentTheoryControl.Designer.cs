namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    partial class ContentTheoryControl
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

		private void InitializeComponent()
		{
			cboContentType = new ComboBox();
			lblTitle = new Label();
			txtTitle = new TextBox();
			label1 = new Label();
			btnBrowsePdf = new Button();
			txtPdfPath = new TextBox();
			lblPdfPath = new Label();
			btnPreviewPdf = new Button();
			SuspendLayout();
			// 
			// cboContentType
			// 
			cboContentType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboContentType.Font = new Font("Segoe UI", 12F);
			cboContentType.FormattingEnabled = true;
            cboContentType.Items.AddRange(new object[] { "Theory", "Video", "Flashcard Set", "Quiz" });
			cboContentType.Location = new Point(20, 75);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(301, 40);
			cboContentType.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 12F);
			lblTitle.Location = new Point(20, 127);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(99, 32);
			lblTitle.TabIndex = 1;
            lblTitle.Text = "Title:";
			// 
			// txtTitle
			// 
			txtTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtTitle.Font = new Font("Segoe UI", 12F);
			txtTitle.Location = new Point(20, 167);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(1317, 39);
			txtTitle.TabIndex = 2;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F);
			label1.Location = new Point(20, 34);
			label1.Name = "label1";
			label1.Size = new Size(166, 32);
			label1.TabIndex = 5;
            label1.Text = "Content Type:";
			// 
			// btnBrowsePdf
			// 
			btnBrowsePdf.BackColor = Color.FromArgb(52, 144, 220);
			btnBrowsePdf.FlatAppearance.BorderSize = 0;
			btnBrowsePdf.FlatStyle = FlatStyle.Flat;
			btnBrowsePdf.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnBrowsePdf.ForeColor = Color.White;
			btnBrowsePdf.Location = new Point(21, 290);
			btnBrowsePdf.Name = "btnBrowsePdf";
			btnBrowsePdf.Size = new Size(150, 40);
			btnBrowsePdf.TabIndex = 7;
            btnBrowsePdf.Text = "📄 Select PDF";
			btnBrowsePdf.UseVisualStyleBackColor = false;
			// 
			// txtPdfPath
			// 
			txtPdfPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtPdfPath.Font = new Font("Segoe UI", 10F);
			txtPdfPath.Location = new Point(177, 293);
			txtPdfPath.Name = "txtPdfPath";
			txtPdfPath.ReadOnly = true;
			txtPdfPath.Size = new Size(977, 34);
			txtPdfPath.TabIndex = 8;
			// 
			// lblPdfPath
			// 
			lblPdfPath.AutoSize = true;
			lblPdfPath.Font = new Font("Segoe UI", 12F);
			lblPdfPath.Location = new Point(20, 234);
			lblPdfPath.Name = "lblPdfPath";
			lblPdfPath.Size = new Size(142, 32);
			lblPdfPath.TabIndex = 9;
            lblPdfPath.Text = "PDF Document:";
			// 
			// btnPreviewPdf
			// 
			btnPreviewPdf.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnPreviewPdf.BackColor = Color.FromArgb(40, 167, 69);
			btnPreviewPdf.FlatAppearance.BorderSize = 0;
			btnPreviewPdf.FlatStyle = FlatStyle.Flat;
			btnPreviewPdf.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnPreviewPdf.ForeColor = Color.White;
			btnPreviewPdf.Image = Properties.Resources.view;
			btnPreviewPdf.Location = new Point(1170, 290);
			btnPreviewPdf.Name = "btnPreviewPdf";
			btnPreviewPdf.Size = new Size(167, 40);
			btnPreviewPdf.TabIndex = 10;
            btnPreviewPdf.Text = " Preview";
			btnPreviewPdf.TextImageRelation = TextImageRelation.ImageBeforeText;
			btnPreviewPdf.UseVisualStyleBackColor = false;
			// 
			// ContentTheoryControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			BackColor = Color.White;
			Controls.Add(btnPreviewPdf);
			Controls.Add(lblPdfPath);
			Controls.Add(txtPdfPath);
			Controls.Add(btnBrowsePdf);
			Controls.Add(label1);
			Controls.Add(txtTitle);
			Controls.Add(lblTitle);
			Controls.Add(cboContentType);
			Name = "ContentTheoryControl";
			Size = new Size(1350, 376);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.ComboBox cboContentType;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
		private Label label1;
		private Button btnBrowsePdf;
		private TextBox txtPdfPath;
		private Label lblPdfPath;
		private Button btnPreviewPdf;
	}
}