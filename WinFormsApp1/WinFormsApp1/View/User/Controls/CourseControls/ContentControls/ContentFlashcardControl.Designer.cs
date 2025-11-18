namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    partial class ContentFlashcardControl
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
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.pnlFlashcards = new System.Windows.Forms.Panel();
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
            this.txtTitle.Size = new System.Drawing.Size(560, 23);
            this.txtTitle.TabIndex = 2;
            // lblDesc
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(8, 72);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(54, 15);
            this.lblDesc.TabIndex = 3;
            this.lblDesc.Text = "Mô t?:";
            // txtDesc
            this.txtDesc.Location = new System.Drawing.Point(8, 92);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDesc.Size = new System.Drawing.Size(672, 60);
            this.txtDesc.TabIndex = 4;
            // pnlFlashcards
            this.pnlFlashcards.Location = new System.Drawing.Point(8, 160);
            this.pnlFlashcards.Name = "pnlFlashcards";
            this.pnlFlashcards.Size = new System.Drawing.Size(672, 80);
            this.pnlFlashcards.TabIndex = 5;
            // ContentFlashcardControl
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlFlashcards);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cboContentType);
            this.Name = "ContentFlashcardControl";
            this.Size = new System.Drawing.Size(700, 260);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.ComboBox cboContentType;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Panel pnlFlashcards;
    }
}