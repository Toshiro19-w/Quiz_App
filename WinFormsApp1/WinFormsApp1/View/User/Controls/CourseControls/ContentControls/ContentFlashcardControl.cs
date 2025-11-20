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

			// disable internal scrolling — we'll expand the panel instead
			pnlFlashcards.AutoScroll = false;

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
			item.Width = pnlFlashcards.ClientSize.Width; // fit width
			item.Location = new Point(0, _items.Count * (item.Height + 6));
			item.DeleteRequested += (o) => RemoveFlashcardItem(item);
			pnlFlashcards.Controls.Add(item);
			_items.Add(item);
			RearrangeItems();
			AdjustContainerSize();
		}

		private void RemoveFlashcardItem(FlashcardItemControl item)
		{
			pnlFlashcards.Controls.Remove(item);
			_items.Remove(item);
			RearrangeItems();
			AdjustContainerSize();
		}

		private void RearrangeItems()
		{
			for (int i = 0; i < _items.Count; i++)
			{
				var item = _items[i];
				item.Location = new Point(0, i * (item.Height + 6));
				// set numbered title
				item.SetIndex(i + 1);
				// ensure width matches container
				item.Width = pnlFlashcards.ClientSize.Width;
			}
		}

		private void AdjustContainerSize()
		{
			// calculate required height to show all items without scroll
			int total = 0;
			foreach (var it in _items)
			{
				total += it.Height + 6;
			}
			// add a little padding
			total += 10;

			// set panel height to required size but not less than a minimum
			int minHeight = 120;
			pnlFlashcards.Height = Math.Max(minHeight, total);

			// expand this control to fit new panel height plus other elements (btnAddFlashcard etc.)
			int desired = pnlFlashcards.Location.Y + pnlFlashcards.Height + btnAddFlashcard.Height + 20;
			if (this.Height < desired) this.Height = desired;
			// also adjust parent container layout if necessary (if parent is a FlowLayoutPanel, call PerformLayout)
			this.Parent?.PerformLayout();
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
			else AdjustContainerSize();
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