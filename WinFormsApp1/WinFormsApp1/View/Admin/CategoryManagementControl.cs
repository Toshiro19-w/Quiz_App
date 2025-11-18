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
    public partial class CategoryManagementControl : AdminBaseControl
    {
        private bool isEditing = false;
        private int editingCategoryId = 0;

        public CategoryManagementControl() : base()
        {
            InitializeComponent();
        }

        private async void CategoryManagementControl_Load(object sender, EventArgs e)
        {
            ApplyModernStyling(dataGridView, formPanel);
            ApplyModernFormStyling(formPanel);
            SetupSearchFunctionality(dataGridView, "Tên", "Mô_tả");
            SetEditMode(false);
            dataGridView.CellClick += DataGridView_CellClick;
            await LoadCategoriesAsync();
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView.Rows[e.RowIndex].Selected = true;
            }
        }

        private void CategoryManagementControl_Resize(object sender, EventArgs e)
        {
            AdjustResponsiveLayout(dataGridView, formPanel);
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                var categories = await _adminController.GetCategoriesAsync();
                dataGridView.DataSource = categories.Select(c => new
                {
                    ID = c.CategoryId,
                    Tên = c.Name,
                    Mô_tả = c.Description?.Length > 50 ? c.Description.Substring(0, 50) + "..." : c.Description,
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
                var categoryId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                _ = LoadCategoryForEditAsync(categoryId);
                SetEditMode(true);
                isEditing = true;
                editingCategoryId = categoryId;
            }
        }

        private async Task LoadCategoryForEditAsync(int categoryId)
        {
            try
            {
                var category = await _adminController.GetCategoryByIdAsync(categoryId);
                if (category != null)
                {
                    SetTextValue(txtName, category.Name);
                    SetTextValue(txtDescription, category.Description);
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), $"Lỗi tải thông tin danh mục: {ex.Message}");
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var validationResult = ValidationHelper.ValidateTitle(txtName.Text);
                if (!ValidateInput(validationResult)) return;

                var category = new CourseCategory
                {
                    Name = GetTextValue(txtName).Trim(),
                    Description = GetTextValue(txtDescription)?.Trim(),
                    CreatedAt = DateTime.UtcNow
                };

                bool success;
                if (isEditing)
                {
                    category.CategoryId = editingCategoryId;
                    success = await _adminController.UpdateCategoryAsync(category);
                }
                else
                {
                    success = await _adminController.CreateCategoryAsync(category);
                }

                if (success)
                {
                    ToastHelper.Show(this.FindForm(), "✅ Lưu thành công!");
                    await LoadCategoriesAsync();
                    SetEditMode(false);
                    ClearForm();
                }
                else
                {
                    ToastHelper.Show(this.FindForm(), "❌ Lưu thất bại!");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError(this.FindForm(), $"Lỗi lưu dữ liệu: {ex.Message}");
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var categoryId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                var result = MessageBox.Show("Bạn có chắc muốn xóa danh mục này?", "Xác nhận", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var success = await _adminController.DeleteCategoryAsync(categoryId);
                        if (success)
                        {
                            ToastHelper.Show(this.FindForm(), "Xóa thành công!");
                            await LoadCategoriesAsync();
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

            txtName.Enabled = editing;
            txtDescription.Enabled = editing;
        }

        private void ClearForm()
        {
            SetTextValue(txtName, "");
            SetTextValue(txtDescription, "");
        }
    }
}