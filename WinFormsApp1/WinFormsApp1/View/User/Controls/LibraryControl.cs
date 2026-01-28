using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Controllers;
using WinFormsApp1.View.User.Controls.FlashcardControls;

namespace WinFormsApp1.View.User.Controls
{
    public partial class LibraryControl : UserControl
    {
        private bool showingCourses = true;
        private bool showingCertificates = false;
        private readonly FlashcardController _flashcardController;

		public LibraryControl(bool showFlashcards = false)
		{
			InitializeComponent();
			_flashcardController = new FlashcardController();
			ApplyLocalization();
			if (showFlashcards)
			{
				ShowFlashcardsTab();
			}
			else
			{
				ShowCoursesTab();
			}
		}

		private void ApplyLocalization()
		{
			lblTitle.Text = LanguageHelper.GetString("MyLibrary");
			btnAllCourses.Text = LanguageHelper.GetString("AllCoursesTab");
			btnFlashcards.Text = "📇 Flashcards";
			btnCertificates.Text = LanguageHelper.GetString("CertificatesTab");
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
			ShowCoursesTab();
		}

		private void BtnFlashcards_Click(object sender, EventArgs e)
		{
			if (!showingCourses && !showingCertificates) return;
			ShowFlashcardsTab();
		}

		private void BtnCertificates_Click(object sender, EventArgs e)
		{
			if (showingCertificates) return;
			ShowCertificatesTab();
		}

		private void ShowCoursesTab()
		{
			showingCourses = true;
			showingCertificates = false;

			btnAllCourses.Font = new Font("Segoe UI", 12, FontStyle.Bold);
			btnAllCourses.ForeColor = Color.White;

			btnFlashcards.Font = new Font("Segoe UI", 12);
			btnFlashcards.ForeColor = Color.FromArgb(200, 200, 255);

			btnCertificates.Font = new Font("Segoe UI", 12);
			btnCertificates.ForeColor = Color.FromArgb(200, 200, 255);

			tabUnderline.Location = new Point(0, 50);

			LoadPurchasedCourses();
		}

		private void ShowFlashcardsTab()
		{
			showingCourses = false;
			showingCertificates = false;

			btnFlashcards.Font = new Font("Segoe UI", 12, FontStyle.Bold);
			btnFlashcards.ForeColor = Color.White;

			btnAllCourses.Font = new Font("Segoe UI", 12);
			btnAllCourses.ForeColor = Color.FromArgb(200, 200, 255);

			btnCertificates.Font = new Font("Segoe UI", 12);
			btnCertificates.ForeColor = Color.FromArgb(200, 200, 255);

			tabUnderline.Location = new Point(200, 50);

			LoadFlashcards();
		}

