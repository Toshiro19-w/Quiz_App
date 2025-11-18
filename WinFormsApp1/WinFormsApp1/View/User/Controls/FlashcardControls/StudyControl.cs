using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
    public partial class StudyControl : UserControl
    {
        private List<Models.Entities.Flashcard> _flashcards;
        private int _currentIndex;
        private bool _isFlipped = false;

        public event Action OnNextCard;
        public event Action OnPreviousCard;
        public event Action OnFinish;

        public StudyControl(List<Models.Entities.Flashcard> flashcards, int currentIndex)
        {
            _flashcards = flashcards;
            _currentIndex = currentIndex;
            InitializeComponent();
            CenterControls();
            UpdateDisplay();
        }

        public void UpdateCard(List<Models.Entities.Flashcard> flashcards, int currentIndex)
        {
            _flashcards = flashcards;
            _currentIndex = currentIndex;
            _isFlipped = false;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            var currentCard = _flashcards[_currentIndex];

            // Update progress
            lblProgress.Text = $"{_currentIndex + 1} / {_flashcards.Count} Flashcards";
            int progressPercent = (int)((_currentIndex + 1) / (float)_flashcards.Count * 100);
            progressBar.Value = progressPercent;
            lblProgressPercent.Text = $"{progressPercent}%";

            // Update card text
            if (_isFlipped)
            {
                lblCardText.Text = currentCard.BackText;
                lblCardText.ForeColor = Color.FromArgb(0, 180, 216);
            }
            else
            {
                lblCardText.Text = currentCard.FrontText;
                lblCardText.ForeColor = Color.FromArgb(255, 140, 0);
            }

            // Update buttons
            btnPrevious.Enabled = _currentIndex > 0;
            btnNext.Enabled = _currentIndex < _flashcards.Count - 1;

            // Show finish button only on last card
            if (_currentIndex == _flashcards.Count - 1)
            {
                btnFinish.Visible = true;
            }
            else
            {
                btnFinish.Visible = false;
            }

            // Update hint text
            lblHint.Text = "Nhấn vào flashcard hoặc gõ phím Space để lật";
        }

        private void cardPanel_Click(object sender, EventArgs e)
        {
            FlipCard();
        }

        private void lblCardText_Click(object sender, EventArgs e)
        {
            FlipCard();
        }

        private void FlipCard()
        {
            _isFlipped = !_isFlipped;
            UpdateDisplay();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            OnPreviousCard?.Invoke();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            OnNextCard?.Invoke();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            OnFinish?.Invoke();
        }

        private void StudyControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                FlipCard();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (_currentIndex > 0)
                    OnPreviousCard?.Invoke();
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (_currentIndex < _flashcards.Count - 1)
                    OnNextCard?.Invoke();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space)
            {
                FlipCard();
                return true;
            }
            else if (keyData == Keys.Left)
            {
                if (_currentIndex > 0)
                    OnPreviousCard?.Invoke();
                return true;
            }
            else if (keyData == Keys.Right)
            {
                if (_currentIndex < _flashcards.Count - 1)
                    OnNextCard?.Invoke();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
