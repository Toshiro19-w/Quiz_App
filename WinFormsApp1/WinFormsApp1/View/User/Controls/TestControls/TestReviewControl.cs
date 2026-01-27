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
    public partial class TestReviewControl : UserControl
    {
        private int _attemptId;
        private Control _previousControl; // Để quay lại trang trước

        public TestReviewControl()
        {
            InitializeComponent();
            btnBack.Click += BtnBack_Click;
        }

        public void SetPreviousControl(Control previousControl)
        {
            _previousControl = previousControl;
        }

        public async Task LoadAttemptAsync(int attemptId)
        {
            _attemptId = attemptId;

            try
            {
                using var context = new LearningPlatformContext();

                var attempt = await context.TestAttempts
                    .Include(ta => ta.Test)
                    .Include(ta => ta.AttemptAnswers)
                        .ThenInclude(aa => aa.Question)
                            .ThenInclude(q => q.QuestionOptions)
                    .FirstOrDefaultAsync(ta => ta.AttemptId == attemptId);

                if (attempt == null)
                {
                    MessageBox.Show("Không tìm thấy bài làm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update header
                lblTitle.Text = $"Xem lại: {attempt.Test.Title}";
                lblScore.Text = $"Điểm: {attempt.Score}/{attempt.MaxScore}";
                
                var percentage = attempt.MaxScore > 0 ? (attempt.Score / attempt.MaxScore) * 100 : 0;
                lblScore.ForeColor = percentage >= 60 ? ColorPalette.Success : ColorPalette.Error;

                lblTimestamp.Text = $"Làm bài: {attempt.SubmittedAt?.ToString("dd/MM/yyyy HH:mm") ?? attempt.StartedAt.ToString("dd/MM/yyyy HH:mm")}";

                // Load questions and answers
                LoadQuestionsWithAnswers(attempt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải bài làm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadQuestionsWithAnswers(Models.Entities.TestAttempt attempt)
        {
            flowQuestions.Controls.Clear();

            var questions = attempt.Test.Questions.OrderBy(q => q.OrderIndex).ToList();

            for (int i = 0; i < questions.Count; i++)
            {
                var question = questions[i];
                var userAnswer = attempt.AttemptAnswers.FirstOrDefault(aa => aa.QuestionId == question.QuestionId);

                var questionPanel = CreateQuestionReviewPanel(question, userAnswer, i + 1);
                flowQuestions.Controls.Add(questionPanel);
            }
        }

        private Panel CreateQuestionReviewPanel(Models.Entities.Question question, Models.Entities.AttemptAnswer userAnswer, int number)
        {
            var panel = new Panel
            {
                Width = 1100,
                AutoSize = true,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 20),
                Padding = new Padding(20),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Question header
            var lblHeader = new Label
            {
                Text = $"Câu {number}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ColorPalette.Primary,
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Result badge
            bool isCorrect = userAnswer?.IsCorrect ?? false;
            var lblResult = new Label
            {
                Text = isCorrect ? "✓ Đúng" : "✗ Sai",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = isCorrect ? ColorPalette.Success : ColorPalette.Error,
                AutoSize = true,
                Padding = new Padding(10, 5, 10, 5),
                Location = new Point(1000, 20)
            };

            // Question text
            var lblQuestion = new Label
            {
                Text = question.StemText,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.Black,
                AutoSize = true,
                MaximumSize = new Size(1040, 0),
                Location = new Point(20, 55)
            };

            panel.Controls.Add(lblHeader);
            panel.Controls.Add(lblResult);
            panel.Controls.Add(lblQuestion);

            int yPos = lblQuestion.Bottom + 20;

            // Parse user's selected options
            var selectedOptions = ParseUserAnswer(userAnswer);

            // Display options
            foreach (var option in question.QuestionOptions.OrderBy(o => o.OrderIndex))
            {
                bool isUserSelected = selectedOptions.Contains(option.OptionId);
                bool isCorrectAnswer = option.IsCorrect;

                var optionPanel = CreateOptionPanel(option, isUserSelected, isCorrectAnswer, yPos);
                panel.Controls.Add(optionPanel);
                yPos = optionPanel.Bottom + 5;
            }

            // Score info
            var lblScoreInfo = new Label
            {
                Text = $"Điểm: {userAnswer?.Score ?? 0}/{question.Points}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.Primary,
                AutoSize = true,
                Location = new Point(20, yPos + 10)
            };
            panel.Controls.Add(lblScoreInfo);

            return panel;
        }

        private Panel CreateOptionPanel(Models.Entities.QuestionOption option, bool isUserSelected, bool isCorrectAnswer, int yPos)
        {
            var pnl = new Panel
            {
                Width = 1040,
                Height = 40,
                Location = new Point(40, yPos),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Determine background color
            if (isCorrectAnswer)
            {
                pnl.BackColor = Color.FromArgb(212, 237, 218); // Light green
            }
            else if (isUserSelected && !isCorrectAnswer)
            {
                pnl.BackColor = Color.FromArgb(248, 215, 218); // Light red
            }
            else
            {
                pnl.BackColor = Color.White;
            }

            // Icon
            string icon = "";
            Color iconColor = Color.Gray;

            if (isCorrectAnswer)
            {
                icon = "✓";
                iconColor = ColorPalette.Success;
            }
            else if (isUserSelected && !isCorrectAnswer)
            {
                icon = "✗";
                iconColor = ColorPalette.Error;
            }
            else if (isUserSelected)
            {
                icon = "●";
                iconColor = ColorPalette.Primary;
            }
            else
            {
                icon = "○";
            }

            var lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = iconColor,
                AutoSize = true,
                Location = new Point(10, 10)
            };

            var lblText = new Label
            {
                Text = option.OptionText,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Black,
                AutoSize = true,
                MaximumSize = new Size(970, 0),
                Location = new Point(35, 10)
            };

            pnl.Controls.Add(lblIcon);
            pnl.Controls.Add(lblText);

            return pnl;
        }

        private System.Collections.Generic.List<int> ParseUserAnswer(Models.Entities.AttemptAnswer userAnswer)
        {
            var result = new System.Collections.Generic.List<int>();

            if (userAnswer == null || string.IsNullOrEmpty(userAnswer.AnswerPayload))
                return result;

            try
            {
                // Parse JSON: {"selectedOptions": [1,2,3]}
                var json = System.Text.Json.JsonDocument.Parse(userAnswer.AnswerPayload);
                if (json.RootElement.TryGetProperty("selectedOptions", out var selectedArray))
                {
                    foreach (var item in selectedArray.EnumerateArray())
                    {
                        result.Add(item.GetInt32());
                    }
                }
            }
            catch
            {
                // Ignore parsing errors
            }

            return result;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                var parentPanel = this.Parent as Panel;
                if (parentPanel == null) return;

                parentPanel.Controls.Clear();

                // Quay lại TestAttemptsListControl
                if (_previousControl != null)
                {
                    _previousControl.Dock = DockStyle.Fill;
                    parentPanel.Controls.Add(_previousControl);
                }
                else
                {
                    // Fallback: remove this control
                    this.Parent?.Controls.Remove(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi quay lại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
