using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;
using WinFormsApp1.View.User.Forms;
using WinFormsApp1.ViewModels;
using WinFormsApp1.View.Dialogs;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.View.User.Controls.CourseControls
{
	public partial class CourseDetailControl : UserControl
	{
		private readonly CourseController _controller;
		private int _courseId;
		private Course _course;

		public CourseDetailControl()
		{
			InitializeComponent();
			_controller = new CourseController();

			btnAddToCart.Click += btnAddToCart_Click;
			btnBuyNow.Click += btnBuyNow_Click;
			btnStartLearning.Click += btnStartLearning_Click;
			lnkExpandAll.LinkClicked += lnkExpandAll_LinkClicked;

			btnEditCourse.Click += BtnEditCourse_Click;
			btnViewCourse.Click += BtnViewCourse_Click;
			btnStatistics.Click += BtnStatistics_Click;
		}
		public CourseDetailControl(int courseId)
		{
			InitializeComponent();
			_controller = new CourseController();

			// fire-and-forget load; callers should await LoadCourseAsync if they need to
			_ = LoadCourseAsync(courseId);

			btnAddToCart.Click += btnAddToCart_Click;
			btnBuyNow.Click += btnBuyNow_Click;
			btnStartLearning.Click += btnStartLearning_Click;
			lnkExpandAll.LinkClicked += lnkExpandAll_LinkClicked;

			btnEditCourse.Click += BtnEditCourse_Click;
			btnViewCourse.Click += BtnViewCourse_Click;
			btnStatistics.Click += BtnStatistics_Click;
		}

		public async System.Threading.Tasks.Task LoadCourseAsync(int courseId)
		{
			_courseId = courseId;
			try
			{
				_course = await _controller.GetCourseDetailAsync(courseId);
			}
			catch (Exception ex)
			{
				_course = null;
				ToastHelper.Show(this.FindForm(), $"Lỗi khi tải khóa học: {ex.Message}");
				return;
			}

			if (_course != null)
			{
				DisplayCourseInfo();
				UpdateActionButtons();
				await LoadRatingDistribution();
				LoadChapters();
				LoadReviews();
			}
			else
			{
				ToastHelper.Show(this.FindForm(), "Không tìm thấy khóa học");
			}
		}

		private void DisplayCourseInfo()
		{
			lblTitle.Text = _course.Title;
			lblBreadcrumb.Text = $"Khóa học / {_course.Category?.Name ?? "Chưa phân loại"} / {_course.Title}";

			var stars = new string('★', (int)Math.Round(_course.AverageRating)) + new string('☆', 5 - (int)Math.Round(_course.AverageRating));
			lblRating.Text = $"{stars} {_course.AverageRating:F1}";
			lblRatingCount.Text = $"({_course.TotalReviews:N0} đánh giá)";
			lblStudents.Text = $"{_course.CoursePurchases.Count:N0} học viên";
			lblInstructor.Text = $"Giảng viên: {_course.Owner.FullName}";
			lblLastUpdated.Text = $"Cập nhật: {_course.UpdatedAt?.ToString("MM/yyyy") ?? _course.CreatedAt.ToString("MM/yyyy")}";
			lblPrice.Text = $"{_course.Price:N0}đ";

			var totalLessons = _course.CourseChapters.Sum(ch => ch.Lessons.Count);
			lblChapterStats.Text = $"{_course.CourseChapters.Count} chương • {totalLessons} bài học";

			rtbDescription.Text = _course.Summary ?? "Chưa có mô tả";


			lblAvgRating.Text = _course.AverageRating.ToString("F1");
			lblTotalRatingCount.Text = $"({_course.TotalReviews:N0} đánh giá)";

			if (!string.IsNullOrEmpty(_course.CoverUrl))
			{
				try { picCover.Load(_course.CoverUrl); } catch { }
			}
		}

		private void UpdateActionButtons()
		{
			// default: buyer=false, owner=false
			bool isOwner = false;
			bool isBuyer = false;

			var currentUser = AuthHelper.CurrentUser;
			if (currentUser != null)
			{
				isOwner = _course.OwnerId == currentUser.UserId;
				isBuyer = _course.CoursePurchases != null && _course.CoursePurchases.Any(p => p.BuyerId == currentUser.UserId && string.Equals(p.Status, "Paid", StringComparison.OrdinalIgnoreCase));
			}

			// Owner: show manage buttons
			btnEditCourse.Visible = isOwner;
			btnViewCourse.Visible = isOwner;
			//btnStatistics.Visible = isOwner;

			// Buyer: show start learning
			btnStartLearning.Visible = isBuyer;

			// If owner, we typically don't show purchase buttons
			btnAddToCart.Visible = !isOwner && !isBuyer;
			btnBuyNow.Visible = !isOwner && !isBuyer;

			// adjust BuyNow/AddToCart enabled state for guest
			if (currentUser == null)
			{
				btnAddToCart.Enabled = true; // allow adding but will prompt to login when clicked
				btnBuyNow.Enabled = true;
			}
		}

		private async System.Threading.Tasks.Task LoadRatingDistribution()
		{
			var distribution = await _controller.GetRatingDistributionAsync(_courseId);
			var total = distribution.Values.Sum();

			if (total > 0 && ratingProgressBars != null && ratingPercentLabels != null)
			{
				for (int i = 0; i < 5; i++)
				{
					var rating = 5 - i;
					var count = distribution[rating];

					if (ratingProgressBars[i] != null && ratingPercentLabels[i] != null)
					{
						ratingProgressBars[i].Maximum = total;
						ratingProgressBars[i].Value = count;
						ratingPercentLabels[i].Text = $"{(count * 100 / total)}%";
					}
				}
			}
		}

		private void LoadChapters()
		{
			pnlChapters.Controls.Clear();

			foreach (var chapter in _course.CourseChapters.OrderBy(c => c.OrderIndex))
			{
				var pnl = new Panel { Width = 700, Height = 50, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(0, 0, 0, 10) };
				var lbl = new Label { Text = $"{chapter.Title} ({chapter.Lessons.Count} bài)", Location = new Point(10, 15), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
				pnl.Controls.Add(lbl);
				pnlChapters.Controls.Add(pnl);
			}
		}

		private void LoadReviews()
		{
			flowReviews.Controls.Clear();

			// Kiểm tra nếu user đã mua khóa học
			var currentUser = AuthHelper.CurrentUser;
			if (currentUser != null)
			{
				var hasPurchased = _course.CoursePurchases?.Any(p => p.BuyerId == currentUser.UserId && p.Status == "Paid") ?? false;
				if (hasPurchased)
				{
					var userReview = _course.CourseReviews.FirstOrDefault(r => r.UserId == currentUser.UserId);
					if (userReview == null)
					{
						flowReviews.Controls.Add(CreateReviewPrompt());
					}
					else
					{
						flowReviews.Controls.Add(CreateUserReviewPanel(userReview));
					}
				}
			}

			foreach (var review in _course.CourseReviews.Where(r => r.IsApproved).OrderByDescending(r => r.CreatedAt).Take(10))
			{
				var pnl = new Panel { Width = 700, AutoSize = true, BackColor = ColorTranslator.FromHtml("#F8F9FA"), Padding = new Padding(15), Margin = new Padding(0, 0, 0, 10) };
				var lblName = new Label { Text = $"{review.User.FullName} - {new string('★', (int)review.Rating)}{new string('☆', 5 - (int)review.Rating)}", AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(0, 0) };
				var lblReview = new Label { Text = review.Comment ?? "", AutoSize = true, MaximumSize = new Size(670, 0), Location = new Point(0, 25) };
				pnl.Controls.Add(lblName);
				pnl.Controls.Add(lblReview);
				flowReviews.Controls.Add(pnl);
			}
		}

		private async void btnAddToCart_Click(object sender, EventArgs e)
		{
			var userId = AuthHelper.CurrentUser?.UserId;
			if (!userId.HasValue)
			{
				MessageBox.Show("Vui lòng đăng nhập để thêm vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (_course == null)
			{
				MessageBox.Show("Thông tin khóa học chưa được tải.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				using var context = new LearningPlatformContext();

				var cart = await context.ShoppingCarts
					.FirstOrDefaultAsync(c => c.UserId == userId.Value);

				if (cart == null)
				{
					cart = new ShoppingCart
					{
						UserId = userId.Value,
						CreatedAt = DateTime.Now
					};
					context.ShoppingCarts.Add(cart);
					await context.SaveChangesAsync();
				}

				var existingItem = await context.CartItems
					.FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.CourseId == _course.CourseId);

				if (existingItem == null)
				{
					var cartItem = new CartItem
					{
						CartId = cart.CartId,
						CourseId = _course.CourseId,
						AddedAt = DateTime.Now
					};
					context.CartItems.Add(cartItem);
					await context.SaveChangesAsync();

					ToastHelper.Show(this.FindForm(), "Đã thêm khóa học vào giỏ hàng!");
					btnAddToCart.Text = "Trong giỏ hàng";
					btnAddToCart.Enabled = false;
				}
				else
				{
					ToastHelper.Show(this.FindForm(), "Khóa học đã có trong giỏ hàng!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi thêm vào giỏ hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async void btnBuyNow_Click(object sender, EventArgs e)
		{
			var userId = AuthHelper.CurrentUser?.UserId;
			if (!userId.HasValue)
			{
				MessageBox.Show("Vui lòng đăng nhập để mua khóa học", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (_course == null)
			{
				MessageBox.Show("Thông tin khóa học chưa được tải.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				using var context = new LearningPlatformContext();

				// Ensure shopping cart exists
				var cart = await context.ShoppingCarts.FirstOrDefaultAsync(c => c.UserId == userId.Value);
				if (cart == null)
				{
					cart = new ShoppingCart
					{
						UserId = userId.Value,
						CreatedAt = DateTime.Now
					};
					context.ShoppingCarts.Add(cart);
					await context.SaveChangesAsync();
				}

				// Add course to cart if not already present
				var existingItem = await context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.CourseId == _course.CourseId);
				if (existingItem == null)
				{
					var cartItem = new CartItem
					{
						CartId = cart.CartId,
						CourseId = _course.CourseId,
						AddedAt = DateTime.Now
					};
					context.CartItems.Add(cartItem);
					await context.SaveChangesAsync();
				}

				// Open checkout form so user can proceed to payment
				using var checkout = new WinFormsApp1.View.User.Forms.frmCheckout();
				checkout.StartPosition = FormStartPosition.CenterParent;
				var owner = this.FindForm();
				DialogResult result;
				if (owner != null)
				{
					result = checkout.ShowDialog(owner);
				}
				else
				{
					result = checkout.ShowDialog();
				}

				// If checkout dialog reported success, reload course to update purchase state
				if (result == DialogResult.OK)
				{
					try
					{
						await LoadCourseAsync(_courseId);
						UpdateActionButtons();
						ToastHelper.Show(this.FindForm(), "Thanh toán thành công! Bạn có thể bắt đầu học ngay.");
					}
					catch { /* ignore reload errors */ }
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi thêm vào giỏ hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async void btnStartLearning_Click(object sender, EventArgs e)
		{
			// If user not logged in, prompt
			var userId = AuthHelper.CurrentUser?.UserId;
			if (!userId.HasValue)
			{
				MessageBox.Show("Vui lòng đăng nhập để bắt đầu học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (_course == null)
			{
				MessageBox.Show("Thông tin khóa học chưa được tải.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Find first lesson in course
			var firstLesson = _course.CourseChapters
				.OrderBy(ch => ch.OrderIndex)
				.SelectMany(ch => ch.Lessons.OrderBy(l => l.OrderIndex))
				.FirstOrDefault();

			if (firstLesson == null)
			{
				MessageBox.Show("Khóa học chưa có bài học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			// Navigate to LessonDetailControl (same approach as in CourseControl / MyCoursesControl)
			var form = this.FindForm();
			if (form is MainContainer mainContainer)
			{
				var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
				if (mainPanel != null)
				{
					mainPanel.Controls.Clear();

					var lessonDetail = new WinFormsApp1.View.User.Controls.LessonDetailControl();
					lessonDetail.Dock = DockStyle.Fill;
					mainPanel.Controls.Add(lessonDetail);

					// load first lesson
					_ = lessonDetail.LoadLessonAsync(_course.Slug, firstLesson.LessonId);
				}
				else
				{
					MessageBox.Show("Không thể tìm thấy vùng nội dung chính để điều hướng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show("Không thể điều hướng từ context hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		private void lnkExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// Try to expand any collapsible content panels inside pnlChapters.
			// The chapter entries in this control may be simple panels; if they contain a FlowLayoutPanel
			// (created by CreateExpandableLessonItem), make it visible.
			try
			{
				foreach (Control chapterCtrl in pnlChapters.Controls)
				{
					// look for a FlowLayoutPanel or Panel representing the contents (children)
					var contents = chapterCtrl.Controls.OfType<FlowLayoutPanel>().FirstOrDefault()
					               ?? chapterCtrl.Controls.OfType<Panel>().FirstOrDefault(p => p.Height > 20 && p != chapterCtrl);

					if (contents != null)
					{
						contents.Visible = true;
					}
				}

				ToastHelper.Show(this.FindForm(), "Đã mở rộng tất cả chương");
			}
			catch
			{
				// ignore errors — this is a best-effort UI behaviour
			}
		}

		private async void BtnEditCourse_Click(object sender, EventArgs e)
		{
			try
			{
				if (_courseId <= 0)
				{
					MessageBox.Show("Không có khóa học để chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				// ensure course is loaded
				if (_course == null)
				{
					_course = await _controller.GetCourseDetailAsync(_courseId);
					if (_course == null)
					{
						MessageBox.Show("Không tìm thấy khóa học", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}

				// convert course entity to CourseBuilderViewModel using CourseBuilderController
				var builderCtrl = new CourseBuilderController();
				var vm = await builderCtrl.LoadCourseAsync(_courseId);

				if (vm == null)
				{
					MessageBox.Show("Không thể nạp dữ liệu chỉnh sửa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				using var form = new CourseBuilderForm(vm, _courseId);
				form.StartPosition = FormStartPosition.CenterParent;
				form.ShowDialog();

				// After editing, reload course details
				await LoadCourseAsync(_courseId);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi mở trình chỉnh sửa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async void BtnViewCourse_Click(object sender, EventArgs e)
		{
			try
			{
				if (_course == null)
				{
					MessageBox.Show("Không thể tải thông tin khóa học", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Find first lesson
				var firstLesson = _course.CourseChapters
					.OrderBy(ch => ch.OrderIndex)
					.SelectMany(ch => ch.Lessons.OrderBy(l => l.OrderIndex))
					.FirstOrDefault();

				if (firstLesson == null)
				{
					MessageBox.Show("Khóa học chưa có bài học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				var form = this.FindForm();
				if (form is MainContainer mainContainer)
				{
					var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
					if (mainPanel != null)
					{
						mainPanel.Controls.Clear();

						var lessonDetailControl = new WinFormsApp1.View.User.Controls.LessonDetailControl();
						lessonDetailControl.Dock = DockStyle.Fill;
						mainPanel.Controls.Add(lessonDetailControl);

						// Load lesson
						_ = lessonDetailControl.LoadLessonAsync(_course.Slug, firstLesson.LessonId);
					}
					else
					{
						MessageBox.Show("Không thể tìm thấy vùng nội dung chính để điều hướng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else
				{
					MessageBox.Show("Không thể điều hướng từ context hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi mở khóa học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void BtnStatistics_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Hiển thị thống kê khóa học (lượt mua, tiến độ)", "Thống kê", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private Panel CreateReviewPrompt()
		{
			var pnl = new Panel { Width = 700, Height = 80, BackColor = ColorTranslator.FromHtml("#D1F2EB"), Padding = new Padding(15), Margin = new Padding(0, 0, 0, 15) };
			var lbl = new Label { Text = "★ Bạn đã mua khóa học này. Hãy chia sẻ trải nghiệm của bạn!", AutoSize = true, Font = new Font("Segoe UI", 10F), Location = new Point(0, 15) };
			var btn = new Button { Text = "Viết đánh giá", Size = new Size(120, 35), BackColor = ColorTranslator.FromHtml("#007BFF"), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(550, 20), Cursor = Cursors.Hand };
			btn.FlatAppearance.BorderSize = 0;
			btn.Click += (s, e) => ShowReviewDialog(null);
			pnl.Controls.Add(lbl);
			pnl.Controls.Add(btn);
			return pnl;
		}

		private Panel CreateUserReviewPanel(CourseReview review)
		{
			var pnl = new Panel { Width = 700, Height = 120, BackColor = ColorTranslator.FromHtml("#D4EDDA"), Padding = new Padding(15), Margin = new Padding(0, 0, 0, 15) };
			var lblTitle = new Label { Text = "✓ Đánh giá của bạn:", AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Location = new Point(0, 0) };
			var lblStars = new Label { Text = new string('★', (int)review.Rating) + new string('☆', 5 - (int)review.Rating), AutoSize = true, Font = new Font("Segoe UI", 14F), ForeColor = ColorTranslator.FromHtml("#FFA500"), Location = new Point(0, 25) };
			var lblComment = new Label { Text = review.Comment ?? "", AutoSize = true, MaximumSize = new Size(500, 0), Location = new Point(0, 55) };
			var btnEdit = new Button { Text = "✏ Sửa", Size = new Size(80, 30), BackColor = ColorTranslator.FromHtml("#FFC107"), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(550, 15), Cursor = Cursors.Hand };
			var btnDelete = new Button { Text = "🗑 Xóa", Size = new Size(80, 30), BackColor = ColorTranslator.FromHtml("#DC3545"), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(550, 55), Cursor = Cursors.Hand };
			btnEdit.FlatAppearance.BorderSize = 0;
			btnDelete.FlatAppearance.BorderSize = 0;
			btnEdit.Click += (s, e) => ShowReviewDialog(review);
			btnDelete.Click += async (s, e) => await DeleteReview(review.ReviewId);
			pnl.Controls.AddRange(new Control[] { lblTitle, lblStars, lblComment, btnEdit, btnDelete });
			return pnl;
		}

		private void ShowReviewDialog(CourseReview existingReview)
		{
			using var form = new Form { Text = existingReview == null ? "Viết đánh giá" : "Sửa đánh giá", Size = new Size(450, 350), StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false };
			var lblRating = new Label { Text = "Xếp hạng:", Location = new Point(20, 20), AutoSize = true };
			var numRating = new NumericUpDown { Location = new Point(120, 18), Width = 60, Minimum = 1, Maximum = 5, Value = existingReview?.Rating ?? 5 };
			var lblComment = new Label { Text = "Nhận xét:", Location = new Point(20, 60), AutoSize = true };
			var txtComment = new TextBox { Location = new Point(20, 85), Size = new Size(390, 120), Multiline = true, Text = existingReview?.Comment ?? "" };
			var btnSave = new Button { Text = "Lưu", Location = new Point(250, 230), Size = new Size(80, 35), BackColor = ColorTranslator.FromHtml("#28A745"), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
			var btnCancel = new Button { Text = "Hủy", Location = new Point(340, 230), Size = new Size(70, 35), DialogResult = DialogResult.Cancel };
			btnSave.Click += async (s, e) => { await SaveReview(existingReview, (int)numRating.Value, txtComment.Text); form.DialogResult = DialogResult.OK; };
			form.Controls.AddRange(new Control[] { lblRating, numRating, lblComment, txtComment, btnSave, btnCancel });
			if (form.ShowDialog() == DialogResult.OK) LoadReviews();
		}

		private async System.Threading.Tasks.Task SaveReview(CourseReview existing, int rating, string comment)
		{
			try
			{
				using var context = new LearningPlatformContext();
				if (existing == null)
				{
					var review = new CourseReview { CourseId = _courseId, UserId = AuthHelper.CurrentUser.UserId, Rating = rating, Comment = comment, CreatedAt = DateTime.Now, IsApproved = true };
					context.CourseReviews.Add(review);
				}
				else
				{
					var review = await context.CourseReviews.FindAsync(existing.ReviewId);
					if (review != null) { review.Rating = rating; review.Comment = comment; }
				}
				await context.SaveChangesAsync();
				await LoadCourseAsync(_courseId);
				ToastHelper.Show(this.FindForm(), "Đánh giá thành công!");
			}
			catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
		}

		private async System.Threading.Tasks.Task DeleteReview(int reviewId)
		{
			if (MessageBox.Show("Xóa đánh giá?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				try
				{
					using var context = new LearningPlatformContext();
					var review = await context.CourseReviews.FindAsync(reviewId);
					if (review != null) { context.CourseReviews.Remove(review); await context.SaveChangesAsync(); }
					await LoadCourseAsync(_courseId);
					ToastHelper.Show(this.FindForm(), "Đã xóa đánh giá");
				}
				catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
			}
		}

		private void CourseDetailControl_Load(object sender, EventArgs e)
		{

		}

	}
}
