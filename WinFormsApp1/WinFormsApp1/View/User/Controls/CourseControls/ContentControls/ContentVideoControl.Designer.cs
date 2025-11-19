namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    partial class ContentVideoControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

		#region Component Designer generated code
		private void InitializeComponent()
		{
			cboContentType = new ComboBox();
			lblTitle = new Label();
			txtTitle = new TextBox();
			lblVideo = new Label();
			txtVideoPath = new TextBox();
			btnBrowse = new Button();
			label1 = new Label();
			SuspendLayout();
			// 
			// cboContentType
			// 
			cboContentType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboContentType.FormattingEnabled = true;
			cboContentType.Items.AddRange(new object[] { "Theory", "Video", "FlashcardSet", "Test" });
			cboContentType.Location = new Point(28, 46);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(200, 33);
			cboContentType.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Location = new Point(28, 13);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(125, 25);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Loại nội dung:";
			// 
			// txtTitle
			// 
			txtTitle.Location = new Point(28, 128);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(480, 31);
			txtTitle.TabIndex = 2;
			// 
			// lblVideo
			// 
			lblVideo.AutoSize = true;
			lblVideo.Location = new Point(28, 173);
			lblVideo.Name = "lblVideo";
			lblVideo.Size = new Size(62, 25);
			lblVideo.TabIndex = 3;
			lblVideo.Text = "Video:";
			// 
			// txtVideoPath
			// 
			txtVideoPath.Location = new Point(28, 207);
			txtVideoPath.Name = "txtVideoPath";
			txtVideoPath.ReadOnly = true;
			txtVideoPath.Size = new Size(480, 31);
			txtVideoPath.TabIndex = 4;
			// 
			// btnBrowse
			// 
			btnBrowse.Location = new Point(114, 173);
			btnBrowse.Name = "btnBrowse";
			btnBrowse.Size = new Size(114, 31);
			btnBrowse.TabIndex = 5;
			btnBrowse.Text = "Chọn video";
			btnBrowse.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(28, 97);
			label1.Name = "label1";
			label1.Size = new Size(73, 25);
			label1.TabIndex = 6;
			label1.Text = "Tiêu đề:";
			// 
			// ContentVideoControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(label1);
			Controls.Add(btnBrowse);
			Controls.Add(txtVideoPath);
			Controls.Add(lblVideo);
			Controls.Add(txtTitle);
			Controls.Add(lblTitle);
			Controls.Add(cboContentType);
			Name = "ContentVideoControl";
			Size = new Size(830, 270);
			ResumeLayout(false);
			PerformLayout();
		}
		#endregion

		private System.Windows.Forms.ComboBox cboContentType;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.TextBox txtVideoPath;
        private System.Windows.Forms.Button btnBrowse;
		private Label label1;
	}
}