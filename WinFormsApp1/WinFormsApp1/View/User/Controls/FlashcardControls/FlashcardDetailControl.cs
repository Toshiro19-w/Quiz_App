using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
    public partial class FlashcardDetailControl : UserControl
    {
        private readonly int _setId;
		private readonly FlashcardDetailSource _source;
        private Models.Entities.FlashcardSet _flashcardSet;
        private readonly FlashcardController _flashcardController;

		public FlashcardDetailControl(int setId, FlashcardDetailSource source = FlashcardDetailSource.PublicLibrary)
        {
            _setId = setId;
			_source = source;
            _flashcardController = new FlashcardController();
            InitializeComponent();
        }

        private async void FlashcardDetailControl_Load(object sender, EventArgs e)
        {
            await LoadFlashcardSetDetails();
        }

        private async System.Threading.Tasks.Task LoadFlashcardSetDetails()
        {
            try
            {
                _flashcardSet = await _flashcardController.GetFlashcardSetByIdAsync(_setId);

                if (_flashcardSet == null)
                {
                    MessageBox.Show("Không tìm thấy bộ flashcard!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DisplayFlashcardSetInfo();
                LoadFlashcardsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayFlashcardSetInfo()
        {
            // Breadcrumb
            lblBreadcrumb.Text = $"Flashcards / {_flashcardSet.Title}";

            // Title
            lblTitle.Text = _flashcardSet.Title;

            // Description
            if (!string.IsNullOrEmpty(_flashcardSet.Description))
            {
                rtbDescription.Text = _flashcardSet.Description;
            }
            else
            {
                rtbDescription.Text = "Chưa có mô tả";
            }

            // Author info
            lblAuthorName.Text = _flashcardSet.Owner?.FullName ?? "Unknown";
            lblAuthorEmail.Text = _flashcardSet.Owner?.Email ?? "";

            // Statistics in info panel
            lblCardCount.Text = $"Số thẻ: {_flashcardSet.Flashcards.Count}";
            lblCreatedDate.Text = $"Tạo lúc: {_flashcardSet.CreatedAt:dd/MM/yyyy}";

            // Language
            if (!string.IsNullOrEmpty(_flashcardSet.Language))
            {
                lblLanguage.Text = $"Ngôn ngữ: {_flashcardSet.Language}";
            }

			UpdateEditButtonVisibility();
        }

		private void UpdateEditButtonVisibility()
		{
			var currentUserId = AuthHelper.CurrentUser?.UserId;
			btnEdit.Visible = currentUserId.HasValue && _flashcardSet.OwnerId == currentUserId.Value;
			btnViewDifferent.Location = btnEdit.Visible ? new Point(25, 150) : new Point(25, 95);
			pnlActions.Height = btnEdit.Visible ? 210 : 160;
		}

        private void LoadFlashcardsList()
        {
            flowFlashcards.Controls.Clear();

            // Header label
            var lblHeader = new Label
            {
                Text = $"Danh sách thẻ ({_flashcardSet.Flashcards.Count})",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(0, 10, 0, 10)
            };
            flowFlashcards.Controls.Add(lblHeader);

            var flashcards = _flashcardSet.Flashcards.OrderBy(f => f.OrderIndex).ToList();

			foreach (var flashcard in flashcards)
			{
				var card = new FlashcardItemDisplayControl(flashcard);
				flowFlashcards.Controls.Add(card);
			}

            if (!flashcards.Any())
            {
                var lblEmpty = new Label
                {
                    Text = "Chưa có thẻ nào trong bộ này",
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Padding = new Padding(0, 20, 0, 20)
                };
                flowFlashcards.Controls.Add(lblEmpty);
            }
        }


        private void btnStartLearning_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có flashcard nào không
            if (_flashcardSet == null || _flashcardSet.Flashcards.Count == 0)
            {
                MessageBox.Show("Bộ flashcard này chưa có thẻ nào để học!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Mở form học flashcard
            var studyForm = new FlashcardStudyForm(_setId);
            studyForm.ShowDialog();
        }

        private void btnViewDifferent_Click(object sender, EventArgs e)
        {
            // Navigate back to FlashcardControl
            var form = this.FindForm();
            if (form == null) return;

            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

            if (mainPanel == null)
            {
                mainPanel = this.Parent as Panel;
            }

            if (mainPanel == null) return;

            mainPanel.Controls.Clear();

			Control targetControl = _source switch
			{
				FlashcardDetailSource.MyFlashcards => new MyFlashcardsControl(),
				FlashcardDetailSource.Library => new LibraryControl(showFlashcards: true),
				_ => new FlashcardControl()
			};
			targetControl.Dock = DockStyle.Fill;
			mainPanel.Controls.Add(targetControl);
        }

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (_flashcardSet == null)
			{
				return;
			}

			var form = this.FindForm();
			if (form == null) return;

			var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

			if (mainPanel == null)
			{
				mainPanel = this.Parent as Panel;
			}

			if (mainPanel == null) return;

			mainPanel.Controls.Clear();

			var editControl = new EditFlashcardControl(_flashcardSet.SetId);
			editControl.Dock = DockStyle.Fill;
			mainPanel.Controls.Add(editControl);
		}

        private Control FindControlRecursive(Control parent, string name)
        {
            foreach (Control c in parent.Controls)
            {
                if (string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase)) return c;
                var found = FindControlRecursive(c, name);
                if (found != null) return found;
            }
            return null;
        }
    }

	public enum FlashcardDetailSource
	{
		PublicLibrary,
		Library,
		MyFlashcards
	}
}