		private void ShowCertificatesTab()
		{
			showingCourses = false;
			showingCertificates = true;

			btnCertificates.Font = new Font("Segoe UI", 12, FontStyle.Bold);
			btnCertificates.ForeColor = Color.White;

			btnAllCourses.Font = new Font("Segoe UI", 12);
			btnAllCourses.ForeColor = Color.FromArgb(200, 200, 255);

			btnFlashcards.Font = new Font("Segoe UI", 12);
			btnFlashcards.ForeColor = Color.FromArgb(200, 200, 255);

			tabUnderline.Location = new Point(400, 50);

			LoadCertificates();
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

                    var hasActiveSubscription = AuthHelper.HasActiveSubscription();

                    // Nếu có subscription: CHỈ hiển thị các khóa học người dùng đã học (có progress)
                    // và luôn include các khóa đã mua.
                    if (hasActiveSubscription)
                    {
                        var studiedCourseIds = context.CourseProgresses
                            .AsNoTracking()
                            .Where(cp => cp.UserId == user.UserId)
                            .Select(cp => cp.CourseId)
                            .Distinct()
                            .ToList();

                        var studiedCourses = context.Courses
                            .Include(c => c.Owner)
                            .Where(c => studiedCourseIds.Contains(c.CourseId))
                            .ToList();

                        var purchasedCourses = context.CoursePurchases
                            .Include(cp => cp.Course)
                            .ThenInclude(c => c.Owner)
                            .Where(cp => cp.BuyerId == user.UserId && cp.Status == "Paid")
                            .Select(cp => cp.Course)
                            .ToList();

                        var merged = studiedCourses
                            .Concat(purchasedCourses)
                            .GroupBy(c => c.CourseId)
                            .Select(g => g.First())
                            .ToList();

                        if (merged.Count == 0)
                        {
                            ShowEmptyState(
                                LanguageHelper.GetString("NoCourseYet"),
                                LanguageHelper.GetString("NoCourseYetDesc")
                            );
                            return;
                        }

                        var subscriptionBanner = CreateSubscriptionBanner();
                        coursesPanel.Controls.Add(subscriptionBanner);

                        foreach (var course in merged.OrderByDescending(c => c.CreatedAt))
                        {
                            var courseCard = CreateCourseCard(course);
                            coursesPanel.Controls.Add(courseCard);
                        }

                        return;
                    }

                    // Nếu không có subscription: chỉ hiển thị khóa học đã mua
                    var purchases = context.CoursePurchases
                        .Include(cp => cp.Course)
                        .ThenInclude(c => c.Owner)
                        .Where(cp => cp.BuyerId == user.UserId && cp.Status == "Paid")
                        .Select(cp => cp.Course)
                        .ToList();

                    if (purchases.Count == 0)
                    {
                        ShowEmptyState(
                            LanguageHelper.GetString("NoCourseYet"),
                            LanguageHelper.GetString("NoCourseYetDescPurchased")
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
                MessageBox.Show(LanguageHelper.GetString("CourseLoadError", ex.Message), LanguageHelper.GetString("Error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateSubscriptionBanner()
        {
            var banner = new Panel
            {
                Size = new Size(coursesPanel.Width - 30, 80),
                BackColor = Color.FromArgb(232, 245, 233),
                Margin = new Padding(15),
                Padding = new Padding(20)
            };

            var iconLabel = new Label
            {
                Text = "⭐",
                Font = new Font("Segoe UI", 24),
                Location = new Point(10, 15),
                AutoSize = true
            };

            var titleLabel = new Label
            {
                Text = LanguageHelper.GetString("UsingPremium"),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 125, 50),
                Location = new Point(60, 10),
                AutoSize = true
            };

            var descLabel = new Label
            {
                Text = LanguageHelper.GetString("UnlimitedAccess"),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(46, 125, 50),
                Location = new Point(60, 35),
                AutoSize = true
            };

            banner.Controls.Add(iconLabel);
            banner.Controls.Add(titleLabel);
            banner.Controls.Add(descLabel);

            return banner;
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

            // Course image
            if (!string.IsNullOrEmpty(course.CoverUrl) && System.IO.File.Exists(course.CoverUrl))
            {
                var pictureBox = new PictureBox
                {
                    Location = new Point(0, 0),
                    Size = new Size(350, 180),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.White,
                    Image = Image.FromFile(course.CoverUrl)
                };

                pictureBox.Disposed += (s, e) => pictureBox.Image?.Dispose();

                pictureBox.Click += (s, e) => NavigateToCourseDetail(course.CourseId);
                card.Controls.Add(pictureBox);
            }
            else
            {
                // Fallback placeholder nếu không có ảnh
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
                imgPanel.Click += (s, e) => NavigateToCourseDetail(course.CourseId);
                card.Controls.Add(imgPanel);
            }

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
                Text = course.Summary ?? LanguageHelper.GetString("ComprehensiveCourse"),
                Location = new Point(15, 255),
                Size = new Size(320, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.LightGray
            };
            card.Controls.Add(lblDescription);

            // Continue button
            var btnContinue = new Button
            {
                Text = "🎓 " + LanguageHelper.GetString("ContinueLearning"),
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
					.Include(fs => fs.Flashcards)
					.Where(fs => fs.OwnerId == user.UserId &&
							!fs.IsDeleted &&
							(fs.Visibility == "Public" || fs.Visibility == "Private") &&
							fs.Visibility != "Course")
                    .OrderByDescending(fs => fs.CreatedAt)
                    .ToListAsync();

                if (flashcardSets.Count == 0)
                {
                    ShowEmptyState(
                        LanguageHelper.GetString("NoFlashcardsYet"),
                        LanguageHelper.GetString("NoFlashcardsYetDesc")
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
                MessageBox.Show(LanguageHelper.GetString("FlashcardLoadError", ex.Message), LanguageHelper.GetString("Error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private FlashcardSetCardControl CreateFlashcardSetCard(Models.Entities.FlashcardSet set)
		{
				var card = new FlashcardSetCardControl
				{
					DetailButtonText = " " + LanguageHelper.GetString("Open"),
					ShowStudyButton = false
				};
			card.Bind(set);
			card.ViewRequested += OpenFlashcardSet;
			return card;
		}

		private void OpenFlashcardSet(int setId)
        {
            var form = this.FindForm();
            if (form is MainContainer mainContainer)
            {
				var flashcardDetail = new FlashcardControls.FlashcardDetailControl(setId, FlashcardDetailSource.Library);
                var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
                if (mainPanel != null)
                {
                    mainPanel.Controls.Clear();
                    flashcardDetail.Dock = DockStyle.Fill;
                    mainPanel.Controls.Add(flashcardDetail);
                }
            }
        }

        private async void LoadCertificates()
        {
            coursesPanel.Controls.Clear();

            try
            {
                var user = AuthHelper.CurrentUser;
                if (user == null) return;

                using var context = new LearningPlatformContext();

                var certs = await context.Certificates
                    .AsNoTracking()
                    .Include(c => c.User)
                    .Include(c => c.Course)
                        .ThenInclude(co => co.Owner)
                    .Where(c => c.UserId == user.UserId)
                    .OrderByDescending(c => c.IssuedAt)
                    .ToListAsync();

                if (certs.Count == 0)
                {
                    ShowEmptyState(
                        LanguageHelper.GetString("NoCertificatesYet"),
                        LanguageHelper.GetString("NoCertificatesYetDesc")
                    );
                    return;
                }

                foreach (var cert in certs)
                {
                    var card = CreateCertificateCourseCard(cert);
                    coursesPanel.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageHelper.GetString("CertificateLoadError", ex.Message), LanguageHelper.GetString("Error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateCertificateCourseCard(Models.Entities.Certificate cert)
        {
            var course = cert.Course;
            var card = new Panel
            {
                Size = new Size(350, 340),
                BackColor = Color.White,
                Margin = new Padding(15),
                Cursor = Cursors.Hand
            };

            // Course image (reuse existing behavior)
            if (!string.IsNullOrEmpty(course.CoverUrl) && System.IO.File.Exists(course.CoverUrl))
            {
                var pictureBox = new PictureBox
                {
                    Location = new Point(0, 0),
                    Size = new Size(350, 180),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.FromArgb(245, 245, 245),
                    Image = Image.FromFile(course.CoverUrl)
                };

                pictureBox.Disposed += (s, e) => pictureBox.Image?.Dispose();

                pictureBox.Click += (s, e) => ViewCertificate(cert);
                card.Controls.Add(pictureBox);
            }
            else
            {
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
                        e.Graphics.DrawString("🎓", font, Brushes.Gray, new PointF(130, 50));
                    }
                };
                imgPanel.Click += (s, e) => ViewCertificate(cert);
                card.Controls.Add(imgPanel);
            }

            var lblTitle = new Label
            {
                Text = course.Title,
                Location = new Point(15, 195),
                Size = new Size(320, 30),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };
            card.Controls.Add(lblTitle);

            var lblInstructor = new Label
            {
                Text = $"👤 {course.Owner?.FullName ?? "N/A"}",
                Location = new Point(15, 230),
                Size = new Size(320, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblInstructor);

            var lblIssued = new Label
            {
                Text = $"🗓️ Cấp ngày: {cert.IssuedAt:dd/MM/yyyy}",
                Location = new Point(15, 255),
                Size = new Size(320, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblIssued);

            var btnViewCert = new Button
            {
                Text = "📄 Xem chứng chỉ",
                Location = new Point(15, 285),
                Size = new Size(320, 40),
                BackColor = Color.FromArgb(88, 56, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnViewCert.FlatAppearance.BorderSize = 0;
            btnViewCert.Click += (s, e) => ViewCertificate(cert);
            card.Controls.Add(btnViewCert);

            card.Click += (s, e) => ViewCertificate(cert);
            lblTitle.Click += (s, e) => ViewCertificate(cert);

            card.MouseEnter += (s, e) => card.BackColor = ColorPalette.Background;
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            return card;
        }

        private void ViewCertificate(Models.Entities.Certificate cert)
        {
            try
            {
                var reportData = new ViewModels.CertificateReportViewModel
                {
                    CertId = cert.CertId,
                    StudentName = cert.User?.FullName ?? AuthHelper.CurrentUser?.FullName ?? string.Empty,
                    CourseTitle = cert.Course?.Title ?? string.Empty,
                    InstructorName = cert.Course?.Owner?.FullName ?? "Giảng viên",
                    IssuedDate = cert.IssuedAt,
                    VerifyCode = cert.VerifyCode,
                    Serial = cert.Serial ?? string.Empty
                };

                var reportForm = new View.Dialogs.CertificateReportForm(reportData);
                reportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở chứng chỉ: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
