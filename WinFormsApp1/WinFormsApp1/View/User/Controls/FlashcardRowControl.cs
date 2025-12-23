using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Controls
{
    public partial class FlashcardRowControl : UserControl
    {
        private FlashcardSet _flashcardSet;
        private int _index;

        public event EventHandler<FlashcardSet> ViewClicked;
        public event EventHandler<FlashcardSet> StudyClicked;
        public event EventHandler<FlashcardSet> EditClicked;
        public event EventHandler<FlashcardSet> DeleteClicked;

        public FlashcardRowControl()
        {
            InitializeComponent();
        }

        public void SetData(FlashcardSet flashcardSet, int index)
        {
            _flashcardSet = flashcardSet;
            _index = index;

            lblId.Text = index.ToString();
            lblTitle.Text = flashcardSet.Title;

            lblCardCount.Text = $"{flashcardSet.Flashcards.Count} thẻ";

            lblVisibility.Text = flashcardSet.Visibility == "Public" ? "Công khai" : "Riêng tư";
            lblVisibility.BackColor = flashcardSet.Visibility == "Public"
                ? Color.FromArgb(40, 167, 69)
                : Color.FromArgb(108, 117, 125);

            lblLanguage.Text = string.IsNullOrEmpty(flashcardSet.Language) ? "vi" : flashcardSet.Language;
            lblDate.Text = flashcardSet.CreatedAt.ToString("dd/MM/yyyy");
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            ViewClicked?.Invoke(this, _flashcardSet);
        }

        private void BtnStudy_Click(object sender, EventArgs e)
        {
            StudyClicked?.Invoke(this, _flashcardSet);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            EditClicked?.Invoke(this, _flashcardSet);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, _flashcardSet);
        }

        private void FlashcardRowControl_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = ColorPalette.Background;
        }

        private void FlashcardRowControl_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }
    }
}
