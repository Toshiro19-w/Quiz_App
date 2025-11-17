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
    public partial class UserManagementControl : AdminBaseControl
    {
        private bool isEditing = false;
        private int editingUserId = 0;

        public UserManagementControl() : base()
        {
            InitializeComponent();
        }

        private async void UserManagementControl_Load(object sender, EventArgs e)
        {
            ApplyModernStyling(dataGridView, formPanel);
            SetEditMode(false);
            dataGridView.CellClick += DataGridView_CellClick;
            await LoadUsersAsync();
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView.Rows[e.RowIndex].Selected = true;
            }
        }

        private void UserManagementControl_Resize(object sender, EventArgs e)
        {
            AdjustResponsiveLayout(dataGridView, formPanel);
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await _adminController.GetUsersAsync();
                dataGridView.DataSource = users.Select(u => new
                {
                    ID = u.UserId,
                    u.Email,
                    Họ_tên = u.FullName,
                    u.Username,
                    Ngày_tạo = u.CreatedAt.ToString("dd/MM/yyyy")
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
                var userId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                _ = LoadUserForEditAsync(userId);
                SetEditMode(true);
                isEditing = true;
                editingUserId = userId;
            }
        }

        private async Task LoadUserForEditAsync(int userId)
        {
            try
            {
                var user = await _adminController.GetUserByIdAsync(userId);
                if (user != null)
                {
                    txtEmail.Text = user.Email;
                    txtUsername.Text = user.Username;
                    txtFullName.Text = user.FullName;
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), $"Lỗi tải thông tin người dùng: {ex.Message}");
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    ToastHelper.Show(this.FindForm(), "Vui lòng nhập email");
                    return;
                }

                var user = new WinFormsApp1.Models.Entities.User
                {
                    Email = txtEmail.Text,
                    Username = txtUsername.Text,
                    FullName = txtFullName.Text,
                    PasswordHash = "default",
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
                    ToastHelper.Show(this.FindForm(), "Lưu thành công!");
                    await LoadUsersAsync();
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
                var userId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                var result = MessageBox.Show("Bạn có chắc muốn xóa người dùng này?", "Xác nhận", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var success = await _adminController.DeleteUserAsync(userId);
                        if (success)
                        {
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

            txtEmail.Enabled = editing;
            txtUsername.Enabled = editing;
            txtFullName.Enabled = editing;
        }

        private void ClearForm()
        {
            txtEmail.Clear();
            txtUsername.Clear();
            txtFullName.Clear();
        }
    }
}