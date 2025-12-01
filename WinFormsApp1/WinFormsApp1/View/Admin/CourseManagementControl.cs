using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;
using WinFormsApp1.View.User.Forms;
using System.Collections.Generic;

namespace WinFormsApp1.View.Admin
{
    public partial class CourseManagementControl : AdminBaseControl
    {
        private List<dynamic> _allCourses = new List<dynamic>();
        private List<dynamic> _filteredCourses = new List<dynamic>();

        public CourseManagementControl() : base()
        {
            InitializeComponent();
        }
        
        protected override void OnAddButtonClick(object sender, EventArgs e) => BtnAdd_Click(sender, e);
        protected override void OnEditButtonClick(object sender, EventArgs e) => BtnEdit_Click(sender, e);
        protected override void OnDeleteButtonClick(object sender, EventArgs e) => BtnDelete_Click(sender, e);
        protected override void OnRefreshButtonClick(object sender, EventArgs e) => _ = LoadCoursesAsync();

        private async void CourseManagementControl_Load(object sender, EventArgs e)
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

            // Setup base layout with modern Grid
            SetupLayout("Quản lý khóa học", dataGridView);
            
            WireCrudEvents();
            SetupSearchFunctionality(dataGridView, "Tên", "Danh_mục", "Mô_tả", "Trạng_thái");
            SetupPaginationEvents();
            
            await LoadCoursesAsync();
        }

        protected override Panel CreateFilterPanel()
        {
            var panel = base.CreateFilterPanel();

            // Category Filter
            var categoryLabel = new Label
            {
                Text = "Danh mục:",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(panel.Width - 600, 15)
            };
            panel.Controls.Add(categoryLabel);

            var categoryCombo = new ComboBox
            {
                Items = { "Tất cả danh mục" },
                SelectedIndex = 0,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                Size = new Size(150, 25),
                Name = "cboCategory",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(panel.Width - 520, 12)
            };
            categoryCombo.SelectedIndexChanged += (s, e) => FilterCoursesLocally();
            panel.Controls.Add(categoryCombo);

            // Status Filter
            var statusLabel = new Label
            {
                Text = "Trạng thái:",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(panel.Width - 450, 15)
            };
            panel.Controls.Add(statusLabel);

            var statusCombo = new ComboBox
            {
                Items = { "Tất cả", "Đã xuất bản", "Nháp" },
                SelectedIndex = 0,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                Size = new Size(120, 25),
                Name = "cboStatus",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(panel.Width - 370, 12)
            };
            statusCombo.SelectedIndexChanged += (s, e) => FilterCoursesLocally();
            panel.Controls.Add(statusCombo);

            return panel;
        }

        private async Task LoadCoursesAsync()
        {
            try
            {
                var courses = await _adminController.GetCoursesAsync();
                
                // Populate categories dynamically
                var categoryCombo = this.Controls.Find("cboCategory", true).FirstOrDefault() as ComboBox;
                if (categoryCombo != null && categoryCombo.Items.Count == 1)
                {
                    var categories = courses.Select(c => c.Category?.Name).Where(n => n != null).Distinct().OrderBy(n => n).ToArray();
                    categoryCombo.Items.AddRange(categories);
                }

                _allCourses = courses.Select(c => new
                {
                    ID = c.CourseId,
                    Tên = c.Title,
                    Danh_mục = c.Category?.Name ?? "Chưa phân loại",
                    Mô_tả = c.Summary?.Length > 50 ? c.Summary.Substring(0, 50) + "..." : c.Summary,
                    Giá = c.Price.ToString("N0") + " VND",
                    Trạng_thái = c.IsPublished ? "Đã xuất bản" : "Nháp",
                    Ngày_tạo = c.CreatedAt.ToString("dd/MM/yyyy"),
                    RawPrice = c.Price,
                    RawStatus = c.IsPublished
                }).Cast<dynamic>().ToList();
                
                FilterCoursesLocally();
                
                UpdateDataGridHeaders(dataGridView, new Dictionary<string, string>
                {
                    { "ID", "Mã" },
                    { "Tên", "Tiêu đề" },
                    { "Danh_mục", "Danh mục" },
                    { "Mô_tả", "Mô tả" },
                    { "Giá", "Giá" },
                    { "Trạng_thái", "Trạng thái" },
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
                searchBox.TextChanged += (s, e) => FilterCoursesLocally();
            }
        }

        private void FilterCoursesLocally()
        {
            if (_allCourses == null || dataGridView == null) return;

            var categoryCombo = this.Controls.Find("cboCategory", true).FirstOrDefault() as ComboBox;
            var statusCombo = this.Controls.Find("cboStatus", true).FirstOrDefault() as ComboBox;

            string categoryFilter = categoryCombo?.SelectedIndex > 0 ? categoryCombo.Text : "";
            string statusFilter = statusCombo?.SelectedIndex > 0 ? statusCombo.Text : "";
            string searchText = searchBox?.Text?.Trim().ToLower() ?? "";

            _filteredCourses = _allCourses.Where(c =>
            {
                bool matchCategory = string.IsNullOrEmpty(categoryFilter) || c.Danh_mục == categoryFilter;
                bool matchStatus = string.IsNullOrEmpty(statusFilter) || c.Trạng_thái == statusFilter;
                
                bool matchSearch = string.IsNullOrEmpty(searchText) || 
                                   ((string)c.Tên).ToLower().Contains(searchText) || 
                                   ((string)c.Danh_mục).ToLower().Contains(searchText) || 
                                   ((string)c.Mô_tả ?? "").ToLower().Contains(searchText);

                return matchCategory && matchStatus && matchSearch;
            }).ToList();

            // Update pagination and display
            paginationHelper.UpdatePagination(_filteredCourses.Count);
            DisplayCurrentPage();
        }

        private void DisplayCurrentPage()
        {
            if (_filteredCourses == null || dataGridView == null) return;

            var pagedData = paginationHelper.GetPagedData(_filteredCourses).ToList();
            dataGridView.DataSource = new BindingSource { DataSource = pagedData };
            
            // Hide raw columns
            if (dataGridView.Columns["RawPrice"] != null) dataGridView.Columns["RawPrice"].Visible = false;
            if (dataGridView.Columns["RawStatus"] != null) dataGridView.Columns["RawStatus"].Visible = false;
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

        private void CourseManagementControl_Resize(object sender, EventArgs e)
        {
            // Handled automatically by Dock
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ShowCourseBuilder(null);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var courseId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                ShowCourseBuilder(courseId);
            }
            else
            {
                ToastHelper.Show(this.FindForm(), "Vui lòng chọn khóa học để sửa!");
            }
        }

        private void ShowCourseBuilder(int? courseId)
        {
            CourseBuilderForm builderForm;
            if (courseId.HasValue)
            {
                builderForm = new CourseBuilderForm(courseId.Value);
            }
            else
            {
                builderForm = new CourseBuilderForm();
            }

            builderForm.StartPosition = FormStartPosition.CenterParent;
            var result = builderForm.ShowDialog(this.FindForm());

            if (result == DialogResult.OK)
            {
                _ = LoadCoursesAsync();
                ToastHelper.Show(this.FindForm(), courseId.HasValue ? "✅ Cập nhật khóa học thành công!" : "✅ Thêm khóa học thành công!");
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
            else
            {
                ToastHelper.Show(this.FindForm(), "Vui lòng chọn khóa học để xóa!");
            }
        }
    }
}