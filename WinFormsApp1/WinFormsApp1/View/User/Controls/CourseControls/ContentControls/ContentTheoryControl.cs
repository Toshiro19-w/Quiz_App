using System.Windows.Forms;
using WinFormsApp1.ViewModels;
using System.Drawing;
using WinFormsApp1.Helpers;

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
			var vietnameseType = cboContentType.SelectedItem?.ToString();

			// ALWAYS FIRE EVENT - chuyển sang tiếng Anh
			if (vietnameseType != null)
			{
				var englishType = ContentTypeHelper.ToEnglish(vietnameseType);
				ContentTypeChanged?.Invoke(this, englishType);
			}

			// local UI change only for Theory control
			if (vietnameseType == "Video")
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
            
            // Chuyển sang tiếng Việt để hiển thị
            var vietnameseType = ContentTypeHelper.ToVietnamese(contentType);
            
            // Find and set the correct item
            for (int i = 0; i < cboContentType.Items.Count; i++)
            {
                if (cboContentType.Items[i].ToString() == vietnameseType)
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
            var vietnameseType = cboContentType.SelectedItem?.ToString() ?? "Lý thuyết";
            return new LessonContentBuilderViewModel
            {
                ContentType = ContentTypeHelper.ToEnglish(vietnameseType), // Lưu bằng tiếng Anh
                Title = txtTitle.Text.Trim(),
                Body = txtBody.Text
            };
        }
    }
}