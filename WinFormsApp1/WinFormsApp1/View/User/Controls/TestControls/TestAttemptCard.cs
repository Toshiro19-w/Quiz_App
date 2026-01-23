using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.User.Controls.TestControls
{
    public partial class TestAttemptCard : UserControl
    {
        private int _attemptId;

        public event EventHandler<int> ViewDetailsClicked;

        public TestAttemptCard()
        {
            InitializeComponent();
            btnView.Click += BtnView_Click;
            pnlMain.Click += PnlMain_Click;
        }

        public void LoadAttempt(Models.Entities.TestAttempt attempt, int number)
        {
            _attemptId = attempt.AttemptId;

            // Set attempt number
            lblNumber.Text = $"Lần {number}";

            // Set date/time
            var dateTime = attempt.SubmittedAt ?? attempt.StartedAt;
            lblDateTime.Text = $"🕒 {dateTime:dd/MM/yyyy HH:mm}";

            // Calculate percentage
            var percentage = attempt.MaxScore > 0 ? (attempt.Score / attempt.MaxScore) * 100 : 0;

            // Set score with color coding
            lblScore.Text = $"Điểm: {attempt.Score:N2}/{attempt.MaxScore:N2} ({percentage:F1}%)";
            lblScore.ForeColor = percentage >= 60 ? ColorPalette.Success : ColorPalette.Error;

            // Set time spent
            var minutes = (attempt.TimeSpentSec ?? 0) / 60;
            lblTimeSpent.Text = $"⏱️ Thời gian: {minutes} phút";
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            ViewDetailsClicked?.Invoke(this, _attemptId);
        }

        private void PnlMain_Click(object sender, EventArgs e)
        {
            ViewDetailsClicked?.Invoke(this, _attemptId);
        }
    }
}
