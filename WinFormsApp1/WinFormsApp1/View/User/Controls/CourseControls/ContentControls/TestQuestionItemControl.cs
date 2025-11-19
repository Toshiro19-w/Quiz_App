using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    public partial class TestQuestionItemControl : UserControl
    {
        public event Action<object>? DeleteRequested;
        private List<AnswerItemControl> _answers = new List<AnswerItemControl>();

        public TestQuestionItemControl(int number)
        {
            InitializeComponent();
            lblQuestionNumber.Text = $"Câu hỏi {number}";
            cboQuestionType.SelectedIndex = 0;

            btnAddAnswer.Click += (s, e) => AddAnswer();
            btnDelete.Click += (s, e) => DeleteRequested?.Invoke(this);

            // Add 2 default answers
            AddAnswer();
            AddAnswer();
        }

        public void SetNumber(int number)
        {
            lblQuestionNumber.Text = $"Câu hỏi {number}";
        }

        private void AddAnswer()
        {
            var answer = new AnswerItemControl();
            answer.Width = pnlAnswers.Width - SystemInformation.VerticalScrollBarWidth - 4;
            answer.Location = new Point(0, _answers.Count * (answer.Height + 4));
            answer.DeleteRequested += (o) => RemoveAnswer(answer);
            pnlAnswers.Controls.Add(answer);
            _answers.Add(answer);
        }

        private void RemoveAnswer(AnswerItemControl answer)
        {
            if (_answers.Count <= 1) return; // Keep at least 1 answer
            pnlAnswers.Controls.Remove(answer);
            _answers.Remove(answer);
            RearrangeAnswers();
        }

        private void RearrangeAnswers()
        {
            for (int i = 0; i < _answers.Count; i++)
            {
                _answers[i].Location = new Point(0, i * (_answers[i].Height + 4));
            }
        }

        // Populate control from VM
        public void PopulateFrom(TestQuestionBuilderViewModel vm)
        {
            if (vm == null) return;

            // Map type to index
            cboQuestionType.SelectedIndex = vm.Type switch
            {
                "MCQ_Single" => 0,
                "MCQ_Multi" => 1,
                "TrueFalse" => 2,
                _ => 0
            };

            txtQuestion.Text = vm.StemText ?? string.Empty;
            numPoint.Value = (decimal)(vm.Points <= 0 ? 1 : vm.Points);

            // Clear existing answers
            foreach (var a in _answers.ToArray())
            {
                pnlAnswers.Controls.Remove(a);
                _answers.Remove(a);
                a.Dispose();
            }

            // Add answers from VM
            if (vm.Options != null && vm.Options.Count > 0)
            {
                foreach (var opt in vm.Options)
                {
                    var answer = new AnswerItemControl();
                    answer.Width = pnlAnswers.Width - SystemInformation.VerticalScrollBarWidth - 4;
                    answer.Location = new Point(0, _answers.Count * (answer.Height + 4));
                    answer.DeleteRequested += (o) => RemoveAnswer(answer);
                    answer.SetAnswer(opt.OptionText ?? string.Empty, opt.IsCorrect);
                    pnlAnswers.Controls.Add(answer);
                    _answers.Add(answer);
                }
            }
            else
            {
                // ensure at least one answer exists
                AddAnswer();
            }

            RearrangeAnswers();
        }

        public int QuestionTypeIndex => cboQuestionType.SelectedIndex;
        public string QuestionText => txtQuestion.Text;
        public double Point => (double)numPoint.Value;
        public IEnumerable<AnswerItemControl> GetAnswers() => _answers;
    }
}
