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
    public partial class LessonDetailControl : UserControl
    {
        private Course _currentCourse;
        private Lesson _currentLesson;
        private List<LessonContent> _currentContents;
        private int _currentContentIndex = 0;

        // Flashcard state
        private List<Flashcard> _flashcards;
        private int _currentFlashcardIndex = 0;
        private bool _isFlipped = false;

        // Test state
        private Test _currentTest;
        private List<Question> _questions;
        private Dictionary<int, int?> _selectedAnswers = new Dictionary<int, int?>();
        private DateTime _testStartTime;

        // Video tracking
        private System.Windows.Forms.Timer _videoProgressTimer;
        private int _totalWatchedSeconds = 0;

        public LessonDetailControl()
        {
            InitializeComponent();
            InitializeEventHandlers();
            SetupVideoProgressTimer();
        }

        private void InitializeEventHandlers()
        {
            // Video events - comment out until WMP is properly configured
            // videoPlayer.PlayStateChange += VideoPlayer_PlayStateChange;

            // Flashcard events
            btnFlipCard.Click += BtnFlipCard_Click;
            btnPrevCard.Click += BtnPrevCard_Click;
            btnNextCard.Click += BtnNextCard_Click;
            btnCompleteFlashcard.Click += BtnCompleteFlashcard_Click;

            // Test events
            btnSubmitTest.Click += BtnSubmitTest_Click;

            // Navigation events
            btnPrevLesson.Click += BtnPrevLesson_Click;
            btnNextLesson.Click += BtnNextLesson_Click;
            btnMarkComplete.Click += BtnMarkComplete_Click;
        }

        private void SetupVideoProgressTimer()
        {
            _videoProgressTimer = new System.Windows.Forms.Timer();
            _videoProgressTimer.Interval = 5000; // 5 seconds
            _videoProgressTimer.Tick += VideoProgressTimer_Tick;
        }

        public async Task LoadLessonAsync(string courseSlug, int lessonId)
        {
            try
            {
                using var context = new LearningPlatformContext();

                // Load course with full details
                _currentCourse = await context.Courses
                    .Include(c => c.CourseChapters.OrderBy(ch => ch.OrderIndex))
                        .ThenInclude(ch => ch.Lessons.OrderBy(l => l.OrderIndex))
                            .ThenInclude(l => l.LessonContents.OrderBy(lc => lc.OrderIndex))
                    .FirstOrDefaultAsync(c => c.Slug == courseSlug);

                if (_currentCourse == null)
                {
                    MessageBox.Show("Không tìm th?y khóa h?c!", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Load specific lesson
                _currentLesson = _currentCourse.CourseChapters
                    .SelectMany(ch => ch.Lessons)
                    .FirstOrDefault(l => l.LessonId == lessonId);

                if (_currentLesson == null)
                {
                    MessageBox.Show("Không tìm th?y bài h?c!", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _currentContents = _currentLesson.LessonContents.OrderBy(lc => lc.OrderIndex).ToList();

                // Update UI
                lblCourseTitle.Text = _currentCourse.Title;
                await LoadSidebarAsync();
                await UpdateProgressAsync();
                await LoadContentAsync(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i t?i bài h?c: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadSidebarAsync()
        {
            flowLessons.Controls.Clear();

            foreach (var chapter in _currentCourse.CourseChapters.OrderBy(ch => ch.OrderIndex))
            {
                // Chapter header
                var lblChapter = new Label
                {
                    Text = chapter.Title,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = ColorPalette.Primary,
                    AutoSize = true,
                    Padding = new Padding(10, 15, 10, 5),
                    Width = 320
                };
                flowLessons.Controls.Add(lblChapter);

                foreach (var lesson in chapter.Lessons.OrderBy(l => l.OrderIndex))
                {
                    var isCompleted = await IsLessonCompletedAsync(lesson.LessonId);
                    var isCurrent = lesson.LessonId == _currentLesson.LessonId;

                    var pnlLesson = CreateLessonItem(lesson, isCompleted, isCurrent);
                    flowLessons.Controls.Add(pnlLesson);
                }
            }

            // Add separator
            var separator = new Panel
            {
                Height = 2,
                Width = 320,
                BackColor = Color.FromArgb(230, 230, 230),
                Margin = new Padding(10, 20, 10, 10)
            };
            flowLessons.Controls.Add(separator);

            // Add "Related Content" section
            await LoadRelatedContentAsync();
        }

        private async Task LoadRelatedContentAsync()
        {
            var lblRelatedTitle = new Label
            {
                Text = "Nội dung khác",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary,
                AutoSize = true,
                Padding = new Padding(10, 10, 10, 10),
                Width = 320
            };
            flowLessons.Controls.Add(lblRelatedTitle);

            using var context = new LearningPlatformContext();

            // Get related lessons from the same category
            var relatedCourses = await context.Courses
                .Include(c => c.CourseChapters.Take(1))
                    .ThenInclude(ch => ch.Lessons.Take(1))
                .Where(c => c.CategoryId == _currentCourse.CategoryId &&
                           c.CourseId != _currentCourse.CourseId &&
                           c.IsPublished)
                .OrderByDescending(c => c.AverageRating)
                .Take(3)
                .ToListAsync();

            foreach (var course in relatedCourses)
            {
                var relatedCard = CreateRelatedCourseCard(course);
                flowLessons.Controls.Add(relatedCard);
            }
        }

        private Panel CreateRelatedCourseCard(Course course)
        {
            var panel = new Panel
            {
                Width = 320,
                Height = 120,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10, 5, 10, 5),
                Cursor = Cursors.Hand
            };

            // Icon/Image placeholder
            var lblIcon = new Label
            {
                Text = "📚",
                Location = new Point(10, 10),
                Size = new Size(40, 40),
                Font = new Font("Segoe UI", 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Course title
            var lblTitle = new Label
            {
                Text = course.Title,
                Location = new Point(60, 10),
                Size = new Size(240, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };

            // Rating
            var lblRating = new Label
            {
                Text = $"⭐ {course.AverageRating:F1} ({course.TotalReviews} đánh giá)",
                Location = new Point(60, 55),
                Size = new Size(240, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = ColorPalette.TextSecondary
            };

            // Button
            var btnView = new Button
            {
                Text = "Xem khóa học",
                Location = new Point(60, 80),
                Size = new Size(240, 30),
                BackColor = ColorPalette.ButtonSecondary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += (s, e) => OnRelatedCourseClick(course);

            panel.Controls.AddRange(new Control[] { lblIcon, lblTitle, lblRating, btnView });
            panel.Click += (s, e) => OnRelatedCourseClick(course);
            lblTitle.Click += (s, e) => OnRelatedCourseClick(course);

            return panel;
        }

        private void OnRelatedCourseClick(Course course)
        {
            var result = MessageBox.Show(
                $"Bạn có muốn chuyển sang khóa học:\n\n{course.Title}?",
                "Chuyển khóa học",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Navigate to course detail or first lesson
                using var context = new LearningPlatformContext();
                var firstLesson = context.Lessons
                    .Where(l => l.Chapter.CourseId == course.CourseId)
                    .OrderBy(l => l.Chapter.OrderIndex)
                    .ThenBy(l => l.OrderIndex)
                    .FirstOrDefault();

                if (firstLesson != null)
                {
                    _ = LoadLessonAsync(course.Slug, firstLesson.LessonId);
                }
            }
        }

        private async Task<bool> IsLessonCompletedAsync(int lessonId)
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return false;

            var lessonContentIds = await context.LessonContents
                .Where(lc => lc.LessonId == lessonId)
                .Select(lc => lc.ContentId)
                .ToListAsync();

            var completedCount = await context.CourseProgresses
                .Where(cp => cp.UserId == userId.Value &&
                            cp.CourseId == _currentCourse.CourseId &&
                            lessonContentIds.Contains(cp.ContentId) &&
                            cp.IsCompleted)
                .CountAsync();

            return completedCount == lessonContentIds.Count;
        }

        private async Task UpdateProgressAsync()
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return;

            var totalContents = await context.LessonContents
                .Where(lc => _currentCourse.CourseChapters
                    .SelectMany(ch => ch.Lessons)
                    .Select(l => l.LessonId)
                    .Contains(lc.LessonId))
                .CountAsync();

            var completedContents = await context.CourseProgresses
                .Where(cp => cp.UserId == userId.Value &&
                            cp.CourseId == _currentCourse.CourseId &&
                            cp.IsCompleted)
                .CountAsync();

            var progress = totalContents > 0 ? (int)((double)completedContents / totalContents * 100) : 0;
            progressBar.Value = progress;
            lblProgress.Text = $"Ti?n ??: {progress}% ({completedContents}/{totalContents} hoàn thành)";
        }

        private async Task LoadContentAsync(int contentIndex)
        {
            if (contentIndex < 0 || contentIndex >= _currentContents.Count)
                return;

            _currentContentIndex = contentIndex;
            var content = _currentContents[contentIndex];

            // Hide all content panels
            pnlVideo.Visible = false;
            pnlTheory.Visible = false;
            pnlFlashcard.Visible = false;
            pnlTest.Visible = false;

            switch (content.ContentType)
            {
                case "Video":
                    await LoadVideoContentAsync(content);
                    break;
                case "Theory":
                    await LoadTheoryContentAsync(content);
                    break;
                case "FlashcardSet":
                    await LoadFlashcardContentAsync(content);
                    break;
                case "Test":
                    await LoadTestContentAsync(content);
                    break;
            }
        }

        #region Video Content

        private async Task LoadVideoContentAsync(LessonContent content)
        {
            pnlVideo.Visible = true;

            if (!string.IsNullOrEmpty(content.VideoUrl))
            {
                var fullPath = System.IO.Path.Combine(Application.StartupPath, "wwwroot", content.VideoUrl.TrimStart('/'));
                // TODO: Setup Windows Media Player
                // videoPlayer.URL = fullPath;
                _totalWatchedSeconds = await GetWatchedDurationAsync(content.ContentId);
            }
        }

        /*
        private void VideoPlayer_PlayStateChange(object sender, dynamic e)
        {
            // WMPLib.WMPPlayState.wmppsPlaying = 3
            if (e.newState == 3)
            {
                _videoProgressTimer.Start();
            }
            else
            {
                _videoProgressTimer.Stop();
            }
        }
        */

        private async void VideoProgressTimer_Tick(object sender, EventArgs e)
        {
            // TODO: Check video player state when WMP is configured
            /*
            if (videoPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                _totalWatchedSeconds += 5;
                await SaveVideoProgressAsync();
            }
            */
            _totalWatchedSeconds += 5;
            await SaveVideoProgressAsync();
        }

        private async Task SaveVideoProgressAsync()
        {
            try
            {
                var content = _currentContents[_currentContentIndex];
                var userId = AuthHelper.CurrentUser?.UserId;
                if (!userId.HasValue) return;

                using var context = new LearningPlatformContext();

                var progress = await context.CourseProgresses
                    .FirstOrDefaultAsync(cp =>
                        cp.UserId == userId.Value &&
                        cp.CourseId == _currentCourse.CourseId &&
                        cp.ContentId == content.ContentId);

                // TODO: Get actual duration from video player
                var totalDuration = 600; // Default 10 minutes
                // var totalDuration = (int)videoPlayer.currentMedia.duration;
                var isCompleted = _totalWatchedSeconds >= totalDuration * 0.9; // 90% completion

                if (progress == null)
                {
                    progress = new CourseProgress
                    {
                        UserId = userId.Value,
                        CourseId = _currentCourse.CourseId,
                        LessonId = _currentLesson.LessonId,
                        ContentType = content.ContentType,
                        ContentId = content.ContentId,
                        IsCompleted = isCompleted,
                        DurationSec = _totalWatchedSeconds,
                        LastViewedAt = DateTime.UtcNow
                    };
                    context.CourseProgresses.Add(progress);
                }
                else
                {
                    progress.DurationSec = _totalWatchedSeconds;
                    progress.IsCompleted = isCompleted;
                    progress.LastViewedAt = DateTime.UtcNow;
                    if (isCompleted && !progress.CompletionAt.HasValue)
                    {
                        progress.CompletionAt = DateTime.UtcNow;
                    }
                }

                await context.SaveChangesAsync();
                await UpdateProgressAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving video progress: {ex.Message}");
            }
        }

        private async Task<int> GetWatchedDurationAsync(int contentId)
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return 0;

            var progress = await context.CourseProgresses
                .FirstOrDefaultAsync(cp =>
                    cp.UserId == userId.Value &&
                    cp.ContentId == contentId);

            return progress?.DurationSec ?? 0;
        }

        #endregion

        #region Theory Content

        private async Task LoadTheoryContentAsync(LessonContent content)
        {
            pnlTheory.Visible = true;

            var html = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <style>
                        body {{ font-family: 'Segoe UI', Arial, sans-serif; padding: 20px; line-height: 1.6; }}
                        h1, h2, h3 {{ color: #3490dc; }}
                        code {{ background: #f4f4f4; padding: 2px 6px; border-radius: 3px; }}
                        pre {{ background: #f4f4f4; padding: 15px; border-radius: 5px; overflow-x: auto; }}
                    </style>
                </head>
                <body>
                    <h2>{content.Title}</h2>
                    {content.Body}
                </body>
                </html>";

            webBrowser.DocumentText = html;

            // Mark as viewed
            await MarkContentViewedAsync(content.ContentId);
        }

        private async Task MarkContentViewedAsync(int contentId)
        {
            try
            {
                var userId = AuthHelper.CurrentUser?.UserId;
                if (!userId.HasValue) return;

                using var context = new LearningPlatformContext();

                var progress = await context.CourseProgresses
                    .FirstOrDefaultAsync(cp =>
                        cp.UserId == userId.Value &&
                        cp.ContentId == contentId);

                if (progress == null)
                {
                    var content = _currentContents.First(c => c.ContentId == contentId);
                    progress = new CourseProgress
                    {
                        UserId = userId.Value,
                        CourseId = _currentCourse.CourseId,
                        LessonId = _currentLesson.LessonId,
                        ContentType = content.ContentType,
                        ContentId = contentId,
                        IsCompleted = true,
                        CompletionAt = DateTime.UtcNow,
                        LastViewedAt = DateTime.UtcNow
                    };
                    context.CourseProgresses.Add(progress);
                }
                else if (!progress.IsCompleted)
                {
                    progress.IsCompleted = true;
                    progress.CompletionAt = DateTime.UtcNow;
                    progress.LastViewedAt = DateTime.UtcNow;
                }

                await context.SaveChangesAsync();
                await UpdateProgressAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking content viewed: {ex.Message}");
            }
        }

        #endregion

        #region Flashcard Content

        private async Task LoadFlashcardContentAsync(LessonContent content)
        {
            pnlFlashcard.Visible = true;

            if (!content.RefId.HasValue) return;

            using var context = new LearningPlatformContext();
            _flashcards = await context.Flashcards
                .Where(f => f.SetId == content.RefId.Value)
                .OrderBy(f => f.OrderIndex)
                .ToListAsync();

            _currentFlashcardIndex = 0;
            _isFlipped = false;
            ShowFlashcard();
        }

        private void ShowFlashcard()
        {
            if (_flashcards == null || _flashcards.Count == 0) return;

            var card = _flashcards[_currentFlashcardIndex];

            lblFlashcardFront.Text = card.FrontText;
            lblFlashcardBack.Text = card.BackText;

            lblFlashcardFront.Visible = !_isFlipped;
            lblFlashcardBack.Visible = _isFlipped;

            btnPrevCard.Enabled = _currentFlashcardIndex > 0;
            btnNextCard.Enabled = _currentFlashcardIndex < _flashcards.Count - 1;

            // Show complete button on last card
            btnCompleteFlashcard.Visible = _currentFlashcardIndex == _flashcards.Count - 1 && _isFlipped;
        }

        private void BtnFlipCard_Click(object sender, EventArgs e)
        {
            _isFlipped = !_isFlipped;
            ShowFlashcard();
        }

        private void BtnPrevCard_Click(object sender, EventArgs e)
        {
            if (_currentFlashcardIndex > 0)
            {
                _currentFlashcardIndex--;
                _isFlipped = false;
                ShowFlashcard();
            }
        }

        private void BtnNextCard_Click(object sender, EventArgs e)
        {
            if (_currentFlashcardIndex < _flashcards.Count - 1)
            {
                _currentFlashcardIndex++;
                _isFlipped = false;
                ShowFlashcard();
            }
        }

        private async void BtnCompleteFlashcard_Click(object sender, EventArgs e)
        {
            var content = _currentContents[_currentContentIndex];
            await MarkContentCompleteAsync(content.ContentId);
            MessageBox.Show("?ã hoàn thành luy?n t?p flashcard!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Test Content

        private async Task LoadTestContentAsync(LessonContent content)
        {
            pnlTest.Visible = true;

            if (!content.RefId.HasValue) return;

            using var context = new LearningPlatformContext();
            _currentTest = await context.Tests
                .Include(t => t.Questions.OrderBy(q => q.OrderIndex))
                    .ThenInclude(q => q.QuestionOptions.OrderBy(o => o.OrderIndex))
                .FirstOrDefaultAsync(t => t.TestId == content.RefId.Value);

            if (_currentTest == null) return;

            lblTestTitle.Text = _currentTest.Title;
            _questions = _currentTest.Questions.OrderBy(q => q.OrderIndex).ToList();
            _selectedAnswers.Clear();
            _testStartTime = DateTime.UtcNow;

            LoadQuestions();
        }

        private void LoadQuestions()
        {
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
                Width = 750,
                AutoSize = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(10),
                Padding = new Padding(15)
            };

            var lblQuestion = new Label
            {
                Text = $"Câu {number}: {question.StemText}",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true,
                Width = 720,
                Location = new Point(0, 0)
            };

            panel.Controls.Add(lblQuestion);
            int yPos = lblQuestion.Height + 10;

            if (question.Type == "MCQ_Single" || question.Type == "MCQ_Multi")
            {
                foreach (var option in question.QuestionOptions.OrderBy(o => o.OrderIndex))
                {
                    RadioButton radio = new RadioButton
                    {
                        Text = option.OptionText,
                        Font = new Font("Segoe UI", 10),
                        AutoSize = true,
                        Width = 720,
                        Location = new Point(20, yPos),
                        Tag = option.OptionId
                    };
                    radio.CheckedChanged += (s, e) =>
                    {
                        if (radio.Checked)
                        {
                            _selectedAnswers[question.QuestionId] = option.OptionId;
                        }
                    };
                    panel.Controls.Add(radio);
                    yPos += radio.Height + 5;
                }
            }
            else if (question.Type == "TrueFalse")
            {
                var trueOption = question.QuestionOptions.FirstOrDefault(o => o.OptionText.Contains("?úng"));
                var falseOption = question.QuestionOptions.FirstOrDefault(o => o.OptionText.Contains("Sai"));

                var radioTrue = new RadioButton
                {
                    Text = "?úng",
                    Location = new Point(20, yPos),
                    Tag = trueOption?.OptionId
                };
                radioTrue.CheckedChanged += (s, e) =>
                {
                    if (radioTrue.Checked && trueOption != null)
                    {
                        _selectedAnswers[question.QuestionId] = trueOption.OptionId;
                    }
                };

                var radioFalse = new RadioButton
                {
                    Text = "Sai",
                    Location = new Point(120, yPos),
                    Tag = falseOption?.OptionId
                };
                radioFalse.CheckedChanged += (s, e) =>
                {
                    if (radioFalse.Checked && falseOption != null)
                    {
                        _selectedAnswers[question.QuestionId] = falseOption.OptionId;
                    }
                };

                panel.Controls.AddRange(new Control[] { radioTrue, radioFalse });
            }

            return panel;
        }

        private async void BtnSubmitTest_Click(object sender, EventArgs e)
        {
            if (_selectedAnswers.Count < _questions.Count)
            {
                var result = MessageBox.Show(
                    $"B?n ch? tr? l?i {_selectedAnswers.Count}/{_questions.Count} câu. B?n có ch?c mu?n n?p bài?",
                    "Xác nh?n",
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
                var userId = AuthHelper.CurrentUser?.UserId;
                if (!userId.HasValue) return;

                using var context = new LearningPlatformContext();

                var timeSpent = (int)(DateTime.UtcNow - _testStartTime).TotalSeconds;

                // Calculate score
                decimal totalScore = 0;
                decimal maxScore = _currentTest.MaxScore ?? _questions.Sum(q => q.Points);

                foreach (var question in _questions)
                {
                    if (_selectedAnswers.TryGetValue(question.QuestionId, out int? selectedOptionId) && selectedOptionId.HasValue)
                    {
                        var option = question.QuestionOptions.FirstOrDefault(o => o.OptionId == selectedOptionId.Value);
                        if (option != null && option.IsCorrect)
                        {
                            totalScore += question.Points;
                        }
                    }
                }

                // Create test attempt
                var attempt = new TestAttempt
                {
                    TestId = _currentTest.TestId,
                    UserId = userId.Value,
                    StartedAt = _testStartTime,
                    SubmittedAt = DateTime.UtcNow,
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
                    if (kvp.Value.HasValue)
                    {
                        var question = _questions.First(q => q.QuestionId == kvp.Key);
                        var option = question.QuestionOptions.First(o => o.OptionId == kvp.Value.Value);

                        var answer = new AttemptAnswer
                        {
                            AttemptId = attempt.AttemptId,
                            QuestionId = kvp.Key,
                            AnswerPayload = $"{{\"optionId\": {kvp.Value.Value}}}",
                            IsCorrect = option.IsCorrect,
                            Score = option.IsCorrect ? question.Points : 0,
                            AutoGraded = true,
                            GradedAt = DateTime.UtcNow
                        };

                        context.AttemptAnswers.Add(answer);
                    }
                }

                await context.SaveChangesAsync();

                // Mark content as complete
                var content = _currentContents[_currentContentIndex];
                await MarkContentCompleteAsync(content.ContentId, totalScore);

                // Show result
                var percentage = (totalScore / maxScore) * 100;
                MessageBox.Show(
                    $"K?t qu? bài ki?m tra:\n\n" +
                    $"?i?m: {totalScore}/{maxScore} ({percentage:F1}%)\n" +
                    $"Th?i gian: {timeSpent / 60} phút {timeSpent % 60} giây\n" +
                    $"S? câu ?úng: {_selectedAnswers.Count(a => _questions.First(q => q.QuestionId == a.Key).QuestionOptions.Any(o => o.OptionId == a.Value && o.IsCorrect))}/{_questions.Count}",
                    "K?t qu?",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i n?p bài: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Navigation

        private async void BtnPrevLesson_Click(object sender, EventArgs e)
        {
            var allLessons = _currentCourse.CourseChapters
                .OrderBy(ch => ch.OrderIndex)
                .SelectMany(ch => ch.Lessons.OrderBy(l => l.OrderIndex))
                .ToList();

            var currentIndex = allLessons.FindIndex(l => l.LessonId == _currentLesson.LessonId);
            if (currentIndex > 0)
            {
                await LoadLessonAsync(_currentCourse.Slug, allLessons[currentIndex - 1].LessonId);
            }
        }

        private async void BtnNextLesson_Click(object sender, EventArgs e)
        {
            var allLessons = _currentCourse.CourseChapters
                .OrderBy(ch => ch.OrderIndex)
                .SelectMany(ch => ch.Lessons.OrderBy(l => l.OrderIndex))
                .ToList();

            var currentIndex = allLessons.FindIndex(l => l.LessonId == _currentLesson.LessonId);
            if (currentIndex < allLessons.Count - 1)
            {
                await LoadLessonAsync(_currentCourse.Slug, allLessons[currentIndex + 1].LessonId);
            }
        }

        private async void BtnMarkComplete_Click(object sender, EventArgs e)
        {
            var content = _currentContents[_currentContentIndex];
            await MarkContentCompleteAsync(content.ContentId);
            MessageBox.Show("?ã ?ánh d?u hoàn thành!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task MarkContentCompleteAsync(int contentId, decimal? score = null)
        {
            try
            {
                var userId = AuthHelper.CurrentUser?.UserId;
                if (!userId.HasValue) return;

                using var context = new LearningPlatformContext();

                var progress = await context.CourseProgresses
                    .FirstOrDefaultAsync(cp =>
                        cp.UserId == userId.Value &&
                        cp.ContentId == contentId);

                var content = _currentContents.First(c => c.ContentId == contentId);

                if (progress == null)
                {
                    progress = new CourseProgress
                    {
                        UserId = userId.Value,
                        CourseId = _currentCourse.CourseId,
                        LessonId = _currentLesson.LessonId,
                        ContentType = content.ContentType,
                        ContentId = contentId,
                        IsCompleted = true,
                        CompletionAt = DateTime.UtcNow,
                        LastViewedAt = DateTime.UtcNow,
                        Score = score
                    };
                    context.CourseProgresses.Add(progress);
                }
                else if (!progress.IsCompleted)
                {
                    progress.IsCompleted = true;
                    progress.CompletionAt = DateTime.UtcNow;
                    progress.Score = score;
                }

                await context.SaveChangesAsync();
                await UpdateProgressAsync();
                await LoadSidebarAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking content complete: {ex.Message}");
            }
        }

        #endregion

        private Panel CreateLessonItem(Lesson lesson, bool isCompleted, bool isCurrent)
        {
            var panel = new Panel
            {
                Width = 320,
                Height = 80,
                BackColor = isCurrent ? Color.FromArgb(225, 239, 254) : Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Cursor = Cursors.Hand,
                Padding = new Padding(10)
            };

            // Status icon (left side)
            var lblStatus = new Label
            {
                Text = isCompleted ? "✓" : "○",
                Location = new Point(15, 15),
                Size = new Size(30, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = isCompleted ? ColorPalette.Success : ColorPalette.TextSecondary,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Content type icon
            var contentIcon = GetContentTypeIcon(lesson);
            var lblContentIcon = new Label
            {
                Text = contentIcon.Icon,
                Location = new Point(15, 50),
                Size = new Size(30, 20),
                Font = new Font("Segoe UI", 10),
                ForeColor = contentIcon.Color,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Lesson title
            var lblTitle = new Label
            {
                Text = lesson.Title,
                Location = new Point(55, 15),
                Size = new Size(250, 35),
                Font = new Font("Segoe UI", 10, isCurrent ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = isCurrent ? ColorPalette.Primary : ColorPalette.TextPrimary
            };

            // Content type label
            var lblContentType = new Label
            {
                Text = contentIcon.Label,
                Location = new Point(55, 50),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = ColorPalette.TextSecondary
            };

            // Duration/Info label (if available)
            var lblDuration = new Label
            {
                Text = GetLessonDuration(lesson),
                Location = new Point(210, 50),
                Size = new Size(95, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = ColorPalette.TextSecondary,
                TextAlign = ContentAlignment.MiddleRight
            };

            panel.Controls.AddRange(new Control[] {
                lblStatus, lblContentIcon, lblTitle, lblContentType, lblDuration
            });

            panel.Click += async (s, e) => await LoadLessonAsync(_currentCourse.Slug, lesson.LessonId);
            lblTitle.Click += async (s, e) => await LoadLessonAsync(_currentCourse.Slug, lesson.LessonId);

            return panel;
        }

        private (string Icon, Color Color, string Label) GetContentTypeIcon(Lesson lesson)
        {
            // Get first content type
            var firstContent = lesson.LessonContents.OrderBy(lc => lc.OrderIndex).FirstOrDefault();

            if (firstContent == null)
                return ("📄", ColorPalette.TextSecondary, "Nội dung");

            return firstContent.ContentType switch
            {
                "Video" => ("▶️", Color.FromArgb(220, 53, 69), "Video"),
                "Theory" => ("📖", Color.FromArgb(52, 144, 220), "Lý thuyết"),
                "FlashcardSet" => ("🗂️", Color.FromArgb(255, 193, 7), "Flashcard"),
                "Test" => ("✍️", Color.FromArgb(40, 167, 69), "Kiểm tra"),
                _ => ("📄", ColorPalette.TextSecondary, "Nội dung")
            };
        }

        private string GetLessonDuration(Lesson lesson)
        {
            // Calculate total duration based on content
            var totalContents = lesson.LessonContents.Count;

            if (totalContents == 0)
                return "";

            if (totalContents == 1)
            {
                var content = lesson.LessonContents.First();
                return content.ContentType switch
                {
                    "Video" => "~10 phút",
                    "Theory" => "~5 phút",
                    "FlashcardSet" => "~15 phút",
                    "Test" => "~20 phút",
                    _ => ""
                };
            }

            return $"{totalContents} nội dung";
        }

        private void lblCourseTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
