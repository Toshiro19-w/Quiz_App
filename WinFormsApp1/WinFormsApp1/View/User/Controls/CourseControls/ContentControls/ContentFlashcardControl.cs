using System.Windows.Forms;
using WinFormsApp1.ViewModels;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
	public partial class ContentFlashcardControl : UserControl, IContentControl
	{
		public event Action<object, string>? ContentTypeChanged;
		public event Action<object>? DeleteRequested;

		private List<FlashcardItemControl> _items = new List<FlashcardItemControl>();

		public ContentFlashcardControl()
		{
			this.Width = 700; this.Height = 260; this.Margin = new Padding(0, 0, 0, 10);
			this.BorderStyle = BorderStyle.FixedSingle;
			InitializeComponent();

			// Add delete button
			var btnDelete = new Button
			{
				Text = "Xóa",
				Size = new Size(80, 30),
				Location = new Point(this.Width - 90, 5),
				BackColor = Color.FromArgb(220, 53, 69),
				ForeColor = Color.White,
				FlatStyle = FlatStyle.Flat
			};
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.Click += (s, e) => DeleteRequested?.Invoke(this);
			this.Controls.Add(btnDelete);

			// Wire designer button
			btnAddFlashcard.Click += (s, e) => AddFlashcardItem();

			// Set default selection and add event handler
			cboContentType.SelectedIndex = 2; // FlashcardSet
			cboContentType.SelectedIndexChanged += (s, e) =>
			{
				var type = cboContentType.SelectedItem?.ToString();
				if (type != null && type != "FlashcardSet")
				{
					ContentTypeChanged?.Invoke(this, type);
				}
			};

			// Ensure at least one flashcard
			AddFlashcardItem();
		}

		private void AddFlashcardItem(string front = "", string back = "", string hint = "")
		{
			var item = new FlashcardItemControl(front, back, hint);
			item.Width = pnlFlashcards.Width - SystemInformation.VerticalScrollBarWidth - 4;
			item.Location = new Point(0, _items.Count * (item.Height + 6));
			item.DeleteRequested += (o) => RemoveFlashcardItem(item);
			pnlFlashcards.Controls.Add(item);
			_items.Add(item);
			RearrangeItems();
		}

		private void RemoveFlashcardItem(FlashcardItemControl item)
		{
			pnlFlashcards.Controls.Remove(item);
			_items.Remove(item);
			RearrangeItems();
		}

		private void RearrangeItems()
		{
			for (int i = 0; i < _items.Count; i++)
			{
				var item = _items[i];
				item.Location = new Point(0, i * (item.Height + 6));
			}
		}

		public void LoadFromViewModel(LessonContentBuilderViewModel vm)
		{
			if (vm == null) return;
			txtTitle.Text = vm.Title ?? string.Empty;
			txtDesc.Text = vm.FlashcardSetDesc ?? string.Empty;

			// Clear existing
			foreach (var it in _items.ToArray()) RemoveFlashcardItem(it);

			if (vm.Flashcards != null)
			{
				foreach (var fc in vm.Flashcards)
				{
					AddFlashcardItem(fc.FrontText ?? "", fc.BackText ?? "", fc.Hint ?? "");
				}
			}

			if (_items.Count == 0) AddFlashcardItem();
		}

		public LessonContentBuilderViewModel SaveToViewModel()
		{
			var vm = new LessonContentBuilderViewModel
			{
				ContentType = "FlashcardSet",
				Title = txtTitle.Text.Trim(),
				FlashcardSetTitle = txtTitle.Text.Trim(),
				FlashcardSetDesc = txtDesc.Text.Trim(),
				Flashcards = new List<FlashcardBuilderViewModel>()
			};

			foreach (var it in _items)
			{
				vm.Flashcards.Add(new FlashcardBuilderViewModel
				{
					FrontText = it.FrontText.Trim(),
					BackText = it.BackText.Trim(),
					Hint = it.Hint.Trim()
				});
			}

			return vm;
		}


	}
}