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

        public ChapterItemControl()
        {
            InitializeComponent();
            // wire designer button click to our event
            if (btnAddLesson != null)
            {
                btnAddLesson.Click += (s, e) => AddLessonClicked?.Invoke(this, EventArgs.Empty);
            }
        }

        // Load from ChapterBuilderViewModel -- will clear existing lesson items and create simple labels
        public void LoadFromViewModel(ChapterBuilderViewModel vm, string? imageUrl = null)
        {
            if (vm == null) return;
            ChapterId = vm.ChapterId;
            lblTitle.Text = vm.Title ?? "(Không tên)";
            flpLessons.Controls.Clear();

            if (!string.IsNullOrEmpty(imageUrl))
            {
                try { picHeader.Load(imageUrl); } catch { /* ignore */ }
            }
            else
            {
                picHeader.Image = null;
            }

            if (vm.Lessons != null && vm.Lessons.Count > 0)
            {
                foreach (var ls in vm.Lessons)
                {
                    var item = CreateLessonItem(ls.Title);
                    flpLessons.Controls.Add(item);
                }
            }
            else
            {
                // show placeholder
                var ph = new Label { Text = "(Ch?a có bài h?c)", AutoSize = false, Width = 660, Height = 28, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Color.Gray };
                flpLessons.Controls.Add(ph);
            }
        }

        // Create a visual for a lesson (simple panel with label)
        private Control CreateLessonItem(string title)
        {
            var p = new Panel { Width = 660, Height = 36, BackColor = Color.Transparent, Margin = new Padding(0, 0, 0, 6) };
            var lbl = new Label { Text = title ?? "(Không tên)", AutoSize = false, Width = 560, Height = 32, Location = new Point(4, 2), TextAlign = ContentAlignment.MiddleLeft };
            var btnDel = new Button { Text = "Xóa", Width = 80, Height = 28, Location = new Point(560, 2), BackColor = Color.FromArgb(242, 75, 75), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnDel.FlatAppearance.BorderSize = 0;
            btnDel.Click += (s, e) => { flpLessons.Controls.Remove(p); };

            p.Controls.Add(lbl);
            p.Controls.Add(btnDel);
            return p;
        }

        // Convert current UI back to ChapterBuilderViewModel
        public ChapterBuilderViewModel ToViewModel()
        {
            var ch = new ChapterBuilderViewModel { ChapterId = ChapterId, Title = lblTitle.Text };
            foreach (var c in flpLessons.Controls.OfType<Panel>())
            {
                var lbl = c.Controls.OfType<Label>().FirstOrDefault();
                if (lbl != null)
                    ch.Lessons.Add(new LessonBuilderViewModel { Title = lbl.Text });
            }
            return ch;
        }
    }
}
