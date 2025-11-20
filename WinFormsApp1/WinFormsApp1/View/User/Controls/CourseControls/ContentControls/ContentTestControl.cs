using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
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

        public ContentTestControl()
        {
            this.Width = 835; this.Height = 680; this.Margin = new Padding(0, 0, 0, 10);
            this.BorderStyle = BorderStyle.FixedSingle;
            InitializeComponent();

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
            lblInfoTitle.Font = new Font(lblInfoTitle.Font.FontFamily, 10, FontStyle.Bold);
            lblInfoDesc.Font = new Font(lblInfoDesc.Font.FontFamily, 10, FontStyle.Bold);
            lblTime.Font = new Font(lblTime.Font.FontFamily, 9);
            lblMaxAttempts.Font = new Font(lblMaxAttempts.Font.FontFamily, 9);

            AddQuestion();
        }

        // Return the created question control so caller can populate it
        private TestQuestionItemControl AddQuestion()
        {
            int number = _questions.Count + 1;
            var q = new TestQuestionItemControl(number);
            q.Width = pnlQuestions.Width - SystemInformation.VerticalScrollBarWidth - 4;
            q.Location = new Point(0, _questions.Count * (q.Height + 8));
            q.DeleteRequested += (o) => RemoveQuestion(q);
            pnlQuestions.Controls.Add(q);
            _questions.Add(q);
            RearrangeQuestions();
            return q;
        }

        private void RemoveQuestion(TestQuestionItemControl q)
        {
            pnlQuestions.Controls.Remove(q);
            _questions.Remove(q);
            RearrangeQuestions();
        }

        private void RearrangeQuestions()
        {
            for (int i = 0; i < _questions.Count; i++)
            {
                var q = _questions[i];
                q.Location = new Point(0, i * (q.Height + 8));
                q.SetNumber(i + 1);
            }
        }

        public void LoadFromViewModel(LessonContentBuilderViewModel vm)
        {
            if (vm == null) return;

            // preserve ids
            _contentId = vm.ContentId;
            _refId = vm.RefId;

            txtTitle.Text = vm.Title ?? string.Empty;
            txtInfoTitle.Text = vm.TestTitle ?? string.Empty;
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
        }

        public LessonContentBuilderViewModel SaveToViewModel()
        {
            Debug.WriteLine($"[ContentTestControl] SaveToViewModel called. Questions count: {_questions.Count}");
            
            var vm = new LessonContentBuilderViewModel
            {
                ContentId = _contentId,
                RefId = _refId,
                ContentType = "Test",
                Title = txtTitle.Text.Trim(),
                TestTitle = txtInfoTitle.Text.Trim(),
                TestDesc = txtInfoDesc.Text.Trim(),
                TimeLimitMinutes = (int)numTime.Value,
                MaxAttempts = (int)numMaxAttempts.Value,
                Questions = new List<TestQuestionBuilderViewModel>()
            };

            Debug.WriteLine($"[ContentTestControl] Title: '{vm.Title}', TestTitle: '{vm.TestTitle}', RefId: {vm.RefId}");

            for (int i = 0; i < _questions.Count; i++)
            {
                var q = _questions[i];
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
                foreach (var a in q.GetAnswers())
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