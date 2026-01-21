using System.Windows.Forms;

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
            pdfViewer = new PdfiumViewer.PdfViewer();
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
            btnCertificate = new Button();
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
            pnlMain.Margin = new Padding(3, 4, 3, 4);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1371, 934);
            pnlMain.TabIndex = 0;
            pnlMain.Paint += pnlMain_Paint;
            // 
            // pnlContent
            // 
            pnlContent.Controls.Add(pnlContentArea);
            pnlContent.Controls.Add(pnlNavigation);
            pnlContent.Controls.Add(pnlHeader);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 0);
            pnlContent.Margin = new Padding(3, 4, 3, 4);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(971, 934);
            pnlContent.TabIndex = 0;
            // 
            // pnlContentArea
            // 
            pnlContentArea.Controls.Add(pnlVideo);
            pnlContentArea.Controls.Add(pnlTheory);
            pnlContentArea.Controls.Add(pnlFlashcard);
            pnlContentArea.Controls.Add(pnlTest);
            pnlContentArea.Dock = DockStyle.Fill;
            pnlContentArea.Location = new Point(0, 132);
            pnlContentArea.Margin = new Padding(3, 4, 3, 4);
            pnlContentArea.Name = "pnlContentArea";
            pnlContentArea.Padding = new Padding(23, 26, 23, 26);
            pnlContentArea.Size = new Size(971, 722);
            pnlContentArea.TabIndex = 1;
            // 
            // pnlVideo
            // 
            pnlVideo.BackColor = Color.Black;
            pnlVideo.Controls.Add(lblVideoPlaceholder);
            pnlVideo.Dock = DockStyle.Fill;
            pnlVideo.Location = new Point(23, 26);
            pnlVideo.Margin = new Padding(3, 4, 3, 4);
            pnlVideo.Name = "pnlVideo";
            pnlVideo.Size = new Size(925, 670);
            pnlVideo.TabIndex = 0;
            pnlVideo.Visible = false;
            // 
            // lblVideoPlaceholder
            // 
            lblVideoPlaceholder.Dock = DockStyle.Fill;
            lblVideoPlaceholder.Font = new Font("Segoe UI", 16F);
            lblVideoPlaceholder.ForeColor = Color.White;
            lblVideoPlaceholder.Location = new Point(0, 0);
            lblVideoPlaceholder.Name = "lblVideoPlaceholder";
            lblVideoPlaceholder.Size = new Size(925, 670);
            lblVideoPlaceholder.TabIndex = 0;
            lblVideoPlaceholder.Text = "- Video Player\r\n(Cần cài đặt Windows Media Player)";
            lblVideoPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
            lblVideoPlaceholder.Click += lblVideoPlaceholder_Click;
            // 
            // pnlTheory
            // 
            pnlTheory.Controls.Add(pdfViewer);
            pnlTheory.Dock = DockStyle.Fill;
            pnlTheory.Location = new Point(23, 26);
            pnlTheory.Margin = new Padding(3, 4, 3, 4);
            pnlTheory.Name = "pnlTheory";
            pnlTheory.Size = new Size(925, 670);
            pnlTheory.TabIndex = 1;
            pnlTheory.Visible = false;
            // 
            // pdfViewer
            // 
            pdfViewer.Dock = DockStyle.Fill;
            pdfViewer.Location = new Point(0, 0);
            pdfViewer.Margin = new Padding(3, 4, 3, 4);
            pdfViewer.Name = "pdfViewer";
            pdfViewer.Size = new Size(925, 670);
            pdfViewer.TabIndex = 0;
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
            pnlFlashcard.Location = new Point(23, 26);
            pnlFlashcard.Margin = new Padding(3, 4, 3, 4);
            pnlFlashcard.Name = "pnlFlashcard";
            pnlFlashcard.Size = new Size(925, 670);
            pnlFlashcard.TabIndex = 2;
            pnlFlashcard.Visible = false;
            // 
            // btnCompleteFlashcard
            // 
            btnCompleteFlashcard.BackColor = Color.FromArgb(40, 167, 69);
            btnCompleteFlashcard.FlatStyle = FlatStyle.Flat;
            btnCompleteFlashcard.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCompleteFlashcard.ForeColor = Color.White;
            btnCompleteFlashcard.Location = new Point(349, 506);
            btnCompleteFlashcard.Margin = new Padding(3, 4, 3, 4);
            btnCompleteFlashcard.Name = "btnCompleteFlashcard";
            btnCompleteFlashcard.Size = new Size(229, 54);
            btnCompleteFlashcard.TabIndex = 5;
            btnCompleteFlashcard.Text = "- Hoàn thành";
            btnCompleteFlashcard.UseVisualStyleBackColor = false;
            btnCompleteFlashcard.Visible = false;
            // 
            // btnNextCard
            // 
            btnNextCard.BackColor = Color.FromArgb(108, 117, 125);
            btnNextCard.FlatStyle = FlatStyle.Flat;
            btnNextCard.Font = new Font("Segoe UI", 10F);
            btnNextCard.ForeColor = Color.White;
            btnNextCard.Location = new Point(497, 440);
            btnNextCard.Margin = new Padding(3, 4, 3, 4);
            btnNextCard.Name = "btnNextCard";
            btnNextCard.Size = new Size(137, 46);
            btnNextCard.TabIndex = 4;
            btnNextCard.Text = "Thẻ sau ";
            btnNextCard.UseVisualStyleBackColor = false;
            // 
            // btnPrevCard
            // 
            btnPrevCard.BackColor = Color.FromArgb(108, 117, 125);
            btnPrevCard.FlatStyle = FlatStyle.Flat;
            btnPrevCard.Font = new Font("Segoe UI", 10F);
            btnPrevCard.ForeColor = Color.White;
            btnPrevCard.Location = new Point(291, 440);
            btnPrevCard.Margin = new Padding(3, 4, 3, 4);
            btnPrevCard.Name = "btnPrevCard";
            btnPrevCard.Size = new Size(137, 46);
            btnPrevCard.TabIndex = 3;
            btnPrevCard.Text = "- Thẻ trước";
            btnPrevCard.UseVisualStyleBackColor = false;
            // 
            // btnFlipCard
            // 
            btnFlipCard.BackColor = Color.FromArgb(52, 144, 220);
            btnFlipCard.FlatStyle = FlatStyle.Flat;
            btnFlipCard.Font = new Font("Segoe UI", 11F);
            btnFlipCard.ForeColor = Color.White;
            btnFlipCard.Location = new Point(377, 360);
            btnFlipCard.Margin = new Padding(3, 4, 3, 4);
            btnFlipCard.Name = "btnFlipCard";
            btnFlipCard.Size = new Size(171, 54);
            btnFlipCard.TabIndex = 2;
            btnFlipCard.Text = "Lật thẻ";
            btnFlipCard.UseVisualStyleBackColor = false;
            // 
            // lblFlashcardBack
            // 
            lblFlashcardBack.BackColor = Color.FromArgb(240, 240, 240);
            lblFlashcardBack.BorderStyle = BorderStyle.FixedSingle;
            lblFlashcardBack.Font = new Font("Segoe UI", 14F);
            lblFlashcardBack.Location = new Point(177, 66);
            lblFlashcardBack.Name = "lblFlashcardBack";
            lblFlashcardBack.Size = new Size(571, 266);
            lblFlashcardBack.TabIndex = 1;
            lblFlashcardBack.Text = "Mặt sau thẻ";
            lblFlashcardBack.TextAlign = ContentAlignment.MiddleCenter;
            lblFlashcardBack.Visible = false;
            // 
            // lblFlashcardFront
            // 
            lblFlashcardFront.BackColor = Color.White;
            lblFlashcardFront.BorderStyle = BorderStyle.FixedSingle;
            lblFlashcardFront.Font = new Font("Segoe UI", 18F);
            lblFlashcardFront.Location = new Point(177, 66);
            lblFlashcardFront.Name = "lblFlashcardFront";
            lblFlashcardFront.Size = new Size(571, 266);
            lblFlashcardFront.TabIndex = 0;
            lblFlashcardFront.Text = "Mặt trước thẻ";
            lblFlashcardFront.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlTest
            // 
            pnlTest.AutoScroll = true;
            pnlTest.Controls.Add(btnSubmitTest);
            pnlTest.Controls.Add(flowQuestions);
            pnlTest.Controls.Add(lblTestTitle);
            pnlTest.Dock = DockStyle.Fill;
            pnlTest.Location = new Point(23, 26);
            pnlTest.Margin = new Padding(3, 4, 3, 4);
            pnlTest.Name = "pnlTest";
            pnlTest.Size = new Size(925, 670);
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
            btnSubmitTest.Location = new Point(0, 604);
            btnSubmitTest.Margin = new Padding(3, 4, 3, 4);
            btnSubmitTest.Name = "btnSubmitTest";
            btnSubmitTest.Size = new Size(925, 66);
            btnSubmitTest.TabIndex = 2;
            btnSubmitTest.Text = "Nộp bài";
            btnSubmitTest.UseVisualStyleBackColor = false;
            // 
            // flowQuestions
            // 
            flowQuestions.AutoScroll = true;
            flowQuestions.Dock = DockStyle.Fill;
            flowQuestions.FlowDirection = FlowDirection.TopDown;
            flowQuestions.Location = new Point(0, 66);
            flowQuestions.Margin = new Padding(3, 4, 3, 4);
            flowQuestions.Name = "flowQuestions";
            flowQuestions.Padding = new Padding(11, 14, 11, 80);
            flowQuestions.Size = new Size(925, 604);
            flowQuestions.TabIndex = 1;
            flowQuestions.WrapContents = false;
            // 
            // lblTestTitle
            // 
            lblTestTitle.Dock = DockStyle.Top;
            lblTestTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTestTitle.Location = new Point(0, 0);
            lblTestTitle.Name = "lblTestTitle";
            lblTestTitle.Padding = new Padding(11, 14, 11, 14);
            lblTestTitle.Size = new Size(925, 66);
            lblTestTitle.TabIndex = 0;
            lblTestTitle.Text = "Bài kiểm tra";
            // 
            // pnlNavigation
            // 
            pnlNavigation.BackColor = Color.White;
            pnlNavigation.BorderStyle = BorderStyle.FixedSingle;
            pnlNavigation.Controls.Add(btnMarkComplete);
            pnlNavigation.Controls.Add(btnNextLesson);
            pnlNavigation.Controls.Add(btnPrevLesson);
            pnlNavigation.Dock = DockStyle.Bottom;
            pnlNavigation.Location = new Point(0, 854);
            pnlNavigation.Margin = new Padding(3, 4, 3, 4);
            pnlNavigation.Name = "pnlNavigation";
            pnlNavigation.Padding = new Padding(23, 14, 23, 14);
            pnlNavigation.Size = new Size(971, 80);
            pnlNavigation.TabIndex = 2;
            // 
            // btnMarkComplete
            // 
            btnMarkComplete.BackColor = Color.FromArgb(40, 167, 69);
            btnMarkComplete.FlatStyle = FlatStyle.Flat;
            btnMarkComplete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnMarkComplete.ForeColor = Color.White;
            btnMarkComplete.Location = new Point(400, 14);
            btnMarkComplete.Margin = new Padding(3, 4, 3, 4);
            btnMarkComplete.Name = "btnMarkComplete";
            btnMarkComplete.Size = new Size(200, 50);
            btnMarkComplete.TabIndex = 2;
            btnMarkComplete.Text = "Đánh dấu hoàn thành";
            btnMarkComplete.UseVisualStyleBackColor = false;
            // 
            // btnNextLesson
            // 
            btnNextLesson.BackColor = Color.FromArgb(52, 144, 220);
            btnNextLesson.Dock = DockStyle.Right;
            btnNextLesson.FlatStyle = FlatStyle.Flat;
            btnNextLesson.Font = new Font("Segoe UI", 10F);
            btnNextLesson.ForeColor = Color.White;
            btnNextLesson.Location = new Point(775, 14);
            btnNextLesson.Margin = new Padding(3, 4, 3, 4);
            btnNextLesson.Name = "btnNextLesson";
            btnNextLesson.Size = new Size(171, 50);
            btnNextLesson.TabIndex = 1;
            btnNextLesson.Text = "Bài sau >";
            btnNextLesson.UseVisualStyleBackColor = false;
            // 
            // btnPrevLesson
            // 
            btnPrevLesson.BackColor = Color.FromArgb(108, 117, 125);
            btnPrevLesson.Dock = DockStyle.Left;
            btnPrevLesson.FlatStyle = FlatStyle.Flat;
            btnPrevLesson.Font = new Font("Segoe UI", 10F);
            btnPrevLesson.ForeColor = Color.White;
            btnPrevLesson.Location = new Point(23, 14);
            btnPrevLesson.Margin = new Padding(3, 4, 3, 4);
            btnPrevLesson.Name = "btnPrevLesson";
            btnPrevLesson.Size = new Size(171, 50);
            btnPrevLesson.TabIndex = 0;
            btnPrevLesson.Text = "< Bài trước";
            btnPrevLesson.UseVisualStyleBackColor = false;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.BorderStyle = BorderStyle.FixedSingle;
            pnlHeader.Controls.Add(btnCertificate);
            pnlHeader.Controls.Add(lblProgress);
            pnlHeader.Controls.Add(progressBar);
            pnlHeader.Controls.Add(lblCourseTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(3, 4, 3, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(23, 26, 23, 26);
            pnlHeader.Size = new Size(971, 132);
            pnlHeader.TabIndex = 0;
            pnlHeader.Paint += pnlHeader_Paint;
            // 
            // btnCertificate
            // 
            btnCertificate.BackColor = Color.FromArgb(40, 167, 69);
            btnCertificate.FlatStyle = FlatStyle.Flat;
            btnCertificate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCertificate.ForeColor = Color.White;
            btnCertificate.Location = new Point(749, 26);
            btnCertificate.Margin = new Padding(3, 4, 3, 4);
            btnCertificate.Name = "btnCertificate";
            btnCertificate.Size = new Size(184, 36);
            btnCertificate.TabIndex = 3;
            btnCertificate.Text = "🎓 Nhận chứng chỉ";
            btnCertificate.UseVisualStyleBackColor = false;
            btnCertificate.Visible = false;
            btnCertificate.Click += btnCertificate_Click;
            // 
            // lblProgress
            // 
            lblProgress.Dock = DockStyle.Bottom;
            lblProgress.Font = new Font("Segoe UI", 9F);
            lblProgress.ForeColor = Color.Gray;
            lblProgress.Location = new Point(23, 72);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(923, 18);
            lblProgress.TabIndex = 2;
            lblProgress.Text = "Tiến độ: 0%";
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Bottom;
            progressBar.Location = new Point(23, 90);
            progressBar.Margin = new Padding(3, 4, 3, 4);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(923, 14);
            progressBar.TabIndex = 1;
            // 
            // lblCourseTitle
            // 
            lblCourseTitle.Dock = DockStyle.Top;
            lblCourseTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblCourseTitle.Location = new Point(23, 26);
            lblCourseTitle.Name = "lblCourseTitle";
            lblCourseTitle.Size = new Size(923, 40);
            lblCourseTitle.TabIndex = 0;
            lblCourseTitle.Text = "Tên khóa học";
            lblCourseTitle.Click += lblCourseTitle_Click;
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(248, 249, 250);
            pnlSidebar.BorderStyle = BorderStyle.FixedSingle;
            pnlSidebar.Controls.Add(flowLessons);
            pnlSidebar.Controls.Add(lblSidebarTitle);
            pnlSidebar.Dock = DockStyle.Right;
            pnlSidebar.Location = new Point(971, 0);
            pnlSidebar.Margin = new Padding(3, 4, 3, 4);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(400, 934);
            pnlSidebar.TabIndex = 1;
            // 
            // flowLessons
            // 
            flowLessons.AutoScroll = true;
            flowLessons.Dock = DockStyle.Fill;
            flowLessons.FlowDirection = FlowDirection.TopDown;
            flowLessons.Location = new Point(0, 66);
            flowLessons.Margin = new Padding(3, 4, 3, 4);
            flowLessons.Name = "flowLessons";
            flowLessons.Padding = new Padding(11, 14, 11, 14);
            flowLessons.Size = new Size(398, 866);
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
            lblSidebarTitle.Name = "lblSidebarTitle";
            lblSidebarTitle.Padding = new Padding(17, 14, 17, 14);
            lblSidebarTitle.Size = new Size(398, 66);
            lblSidebarTitle.TabIndex = 0;
            lblSidebarTitle.Text = "Nội dung bài học";
            // 
            // LessonDetailControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(pnlMain);
            Margin = new Padding(3, 4, 3, 4);
            Name = "LessonDetailControl";
            Size = new Size(1371, 934);
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
        private System.Windows.Forms.Button btnCertificate;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Panel pnlContentArea;
        private System.Windows.Forms.Panel pnlVideo;
        private System.Windows.Forms.Label lblVideoPlaceholder;
        private System.Windows.Forms.Panel pnlTheory;
        private PdfiumViewer.PdfViewer pdfViewer;
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
