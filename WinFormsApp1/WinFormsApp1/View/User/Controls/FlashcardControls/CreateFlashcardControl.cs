using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Controllers;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
	public partial class CreateFlashcardControl : UserControl
	{
		private List<FlashcardCardControl> flashcardCards = new List<FlashcardCardControl>();
		private int cardCounter = 1;
		private readonly FlashcardController _flashcardController;
		private int currentPage = 1;
		private const int cardsPerPage = 5;

		private readonly Dictionary<string, string> languageCodeMap = new Dictionary<string, string>
		{
			{ "Tiếng Việt", "vi" },
			{ "English", "en" }
		};

		public CreateFlashcardControl()
		{
			InitializeComponent();
			_flashcardController = new FlashcardController();
			AddFirstCard();
		}

		private void AddFirstCard()
		{
			AddNewCard();
		}

		private void AddNewCard()
		{
			var card = new FlashcardCardControl(cardCounter);
			card.OnDeleteClicked += Card_OnDeleteClicked;
			card.Margin = new Padding(0, 0, 0, 15);
			flashcardCards.Add(card);
			cardCounter++;
			UpdateCardCount();
			UpdatePagination();
		}

		private void Card_OnDeleteClicked(FlashcardCardControl card)
		{
			if (flashcardCards.Count <= 1)
			{
				MessageBox.Show("Phải có ít nhất 1 thẻ flashcard!", "Cảnh báo",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			flashcardCards.Remove(card);
			card.Dispose();

			int totalPages = GetTotalPages();
			if (currentPage > totalPages && currentPage > 1)
			{
				currentPage = totalPages;
			}

			UpdateCardCount();
			UpdatePagination();
		}

		private void UpdateCardCount()
		{
			lblCardCount.Text = $"📄 {flashcardCards.Count} thẻ";
			lblCardsHeader.Text = $"📄 Các thẻ Flashcard ({flashcardCards.Count})";
		}

		private int GetTotalPages()
		{
			return (int)Math.Ceiling((double)flashcardCards.Count / cardsPerPage);
		}

		private void UpdatePagination()
		{
			flowCards.SuspendLayout();
			flowCards.Controls.Clear();

			int totalPages = GetTotalPages();
			int startIndex = (currentPage - 1) * cardsPerPage;
			int endIndex = Math.Min(startIndex + cardsPerPage, flashcardCards.Count);

			for (int i = startIndex; i < endIndex; i++)
			{
				flowCards.Controls.Add(flashcardCards[i]);
			}

			if (totalPages > 1)
			{
				lblPageInfo.Text = $"Trang {currentPage} / {totalPages}";
				lblPageInfo.Visible = true;
				btnPrevPage.Visible = true;
				btnNextPage.Visible = true;
				btnPrevPage.Enabled = currentPage > 1;
				btnNextPage.Enabled = currentPage < totalPages;
			}
			else
			{
				lblPageInfo.Visible = false;
				btnPrevPage.Visible = false;
				btnNextPage.Visible = false;
			}

			flowCards.ResumeLayout();
		}

		private void btnPrevPage_Click(object sender, EventArgs e)
		{
			if (currentPage > 1)
			{
				currentPage--;
				UpdatePagination();
			}
		}

		private void btnNextPage_Click(object sender, EventArgs e)
		{
			int totalPages = GetTotalPages();
			if (currentPage < totalPages)
			{
				currentPage++;
				UpdatePagination();
			}
		}

		private void btnAddCard_Click(object sender, EventArgs e)
		{
			AddNewCard();

			int totalPages = GetTotalPages();
			if (currentPage != totalPages)
			{
				currentPage = totalPages;
				UpdatePagination();
			}
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

		private async void btnCreate_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtTitle.Text))
			{
				MessageBox.Show("Vui lòng nhập tiêu đề!", "Cảnh báo",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtTitle.Focus();
				return;
			}

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
				btnCreate.Enabled = false;
				btnCreate.Text = "Đang tạo...";

				var flashcardSet = new FlashcardSet
				{
					OwnerId = AuthHelper.CurrentUser.UserId,
					Title = txtTitle.Text.Trim(),
					Description = txtDescription.Text.Trim(),
					Visibility = cboVisibility.SelectedItem?.ToString() ?? "Public",
					Language = GetLanguageCode(cboLanguage.SelectedItem?.ToString() ?? "Tiếng Việt")
				};

				var flashcards = new List<Flashcard>();
				foreach (var card in flashcardCards)
				{
					if (!string.IsNullOrWhiteSpace(card.FrontText) && !string.IsNullOrWhiteSpace(card.BackText))
					{
						var flashcard = new Flashcard
						{
							FrontText = card.FrontText.Trim(),
							BackText = card.BackText.Trim(),
							Hint = card.HintText?.Trim()
						};
						flashcards.Add(flashcard);
					}
				}

				await _flashcardController.CreateFlashcardSetAsync(flashcardSet, flashcards);

				MessageBox.Show("Tạo bộ flashcard thành công!", "Thành công",
					MessageBoxButtons.OK, MessageBoxIcon.Information);

				NavigateBack();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi tạo flashcard: {ex.Message}", "Lỗi",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnCreate.Enabled = true;
				btnCreate.Text = "✨ Tạo bộ Flashcard";
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

		private string GetLanguageDisplayName(string languageCode)
		{
			var pair = languageCodeMap.FirstOrDefault(x => x.Value == languageCode);
			return pair.Key ?? "Tiếng Việt";
		}

		private string GetLanguageCode(string displayName)
		{
			if (languageCodeMap.TryGetValue(displayName, out string code))
			{
				return code;
			}
			return "vi";
		}
	}

	public class FlashcardCardControl : Panel
	{
		private TextBox txtFront;
		private TextBox txtBack;
		private TextBox txtHint;
		private Button btnDelete;
		private Label lblTitle;
		private Panel divider;

		public event Action<FlashcardCardControl> OnDeleteClicked;

		public string FrontText => txtFront.Text;
		public string BackText => txtBack.Text;
		public string HintText => txtHint.Text;

		public FlashcardCardControl(int cardNumber)
		{
			InitializeComponent(cardNumber);
		}

		private void InitializeComponent(int cardNumber)
		{
			this.Size = new Size(850, 260);
			this.BackColor = Color.White;
			this.BorderStyle = BorderStyle.FixedSingle;
			this.Padding = new Padding(25);

			lblTitle = new Label
			{
				Text = $"= Thẻ #{cardNumber}",
				Font = new Font("Segoe UI", 11F, FontStyle.Bold),
				ForeColor = Color.FromArgb(25, 118, 210),
				Location = new Point(25, 18),
				AutoSize = true
			};
			this.Controls.Add(lblTitle);

			btnDelete = new Button
			{
				Text = "🗑️ Xóa",
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

			divider = new Panel
			{
				Location = new Point(25, 50),
				Size = new Size(800, 1),
				BackColor = Color.FromArgb(224, 224, 224)
			};
			this.Controls.Add(divider);

			var lblFront = new Label
			{
				Text = "Mặt trước *",
				Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
				ForeColor = Color.FromArgb(66, 66, 66),
				Location = new Point(25, 60),
				AutoSize = true
			};
			this.Controls.Add(lblFront);

			txtFront = new TextBox
			{
				Multiline = true,
				Size = new Size(385, 75),
				Location = new Point(25, 85),
				Font = new Font("Segoe UI", 10F),
				PlaceholderText = "Nhập câu hỏi hoặc từ vựng",
				BorderStyle = BorderStyle.FixedSingle,
				BackColor = Color.FromArgb(250, 250, 250)
			};
			this.Controls.Add(txtFront);

			var lblBack = new Label
			{
				Text = "Mặt sau *",
				Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
				ForeColor = Color.FromArgb(66, 66, 66),
				Location = new Point(440, 60),
				AutoSize = true
			};
			this.Controls.Add(lblBack);

			txtBack = new TextBox
			{
				Multiline = true,
				Size = new Size(385, 75),
				Location = new Point(440, 85),
				Font = new Font("Segoe UI", 10F),
				PlaceholderText = "Nhập câu trả lời hoặc nghĩa",
				BorderStyle = BorderStyle.FixedSingle,
				BackColor = Color.FromArgb(250, 250, 250)
			};
			this.Controls.Add(txtBack);

			var lblHint = new Label
			{
				Text = "Gợi ý (tùy chọn)",
				Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
				ForeColor = Color.FromArgb(66, 66, 66),
				Location = new Point(25, 175),
				AutoSize = true
			};
			this.Controls.Add(lblHint);

			txtHint = new TextBox
			{
				Size = new Size(800, 50),
				Location = new Point(25, 200),
				Font = new Font("Segoe UI", 10F),
				PlaceholderText = "Nhập gợi ý giúp ghi nhớ",
				BorderStyle = BorderStyle.FixedSingle,
				BackColor = Color.FromArgb(250, 250, 250)
			};
			this.Controls.Add(txtHint);
		}
	}
}
