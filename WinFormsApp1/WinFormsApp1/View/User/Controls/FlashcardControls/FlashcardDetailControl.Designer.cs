using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
    partial class FlashcardDetailControl
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblBreadcrumb;
        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblCardCount;
        private Label lblCreatedDate;
        private Label lblLanguage;
        private Panel pnlActions;
        private Button btnStartLearning;
        private Button btnViewDifferent;
        private Panel pnlInfo;
        private Label lblDescriptionTitle;
        private RichTextBox rtbDescription;
        private Label lblAuthorTitle;
        private Label lblAuthorName;
        private Label lblAuthorEmail;
        private Label lblFlashcardsTitle;
        private FlowLayoutPanel flowFlashcards;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblBreadcrumb = new Label();
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblCardCount = new Label();
            lblCreatedDate = new Label();
            lblLanguage = new Label();
            pnlActions = new Panel();
            btnStartLearning = new Button();
            btnViewDifferent = new Button();
            pnlInfo = new Panel();
            lblDescriptionTitle = new Label();
            rtbDescription = new RichTextBox();
            lblAuthorTitle = new Label();
            lblAuthorName = new Label();
            lblAuthorEmail = new Label();
            lblFlashcardsTitle = new Label();
            flowFlashcards = new FlowLayoutPanel();
            pnlHeader.SuspendLayout();
            pnlActions.SuspendLayout();
            pnlInfo.SuspendLayout();
            SuspendLayout();
            // 
            // lblBreadcrumb
            // 
            lblBreadcrumb.AutoSize = true;
            lblBreadcrumb.Font = new Font("Segoe UI", 9F);
            lblBreadcrumb.ForeColor = Color.Gray;
            lblBreadcrumb.Location = new Point(30, 25);
            lblBreadcrumb.Name = "lblBreadcrumb";
            lblBreadcrumb.Size = new Size(250, 25);
            lblBreadcrumb.TabIndex = 0;
            lblBreadcrumb.Text = "Flashcards / HTML/CSS Quick Reference";
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(124, 77, 255);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblCardCount);
            pnlHeader.Controls.Add(lblCreatedDate);
            pnlHeader.Controls.Add(lblLanguage);
            pnlHeader.Location = new Point(30, 70);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1200, 160);
            pnlHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(25, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1150, 70);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "HTML/CSS Quick Reference";
            // 
            // lblCardCount
            // 
            lblCardCount.AutoSize = true;
            lblCardCount.Font = new Font("Segoe UI", 12F);
            lblCardCount.ForeColor = Color.White;
            lblCardCount.Location = new Point(25, 105);
            lblCardCount.Name = "lblCardCount";
            lblCardCount.Size = new Size(100, 32);
            lblCardCount.TabIndex = 1;
            lblCardCount.Text = "Số thẻ: 4";
            // 
            // lblCreatedDate
            // 
            lblCreatedDate.AutoSize = true;
            lblCreatedDate.Font = new Font("Segoe UI", 11F);
            lblCreatedDate.ForeColor = Color.White;
            lblCreatedDate.Location = new Point(220, 107);
            lblCreatedDate.Name = "lblCreatedDate";
            lblCreatedDate.Size = new Size(200, 30);
            lblCreatedDate.TabIndex = 2;
            lblCreatedDate.Text = "Tạo lúc: 15/11/2025";
            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Font = new Font("Segoe UI", 11F);
            lblLanguage.ForeColor = Color.White;
            lblLanguage.Location = new Point(480, 107);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(120, 30);
            lblLanguage.TabIndex = 3;
            lblLanguage.Text = "Ngôn ngữ: vi";
            // 
            // pnlActions
            // 
            pnlActions.BackColor = Color.White;
            pnlActions.BorderStyle = BorderStyle.FixedSingle;
            pnlActions.Controls.Add(btnStartLearning);
            pnlActions.Controls.Add(btnViewDifferent);
            pnlActions.Location = new Point(1260, 70);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new Size(380, 160);
            pnlActions.TabIndex = 2;
            // 
            // btnStartLearning
            // 
            btnStartLearning.BackColor = Color.FromArgb(0, 180, 216);
            btnStartLearning.Cursor = Cursors.Hand;
            btnStartLearning.FlatAppearance.BorderSize = 0;
            btnStartLearning.FlatStyle = FlatStyle.Flat;
            btnStartLearning.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnStartLearning.ForeColor = Color.White;
            btnStartLearning.Location = new Point(25, 30);
            btnStartLearning.Name = "btnStartLearning";
            btnStartLearning.Size = new Size(330, 50);
            btnStartLearning.TabIndex = 0;
            btnStartLearning.Text = "▶ Bắt đầu học";
            btnStartLearning.UseVisualStyleBackColor = false;
            btnStartLearning.Click += btnStartLearning_Click;
            // 
            // btnViewDifferent
            // 
            btnViewDifferent.BackColor = Color.White;
            btnViewDifferent.Cursor = Cursors.Hand;
            btnViewDifferent.FlatStyle = FlatStyle.Flat;
            btnViewDifferent.Font = new Font("Segoe UI", 11F);
            btnViewDifferent.ForeColor = Color.FromArgb(124, 77, 255);
            btnViewDifferent.Location = new Point(25, 95);
            btnViewDifferent.Name = "btnViewDifferent";
            btnViewDifferent.Size = new Size(330, 45);
            btnViewDifferent.TabIndex = 1;
            btnViewDifferent.Text = "← Xem bộ khác";
            btnViewDifferent.UseVisualStyleBackColor = false;
            btnViewDifferent.Click += btnViewDifferent_Click;
            // 
            // pnlInfo
            // 
            pnlInfo.BackColor = Color.FromArgb(248, 249, 250);
            pnlInfo.BorderStyle = BorderStyle.FixedSingle;
            pnlInfo.Controls.Add(lblDescriptionTitle);
            pnlInfo.Controls.Add(rtbDescription);
            pnlInfo.Controls.Add(lblAuthorTitle);
            pnlInfo.Controls.Add(lblAuthorName);
            pnlInfo.Controls.Add(lblAuthorEmail);
            pnlInfo.Location = new Point(1260, 250);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Size = new Size(380, 350);
            pnlInfo.TabIndex = 3;
            // 
            // lblDescriptionTitle
            // 
            lblDescriptionTitle.AutoSize = true;
            lblDescriptionTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblDescriptionTitle.Location = new Point(25, 25);
            lblDescriptionTitle.Name = "lblDescriptionTitle";
            lblDescriptionTitle.Size = new Size(100, 36);
            lblDescriptionTitle.TabIndex = 0;
            lblDescriptionTitle.Text = "Mô tả";
            // 
            // rtbDescription
            // 
            rtbDescription.BackColor = Color.FromArgb(248, 249, 250);
            rtbDescription.BorderStyle = BorderStyle.None;
            rtbDescription.Font = new Font("Segoe UI", 10F);
            rtbDescription.Location = new Point(25, 70);
            rtbDescription.Name = "rtbDescription";
            rtbDescription.ReadOnly = true;
            rtbDescription.Size = new Size(330, 100);
            rtbDescription.TabIndex = 1;
            rtbDescription.Text = "Các thẻ HTML và CSS properties thường dùng";
            // 
            // lblAuthorTitle
            // 
            lblAuthorTitle.AutoSize = true;
            lblAuthorTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblAuthorTitle.Location = new Point(25, 190);
            lblAuthorTitle.Name = "lblAuthorTitle";
            lblAuthorTitle.Size = new Size(100, 36);
            lblAuthorTitle.TabIndex = 2;
            lblAuthorTitle.Text = "Tác giả";
            // 
            // lblAuthorName
            // 
            lblAuthorName.AutoSize = true;
            lblAuthorName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblAuthorName.Location = new Point(25, 240);
            lblAuthorName.Name = "lblAuthorName";
            lblAuthorName.Size = new Size(180, 30);
            lblAuthorName.TabIndex = 3;
            lblAuthorName.Text = "Trần Minh Khoa";
            // 
            // lblAuthorEmail
            // 
            lblAuthorEmail.AutoSize = true;
            lblAuthorEmail.Font = new Font("Segoe UI", 9F);
            lblAuthorEmail.ForeColor = Color.Gray;
            lblAuthorEmail.Location = new Point(25, 280);
            lblAuthorEmail.Name = "lblAuthorEmail";
            lblAuthorEmail.Size = new Size(220, 25);
            lblAuthorEmail.TabIndex = 4;
            lblAuthorEmail.Text = "teacher@learn.vn";
            // 
            // lblFlashcardsTitle
            // 
            lblFlashcardsTitle.AutoSize = true;
            lblFlashcardsTitle.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblFlashcardsTitle.Location = new Point(30, 250);
            lblFlashcardsTitle.Name = "lblFlashcardsTitle";
            lblFlashcardsTitle.Size = new Size(234, 46);
            lblFlashcardsTitle.TabIndex = 4;
            lblFlashcardsTitle.Text = "Danh sách thẻ";
            // 
            // flowFlashcards
            // 
            flowFlashcards.AutoScroll = true;
            flowFlashcards.FlowDirection = FlowDirection.TopDown;
            flowFlashcards.Location = new Point(30, 310);
            flowFlashcards.Name = "flowFlashcards";
            flowFlashcards.Size = new Size(1200, 750);
            flowFlashcards.TabIndex = 5;
            flowFlashcards.WrapContents = false;
            // 
            // FlashcardDetailControl
            // 
            AutoScroll = true;
            BackColor = Color.White;
            Controls.Add(flowFlashcards);
            Controls.Add(lblFlashcardsTitle);
            Controls.Add(pnlInfo);
            Controls.Add(pnlActions);
            Controls.Add(pnlHeader);
            Controls.Add(lblBreadcrumb);
            Name = "FlashcardDetailControl";
            Size = new Size(1700, 1100);
            Load += FlashcardDetailControl_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlActions.ResumeLayout(false);
            pnlInfo.ResumeLayout(false);
            pnlInfo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
