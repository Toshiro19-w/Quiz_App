using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;

namespace WinFormsApp1.View.User.Controls.TestControls
{
    public partial class TestAttemptsListControl : UserControl
    {
        private int _testId;
        private Models.Entities.Test _test;
        private Control _previousControl; // Lưu control trước đó để quay lại

        public TestAttemptsListControl()
        {
            InitializeComponent();
            btnBack.Click += BtnBack_Click;
        }

        public async Task LoadAttemptsAsync(int testId, Control previousControl = null)
        {
            _testId = testId;
            _previousControl = previousControl;

            try
            {
                using var context = new LearningPlatformContext();
                var userId = AuthHelper.CurrentUser?.UserId;

                if (!userId.HasValue)
                {
                    MessageBox.Show("Vui lòng đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _test = await context.Tests
                    .Include(t => t.TestAttempts.Where(ta => ta.UserId == userId.Value))
                    .FirstOrDefaultAsync(t => t.TestId == testId);

                if (_test == null)
                {
                    MessageBox.Show("Không tìm thấy bài kiểm tra!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblTitle.Text = $"Lịch sử làm bài: {_test.Title}";

                LoadAttemptsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAttemptsList()
        {
            flowAttempts.Controls.Clear();

            var attempts = _test.TestAttempts
                .OrderByDescending(ta => ta.SubmittedAt ?? ta.StartedAt)
                .ToList();

            if (attempts.Count == 0)
            {
                var lblEmpty = new Label
                {
                    Text = "Chưa có lần làm bài nào",
                    Font = new Font("Segoe UI", 12),
                    ForeColor = Color.Gray,
                    AutoSize = true
                };
                flowAttempts.Controls.Add(lblEmpty);
                return;
            }

            for (int i = 0; i < attempts.Count; i++)
            {
                var attempt = attempts[i];
                var attemptCard = new TestAttemptCard();
                attemptCard.LoadAttempt(attempt, attempts.Count - i);
                attemptCard.ViewDetailsClicked += (s, attemptId) => ViewAttemptDetails(attemptId);
                
                flowAttempts.Controls.Add(attemptCard);
            }
        }

        private void ViewAttemptDetails(int attemptId)
        {
            try
            {
                var parentPanel = this.Parent as Panel;
                if (parentPanel == null)
                {
                    MessageBox.Show("Không thể mở trang xem chi tiết!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                parentPanel.Controls.Clear();

                var reviewControl = new TestReviewControl();
                reviewControl.Dock = DockStyle.Fill;
                
                // Truyền reference của TestAttemptsListControl này để quay lại
                reviewControl.SetPreviousControl(this);
                
                parentPanel.Controls.Add(reviewControl);
                _ = reviewControl.LoadAttemptAsync(attemptId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở trang xem chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                var parentPanel = this.Parent as Panel;
                if (parentPanel == null) return;

                parentPanel.Controls.Clear();

                // Nếu có previousControl (TestTakingControl), thì hiển thị lại nó
                if (_previousControl != null)
                {
                    _previousControl.Dock = DockStyle.Fill;
                    parentPanel.Controls.Add(_previousControl);
                }
                else
                {
                    // Nếu không có, tạo mới TestTakingControl
                    var testTakingControl = new TestTakingControl();
                    testTakingControl.Dock = DockStyle.Fill;
                    parentPanel.Controls.Add(testTakingControl);
                    
                    // Load lại test
                    using var context = new LearningPlatformContext();
                    var test = context.Tests.Find(_testId);
                    if (test != null)
                    {
                        // Cần thêm logic để load lại test với lesson content và course
                        // Tạm thời chỉ thông báo
                        MessageBox.Show("Đã quay lại trang làm bài", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi quay lại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

