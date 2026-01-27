using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;

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

            var formPanel = CreateInputForm(Lang("UserInfo"),
                (Lang("Email"), "txtEmail", $"{Lang("EnterEmail")}...", true, false),
                (Lang("FullName"), "txtFullName", $"{Lang("EnterFullName")}...", true, false),
                (Lang("Username"), "txtUsername", $"{Lang("EnterUsername")}...", true, false),
                (Lang("Password"), "txtPassword", $"{Lang("EnterPassword")}...", true, true)
            );
            
            // Add Role ComboBox to form panel
            AddRoleComboToForm(formPanel);
            
            SetupLayoutWithForm(Lang("UserManagement"), dataGridView, formPanel);
            
            // Add custom filters AFTER layout is setup
            SetupCustomFilters();
            
            WireCrudEvents();
            WireFormEvents();
            SetupSearchFunctionality(dataGridView, "Email", "Họ_tên", "Tên_đăng_nhập");
            SetupPaginationEvents();
            
            await LoadUsersAsync();
        }
        
        /// <summary>
        /// Setup custom filters for User Management
        /// </summary>
        private void SetupCustomFilters()
        {
            // Role Filter
            var roleCombo = new ComboBox
            {
                Name = "cboRoleFilter", // Changed name to avoid conflict with form's cboRole
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            roleCombo.Items.AddRange(new object[] { Lang("AllRoles"), "Admin", "User" });
            roleCombo.SelectedIndex = 0;
            roleCombo.SelectedIndexChanged += (s, e) => FilterUsersLocally();

            // Status Filter
            var statusCombo = new ComboBox
            {
                Name = "cboStatusFilter",
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            statusCombo.Items.AddRange(new object[] { Lang("AllStatuses"), Lang("Active"), Lang("Inactive") });
            statusCombo.SelectedIndex = 0;
            statusCombo.SelectedIndexChanged += (s, e) => FilterUsersLocally();

            // Add filters using the new helper method
            AddCustomFilters(
                ($"{Lang("Role")}:", roleCombo),
                ($"{Lang("Status")}:", statusCombo)
            );
        }
        
        private void AddRoleComboToForm(Panel formPanel)
        {
            var scrollPanel = formPanel.Controls.OfType<Panel>().FirstOrDefault(p => p.AutoScroll);
            if (scrollPanel == null) return;

            int yPos = 380; // Position after password field
            
            var roleLabel = new Label
            {
                Text = $"{Lang("Role")} *",
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

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await _adminController.GetUsersAsync();
                _allUsers = users.Select(u => new
                {
                    ID = u.UserId,
                    Email = u.Email,
                    FullName = u.FullName,
                    Username = u.Username,
                    Role = u.RoleId == 1 ? "Admin" : "User",
                    Status = u.Status == 1 ? Lang("Active") : Lang("Inactive"),
                    CreatedAt = LanguageHelper.FormatDate(u.CreatedAt),
                    RoleId = u.RoleId
                }).Cast<dynamic>().ToList();
                
                FilterUsersLocally();

                UpdateDataGridHeaders(dataGridView, new Dictionary<string, string>
                {
                    { "ID", Lang("ID") },
                    { "Email", Lang("Email") },
                    { "FullName", Lang("FullName") },
                    { "Username", Lang("Username") },
                    { "Role", Lang("Role") },
                    { "Status", Lang("Status") },
                    { "CreatedAt", Lang("CreatedAt") }
                });
                
                // Hide RoleId column
                if (dataGridView.Columns["RoleId"] != null)
                    dataGridView.Columns["RoleId"].Visible = false;
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), Lang("DataLoadError", ex.Message));
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

            // Use new filter names
            var roleCombo = this.Controls.Find("cboRoleFilter", true).FirstOrDefault() as ComboBox;
            var statusCombo = this.Controls.Find("cboStatusFilter", true).FirstOrDefault() as ComboBox;

            string roleFilter = roleCombo?.SelectedIndex > 0 ? roleCombo.Text : "";
            string statusFilter = statusCombo?.SelectedIndex > 0 ? (statusCombo.SelectedIndex == 1 ? Lang("Active") : Lang("Inactive")) : "";
            string searchText = searchBox?.Text?.Trim().ToLower() ?? "";

            _filteredUsers = _allUsers.Where(u => 
            {
                bool matchRole = string.IsNullOrEmpty(roleFilter) || u.Role == roleFilter;
                bool matchStatus = string.IsNullOrEmpty(statusFilter) || u.Status == statusFilter;
                bool matchSearch = string.IsNullOrEmpty(searchText) || 
                                   ((string)u.Email).ToLower().Contains(searchText) || 
                                   ((string)u.FullName).ToLower().Contains(searchText) || 
                                   ((string)u.Username).ToLower().Contains(searchText);

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
            // Create new pagination panel using helper
            var newPagination = paginationHelper.CreatePaginationPanel((page) => DisplayCurrentPage());
            
            // Add to control
            if (!this.Controls.Contains(newPagination))
            {
                this.Controls.Add(newPagination);
                newPagination.BringToFront();
            }
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
                SetFormValue("txtFullName", selectedRow.Cells["FullName"].Value?.ToString());
                SetFormValue("txtUsername", selectedRow.Cells["Username"].Value?.ToString());
                SetFormValue("txtPassword", ""); // Don't show existing password
                
                // Set role combo
                var roleCombo = inputFormPanel?.Controls.Find("cboRole", true).FirstOrDefault() as ComboBox;
                if (roleCombo != null)
                {
                    string role = selectedRow.Cells["Role"].Value?.ToString();
                    roleCombo.SelectedIndex = role == "Admin" ? 1 : 0;
                }
                
                ShowInputForm();
                isEditing = true;
            }
            else
            {
                ToastHelper.Show(this.FindForm(), Lang("PleaseSelectUserToEdit"));
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
                    ToastHelper.Show(this.FindForm(), Lang("FixErrorsBeforeSave"));
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
                        $"{(isEditing ? "Updated" : "Created")} user: {user.Email}");
                    
                    ToastHelper.Show(this.FindForm(), $"✅ {Lang("SaveSuccess")}");
                    await LoadUsersAsync();
                    HideInputForm();
                }
                else
                {
                    ToastHelper.Show(this.FindForm(), $"❌ {Lang("SaveFailed")}");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), Lang("DataSaveError", ex.Message));
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView.SelectedRows[0];
                int userId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                
                var result = MessageBox.Show(Lang("ConfirmDeleteUser"), Lang("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var success = await _adminController.DeleteUserAsync(userId);
                        if (success)
                        {
                            await LogAdminActionAsync("DELETE", "User", userId, $"Deleted user ID: {userId}");
                            ToastHelper.Show(this.FindForm(), $"✅ {Lang("DeleteSuccess")}");
                            await LoadUsersAsync();
                        }
                        else
                        {
                            ToastHelper.Show(this.FindForm(), $"❌ {Lang("DeleteFailed")}");
                        }
                    }
                    catch (Exception ex)
                    {
                        ToastHelper.Show(this.FindForm(), Lang("DataDeleteError", ex.Message));
                    }
                }
            }
            else
            {
                ToastHelper.Show(this.FindForm(), Lang("PleaseSelectUserToDelete"));
            }
        }
    }
}