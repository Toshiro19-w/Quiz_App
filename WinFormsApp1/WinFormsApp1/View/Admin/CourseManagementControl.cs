using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    public partial class CourseManagementControl : UserControl
    {
        private AdminController _adminController;
        private bool isEditing = false;
        private int editingCourseId = 0;

        public CourseManagementControl()
        {
            _adminController = new AdminController();
            InitializeComponent();
        }

        private void CourseManagementControl_Load(object sender, EventArgs e)
        {
            ApplyModernStyling();
            SetEditMode(false);
            LoadCourses();
            dataGridView.CellClick += DataGridView_CellClick;
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView.Rows[e.RowIndex].Selected = true;
            }
        }

        private void CourseManagementControl_Resize(object sender, EventArgs e)
        {
            AdjustResponsiveLayout();
        }

        private void ApplyModernStyling()
        {
            if (dataGridView == null || formPanel == null) return;
            
            // Apply modern styling to DataGridView
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 144, 220);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 144, 220);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            // Apply card styling to form panel
            formPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void AdjustResponsiveLayout()
        {
            if (dataGridView == null || formPanel == null) return;
            
            if (Width < 1150)
            {
                dataGridView.Width = Width - 60;
                formPanel.Location = new Point(20, dataGridView.Bottom + 20);
            }
            else
            {
                dataGridView.Width = Width - 450;
                formPanel.Location = new Point(dataGridView.Right + 20, 80);
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