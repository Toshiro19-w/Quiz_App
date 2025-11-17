using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    public partial class CourseManagementControl : AdminBaseControl
    {
        private bool isEditing = false;
        private int editingCourseId = 0;

        public CourseManagementControl() : base()
        {
            InitializeComponent();
        }

        private async void CourseManagementControl_Load(object sender, EventArgs e)
        {
            ApplyModernStyling(dataGridView, formPanel);
            SetEditMode(false);
            dataGridView.CellClick += DataGridView_CellClick;
            await LoadCoursesAsync();
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
            AdjustResponsiveLayout(dataGridView, formPanel, breakpoint: 1150, rightOffset: 450);
        }

        private async Task LoadCoursesAsync()
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
                ToastHelper.Show(this.FindForm(), $"Lỗi tải dữ liệu: {ex.Message}");
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
                _ = LoadCourseForEditAsync(courseId);
                SetEditMode(true);
                isEditing = true;
                editingCourseId = courseId;
            }
        }

        private async Task LoadCourseForEditAsync(int courseId)
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
                ToastHelper.Show(this.FindForm(), $"Lỗi tải thông tin khóa học: {ex.Message}");
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    ToastHelper.Show(this.FindForm(), "Vui lòng nhập tên khóa học");
                    return;
                }

                decimal price = 0;
                if (!string.IsNullOrWhiteSpace(txtPrice.Text) && !decimal.TryParse(txtPrice.Text, out price))
                {
                    ToastHelper.Show(this.FindForm(), "Giá không hợp lệ");
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
                    ToastHelper.Show(this.FindForm(), "Lưu thành công!");
                    await LoadCoursesAsync();
                    SetEditMode(false);
                    ClearForm();
                }
                else
                {
                    ToastHelper.Show(this.FindForm(), "Lưu thất bại!");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), $"Lỗi lưu dữ liệu: {ex.Message}");
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
                            ToastHelper.Show(this.FindForm(), "Xóa thành công!");
                            await LoadCoursesAsync();
                        }
                        else
                        {
                            ToastHelper.Show(this.FindForm(), "Xóa thất bại!");
                        }
                    }
                    catch (Exception ex)
                    {
                        ToastHelper.Show(this.FindForm(), $"Lỗi xóa dữ liệu: {ex.Message}");
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

            // ensure formPanel is visible and sized properly
            if (formPanel != null)
            {
                int maxFormWidth = formPanel.MaximumSize.Width > 0 ? formPanel.MaximumSize.Width : 700;
                int minFormWidth = 360;
                int defaultWidth = Math.Max(minFormWidth, Math.Min(maxFormWidth, (int)(this.Width * 0.33)));

                if (editing)
                {
                    formPanel.Width = maxFormWidth;
                    formPanel.BringToFront();
                    formPanel.AutoScroll = true;
                    formPanel.AutoScrollPosition = new Point(0, 0);
                    // shrink datagrid to accommodate
                    if (dataGridView != null)
                    {
                        dataGridView.Width = Math.Max(700, this.Width - formPanel.Width - 60);
                    }
                }
                else
                {
                    formPanel.Width = defaultWidth;
                    formPanel.AutoScrollPosition = new Point(0, 0);
                    if (dataGridView != null)
                    {
                        dataGridView.Width = Math.Max(700, this.Width - formPanel.Width - 60);
                    }
                }
            }
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