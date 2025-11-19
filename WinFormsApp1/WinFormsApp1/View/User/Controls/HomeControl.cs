using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Controls
{
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();
            HomeControl_Load(null, null);
        }

        private async void HomeControl_Load(object sender, EventArgs e)
        {
            await LoadPopularCoursesAsync();
            await LoadFlashcardSetsAsync();
        }

        private async Task LoadPopularCoursesAsync()
        {
            try
            {
                using var context = new LearningPlatformContext();
                
                var popularCourses = await context.Courses
                    .Include(c => c.Owner)
                    .Include(c => c.Category)
                    .Where(c => c.IsPublished)
                    .OrderByDescending(c => c.TotalReviews)
                    .ThenByDescending(c => c.AverageRating)
                    .Take(4)
                    .ToListAsync();

                flowPopular.Controls.Clear();

                foreach (var course in popularCourses)
                {
                    var card = CreateCourseCard(course);
                    flowPopular.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải khóa học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadFlashcardSetsAsync()
        {
            try
            {
                using var context = new LearningPlatformContext();
                
                var flashcardSets = await context.FlashcardSets
                    .Include(fs => fs.Owner)
                    .Where(fs => fs.Visibility == "Public" && !fs.IsDeleted)
                    .OrderByDescending(fs => fs.CreatedAt)
                    .Take(4)
                    .ToListAsync();

                flowFlashcards.Controls.Clear();

                foreach (var flashcardSet in flashcardSets)
                {
                    var card = CreateFlashcardCard(flashcardSet);
                    flowFlashcards.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải flashcard: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateCourseCard(Course course)
        {
            var card = new Panel
            {
                Size = new Size(400, 280),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Cursor = Cursors.Hand
            };

            // Course image placeholder
            var picBox = new PictureBox
            {
                Size = new Size(400, 150),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(240, 240, 240),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            card.Controls.Add(picBox);

            // Title
            var lblTitle = new Label
            {
                Text = course.Title,
                Location = new Point(10, 160),
                Size = new Size(380, 40),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            card.Controls.Add(lblTitle);

            // Author
            var lblAuthor = new Label
            {
                Text = course.Owner?.FullName ?? "Giảng viên",
                Location = new Point(10, 205),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextSecondary
            };
            card.Controls.Add(lblAuthor);

            // Rating
            var lblRating = new Label
            {
                Text = $"⭐ {course.AverageRating:F1} ({course.TotalReviews} đánh giá)",
                Location = new Point(10, 230),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.Warning
            };
            card.Controls.Add(lblRating);

            // Price
            var lblPrice = new Label
            {
                Text = course.Price > 0 ? $"{course.Price:N0}đ" : "Miễn phí",
                Location = new Point(10, 255),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ColorPalette.Primary
            };
            card.Controls.Add(lblPrice);

            // Click event
            card.Click += (s, e) => NavigateToCourseDetail(course.CourseId);

            return card;
        }

        private Panel CreateFlashcardCard(FlashcardSet flashcardSet)
        {
            var card = new Panel
            {
                Size = new Size(400, 180),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Cursor = Cursors.Hand
            };

            // Icon
            var lblIcon = new Label
            {
                Text = "🗂️",
                Location = new Point(10, 10),
                Size = new Size(50, 50),
                Font = new Font("Segoe UI", 32),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblIcon);

            // Title
            var lblTitle = new Label
            {
                Text = flashcardSet.Title,
                Location = new Point(70, 10),
                Size = new Size(320, 50),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            card.Controls.Add(lblTitle);

            // Description
            var lblDesc = new Label
            {
                Text = flashcardSet.Description ?? "Không có mô tả",
                Location = new Point(10, 70),
                Size = new Size(380, 60),
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextSecondary
            };
            card.Controls.Add(lblDesc);

            // Card count
            var lblCount = new Label
            {
                Text = $"Số thẻ: Đang tải...",
                Location = new Point(10, 140),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextSecondary
            };
            card.Controls.Add(lblCount);

            // Button
            var btnStudy = new Button
            {
                Text = "Học ngay",
                Location = new Point(290, 135),
                Size = new Size(100, 35),
                BackColor = ColorPalette.Primary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStudy.FlatAppearance.BorderSize = 0;
            btnStudy.Click += (s, e) => MessageBox.Show($"Học flashcard: {flashcardSet.Title}");
            card.Controls.Add(btnStudy);

            return card;
        }

        private void NavigateToCourseDetail(int courseId)
        {
            try
            {
                var mainContainer = this.FindForm() as MainContainer;
                if (mainContainer != null)
                {
                    mainContainer.NavigateToCourseDetail(courseId);
                }
                else
                {
                    MessageBox.Show("Không thể điều hướng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            var mainContainer = this.FindForm() as MainContainer;
            if (mainContainer != null)
            {
                // Navigate to course list
                NavigationHelper.NavigateTo("course");
            }
        }

        private void btnViewAllFlashcards_Click(object sender, EventArgs e)
        {
            var mainContainer = this.FindForm() as MainContainer;
            if (mainContainer != null)
            {
                // Navigate to flashcard list
                NavigationHelper.NavigateTo("flashcard");
            }
        }
    }
}
