using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Controls.TestControls
{
    public partial class TestTakingControl : UserControl
    {
        private Test _currentTest;
        private LessonContent _lessonContent;
        private Course _currentCourse;
        
        private List<Question> _questions;
        private Dictionary<int, List<int>> _selectedAnswers = new Dictionary<int, List<int>>();
        private DateTime _testStartTime;
        private System.Windows.Forms.Timer _countdownTimer;
        private int _remainingSeconds;
        
        private bool _isTeacherView = false;

        public TestTakingControl()
        {
            InitializeComponent();
            
            btnStartTest.Click += BtnStartTest_Click;
            btnReviewTest.Click += BtnReviewTest_Click;
            btnViewResults.Click += BtnViewResults_Click;
            btnSubmitTest.Click += BtnSubmitTest_Click;
        }

        public async Task LoadTestAsync(Test test, LessonContent lessonContent, Course course)
        {
            _currentTest = test;
            _lessonContent = lessonContent;
            _currentCourse = course;
            
            // Check if current user is the course owner (teacher)
            _isTeacherView = AuthHelper.CurrentUser?.UserId == course.OwnerId;

            lblTestTitle.Text = test.Title;
            lblTestDescription.Text = test.Description ?? "Không có mô tả";

            await LoadTestInfoAsync();
        }

        private async Task LoadTestInfoAsync()
        {
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return;

            using var context = new LearningPlatformContext();

            // Count attempts
            var attemptCount = await context.TestAttempts
                .Where(ta => ta.TestId == _currentTest.TestId && ta.UserId == userId.Value)
                .CountAsync();

            // Get highest score
            var highestScore = await context.TestAttempts
                .Where(ta => ta.TestId == _currentTest.TestId && ta.UserId == userId.Value)
                .MaxAsync(ta => (decimal?)ta.Score) ?? 0;

            // Calculate max score
            var questions = await context.Questions
                .AsNoTracking()
                .Where(q => q.TestId == _currentTest.TestId)
                .ToListAsync();

            decimal maxScore = _currentTest.MaxScore ?? questions.Sum(q => q.Points);

            // Update UI
            int timeLimit = (_currentTest.TimeLimitSec ?? 0) / 60;
            lblTimeLimit.Text = $"⏱️ Thời gian: {timeLimit} phút";

            // Display attempts info
            if (_currentTest.MaxAttempts.HasValue && _currentTest.MaxAttempts.Value > 0)
            {
                lblAttempts.Text = $"🔄 Số lần làm: {attemptCount}/{_currentTest.MaxAttempts}";
            }
            else
            {
                lblAttempts.Text = $"🔄 Số lần làm: {attemptCount}/∞ (Không giới hạn)";
            }

            if (attemptCount > 0)
            {
                lblHighScore.Text = $"🏆 Điểm cao nhất: {highestScore}/{maxScore}";
                lblHighScore.Visible = true;
            }
            else
            {
                lblHighScore.Visible = false;
            }

            // Check if attempts are exhausted
            bool hasAttemptsLeft = !_currentTest.MaxAttempts.HasValue || 
                                   _currentTest.MaxAttempts.Value == 0 || 
                                   attemptCount < _currentTest.MaxAttempts.Value;

            btnStartTest.Enabled = hasAttemptsLeft;
            btnStartTest.BackColor = hasAttemptsLeft ? Color.FromArgb(40, 167, 69) : Color.Gray;

            // SỬA LẠI: Hiện nút xem lại khi có ít nhất 1 lần làm (bỏ điều kiện hết lượt)
            btnReviewTest.Visible = attemptCount > 0;

            // Show results button only for teacher
            btnViewResults.Visible = _isTeacherView;

            // Hide test content initially
            pnlTestContent.Visible = false;
            pnlTestFooter.Visible = false;
        }

        private async void BtnStartTest_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirm before starting
                var result = MessageBox.Show(
                    "Bạn có chắc chắn muốn bắt đầu làm bài?\n\n" +
                    "Sau khi bắt đầu, đồng hồ đếm ngược sẽ bắt đầu chạy.",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                // Hide info panel
                pnlTestInfo.Visible = false;

                // Show test content
                pnlTestContent.Visible = true;
                pnlTestFooter.Visible = true;

                // Load test questions
                await LoadTestQuestionsAsync();

                // Start timer
                _testStartTime = DateTime.Now;
                StartCountdownTimer();

                // Scroll to top
                pnlTestContent.AutoScrollPosition = new Point(0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi bắt đầu làm bài: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadTestQuestionsAsync()
        {
            using var context = new LearningPlatformContext();

            _questions = await context.Questions
                .Include(q => q.QuestionOptions.OrderBy(o => o.OrderIndex))
                .Where(q => q.TestId == _currentTest.TestId)
                .OrderBy(q => q.OrderIndex)
                .ToListAsync();

            _selectedAnswers.Clear();

            flowQuestions.Controls.Clear();

            for (int i = 0; i < _questions.Count; i++)
            {
                var question = _questions[i];
                var questionPanel = CreateQuestionPanel(question, i + 1);
                flowQuestions.Controls.Add(questionPanel);
            }
        }

        private Panel CreateQuestionPanel(Question question, int number)
        {
            var panel = new Panel
            {
                Width = 1000,
                AutoSize = true,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 20),
                Padding = new Padding(20),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblQuestionNumber = new Label
            {
                Text = $"Câu {number}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ColorPalette.Primary,
                AutoSize = true,
                Location = new Point(20, 20)
            };

            var lblQuestionText = new Label
            {
                Text = question.StemText,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.Black,
                AutoSize = true,
                MaximumSize = new Size(940, 0),
                Location = new Point(20, 50)
            };

            panel.Controls.Add(lblQuestionNumber);
            panel.Controls.Add(lblQuestionText);

            int yPos = lblQuestionText.Bottom + 20;

            if (question.Type == "MCQ_Multi")
            {
                if (!_selectedAnswers.ContainsKey(question.QuestionId))
                    _selectedAnswers[question.QuestionId] = new List<int>();

                var lblGuide = new Label
                {
                    Text = "(Chọn nhiều đáp án)",
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Location = new Point(100, 23),
                    AutoSize = true
                };
                panel.Controls.Add(lblGuide);

                foreach (var option in question.QuestionOptions.OrderBy(o => o.OrderIndex))
                {
                    CheckBox chk = new CheckBox
                    {
                        Text = option.OptionText,
                        Font = new Font("Segoe UI", 10),
                        AutoSize = false,
                        Width = 940,
                        Height = 35,
                        Location = new Point(40, yPos),
                        Tag = option.OptionId,
                        Cursor = Cursors.Hand,
                        Padding = new Padding(5, 0, 0, 0)
                    };

                    chk.MouseEnter += (s, e) => chk.BackColor = Color.FromArgb(240, 248, 255);
                    chk.MouseLeave += (s, e) => chk.BackColor = Color.White;

                    chk.CheckedChanged += (s, e) =>
                    {
                        if (chk.Checked)
                        {
                            if (!_selectedAnswers[question.QuestionId].Contains(option.OptionId))
                                _selectedAnswers[question.QuestionId].Add(option.OptionId);
                        }
                        else
                        {
                            _selectedAnswers[question.QuestionId].Remove(option.OptionId);
                        }
                    };

                    panel.Controls.Add(chk);
                    yPos += 40;
                }
            }
            else
            {
                if (!_selectedAnswers.ContainsKey(question.QuestionId))
                    _selectedAnswers[question.QuestionId] = new List<int>();

                foreach (var option in question.QuestionOptions.OrderBy(o => o.OrderIndex))
                {
                    RadioButton radio = new RadioButton
                    {
                        Text = option.OptionText,
                        Font = new Font("Segoe UI", 10),
                        AutoSize = false,
                        Width = 940,
                        Height = 35,
                        Location = new Point(40, yPos),
                        Tag = option.OptionId,
                        Cursor = Cursors.Hand,
                        Padding = new Padding(5, 0, 0, 0)
                    };

                    radio.MouseEnter += (s, e) => radio.BackColor = Color.FromArgb(240, 248, 255);
                    radio.MouseLeave += (s, e) => radio.BackColor = Color.White;

                    radio.CheckedChanged += (s, e) =>
                    {
                        if (radio.Checked)
                        {
                            _selectedAnswers[question.QuestionId].Clear();
                            _selectedAnswers[question.QuestionId].Add(option.OptionId);
                        }
                    };

                    panel.Controls.Add(radio);
                    yPos += 40;
                }
            }

            return panel;
        }

        private void StartCountdownTimer()
        {
            // Ensure timer label is visible
            lblTimer.Visible = true;
            
            if (_currentTest.TimeLimitSec.HasValue && _currentTest.TimeLimitSec.Value > 0)
            {
                _remainingSeconds = _currentTest.TimeLimitSec.Value;

                _countdownTimer = new System.Windows.Forms.Timer();
                _countdownTimer.Interval = 1000; // 1 second
                _countdownTimer.Tick += CountdownTimer_Tick;
                _countdownTimer.Start();

                UpdateTimerLabel();
            }
            else
            {
                lblTimer.Text = "⏰ Không giới hạn thời gian";
                lblTimer.ForeColor = Color.FromArgb(0, 102, 153); // Blue color
            }
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            _remainingSeconds--;
            UpdateTimerLabel();

            if (_remainingSeconds <= 0)
            {
                _countdownTimer.Stop();
                _countdownTimer.Dispose();
                MessageBox.Show("Hết thời gian làm bài!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _ = SubmitTestAsync();
            }
        }

        private void UpdateTimerLabel()
        {
            int minutes = _remainingSeconds / 60;
            int seconds = _remainingSeconds % 60;
            lblTimer.Text = $"⏰ Thời gian còn lại: {minutes:D2}:{seconds:D2}";

            // Change color based on remaining time
            if (_remainingSeconds <= 60)
            {
                lblTimer.ForeColor = Color.FromArgb(220, 53, 69); // Red
            }
            else if (_remainingSeconds <= 300)
            {
                lblTimer.ForeColor = Color.FromArgb(255, 193, 7); // Orange/Yellow
            }
            else
            {
                lblTimer.ForeColor = Color.FromArgb(0, 102, 153); // Blue
            }
        }

        private async void BtnSubmitTest_Click(object sender, EventArgs e)
        {
            if (_selectedAnswers.Count < _questions.Count)
            {
                var result = MessageBox.Show(
                    $"Bạn chưa trả lời hết câu hỏi ({_selectedAnswers.Count}/{_questions.Count}).\n\nBạn có chắc muốn nộp bài?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;
            }

            await SubmitTestAsync();
        }

        private async Task SubmitTestAsync()
        {
            try
            {
                // Stop and dispose timer
                if (_countdownTimer != null)
                {
                    _countdownTimer.Stop();
                    _countdownTimer.Dispose();
                    _countdownTimer = null;
                }

                var userId = AuthHelper.CurrentUser?.UserId;
                if (!userId.HasValue) return;

                using var context = new LearningPlatformContext();
                var timeSpent = (int)(DateTime.Now - _testStartTime).TotalSeconds;

                decimal totalScore = 0;
                decimal maxScore = _currentTest.MaxScore ?? _questions.Sum(q => q.Points);

                // Grade answers
                foreach (var question in _questions)
                {
                    var correctOptionIds = question.QuestionOptions
                        .Where(o => o.IsCorrect)
                        .Select(o => o.OptionId)
                        .ToList();

                    if (_selectedAnswers.TryGetValue(question.QuestionId, out List<int> userSelectedIds))
                    {
                        bool isCorrect = userSelectedIds.Count == correctOptionIds.Count &&
                                       !userSelectedIds.Except(correctOptionIds).Any();

                        if (isCorrect)
                        {
                            totalScore += question.Points;
                        }
                    }
                }

                // Save attempt
                var attempt = new TestAttempt
                {
                    TestId = _currentTest.TestId,
                    UserId = userId.Value,
                    StartedAt = _testStartTime,
                    SubmittedAt = DateTime.Now,
                    Status = "Graded",
                    TimeSpentSec = timeSpent,
                    Score = totalScore,
                    MaxScore = maxScore
                };

                context.TestAttempts.Add(attempt);
                await context.SaveChangesAsync();

                // Save answers
                foreach (var kvp in _selectedAnswers)
                {
                    if (kvp.Value != null && kvp.Value.Count > 0)
                    {
                        string answerString = string.Join(",", kvp.Value);

                        var question = _questions.First(q => q.QuestionId == kvp.Key);
                        var correctOptionIds = question.QuestionOptions.Where(o => o.IsCorrect).Select(o => o.OptionId).ToList();
                        bool isCorrect = kvp.Value.Count == correctOptionIds.Count && !kvp.Value.Except(correctOptionIds).Any();

                        var answer = new AttemptAnswer
                        {
                            AttemptId = attempt.AttemptId,
                            QuestionId = kvp.Key,
                            AnswerPayload = $"{{\"selectedOptions\": [{answerString}]}}",
                            IsCorrect = isCorrect,
                            Score = isCorrect ? question.Points : 0,
                            GradedAt = DateTime.Now,
                            AutoGraded = true
                        };
                        context.AttemptAnswers.Add(answer);
                    }
                }

                await context.SaveChangesAsync();

                // Mark content complete
                await MarkContentCompleteAsync(userId.Value, totalScore, context);

                // Show result
                var percentage = maxScore > 0 ? (totalScore / maxScore) * 100 : 0;
                
                string resultMessage = $"Hoàn thành bài kiểm tra!\n\n" +
                    $"Điểm: {totalScore}/{maxScore} ({percentage:F1}%)\n" +
                    $"Thời gian làm bài: {timeSpent / 60} phút {timeSpent % 60} giây\n\n";

                if (percentage >= 80)
                    resultMessage += "🎉 Xuất sắc!";
                else if (percentage >= 60)
                    resultMessage += "👍 Khá tốt!";
                else if (percentage >= 40)
                    resultMessage += "💪 Cố gắng thêm nhé!";
                else
                    resultMessage += "📚 Hãy ôn tập thêm!";

                MessageBox.Show(resultMessage, "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload info panel
                pnlTestContent.Visible = false;
                pnlTestFooter.Visible = false;
                pnlTestInfo.Visible = true;

                await LoadTestInfoAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nộp bài: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task MarkContentCompleteAsync(int userId, decimal score, LearningPlatformContext context)
        {
            try
            {
                var progress = await context.CourseProgresses
                    .FirstOrDefaultAsync(cp =>
                        cp.UserId == userId &&
                        cp.ContentId == _lessonContent.ContentId);

                if (progress == null)
                {
                    progress = new CourseProgress
                    {
                        UserId = userId,
                        CourseId = _currentCourse.CourseId,
                        LessonId = _lessonContent.LessonId,
                        ContentType = _lessonContent.ContentType,
                        ContentId = _lessonContent.ContentId,
                        IsCompleted = true,
                        CompletionAt = DateTime.Now,
                        LastViewedAt = DateTime.Now,
                        Score = score
                    };
                    context.CourseProgresses.Add(progress);
                }
                else if (!progress.IsCompleted)
                {
                    progress.IsCompleted = true;
                    progress.CompletionAt = DateTime.Now;
                    progress.Score = score;
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking content complete: {ex.Message}");
            }
        }

        private async void BtnReviewTest_Click(object sender, EventArgs e)
        {
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return;

            try
            {
                var parentPanel = this.Parent as Panel;
                if (parentPanel == null)
                {
                    MessageBox.Show("Không thể mở trang xem lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                parentPanel.Controls.Clear();

                var attemptsListControl = new TestAttemptsListControl
                {
                    Dock = DockStyle.Fill
                };

                parentPanel.Controls.Add(attemptsListControl);
                
                // Truyền chính TestTakingControl này để có thể quay lại
                await attemptsListControl.LoadAttemptsAsync(_currentTest.TestId, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnViewResults_Click(object sender, EventArgs e)
        {
            try
            {
                var parentPanel = this.Parent as Panel;
                if (parentPanel == null)
                {
                    MessageBox.Show("Không thể mở trang danh sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                parentPanel.Controls.Clear();

                var resultsListControl = new TestResultsListControl
                {
                    Dock = DockStyle.Fill
                };

                parentPanel.Controls.Add(resultsListControl);
                
                // Truyền chính TestTakingControl này để có thể quay lại
                _ = resultsListControl.LoadResultsAsync(_currentTest.TestId, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
