namespace WinFormsApp1.View.User.Controls
{
    partial class LessonDetailControl
    {
        private System.ComponentModel.IContainer components = null;

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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.lblSidebarTitle = new System.Windows.Forms.Label();
            this.flowLessons = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblCourseTitle = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pnlContentArea = new System.Windows.Forms.Panel();
            this.pnlVideo = new System.Windows.Forms.Panel();
            this.lblVideoPlaceholder = new System.Windows.Forms.Label();
            this.pnlTheory = new System.Windows.Forms.Panel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.pnlFlashcard = new System.Windows.Forms.Panel();
            this.lblFlashcardFront = new System.Windows.Forms.Label();
            this.lblFlashcardBack = new System.Windows.Forms.Label();
            this.btnFlipCard = new System.Windows.Forms.Button();
            this.btnPrevCard = new System.Windows.Forms.Button();
            this.btnNextCard = new System.Windows.Forms.Button();
            this.btnCompleteFlashcard = new System.Windows.Forms.Button();
            this.pnlTest = new System.Windows.Forms.Panel();
            this.lblTestTitle = new System.Windows.Forms.Label();
            this.flowQuestions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSubmitTest = new System.Windows.Forms.Button();
            this.pnlNavigation = new System.Windows.Forms.Panel();
            this.btnPrevLesson = new System.Windows.Forms.Button();
            this.btnNextLesson = new System.Windows.Forms.Button();
            this.btnMarkComplete = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContentArea.SuspendLayout();
            this.pnlVideo.SuspendLayout();
            this.pnlTheory.SuspendLayout();
            this.pnlFlashcard.SuspendLayout();
            this.pnlTest.SuspendLayout();
            this.pnlNavigation.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlSidebar);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(0, 70, 0, 0);
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlContentArea);
            this.pnlContent.Controls.Add(this.pnlNavigation);
            this.pnlContent.Controls.Add(this.pnlHeader);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 70);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(850, 630);
            this.pnlContent.TabIndex = 0;
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlSidebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSidebar.Controls.Add(this.flowLessons);
            this.pnlSidebar.Controls.Add(this.lblSidebarTitle);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSidebar.Location = new System.Drawing.Point(850, 70);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(350, 630);
            this.pnlSidebar.TabIndex = 1;
            // 
            // lblSidebarTitle
            // 
            this.lblSidebarTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(144)))), ((int)(((byte)(220)))));
            this.lblSidebarTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSidebarTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSidebarTitle.ForeColor = System.Drawing.Color.White;
            this.lblSidebarTitle.Location = new System.Drawing.Point(0, 0);
            this.lblSidebarTitle.Name = "lblSidebarTitle";
            this.lblSidebarTitle.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.lblSidebarTitle.Size = new System.Drawing.Size(348, 50);
            this.lblSidebarTitle.TabIndex = 0;
            this.lblSidebarTitle.Text = "N?i dung bài h?c";
            // 
            // flowLessons
            // 
            this.flowLessons.AutoScroll = true;
            this.flowLessons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLessons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLessons.Location = new System.Drawing.Point(0, 50);
            this.flowLessons.Name = "flowLessons";
            this.flowLessons.Padding = new System.Windows.Forms.Padding(10);
            this.flowLessons.Size = new System.Drawing.Size(348, 578);
            this.flowLessons.TabIndex = 1;
            this.flowLessons.WrapContents = false;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.lblProgress);
            this.pnlHeader.Controls.Add(this.progressBar);
            this.pnlHeader.Controls.Add(this.lblCourseTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20);
            this.pnlHeader.Size = new System.Drawing.Size(850, 100);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblCourseTitle
            // 
            this.lblCourseTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCourseTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCourseTitle.Location = new System.Drawing.Point(20, 20);
            this.lblCourseTitle.Name = "lblCourseTitle";
            this.lblCourseTitle.Size = new System.Drawing.Size(808, 30);
            this.lblCourseTitle.TabIndex = 0;
            this.lblCourseTitle.Text = "Tên khóa h?c";
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(20, 55);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(808, 10);
            this.progressBar.TabIndex = 1;
            // 
            // lblProgress
            // 
            this.lblProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProgress.ForeColor = System.Drawing.Color.Gray;
            this.lblProgress.Location = new System.Drawing.Point(20, 65);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(808, 13);
            this.lblProgress.TabIndex = 2;
            this.lblProgress.Text = "Ti?n ??: 0%";
            // 
            // pnlContentArea
            // 
            this.pnlContentArea.Controls.Add(this.pnlVideo);
            this.pnlContentArea.Controls.Add(this.pnlTheory);
            this.pnlContentArea.Controls.Add(this.pnlFlashcard);
            this.pnlContentArea.Controls.Add(this.pnlTest);
            this.pnlContentArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContentArea.Location = new System.Drawing.Point(0, 100);
            this.pnlContentArea.Name = "pnlContentArea";
            this.pnlContentArea.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContentArea.Size = new System.Drawing.Size(850, 470);
            this.pnlContentArea.TabIndex = 1;
            // 
            // pnlVideo
            // 
            this.pnlVideo.BackColor = System.Drawing.Color.Black;
            this.pnlVideo.Controls.Add(this.lblVideoPlaceholder);
            this.pnlVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVideo.Location = new System.Drawing.Point(20, 20);
            this.pnlVideo.Name = "pnlVideo";
            this.pnlVideo.Size = new System.Drawing.Size(810, 430);
            this.pnlVideo.TabIndex = 0;
            this.pnlVideo.Visible = false;
            // 
            // lblVideoPlaceholder
            // 
            this.lblVideoPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVideoPlaceholder.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblVideoPlaceholder.ForeColor = System.Drawing.Color.White;
            this.lblVideoPlaceholder.Location = new System.Drawing.Point(0, 0);
            this.lblVideoPlaceholder.Name = "lblVideoPlaceholder";
            this.lblVideoPlaceholder.Size = new System.Drawing.Size(810, 430);
            this.lblVideoPlaceholder.TabIndex = 0;
            this.lblVideoPlaceholder.Text = "? Video Player\r\n(C?n cài ??t Windows Media Player)";
            this.lblVideoPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTheory
            // 
            this.pnlTheory.Controls.Add(this.webBrowser);
            this.pnlTheory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTheory.Location = new System.Drawing.Point(20, 20);
            this.pnlTheory.Name = "pnlTheory";
            this.pnlTheory.Size = new System.Drawing.Size(810, 430);
            this.pnlTheory.TabIndex = 1;
            this.pnlTheory.Visible = false;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(810, 430);
            this.webBrowser.TabIndex = 0;
            // 
            // pnlFlashcard
            // 
            this.pnlFlashcard.Controls.Add(this.btnCompleteFlashcard);
            this.pnlFlashcard.Controls.Add(this.btnNextCard);
            this.pnlFlashcard.Controls.Add(this.btnPrevCard);
            this.pnlFlashcard.Controls.Add(this.btnFlipCard);
            this.pnlFlashcard.Controls.Add(this.lblFlashcardBack);
            this.pnlFlashcard.Controls.Add(this.lblFlashcardFront);
            this.pnlFlashcard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFlashcard.Location = new System.Drawing.Point(20, 20);
            this.pnlFlashcard.Name = "pnlFlashcard";
            this.pnlFlashcard.Size = new System.Drawing.Size(810, 430);
            this.pnlFlashcard.TabIndex = 2;
            this.pnlFlashcard.Visible = false;
            // 
            // lblFlashcardFront
            // 
            this.lblFlashcardFront.BackColor = System.Drawing.Color.White;
            this.lblFlashcardFront.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFlashcardFront.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblFlashcardFront.Location = new System.Drawing.Point(155, 50);
            this.lblFlashcardFront.Name = "lblFlashcardFront";
            this.lblFlashcardFront.Size = new System.Drawing.Size(500, 200);
            this.lblFlashcardFront.TabIndex = 0;
            this.lblFlashcardFront.Text = "M?t tr??c th?";
            this.lblFlashcardFront.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFlashcardBack
            // 
            this.lblFlashcardBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblFlashcardBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFlashcardBack.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblFlashcardBack.Location = new System.Drawing.Point(155, 50);
            this.lblFlashcardBack.Name = "lblFlashcardBack";
            this.lblFlashcardBack.Size = new System.Drawing.Size(500, 200);
            this.lblFlashcardBack.TabIndex = 1;
            this.lblFlashcardBack.Text = "M?t sau th?";
            this.lblFlashcardBack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFlashcardBack.Visible = false;
            // 
            // btnFlipCard
            // 
            this.btnFlipCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(144)))), ((int)(((byte)(220)))));
            this.btnFlipCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFlipCard.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnFlipCard.ForeColor = System.Drawing.Color.White;
            this.btnFlipCard.Location = new System.Drawing.Point(330, 270);
            this.btnFlipCard.Name = "btnFlipCard";
            this.btnFlipCard.Size = new System.Drawing.Size(150, 40);
            this.btnFlipCard.TabIndex = 2;
            this.btnFlipCard.Text = "L?t th?";
            this.btnFlipCard.UseVisualStyleBackColor = false;
            // 
            // btnPrevCard
            // 
            this.btnPrevCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnPrevCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevCard.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPrevCard.ForeColor = System.Drawing.Color.White;
            this.btnPrevCard.Location = new System.Drawing.Point(255, 330);
            this.btnPrevCard.Name = "btnPrevCard";
            this.btnPrevCard.Size = new System.Drawing.Size(120, 35);
            this.btnPrevCard.TabIndex = 3;
            this.btnPrevCard.Text = "? Th? tr??c";
            this.btnPrevCard.UseVisualStyleBackColor = false;
            // 
            // btnNextCard
            // 
            this.btnNextCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnNextCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextCard.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnNextCard.ForeColor = System.Drawing.Color.White;
            this.btnNextCard.Location = new System.Drawing.Point(435, 330);
            this.btnNextCard.Name = "btnNextCard";
            this.btnNextCard.Size = new System.Drawing.Size(120, 35);
            this.btnNextCard.TabIndex = 4;
            this.btnNextCard.Text = "Th? sau ?";
            this.btnNextCard.UseVisualStyleBackColor = false;
            // 
            // btnCompleteFlashcard
            // 
            this.btnCompleteFlashcard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCompleteFlashcard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompleteFlashcard.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCompleteFlashcard.ForeColor = System.Drawing.Color.White;
            this.btnCompleteFlashcard.Location = new System.Drawing.Point(305, 380);
            this.btnCompleteFlashcard.Name = "btnCompleteFlashcard";
            this.btnCompleteFlashcard.Size = new System.Drawing.Size(200, 40);
            this.btnCompleteFlashcard.TabIndex = 5;
            this.btnCompleteFlashcard.Text = "? Hoàn thành";
            this.btnCompleteFlashcard.UseVisualStyleBackColor = false;
            this.btnCompleteFlashcard.Visible = false;
            // 
            // pnlTest
            // 
            this.pnlTest.AutoScroll = true;
            this.pnlTest.Controls.Add(this.btnSubmitTest);
            this.pnlTest.Controls.Add(this.flowQuestions);
            this.pnlTest.Controls.Add(this.lblTestTitle);
            this.pnlTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTest.Location = new System.Drawing.Point(20, 20);
            this.pnlTest.Name = "pnlTest";
            this.pnlTest.Size = new System.Drawing.Size(810, 430);
            this.pnlTest.TabIndex = 3;
            this.pnlTest.Visible = false;
            // 
            // lblTestTitle
            // 
            this.lblTestTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTestTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTestTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTestTitle.Name = "lblTestTitle";
            this.lblTestTitle.Padding = new System.Windows.Forms.Padding(10);
            this.lblTestTitle.Size = new System.Drawing.Size(810, 50);
            this.lblTestTitle.TabIndex = 0;
            this.lblTestTitle.Text = "Bài ki?m tra";
            // 
            // flowQuestions
            // 
            this.flowQuestions.AutoScroll = true;
            this.flowQuestions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowQuestions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowQuestions.Location = new System.Drawing.Point(0, 50);
            this.flowQuestions.Name = "flowQuestions";
            this.flowQuestions.Padding = new System.Windows.Forms.Padding(10);
            this.flowQuestions.Size = new System.Drawing.Size(810, 330);
            this.flowQuestions.TabIndex = 1;
            this.flowQuestions.WrapContents = false;
            // 
            // btnSubmitTest
            // 
            this.btnSubmitTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnSubmitTest.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSubmitTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmitTest.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSubmitTest.ForeColor = System.Drawing.Color.White;
            this.btnSubmitTest.Location = new System.Drawing.Point(0, 380);
            this.btnSubmitTest.Name = "btnSubmitTest";
            this.btnSubmitTest.Size = new System.Drawing.Size(810, 50);
            this.btnSubmitTest.TabIndex = 2;
            this.btnSubmitTest.Text = "N?p bài";
            this.btnSubmitTest.UseVisualStyleBackColor = false;
            // 
            // pnlNavigation
            // 
            this.pnlNavigation.BackColor = System.Drawing.Color.White;
            this.pnlNavigation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNavigation.Controls.Add(this.btnMarkComplete);
            this.pnlNavigation.Controls.Add(this.btnNextLesson);
            this.pnlNavigation.Controls.Add(this.btnPrevLesson);
            this.pnlNavigation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNavigation.Location = new System.Drawing.Point(0, 570);
            this.pnlNavigation.Name = "pnlNavigation";
            this.pnlNavigation.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlNavigation.Size = new System.Drawing.Size(850, 60);
            this.pnlNavigation.TabIndex = 2;
            // 
            // btnPrevLesson
            // 
            this.btnPrevLesson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnPrevLesson.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrevLesson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevLesson.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPrevLesson.ForeColor = System.Drawing.Color.White;
            this.btnPrevLesson.Location = new System.Drawing.Point(20, 10);
            this.btnPrevLesson.Name = "btnPrevLesson";
            this.btnPrevLesson.Size = new System.Drawing.Size(150, 38);
            this.btnPrevLesson.TabIndex = 0;
            this.btnPrevLesson.Text = "? Bài tr??c";
            this.btnPrevLesson.UseVisualStyleBackColor = false;
            // 
            // btnNextLesson
            // 
            this.btnNextLesson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(144)))), ((int)(((byte)(220)))));
            this.btnNextLesson.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNextLesson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextLesson.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnNextLesson.ForeColor = System.Drawing.Color.White;
            this.btnNextLesson.Location = new System.Drawing.Point(678, 10);
            this.btnNextLesson.Name = "btnNextLesson";
            this.btnNextLesson.Size = new System.Drawing.Size(150, 38);
            this.btnNextLesson.TabIndex = 1;
            this.btnNextLesson.Text = "Bài sau ?";
            this.btnNextLesson.UseVisualStyleBackColor = false;
            // 
            // btnMarkComplete
            // 
            this.btnMarkComplete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnMarkComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarkComplete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMarkComplete.ForeColor = System.Drawing.Color.White;
            this.btnMarkComplete.Location = new System.Drawing.Point(350, 10);
            this.btnMarkComplete.Name = "btnMarkComplete";
            this.btnMarkComplete.Size = new System.Drawing.Size(150, 38);
            this.btnMarkComplete.TabIndex = 2;
            this.btnMarkComplete.Text = "? ?ánh d?u hoàn thành";
            this.btnMarkComplete.UseVisualStyleBackColor = false;
            // 
            // LessonDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlMain);
            this.Name = "LessonDetailControl";
            this.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlContentArea.ResumeLayout(false);
            this.pnlVideo.ResumeLayout(false);
            this.pnlTheory.ResumeLayout(false);
            this.pnlFlashcard.ResumeLayout(false);
            this.pnlTest.ResumeLayout(false);
            this.pnlNavigation.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Label lblSidebarTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLessons;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblCourseTitle;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Panel pnlContentArea;
        private System.Windows.Forms.Panel pnlVideo;
        private System.Windows.Forms.Label lblVideoPlaceholder;
        private System.Windows.Forms.Panel pnlTheory;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Panel pnlFlashcard;
        private System.Windows.Forms.Label lblFlashcardFront;
        private System.Windows.Forms.Label lblFlashcardBack;
        private System.Windows.Forms.Button btnFlipCard;
        private System.Windows.Forms.Button btnPrevCard;
        private System.Windows.Forms.Button btnNextCard;
        private System.Windows.Forms.Button btnCompleteFlashcard;
        private System.Windows.Forms.Panel pnlTest;
        private System.Windows.Forms.Label lblTestTitle;
        private System.Windows.Forms.FlowLayoutPanel flowQuestions;
        private System.Windows.Forms.Button btnSubmitTest;
        private System.Windows.Forms.Panel pnlNavigation;
        private System.Windows.Forms.Button btnPrevLesson;
        private System.Windows.Forms.Button btnNextLesson;
        private System.Windows.Forms.Button btnMarkComplete;
    }
}
