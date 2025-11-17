using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.View.User.Controls
{
    public partial class LibraryControl : UserControl
    {
        private bool showingCourses = true;

        public LibraryControl()
        {
            InitializeComponent();
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
                Size = new Size(350, 280),
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

            // Draw cabinet icon
            imgPanel.Paint += (s, e) =>
            {
                using (var font = new Font("Segoe UI", 48))
                {
                    e.Graphics.DrawString("🗄️", font, Brushes.Gray, new PointF(130, 50));
                }
            };
            card.Controls.Add(imgPanel);

            // Course title
            var lblTitle = new Label
            {
                Text = course.Title,
                Location = new Point(15, 195),
                Size = new Size(320, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            card.Controls.Add(lblTitle);

            // Course description
            var lblDescription = new Label
            {
                Text = course.Summary ?? "Khóa học toàn diện về lĩnh vực này",
                Location = new Point(15, 225),
                Size = new Size(320, 40),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblDescription);

            // Instructor
            var lblInstructor = new Label
            {
                Text = $"👤 {course.Owner?.FullName ?? "N/A"}",
                Location = new Point(15, 250),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblInstructor);

            // Continue button
            var btnContinue = new Button
            {
                Text = "🎓 Tiếp tục học",
                Location = new Point(200, 245),
                Size = new Size(135, 30),
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnContinue.FlatAppearance.BorderSize = 0;
            btnContinue.Click += (s, e) =>
            {
                MessageBox.Show($"Mở khóa học: {course.Title}", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            card.Controls.Add(btnContinue);

            // Hover effect
            card.MouseEnter += (s, e) => card.BackColor = ColorPalette.Background;
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            return card;
        }

        private void LoadFlashcards()
        {
            coursesPanel.Controls.Clear();

            try
            {
                using (var context = new LearningPlatformContext())
                {
                    var user = AuthHelper.CurrentUser;
                    if (user == null) return;

                    var flashcardSets = context.SavedItems
                        .Include(si => si.Library)
                        .Where(si => si.Library.OwnerId == user.UserId && si.ContentType == "FlashcardSet")
                        .ToList();

                    if (flashcardSets.Count == 0)
                    {
                        ShowEmptyState(
                            "Chưa có flashcard nào",
                            "Bạn chưa lưu flashcard nào. Hãy bắt đầu học ngay hôm nay!"
                        );
                        return;
                    }

                    foreach (var item in flashcardSets)
                    {
                        var card = CreateFlashcardCard(item);
                        coursesPanel.Controls.Add(card);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải flashcards: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateFlashcardCard(Models.Entities.SavedItem item)
        {
            var card = new Panel
            {
                Size = new Size(350, 150),
                BackColor = Color.White,
                Margin = new Padding(15),
                Cursor = Cursors.Hand
            };

            var lblTitle = new Label
            {
                Text = $"Flashcard Set #{item.ContentId}",
                Location = new Point(15, 15),
                Size = new Size(320, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            card.Controls.Add(lblTitle);

            var lblNote = new Label
            {
                Text = item.Note ?? "Không có ghi chú",
                Location = new Point(15, 50),
                Size = new Size(320, 40),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblNote);

            var btnOpen = new Button
            {
                Text = "📚 Mở",
                Location = new Point(200, 100),
                Size = new Size(135, 35),
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOpen.FlatAppearance.BorderSize = 0;
            card.Controls.Add(btnOpen);

            return card;
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
