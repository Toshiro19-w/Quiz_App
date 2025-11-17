using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.View.User.Controls.CourseControls;

namespace WinFormsApp1.View.User.Controls
{
    public partial class HomeControl : UserControl
    {
        private FlowLayoutPanel flowRecent;
        private FlowLayoutPanel flowPopular;

        public HomeControl()
        {
            InitializeComponent();
            SetupUI();
            LoadData();
        }

        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ColorPalette.Background;
            this.Padding = new Padding(0, 70, 0, 0);

            var lblWelcome = new Label
            {
                Text = $"Xin chào, {AuthHelper.CurrentUser?.FullName ?? "User"}!",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(20, 10),
                AutoSize = true,
                ForeColor = ColorPalette.Primary
            };

            var lblRecent = new Label
            {
                Text = "📚 Khóa học gần đây",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 60),
                AutoSize = true
            };

            flowRecent = new FlowLayoutPanel
            {
                Location = new Point(20, 100),
                Size = new Size(1150, 200),
                AutoScroll = true
            };

            var lblPopular = new Label
            {
                Text = "🔥 Khóa học phổ biến",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 320),
                AutoSize = true
            };

            flowPopular = new FlowLayoutPanel
            {
                Location = new Point(20, 360),
                Size = new Size(1150, 200),
                AutoScroll = true
            };

            this.Controls.AddRange(new Control[] { lblWelcome, lblRecent, flowRecent, lblPopular, flowPopular });
        }

        private async void LoadData()
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;

            if (userId.HasValue)
            {
                var recent = await context.CourseProgresses
                    .Where(cp => cp.UserId == userId.Value)
                    .OrderByDescending(cp => cp.LastViewedAt)
                    .Take(8)
                    .Include(cp => cp.Course)
                    .Select(cp => cp.Course)
                    .ToListAsync();

                foreach (var course in recent)
                {
                    flowRecent.Controls.Add(CreateCourseCard(course));
                }
            }

            var popular = await context.Courses
                .OrderByDescending(c => c.TotalReviews)
                .Take(8)
                .ToListAsync();

            foreach (var course in popular)
            {
                flowPopular.Controls.Add(CreateCourseCard(course));
            }
        }

        private Panel CreateCourseCard(Models.Entities.Course course)
        {
            var card = new Panel
            {
                Size = new Size(270, 160),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Cursor = Cursors.Hand
            };
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(245, 245, 245);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            var lblTitle = new Label
            {
                Text = course.Title,
                Location = new Point(10, 10),
                Size = new Size(250, 50),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            var lblReviews = new Label
            {
                Text = $"⭐ {course.TotalReviews} đánh giá",
                Location = new Point(10, 65),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextSecondary
            };

            var lblPrice = new Label
            {
                Text = course.Price > 0 ? $"{course.Price:N0} VNĐ" : "Miễn phí",
                Location = new Point(10, 90),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.ButtonPrimary
            };

            var btnView = new Button
            {
                Text = "Xem khóa học",
                Location = new Point(70, 120),
                Size = new Size(130, 30),
                BackColor = ColorPalette.ButtonSecondary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = course.CourseId
            };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += (s, e) => ShowCourseDetail((int)btnView.Tag);

            card.Controls.AddRange(new Control[] { lblTitle, lblReviews, lblPrice, btnView });
            return card;
        }

        private void ShowCourseDetail(int courseId)
        {
            // Try to find the main content panel by name on the parent form
            var form = this.FindForm();
            if (form == null) return;

            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

            // Fallback: if not found, use this.Parent (likely the panel hosting this control)
            if (mainPanel == null)
            {
                mainPanel = this.Parent as Panel;
            }

            if (mainPanel == null) return;

            mainPanel.Controls.Clear();

            var detail = new CourseDetailControl(courseId);
            detail.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(detail);
        }

        private Control FindControlRecursive(Control parent, string name)
        {
            foreach (Control c in parent.Controls)
            {
                if (string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase)) return c;
                var found = FindControlRecursive(c, name);
                if (found != null) return found;
            }
            return null;
        }
    }
}
