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

			lblInstructorName.Text = _course.Owner.FullName;
			lblInstructorEmail.Text = _course.Owner.Email;

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
			btnStatistics.Visible = isOwner;

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

			ShowPaymentForm();
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

		private void BtnViewCourse_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Xem khóa học như học viên (preview)", "Xem khóa học", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void BtnStatistics_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Hiển thị thống kê khóa học (lượt mua, tiến độ)", "Thống kê", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void ShowPaymentForm()
		{
			if (_course == null)
			{
				MessageBox.Show("Không thể tải thông tin khóa học", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				using (var paymentForm = new PaymentForm(_course))
				{
					var result = paymentForm.ShowDialog(this.FindForm());

					if (result == DialogResult.OK)
					{
						// Thanh toán thành công, cập nhật giao diện
						UpdateActionButtons();
						ToastHelper.Show(this.FindForm(), "Thanh toán thành công! Bạn có thể bắt đầu học ngay.");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi mở form thanh toán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CourseDetailControl_Load(object sender, EventArgs e)
		{

		}

		private void btnStartLearning_Click_1(object sender, EventArgs e)
		{

		}
	}
}
