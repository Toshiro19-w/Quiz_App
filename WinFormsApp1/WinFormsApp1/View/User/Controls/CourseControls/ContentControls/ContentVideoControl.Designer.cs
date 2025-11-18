namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    partial class ContentVideoControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cboContentType = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblVideo = new System.Windows.Forms.Label();
            this.txtVideoPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // cboContentType
            this.cboContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboContentType.FormattingEnabled = true;
            this.cboContentType.Items.AddRange(new object[] { "Theory", "Video", "FlashcardSet", "Test" });
            this.cboContentType.Location = new System.Drawing.Point(8, 8);
            this.cboContentType.Name = "cboContentType";
            this.cboContentType.Size = new System.Drawing.Size(200, 23);
            this.cboContentType.TabIndex = 0;
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(8, 44);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(42, 15);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Tiêu ??:";
            // txtTitle
            this.txtTitle.Location = new System.Drawing.Point(80, 40);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(480, 23);
            this.txtTitle.TabIndex = 2;
            // lblVideo
            this.lblVideo.AutoSize = true;
            this.lblVideo.Location = new System.Drawing.Point(8, 72);
            this.lblVideo.Name = "lblVideo";
            this.lblVideo.Size = new System.Drawing.Size(36, 15);
            this.lblVideo.TabIndex = 3;
            this.lblVideo.Text = "Video:";
            // txtVideoPath
            this.txtVideoPath.Location = new System.Drawing.Point(80, 68);
            this.txtVideoPath.Name = "txtVideoPath";
            this.txtVideoPath.ReadOnly = true;
            this.txtVideoPath.Size = new System.Drawing.Size(480, 23);
            this.txtVideoPath.TabIndex = 4;
            // btnBrowse
            this.btnBrowse.Location = new System.Drawing.Point(570, 66);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(100, 25);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "Ch?n video";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // ContentVideoControl
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtVideoPath);
            this.Controls.Add(this.lblVideo);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cboContentType);
            this.Name = "ContentVideoControl";
            this.Size = new System.Drawing.Size(700, 120);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.ComboBox cboContentType;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.TextBox txtVideoPath;
        private System.Windows.Forms.Button btnBrowse;
    }
}