using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    public partial class ContentTestControl : UserControl, IContentControl
    {
        public event Action<object, string>? ContentTypeChanged;
        public event Action<object>? DeleteRequested;

        private List<TestQuestionItemControl> _questions = new List<TestQuestionItemControl>();

        // preserve ids from existing LessonContent
        private int? _contentId;
        private int? _refId;

        // inline error labels
        private Label lblTitleError;
        private Label lblTimeError;
        private Label lblMaxAttemptsError;

        public ContentTestControl()
        {
            this.Width = 835; this.Height = 680; this.Margin = new Padding(0, 0, 0, 10);
            this.BorderStyle = BorderStyle.FixedSingle;
            InitializeComponent();

            // create inline error labels
            lblTitleError = new Label { ForeColor = Color.Red, AutoSize = true, Visible = false };
            lblTimeError = new Label { ForeColor = Color.Red, AutoSize = true, Visible = false };
            lblMaxAttemptsError = new Label { ForeColor = Color.Red, AutoSize = true, Visible = false };

            // position error labels relative to existing controls (safe defaults)
            // Title error below txtTitle
            lblTitleError.Location = new Point(txtTitle.Left, txtTitle.Bottom + 2);
            // Time error to the right of numTime
            lblTimeError.Location = new Point(numTime.Right + 8, numTime.Top);
            // Max attempts error to the right of numMaxAttempts
            lblMaxAttemptsError.Location = new Point(numMaxAttempts.Right + 8, numMaxAttempts.Top);

            this.Controls.Add(lblTitleError);
            this.Controls.Add(lblTimeError);
            this.Controls.Add(lblMaxAttemptsError);

            // Add delete button (big red)
            btnDeleteContent.Click += (s, e) => DeleteRequested?.Invoke(this);

            // Set default selection and add event handler
            cboContentType.SelectedIndex = 3; // Test
            cboContentType.SelectedIndexChanged += (s, e) => {
                var type = cboContentType.SelectedItem?.ToString();
                if (type != null && type != "Test")
                {
                    ContentTypeChanged?.Invoke(this, type);
                }
            };

            btnAddQuestion.Click += (s, e) => AddQuestion();
            
            // Style labels
            lblTitle.Font = new Font(lblTitle.Font.FontFamily, 10, FontStyle.Bold);
            lblInfoDesc.Font = new Font(lblInfoDesc.Font.FontFamily, 10, FontStyle.Bold);
            lblTime.Font = new Font(lblTime.Font.FontFamily, 9);
            lblMaxAttempts.Font = new Font(lblMaxAttempts.Font.FontFamily, 9);

            // Use txtTitle as test title source
            lblTitle.Text = "Loại nội dung";

            // Disable internal scrolling — we'll expand the panel instead
            pnlQuestions.AutoScroll = false;

            // adjust questions width when panel resizes
            pnlQuestions.Resize += (s, e) => {
                foreach (var q in _questions)
                {
                    q.Width = Math.Max(0, pnlQuestions.ClientSize.Width);
                }
                RearrangeQuestions();
                AdjustContainerSize();
            };

            // hide inline errors when user edits fields
            txtTitle.TextChanged += (s, e) => lblTitleError.Visible = false;
            numTime.ValueChanged += (s, e) => lblTimeError.Visible = false;
            numMaxAttempts.ValueChanged += (s, e) => lblMaxAttemptsError.Visible = false;

            AddQuestion();
        }

        // Return the created question control so caller can populate it
        private TestQuestionItemControl AddQuestion()
        {
            int number = _questions.Count + 1;
            var q = new TestQuestionItemControl(number);
            q.Width = pnlQuestions.ClientSize.Width;
            q.Location = new Point(0, _questions.Count * (q.Height + 8));
            q.DeleteRequested += (o) => RemoveQuestion(q);
            pnlQuestions.Controls.Add(q);
            _questions.Add(q);
            RearrangeQuestions();
            AdjustContainerSize();
            return q;
        }

        private void RemoveQuestion(TestQuestionItemControl q)
        {
            pnlQuestions.Controls.Remove(q);
            _questions.Remove(q);
            RearrangeQuestions();
            AdjustContainerSize();
        }

        private void RearrangeQuestions()
        {
            for (int i = 0; i < _questions.Count; i++)
            {
                var q = _questions[i];
                q.Location = new Point(0, i * (q.Height + 8));
                q.SetNumber(i + 1);
                q.Width = pnlQuestions.ClientSize.Width;
            }
        }

        private void AdjustContainerSize()
        {
            int total = 0;
            foreach (var it in _questions)
            {
                total += it.Height + 8;
            }
            total += 10; // padding

            int minHeight = 120;
            pnlQuestions.Height = Math.Max(minHeight, total);

            int desired = pnlQuestions.Location.Y + pnlQuestions.Height + btnAddQuestion.Height + 20;
            if (this.Height < desired) this.Height = desired;
            this.Parent?.PerformLayout();
        }

        public void LoadFromViewModel(LessonContentBuilderViewModel vm)
        {
            if (vm == null) return;

            // preserve ids
            _contentId = vm.ContentId;
            _refId = vm.RefId;

            // Title and test title sourced from txtTitle
            txtTitle.Text = vm.Title ?? vm.TestTitle ?? string.Empty;

            txtInfoDesc.Text = vm.TestDesc ?? string.Empty;
            numTime.Value = vm.TimeLimitMinutes.HasValue ? vm.TimeLimitMinutes.Value : numTime.Value;
            numMaxAttempts.Value = vm.MaxAttempts.HasValue ? vm.MaxAttempts.Value : numMaxAttempts.Value;

            foreach (var q in _questions.ToArray()) RemoveQuestion(q);

            if (vm.Questions != null && vm.Questions.Count > 0)
            {
                foreach (var tq in vm.Questions)
                {
                    var qctl = AddQuestion();
                    // populate fields from VM into the control
                    qctl.PopulateFrom(tq);
                }
            }
            else if (_questions.Count == 0)
            {
                AddQuestion();
            }
            else
            {
                AdjustContainerSize();
            }
        }

        public LessonContentBuilderViewModel SaveToViewModel()
        {
            Debug.WriteLine($"[ContentTestControl] SaveToViewModel called. Questions count: {_questions.Count}");

            // clear previous inline errors
            lblTitleError.Visible = false;
            lblTimeError.Visible = false;
            lblMaxAttemptsError.Visible = false;

            // Validation
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                lblTitleError.Text = "Tiêu đề bài kiểm tra không được để trống.";
                lblTitleError.Visible = true;
                throw new InvalidOperationException(lblTitleError.Text);
            }

            if (numTime.Value <= 0)
            {
                lblTimeError.Text = "Thời gian phải lớn hơn 0 phút.";
                lblTimeError.Visible = true;
                throw new InvalidOperationException(lblTimeError.Text);
            }

            if (numMaxAttempts.Value <= 0)
            {
                lblMaxAttemptsError.Text = "Số lần làm bài phải lớn hơn 0.";
                lblMaxAttemptsError.Visible = true;
                throw new InvalidOperationException(lblMaxAttemptsError.Text);
            }
            
            var vm = new LessonContentBuilderViewModel
            {
                ContentId = _contentId,
                RefId = _refId,
                ContentType = "Test",
                Title = txtTitle.Text.Trim(),
                // Use txtTitle as test title as well
                TestTitle = txtTitle.Text.Trim(),
                TestDesc = txtInfoDesc.Text.Trim(),
                TimeLimitMinutes = (int)numTime.Value,
                MaxAttempts = (int)numMaxAttempts.Value,
                Questions = new List<TestQuestionBuilderViewModel>()
            };

            Debug.WriteLine($"[ContentTestControl] Title: '{vm.Title}', TestTitle: '{vm.TestTitle}', RefId: {vm.RefId}");

            for (int i = 0; i < _questions.Count; i++)
            {
                var q = _questions[i];
                
                // Validate question text
                if (string.IsNullOrWhiteSpace(q.QuestionText))
                {
                    ToastHelper.Show(this, $"Câu hỏi {i+1} không được để trống.");
                    throw new InvalidOperationException();
                }
                
                var answers = q.GetAnswers().ToList();
                
                // Validate has answers
                if (answers.Count == 0)
                {
                    ToastHelper.Show(this, $"Câu hỏi {i+1} phải có ít nhất một đáp án.");
                    throw new InvalidOperationException();
                }
                
                // Validate answer text not empty
                for (int j = 0; j < answers.Count; j++)
                {
                    if (string.IsNullOrWhiteSpace(answers[j].AnswerText))
                    {
                        ToastHelper.Show(this, $"Câu hỏi {i+1}: Đáp án {j+1} không được để trống.");
                        throw new InvalidOperationException();
                    }
                }
                
                // Validate has correct answer
                if (!answers.Any(a => a.IsCorrect))
                {
                    ToastHelper.Show(this, $"Câu hỏi {i+1} phải có ít nhất một đáp án đúng.");
                    throw new InvalidOperationException();
                }
                
                var tq = new TestQuestionBuilderViewModel
                {
                    Type = q.QuestionTypeIndex == 0 ? "MCQ_Single" : q.QuestionTypeIndex == 1 ? "MCQ_Multi" : "TrueFalse",
                    StemText = q.QuestionText ?? string.Empty,
                    Points = (decimal)q.Point,
                    OrderIndex = i + 1,
                    Options = new List<TestQuestionOptionBuilderViewModel>()
                };

                Debug.WriteLine($"[ContentTestControl] Question {i+1}: Type={tq.Type}, StemText='{tq.StemText}', Points={tq.Points}");

                int optIndex = 1;
                foreach (var a in answers)
                {
                    tq.Options.Add(new TestQuestionOptionBuilderViewModel
                    {
                        OptionText = a.AnswerText ?? string.Empty,
                        IsCorrect = a.IsCorrect,
                        OrderIndex = optIndex++
                    });
                    Debug.WriteLine($"[ContentTestControl]   Option {optIndex-1}: Text='{a.AnswerText}', IsCorrect={a.IsCorrect}");
                }

                vm.Questions.Add(tq);
            }

            Debug.WriteLine($"[ContentTestControl] Final VM has {vm.Questions.Count} questions");
            return vm;
        }
    }
}