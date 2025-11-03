using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    public class CourseManagementControl : UserControl
    {
        private AdminController _adminController;
        private DataGridView dataGridView;
        private TextBox txtTitle, txtDescription, txtPrice;
        private CheckBox chkPublished;
        private Button btnAdd, btnEdit, btnDelete, btnSave, btnCancel;
        private Panel formPanel, mainContainer;
        private bool isEditing = false;
        private int editingCourseId = 0;

        public CourseManagementControl()
        {
            _adminController = new AdminController();
            SetupLayout();
            LoadCourses();
        }

        private void SetupLayout()
        {
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(248, 249, 250);

            mainContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            var titleLabel = new Label
            {
                Text = "Quản lý khóa học",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(0, 0),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };

            dataGridView = FormLayoutHelper.CreateModernDataGridView();
            dataGridView.Location = new Point(0, 50);
            dataGridView.Size = new Size(700, 400);

            formPanel = FormLayoutHelper.CreateCardPanel(new Size(380, 500), new Point(720, 50));

            var titlePanel = FormLayoutHelper.CreateFormField("Tên khóa học:", out txtTitle, 0, 340);
            
            var descLabel = new Label
            {
                Text = "Mô tả:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 80),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            txtDescription = new TextBox
            {
                Location = new Point(10, 105),
                Size = new Size(340, 80),
                Multiline = true,
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            var pricePanel = FormLayoutHelper.CreateFormField("Giá:", out txtPrice, 200, 160);

            chkPublished = new CheckBox
            {
                Text = "Đã xuất bản",
                Location = new Point(180, 245),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72)
            };

            btnAdd = FormLayoutHelper.CreateModernButton("➕ Thêm", Color.FromArgb(52, 144, 220), Color.White, new Size(100, 35));
            btnAdd.Location = new Point(10, 300);

            btnEdit = FormLayoutHelper.CreateModernButton("✏️ Sửa", Color.FromArgb(34, 197, 94), Color.White, new Size(100, 35));
            btnEdit.Location = new Point(120, 300);

            btnDelete = FormLayoutHelper.CreateModernButton("🗑️ Xóa", Color.FromArgb(239, 68, 68), Color.White, new Size(100, 35));
            btnDelete.Location = new Point(230, 300);

            btnSave = FormLayoutHelper.CreateModernButton("💾 Lưu", Color.FromArgb(52, 144, 220), Color.White, new Size(155, 35));
            btnSave.Location = new Point(10, 350);
            btnSave.Visible = false;

            btnCancel = FormLayoutHelper.CreateModernButton("❌ Hủy", Color.FromArgb(107, 114, 128), Color.White, new Size(155, 35));
            btnCancel.Location = new Point(175, 350);
            btnCancel.Visible = false;

            formPanel.Controls.AddRange(new Control[] { 
                titlePanel, descLabel, txtDescription, pricePanel, chkPublished,
                btnAdd, btnEdit, btnDelete, btnSave, btnCancel 
            });

            mainContainer.Controls.AddRange(new Control[] { titleLabel, dataGridView, formPanel });
            Controls.Add(mainContainer);

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            this.Resize += (s, e) => AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (Width < 1150)
            {
                dataGridView.Width = Width - 60;
                formPanel.Location = new Point(20, dataGridView.Bottom + 20);
            }
            else
            {
                dataGridView.Width = Width - 450;
                formPanel.Location = new Point(dataGridView.Right + 20, 50);
            }
        }

        private async void LoadCourses()
        {
            try
            {
                var courses = await _adminController.GetCoursesAsync();
                dataGridView.DataSource = courses.Select(c => new
                {
                    ID = c.CourseId,
                    Tên = c.Title,
                    Mô_tả = c.Summary?.Length > 50 ? c.Summary.Substring(0, 50) + "..." : c.Summary,
                    Giá = c.Price.ToString("C"),
                    Trạng_thái = c.IsPublished ? "Đã xuất bản" : "Nháp",
                    Ngày_tạo = c.CreatedAt.ToString("dd/MM/yyyy")
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi");
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetEditMode(true);
            isEditing = false;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var courseId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                LoadCourseForEdit(courseId);
                SetEditMode(true);
                isEditing = true;
                editingCourseId = courseId;
            }
        }

        private async void LoadCourseForEdit(int courseId)
        {
            try
            {
                var course = await _adminController.GetCourseByIdAsync(courseId);
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
                MessageBox.Show($"Lỗi tải thông tin khóa học: {ex.Message}", "Lỗi");
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khóa học", "Thông báo");
                    return;
                }

                decimal price = 0;
                if (!string.IsNullOrWhiteSpace(txtPrice.Text) && !decimal.TryParse(txtPrice.Text, out price))
                {
                    MessageBox.Show("Giá không hợp lệ", "Thông báo");
                    return;
                }

                var course = new Course
                {
                    Title = txtTitle.Text,
                    Summary = txtDescription.Text,
                    Slug = txtTitle.Text.ToLower().Replace(" ", "-"),
                    Price = price,
                    IsPublished = chkPublished.Checked,
                    OwnerId = 1,
                    AverageRating = 0,
                    TotalReviews = 0,
                    CreatedAt = DateTime.UtcNow
                };

                bool success;
                if (isEditing)
                {
                    course.CourseId = editingCourseId;
                    success = await _adminController.UpdateCourseAsync(course);
                }
                else
                {
                    success = await _adminController.CreateCourseAsync(course);
                }

                if (success)
                {
                    MessageBox.Show("Lưu thành công!", "Thông báo");
                    LoadCourses();
                    SetEditMode(false);
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Lưu thất bại!", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu dữ liệu: {ex.Message}", "Lỗi");
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var courseId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                var result = MessageBox.Show("Bạn có chắc muốn xóa khóa học này?", "Xác nhận", MessageBoxButtons.YesNo);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var success = await _adminController.DeleteCourseAsync(courseId);
                        if (success)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo");
                            LoadCourses();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại!", "Lỗi");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi xóa dữ liệu: {ex.Message}", "Lỗi");
                    }
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            ClearForm();
        }

        private void SetEditMode(bool editing)
        {
            btnAdd.Visible = !editing;
            btnEdit.Visible = !editing;
            btnDelete.Visible = !editing;
            btnSave.Visible = editing;
            btnCancel.Visible = editing;
            
            txtTitle.Enabled = editing;
            txtDescription.Enabled = editing;
            txtPrice.Enabled = editing;
            chkPublished.Enabled = editing;
        }

        private void ClearForm()
        {
            txtTitle.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            chkPublished.Checked = false;
        }
    }
}