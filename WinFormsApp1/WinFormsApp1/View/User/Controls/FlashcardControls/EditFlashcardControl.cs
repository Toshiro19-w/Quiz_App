using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;
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
		private int currentPage = 1;
		private const int cardsPerPage = 5;

		private readonly Dictionary<string, string> languageCodeMap = new Dictionary<string, string>
		{
			{ "Tiếng Việt", "vi" },
			{ "English", "en" }
		};
		public EditFlashcardControl(int flashcardSetId)
		{
			InitializeComponent();
			_flashcardController = new FlashcardController();
			_flashcardSetId = flashcardSetId;
			LocalizeUI();
			LoadData();
		}

		private void LocalizeUI()
		{
			// Header
			lblHeader.Text = LanguageHelper.GetString("EditFlashcardSet");
			
			// Info panel labels
			lblTitleLabel.Text = LanguageHelper.GetString("TitleRequired2");
			txtTitle.PlaceholderText = LanguageHelper.GetString("TitlePlaceholder");
			lblDescLabel.Text = LanguageHelper.GetString("Description");
			txtDescription.PlaceholderText = LanguageHelper.GetString("DescriptionPlaceholder");
			lblVisibilityLabel.Text = LanguageHelper.GetString("VisibilityMode");
			lblLanguageLabel.Text = LanguageHelper.GetString("Language");
			
			// Pagination
			btnPrevPage.Text = LanguageHelper.GetString("PrevPage");
			btnNextPage.Text = LanguageHelper.GetString("NextPage");
			
			// Buttons
			btnAddCard.Text = LanguageHelper.GetString("AddCard");
			btnCancel.Text = LanguageHelper.GetString("Cancel");
			btnSave.Text = LanguageHelper.GetString("UpdateFlashcardSetBtn");
		}

		private async void LoadData()
		{
			try
			{
				_currentFlashcardSet = await _flashcardController.GetFlashcardSetByIdAsync(_flashcardSetId);
				if (_currentFlashcardSet == null)
				{
				MessageBox.Show(LanguageHelper.GetString("FlashcardSetNotFound"), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
					NavigateBack();
					return;
				}

				txtTitle.Text = _currentFlashcardSet.Title;
				txtDescription.Text = _currentFlashcardSet.Description;

				// Set language ComboBox
				if (!string.IsNullOrEmpty(_currentFlashcardSet.Language))
				{
					string displayName = GetLanguageDisplayName(_currentFlashcardSet.Language);
					int index = cboLanguage.Items.IndexOf(displayName);
					cboLanguage.SelectedIndex = index >= 0 ? index : 0;
				}
				else
				{
					cboLanguage.SelectedIndex = 0;
				}
				cboVisibility.SelectedItem = _currentFlashcardSet.Visibility;

				var flashcards = await _flashcardController.GetFlashcardsInSetAsync(_flashcardSetId);
				foreach (var flashcard in flashcards)
				{
					AddCard(flashcard);
				}

				if (flashcardCards.Count == 0)
				{
					AddCard(null);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(LanguageHelper.GetString("DataLoadError", ex.Message), LanguageHelper.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void AddCard(Flashcard flashcard = null)
		{
			var card = new EditFlashcardCardControl(cardCounter, flashcard);
			card.OnDeleteClicked += Card_OnDeleteClicked;
			card.Margin = new Padding(0, 0, 0, 15);
			flashcardCards.Add(card);
			cardCounter++;
			UpdateCardCount();
			UpdatePagination();
		}

		private void Card_OnDeleteClicked(EditFlashcardCardControl card)
		{
			if (flashcardCards.Count <= 1)
			{
				MessageBox.Show(LanguageHelper.GetString("AtLeastOneCard"), LanguageHelper.GetString("Warning"),
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (card.FlashcardId.HasValue)
			{
				_deletedFlashcardIds.Add(card.FlashcardId.Value);
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
			lblCardCount.Text = LanguageHelper.GetString("CardCount", flashcardCards.Count);
			lblCardsHeader.Text = LanguageHelper.GetString("FlashcardCards", flashcardCards.Count);
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
				lblPageInfo.Text = LanguageHelper.GetString("PageOf", currentPage, totalPages);
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
			AddCard();
			
			int totalPages = GetTotalPages();
			if (currentPage != totalPages)
			{
				currentPage = totalPages;
				UpdatePagination();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show(LanguageHelper.GetString("CancelConfirm"),
				LanguageHelper.GetString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				NavigateBack();
			}
		}

		private async void btnSave_Click(object sender, EventArgs e)
		{
		if (string.IsNullOrWhiteSpace(txtTitle.Text))
		{
		MessageBox.Show(LanguageHelper.GetString("EnterTitle"), LanguageHelper.GetString("Warning"),
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
		MessageBox.Show(LanguageHelper.GetString("EnterAtLeastOneValidCard"),
		LanguageHelper.GetString("Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
		return;
		}

		try
		{
		btnSave.Enabled = false;
		btnSave.Text = LanguageHelper.GetString("Updating");

				_currentFlashcardSet.Title = txtTitle.Text.Trim();
				_currentFlashcardSet.Description = txtDescription.Text.Trim();
				_currentFlashcardSet.Visibility = cboVisibility.SelectedItem?.ToString() ?? "Public";
				string selectedLanguage = cboLanguage.SelectedItem?.ToString() ?? "Tiếng Việt";
				_currentFlashcardSet.Language = GetLanguageCode(selectedLanguage);

				await _flashcardController.UpdateFlashcardSetAsync(_currentFlashcardSet);

				foreach (var id in _deletedFlashcardIds)
				{
					await _flashcardController.DeleteFlashcardAsync(id);
				}

				foreach (var card in flashcardCards)
				{
					if (!string.IsNullOrWhiteSpace(card.FrontText) && !string.IsNullOrWhiteSpace(card.BackText))
					{
						if (card.FlashcardId.HasValue)
						{
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

				MessageBox.Show(LanguageHelper.GetString("FlashcardUpdateSuccess"), LanguageHelper.GetString("Success"),
				MessageBoxButtons.OK, MessageBoxIcon.Information);

				NavigateBack();
				}
				catch (Exception ex)
				{
				MessageBox.Show(LanguageHelper.GetString("FlashcardUpdateError", ex.Message), LanguageHelper.GetString("Error"),
				MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnSave.Enabled = true;
				btnSave.Text = LanguageHelper.GetString("UpdateFlashcardSetBtn");
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

	lblTitle = new Label
	{
		Text = LanguageHelper.GetString("CardNumber", cardNumber),
		Font = new Font("Segoe UI", 11F, FontStyle.Bold),
		ForeColor = Color.FromArgb(25, 118, 210),
		Location = new Point(25, 18),
		AutoSize = true
	};
	this.Controls.Add(lblTitle);

	btnDelete = new Button
	{
		Text = LanguageHelper.GetString("DeleteBtn"),
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
		Text = LanguageHelper.GetString("FrontSide"),
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
		PlaceholderText = LanguageHelper.GetString("FrontPlaceholder"),
		BorderStyle = BorderStyle.FixedSingle,
		BackColor = Color.FromArgb(250, 250, 250),
		Text = flashcard?.FrontText ?? ""
	};
	this.Controls.Add(txtFront);

	var lblBack = new Label
	{
		Text = LanguageHelper.GetString("BackSide"),
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
		PlaceholderText = LanguageHelper.GetString("BackPlaceholder"),
		BorderStyle = BorderStyle.FixedSingle,
		BackColor = Color.FromArgb(250, 250, 250),
		Text = flashcard?.BackText ?? ""
	};
	this.Controls.Add(txtBack);

	var lblHint = new Label
	{
		Text = LanguageHelper.GetString("HintOptional"),
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
		PlaceholderText = LanguageHelper.GetString("HintPlaceholder"),
		BorderStyle = BorderStyle.FixedSingle,
		BackColor = Color.FromArgb(250, 250, 250),
		Text = flashcard?.Hint ?? ""
	};
	this.Controls.Add(txtHint);
	}
	}
	}
