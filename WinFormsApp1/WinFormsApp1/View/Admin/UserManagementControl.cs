using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    public class UserManagementControl : UserControl
    {
        private AdminController _adminController;
        private DataGridView dataGridView;
        private TextBox txtEmail, txtUsername, txtFullName;
        private Button btnAdd, btnEdit, btnDelete, btnSave, btnCancel;
        private Panel formPanel, mainContainer;
        private bool isEditing = false;
        private int editingUserId = 0;

        public UserManagementControl()
        {
            _adminController = new AdminController();
            SetupLayout();
            LoadUsers();
        }

        private void SetupLayout()
        {
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(248, 249, 250);

            mainContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            var titleLabel = new Label
            {
                Text = "Quản lý người dùng",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(0, 0),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };

            // Sử dụng FormLayoutHelper để tạo DataGridView hiện đại
            dataGridView = FormLayoutHelper.CreateModernDataGridView();
            dataGridView.Location = new Point(0, 50);
            dataGridView.Size = new Size(700, 400);

            // Tạo form panel với style card
            formPanel = FormLayoutHelper.CreateCardPanel(new Size(350, 450), new Point(720, 50));

            // Tạo form fields với FormLayoutHelper
            var emailPanel = FormLayoutHelper.CreateFormField("Email:", out txtEmail, 0, 310);
            var usernamePanel = FormLayoutHelper.CreateFormField("Username:", out txtUsername, 80, 310);
            var fullNamePanel = FormLayoutHelper.CreateFormField("Họ tên:", out txtFullName, 160, 310);

            // Tạo buttons với style hiện đại
            btnAdd = FormLayoutHelper.CreateModernButton("➕ Thêm", Color.FromArgb(52, 144, 220), Color.White, new Size(90, 35));
            btnAdd.Location = new Point(10, 260);

            btnEdit = FormLayoutHelper.CreateModernButton("✏️ Sửa", Color.FromArgb(34, 197, 94), Color.White, new Size(90, 35));
            btnEdit.Location = new Point(110, 260);

            btnDelete = FormLayoutHelper.CreateModernButton("🗑️ Xóa", Color.FromArgb(239, 68, 68), Color.White, new Size(90, 35));
            btnDelete.Location = new Point(210, 260);

            btnSave = FormLayoutHelper.CreateModernButton("💾 Lưu", Color.FromArgb(52, 144, 220), Color.White, new Size(140, 35));
            btnSave.Location = new Point(10, 310);
            btnSave.Visible = false;

            btnCancel = FormLayoutHelper.CreateModernButton("❌ Hủy", Color.FromArgb(107, 114, 128), Color.White, new Size(140, 35));
            btnCancel.Location = new Point(160, 310);
            btnCancel.Visible = false;

            formPanel.Controls.AddRange(new Control[] { 
                emailPanel, usernamePanel, fullNamePanel,
                btnAdd, btnEdit, btnDelete, btnSave, btnCancel 
            });

            mainContainer.Controls.AddRange(new Control[] { titleLabel, dataGridView, formPanel });
            Controls.Add(mainContainer);

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            // Setup responsive
            this.Resize += (s, e) => AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (Width < 1100)
            {
                dataGridView.Width = Width - 60;
                formPanel.Location = new Point(20, dataGridView.Bottom + 20);
            }
            else
            {
                dataGridView.Width = Width - 420;
                formPanel.Location = new Point(dataGridView.Right + 20, 50);
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

                var user = new User
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