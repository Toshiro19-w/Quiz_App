using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    public partial class UserManagementControl : UserControl
    {
        private AdminController _adminController;
        private bool isEditing = false;
        private int editingUserId = 0;

        public UserManagementControl()
        {
            _adminController = new AdminController();
            InitializeComponent();
        }

        private void UserManagementControl_Load(object sender, EventArgs e)
        {
            ApplyModernStyling();
            SetEditMode(false);
            LoadUsers();
            dataGridView.CellClick += DataGridView_CellClick;
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
            
            if (Width < 1100)
            {
                dataGridView.Width = Width - 60;
                formPanel.Location = new Point(20, dataGridView.Bottom + 20);
            }
            else
            {
                dataGridView.Width = Width - 420;
                formPanel.Location = new Point(dataGridView.Right + 20, 80);
            }
        }

        private async void LoadUsers()
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
                var userId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                LoadUserForEdit(userId);
                SetEditMode(true);
                isEditing = true;
                editingUserId = userId;
            }
        }

        private async void LoadUserForEdit(int userId)
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
                MessageBox.Show($"Lỗi tải thông tin người dùng: {ex.Message}", "Lỗi");
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Vui lòng nhập email", "Thông báo");
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
                    MessageBox.Show("Lưu thành công!", "Thông báo");
                    LoadUsers();
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
                var userId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                var result = MessageBox.Show("Bạn có chắc muốn xóa người dùng này?", "Xác nhận", MessageBoxButtons.YesNo);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var success = await _adminController.DeleteUserAsync(userId);
                        if (success)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo");
                            LoadUsers();
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