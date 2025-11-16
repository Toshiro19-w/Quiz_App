using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Controls
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
        }

        public async void LoadCourse(int courseId)
        {
            _courseId = courseId;
            _course = await _controller.GetCourseDetailAsync(courseId);
            
            if (_course != null)
            {
                DisplayCourseInfo();
                await LoadRatingDistribution();
                LoadChapters();
                LoadReviews();
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

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã thêm vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBuyNow_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chuyển đến trang thanh toán...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStartLearning_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bắt đầu học khóa học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lnkExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Mở rộng tất cả chương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
