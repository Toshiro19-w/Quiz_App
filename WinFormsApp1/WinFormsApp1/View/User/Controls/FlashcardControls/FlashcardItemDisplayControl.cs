using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
	public partial class FlashcardItemDisplayControl : UserControl
	{
		private readonly Color _defaultBackColor = Color.FromArgb(248, 249, 250);
		private readonly Color _hoverBackColor = Color.FromArgb(230, 230, 230);

		public FlashcardItemDisplayControl()
		{
			InitializeComponent();
			WireHoverEvents();
		}

		public FlashcardItemDisplayControl(Flashcard flashcard) : this()
		{
			SetFlashcard(flashcard);
		}

		public void SetFlashcard(Flashcard flashcard)
		{
			if (flashcard == null)
			{
				lblFront.Text = string.Empty;
				lblBack.Text = string.Empty;
				return;
			}

			lblFront.Text = TruncateText(flashcard.FrontText, 100);
			lblBack.Text = TruncateText(flashcard.BackText, 100);
		}

		private void WireHoverEvents()
		{
			MouseEnter += (_, _) => SetHover(true);
			MouseLeave += (_, _) => SetHover(false);

			foreach (Control control in Controls)
			{
				control.MouseEnter += (_, _) => SetHover(true);
				control.MouseLeave += (_, _) => SetHover(false);
			}
		}

		private void SetHover(bool isHover)
		{
			BackColor = isHover ? _hoverBackColor : _defaultBackColor;
		}

		private static string TruncateText(string text, int maxLength)
		{
			if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
			{
				return text ?? string.Empty;
			}

			return text.Substring(0, maxLength) + "...";
		}
	}
}
