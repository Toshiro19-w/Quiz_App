using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
    public partial class FlashcardDetailControl : UserControl
    {
        private readonly int _setId;
        private Models.Entities.FlashcardSet _flashcardSet;
        private readonly FlashcardController _flashcardController;

        public FlashcardDetailControl(int setId)
        {
            _setId = setId;
            _flashcardController = new FlashcardController();
            InitializeComponent();
        }

        private async void FlashcardDetailControl_Load(object sender, EventArgs e)
        {
            await LoadFlashcardSetDetails();
        }

        private async System.Threading.Tasks.Task LoadFlashcardSetDetails()
        {
            try
            {
                _flashcardSet = await _flashcardController.GetFlashcardSetByIdAsync(_setId);

                if (_flashcardSet == null)
                {
                    MessageBox.Show("Không tìm thấy bộ flashcard!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DisplayFlashcardSetInfo();
                LoadFlashcardsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayFlashcardSetInfo()
        {
            // Breadcrumb
            lblBreadcrumb.Text = $"Flashcards / {_flashcardSet.Title}";

            // Title
            lblTitle.Text = _flashcardSet.Title;

            // Description
            if (!string.IsNullOrEmpty(_flashcardSet.Description))
            {
                rtbDescription.Text = _flashcardSet.Description;
            }
            else
            {
                rtbDescription.Text = "Chưa có mô tả";
            }

            // Author info
            lblAuthorName.Text = _flashcardSet.Owner?.FullName ?? "Unknown";
            lblAuthorEmail.Text = _flashcardSet.Owner?.Email ?? "";

            // Statistics in info panel
            lblCardCount.Text = $"Số thẻ: {_flashcardSet.Flashcards.Count}";
            lblCreatedDate.Text = $"Tạo lúc: {_flashcardSet.CreatedAt:dd/MM/yyyy}";

            // Language
            if (!string.IsNullOrEmpty(_flashcardSet.Language))
            {
                lblLanguage.Text = $"Ngôn ngữ: {_flashcardSet.Language}";
            }
        }

        private void LoadFlashcardsList()
        {
            flowFlashcards.Controls.Clear();

            // Header label
            var lblHeader = new Label
            {
                Text = $"Danh sách thẻ ({_flashcardSet.Flashcards.Count})",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(0, 10, 0, 10)
            };
            flowFlashcards.Controls.Add(lblHeader);

            var flashcards = _flashcardSet.Flashcards.OrderBy(f => f.OrderIndex).ToList();

            foreach (var flashcard in flashcards)
            {
                var card = CreateFlashcardItemCard(flashcard);
                flowFlashcards.Controls.Add(card);
            }

            if (!flashcards.Any())
            {
                var lblEmpty = new Label
                {
                    Text = "Chưa có thẻ nào trong bộ này",
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Padding = new Padding(0, 20, 0, 20)
                };
                flowFlashcards.Controls.Add(lblEmpty);
            }
        }

        private Panel CreateFlashcardItemCard(Models.Entities.Flashcard flashcard)
        {
            var card = new Panel
            {
                Width = 1150,
                Height = 130,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(248, 249, 250),
                Margin = new Padding(0, 8, 0, 8),
                Cursor = Cursors.Hand
            };

            // Question icon
            var lblIcon = new Label
            {
                Text = "❓",
                Font = new Font("Segoe UI", 28),
                Location = new Point(20, 40),
                Size = new Size(60, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblIcon);

            // Front text (question)
            var lblFront = new Label
            {
                Text = flashcard.FrontText.Length > 100 
                    ? flashcard.FrontText.Substring(0, 100) + "..." 
                    : flashcard.FrontText,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(100, 20),
                Size = new Size(850, 35),
                ForeColor = Color.FromArgb(0, 102, 102)
            };
            card.Controls.Add(lblFront);

            // Back text (answer) - preview
            var lblBack = new Label
            {
                Text = flashcard.BackText.Length > 100 
                    ? flashcard.BackText.Substring(0, 100) + "..." 
                    : flashcard.BackText,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(100, 60),
                Size = new Size(850, 55),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblBack);

            // Status icon (completed/not completed)
            var lblStatus = new Label
            {
                Text = "✓",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.FromArgb(76, 175, 80),
                Location = new Point(1000, 45),
                Size = new Size(50, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblStatus);

            // Hover effect
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(230, 230, 230);
            card.MouseLeave += (s, e) => card.BackColor = Color.FromArgb(248, 249, 250);

            return card;
        }

        private void btnStartLearning_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có flashcard nào không
            if (_flashcardSet == null || _flashcardSet.Flashcards.Count == 0)
            {
                MessageBox.Show("Bộ flashcard này chưa có thẻ nào để học!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Mở form học flashcard
            var studyForm = new FlashcardStudyForm(_setId);
            studyForm.ShowDialog();
        }

        private void btnViewDifferent_Click(object sender, EventArgs e)
        {
            // Navigate back to FlashcardControl
            var form = this.FindForm();
            if (form == null) return;

            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

            if (mainPanel == null)
            {
                mainPanel = this.Parent as Panel;
            }

            if (mainPanel == null) return;

            mainPanel.Controls.Clear();

            var flashcardControl = new FlashcardControl();
            flashcardControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(flashcardControl);
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
