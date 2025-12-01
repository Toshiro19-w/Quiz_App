using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    public partial class UserManagementControl : AdminBaseControl
    {
        private bool isEditing = false;
        private int editingUserId = 0;
        private List<dynamic> _allUsers = new List<dynamic>();
        private List<dynamic> _filteredUsers = new List<dynamic>();
        
        public UserManagementControl() : base()
        {
            InitializeComponent();
        }

        protected override void OnAddButtonClick(object sender, EventArgs e) => BtnAdd_Click(sender, e);
        protected override void OnEditButtonClick(object sender, EventArgs e) => BtnEdit_Click(sender, e);
        protected override void OnDeleteButtonClick(object sender, EventArgs e) => BtnDelete_Click(sender, e);
        protected override void OnRefreshButtonClick(object sender, EventArgs e) => _ = LoadUsersAsync();

        private async void UserManagementControl_Load(object sender, EventArgs e)
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

            var formPanel = CreateInputForm("Thông tin người dùng",
                ("Email", "txtEmail", "Nhập email...", true, false),
                ("Họ tên", "txtFullName", "Nhập họ tên...", true, false),
                ("Tên đăng nhập", "txtUsername", "Nhập tên đăng nhập...", true, false),
                ("Mật khẩu", "txtPassword", "Nhập mật khẩu...", true, true)
            );
            
            // Add Role ComboBox to form panel
            AddRoleComboToForm(formPanel);
            
            SetupLayoutWithForm("Quản lý người dùng", dataGridView, formPanel);
            WireCrudEvents();
            WireFormEvents();
            SetupSearchFunctionality(dataGridView, "Email", "Họ_tên", "Tên_đăng_nhập");
            SetupPaginationEvents();
            
            await LoadUsersAsync();
        }
        
        private void AddRoleComboToForm(Panel formPanel)
        {
            var scrollPanel = formPanel.Controls.OfType<Panel>().FirstOrDefault(p => p.AutoScroll);
            if (scrollPanel == null) return;

            int yPos = 380; // Position after password field
            
            var roleLabel = new Label
            {
                Text = "Vai trò *",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(0, yPos),
                Size = new Size(300, 20),
                ForeColor = Color.FromArgb(220, 53, 69)
            };

            var roleCombo = new ComboBox
            {
                Name = "cboRole",
                Font = new Font("Segoe UI", 10),
                Size = new Size(300, 30),
                Location = new Point(0, yPos + 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            roleCombo.Items.AddRange(new object[] { "User", "Admin" });
            roleCombo.SelectedIndex = 0; // Default to User

            scrollPanel.Controls.AddRange(new Control[] { roleLabel, roleCombo });
        }
        
        private void WireFormEvents()
        {
            var saveBtn = this.Controls.Find("btnSave", true).FirstOrDefault() as Button;
            if (saveBtn != null)
            {
                saveBtn.Click += BtnSave_Click;
            }
        }

        protected override Panel CreateFilterPanel()
        {
            var panel = base.CreateFilterPanel();

            // Role Filter - Updated to only Admin and User
            var roleLabel = new Label
            {
                Text = "Vai trò:",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(panel.Width - 550, 15)
            };
            panel.Controls.Add(roleLabel);

            var roleCombo = new ComboBox
            {
                Items = { "Tất cả vai trò", "Admin", "User" },
                SelectedIndex = 0,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                Size = new Size(150, 25),
                Name = "cboRole",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(panel.Width - 470, 12)
            };
            roleCombo.SelectedIndexChanged += (s, e) => FilterUsersLocally();
            panel.Controls.Add(roleCombo);

            // Status Filter
            var statusLabel = new Label
            {
                Text = "Trạng thái:",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(panel.Width - 400, 15)
            };
            panel.Controls.Add(statusLabel);

            var statusCombo = new ComboBox
            {
                Items = { "Tất cả trạng thái", "Hoạt động", "Không hoạt động" },
                SelectedIndex = 0,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                Size = new Size(150, 25),
                Name = "cboStatus",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(panel.Width - 320, 12)
            };
            statusCombo.SelectedIndexChanged += (s, e) => FilterUsersLocally();
            panel.Controls.Add(statusCombo);

            return panel;
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await _adminController.GetUsersAsync();
                _allUsers = users.Select(u => new
                {
                    ID = u.UserId,
                    Email = u.Email,
                    Họ_tên = u.FullName,
                    Tên_đăng_nhập = u.Username,
                    Vai_trò = u.RoleId == 1 ? "Admin" : "User",
                    Trạng_thái = u.Status == 1 ? "Hoạt động" : "Không hoạt động",
                    Ngày_tạo = u.CreatedAt.ToString("dd/MM/yyyy"),
                    RoleId = u.RoleId
                }).Cast<dynamic>().ToList();
                
                FilterUsersLocally();

                UpdateDataGridHeaders(dataGridView, new Dictionary<string, string>
                {
                    { "ID", "Mã" },
                    { "Email", "Email" },
                    { "Họ_tên", "Họ tên" },
                    { "Tên_đăng_nhập", "Tên đăng nhập" },
                    { "Vai_trò", "Vai trò" },
                    { "Trạng_thái", "Trạng thái" },
                    { "Ngày_tạo", "Ngày tạo" }
                });
                
                // Hide RoleId column
                if (dataGridView.Columns["RoleId"] != null)
                    dataGridView.Columns["RoleId"].Visible = false;
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
                searchBox.TextChanged += (s, e) => FilterUsersLocally();
            }
        }

        private void FilterUsersLocally()
        {
            if (_allUsers == null || dataGridView == null) return;

            var roleCombo = this.Controls.Find("cboRole", true).FirstOrDefault() as ComboBox;
            var statusCombo = this.Controls.Find("cboStatus", true).FirstOrDefault() as ComboBox;

            string roleFilter = roleCombo?.SelectedIndex > 0 ? roleCombo.Text : "";
            string statusFilter = statusCombo?.SelectedIndex > 0 ? (statusCombo.SelectedIndex == 1 ? "Hoạt động" : "Không hoạt động") : "";
            string searchText = searchBox?.Text?.Trim().ToLower() ?? "";

            _filteredUsers = _allUsers.Where(u => 
            {
                bool matchRole = string.IsNullOrEmpty(roleFilter) || u.Vai_trò == roleFilter;
                bool matchStatus = string.IsNullOrEmpty(statusFilter) || u.Trạng_thái == statusFilter;
                bool matchSearch = string.IsNullOrEmpty(searchText) || 
                                   ((string)u.Email).ToLower().Contains(searchText) || 
                                   ((string)u.Họ_tên).ToLower().Contains(searchText) || 
                                   ((string)u.Tên_đăng_nhập).ToLower().Contains(searchText);

                return matchRole && matchStatus && matchSearch;
            }).ToList();

            // Update pagination and display
            paginationHelper.UpdatePagination(_filteredUsers.Count);
            DisplayCurrentPage();
        }

        private void DisplayCurrentPage()
        {
            if (_filteredUsers == null || dataGridView == null) return;

            var pagedData = paginationHelper.GetPagedData(_filteredUsers).ToList();
            dataGridView.DataSource = new BindingSource { DataSource = pagedData };
            
            // Hide RoleId column
            if (dataGridView.Columns["RoleId"] != null)
                dataGridView.Columns["RoleId"].Visible = false;
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

        private void UserManagementControl_Resize(object sender, EventArgs e)
        {
            // Handled automatically by Dock
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ClearFormInputs();
            ClearFormErrors();
            
            // Reset role combo to User
            var roleCombo = inputFormPanel?.Controls.Find("cboRole", true).FirstOrDefault() as ComboBox;
            if (roleCombo != null) roleCombo.SelectedIndex = 0;
            
            ShowInputForm();
            isEditing = false;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView.SelectedRows[0];
                editingUserId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                
                SetFormValue("txtEmail", selectedRow.Cells["Email"].Value?.ToString());
                SetFormValue("txtFullName", selectedRow.Cells["Họ_tên"].Value?.ToString());
                SetFormValue("txtUsername", selectedRow.Cells["Tên_đăng_nhập"].Value?.ToString());
                SetFormValue("txtPassword", ""); // Don't show existing password
                
                // Set role combo
                var roleCombo = inputFormPanel?.Controls.Find("cboRole", true).FirstOrDefault() as ComboBox;
                if (roleCombo != null)
                {
                    string role = selectedRow.Cells["Vai_trò"].Value?.ToString();
                    roleCombo.SelectedIndex = role == "Admin" ? 1 : 0;
                }
                
                ShowInputForm();
                isEditing = true;
            }
            else
            {
                ToastHelper.Show(this.FindForm(), "Vui lòng chọn người dùng để sửa!");
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate all fields one more time
                ValidateField("txtEmail", true, false);
                ValidateField("txtFullName", true, false);
                ValidateField("txtUsername", true, false);
                ValidateField("txtPassword", !isEditing, true); // Password required only when creating
                
                // Check if there are any visible errors
                var errorLabels = GetAllControls(inputFormPanel).OfType<Label>()
                    .Where(l => l.Name != null && l.Name.EndsWith("Error") && l.Visible);
                
                if (errorLabels.Any())
                {
                    ToastHelper.Show(this.FindForm(), "Vui lòng sửa các lỗi trước khi lưu!");
                    return;
                }
                
                var email = GetFormValue("txtEmail").Trim();
                var fullName = GetFormValue("txtFullName").Trim();
                var username = GetFormValue("txtUsername").Trim();
                var password = GetFormValue("txtPassword").Trim();
                
                // Get role from combo
                var roleCombo = inputFormPanel?.Controls.Find("cboRole", true).FirstOrDefault() as ComboBox;
                int roleId = roleCombo?.SelectedIndex == 1 ? 1 : 3; // 1 = Admin, 3 = User

                var user = new WinFormsApp1.Models.Entities.User
                {
                    Email = email,
                    Username = username,
                    FullName = fullName,
                    RoleId = roleId,
                    Status = 1,
                    CreatedAt = DateTime.UtcNow
                };
                
                // Only update password if provided
                if (!string.IsNullOrEmpty(password))
                {
                    user.PasswordHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(password)));
                }

                bool success;
                if (isEditing)
                {
                    user.UserId = editingUserId;
                    success = await _adminController.UpdateUserAsync(user);
                }
                else
                {
                    success = await _adminController.CreateUserAsync(user);
                }

                if (success)
                {
                    await LogAdminActionAsync(isEditing ? "UPDATE" : "CREATE", "User", 
                        isEditing ? editingUserId : (int?)null, 
                        $"{(isEditing ? "Cập nhật" : "Tạo")} người dùng: {user.Email}");
                    
                    ToastHelper.Show(this.FindForm(), "✅ Lưu thành công!");
                    await LoadUsersAsync();
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
                var selectedRow = dataGridView.SelectedRows[0];
                int userId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                
                var result = MessageBox.Show("Bạn có chắc muốn xóa người dùng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var success = await _adminController.DeleteUserAsync(userId);
                        if (success)
                        {
                            await LogAdminActionAsync("DELETE", "User", userId, $"Xóa người dùng ID: {userId}");
                            ToastHelper.Show(this.FindForm(), "✅ Xóa thành công!");
                            await LoadUsersAsync();
                        }
                        else
                        {
                            ToastHelper.Show(this.FindForm(), "❌ Xóa thất bại!");
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
                ToastHelper.Show(this.FindForm(), "Vui lòng chọn người dùng để xóa!");
            }
        }
    }
}