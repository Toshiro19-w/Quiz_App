using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.ViewModels;
using System.Drawing;
using System.Collections.Generic;
using WinFormsApp1.View.User.Controls.CourseControls.ContentControls;

namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    public partial class Step3_ContentControl : UserControl, IStepControl
    {
        public Step3_ContentControl()
        {
            InitializeComponent();

            // wire basic navigation
            btnPrev.Click += (s, e) => OnPrevRequested?.Invoke(this, EventArgs.Empty);
            btnNext.Click += (s, e) => OnNextRequested?.Invoke(this, EventArgs.Empty);

            // wire selection changes
            cmbLessonSelector.SelectedIndexChanged += (s, e) => LoadSelectedLessonContents();

            // add content button
            btnAddContent.Click += (s, e) => AddNewContentControl();
        }

        public event EventHandler? OnPrevRequested;
        public event EventHandler? OnNextRequested;

        private CourseBuilderViewModel? _vm;

        // Add an empty new content control (default Theory)
        private void AddNewContentControl()
        {
            var ctl = new ContentTheoryControl();
            flpContents.Controls.Add(ctl);
        }

        public void LoadFromViewModel(CourseBuilderViewModel vm)
        {
            _vm = vm;
            cmbLessonSelector.Items.Clear();
            flpContents.Controls.Clear();

            if (vm?.Chapters == null || vm.Chapters.Count == 0)
            {
                cmbLessonSelector.Enabled = false;
                btnAddContent.Enabled = false;
                return;
            }

            cmbLessonSelector.Enabled = true;
            btnAddContent.Enabled = true;

            // populate lesson selector with pairs (chapterIndex, lessonIndex)
            for (int ch = 0; ch < vm.Chapters.Count; ch++)
            {
                var chapter = vm.Chapters[ch];
                if (chapter.Lessons == null) continue;
                for (int ls = 0; ls < chapter.Lessons.Count; ls++)
                {
                    var lesson = chapter.Lessons[ls];
                    cmbLessonSelector.Items.Add(new ComboboxItem { Text = $"{chapter.Title} → {lesson.Title}", Value = (ch, ls) });
                }
            }

            if (cmbLessonSelector.Items.Count > 0) cmbLessonSelector.SelectedIndex = 0;
            else
            {
                cmbLessonSelector.Enabled = false;
            }

            LoadSelectedLessonContents();
        }

        private void LoadSelectedLessonContents()
        {
            flpContents.Controls.Clear();
            if (_vm == null) return;
            if (cmbLessonSelector.SelectedItem is not ComboboxItem item) return;

            if (item.Value is not ValueTuple<int, int> pair) return;
            var chIdx = pair.Item1;
            var lsIdx = pair.Item2;

            if (chIdx < 0 || chIdx >= _vm.Chapters.Count) return;
            var chapter = _vm.Chapters[chIdx];
            if (chapter.Lessons == null || lsIdx < 0 || lsIdx >= chapter.Lessons.Count) return;

            var lesson = chapter.Lessons[lsIdx];
            if (lesson.Contents == null || lesson.Contents.Count == 0)
            {
                // show one empty theory control by default
                flpContents.Controls.Add(new ContentTheoryControl());
                return;
            }

            foreach (var c in lesson.Contents)
            {
                UserControl ctl = c.ContentType switch
                {
                    "Video" => new ContentVideoControl(),
                    "FlashcardSet" => new ContentFlashcardControl(),
                    "Test" => new ContentTestControl(),
                    _ => new ContentTheoryControl()
                };

                // call loader if control supports it
                if (ctl is IContentControl ic) ic.LoadFromViewModel(c);
                flpContents.Controls.Add(ctl);
            }
        }

        public void SaveToViewModel(CourseBuilderViewModel vm)
        {
            if (vm == null) return;
            if (cmbLessonSelector.SelectedItem is not ComboboxItem item) return;
            if (item.Value is not ValueTuple<int, int> pair) return;
            var chIdx = pair.Item1;
            var lsIdx = pair.Item2;

            if (chIdx < 0 || chIdx >= vm.Chapters.Count) return;
            var chapter = vm.Chapters[chIdx];
            if (chapter.Lessons == null || lsIdx < 0 || lsIdx >= chapter.Lessons.Count) return;

            var lesson = chapter.Lessons[lsIdx];
            lesson.Contents.Clear();

            foreach (Control c in flpContents.Controls)
            {
                if (c is IContentControl ic)
                {
                    var model = ic.SaveToViewModel();
                    lesson.Contents.Add(model);
                }
            }
        }

        public void OnEnter() { }
        public void OnLeaving() { }

        // small helper for combo items
        private class ComboboxItem
        {
            public string Text { get; set; } = string.Empty;
            public object? Value { get; set; }
            public override string ToString() => Text;
        }
    }
}