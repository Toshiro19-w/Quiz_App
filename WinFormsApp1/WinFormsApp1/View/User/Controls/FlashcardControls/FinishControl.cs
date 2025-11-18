using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.View.User;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
    public partial class FinishControl : UserControl
    {
        private readonly int _totalCards;

        public event Action OnRestart;
        public event Action OnViewOther;
        public event Action OnGoHome;

        public FinishControl(int totalCards)
        {
            _totalCards = totalCards;
            InitializeComponent();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            lblMessage.Text = $"Bạn đã hoàn thành tất cả {_totalCards} flashcards trong bộ này.\nHãy tiếp tục học tập để nâng cao kiến thức của bạn!";
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            OnRestart?.Invoke();
        }

        private void btnViewOther_Click(object sender, EventArgs e)
        {
            // Đóng form hiện tại
            var studyForm = this.FindForm();
            if (studyForm != null)
            {
                studyForm.Close();
            }

            // Tìm MainContainer và chuyển sang FlashcardControl
            var mainContainer = Application.OpenForms.OfType<MainContainer>().FirstOrDefault();
            if (mainContainer != null)
            {
                mainContainer.NavigateToFlashcards();
            }

            OnViewOther?.Invoke();
        }

        private void btnGoHome_Click(object sender, EventArgs e)
        {
            // Đóng form hiện tại
            var studyForm = this.FindForm();
            if (studyForm != null)
            {
                studyForm.Close();
            }

            // Tìm MainContainer và chuyển sang HomeControl
            var mainContainer = Application.OpenForms.OfType<MainContainer>().FirstOrDefault();
            if (mainContainer != null)
            {
                mainContainer.NavigateToHome();
            }

            OnGoHome?.Invoke();
        }

        private void FinishControl_Load(object sender, EventArgs e)
        {
            // Center the panel on load
            centerPanel.Location = new Point(
                (this.Width - centerPanel.Width) / 2,
                (this.Height - centerPanel.Height) / 2
            );
        }
    }
}
