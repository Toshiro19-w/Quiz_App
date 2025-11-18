using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
    public partial class FlashcardStudyForm : Form
    {
        private readonly int _setId;
        private Models.Entities.FlashcardSet _flashcardSet;
        private List<Models.Entities.Flashcard> _flashcards;
        private int _currentIndex = 0;
        private StudyControl _studyControl;
        private FinishControl _finishControl;

        public FlashcardStudyForm(int setId)
        {
            _setId = setId;
            InitializeComponent();
            LoadFlashcardSet();
        }

        private async void LoadFlashcardSet()
        {
            try
            {
                using (var context = new LearningPlatformContext())
                {
                    _flashcardSet = await context.FlashcardSets
                        .Include(fs => fs.Flashcards)
                        .FirstOrDefaultAsync(fs => fs.SetId == _setId);

                    if (_flashcardSet == null)
                    {
                        MessageBox.Show("Không tìm thấy bộ flashcard!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    _flashcards = _flashcardSet.Flashcards
                        .OrderBy(f => f.OrderIndex)
                        .ToList();

                    if (_flashcards.Count == 0)
                    {
                        MessageBox.Show("Bộ flashcard này chưa có thẻ nào!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        return;
                    }

                    StartStudy();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải flashcards: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void StartStudy()
        {
            _studyControl = new StudyControl(_flashcards, _currentIndex);
            _studyControl.OnNextCard += NextCard;
            _studyControl.OnPreviousCard += PreviousCard;
            _studyControl.OnFinish += FinishStudy;
            _studyControl.Dock = DockStyle.Fill;

            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(_studyControl);
        }

        private void NextCard()
        {
            if (_currentIndex < _flashcards.Count - 1)
            {
                _currentIndex++;
                _studyControl.UpdateCard(_flashcards, _currentIndex);
            }
        }

        private void PreviousCard()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                _studyControl.UpdateCard(_flashcards, _currentIndex);
            }
        }

        private void FinishStudy()
        {
            _finishControl = new FinishControl(_flashcards.Count);
            _finishControl.OnRestart += RestartStudy;
            _finishControl.OnViewOther += ViewOtherFlashcards;
            _finishControl.OnGoHome += GoHome;
            _finishControl.Dock = DockStyle.Fill;

            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(_finishControl);
        }

        private void RestartStudy()
        {
            _currentIndex = 0;
            StartStudy();
        }

        private void ViewOtherFlashcards()
        {
            this.Close();
        }

        private void GoHome()
        {
            this.Close();
        }
    }
}
