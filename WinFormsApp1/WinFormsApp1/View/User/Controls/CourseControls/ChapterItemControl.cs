using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.User.Controls.CourseControls
{
    public partial class ChapterItemControl : UserControl
    {
        public int? ChapterId { get; private set; }

        public event EventHandler? AddLessonClicked;
        public event EventHandler? RemoveChapterClicked;

        private string _baseTitle = string.Empty;

        public ChapterItemControl()
        {
            InitializeComponent();
            // wire designer button click to our events
            if (btnAddLesson != null)
            {
                btnAddLesson.Click += (s, e) => AddLessonClicked?.Invoke(this, EventArgs.Empty);
            }

            if (btnRemoveChapter != null)
            {
                btnRemoveChapter.Click += BtnRemoveChapter_Click;
            }
        }

        private void BtnRemoveChapter_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn xóa chương này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // raise event so parent can handle removal from data model
                RemoveChapterClicked?.Invoke(this, EventArgs.Empty);

                // also remove this control from parent container UI
                this.Parent?.Controls.Remove(this);
                this.Dispose();
            }
        }

        // Load from ChapterBuilderViewModel -- will clear existing lesson items and create simple labels
        public void LoadFromViewModel(ChapterBuilderViewModel vm, string? imageUrl = null)
        {
            if (vm == null) return;
            _originalViewModel = vm; // Store reference to preserve data
            ChapterId = vm.ChapterId;
            _baseTitle = vm.Title ?? "(Không tên)";
            // keep title unchanged
            lblTitle.Text = _baseTitle;
            flpLessons.Controls.Clear();

            if (vm.Lessons != null && vm.Lessons.Count > 0)
            {
                // show count label in the lessons area (do not modify the chapter title)
                var countLabel = new Label { Text = $"({vm.Lessons.Count} bài)", AutoSize = false, Width = 660, Height = 28, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Color.Gray, Tag = "lesson-count" };
                flpLessons.Controls.Add(countLabel);

                foreach (var ls in vm.Lessons)
                {
                    var item = CreateLessonItem(ls.Title);
                    flpLessons.Controls.Add(item);
                }
            }
            else
            {
                // show placeholder (no lessons)
                var ph = new Label { Text = "(Chưa có bài học)", AutoSize = false, Width = 660, Height = 28, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Color.Gray, Tag = "placeholder" };
                flpLessons.Controls.Add(ph);
            }

            RefreshLessonCount();
        }

        // Create a visual for a lesson (panel with editable textbox and delete button)
        private Control CreateLessonItem(string title)
        {
            var p = new Panel { Width = 660, Height = 36, BackColor = Color.Transparent, Margin = new Padding(0, 0, 0, 6) };
            var txt = new TextBox
            {
                Text = title ?? "(Không tên)",
                AutoSize = false,
                Width = 560,
                Height = 28,
                Location = new Point(4, 4),
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Black
            };
            // optional: allow Enter to move focus
            txt.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    p.Parent?.Focus();
                }
            };

            var btnDel = new Button { Text = "Xóa", Width = 80, Height = 35, Location = new Point(560, 4), BackColor = Color.FromArgb(242, 75, 75), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnDel.FlatAppearance.BorderSize = 0;
            btnDel.Click += (s, e) => { flpLessons.Controls.Remove(p); RefreshLessonCount(); };

            p.Controls.Add(txt);
            p.Controls.Add(btnDel);
            return p;
        }

        // Allow parent to programmatically add a new lesson item and focus its textbox
        public void AddNewLesson(string? title = null)
        {
            // remove placeholder if present
            var placeholder = flpLessons.Controls.OfType<Label>().FirstOrDefault(lbl => (lbl.Tag as string) == "placeholder");
            if (placeholder != null) flpLessons.Controls.Remove(placeholder);

            // compute sequential number
            var currentCount = flpLessons.Controls.OfType<Panel>().Count();
            var defaultTitle = title ?? $"Bài {currentCount + 1}";

            var item = CreateLessonItem(defaultTitle);
            flpLessons.Controls.Add(item);

            // focus the newly added textbox after layout
            var txt = item.Controls.OfType<TextBox>().FirstOrDefault();
            if (txt != null)
            {
                this.BeginInvoke((Action)(() => {
                    txt.Focus();
                    txt.SelectAll();
                }));
            }

            RefreshLessonCount();
        }

        private ChapterBuilderViewModel? _originalViewModel;
        
        // Convert current UI back to ChapterBuilderViewModel
        public ChapterBuilderViewModel ToViewModel()
        {
            var ch = new ChapterBuilderViewModel { ChapterId = ChapterId, Title = _baseTitle };
            
            var panelIndex = 0;
            foreach (var c in flpLessons.Controls.OfType<Panel>())
            {
                // prefer TextBox inside the panel
                var txt = c.Controls.OfType<TextBox>().FirstOrDefault();
                var title = txt?.Text ?? c.Controls.OfType<Label>().FirstOrDefault()?.Text ?? "";
                
                // Try to preserve existing lesson data if available
                LessonBuilderViewModel lesson;
                if (_originalViewModel?.Lessons != null && panelIndex < _originalViewModel.Lessons.Count)
                {
                    lesson = _originalViewModel.Lessons[panelIndex];
                    lesson.Title = title; // Update title from UI
                }
                else
                {
                    lesson = new LessonBuilderViewModel { Title = title };
                }
                
                ch.Lessons.Add(lesson);
                panelIndex++;
            }
            return ch;
        }

        private void RefreshLessonCount()
        {
            // count panels = number of lesson items
            var count = flpLessons.Controls.OfType<Panel>().Count();

            // remove any existing lesson-count label
            var existingCountLabel = flpLessons.Controls.OfType<Label>().FirstOrDefault(lbl => (lbl.Tag as string) == "lesson-count");
            if (existingCountLabel != null)
                flpLessons.Controls.Remove(existingCountLabel);

            // if no lessons, ensure placeholder exists
            if (count == 0)
            {
                // remove any panels just in case
                // show placeholder if not present
                var placeholder = flpLessons.Controls.OfType<Label>().FirstOrDefault(lbl => (lbl.Tag as string) == "placeholder");
                if (placeholder == null)
                {
                    var ph = new Label { Text = "(Chưa có bài học)", AutoSize = false, Width = 660, Height = 28, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Color.Gray, Tag = "placeholder" };
                    flpLessons.Controls.Add(ph);
                }
            }
            else
            {
                // remove placeholder if present
                var placeholder = flpLessons.Controls.OfType<Label>().FirstOrDefault(lbl => (lbl.Tag as string) == "placeholder");
                if (placeholder != null) flpLessons.Controls.Remove(placeholder);

                // add/update count label at top
                var countLabel = new Label { Text = $"({count} bài)", AutoSize = false, Width = 660, Height = 28, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Color.Gray, Tag = "lesson-count" };
                flpLessons.Controls.Add(countLabel);
                flpLessons.Controls.SetChildIndex(countLabel, 0);
            }

            // do NOT modify chapter title; keep _baseTitle as-is
            lblTitle.Text = _baseTitle;
        }
    }
}
