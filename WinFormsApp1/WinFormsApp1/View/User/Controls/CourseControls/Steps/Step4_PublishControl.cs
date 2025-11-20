using System;
using System.Windows.Forms;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    public partial class Step4_PublishControl : UserControl, IStepControl
    {
        public Step4_PublishControl()
        {
            InitializeComponent();
            btnSaveDraft.Click += (s, e) => OnSaveDraftRequested?.Invoke(this, EventArgs.Empty);
            btnPublish.Click += (s, e) => OnPublishRequested?.Invoke(this, EventArgs.Empty);
            btnPrev.Click += (s, e) => OnPrevRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler? OnSaveDraftRequested;
        public event EventHandler? OnPublishRequested;
        public event EventHandler? OnPrevRequested;

        public void LoadFromViewModel(CourseBuilderViewModel vm)
        {
            lblTitleValue.Text = vm?.Title ?? "";
            lblSlugValue.Text = vm?.Slug ?? "";
            lblPriceValue.Text = $"{vm?.Price:N0} VNĐ";
            lblStatusValue.Text = vm?.IsPublished == true ? "Đã xuất bản" : "Bản nháp";

            int totalLessons = 0;
            int totalContents = 0;
            if (vm?.Chapters != null)
            {
                foreach (var ch in vm.Chapters)
                {
                    if (ch.Lessons != null)
                    {
                        totalLessons += ch.Lessons.Count;
                        foreach (var lesson in ch.Lessons)
                        {
                            totalContents += lesson.Contents?.Count ?? 0;
                        }
                    }
                }
            }

            lblChaptersValue.Text = (vm?.Chapters?.Count ?? 0).ToString();
            lblLessonsValue.Text = totalLessons.ToString();
            lblContentsValue.Text = totalContents.ToString();

            pnlCourseStructure.Controls.Clear();
            int y = 0;
            if (vm?.Chapters != null)
            {
                foreach (var chapter in vm.Chapters)
                {
                    var lblChapter = new Label { Text = $"📚 {chapter.Title}", Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.Purple, Location = new System.Drawing.Point(0, y), AutoSize = true };
                    pnlCourseStructure.Controls.Add(lblChapter);
                    y += 35;

                    if (chapter.Lessons != null)
                    {
                        foreach (var lesson in chapter.Lessons)
                        {
                            var lblLesson = new Label { Text = $"    📖 {lesson.Title} ({lesson.Contents?.Count ?? 0} nội dung)", Location = new System.Drawing.Point(20, y), AutoSize = true };
                            pnlCourseStructure.Controls.Add(lblLesson);
                            y += 30;
                        }
                    }
                }
            }
        }

        public void SaveToViewModel(CourseBuilderViewModel vm) { }

        public void OnEnter() { }
        public void OnLeaving() { }
    }
}