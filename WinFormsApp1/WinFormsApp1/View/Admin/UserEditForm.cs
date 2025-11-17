using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    public class UserEditForm : Form
    {
        private TextBox txtEmail;
        private TextBox txtUsername;
        private TextBox txtFullName;
        private ComboBox cmbRole;
        private Button btnSave;
        private Button btnCancel;
        private AdminController _controller;
        private int? _editingId;

        public UserEditForm(int? editingUserId = null)
        {
            _editingId = editingUserId;
            _controller = new AdminController();
            InitializeComponent();
            if (_editingId.HasValue)
            {
                _ = LoadUser(_editingId.Value);
            }
        }

        private void InitializeComponent()
        {
            Text = _editingId.HasValue ? "S?a ng??i dùng" : "Thêm ng??i dùng";
            Size = new Size(700, 420);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            BackColor = Color.White;

            int left = 20;
            int top = 20;

            var lblEmail = new Label { Text = "Email", Location = new Point(left, top), AutoSize = true };
            txtEmail = new TextBox { Location = new Point(left, top + 24), Size = new Size(640, 30) };
            top += 70;

            var lblUsername = new Label { Text = "Username", Location = new Point(left, top), AutoSize = true };
            txtUsername = new TextBox { Location = new Point(left, top + 24), Size = new Size(300, 30) };
            top += 70;

            var lblFullName = new Label { Text = "H? và tên", Location = new Point(left, top), AutoSize = true };
            txtFullName = new TextBox { Location = new Point(left, top + 24), Size = new Size(640, 30) };
            top += 70;

            var lblRole = new Label { Text = "RoleId", Location = new Point(left, top), AutoSize = true };
            cmbRole = new ComboBox { Location = new Point(left, top + 24), Size = new Size(200, 30), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbRole.Items.AddRange(new object[] { 1, 2, 3 });
            cmbRole.SelectedIndex = 0;
            top += 70;

            btnSave = new Button { Text = "L?u", Size = new Size(120, 40), Location = new Point(20, top) };
            btnCancel = new Button { Text = "H?y", Size = new Size(120, 40), Location = new Point(160, top) };
            btnSave.Click += async (s, e) => await OnSave();
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[] { lblEmail, txtEmail, lblUsername, txtUsername, lblFullName, txtFullName, lblRole, cmbRole, btnSave, btnCancel });
        }

        private async Task LoadUser(int id)
        {
            try
            {
                var user = await _controller.GetUserByIdAsync(id);
                if (user != null)
                {
                    txtEmail.Text = user.Email;
                    txtUsername.Text = user.Username;
                    txtFullName.Text = user.FullName;
                    cmbRole.SelectedItem = user.RoleId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi t?i ng??i dùng: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private async Task OnSave()
        {
            try
            {
                var email = txtEmail.Text?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email không ???c ?? tr?ng.");
                if (email.Length > 200) throw new ArgumentException("Email quá dài (t?i ?a 200 ký t?).");

                var username = txtUsername.Text?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username không ???c ?? tr?ng.");
                if (username.Length > 100) throw new ArgumentException("Username quá dài (t?i ?a 100 ký t?).");

                var fullName = txtFullName.Text?.Trim() ?? string.Empty;
                if (fullName.Length > 200) throw new ArgumentException("H? tên quá dài (t?i ?a 200 ký t?).");

                var userEntity = new WinFormsApp1.Models.Entities.User
                {
                    Email = email,
                    Username = username,
                    FullName = fullName,
                    RoleId = Convert.ToInt32(cmbRole.SelectedItem),
                    Status = 1,
                    CreatedAt = DateTime.UtcNow
                };

                bool ok;
                if (_editingId.HasValue)
                {
                    userEntity.UserId = _editingId.Value;
                    ok = await _controller.UpdateUserAsync(userEntity);
                }
                else
                {
                    ok = await _controller.CreateUserAsync(userEntity);
                }

                if (ok)
                {
                    ToastHelper.Show(this.Owner ?? this, "L?u thành công!");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("L?u th?t b?i.", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "L?i xác th?c", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi l?u: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
