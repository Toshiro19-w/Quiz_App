using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.ViewModels;
using WinFormsApp1.View.User.Controls.CourseControls.ContentControls;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
	public partial class Step3_ContentControl : UserControl, IStepControl
	{
		private CourseBuilderViewModel? _vm;
		private int _currentChapterIndex = -1;
		private int _currentLessonIndex = -1;
		private int _previousComboIndex = -1;			
		
		private LessonContentBuilderViewModel? _selectedContent;
		private Control? _currentEditor;

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
			flpContentList.Controls.Clear();
			pnlEditor.Controls.Clear();
			_currentChapterIndex = -1;
			_currentLessonIndex = -1;
			_previousComboIndex = -1;
			_selectedContent = null;
			_currentEditor = null;

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
			if (!SaveCurrentLesson())
			{
				throw new InvalidOperationException();
			}
		}

		// ============================================================
		// CHUYỂN BÀI HỌC
		// ============================================================
		private void ChangeSelectedLesson()
		{
			int newComboIndex = cmbLessonSelector.SelectedIndex;

			if (_currentChapterIndex >= 0 && _currentLessonIndex >= 0)
			{
				if (!SaveCurrentLesson())
				{
					if (_previousComboIndex >= 0 && _previousComboIndex < cmbLessonSelector.Items.Count)
					{
						cmbLessonSelector.SelectedIndex = _previousComboIndex;
					}
					else
					{
						cmbLessonSelector.SelectedIndex = -1;
					}
					return;
				}
			}

			if (cmbLessonSelector.SelectedItem is not ComboItem item)
			{
				return;
			}

			_previousComboIndex = newComboIndex;
			_currentChapterIndex = item.ChapterIndex;
			_currentLessonIndex = item.LessonIndex;

			LoadLessonContents();
		}

		// ============================================================
		// LƯU BÀI HỌC HIỆN TẠI
		// ============================================================
		private bool SaveCurrentLesson()
		{
			if (_vm == null) return true;
			if (_currentChapterIndex < 0 || _currentLessonIndex < 0) return true;

			// Save editor trước nếu có
			if (!SaveCurrentEditor()) return false;

			var chapter = _vm.Chapters[_currentChapterIndex];
			var lesson = chapter.Lessons[_currentLessonIndex];

			Debug.WriteLine($"[Step3] SaveCurrentLesson: Lesson '{lesson.Title}'");

			// Contents đã được cập nhật trực tiếp trong lesson.Contents
			// không cần rebuild lại list

			return true;
		}

		// ============================================================
		// LƯU EDITOR HIỆN TẠI
		// ============================================================
		private bool SaveCurrentEditor()
		{
			if (_currentEditor is IContentControl ic && _selectedContent != null)
			{
				try
				{
					var saved = ic.SaveToViewModel();
					
					// Copy dữ liệu từ editor vào content hiện tại
					_selectedContent.Title = saved.Title;
					_selectedContent.Body = saved.Body;
					_selectedContent.VideoUrl = saved.VideoUrl;
					_selectedContent.FlashcardSetTitle = saved.FlashcardSetTitle;
					_selectedContent.FlashcardSetDesc = saved.FlashcardSetDesc;
					_selectedContent.Flashcards = saved.Flashcards;
					_selectedContent.TestTitle = saved.TestTitle;
					_selectedContent.TestDesc = saved.TestDesc;
					_selectedContent.TimeLimitMinutes = saved.TimeLimitMinutes;
					_selectedContent.MaxAttempts = saved.MaxAttempts;
					_selectedContent.Questions = saved.Questions;

					Debug.WriteLine($"[Step3] Saved editor: Type={saved.ContentType}, Title='{saved.Title}'");
					
					// Refresh display của item trong list
					RefreshContentList();
					
					return true;
				}
				catch (InvalidOperationException ex)
				{
					Debug.WriteLine($"[Step3] Validation failed: {ex.Message}");
					return false;
				}
			}
			return true;
		}

		// ============================================================
		// LOAD NỘI DUNG CỦA BÀI ĐANG CHỌN
		// ============================================================
		private void LoadLessonContents()
		{
			if (_vm == null) return;
			if (cmbLessonSelector.SelectedItem is not ComboItem item) return;

			flpContentList.SuspendLayout();
			flpContentList.Controls.Clear();

			var lesson = item.Lesson ?? _vm.Chapters[item.ChapterIndex].Lessons[item.LessonIndex];

			Debug.WriteLine($"[Step3] LoadLessonContents: Lesson '{lesson.Title}', Contents: {lesson.Contents.Count}");

			// Tạo item controls cho mỗi content
			foreach (var contentVm in lesson.Contents)
			{
				var itemControl = new ContentItemControl(contentVm);
				itemControl.ItemClicked += OnContentItemClicked;
				itemControl.DeleteRequested += OnContentDeleteRequested;
				flpContentList.Controls.Add(itemControl);
			}

			flpContentList.ResumeLayout(true);

			// Clear editor
			_selectedContent = null;
			_currentEditor = null;
			pnlEditor.Controls.Clear();
			var lblRightTitle = (Label)splitContainer.Panel2.Controls.OfType<Panel>().First().Controls[0];
			lblRightTitle.Text = $"Chỉnh sửa nội dung (0/{lesson.Contents.Count})";

			// Tự động chọn content đầu tiên nếu có
			if (lesson.Contents.Count > 0)
			{
				SelectContent(lesson.Contents[0]);
			}
		}

		// ============================================================
		// CHỌN CONTENT ĐỂ CHỈNH SỬA
		// ============================================================
		private void OnContentItemClicked(object? sender, LessonContentBuilderViewModel contentVm)
		{
			SelectContent(contentVm);
		}

		private void SelectContent(LessonContentBuilderViewModel contentVm)
		{
			// Lưu editor hiện tại trước
			if (!SaveCurrentEditor())
			{
				return; // Không cho phép chuyển nếu validate fail
			}

			_selectedContent = contentVm;

			// Update selected state cho các items
			foreach (Control ctrl in flpContentList.Controls)
			{
				if (ctrl is ContentItemControl contentItem)
				{
					contentItem.IsSelected = (contentItem.ContentViewModel == contentVm);
				}
			}

			// Load editor tương ứng
			LoadEditor(contentVm);

			// Update title
			if (cmbLessonSelector.SelectedItem is ComboItem comboItem)
			{
				var lesson = comboItem.Lesson ?? _vm.Chapters[comboItem.ChapterIndex].Lessons[comboItem.LessonIndex];
				int index = lesson.Contents.IndexOf(contentVm) + 1;
				var lblRightTitle = (Label)splitContainer.Panel2.Controls.OfType<Panel>().First().Controls[0];
				lblRightTitle.Text = $"Chỉnh sửa nội dung ({index}/{lesson.Contents.Count})";
			}
		}

		// ============================================================
		// LOAD EDITOR CHO CONTENT
		// ============================================================
		private void LoadEditor(LessonContentBuilderViewModel contentVm)
		{
			pnlEditor.SuspendLayout();
			pnlEditor.Controls.Clear();

			_currentEditor = CreateEditorByType(contentVm.ContentType);
			
			if (_currentEditor is IContentControl ic)
			{
				ic.LoadFromViewModel(contentVm);
				
				// Gắn event ContentTypeChanged
				if (_currentEditor is ContentTheoryControl theoryCtl)
				{
					theoryCtl.ContentTypeChanged += OnEditorContentTypeChanged;
				}
				else if (_currentEditor is ContentVideoControl videoCtl)
				{
					videoCtl.ContentTypeChanged += OnEditorContentTypeChanged;
				}
				else if (_currentEditor is ContentFlashcardControl flashcardCtl)
				{
					flashcardCtl.ContentTypeChanged += OnEditorContentTypeChanged;
				}
				else if (_currentEditor is ContentTestControl testCtl)
				{
					testCtl.ContentTypeChanged += OnEditorContentTypeChanged;
				}

				// Không gắn DeleteRequested ở đây vì đã có nút xóa ở ContentItemControl
			}

			// THAY ĐỔI: Không dùng Dock.Fill để cho phép scroll
			// Đặt Location và Width thủ công, để Height tự nhiên
			_currentEditor.Location = new System.Drawing.Point(0, 0);
			_currentEditor.Width = pnlEditor.ClientSize.Width - pnlEditor.Padding.Horizontal;
			// Không set Height, để control tự xác định chiều cao của nó
			
			// Đăng ký event để adjust width khi panel resize
			pnlEditor.Resize += (s, e) => {
				if (_currentEditor != null && pnlEditor.Controls.Contains(_currentEditor))
				{
					_currentEditor.Width = pnlEditor.ClientSize.Width - pnlEditor.Padding.Horizontal;
				}
			};
			
			pnlEditor.Controls.Add(_currentEditor);
			pnlEditor.ResumeLayout(true);
			
			// Force scroll to top khi load editor mới
			pnlEditor.AutoScrollPosition = new System.Drawing.Point(0, 0);
		}

		// ============================================================
		// XỬ LÝ THAY ĐỔI LOẠI CONTENT
		// ============================================================
		private void OnEditorContentTypeChanged(object? sender, string newType)
		{
			if (_selectedContent == null) return;

			// Lưu dữ liệu từ editor cũ
			if (_currentEditor is IContentControl oldIc)
			{
				try
				{
					var savedData = oldIc.SaveToViewModel();
					_selectedContent.Title = savedData.Title;
					_selectedContent.Body = savedData.Body;
					_selectedContent.VideoUrl = savedData.VideoUrl;
				}
				catch (InvalidOperationException)
				{
					// ignore validation errors when changing content type
				}
			}

			// Cập nhật content type
			_selectedContent.ContentType = newType;

			// Load lại editor mới
			LoadEditor(_selectedContent);

			// Refresh display
			RefreshContentList();
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

			// Lưu editor hiện tại trước
			if (!SaveCurrentEditor())
			{
				return;
			}

			var lesson = item.Lesson ?? _vm.Chapters[item.ChapterIndex].Lessons[item.LessonIndex];

			var newContent = new LessonContentBuilderViewModel
			{
				Title = "",
				ContentType = "Theory",
				Body = "",
				OrderIndex = lesson.Contents.Count + 1
			};

			lesson.Contents.Add(newContent);

			// Tạo item control
			var itemControl = new ContentItemControl(newContent);
			itemControl.ItemClicked += OnContentItemClicked;
			itemControl.DeleteRequested += OnContentDeleteRequested;
			
			flpContentList.SuspendLayout();
			flpContentList.Controls.Add(itemControl);
			flpContentList.ResumeLayout(true);

			// Tự động chọn content mới
			SelectContent(newContent);
		}

		// ============================================================
		// XÓA CONTENT
		// ============================================================
		private void OnContentDeleteRequested(object? sender, LessonContentBuilderViewModel contentVm)
		{
			if (MessageBox.Show("Bạn có chắc muốn xóa nội dung này?", "Xác nhận", 
				MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				if (cmbLessonSelector.SelectedItem is not ComboItem item) return;

				var lesson = item.Lesson ?? _vm.Chapters[item.ChapterIndex].Lessons[item.LessonIndex];
				lesson.Contents.Remove(contentVm);

				// Nếu đang chỉnh sửa content này thì clear editor
				if (_selectedContent == contentVm)
				{
					_selectedContent = null;
					_currentEditor = null;
					pnlEditor.Controls.Clear();
				}

				// Refresh list
				LoadLessonContents();
			}
		}

		// ============================================================
		// REFRESH DANH SÁCH CONTENT
		// ============================================================
		private void RefreshContentList()
		{
			foreach (Control ctrl in flpContentList.Controls)
			{
				if (ctrl is ContentItemControl contentItem)
				{
					contentItem.RefreshDisplay();
				}
			}
		}

		// ============================================================
		// FACTORY TẠO EDITOR THEO TYPE
		// ============================================================
		private Control CreateEditorByType(string? type)
		{
			return type switch
			{
				"Video" => new ContentVideoControl(),
				"FlashcardSet" => new ContentFlashcardControl(),
				"Test" => new ContentTestControl(),
				_ => new ContentTheoryControl()
			};
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
			public LessonBuilderViewModel? Lesson { get; set; }

			public override string ToString() => Text;
		}
	}
}
