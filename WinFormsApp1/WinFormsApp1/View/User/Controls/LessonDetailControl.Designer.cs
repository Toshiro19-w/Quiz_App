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
            pnlMain = new Panel();
            pnlContent = new Panel();
            pnlContentArea = new Panel();
            pnlVideo = new Panel();
            lblVideoPlaceholder = new Label();
            pnlTheory = new Panel();
            webBrowser = new WebBrowser();
            pnlFlashcard = new Panel();
            btnCompleteFlashcard = new Button();
            btnNextCard = new Button();
            btnPrevCard = new Button();
            btnFlipCard = new Button();
            lblFlashcardBack = new Label();
            lblFlashcardFront = new Label();
            pnlTest = new Panel();
            btnSubmitTest = new Button();
            flowQuestions = new FlowLayoutPanel();
            lblTestTitle = new Label();
            pnlNavigation = new Panel();
            btnMarkComplete = new Button();
            btnNextLesson = new Button();
            btnPrevLesson = new Button();
            pnlHeader = new Panel();
            lblProgress = new Label();
            progressBar = new ProgressBar();
            lblCourseTitle = new Label();
            pnlSidebar = new Panel();
            flowLessons = new FlowLayoutPanel();
            lblSidebarTitle = new Label();
            pnlMain.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlContentArea.SuspendLayout();
            pnlVideo.SuspendLayout();
            pnlTheory.SuspendLayout();
            pnlFlashcard.SuspendLayout();
            pnlTest.SuspendLayout();
            pnlNavigation.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(pnlContent);
            pnlMain.Controls.Add(pnlSidebar);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Margin = new Padding(4, 5, 4, 5);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(0, 117, 0, 0);
            pnlMain.Size = new Size(1714, 1167);
            pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            pnlContent.Controls.Add(pnlContentArea);
            pnlContent.Controls.Add(pnlNavigation);
            pnlContent.Controls.Add(pnlHeader);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 117);
            pnlContent.Margin = new Padding(4, 5, 4, 5);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1215, 1050);
            pnlContent.TabIndex = 0;
            // 
            // pnlContentArea
            // 
            pnlContentArea.Controls.Add(pnlVideo);
            pnlContentArea.Controls.Add(pnlTheory);
            pnlContentArea.Controls.Add(pnlFlashcard);
            pnlContentArea.Controls.Add(pnlTest);
            pnlContentArea.Dock = DockStyle.Fill;
            pnlContentArea.Location = new Point(0, 165);
            pnlContentArea.Margin = new Padding(4, 5, 4, 5);
            pnlContentArea.Name = "pnlContentArea";
            pnlContentArea.Padding = new Padding(29, 33, 29, 33);
            pnlContentArea.Size = new Size(1215, 786);
            pnlContentArea.TabIndex = 1;
            // 
            // pnlVideo
            // 
            pnlVideo.BackColor = Color.Black;
            pnlVideo.Controls.Add(lblVideoPlaceholder);
            pnlVideo.Dock = DockStyle.Fill;
            pnlVideo.Location = new Point(29, 33);
            pnlVideo.Margin = new Padding(4, 5, 4, 5);
            pnlVideo.Name = "pnlVideo";
            pnlVideo.Size = new Size(1157, 720);
            pnlVideo.TabIndex = 0;
            pnlVideo.Visible = false;
            // 
            // lblVideoPlaceholder
            // 
            lblVideoPlaceholder.Dock = DockStyle.Fill;
            lblVideoPlaceholder.Font = new Font("Segoe UI", 16F);
            lblVideoPlaceholder.ForeColor = Color.White;
            lblVideoPlaceholder.Location = new Point(0, 0);
            lblVideoPlaceholder.Margin = new Padding(4, 0, 4, 0);
            lblVideoPlaceholder.Name = "lblVideoPlaceholder";
            lblVideoPlaceholder.Size = new Size(1157, 720);
            lblVideoPlaceholder.TabIndex = 0;
            lblVideoPlaceholder.Text = "? Video Player\r\n(C?n cài ??t Windows Media Player)";
            lblVideoPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlTheory
            // 
            pnlTheory.Controls.Add(webBrowser);
            pnlTheory.Dock = DockStyle.Fill;
            pnlTheory.Location = new Point(29, 33);
            pnlTheory.Margin = new Padding(4, 5, 4, 5);
            pnlTheory.Name = "pnlTheory";
            pnlTheory.Size = new Size(1157, 720);
            pnlTheory.TabIndex = 1;
            pnlTheory.Visible = false;
            // 
            // webBrowser
            // 
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.Location = new Point(0, 0);
            webBrowser.Margin = new Padding(4, 5, 4, 5);
            webBrowser.MinimumSize = new Size(29, 33);
            webBrowser.Name = "webBrowser";
            webBrowser.Size = new Size(1157, 720);
            webBrowser.TabIndex = 0;
            // 
            // pnlFlashcard
            // 
            pnlFlashcard.Controls.Add(btnCompleteFlashcard);
            pnlFlashcard.Controls.Add(btnNextCard);
            pnlFlashcard.Controls.Add(btnPrevCard);
            pnlFlashcard.Controls.Add(btnFlipCard);
            pnlFlashcard.Controls.Add(lblFlashcardBack);
            pnlFlashcard.Controls.Add(lblFlashcardFront);
            pnlFlashcard.Dock = DockStyle.Fill;
            pnlFlashcard.Location = new Point(29, 33);
            pnlFlashcard.Margin = new Padding(4, 5, 4, 5);
            pnlFlashcard.Name = "pnlFlashcard";
            pnlFlashcard.Size = new Size(1157, 720);
            pnlFlashcard.TabIndex = 2;
            pnlFlashcard.Visible = false;
            // 
            // btnCompleteFlashcard
            // 
            btnCompleteFlashcard.BackColor = Color.FromArgb(40, 167, 69);
            btnCompleteFlashcard.FlatStyle = FlatStyle.Flat;
            btnCompleteFlashcard.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCompleteFlashcard.ForeColor = Color.White;
            btnCompleteFlashcard.Location = new Point(436, 633);
            btnCompleteFlashcard.Margin = new Padding(4, 5, 4, 5);
            btnCompleteFlashcard.Name = "btnCompleteFlashcard";
            btnCompleteFlashcard.Size = new Size(286, 67);
            btnCompleteFlashcard.TabIndex = 5;
            btnCompleteFlashcard.Text = "? Hoàn thành";
            btnCompleteFlashcard.UseVisualStyleBackColor = false;
            btnCompleteFlashcard.Visible = false;
            // 
            // btnNextCard
            // 
            btnNextCard.BackColor = Color.FromArgb(108, 117, 125);
            btnNextCard.FlatStyle = FlatStyle.Flat;
            btnNextCard.Font = new Font("Segoe UI", 10F);
            btnNextCard.ForeColor = Color.White;
            btnNextCard.Location = new Point(621, 550);
            btnNextCard.Margin = new Padding(4, 5, 4, 5);
            btnNextCard.Name = "btnNextCard";
            btnNextCard.Size = new Size(171, 58);
            btnNextCard.TabIndex = 4;
            btnNextCard.Text = "Th? sau ?";
            btnNextCard.UseVisualStyleBackColor = false;
            // 
            // btnPrevCard
            // 
            btnPrevCard.BackColor = Color.FromArgb(108, 117, 125);
            btnPrevCard.FlatStyle = FlatStyle.Flat;
            btnPrevCard.Font = new Font("Segoe UI", 10F);
            btnPrevCard.ForeColor = Color.White;
            btnPrevCard.Location = new Point(364, 550);
            btnPrevCard.Margin = new Padding(4, 5, 4, 5);
            btnPrevCard.Name = "btnPrevCard";
            btnPrevCard.Size = new Size(171, 58);
            btnPrevCard.TabIndex = 3;
            btnPrevCard.Text = "? Th? tr??c";
            btnPrevCard.UseVisualStyleBackColor = false;
            // 
            // btnFlipCard
            // 
            btnFlipCard.BackColor = Color.FromArgb(52, 144, 220);
            btnFlipCard.FlatStyle = FlatStyle.Flat;
            btnFlipCard.Font = new Font("Segoe UI", 11F);
            btnFlipCard.ForeColor = Color.White;
            btnFlipCard.Location = new Point(471, 450);
            btnFlipCard.Margin = new Padding(4, 5, 4, 5);
            btnFlipCard.Name = "btnFlipCard";
            btnFlipCard.Size = new Size(214, 67);
            btnFlipCard.TabIndex = 2;
            btnFlipCard.Text = "L?t th?";
            btnFlipCard.UseVisualStyleBackColor = false;
            // 
            // lblFlashcardBack
            // 
            lblFlashcardBack.BackColor = Color.FromArgb(240, 240, 240);
            lblFlashcardBack.BorderStyle = BorderStyle.FixedSingle;
            lblFlashcardBack.Font = new Font("Segoe UI", 14F);
            lblFlashcardBack.Location = new Point(221, 83);
            lblFlashcardBack.Margin = new Padding(4, 0, 4, 0);
            lblFlashcardBack.Name = "lblFlashcardBack";
            lblFlashcardBack.Size = new Size(713, 332);
            lblFlashcardBack.TabIndex = 1;
            lblFlashcardBack.Text = "M?t sau th?";
            lblFlashcardBack.TextAlign = ContentAlignment.MiddleCenter;
            lblFlashcardBack.Visible = false;
            // 
            // lblFlashcardFront
            // 
            lblFlashcardFront.BackColor = Color.White;
            lblFlashcardFront.BorderStyle = BorderStyle.FixedSingle;
            lblFlashcardFront.Font = new Font("Segoe UI", 18F);
            lblFlashcardFront.Location = new Point(221, 83);
            lblFlashcardFront.Margin = new Padding(4, 0, 4, 0);
            lblFlashcardFront.Name = "lblFlashcardFront";
            lblFlashcardFront.Size = new Size(713, 332);
            lblFlashcardFront.TabIndex = 0;
            lblFlashcardFront.Text = "M?t tr??c th?";
            lblFlashcardFront.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlTest
            // 
            pnlTest.AutoScroll = true;
            pnlTest.Controls.Add(btnSubmitTest);
            pnlTest.Controls.Add(flowQuestions);
            pnlTest.Controls.Add(lblTestTitle);
            pnlTest.Dock = DockStyle.Fill;
            pnlTest.Location = new Point(29, 33);
            pnlTest.Margin = new Padding(4, 5, 4, 5);
            pnlTest.Name = "pnlTest";
            pnlTest.Size = new Size(1157, 720);
            pnlTest.TabIndex = 3;
            pnlTest.Visible = false;
            // 
            // btnSubmitTest
            // 
            btnSubmitTest.BackColor = Color.FromArgb(220, 53, 69);
            btnSubmitTest.Dock = DockStyle.Bottom;
            btnSubmitTest.FlatStyle = FlatStyle.Flat;
            btnSubmitTest.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSubmitTest.ForeColor = Color.White;
            btnSubmitTest.Location = new Point(0, 637);
            btnSubmitTest.Margin = new Padding(4, 5, 4, 5);
            btnSubmitTest.Name = "btnSubmitTest";
            btnSubmitTest.Size = new Size(1157, 83);
            btnSubmitTest.TabIndex = 2;
            btnSubmitTest.Text = "N?p bài";
            btnSubmitTest.UseVisualStyleBackColor = false;
            // 
            // flowQuestions
            // 
            flowQuestions.AutoScroll = true;
            flowQuestions.Dock = DockStyle.Fill;
            flowQuestions.FlowDirection = FlowDirection.TopDown;
            flowQuestions.Location = new Point(0, 83);
            flowQuestions.Margin = new Padding(4, 5, 4, 5);
            flowQuestions.Name = "flowQuestions";
            flowQuestions.Padding = new Padding(14, 17, 14, 17);
            flowQuestions.Size = new Size(1157, 637);
            flowQuestions.TabIndex = 1;
            flowQuestions.WrapContents = false;
            // 
            // lblTestTitle
            // 
            lblTestTitle.Dock = DockStyle.Top;
            lblTestTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTestTitle.Location = new Point(0, 0);
            lblTestTitle.Margin = new Padding(4, 0, 4, 0);
            lblTestTitle.Name = "lblTestTitle";
            lblTestTitle.Padding = new Padding(14, 17, 14, 17);
            lblTestTitle.Size = new Size(1157, 83);
            lblTestTitle.TabIndex = 0;
            lblTestTitle.Text = "Bài ki?m tra";
            // 
            // pnlNavigation
            // 
            pnlNavigation.BackColor = Color.White;
            pnlNavigation.BorderStyle = BorderStyle.FixedSingle;
            pnlNavigation.Controls.Add(btnMarkComplete);
            pnlNavigation.Controls.Add(btnNextLesson);
            pnlNavigation.Controls.Add(btnPrevLesson);
            pnlNavigation.Dock = DockStyle.Bottom;
            pnlNavigation.Location = new Point(0, 951);
            pnlNavigation.Margin = new Padding(4, 5, 4, 5);
            pnlNavigation.Name = "pnlNavigation";
            pnlNavigation.Padding = new Padding(29, 17, 29, 17);
            pnlNavigation.Size = new Size(1215, 99);
            pnlNavigation.TabIndex = 2;
            // 
            // btnMarkComplete
            // 
            btnMarkComplete.BackColor = Color.FromArgb(40, 167, 69);
            btnMarkComplete.FlatStyle = FlatStyle.Flat;
            btnMarkComplete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnMarkComplete.ForeColor = Color.White;
            btnMarkComplete.Location = new Point(500, 17);
            btnMarkComplete.Margin = new Padding(4, 5, 4, 5);
            btnMarkComplete.Name = "btnMarkComplete";
            btnMarkComplete.Size = new Size(214, 63);
            btnMarkComplete.TabIndex = 2;
            btnMarkComplete.Text = "? ?ánh d?u hoàn thành";
            btnMarkComplete.UseVisualStyleBackColor = false;
            // 
            // btnNextLesson
            // 
            btnNextLesson.BackColor = Color.FromArgb(52, 144, 220);
            btnNextLesson.Dock = DockStyle.Right;
            btnNextLesson.FlatStyle = FlatStyle.Flat;
            btnNextLesson.Font = new Font("Segoe UI", 10F);
            btnNextLesson.ForeColor = Color.White;
            btnNextLesson.Location = new Point(970, 17);
            btnNextLesson.Margin = new Padding(4, 5, 4, 5);
            btnNextLesson.Name = "btnNextLesson";
            btnNextLesson.Size = new Size(214, 63);
            btnNextLesson.TabIndex = 1;
            btnNextLesson.Text = "Bài sau ?";
            btnNextLesson.UseVisualStyleBackColor = false;
            // 
            // btnPrevLesson
            // 
            btnPrevLesson.BackColor = Color.FromArgb(108, 117, 125);
            btnPrevLesson.Dock = DockStyle.Left;
            btnPrevLesson.FlatStyle = FlatStyle.Flat;
            btnPrevLesson.Font = new Font("Segoe UI", 10F);
            btnPrevLesson.ForeColor = Color.White;
            btnPrevLesson.Location = new Point(29, 17);
            btnPrevLesson.Margin = new Padding(4, 5, 4, 5);
            btnPrevLesson.Name = "btnPrevLesson";
            btnPrevLesson.Size = new Size(214, 63);
            btnPrevLesson.TabIndex = 0;
            btnPrevLesson.Text = "? Bài tr??c";
            btnPrevLesson.UseVisualStyleBackColor = false;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.BorderStyle = BorderStyle.FixedSingle;
            pnlHeader.Controls.Add(lblProgress);
            pnlHeader.Controls.Add(progressBar);
            pnlHeader.Controls.Add(lblCourseTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(4, 5, 4, 5);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(29, 33, 29, 33);
            pnlHeader.Size = new Size(1215, 165);
            pnlHeader.TabIndex = 0;
            // 
            // lblProgress
            // 
            lblProgress.Dock = DockStyle.Bottom;
            lblProgress.Font = new Font("Segoe UI", 9F);
            lblProgress.ForeColor = Color.Gray;
            lblProgress.Location = new Point(29, 91);
            lblProgress.Margin = new Padding(4, 0, 4, 0);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(1155, 22);
            lblProgress.TabIndex = 2;
            lblProgress.Text = "Ti?n ??: 0%";
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Bottom;
            progressBar.Location = new Point(29, 113);
            progressBar.Margin = new Padding(4, 5, 4, 5);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1155, 17);
            progressBar.TabIndex = 1;
            // 
            // lblCourseTitle
            // 
            lblCourseTitle.Dock = DockStyle.Top;
            lblCourseTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblCourseTitle.Location = new Point(29, 33);
            lblCourseTitle.Margin = new Padding(4, 0, 4, 0);
            lblCourseTitle.Name = "lblCourseTitle";
            lblCourseTitle.Size = new Size(1155, 50);
            lblCourseTitle.TabIndex = 0;
            lblCourseTitle.Text = "Tên khóa h?c";
            lblCourseTitle.Click += lblCourseTitle_Click;
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(248, 249, 250);
            pnlSidebar.BorderStyle = BorderStyle.FixedSingle;
            pnlSidebar.Controls.Add(flowLessons);
            pnlSidebar.Controls.Add(lblSidebarTitle);
            pnlSidebar.Dock = DockStyle.Right;
            pnlSidebar.Location = new Point(1215, 117);
            pnlSidebar.Margin = new Padding(4, 5, 4, 5);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(499, 1050);
            pnlSidebar.TabIndex = 1;
            // 
            // flowLessons
            // 
            flowLessons.AutoScroll = true;
            flowLessons.Dock = DockStyle.Fill;
            flowLessons.FlowDirection = FlowDirection.TopDown;
            flowLessons.Location = new Point(0, 83);
            flowLessons.Margin = new Padding(4, 5, 4, 5);
            flowLessons.Name = "flowLessons";
            flowLessons.Padding = new Padding(14, 17, 14, 17);
            flowLessons.Size = new Size(497, 965);
            flowLessons.TabIndex = 1;
            flowLessons.WrapContents = false;
            // 
            // lblSidebarTitle
            // 
            lblSidebarTitle.BackColor = Color.FromArgb(52, 144, 220);
            lblSidebarTitle.Dock = DockStyle.Top;
            lblSidebarTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSidebarTitle.ForeColor = Color.White;
            lblSidebarTitle.Location = new Point(0, 0);
            lblSidebarTitle.Margin = new Padding(4, 0, 4, 0);
            lblSidebarTitle.Name = "lblSidebarTitle";
            lblSidebarTitle.Padding = new Padding(21, 17, 21, 17);
            lblSidebarTitle.Size = new Size(497, 83);
            lblSidebarTitle.TabIndex = 0;
            lblSidebarTitle.Text = "N?i dung bài h?c";
            // 
            // LessonDetailControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(pnlMain);
            Margin = new Padding(4, 5, 4, 5);
            Name = "LessonDetailControl";
            Size = new Size(1714, 1167);
            pnlMain.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlContentArea.ResumeLayout(false);
            pnlVideo.ResumeLayout(false);
            pnlTheory.ResumeLayout(false);
            pnlFlashcard.ResumeLayout(false);
            pnlTest.ResumeLayout(false);
            pnlNavigation.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlSidebar.ResumeLayout(false);
            ResumeLayout(false);
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
