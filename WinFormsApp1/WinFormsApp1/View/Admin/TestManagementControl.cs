using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    public class TestManagementControl : UserControl
    {
        private AdminController _adminController;
        private DataGridView dataGridView;
        private TextBox txtTitle, txtTimeLimit, txtDescription;
        private Button btnAdd, btnEdit, btnDelete, btnSave, btnCancel;
        private Panel formPanel, mainContainer;
        private bool isEditing = false;
        private int editingTestId = 0;

        public TestManagementControl()
        {
            _adminController = new AdminController();
            SetupLayout();
            LoadTests();
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
                Text = "Quản lý bài kiểm tra",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(0, 0),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };

            dataGridView = FormLayoutHelper.CreateModernDataGridView();
            dataGridView.Location = new Point(0, 50);
            dataGridView.Size = new Size(700, 400);

            formPanel = FormLayoutHelper.CreateCardPanel(new Size(350, 480), new Point(720, 50));

            var titlePanel = FormLayoutHelper.CreateFormField("Tên bài kiểm tra:", out txtTitle, 0, 310);
            var timeLimitPanel = FormLayoutHelper.CreateFormField("Thời gian (phút):", out txtTimeLimit, 80, 310);
            
            var descLabel = new Label
            {
                Text = "Mô tả:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 160),
                AutoSize = true,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            txtDescription = new TextBox
            {
                Location = new Point(10, 185),
                Size = new Size(310, 80),
                Multiline = true,
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            btnAdd = FormLayoutHelper.CreateModernButton("➕ Thêm", Color.FromArgb(52, 144, 220), Color.White, new Size(90, 35));
            btnAdd.Location = new Point(10, 290);

            btnEdit = FormLayoutHelper.CreateModernButton("✏️ Sửa", Color.FromArgb(34, 197, 94), Color.White, new Size(90, 35));
            btnEdit.Location = new Point(110, 290);

            btnDelete = FormLayoutHelper.CreateModernButton("🗑️ Xóa", Color.FromArgb(239, 68, 68), Color.White, new Size(90, 35));
            btnDelete.Location = new Point(210, 290);

            btnSave = FormLayoutHelper.CreateModernButton("💾 Lưu", Color.FromArgb(52, 144, 220), Color.White, new Size(140, 35));
            btnSave.Location = new Point(10, 340);
            btnSave.Visible = false;

            btnCancel = FormLayoutHelper.CreateModernButton("❌ Hủy", Color.FromArgb(107, 114, 128), Color.White, new Size(140, 35));
            btnCancel.Location = new Point(160, 340);
            btnCancel.Visible = false;

            formPanel.Controls.AddRange(new Control[] { 
                titlePanel, timeLimitPanel, descLabel, txtDescription,
                btnAdd, btnEdit, btnDelete, btnSave, btnCancel 
            });

            mainContainer.Controls.AddRange(new Control[] { titleLabel, dataGridView, formPanel });
            Controls.Add(mainContainer);

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

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

        private async void LoadTests()
        {
            try
            {
                var tests = await _adminController.GetTestsAsync();
                dataGridView.DataSource = tests.Select(t => new
                {
                    ID = t.TestId,
                    Tên = t.Title,
                    Thời_gian = t.TimeLimitSec.HasValue ? t.TimeLimitSec.Value / 60 + " phút" : "Không giới hạn",
                    Số_câu = t.Questions.Count,
                    Ngày_tạo = t.CreatedAt.ToString("dd/MM/yyyy")
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
                var testId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                LoadTestForEdit(testId);
                SetEditMode(true);
                isEditing = true;
                editingTestId = testId;
            }
        }

        private async void LoadTestForEdit(int testId)
        {
            try
            {
                var test = await _adminController.GetTestByIdAsync(testId);
                if (test != null)
                {
                    txtTitle.Text = test.Title;
                    txtTimeLimit.Text = test.TimeLimitSec.HasValue ? (test.TimeLimitSec.Value / 60).ToString() : "";
                    txtDescription.Text = test.Description;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thông tin bài kiểm tra: {ex.Message}", "Lỗi");
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên bài kiểm tra", "Thông báo");
                    return;
                }

                int timeLimit = 0;
                if (!string.IsNullOrWhiteSpace(txtTimeLimit.Text) && !int.TryParse(txtTimeLimit.Text, out timeLimit))
                {
                    MessageBox.Show("Thời gian không hợp lệ", "Thông báo");
                    return;
                }

                var test = new Test
                {
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    TimeLimitSec = timeLimit > 0 ? timeLimit * 60 : null,
                    OwnerId = 1,
                    Visibility = "private",
                    ShuffleQuestions = false,
                    ShuffleOptions = false,
                    GradingMode = "auto",
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow
                };

                bool success;
                if (isEditing)
                {
                    test.TestId = editingTestId;
                    success = await _adminController.UpdateTestAsync(test);
                }
                else
                {
                    success = await _adminController.CreateTestAsync(test);
                }

                if (success)
                {
                    MessageBox.Show("Lưu thành công!", "Thông báo");
                    LoadTests();
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
                var testId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                var result = MessageBox.Show("Bạn có chắc muốn xóa bài kiểm tra này?", "Xác nhận", MessageBoxButtons.YesNo);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var success = await _adminController.DeleteTestAsync(testId);
                        if (success)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo");
                            LoadTests();
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
            
            txtTitle.Enabled = editing;
            txtTimeLimit.Enabled = editing;
            txtDescription.Enabled = editing;
        }

        private void ClearForm()
        {
            txtTitle.Clear();
            txtTimeLimit.Clear();
            txtDescription.Clear();
        }
    }
}