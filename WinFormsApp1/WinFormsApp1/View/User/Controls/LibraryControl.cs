using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Controllers;

namespace WinFormsApp1.View.User.Controls
{
    public partial class LibraryControl : UserControl
    {
        private bool showingCourses = true;
        private readonly FlashcardController _flashcardController;

        public LibraryControl()
        {
            InitializeComponent();
            _flashcardController = new FlashcardController();
            LoadPurchasedCourses();
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {
            // Gradient background
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                headerPanel.ClientRectangle,
                Color.FromArgb(88, 56, 255),
                Color.FromArgb(120, 90, 255),
                90F))
            {
                e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
            }
        }

        private void BtnAllCourses_Click(object sender, EventArgs e)
        {
            if (showingCourses) return;

            showingCourses = true;
            btnAllCourses.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnAllCourses.ForeColor = Color.White;
            btnFlashcards.Font = new Font("Segoe UI", 12);
            btnFlashcards.ForeColor = Color.FromArgb(200, 200, 255);
            tabUnderline.Location = new Point(0, 50);

            LoadPurchasedCourses();
        }

        private void BtnFlashcards_Click(object sender, EventArgs e)
        {
            if (!showingCourses) return;

            showingCourses = false;
            btnFlashcards.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnFlashcards.ForeColor = Color.White;
            btnAllCourses.Font = new Font("Segoe UI", 12);
            btnAllCourses.ForeColor = Color.FromArgb(200, 200, 255);
            tabUnderline.Location = new Point(200, 50);

            LoadFlashcards();
        }

