using System.Windows.Forms;
using WinFormsApp1.ViewModels;
using System.Drawing;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    public partial class ContentTheoryControl : UserControl, IContentControl
    {
        public event Action<object, string>? ContentTypeChanged;
        public event Action<object>? DeleteRequested;
        
        public ContentTheoryControl()
        {
            this.Width = 700; this.Height = 200; this.Margin = new Padding(0, 0, 0, 10);
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

            // default selection
            cboContentType.SelectedIndexChanged += (s, e) => OnContentTypeChanged();
            if (cboContentType.Items.Count > 0) cboContentType.SelectedIndex = 0;
        }

		private void OnContentTypeChanged()
		{
			var type = cboContentType.SelectedItem?.ToString();

			// ALWAYS FIRE EVENT
			if (type != null)
				ContentTypeChanged?.Invoke(this, type);

			// local UI change only for Theory control
			if (type == "Video")
			{
				lblBody.Visible = false;
				txtBody.Visible = false;
			}
			else
			{
				lblBody.Visible = true;
				txtBody.Visible = true;
			}
		}

		public void LoadFromViewModel(LessonContentBuilderViewModel vm)
        {
            if (vm == null) return;
            var contentType = string.IsNullOrEmpty(vm.ContentType) ? "Theory" : vm.ContentType;
            
            // Find and set the correct item
            for (int i = 0; i < cboContentType.Items.Count; i++)
            {
                if (cboContentType.Items[i].ToString() == contentType)
                {
                    cboContentType.SelectedIndex = i;
                    break;
                }
            }
            
            txtTitle.Text = vm.Title ?? string.Empty;
            txtBody.Text = vm.Body ?? string.Empty;
        }

        public LessonContentBuilderViewModel SaveToViewModel()
        {
            return new LessonContentBuilderViewModel
            {
                ContentType = cboContentType.SelectedItem?.ToString() ?? "Theory",
                Title = txtTitle.Text.Trim(),
                Body = txtBody.Text
            };
        }
    }
}