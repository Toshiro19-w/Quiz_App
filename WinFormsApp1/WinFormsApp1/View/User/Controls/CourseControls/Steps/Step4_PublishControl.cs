using System;
using System.Windows.Forms;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    public partial class Step4_PublishControl : UserControl, IStepControl
    {
        public Step4_PublishControl()
        {
            InitializeComponent();
            btnSaveDraft.Click += (s, e) => OnSaveDraftRequested?.Invoke(this, EventArgs.Empty);
            btnPublish.Click += (s, e) => OnPublishRequested?.Invoke(this, EventArgs.Empty);
            // wire cancel if button exists
            if (this.Controls.ContainsKey("btnCancel"))
            {
                var btn = this.Controls["btnCancel"] as Button;
                if (btn != null) btn.Click += (s, e) => OnCancelRequested?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler? OnSaveDraftRequested;
        public event EventHandler? OnPublishRequested;
        public event EventHandler? OnCancelRequested;

        public void LoadFromViewModel(CourseBuilderViewModel vm)
        {
            pnlPreview.Controls.Clear();
            var lbl = new Label { Text = $"Tiêu đề: {vm?.Title}\nSlug: {vm?.Slug}\nChương: {vm?.Chapters?.Count ?? 0}", AutoSize = true, Location = new System.Drawing.Point(10, 10) };
            pnlPreview.Controls.Add(lbl);
        }

        public void SaveToViewModel(CourseBuilderViewModel vm) { }

        public void OnEnter() { }
        public void OnLeaving() { }
    }
}