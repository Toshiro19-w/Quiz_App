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
        
        protected override void OnAddButtonClick(object sender, EventArgs e)
        {
            BtnAdd_Click(sender, e);
        }
        
        protected override void OnEditButtonClick(object sender, EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }
        
        protected override void OnDeleteButtonClick(object sender, EventArgs e)
        {
            BtnDelete_Click(sender, e);
        }
        
        protected override void OnRefreshButtonClick(object sender, EventArgs e)
        {
            _ = LoadUsersAsync();
        }

        public UserManagementControl() : base()
        {
            InitializeComponent();
        }

        private async void UserManagementControl_Load(object sender, EventArgs e)
        {
            var formPanel = CreateInputForm("Thông tin người dùng",
                ("Email", "txtEmail", "Nhập email...", true, false),
                ("Họ tên", "txtFullName", "Nhập họ tên...", true, false),
                ("Tên đăng nhập", "txtUsername", "Nhập tên đăng nhập...", true, false),
                ("Mật khẩu", "txtPassword", "Nhập mật khẩu...", true, true)
            );
            
            SetupLayoutWithForm("Quản lý người dùng", dataGridView, formPanel);
            WireCrudEvents();
            WireFormEvents();
            SetupSearchFunctionality(dataGridView, "Email", "Họ_tên", "Tên_đăng_nhập");
            
            await LoadUsersAsync();
        }
        
        private void WireFormEvents()
        {
            var saveBtn = this.Controls.Find("btnSave", true).FirstOrDefault() as Button;
            if (saveBtn != null)
            {
                saveBtn.Click += BtnSave_Click;
            }
        }

        private void UserManagementControl_Resize(object sender, EventArgs e)
        {
            AdjustResponsiveLayout(dataGridView, null);
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await _adminController.GetUsersAsync();
                var userData = users.Select(u => new
                {
                    ID = u.UserId,
                    Email = u.Email,
                    Họ_tên = u.FullName,
                    Tên_đăng_nhập = u.Username,
                    Ngày_tạo = u.CreatedAt.ToString("dd/MM/yyyy")
                }).ToList();
                
                dataGridView.DataSource = userData;
                ApplyModernStyling(dataGridView, null);
                
                UpdateDataGridHeaders(dataGridView, new Dictionary<string, string>
                {
                    { "ID", "Mã" },
                    { "Email", "Email" },
                    { "Họ_tên", "Họ tên" },
                    { "Tên_đăng_nhập", "Tên đăng nhập" },
                    { "Ngày_tạo", "Ngày tạo" }
                });
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), $"Lỗi tải dữ liệu: {ex.Message}");
            }
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
                editingUserId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                
                SetFormValue("txtEmail", selectedRow.Cells["Email"].Value?.ToString());
                SetFormValue("txtFullName", selectedRow.Cells["Họ_tên"].Value?.ToString());
                SetFormValue("txtUsername", selectedRow.Cells["Tên_đăng_nhập"].Value?.ToString());
                SetFormValue("txtPassword", ""); // Don't show existing password
                
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
                ValidateField("txtPassword", true, true);
                
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

                var user = new WinFormsApp1.Models.Entities.User
                {
                    Email = email,
                    Username = username,
                    FullName = fullName,
                    PasswordHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(password))),
                    RoleId = 1,
                    Status = 1,
                    CreatedAt = DateTime.UtcNow
                };

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
                
                var result = MessageBox.Show("Bạn có chắc muốn xóa người dùng này?", "Xác nhận", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var success = await _adminController.DeleteUserAsync(userId);
                        if (success)
                        {
                            await LogAdminActionAsync("DELETE", "User", userId, $"Xóa người dùng ID: {userId}");
                            ToastHelper.Show(this.FindForm(), "Xóa thành công!");
                            await LoadUsersAsync();
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
                ToastHelper.Show(this.FindForm(), "Vui lòng chọn người dùng để xóa!");
            }
        }
        





    }
}