        private void LoadPurchasedCourses()
        {
            coursesPanel.Controls.Clear();

            try
            {
                using (var context = new LearningPlatformContext())
                {
                    var user = AuthHelper.CurrentUser;
                    if (user == null) return;

                    // Get purchased courses
                    var purchases = context.CoursePurchases
                        .Include(cp => cp.Course)
                        .ThenInclude(c => c.Owner)
                        .Where(cp => cp.BuyerId == user.UserId && cp.Status == "Paid")
                        .Select(cp => cp.Course)
                        .ToList();

                    if (purchases.Count == 0)
                    {
                        ShowEmptyState(
                            "Chưa có khóa học nào",
                            "Bạn chưa mua khóa học nào. Hãy bắt đầu học ngay hôm nay!"
                        );
                        return;
                    }

                    foreach (var course in purchases)
                    {
                        var courseCard = CreateCourseCard(course);
                        coursesPanel.Controls.Add(courseCard);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải khóa học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateCourseCard(Models.Entities.Course course)
        {
            var card = new Panel
            {
                Size = new Size(350, 320),
                BackColor = Color.White,
                Margin = new Padding(15),
                Cursor = Cursors.Hand
            };

            // Course image placeholder
            var imgPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(350, 180),
                BackColor = Color.FromArgb(245, 245, 245)
            };

            imgPanel.Paint += (s, e) =>
            {
                using (var font = new Font("Segoe UI", 48))
                {
                    e.Graphics.DrawString("📚", font, Brushes.Gray, new PointF(130, 50));
                }
            };
            card.Controls.Add(imgPanel);

            // Course title
            var lblTitle = new Label
            {
                Text = course.Title,
                Location = new Point(15, 195),
                Size = new Size(320, 30),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            card.Controls.Add(lblTitle);

            // Instructor
            var lblInstructor = new Label
            {
                Text = $"👤 {course.Owner?.FullName ?? "N/A"}",
                Location = new Point(15, 230),
                Size = new Size(320, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblInstructor);

            // Course description
            var lblDescription = new Label
            {
                Text = course.Summary ?? "Khóa học toàn diện",
                Location = new Point(15, 255),
                Size = new Size(320, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.LightGray
            };
            card.Controls.Add(lblDescription);

            // Continue button
            var btnContinue = new Button
            {
                Text = "🎓 Tiếp tục học",
                Location = new Point(15, 280),
                Size = new Size(320, 35),
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnContinue.FlatAppearance.BorderSize = 0;
            btnContinue.Click += (s, e) => NavigateToCourseDetail(course.CourseId);
            card.Controls.Add(btnContinue);

            // Click on card to navigate
            card.Click += (s, e) => NavigateToCourseDetail(course.CourseId);
            imgPanel.Click += (s, e) => NavigateToCourseDetail(course.CourseId);
            lblTitle.Click += (s, e) => NavigateToCourseDetail(course.CourseId);

            // Hover effect
            card.MouseEnter += (s, e) => card.BackColor = ColorPalette.Background;
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            return card;
        }

        private void NavigateToCourseDetail(int courseId)
        {
            var form = this.FindForm();
            if (form is MainContainer mainContainer)
            {
                var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
                if (mainPanel != null)
                {
                    mainPanel.Controls.Clear();
                    var courseDetail = new CourseControls.CourseDetailControl();
                    courseDetail.Dock = DockStyle.Fill;
                    mainPanel.Controls.Add(courseDetail);
                    _ = courseDetail.LoadCourseAsync(courseId);
                }
            }
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

        private async void LoadFlashcards()
        {
            coursesPanel.Controls.Clear();

            try
            {
                var user = AuthHelper.CurrentUser;
                if (user == null) return;

                using var context = new LearningPlatformContext();
                
                // Lấy flashcard sets mà user đã tạo (chỉ Public và Private, KHÔNG lấy Course)
                var flashcardSets = await context.FlashcardSets
                    .Where(fs => fs.OwnerId == user.UserId && 
                                (fs.Visibility == "Public" || fs.Visibility == "Private") &&
                                fs.Visibility != "Course")
                    .OrderByDescending(fs => fs.CreatedAt)
                    .ToListAsync();

                if (flashcardSets.Count == 0)
                {
                    ShowEmptyState(
                        "Chưa có flashcard nào",
                        "Bạn chưa tạo flashcard nào. Hãy bắt đầu tạo ngay hôm nay!"
                    );
                    return;
                }

                foreach (var set in flashcardSets)
                {
                    var card = CreateFlashcardSetCard(set);
                    coursesPanel.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải flashcards: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateFlashcardSetCard(Models.Entities.FlashcardSet set)
        {
            var card = new Panel
            {
                Size = new Size(350, 180),
                BackColor = Color.White,
                Margin = new Padding(15),
                Cursor = Cursors.Hand
            };

            var lblTitle = new Label
            {
                Text = set.Title,
                Location = new Point(15, 15),
                Size = new Size(320, 30),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            card.Controls.Add(lblTitle);

            var lblDescription = new Label
            {
                Text = set.Description ?? "Không có mô tả",
                Location = new Point(15, 50),
                Size = new Size(320, 40),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblDescription);

            var lblCount = new Label
            {
                Text = $"📝 {set.Flashcards?.Count ?? 0} thẻ",
                Location = new Point(15, 100),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblCount);

            var btnOpen = new Button
            {
                Text = "📚 Mở",
                Location = new Point(200, 130),
                Size = new Size(135, 35),
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnOpen.FlatAppearance.BorderSize = 0;
            btnOpen.Click += (s, e) => OpenFlashcardSet(set.SetId);
            card.Controls.Add(btnOpen);

            card.MouseEnter += (s, e) => card.BackColor = ColorPalette.Background;
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            return card;
        }

        private void OpenFlashcardSet(int setId)
        {
            var form = this.FindForm();
            if (form is MainContainer mainContainer)
            {
                var flashcardDetail = new FlashcardControls.FlashcardDetailControl(setId);
                var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
                if (mainPanel != null)
                {
                    mainPanel.Controls.Clear();
                    flashcardDetail.Dock = DockStyle.Fill;
                    mainPanel.Controls.Add(flashcardDetail);
                }
            }
        }

        private void ShowEmptyState(string title, string message)
        {
            var emptyPanel = new Panel
            {
                Size = new Size(800, 400),
                BackColor = Color.Transparent
            };

            // Title (Bold, Large)
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(200, 120),
                AutoSize = true
            };
            emptyPanel.Controls.Add(lblTitle);

            // Subtitle message
            var lblMessage = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Location = new Point(120, 180),
                Size = new Size(560, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            emptyPanel.Controls.Add(lblMessage);

            coursesPanel.Controls.Add(emptyPanel);
        }

        private void LibraryControl_Resize(object sender, EventArgs e)
        {
            if (coursesPanel != null)
            {
                coursesPanel.Size = new Size(this.Width - 200, this.Height - 200);
            }
        }
    }
}
