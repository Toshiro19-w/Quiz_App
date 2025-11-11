using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;
using static WinFormsApp1.Helpers.ColorPalette;

namespace WinFormsApp1.View.User
{
    public partial class formInProgressLesson : Form
    {
        private FlowLayoutPanel flowPanel;

        public formInProgressLesson()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            var lblTitle = new Label
            {
                Text = "Bài học đang học",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(20, 80),
                AutoSize = true
            };

            flowPanel = new FlowLayoutPanel
            {
                Location = new Point(20, 130),
                Size = new Size(1100, 450),
                AutoScroll = true
            };

            this.Controls.AddRange(new Control[] { lblTitle, flowPanel });
            LoadLessons();
        }

        private async void LoadLessons()
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return;

            var progresses = await context.CourseProgresses
                .Where(cp => cp.UserId == userId.Value && !cp.IsCompleted)
                .Include(cp => cp.Course)
                .ThenInclude(c => c.CourseChapters)
                .ThenInclude(ch => ch.Lessons)
                .ToListAsync();

            flowPanel.Controls.Clear();
            foreach (var progress in progresses)
            {
                foreach (var chapter in progress.Course.CourseChapters)
                {
                    foreach (var lesson in chapter.Lessons)
                    {
                        var card = CreateLessonCard(lesson, progress);
                        flowPanel.Controls.Add(card);
                    }
                }
            }
        }

        private Panel CreateLessonCard(Models.Entities.Lesson lesson, Models.Entities.CourseProgress progress)
        {
            using var context = new LearningPlatformContext();
            var card = new Panel
            {
                Size = new Size(350, 150),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            var lblTitle = new Label
            {
                Text = lesson.Title,
                Location = new Point(10, 10),
                Size = new Size(330, 40),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            var lblCourse = new Label
            {
                Text = $"Khóa học: {progress.Course.Title}",
                Location = new Point(10, 55),
                Size = new Size(330, 20),
                ForeColor = TextSecondary
            };

            var completedCount = progress.Course.CourseChapters
                .SelectMany(ch => ch.Lessons)
                .Count(l => context.CourseProgresses.Any(cp => cp.UserId == progress.UserId && cp.LessonId == l.LessonId && cp.IsCompleted));
            var totalLessons = progress.Course.CourseChapters.SelectMany(ch => ch.Lessons).Count();
            var progressPercent = totalLessons > 0 ? (completedCount * 100 / totalLessons) : 0;

            var lblProgress = new Label
            {
                Text = $"Tiến độ: {progressPercent}%",
                Location = new Point(10, 80),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ButtonPrimary
            };

            var btnContinue = new Button
            {
                Text = "Tiếp tục học",
                Location = new Point(10, 110),
                Size = new Size(330, 30),
                BackColor = ButtonSecondary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnContinue.FlatAppearance.BorderSize = 0;
            btnContinue.Click += (s, e) => MessageBox.Show($"Tiếp tục bài học: {lesson.Title}");

            card.Controls.AddRange(new Control[] { lblTitle, lblCourse, lblProgress, btnContinue });
            return card;
        }
    }
}
