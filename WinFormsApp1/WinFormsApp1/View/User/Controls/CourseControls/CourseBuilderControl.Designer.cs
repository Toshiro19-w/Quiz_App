namespace WinFormsApp1.View.User.Controls.CourseControls
{
    partial class CourseBuilderControl
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

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabStep1 = new System.Windows.Forms.TabPage();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblSlug = new System.Windows.Forms.Label();
            this.txtSlug = new System.Windows.Forms.TextBox();
            this.lblSlugError = new System.Windows.Forms.Label();
            this.lblTitleError = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblCover = new System.Windows.Forms.Label();
            this.txtCoverUrl = new System.Windows.Forms.TextBox();
            this.btnUploadCover = new System.Windows.Forms.Button();
            this.picCover = new System.Windows.Forms.PictureBox();
            this.tabStep2 = new System.Windows.Forms.TabPage();
            this.btnAddChapter = new System.Windows.Forms.Button();
            this.flpChapters = new System.Windows.Forms.FlowLayoutPanel();
            this.tabStep3 = new System.Windows.Forms.TabPage();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnSaveDraft = new System.Windows.Forms.Button();
            this.btnPublish = new System.Windows.Forms.Button();

            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabStep1);
            this.tabControl1.Controls.Add(this.tabStep2);
            this.tabControl1.Controls.Add(this.tabStep3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 600);
            this.tabControl1.TabIndex = 0;

            // 
            // tabStep1
            // 
            this.tabStep1.Controls.Add(this.lblTitle);
            this.tabStep1.Controls.Add(this.txtTitle);
            this.tabStep1.Controls.Add(this.lblTitleError);
            this.tabStep1.Controls.Add(this.lblSlug);
            this.tabStep1.Controls.Add(this.txtSlug);
            this.tabStep1.Controls.Add(this.lblSlugError);
            this.tabStep1.Controls.Add(this.lblSummary);
            this.tabStep1.Controls.Add(this.txtSummary);
            this.tabStep1.Controls.Add(this.lblPrice);
            this.tabStep1.Controls.Add(this.txtPrice);
            this.tabStep1.Controls.Add(this.lblCover);
            this.tabStep1.Controls.Add(this.txtCoverUrl);
            this.tabStep1.Controls.Add(this.btnUploadCover);
            this.tabStep1.Controls.Add(this.picCover);
            this.tabStep1.Location = new System.Drawing.Point(4, 24);
            this.tabStep1.Name = "tabStep1";
            this.tabStep1.Padding = new System.Windows.Forms.Padding(3);
            this.tabStep1.Size = new System.Drawing.Size(792, 572);
            this.tabStep1.TabIndex = 0;
            this.tabStep1.Text = "Thông tin c? b?n";
            this.tabStep1.UseVisualStyleBackColor = true;

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(34, 15);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tiêu ??";

            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(20, 40);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(520, 23);
            this.txtTitle.TabIndex = 1;

            // 
            // lblTitleError
            // 
            this.lblTitleError.AutoSize = true;
            this.lblTitleError.ForeColor = System.Drawing.Color.Red;
            this.lblTitleError.Location = new System.Drawing.Point(20, 66);
            this.lblTitleError.Name = "lblTitleError";
            this.lblTitleError.Size = new System.Drawing.Size(0, 15);
            this.lblTitleError.TabIndex = 2;
            this.lblTitleError.Visible = false;

            // 
            // lblSlug
            // 
            this.lblSlug.AutoSize = true;
            this.lblSlug.Location = new System.Drawing.Point(20, 90);
            this.lblSlug.Name = "lblSlug";
            this.lblSlug.Size = new System.Drawing.Size(31, 15);
            this.lblSlug.TabIndex = 3;
            this.lblSlug.Text = "Slug";

            // 
            // txtSlug
            // 
            this.txtSlug.Location = new System.Drawing.Point(20, 110);
            this.txtSlug.Name = "txtSlug";
            this.txtSlug.Size = new System.Drawing.Size(320, 23);
            this.txtSlug.TabIndex = 4;

            // 
            // lblSlugError
            // 
            this.lblSlugError.AutoSize = true;
            this.lblSlugError.ForeColor = System.Drawing.Color.Red;
            this.lblSlugError.Location = new System.Drawing.Point(20, 136);
            this.lblSlugError.Name = "lblSlugError";
            this.lblSlugError.Size = new System.Drawing.Size(0, 15);
            this.lblSlugError.TabIndex = 5;
            this.lblSlugError.Visible = false;

            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Location = new System.Drawing.Point(20, 160);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(42, 15);
            this.lblSummary.TabIndex = 6;
            this.lblSummary.Text = "Tóm t?t";

            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(20, 180);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(520, 80);
            this.txtSummary.TabIndex = 7;

            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(20, 270);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(32, 15);
            this.lblPrice.TabIndex = 8;
            this.lblPrice.Text = "Giá";

            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(20, 290);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(120, 23);
            this.txtPrice.TabIndex = 9;

            // 
            // lblCover
            // 
            this.lblCover.AutoSize = true;
            this.lblCover.Location = new System.Drawing.Point(560, 20);
            this.lblCover.Name = "lblCover";
            this.lblCover.Size = new System.Drawing.Size(36, 15);
            this.lblCover.TabIndex = 10;
            this.lblCover.Text = "Cover";

            // 
            // txtCoverUrl
            // 
            this.txtCoverUrl.Location = new System.Drawing.Point(560, 40);
            this.txtCoverUrl.Name = "txtCoverUrl";
            this.txtCoverUrl.Size = new System.Drawing.Size(200, 23);
            this.txtCoverUrl.TabIndex = 11;

            // 
            // btnUploadCover
            // 
            this.btnUploadCover.Location = new System.Drawing.Point(560, 70);
            this.btnUploadCover.Name = "btnUploadCover";
            this.btnUploadCover.Size = new System.Drawing.Size(75, 23);
            this.btnUploadCover.TabIndex = 12;
            this.btnUploadCover.Text = "Upload";
            this.btnUploadCover.UseVisualStyleBackColor = true;

            // 
            // picCover
            // 
            this.picCover.Location = new System.Drawing.Point(560, 100);
            this.picCover.Name = "picCover";
            this.picCover.Size = new System.Drawing.Size(200, 120);
            this.picCover.TabIndex = 13;
            this.picCover.TabStop = false;

            // 
            // tabStep2
            // 
            this.tabStep2.Controls.Add(this.btnAddChapter);
            this.tabStep2.Controls.Add(this.flpChapters);
            this.tabStep2.Location = new System.Drawing.Point(4, 24);
            this.tabStep2.Name = "tabStep2";
            this.tabStep2.Padding = new System.Windows.Forms.Padding(3);
            this.tabStep2.Size = new System.Drawing.Size(792, 572);
            this.tabStep2.TabIndex = 1;
            this.tabStep2.Text = "N?i dung";
            this.tabStep2.UseVisualStyleBackColor = true;

            // 
            // btnAddChapter
            // 
            this.btnAddChapter.Location = new System.Drawing.Point(20, 20);
            this.btnAddChapter.Name = "btnAddChapter";
            this.btnAddChapter.Size = new System.Drawing.Size(100, 23);
            this.btnAddChapter.TabIndex = 0;
            this.btnAddChapter.Text = "Thêm ch??ng";
            this.btnAddChapter.UseVisualStyleBackColor = true;

            // 
            // flpChapters
            // 
            this.flpChapters.AutoScroll = true;
            this.flpChapters.Location = new System.Drawing.Point(20, 60);
            this.flpChapters.Name = "flpChapters";
            this.flpChapters.Size = new System.Drawing.Size(740, 480);
            this.flpChapters.TabIndex = 1;

            // 
            // tabStep3
            // 
            this.tabStep3.Controls.Add(this.btnPrev);
            this.tabStep3.Controls.Add(this.btnNext);
            this.tabStep3.Controls.Add(this.btnSaveDraft);
            this.tabStep3.Controls.Add(this.btnPublish);
            this.tabStep3.Location = new System.Drawing.Point(4, 24);
            this.tabStep3.Name = "tabStep3";
            this.tabStep3.Padding = new System.Windows.Forms.Padding(3);
            this.tabStep3.Size = new System.Drawing.Size(792, 572);
            this.tabStep3.TabIndex = 2;
            this.tabStep3.Text = "Hoàn t?t";
            this.tabStep3.UseVisualStyleBackColor = true;

            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(20, 20);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.Text = "Quay l?i";
            this.btnPrev.UseVisualStyleBackColor = true;

            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(110, 20);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Ti?p";
            this.btnNext.UseVisualStyleBackColor = true;

            // 
            // btnSaveDraft
            // 
            this.btnSaveDraft.Location = new System.Drawing.Point(560, 20);
            this.btnSaveDraft.Name = "btnSaveDraft";
            this.btnSaveDraft.Size = new System.Drawing.Size(100, 23);
            this.btnSaveDraft.TabIndex = 2;
            this.btnSaveDraft.Text = "L?u nháp";
            this.btnSaveDraft.UseVisualStyleBackColor = true;

            // 
            // btnPublish
            // 
            this.btnPublish.Location = new System.Drawing.Point(680, 20);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(100, 23);
            this.btnPublish.TabIndex = 3;
            this.btnPublish.Text = "??ng khóa h?c";
            this.btnPublish.UseVisualStyleBackColor = true;

            // 
            // CourseBuilderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "CourseBuilderControl";
            this.Size = new System.Drawing.Size(800, 600);

            ((System.ComponentModel.ISupportInitialize)(this.picCover)).EndInit();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabStep1;
        private System.Windows.Forms.TabPage tabStep2;
        private System.Windows.Forms.TabPage tabStep3;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblSlug;
        private System.Windows.Forms.TextBox txtSlug;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblCover;
        private System.Windows.Forms.TextBox txtCoverUrl;
        private System.Windows.Forms.Button btnUploadCover;
        private System.Windows.Forms.PictureBox picCover;
        private System.Windows.Forms.Button btnAddChapter;
        private System.Windows.Forms.FlowLayoutPanel flpChapters;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnSaveDraft;
        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.Label lblSlugError;
        private System.Windows.Forms.Label lblTitleError;
    }
}
