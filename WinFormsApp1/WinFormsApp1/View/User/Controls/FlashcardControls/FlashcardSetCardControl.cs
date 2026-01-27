using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
	public partial class FlashcardSetCardControl : UserControl
	{
		public event Action<int>? ViewRequested;
		public event Action<int>? StudyRequested;

		public int SetId { get; private set; }

		public bool ShowStudyButton
		{
			get => btnStudy.Visible;
			set
			{
				btnStudy.Visible = value;
				UpdateButtonLayout();
			}
		}

		public string DetailButtonText
		{
			get => btnDetail.Text;
			set => btnDetail.Text = value;
		}

		public FlashcardSetCardControl()
		{
			InitializeComponent();
			WireEvents();
			UpdateButtonLayout();
		}

		public void Bind(FlashcardSet flashcardSet)
		{
			if (flashcardSet == null) return;

			SetId = flashcardSet.SetId;
			lblTitle.Text = flashcardSet.Title;
			var authorName = flashcardSet.Owner?.FullName;
			if (string.IsNullOrWhiteSpace(authorName))
			{
				authorName = flashcardSet.Owner?.Username;
			}
			if (string.IsNullOrWhiteSpace(authorName))
			{
				authorName = flashcardSet.Owner?.Email;
			}
			if (string.IsNullOrWhiteSpace(authorName) && AuthHelper.CurrentUser?.UserId == flashcardSet.OwnerId)
			{
				authorName = AuthHelper.CurrentUser.FullName;
				if (string.IsNullOrWhiteSpace(authorName))
				{
					authorName = AuthHelper.CurrentUser.Username;
				}
				if (string.IsNullOrWhiteSpace(authorName))
				{
					authorName = AuthHelper.CurrentUser.Email;
				}
			}
			lblAuthor.Text = $"👤 {authorName ?? "Unknown"}";
			lblAuthor.Visible = true;
			lblCount.Text = $"📇 {flashcardSet.Flashcards?.Count ?? 0} thẻ";
			lblLanguage.Text = flashcardSet.Language ?? string.Empty;
			lblLanguage.Visible = !string.IsNullOrWhiteSpace(flashcardSet.Language);

			if (!string.IsNullOrWhiteSpace(flashcardSet.Description))
			{
				lblDescription.Text = flashcardSet.Description.Length > 70
					? flashcardSet.Description.Substring(0, 70) + "..."
					: flashcardSet.Description;
				lblDescription.Visible = true;
			}
			else
			{
				lblDescription.Text = string.Empty;
				lblDescription.Visible = false;
			}
		}

		private void WireEvents()
		{
			btnDetail.Click += (s, e) => ViewRequested?.Invoke(SetId);
			btnStudy.Click += (s, e) => StudyRequested?.Invoke(SetId);

			pnlCard.Paint += PnlCard_Paint;
			pnlCard.MouseEnter += (s, e) => pnlCard.BackColor = Color.FromArgb(140, 95, 255);
			pnlCard.MouseLeave += (s, e) => pnlCard.BackColor = Color.FromArgb(124, 77, 255);
			pnlCard.Click += (s, e) => ViewRequested?.Invoke(SetId);

			foreach (Control ctrl in pnlCard.Controls)
			{
				if (ctrl is Button) continue;
				ctrl.Click += (s, e) => ViewRequested?.Invoke(SetId);
			}
		}

		private void UpdateButtonLayout()
		{
			btnDetail.Location = ShowStudyButton ? new Point(90, 300) : new Point(187, 300);
			btnStudy.Location = new Point(285, 300);
		}

		private void PnlCard_Paint(object? sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using var path = GetRoundedRectPath(new Rectangle(0, 0, pnlCard.Width, pnlCard.Height), 12);
			pnlCard.Region = new Region(path);
		}

		private static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
		{
			var path = new GraphicsPath();
			int diameter = radius * 2;
			var arc = new Rectangle(rect.Location, new Size(diameter, diameter));

			path.AddArc(arc, 180, 90);
			arc.X = rect.Right - diameter;
			path.AddArc(arc, 270, 90);
			arc.Y = rect.Bottom - diameter;
			path.AddArc(arc, 0, 90);
			arc.X = rect.Left;
			path.AddArc(arc, 90, 90);

			path.CloseFigure();
			return path;
		}
	}
}
