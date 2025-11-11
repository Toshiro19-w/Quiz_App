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
    public partial class formLesson : Form
    {
        private FlowLayoutPanel flowPanel;

        public formLesson()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Bài học";
            this.BackColor = Background;
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            var lblTitle = new Label
            {
                Text = "Danh sách bài học",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            flowPanel = new FlowLayoutPanel
            {
                Location = new Point(20, 70),
                Size = new Size(1100, 500),
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

            var lessons = await context.CourseProgresses
                .Where(cp => cp.UserId == userId.Value)
                .Include(cp => cp.Course)
                .ThenInclude(c => c.CourseChapters)
                .ThenInclude(ch => ch.Lessons)
                .SelectMany(cp => cp.Course.CourseChapters.SelectMany(ch => ch.Lessons))
                .Take(20)
                .ToListAsync();

            flowPanel.Controls.Clear();
            foreach (var lesson in lessons)
            {
                var card = CreateLessonCard(lesson);
                flowPanel.Controls.Add(card);
            }
        }

        private Panel CreateLessonCard(Models.Entities.Lesson lesson)
        {
            var card = new Panel
            {
                Size = new Size(350, 120),
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

            var lblDesc = new Label
            {
                Text = lesson.Description ?? "Không có mô tả",
                Location = new Point(10, 55),
                Size = new Size(330, 20),
                ForeColor = TextSecondary
            };

            var btnView = new Button
            {
                Text = "Xem bài học",
                Location = new Point(10, 80),
                Size = new Size(330, 30),
                BackColor = ButtonSecondary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = lesson.LessonId
            };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += (s, e) => MessageBox.Show($"Mở bài học ID: {lesson.LessonId}");

            card.Controls.AddRange(new Control[] { lblTitle, lblDesc, btnView });
            return card;
        }
    }
}
