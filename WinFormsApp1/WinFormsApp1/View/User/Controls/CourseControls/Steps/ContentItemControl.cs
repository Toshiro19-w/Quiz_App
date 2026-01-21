using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
	public partial class ContentItemControl : UserControl
	{
		public event EventHandler<LessonContentBuilderViewModel>? ItemClicked;
		public event EventHandler<LessonContentBuilderViewModel>? DeleteRequested;

		private LessonContentBuilderViewModel _contentVm;
		private bool _isSelected;

		public LessonContentBuilderViewModel ContentViewModel => _contentVm;

		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				_isSelected = value;
				UpdateAppearance();
			}
		}

		public ContentItemControl(LessonContentBuilderViewModel contentVm)
		{
			_contentVm = contentVm;
			InitializeComponent();
			SetupEventHandlers();
			UpdateDisplay();
		}

		private void SetupEventHandlers()
		{
			this.Click += (s, e) => ItemClicked?.Invoke(this, _contentVm);
			lblIcon.Click += (s, e) => ItemClicked?.Invoke(this, _contentVm);
			lblTitle.Click += (s, e) => ItemClicked?.Invoke(this, _contentVm);
			lblType.Click += (s, e) => ItemClicked?.Invoke(this, _contentVm);
			btnDelete.Click += (s, e) => DeleteRequested?.Invoke(this, _contentVm);
		}

		private void UpdateDisplay()
		{
			var (icon, color, label) = GetContentTypeInfo(_contentVm.ContentType);

			lblIcon.Text = icon;
			lblIcon.ForeColor = color;
			lblTitle.Text = string.IsNullOrWhiteSpace(_contentVm.Title) ? label : _contentVm.Title;
			lblType.Text = label;

			UpdateAppearance();
		}

		private void UpdateAppearance()
		{
			if (_isSelected)
			{
				this.BackColor = Color.FromArgb(225, 239, 254);
				this.BorderStyle = BorderStyle.FixedSingle;
			}
			else
			{
				this.BackColor = Color.White;
				this.BorderStyle = BorderStyle.FixedSingle;
			}
		}

		private (string Icon, Color Color, string Label) GetContentTypeInfo(string contentType)
		{
			return contentType switch
			{
				"Video" => ("\u25B6\uFE0F", Color.FromArgb(220, 53, 69), "Video"),
				"Theory" => ("\uD83D\uDCD6", Color.FromArgb(52, 144, 220), "Lý thuyết"),
				"FlashcardSet" => ("\uD83D\uDDC2\uFE0F", Color.FromArgb(255, 193, 7), "Flashcard"),
				"Test" => ("\u270D\uFE0F", Color.FromArgb(40, 167, 69), "Kiểm tra"),
				_ => ("\uD83D\uDCC4", ColorPalette.TextSecondary, "Nội dung")
			};
		}

		public void RefreshDisplay()
		{
			UpdateDisplay();
		}
	}
}
