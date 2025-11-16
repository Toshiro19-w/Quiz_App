using WinFormsApp1.Models.EF;           // Đúng namespace
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Service;

namespace WinFormsApp1.View.User.Controls
{
	public partial class CourseDetailControl : UserControl
	{
		private readonly CourseService _courseService;
		private Course _course;
		private int _currentUserId = 1; // Giả lập user

		public CourseDetailControl()
		{
			InitializeComponent();
			_courseService = new CourseService(new LearningPlatformContext(), null);
			this.Load += CourseDetailControl_Load;
		}



		private void CourseDetailControl_Load(object sender, EventArgs e)
		{
			LoadCourse("sql-server-tu-co-ban-den-nang-cao");
		}

		public void LoadCourse(string slug)
		{
			_course = _courseService.GetCourseBySlugWithFullDetails(slug);
			if (_course == null)
			{
				MessageBox.Show("Không tìm thấy khóa học.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			PopulateUI();
		}

		private void PopulateUI()
		{
			lblBreadcrumb.Text = $"Khóa học / {_course.Category?.Name ?? "Chưa có danh mục"} / {_course.Title}";
			lblTitle.Text = _course.Title;
			pnlBestseller.Visible = _course.AverageRating >= 4.5m;

			lblRating.Text = $"{_course.AverageRating:F1}";
			lblRatingCount.Text = $"({_course.TotalReviews} đánh giá)";
			lblStudents.Text = $"{_course.CoursePurchases?.Count ?? 0} học viên";

			lblInstructor.Text = $"Được tạo bởi {_course.Owner.FullName}";
			lblLastUpdated.Text = $"Cập nhật lần cuối {(_course.UpdatedAt ?? _course.CreatedAt):MM/yyyy}";

			if (!string.IsNullOrEmpty(_course.CoverUrl))
			{
				try { picCover.Load(_course.CoverUrl); }
				catch { picCover.Image = GetPlaceholderImage(); }
			}
			else
			{
				picCover.Image = GetPlaceholderImage();
			}

			lblPrice.Text = _course.Price > 0 ? $"₫{_course.Price:N0}" : "MIỄN PHÍ";

			UpdateActionButtons();
			LoadCourseContent();
			rtbDescription.Text = _course.Summary ?? "Khóa học toàn diện về SQL Server...";

			lblInstructorName.Text = _course.Owner.FullName;
			lblInstructorEmail.Text = _course.Owner.Email ?? "teacher@learn.vn";

			LoadRatingSummary();
			LoadReviews();
		}

		private void LoadCourseContent()
		{
			pnlChapters.Controls.Clear();
			int y = 0;
			int totalLessons = 0;

			foreach (var chapter in _course.CourseChapters?.OrderBy(c => c.OrderIndex) ?? Enumerable.Empty<CourseChapter>())
			{
				var panel = CreateChapterPanel(chapter, ref y, ref totalLessons);
				pnlChapters.Controls.Add(panel);
			}

			lblChapterStats.Text = $"{_course.CourseChapters?.Count ?? 0} chương • {totalLessons} bài học";
		}

		private Panel CreateChapterPanel(CourseChapter chapter, ref int y, ref int totalLessons)
		{
			var panel = new Panel
			{
				Height = 50,
				Width = pnlChapters.Width - 25,
				Location = new Point(0, y),
				BackColor = Color.White,
				Tag = chapter
			};

			var lblTitle = new Label
			{
				Text = chapter.Title,
				Location = new Point(15, 15),
				AutoSize = true,
				Font = new Font("Segoe UI", 10F, FontStyle.Bold),
				ForeColor = Color.FromArgb(0, 102, 102)
			};

			totalLessons += chapter.Lessons?.Count ?? 0;
			var lblCount = new Label
			{
				Text = $"{chapter.Lessons?.Count ?? 0} bài học",
				Location = new Point(panel.Width - 180, 15),
				AutoSize = true,
				ForeColor = Color.Gray
			};

			var btnToggle = new Button
			{
				Text = "Mở rộng",
				Location = new Point(panel.Width - 100, 10),
				Size = new Size(80, 30),
				FlatStyle = FlatStyle.Flat,
				ForeColor = Color.FromArgb(0, 153, 153),
				Tag = panel
			};
			btnToggle.FlatAppearance.BorderSize = 0;
			btnToggle.Click += (s, e) =>
			{
				var lessonsPanel = panel.Controls.OfType<Panel>().FirstOrDefault(p => p.Name == "LessonsPanel");
				if (lessonsPanel == null)
				{
					lessonsPanel = CreateLessonsPanel(chapter);
					lessonsPanel.Name = "LessonsPanel";
					lessonsPanel.Location = new Point(0, 50);
					panel.Controls.Add(lessonsPanel);
					panel.Height += lessonsPanel.Height + 10;
					btnToggle.Text = "Thu gọn";
				}
				else
				{
					panel.Controls.Remove(lessonsPanel);
					panel.Height = 50;
					btnToggle.Text = "Mở rộng";
				}
				AdjustSubsequentPanels(panel, lessonsPanel?.Height ?? 0);
			};

			panel.Controls.AddRange(new Control[] { lblTitle, lblCount, btnToggle });
			y += 60;
			return panel;
		}

		private Panel CreateLessonsPanel(CourseChapter chapter)
		{
			var panel = new Panel
			{
				Width = pnlChapters.Width - 50,
				AutoSize = true,
				Padding = new Padding(30, 10, 10, 10)
			};

			int ly = 0;
			foreach (var lesson in chapter.Lessons?.OrderBy(l => l.OrderIndex) ?? Enumerable.Empty<Lesson>())
			{
				var row = new Panel { Height = 40, Width = panel.Width, Location = new Point(0, ly) };

				var icon = new Label
				{
					Text = "Play",
					Location = new Point(0, 10),
					Width = 20,
					ForeColor = Color.Gray,
					Font = new Font("Segoe UI", 9F)
				};

				var lblLesson = new Label
				{
					Text = lesson.Title,
					Location = new Point(25, 10),
					AutoSize = true,
					ForeColor = Color.FromArgb(51, 51, 51)
				};

				var iconsPanel = new FlowLayoutPanel
				{
					Location = new Point(panel.Width - 150, 5),
					Width = 140,
					FlowDirection = FlowDirection.LeftToRight
				};

				foreach (var content in lesson.LessonContents?.Take(3) ?? Enumerable.Empty<LessonContent>())
				{
					var btn = new Button
					{
						Width = 28,
						Height = 28,
						FlatStyle = FlatStyle.Flat,
						BackgroundImageLayout = ImageLayout.Zoom
					};
					btn.FlatAppearance.BorderSize = 0;

					btn.BackgroundImage = GetContentIcon(content.ContentType);
					iconsPanel.Controls.Add(btn);
				}

				row.Controls.AddRange(new Control[] { icon, lblLesson, iconsPanel });
				panel.Controls.Add(row);
				ly += 45;
			}

			panel.Height = ly + 10;
			return panel;
		}

		private void AdjustSubsequentPanels(Panel current, int heightChange)
		{
			bool found = false;
			foreach (Control ctrl in pnlChapters.Controls)
			{
				if (ctrl == current) { found = true; continue; }
				if (found && ctrl is Panel p) p.Top += heightChange - 10;
			}
		}

		private void LoadRatingSummary()
		{
			lblAvgRating.Text = _course.AverageRating.ToString("F1");
			lblTotalRatingCount.Text = $"{_course.TotalReviews} đánh giá";

			for (int i = 5; i >= 1; i--)
			{
				var count = _course.CourseReviews?.Count(r => r.Rating == i) ?? 0;
				var percent = _course.TotalReviews > 0 ? (int)Math.Round((double)count * 100 / _course.TotalReviews) : 0;

				var pb = ratingProgressBars[5 - i];
				pb.Value = Math.Min(percent, 100); // Ép kiểu int

				var lbl = ratingPercentLabels[5 - i];
				lbl.Text = $"{percent}%";
			}
		}

		private void LoadReviews()
		{
			flowReviews.Controls.Clear();
			foreach (var review in _course.CourseReviews?.OrderByDescending(r => r.CreatedAt).Take(10) ?? Enumerable.Empty<CourseReview>())
			{
				var panel = new Panel
				{
					Width = flowReviews.Width - 30,
					Height = 110,
					Margin = new Padding(10),
					BackColor = Color.White
				};

				var avatar = new Panel
				{
					Size = new Size(50, 50),
					Location = new Point(10, 10),
					BackColor = Color.FromArgb(0, 128, 128)
				};
				var initials = new Label
				{
					Text = GetInitials(review.User.FullName),
					Dock = DockStyle.Fill,
					TextAlign = ContentAlignment.MiddleCenter,
					ForeColor = Color.White,
					Font = new Font("Segoe UI", 12F, FontStyle.Bold)
				};
				avatar.Controls.Add(initials);

				var lblName = new Label { Text = review.User.FullName, Location = new Point(70, 10), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
				var lblDate = new Label { Text = review.CreatedAt.ToString("dd/MM/yyyy"), Location = new Point(70, 30), AutoSize = true, ForeColor = Color.Gray };
				//var stars = new Label { Text = new string('Star', (int)review.Rating) + new string('Empty Star', (int)(5 - review.Rating)), Location = new Point(70, 50), ForeColor = Color.Orange, Font = new Font("Segoe UI", 10F) };
				var stars = new Label { Text = new string(""), Location = new Point(70, 50), ForeColor = Color.Orange, Font = new Font("Segoe UI", 10F) };
				var lblComment = new Label { Text = review.Comment, Location = new Point(10, 75), Width = panel.Width - 20, AutoSize = true, MaximumSize = new Size(panel.Width - 20, 0), ForeColor = Color.FromArgb(51, 51, 51) };

				panel.Controls.AddRange(new Control[] { avatar, lblName, lblDate, stars, lblComment });
				flowReviews.Controls.Add(panel);
			}
		}

		private string GetInitials(string name)
		{
			return string.Join("", name.Split(' ').Take(2).Select(w => char.ToUpper(w[0])));
		}

		private void UpdateActionButtons()
		{
			var hasPurchased = _course.CoursePurchases?.Any(p => p.BuyerId == _currentUserId && p.Status == "Paid") ?? false;
			var isOwner = _course.OwnerId == _currentUserId;

			btnStartLearning.Visible = hasPurchased;
			btnAddToCart.Visible = !hasPurchased && !isOwner;
			btnBuyNow.Visible = !hasPurchased && !isOwner;
		}

		private void btnAddToCart_Click(object sender, EventArgs e) => MessageBox.Show("Đã thêm vào giỏ hàng!");
		private void btnBuyNow_Click(object sender, EventArgs e) => MessageBox.Show("Chuyển đến thanh toán...");
		private void btnStartLearning_Click(object sender, EventArgs e) => MessageBox.Show("Mở khóa học...");

		// Helper: Trả về ảnh thay thế
		private Image GetPlaceholderImage()
		{
			var bmp = new Bitmap(320, 180);
			using (var g = Graphics.FromImage(bmp))
			{
				g.Clear(Color.LightGray);
				g.DrawString("No Image", new Font("Segoe UI", 14F), Brushes.DarkGray, new PointF(100, 80));
			}
			return bmp;
		}

		// Helper: Trả về icon theo loại nội dung
		private Image GetContentIcon(string type)
		{
			var bmp = new Bitmap(28, 28);
			using (var g = Graphics.FromImage(bmp))
			{
				g.Clear(Color.Transparent);
				var brush = Brushes.Teal;
				switch (type)
				{
					case "Video": g.FillEllipse(brush, 4, 4, 20, 20); g.DrawString("Vid", new Font("Segoe UI", 6F), Brushes.White, new PointF(6, 9)); break;
					case "Theory": g.FillRectangle(brush, 4, 4, 20, 20); g.DrawString("Doc", new Font("Segoe UI", 6F), Brushes.White, new PointF(6, 9)); break;
					case "FlashcardSet": g.FillPolygon(brush, new Point[] { new(14, 4), new(24, 14), new(14, 24), new(4, 14) }); break;
					case "Test": g.DrawRectangle(Pens.Teal, 4, 4, 20, 20); g.DrawString("Quiz", new Font("Segoe UI", 6F), Brushes.Teal, new PointF(5, 9)); break;
					default: g.FillRectangle(brush, 4, 4, 20, 20); break;
				}
			}
			return bmp;
		}
	}
}