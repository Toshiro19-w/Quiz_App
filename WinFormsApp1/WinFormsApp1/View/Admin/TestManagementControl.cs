using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    public partial class TestManagementControl : UserControl
    {
        private AdminController _adminController;
        private bool isEditing = false;
        private int editingTestId = 0;

        public TestManagementControl()
        {
            _adminController = new AdminController();
            InitializeComponent();
        }

        private void TestManagementControl_Load(object sender, EventArgs e)
        {
            ApplyModernStyling();
            SetEditMode(false);
            LoadTests();
            dataGridView.CellClick += DataGridView_CellClick;
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView.Rows[e.RowIndex].Selected = true;
            }
        }

        private void TestManagementControl_Resize(object sender, EventArgs e)
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