using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;
using System.Collections.Generic;

namespace WinFormsApp1.View.Admin
{
    public partial class CategoryManagementControl : AdminBaseControl
    {
        private bool isEditing = false;
        private int editingCategoryId = 0;
        private List<dynamic> _allCategories = new List<dynamic>();
        private List<dynamic> _filteredCategories = new List<dynamic>();

        public CategoryManagementControl() : base()
        {
            InitializeComponent();
        }
        
        protected override void OnAddButtonClick(object sender, EventArgs e) => BtnAdd_Click(sender, e);
        protected override void OnEditButtonClick(object sender, EventArgs e) => BtnEdit_Click(sender, e);
        protected override void OnDeleteButtonClick(object sender, EventArgs e) => BtnDelete_Click(sender, e);
        protected override void OnRefreshButtonClick(object sender, EventArgs e) => _ = LoadCategoriesAsync();

        private async void CategoryManagementControl_Load(object sender, EventArgs e)
        {
            // Remove the designer-generated container completely
            var mainContainer = this.Controls.Find("mainContainer", true).FirstOrDefault();
            if (mainContainer != null)
            {
                this.Controls.Remove(mainContainer);
                mainContainer.Dispose();
            }

            // Create a new modern DataGridView using helper method
            dataGridView = CreateModernDataGridView();

            var formPanel = CreateInputForm("Thông tin danh mục",
                ("Tên danh mục", "txtName", "Nhập tên danh mục...", true, false),
                ("Mô tả", "txtDescription", "Nhập mô tả...", false, false)
            );
            
            SetupLayoutWithForm("Quản lý danh mục", dataGridView, formPanel);
            WireCrudEvents();
            WireFormEvents();
            SetupSearchFunctionality(dataGridView, "Tên", "Mô_tả");
            SetupPaginationEvents();
            
            dataGridView.CellClick += DataGridView_CellClick;
            await LoadCategoriesAsync();
        }
        
        private void WireFormEvents()
        {
            var saveBtn = this.Controls.Find("btnSave", true).FirstOrDefault() as Button;
            if (saveBtn != null)
            {
                saveBtn.Click += BtnSave_Click;
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView.Rows[e.RowIndex].Selected = true;
            }
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                var categories = await _adminController.GetCategoriesAsync();
                _allCategories = categories.Select(c => new
                {
                    ID = c.CategoryId,
                    Tên = c.Name,
                    Mô_tả = c.Description?.Length > 50 ? c.Description.Substring(0, 50) + "..." : c.Description,
                    Ngày_tạo = c.CreatedAt.ToString("dd/MM/yyyy")
                }).Cast<dynamic>().ToList();
                
                FilterCategoriesLocally();
                
                UpdateDataGridHeaders(dataGridView, new Dictionary<string, string>
                {
                    { "ID", "Mã" },
                    { "Tên", "Tên danh mục" },
                    { "Mô_tả", "Mô tả" },
                    { "Ngày_tạo", "Ngày tạo" }
                });
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), $"Lỗi tải dữ liệu: {ex.Message}");
            }
        }

        protected override void SetupSearchFunctionality(DataGridView dataGridView, params string[] searchColumns)
        {
            if (searchBox != null)
            {
                searchBox.TextChanged += (s, e) => FilterCategoriesLocally();
            }
        }

        private void FilterCategoriesLocally()
        {
            if (_allCategories == null || dataGridView == null) return;

            string searchText = searchBox?.Text?.Trim().ToLower() ?? "";

            _filteredCategories = _allCategories.Where(c =>
            {
                bool matchSearch = string.IsNullOrEmpty(searchText) || 
                                   ((string)c.Tên).ToLower().Contains(searchText) || 
                                   ((string)c.Mô_tả ?? "").ToLower().Contains(searchText);

                return matchSearch;
            }).ToList();

            // Update pagination and display
            paginationHelper.UpdatePagination(_filteredCategories.Count);
            DisplayCurrentPage();
        }

        private void DisplayCurrentPage()
        {
            if (_filteredCategories == null || dataGridView == null) return;

            var pagedData = paginationHelper.GetPagedData(_filteredCategories).ToList();
            dataGridView.DataSource = new BindingSource { DataSource = pagedData };
        }

        private void SetupPaginationEvents()
        {
            // Wire up pagination panel if exists
            var existingPagination = this.Controls.Find("paginationPanel", true).FirstOrDefault();
            if (existingPagination != null)
            {
                this.Controls.Remove(existingPagination);
            }

            // Create new pagination panel using helper
            var newPagination = paginationHelper.CreatePaginationPanel((page) => DisplayCurrentPage());
            this.Controls.Add(newPagination);
            newPagination.BringToFront();
        }

        private void CategoryManagementControl_Resize(object sender, EventArgs e)
        {
            // Handled automatically by Dock
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ClearFormInputs();
            ClearFormErrors();
            ShowInputForm();
            isEditing = false;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView.SelectedRows[0];
                editingCategoryId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                
                SetFormValue("txtName", selectedRow.Cells["Tên"].Value?.ToString());
                SetFormValue("txtDescription", selectedRow.Cells["Mô_tả"].Value?.ToString());
                
                ShowInputForm();
                isEditing = true;
            }
            else
            {
                ToastHelper.Show(this.FindForm(), "Vui lòng chọn danh mục để sửa!");
            }
        }
        
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate all fields
                ValidateField("txtName", true, false);
                
                // Check if there are any visible errors
                var errorLabels = GetAllControls(inputFormPanel).OfType<Label>()
                    .Where(l => l.Name != null && l.Name.EndsWith("Error") && l.Visible);
                
                if (errorLabels.Any())
                {
                    ToastHelper.Show(this.FindForm(), "Vui lòng sửa các lỗi trước khi lưu!");
                    return;
                }
                
                var name = GetFormValue("txtName").Trim();
                var description = GetFormValue("txtDescription").Trim();

                var category = new CourseCategory
                {
                    Name = name,
                    Description = string.IsNullOrEmpty(description) ? null : description,
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
                    await LogAdminActionAsync(isEditing ? "UPDATE" : "CREATE", "Category", 
                        isEditing ? editingCategoryId : (int?)null, 
                        $"{(isEditing ? "Cập nhật" : "Tạo")} danh mục: {category.Name}");
                    
                    ToastHelper.Show(this.FindForm(), "✅ Lưu thành công!");
                    await LoadCategoriesAsync();
                    HideInputForm();
                }
                else
                {
                    ToastHelper.Show(this.FindForm(), "❌ Lưu thất bại!");
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
            else
            {
                ToastHelper.Show(this.FindForm(), "Vui lòng chọn danh mục để xóa!");
            }
        }
    }
}