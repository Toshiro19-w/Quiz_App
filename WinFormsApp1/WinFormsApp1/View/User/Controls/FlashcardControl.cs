using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.View.User.Controls.FlashcardControls;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Controls
{
    public partial class FlashcardControl : UserControl
    {
        private readonly FlashcardController _flashcardController;

        public FlashcardControl()
        {
            InitializeComponent();
            _flashcardController = new FlashcardController();
        }

        private void FlashcardControl_Load(object sender, EventArgs e)
        {
            LoadAllFlashcardSets();
        }


        private async void LoadAllFlashcardSets()
        {
            flowFlashcards.Controls.Clear();

            try
            {
                // Use controller to get all public flashcard sets
                var flashcardSets = await _flashcardController.GetAllPublicFlashcardSetsAsync();

                if (flashcardSets.Count == 0)
                {
                    ShowEmptyState();
                    return;
                }

                // Update count label
                lblFlashcardCount.Text = $"{flashcardSets.Count} bộ flashcard";

                // Create card for each flashcard set
                foreach (var flashcardSet in flashcardSets)
                {
                    var card = CreateFlashcardCard(flashcardSet);
                    flowFlashcards.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải flashcards: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private FlashcardSetCardControl CreateFlashcardCard(FlashcardSet flashcardSet)
		{
			var card = new FlashcardSetCardControl();
			card.Bind(flashcardSet);
			card.ViewRequested += ShowFlashcardDetail;
			card.StudyRequested += StartStudying;
			return card;
		}

        private void ShowFlashcardDetail(int setId)
        {
            var form = this.FindForm();
            if (form == null) return;

            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

            if (mainPanel == null)
            {
                mainPanel = this.Parent as Panel;
            }

            if (mainPanel == null) return;

            mainPanel.Controls.Clear();

			var detail = new FlashcardDetailControl(setId, FlashcardDetailSource.PublicLibrary);
            detail.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(detail);
        }

        private async void StartStudying(int setId)
        {
            // Kiểm tra xem bộ flashcard có thẻ nào không
            try
            {
                var flashcardSet = await _flashcardController.GetFlashcardSetByIdAsync(setId);

                if (flashcardSet == null)
                {
                    MessageBox.Show("Không tìm thấy bộ flashcard!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (flashcardSet.Flashcards.Count == 0)
                {
                    MessageBox.Show("Bộ flashcard này chưa có thẻ nào để học!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Mở form học flashcard
                var studyForm = new FlashcardStudyForm(setId);
                studyForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở flashcard: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowEmptyState()
        {
            var emptyPanel = new Panel
            {
                Size = new Size(800, 400),
                BackColor = Color.Transparent,
                Location = new Point(450, 200)
            };

            var lblIcon = new Label
            {
                Text = "📚",
                Font = new Font("Segoe UI", 72),
                ForeColor = Color.Gray,
                Location = new Point(330, 80),
                AutoSize = true
            };
            emptyPanel.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = "Chưa có flashcard nào",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(220, 180),
                AutoSize = true
            };
            emptyPanel.Controls.Add(lblTitle);

            var lblMessage = new Label
            {
                Text = "Hãy quay lại sau để khám phá các bộ flashcard mới!",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Location = new Point(180, 230),
                Size = new Size(440, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            emptyPanel.Controls.Add(lblMessage);

            flowFlashcards.Controls.Add(emptyPanel);
        }

        private void FlashcardControl_Resize(object sender, EventArgs e)
        {
            if (flowFlashcards != null)
            {
                flowFlashcards.Size = new Size(this.Width - 40, this.Height - 180);
            }
        }

        private Control FindControlRecursive(Control parent, string name)
        {
            foreach (Control c in parent.Controls)
            {
                if (string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase)) return c;
                var found = FindControlRecursive(c, name);
                if (found != null) return found;
            }
            return null;
        }
    }
}
