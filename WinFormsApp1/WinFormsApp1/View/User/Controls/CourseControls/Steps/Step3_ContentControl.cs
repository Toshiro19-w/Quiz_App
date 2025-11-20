using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.ViewModels;
using WinFormsApp1.View.User.Controls.CourseControls.ContentControls;

namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
	public partial class Step3_ContentControl : UserControl, IStepControl
	{
		private CourseBuilderViewModel? _vm;
		private int _currentChapterIndex = -1;
		private int _currentLessonIndex = -1;

		public Step3_ContentControl()
		{
			InitializeComponent();

			btnPrev.Click += (s, e) => OnPrevRequested?.Invoke(this, EventArgs.Empty);
			btnNext.Click += (s, e) => OnNextRequested?.Invoke(this, EventArgs.Empty);

			cmbLessonSelector.SelectedIndexChanged += (s, e) => ChangeSelectedLesson();
			btnAddContent.Click += (s, e) => AddNewContent();
		}

		public event EventHandler? OnPrevRequested;
		public event EventHandler? OnNextRequested;

		// ============================================================
		// LOAD FROM VIEWMODEL
		// ============================================================
		public void LoadFromViewModel(CourseBuilderViewModel vm)
		{
			_vm = vm;
			cmbLessonSelector.Items.Clear();
			flpContents.Controls.Clear();
			_currentChapterIndex = -1;
			_currentLessonIndex = -1;

			if (vm?.Chapters == null || vm.Chapters.Count == 0)
			{
				cmbLessonSelector.Enabled = false;
				btnAddContent.Enabled = false;
				return;
			}

			// Fill selector
			for (int ch = 0; ch < vm.Chapters.Count; ch++)
			{
				var chapter = vm.Chapters[ch];
				
				if (chapter.Lessons == null) continue;

					for (int ls = 0; ls < chapter.Lessons.Count; ls++)
				{
					var lesson = chapter.Lessons[ls];
					cmbLessonSelector.Items.Add(new ComboItem
					{
						Text = $"{chapter.Title} → {lesson.Title}",
						ChapterIndex = ch,
						LessonIndex = ls,
						Lesson = lesson
					});
				}
			}

			if (cmbLessonSelector.Items.Count > 0)
			{
				cmbLessonSelector.SelectedIndex = 0;
				cmbLessonSelector.Enabled = true;
				btnAddContent.Enabled = true;
				ChangeSelectedLesson();
			}
		}

		// ============================================================
		// SAVE TO VIEWMODEL
		// ============================================================
		public void SaveToViewModel(CourseBuilderViewModel vm)
		{
			SaveCurrentLesson();
		}

		// ============================================================
		// CHUYỂN BÀI HỌC (LƯU TRƯỚC, LOAD SAU)
		// ============================================================
		private void ChangeSelectedLesson()
		{
			// Only save if we have a valid current lesson
			if (_currentChapterIndex >= 0 && _currentLessonIndex >= 0)
			{
				SaveCurrentLesson();   // save old lesson BEFORE switching
			}

			if (cmbLessonSelector.SelectedItem is not ComboItem item)
			{
				return;
			}

			_currentChapterIndex = item.ChapterIndex;
			_currentLessonIndex = item.LessonIndex;

			LoadLessonContents();
		}

		// ============================================================
		// LƯU TẤT CẢ CONTENTS CỦA BÀI HIỆN TẠI
		// ============================================================
		private void SaveCurrentLesson()
		{
			if (_vm == null) return;
			if (_currentChapterIndex < 0 || _currentLessonIndex < 0) return;

			var chapter = _vm.Chapters[_currentChapterIndex];
			var lesson = chapter.Lessons[_currentLessonIndex];

			Debug.WriteLine($"[Step3] SaveCurrentLesson: Lesson '{lesson.Title}', Controls count: {flpContents.Controls.Count}");

			// Only clear and rebuild if we have UI controls to save from
			if (flpContents.Controls.Count > 0)
			{
				var savedList = new System.Collections.Generic.List<LessonContentBuilderViewModel>();
				int order = 0;
				foreach (Control c in flpContents.Controls)
				{
					if (c is IContentControl ic)
					{
						var saved = ic.SaveToViewModel();
						Debug.WriteLine($"[Step3] Saved content: Type={saved.ContentType}, Title='{saved.Title}', Questions={saved.Questions?.Count ?? 0}");

						// If control.Tag holds original contentVm, preserve identifiers
						if (c.Tag is LessonContentBuilderViewModel original)
						{
							saved.ContentId = original.ContentId;
							saved.RefId = original.RefId;
							// preserve original OrderIndex if present, otherwise set sequential
							saved.OrderIndex = original.OrderIndex != 0 ? original.OrderIndex : ++order;
						}
						else
						{
							// new content created in UI
							saved.OrderIndex = ++order;
						}

						savedList.Add(saved);
					}
				}

				Debug.WriteLine($"[Step3] Total saved contents: {savedList.Count}");
				// replace lesson contents with preserved list
				lesson.Contents.Clear();
				lesson.Contents.AddRange(savedList);
			}
		}

		// ============================================================
		// LOAD NỘI DUNG CỦA BÀI ĐANG CHỌN
		// ============================================================
		private void LoadLessonContents()
		{
			if (_vm == null) return;
			if (cmbLessonSelector.SelectedItem is not ComboItem item) return;

			// Use direct reference if available, fallback to index-based lookup
			var lesson = item.Lesson ?? _vm.Chapters[item.ChapterIndex].Lessons[item.LessonIndex];

			flpContents.Controls.Clear();

			if (lesson.Contents.Count == 0)
			{
				// Force reload from original ViewModel if current lesson has no contents
				if (item.Lesson != null && item.Lesson.Contents.Count > 0)
				{
					lesson = item.Lesson;
				}
				else
				{
					return;
				}
			}

			foreach (var contentVm in lesson.Contents)
			{
				Control ctl = CreateControlByType(contentVm.ContentType);
				if (ctl is IContentControl ic)
				{
					ic.LoadFromViewModel(contentVm);
					// attach original VM to control so we can preserve IDs later
					ctl.Tag = contentVm;
					ic.DeleteRequested += (s) => OnContentDeleteRequested(ctl, contentVm);
				}
				
				if (ctl is ContentTheoryControl theoryCtl)
				{
					theoryCtl.ContentTypeChanged += (s, newType) => OnContentTypeChanged(ctl, newType, contentVm);
				}
				else if (ctl is ContentVideoControl videoCtl)
				{
					videoCtl.ContentTypeChanged += (s, newType) => OnContentTypeChanged(ctl, newType, contentVm);
				}
				else if (ctl is ContentFlashcardControl flashcardCtl)
				{
					flashcardCtl.ContentTypeChanged += (s, newType) => OnContentTypeChanged(ctl, newType, contentVm);
				}
				else if (ctl is ContentTestControl testCtl)
				{
					testCtl.ContentTypeChanged += (s, newType) => OnContentTypeChanged(ctl, newType, contentVm);
				}

				flpContents.Controls.Add(ctl);
			}
		}

		// ============================================================
		// THÊM MỚI CONTENT
		// ============================================================
		private void AddNewContent()
		{
			if (_vm == null) return;
			if (cmbLessonSelector.SelectedItem is not ComboItem item)
			{
				MessageBox.Show("Vui lòng chọn bài học.");
				return;
			}

			var lesson = item.Lesson ?? _vm.Chapters[item.ChapterIndex].Lessons[item.LessonIndex];

			var newContent = new LessonContentBuilderViewModel
			{
				Title = "Nội dung mới",
				ContentType = "Theory",
				Body = "Nhập nội dung ở đây..."
			};

			lesson.Contents.Add(newContent);

			var ctl = new ContentTheoryControl();
			ctl.LoadFromViewModel(newContent);
			// attach VM so Save can preserve metadata if user later edits
			ctl.Tag = newContent;

			// 🎯 FIX QUAN TRỌNG: GẮN EVENT CHUYỂN TYPE
			ctl.ContentTypeChanged += (s, newType)
				=> OnContentTypeChanged(ctl, newType, newContent);
			ctl.DeleteRequested += (s) => OnContentDeleteRequested(ctl, newContent);

			flpContents.Controls.Add(ctl);
		}

		// ============================================================
		// FACTORY TẠO CONTROL THEO TYPE
		// ============================================================
		private Control CreateControlByType(string? type)
		{
			return type switch
			{
				"Video" => new ContentVideoControl(),
				"FlashcardSet" => new ContentFlashcardControl(),
				"Test" => new ContentTestControl(),
				_ => new ContentTheoryControl()
			};
		}

		// ============================================================
		// XỬ LÝ THAY ĐỔI LOẠI CONTENT
		// ============================================================
		private void OnContentTypeChanged(Control oldControl, string newType, LessonContentBuilderViewModel contentVm)
		{
			// Save current data from old control
			if (oldControl is IContentControl oldIc)
			{
				var savedData = oldIc.SaveToViewModel();
				contentVm.Title = savedData.Title;
				contentVm.Body = savedData.Body;
				contentVm.VideoUrl = savedData.VideoUrl;
			}

			// Update content type
			contentVm.ContentType = newType;

			// Create new control
			var newControl = CreateControlByType(newType);
			if (newControl is IContentControl newIc)
			{
				newIc.LoadFromViewModel(contentVm);
				// attach same VM so IDs preserved
				newControl.Tag = contentVm;
			}

			// Add event handler for new control
			if (newControl is IContentControl newContentCtl)
			{
				newContentCtl.DeleteRequested += (s) => OnContentDeleteRequested(newControl, contentVm);
			}

			if (newControl is ContentTheoryControl theoryCtl)
			{
				theoryCtl.ContentTypeChanged += (s, type) => OnContentTypeChanged(newControl, type, contentVm);
			}
			else if (newControl is ContentVideoControl videoCtl)
			{
				videoCtl.ContentTypeChanged += (s, type) => OnContentTypeChanged(newControl, type, contentVm);
			}
			else if (newControl is ContentFlashcardControl flashcardCtl)
			{
				flashcardCtl.ContentTypeChanged += (s, type) => OnContentTypeChanged(newControl, type, contentVm);
			}
			else if (newControl is ContentTestControl testCtl)
			{
				testCtl.ContentTypeChanged += (s, type) => OnContentTypeChanged(newControl, type, contentVm);
			}

			// Replace control in FlowLayoutPanel
			var index = flpContents.Controls.IndexOf(oldControl);
			flpContents.Controls.RemoveAt(index);
			flpContents.Controls.Add(newControl);
			flpContents.Controls.SetChildIndex(newControl, index);

			// Dispose old control
			oldControl.Dispose();
		}

		// ============================================================
		// XỬ LÝ XÓA CONTENT
		// ============================================================
		private void OnContentDeleteRequested(Control control, LessonContentBuilderViewModel contentVm)
		{
			if (MessageBox.Show("Bạn có chắc muốn xóa nội dung này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				// Remove from UI
				flpContents.Controls.Remove(control);
				
				// Remove from ViewModel
				if (cmbLessonSelector.SelectedItem is ComboItem item)
				{
					var lesson = item.Lesson ?? _vm.Chapters[item.ChapterIndex].Lessons[item.LessonIndex];
					lesson.Contents.Remove(contentVm);
				}
				
				// Dispose control
				control.Dispose();
			}
		}

		public void OnEnter() { }
		public void OnLeaving() { }

		// ============================================================
		// COMBO ITEM
		// ============================================================
		private class ComboItem
		{
			public string Text { get; set; } = "";
			public int ChapterIndex { get; set; }
			public int LessonIndex { get; set; }
			public LessonBuilderViewModel? Lesson { get; set; }  // Direct reference

			public override string ToString() => Text;
		}
	}
}
