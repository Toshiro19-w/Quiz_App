using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.View.User.Controls.CourseControls;
using WinFormsApp1.View.User.Controls.FlashcardControls;
using System.IO;

namespace WinFormsApp1.View.User.Controls
{
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();
        }

        private void HomeControl_Load(object sender, EventArgs e)
        {
            SetupWelcomeBanner();
            LoadMotivationImage();
            LoadData();
            LoadFlashcardSets();
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            // Navigate to CourseControl
            var form = this.FindForm();
            if (form == null) return;

            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

            if (mainPanel == null)
            {
                mainPanel = this.Parent as Panel;
            }

            if (mainPanel == null) return;

            mainPanel.Controls.Clear();

            var courseControl = new CourseControl();
            courseControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(courseControl);
        }

        private void btnViewAllFlashcards_Click(object sender, EventArgs e)
        {
            // Navigate to FlashcardControl
            var form = this.FindForm();
            if (form == null) return;

            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

            if (mainPanel == null)
            {
                mainPanel = this.Parent as Panel;
            }

            if (mainPanel == null) return;

            mainPanel.Controls.Clear();

            var flashcardControl = new FlashcardControl();
            flashcardControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(flashcardControl);
        }

        private void SetupWelcomeBanner()
        {
            // Set user name with dynamic text
            var userName = AuthHelper.CurrentUser?.FullName ?? "Trần Minh Khoa";

            // Update welcome text
            lblWelcomeText.Text = $"Chào mừng {userName} trở lại!";
            lblWelcomeText.ForeColor = Color.FromArgb(218, 165, 32); // Gold color

            // Load and set circular avatar image
            LoadAvatarImage();
            MakeCircularPictureBox();
        }

        private void LoadMotivationImage()
        {
            // Try to load the motivation image from Library/Image folder
            string imagePath = Path.Combine(Application.StartupPath, "Library", "Image", "image1.jpg");
            
            if (File.Exists(imagePath))
            {
                try
                {
                    pictureBoxMotivation.Image = Image.FromFile(imagePath);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading motivation image: {ex.Message}");
                }
            }
        }

        private void LoadAvatarImage()
        {
            // Try to load from Library/Image folder
            string[] possiblePaths = new[]
            {
                Path.Combine(Application.StartupPath, "Library", "Image", "avatar.png"),
                Path.Combine(Application.StartupPath, "Library", "Image", "avatar.jpg"),
                Path.Combine(Application.StartupPath, "Library", "Image", "user.png"),
                Path.Combine(Application.StartupPath, "Library", "Image", "user.jpg")
            };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    try
                    {
                        pictureBoxAvatar.Image = Image.FromFile(path);
                        return;
                    }
                    catch { }
                }
            }

            // If no image found, create a default avatar with initials
            CreateDefaultAvatar();
        }

        private void CreateDefaultAvatar()
        {
            var bitmap = new Bitmap(80, 80);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                
                // Draw background circle with a nice color
                using (var brush = new SolidBrush(Color.FromArgb(124, 77, 255)))
                {
                    g.FillEllipse(brush, 0, 0, 80, 80);
                }

                // Draw user icon instead of initials when no name available
                var userName = AuthHelper.CurrentUser?.FullName;
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    var initials = GetInitials(userName);
                    using (var font = new Font("Segoe UI", 24, FontStyle.Bold))
                    using (var textBrush = new SolidBrush(Color.White))
                    {
                        var format = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        g.DrawString(initials, font, textBrush, new RectangleF(0, 0, 80, 80), format);
                    }
                }
                else
                {
                    // Draw user icon emoji when no user name
                    using (var font = new Font("Segoe UI Emoji", 36, FontStyle.Regular))
                    using (var textBrush = new SolidBrush(Color.White))
                    {
                        var format = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        g.DrawString("👤", font, textBrush, new RectangleF(0, 0, 80, 80), format);
                    }
                }
            }

            pictureBoxAvatar.Image = bitmap;
        }

        private string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "?";

            var parts = name.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
                return parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpper();
            
            return (parts[0][0].ToString() + parts[parts.Length - 1][0].ToString()).ToUpper();
        }

        private void MakeCircularPictureBox()
        {
            // Create circular clip region
            var path = new GraphicsPath();
            path.AddEllipse(0, 0, pictureBoxAvatar.Width, pictureBoxAvatar.Height);
            pictureBoxAvatar.Region = new Region(path);
            
            // Add border
            pictureBoxAvatar.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.White, 3))
                {
                    e.Graphics.DrawEllipse(pen, 1, 1, pictureBoxAvatar.Width - 3, pictureBoxAvatar.Height - 3);
                }
            };
        }

        private async void LoadData()
        {
            using var context = new LearningPlatformContext();

            // Load 4 courses with best ratings (highest AverageRating)
            var popular = await context.Courses
                .Where(c => c.IsPublished)
                .OrderByDescending(c => c.AverageRating)
                .ThenByDescending(c => c.TotalReviews)
                .Take(4)
                .ToListAsync();

            foreach (var course in popular)
            {
                flowPopular.Controls.Add(CreateCourseCard(course));
            }
        }

        private async void LoadFlashcardSets()
        {
            try
            {
                var flashcardController = new WinFormsApp1.Controllers.FlashcardController();
                
                // Load 4 flashcard sets with most cards (highest Flashcards count)
                var flashcardSets = await flashcardController.GetPopularFlashcardSetsAsync(4);

                foreach (var flashcardSet in flashcardSets)
                {
                    flowFlashcards.Controls.Add(CreateFlashcardCard(flashcardSet));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading flashcard sets: {ex.Message}");
            }
        }

        private Panel CreateFlashcardCard(Models.Entities.FlashcardSet flashcardSet)
        {
            var card = new Panel
            {
                Size = new Size(280, 200),
                BackColor = Color.FromArgb(124, 77, 255), // Purple gradient color
                BorderStyle = BorderStyle.None,
                Margin = new Padding(10),
                Cursor = Cursors.Hand
            };

            // Add rounded corners effect with paint event
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(new Rectangle(0, 0, card.Width, card.Height), 10))
                {
                    card.Region = new Region(path);
                }
            };

            // Icon placeholder
            var lblIcon = new Label
            {
                Text = "📇",
                Font = new Font("Segoe UI", 36),
                ForeColor = Color.White,
                Location = new Point(110, 20),
                Size = new Size(60, 60),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            card.Controls.Add(lblIcon);

            // Card count badge
            var lblCount = new Label
            {
                Text = $"⊕ {flashcardSet.Flashcards.Count} thẻ",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(80, 0, 0, 0),
                Location = new Point(10, 20),
                Size = new Size(80, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblCount);

            // Title
            var lblTitle = new Label
            {
                Text = flashcardSet.Title,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 100),
                Size = new Size(260, 50),
                BackColor = Color.Transparent
            };
            card.Controls.Add(lblTitle);

            // Author
            var lblAuthor = new Label
            {
                Text = $"👤 {flashcardSet.Owner?.FullName ?? "Unknown"}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.White,
                Location = new Point(10, 155),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            card.Controls.Add(lblAuthor);

            // Language tag
            if (!string.IsNullOrEmpty(flashcardSet.Language))
            {
                var lblLanguage = new Label
                {
                    Text = $"🌐 {flashcardSet.Language}",
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = Color.White,
                    Location = new Point(160, 160),
                    AutoSize = true,
                    BackColor = Color.Transparent
                };
                card.Controls.Add(lblLanguage);
            }

            // Click event
            card.Click += (s, e) => ShowFlashcardDetail(flashcardSet.SetId);
            foreach (Control ctrl in card.Controls)
            {
                ctrl.Click += (s, e) => ShowFlashcardDetail(flashcardSet.SetId);
            }

            // Hover effects
            card.MouseEnter += (s, e) =>
            {
                card.BackColor = Color.FromArgb(140, 95, 255);
            };
            card.MouseLeave += (s, e) =>
            {
                card.BackColor = Color.FromArgb(124, 77, 255);
            };

            return card;
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;
            var arc = new Rectangle(rect.Location, new Size(diameter, diameter));

            // Top left
            path.AddArc(arc, 180, 90);
            // Top right
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);
            // Bottom right
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            // Bottom left
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void ShowFlashcardDetail(int setId)
        {
            var form = this.FindForm();
            if (form == null) return;

            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

            if (mainPanel == null)
            {
                mainPanel = this.Parent as Panel;
            }

            if (mainPanel == null) return;

            mainPanel.Controls.Clear();

            var detail = new FlashcardDetailControl(setId);
            detail.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(detail);
        }

        private Panel CreateCourseCard(Models.Entities.Course course)
        {
            // Use larger card size for all courses
            var card = new Panel
            {
                Size = new Size(340, 200),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Cursor = Cursors.Hand
            };
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(245, 245, 245);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            var lblTitle = new Label
            {
                Text = course.Title,
                Location = new Point(10, 10),
                Size = new Size(320, 50),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };

            var lblReviews = new Label
            {
                Text = $"⭐ {course.TotalReviews} đánh giá",
                Location = new Point(10, 70),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray
            };

            var lblPrice = new Label
            {
                Text = course.Price > 0 ? $"{course.Price:N0} VNĐ" : "Miễn phí",
                Location = new Point(10, 95),
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.Green
            };

            var btnView = new Button
            {
                Text = "Xem khóa học",
                Location = new Point(105, 155),
                Size = new Size(130, 35),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = course.CourseId,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += (s, e) => ShowCourseDetail((int)btnView.Tag);

            card.Controls.AddRange(new Control[] { lblTitle, lblReviews, lblPrice, btnView });
            return card;
        }

        private void ShowCourseDetail(int courseId)
        {
            var form = this.FindForm();
            if (form == null) return;

            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

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
