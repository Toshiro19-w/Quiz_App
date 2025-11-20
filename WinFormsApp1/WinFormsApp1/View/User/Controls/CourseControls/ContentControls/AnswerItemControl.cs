using System;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    public partial class AnswerItemControl : UserControl
    {
        public event Action<object>? DeleteRequested;

        public AnswerItemControl()
        {
            InitializeComponent();
            btnDelete.Click += (s, e) => DeleteRequested?.Invoke(this);
        }

        public bool IsCorrect => chkCorrect.Checked;
        public string AnswerText => txtAnswer.Text;

        // Allow setting values programmatically
        public void SetAnswer(string text, bool isCorrect)
        {
            txtAnswer.Text = text ?? string.Empty;
            chkCorrect.Checked = isCorrect;
        }
    }
}
