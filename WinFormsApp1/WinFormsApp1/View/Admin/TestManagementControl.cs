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
    public partial class TestManagementControl : AdminBaseControl
    {
        private bool isEditing = false;
        private int editingTestId = 0;

        public TestManagementControl() : base()
        {
            InitializeComponent();
        }

        private async void TestManagementControl_Load(object sender, EventArgs e)
        {
            ApplyModernStyling(dataGridView, formPanel);
            SetEditMode(false);
            dataGridView.CellClick += DataGridView_CellClick;
            await LoadTestsAsync();
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
            AdjustResponsiveLayout(dataGridView, formPanel);
        }

        private async Task LoadTestsAsync()
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
                var testId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                _ = LoadTestForEditAsync(testId);
                SetEditMode(true);
                isEditing = true;
                editingTestId = testId;
            }
        }

        private async Task LoadTestForEditAsync(int testId)
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
                ToastHelper.Show(this.FindForm(), $"Lỗi tải thông tin bài kiểm tra: {ex.Message}");
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    ToastHelper.Show(this.FindForm(), "Vui lòng nhập tên bài kiểm tra");
                    return;
                }

                int timeLimit = 0;
                if (!string.IsNullOrWhiteSpace(txtTimeLimit.Text) && !int.TryParse(txtTimeLimit.Text, out timeLimit))
                {
                    ToastHelper.Show(this.FindForm(), "Thời gian không hợp lệ");
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
                    ToastHelper.Show(this.FindForm(), "Lưu thành công!");
                    await LoadTestsAsync();
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
                var testId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                var result = MessageBox.Show("Bạn có chắc muốn xóa bài kiểm tra này?", "Xác nhận", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var success = await _adminController.DeleteTestAsync(testId);
                        if (success)
                        {
                            ToastHelper.Show(this.FindForm(), "Xóa thành công!");
                            await LoadTestsAsync();
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