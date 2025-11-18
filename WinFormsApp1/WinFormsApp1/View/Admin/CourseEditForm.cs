using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    public class CourseEditForm : Form
    {
        private TextBox txtTitle;
        private TextBox txtDescription;
        private TextBox txtPrice;
        private CheckBox chkPublished;
        private Button btnSave;
        private Button btnCancel;
        private AdminController _controller;
        private int? _editingId;

        public CourseEditForm(int? editingCourseId = null)
        {
            _editingId = editingCourseId;
            _controller = new AdminController();
            InitializeComponent();
            if (editingCourseId.HasValue)
            {
                LoadCourse(editingCourseId.Value).ConfigureAwait(false);
            }
        }

        private void InitializeComponent()
        {
            Text = _editingId.HasValue ? "S?a khóa h?c" : "Thêm khóa h?c";
            Size = new Size(700, 560);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            BackColor = Color.White;

            int left = 20;
            int top = 20;

            var lblTitle = new Label { Text = "Tên khóa h?c", Location = new Point(left, top), AutoSize = true };
            txtTitle = new TextBox { Location = new Point(left, top + 28), Size = new Size(640, 30) };
            top += 80;

            var lblDesc = new Label { Text = "Mô t?", Location = new Point(left, top - 20), AutoSize = true };
            txtDescription = new TextBox { Location = new Point(left, top), Size = new Size(640, 180), Multiline = true, ScrollBars = ScrollBars.Vertical };
            top += 200;

            var lblPrice = new Label { Text = "Giá (USD)", Location = new Point(left, top), AutoSize = true };
            txtPrice = new TextBox { Location = new Point(left + 0, top + 24), Size = new Size(200, 30) };

            chkPublished = new CheckBox { Text = "?ã xu?t b?n", Location = new Point(left + 220, top + 26), AutoSize = true };
            top += 70;

            btnSave = new Button { Text = "L?u", Size = new Size(120, 40), Location = new Point(20, top) };
            btnCancel = new Button { Text = "H?y", Size = new Size(120, 40), Location = new Point(160, top) };
            btnSave.Click += async (s, e) => await OnSave();
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[] { lblTitle, txtTitle, lblDesc, txtDescription, lblPrice, txtPrice, chkPublished, btnSave, btnCancel });
        }

        private async Task LoadCourse(int id)
        {
            try
            {
                var course = await _controller.GetCourseByIdAsync(id);
                if (course != null)
                {
                    txtTitle.Text = course.Title;
                    txtDescription.Text = course.Summary;
                    txtPrice.Text = course.Price.ToString();
                    chkPublished.Checked = course.IsPublished;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi t?i khóa h?c: {ex.Message}");
                Close();
            }
        }

        private async Task OnSave()
        {
            try
            {
                // Validation
                var title = txtTitle.Text?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException("Tiêu ?? không ???c ?? tr?ng.");
                if (title.Length > 200)
                    throw new ArgumentException("Tiêu ?? quá dài (t?i ?a 200 ký t?).");

                decimal price = 0;
                if (!string.IsNullOrWhiteSpace(txtPrice.Text) && !decimal.TryParse(txtPrice.Text, out price))
                    throw new ArgumentException("Giá không h?p l?.");
                if (price < 0) throw new ArgumentException("Giá ph?i l?n h?n ho?c b?ng 0.");

                var course = new Course
                {
                    Title = title,
                    Summary = txtDescription.Text,
                    Price = price,
                    IsPublished = chkPublished.Checked,
                    Slug = title.ToLower().Replace(" ", "-"),
                    OwnerId = 1
                };

                bool ok;
                if (_editingId.HasValue)
                {
                    course.CourseId = _editingId.Value;
                    ok = await _controller.UpdateCourseAsync(course);
                }
                else
                {
                    ok = await _controller.CreateCourseAsync(course);
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
