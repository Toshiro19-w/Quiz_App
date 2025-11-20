using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
    public partial class EditFlashcardControl : UserControl
    {
        private List<EditFlashcardCardControl> flashcardCards = new List<EditFlashcardCardControl>();
        private int cardCounter = 1;
        private readonly FlashcardController _flashcardController;
        private readonly int _flashcardSetId;
        private FlashcardSet _currentFlashcardSet;
        private List<int> _deletedFlashcardIds = new List<int>();

        public EditFlashcardControl(int flashcardSetId)
        {
            InitializeComponent();
            _flashcardController = new FlashcardController();
            _flashcardSetId = flashcardSetId;
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                _currentFlashcardSet = await _flashcardController.GetFlashcardSetByIdAsync(_flashcardSetId);
                if (_currentFlashcardSet == null)
                {
                    MessageBox.Show("Không tìm thấy bộ flashcard!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    NavigateBack();
                    return;
                }

                // Populate fields
                txtTitle.Text = _currentFlashcardSet.Title;
                txtDescription.Text = _currentFlashcardSet.Description;
                txtLanguage.Text = _currentFlashcardSet.Language;
                cboVisibility.SelectedItem = _currentFlashcardSet.Visibility;

                // Load cards
                var flashcards = await _flashcardController.GetFlashcardsInSetAsync(_flashcardSetId);
                foreach (var flashcard in flashcards)
                {
                    AddCard(flashcard);
                }

                if (flashcardCards.Count == 0)
                {
                    AddCard(null); // Add at least one empty card if none exist
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddCard(Flashcard flashcard = null)
        {
            var card = new EditFlashcardCardControl(cardCounter, flashcard);
            card.OnDeleteClicked += Card_OnDeleteClicked;
            card.Margin = new Padding(0, 0, 0, 15);
            flashcardCards.Add(card);
            flowCards.Controls.Add(card);
            cardCounter++;
            UpdateCardCount();
        }

        private void Card_OnDeleteClicked(EditFlashcardCardControl card)
        {
            if (flashcardCards.Count <= 1)
            {
                MessageBox.Show("Phải có ít nhất 1 thẻ flashcard!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // If it's an existing card (has ID), track it for deletion
            if (card.FlashcardId.HasValue)
            {
                _deletedFlashcardIds.Add(card.FlashcardId.Value);
            }

            flashcardCards.Remove(card);
            flowCards.Controls.Remove(card);
            card.Dispose();
            UpdateCardCount();
        }

        private void UpdateCardCount()
        {
            lblCardCount.Text = $"🃏 {flashcardCards.Count} thẻ";
            lblCardsHeader.Text = $"📋 Các thẻ Flashcard ({flashcardCards.Count})";
        }

        private void btnAddCard_Click(object sender, EventArgs e)
        {
            AddCard();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn hủy? Các thay đổi sẽ không được lưu.",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                NavigateBack();
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Vui lòng nhập tiêu đề!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return;
            }

            // Validate at least one card with both sides filled
            bool hasValidCard = false;
            foreach (var card in flashcardCards)
            {
                if (!string.IsNullOrWhiteSpace(card.FrontText) && !string.IsNullOrWhiteSpace(card.BackText))
                {
                    hasValidCard = true;
                    break;
                }
            }

            if (!hasValidCard)
            {
                MessageBox.Show("Vui lòng nhập ít nhất 1 thẻ với đầy đủ mặt trước và mặt sau!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnSave.Enabled = false;
                btnSave.Text = "Đang lưu...";

                // Update FlashcardSet
                _currentFlashcardSet.Title = txtTitle.Text.Trim();
                _currentFlashcardSet.Description = txtDescription.Text.Trim();
                _currentFlashcardSet.Visibility = cboVisibility.SelectedItem?.ToString() ?? "Public";
                _currentFlashcardSet.Language = txtLanguage.Text.Trim();

                await _flashcardController.UpdateFlashcardSetAsync(_currentFlashcardSet);

                // Process deletions
                foreach (var id in _deletedFlashcardIds)
                {
                    await _flashcardController.DeleteFlashcardAsync(id);
                }

                // Process updates and additions
                foreach (var card in flashcardCards)
                {
                    if (!string.IsNullOrWhiteSpace(card.FrontText) && !string.IsNullOrWhiteSpace(card.BackText))
                    {
                        if (card.FlashcardId.HasValue)
                        {
                            // Update existing
                            var flashcard = new Flashcard
                            {
                                CardId = card.FlashcardId.Value,
                                SetId = _flashcardSetId,
                                FrontText = card.FrontText.Trim(),
                                BackText = card.BackText.Trim(),
                                Hint = card.HintText?.Trim()
                            };
                            await _flashcardController.UpdateFlashcardAsync(flashcard);
                        }
                        else
                        {
                            // Add new
                            var flashcard = new Flashcard
                            {
                                SetId = _flashcardSetId,
                                FrontText = card.FrontText.Trim(),
                                BackText = card.BackText.Trim(),
                                Hint = card.HintText?.Trim()
                            };
                            await _flashcardController.AddFlashcardToSetAsync(flashcard);
                        }
                    }
                }

                MessageBox.Show("Cập nhật bộ flashcard thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                NavigateBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật flashcard: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = true;
                btnSave.Text = "💾 Lưu thay đổi";
            }
        }

        private void NavigateBack()
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

            var myFlashcardsControl = new MyFlashcardsControl();
            myFlashcardsControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(myFlashcardsControl);
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

    // Individual Flashcard Card Control (Modified to support editing)
    public class EditFlashcardCardControl : Panel
    {
        private TextBox txtFront;
        private TextBox txtBack;
        private TextBox txtHint;
        private Button btnDelete;
        private Label lblTitle;
        private Panel divider;

        public event Action<EditFlashcardCardControl> OnDeleteClicked;

        public int? FlashcardId { get; private set; }
        public string FrontText => txtFront.Text;
        public string BackText => txtBack.Text;
        public string HintText => txtHint.Text;

        public EditFlashcardCardControl(int cardNumber, Flashcard flashcard = null)
        {
            if (flashcard != null)
            {
                FlashcardId = flashcard.CardId;
            }
            InitializeComponent(cardNumber, flashcard);
        }

        private void InitializeComponent(int cardNumber, Flashcard flashcard)
        {
            this.Size = new Size(850, 260);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Padding = new Padding(25);

            // Header with card number and delete button
            lblTitle = new Label
            {
                Text = $"≡ Thẻ #{cardNumber}",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 118, 210),
                Location = new Point(25, 18),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            btnDelete = new Button
            {
                Text = "✖ Xóa",
                Size = new Size(85, 34),
                Location = new Point(745, 12),
                BackColor = Color.FromArgb(229, 57, 53),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += (s, e) => OnDeleteClicked?.Invoke(this);
            this.Controls.Add(btnDelete);

            // Divider line
            divider = new Panel
            {
                Location = new Point(25, 50),
                Size = new Size(800, 1),
                BackColor = Color.FromArgb(224, 224, 224)
            };
            this.Controls.Add(divider);

            // Front text label
            var lblFront = new Label
            {
                Text = "Mặt trước *",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(66, 66, 66),
                Location = new Point(25, 60),
                AutoSize = true
            };
            this.Controls.Add(lblFront);

            // Front text box
            txtFront = new TextBox
            {
                Multiline = true,
                Size = new Size(385, 75),
                Location = new Point(25, 85),
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "Nhập câu hỏi hoặc từ vựng",
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(250, 250, 250),
                Text = flashcard?.FrontText ?? ""
            };
            this.Controls.Add(txtFront);

            // Back text label
            var lblBack = new Label
            {
                Text = "Mặt sau *",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(66, 66, 66),
                Location = new Point(440, 60),
                AutoSize = true
            };
            this.Controls.Add(lblBack);

            // Back text box
            txtBack = new TextBox
            {
                Multiline = true,
                Size = new Size(385, 75),
                Location = new Point(440, 85),
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "Nhập câu trả lời hoặc nghĩa",
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(250, 250, 250),
                Text = flashcard?.BackText ?? ""
            };
            this.Controls.Add(txtBack);

            // Hint label
            var lblHint = new Label
            {
                Text = "Gợi ý (tùy chọn)",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(66, 66, 66),
                Location = new Point(25, 175),
                AutoSize = true
            };
            this.Controls.Add(lblHint);

            // Hint text box
            txtHint = new TextBox
            {
                Size = new Size(800, 50),
                Location = new Point(25, 200),
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "Nhập gợi ý giúp ghi nhớ",
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(250, 250, 250),
                Text = flashcard?.Hint ?? ""
            };
            this.Controls.Add(txtHint);
        }
    }
}
