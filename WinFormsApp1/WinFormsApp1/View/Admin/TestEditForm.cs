using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    public class TestEditForm : Form
    {
        private TextBox txtTitle;
        private TextBox txtDescription;
        private TextBox txtTimeLimit;
        private Button btnSave;
        private Button btnCancel;
        private AdminController _controller;
        private int? _editingId;

        public TestEditForm(int? editingTestId = null)
        {
            _editingId = editingTestId;
            _controller = new AdminController();
            InitializeComponent();
            if (editingTestId.HasValue)
            {
                _ = LoadTest(editingTestId.Value);
            }
        }

        private void InitializeComponent()
        {
            Text = _editingId.HasValue ? "S?a bài ki?m tra" : "Thêm bài ki?m tra";
            Size = new Size(700, 480);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            BackColor = Color.White;

            int left = 20;
            int top = 20;

            var lblTitle = new Label { Text = "Tiêu ??", Location = new Point(left, top), AutoSize = true };
            txtTitle = new TextBox { Location = new Point(left, top + 24), Size = new Size(640, 30) };
            top += 70;

            var lblTime = new Label { Text = "Th?i gian (phút)", Location = new Point(left, top), AutoSize = true };
            txtTimeLimit = new TextBox { Location = new Point(left, top + 24), Size = new Size(200, 30) };
            top += 70;

            var lblDesc = new Label { Text = "Mô t?", Location = new Point(left, top), AutoSize = true };
            txtDescription = new TextBox { Location = new Point(left, top + 24), Size = new Size(640, 120), Multiline = true, ScrollBars = ScrollBars.Vertical };
            top += 160;

            btnSave = new Button { Text = "L?u", Size = new Size(120, 40), Location = new Point(20, top) };
            btnCancel = new Button { Text = "H?y", Size = new Size(120, 40), Location = new Point(160, top) };
            btnSave.Click += async (s, e) => await OnSave();
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[] { lblTitle, txtTitle, lblTime, txtTimeLimit, lblDesc, txtDescription, btnSave, btnCancel });
        }

        private async Task LoadTest(int id)
        {
            try
            {
                var test = await _controller.GetTestByIdAsync(id);
                if (test != null)
                {
                    txtTitle.Text = test.Title;
                    txtTimeLimit.Text = test.TimeLimitSec.HasValue ? (test.TimeLimitSec.Value / 60).ToString() : string.Empty;
                    txtDescription.Text = test.Description;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi t?i bài ki?m tra: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private async Task OnSave()
        {
            try
            {
                var title = txtTitle.Text?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Tiêu ?? không ???c ?? tr?ng.");
                if (title.Length > 200) throw new ArgumentException("Tiêu ?? quá dài (t?i ?a 200 ký t?).");

                int timeLimit = 0;
                if (!string.IsNullOrWhiteSpace(txtTimeLimit.Text) && !int.TryParse(txtTimeLimit.Text, out timeLimit))
                    throw new ArgumentException("Th?i gian không h?p l?.");
                if (timeLimit < 0) throw new ArgumentException("Th?i gian ph?i l?n h?n ho?c b?ng 0.");

                var test = new Test
                {
                    Title = title,
                    Description = txtDescription.Text,
                    TimeLimitSec = timeLimit > 0 ? timeLimit * 60 : null,
                    OwnerId = 1,
                    CreatedAt = DateTime.UtcNow
                };

                bool ok;
                if (_editingId.HasValue)
                {
                    test.TestId = _editingId.Value;
                    ok = await _controller.UpdateTestAsync(test);
                }
                else
                {
                    ok = await _controller.CreateTestAsync(test);
                }

                if (ok)
                {
                    ToastHelper.Show(this.Owner ?? this, "L?u thành công!");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("L?u th?t b?i.", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "L?i xác th?c", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi l?u: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
