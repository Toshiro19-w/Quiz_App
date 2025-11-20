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
        private DataGridView questionsGrid;
        private Panel questionsPanel;
        private Button btnAddQuestion, btnEditQuestion, btnDeleteQuestion, btnManageQuestions;

        public TestManagementControl() : base()
        {
            InitializeComponent();
            SetupQuestionsPanel();
        }
        
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
            _ = LoadTestsAsync();
        }

        private async void TestManagementControl_Load(object sender, EventArgs e)
        {
            var formPanel = CreateInputForm("Thông tin bài kiểm tra",
                ("Tiêu đề", "txtTitle", "Nhập tiêu đề bài kiểm tra...", true, false),
                ("Thời gian (phút)", "txtTimeLimit", "Nhập thời gian...", false, false),
                ("Mô tả", "txtDescription", "Nhập mô tả...", false, false)
            );
            
            SetupLayoutWithForm("Quản lý bài kiểm tra", dataGridView, formPanel);
            WireCrudEvents();
            WireFormEvents();
            SetupSearchFunctionality(dataGridView, "Tên", "Thời_gian", "Số_câu");
            
            dataGridView.CellClick += DataGridView_CellClick;
            await LoadTestsAsync();
        }
        
        private void WireFormEvents()
        {
            var saveBtn = this.Controls.Find("btnSave", true).FirstOrDefault() as Button;
            if (saveBtn != null)
            {
                saveBtn.Click += BtnSave_Click;
            }
        }

        private async void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView.Rows[e.RowIndex].Selected = true;
                var testId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                await LoadQuestionsAsync(testId);
                questionsPanel.Visible = true;
            }
        }

        private async Task LoadQuestionsAsync(int testId)
        {
            try
            {
                var questions = await _adminController.GetQuestionsByTestIdAsync(testId);
                questionsGrid.Rows.Clear();
                
                foreach (var question in questions)
                {
                    var stemPreview = question.StemText.Length > 50 ? 
                        question.StemText.Substring(0, 50) + "..." : question.StemText;
                    
                    questionsGrid.Rows.Add(
                        question.QuestionId,
                        question.OrderIndex,
                        stemPreview,
                        GetTypeDisplayName(question.Type),
                        question.Points,
                        question.QuestionOptions?.Count ?? 0
                    );
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError(this.FindForm(), $"Lỗi tải câu hỏi: {ex.Message}");
            }
        }

        private string GetTypeDisplayName(string type)
        {
            return type switch
            {
                "multiple_choice" => "Nhiều lựa chọn",
                "single_choice" => "Một lựa chọn",
                "true_false" => "Đúng/Sai",
                "short_answer" => "Trả lời ngắn",
                "essay" => "Tự luận",
                "fill_blank" => "Điền chỗ trống",
                _ => type
            };
        }

        private async void BtnAddQuestion_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;
            
            var testId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
            using (var form = new QuestionEditForm(testId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await LogAdminActionAsync("CREATE", "Question", null, $"Thêm câu hỏi vào bài kiểm tra {testId}");
                    await LoadQuestionsAsync(testId);
                    ToastHelper.Show(this.FindForm(), "✅ Thêm câu hỏi thành công!");
                }
            }
        }

        private async void BtnEditQuestion_Click(object sender, EventArgs e)
        {
            if (questionsGrid.SelectedRows.Count == 0) return;
            
            var questionId = (int)questionsGrid.SelectedRows[0].Cells["QuestionId"].Value;
            var testId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
            
            try
            {
                var question = await _adminController.GetQuestionByIdAsync(questionId);
                if (question != null)
                {
                    using (var form = new QuestionEditForm(testId, question))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            await LogAdminActionAsync("UPDATE", "Question", questionId, $"Sửa câu hỏi {questionId}");
                            await LoadQuestionsAsync(testId);
                            ToastHelper.Show(this.FindForm(), "✅ Cập nhật câu hỏi thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError(this.FindForm(), ex.Message);
            }
        }

        private async void BtnDeleteQuestion_Click(object sender, EventArgs e)
        {
            if (questionsGrid.SelectedRows.Count == 0) return;
            
            var questionId = (int)questionsGrid.SelectedRows[0].Cells["QuestionId"].Value;
            var testId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
            
            var result = MessageBox.Show("Bạn có chắc muốn xóa câu hỏi này?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    var success = await _adminController.DeleteQuestionAsync(questionId);
                    if (success)
                    {
                        await LogAdminActionAsync("DELETE", "Question", questionId, $"Xóa câu hỏi {questionId}");
                        await LoadQuestionsAsync(testId);
                        ToastHelper.Show(this.FindForm(), "✅ Xóa câu hỏi thành công!");
                    }
                }
                catch (Exception ex)
                {
                    ValidationHelper.ShowValidationError(this.FindForm(), ex.Message);
                }
            }
        }

        private void TestManagementControl_Resize(object sender, EventArgs e)
        {
            AdjustResponsiveLayout(dataGridView, formPanel);
            AdjustBottomPanelLayout(questionsPanel, 300);
        }

        private async Task LoadTestsAsync()
        {
            try
            {
                var tests = await _adminController.GetTestsAsync();
                var testData = tests.Select(t => new
                {
                    ID = t.TestId,
                    Tên = t.Title,
                    Thời_gian = t.TimeLimitSec.HasValue ? t.TimeLimitSec.Value / 60 + " phút" : "Không giới hạn",
                    Số_câu = t.Questions.Count,
                    Ngày_tạo = t.CreatedAt.ToString("dd/MM/yyyy")
                }).ToList();
                
                dataGridView.DataSource = testData;
                ApplyModernStyling(dataGridView, null);
                
                UpdateDataGridHeaders(dataGridView, new Dictionary<string, string>
                {
                    { "ID", "Mã" },
                    { "Tên", "Tiêu đề" },
                    { "Thời_gian", "Thời gian" },
                    { "Số_câu", "Số câu hỏi" },
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

        private async void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var testId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                var test = await _adminController.GetTestByIdAsync(testId);
                
                if (test != null)
                {
                    using (var form = new TestDetailEditForm(test))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            await LoadTestsAsync();
                            ToastHelper.Show(this.FindForm(), "✅ Cập nhật thành công!");
                        }
                    }
                }
            }
            else
            {
                ToastHelper.Show(this.FindForm(), "Vui lòng chọn bài kiểm tra để sửa!");
            }
        }

        private async Task LoadTestForEditAsync(int testId)
        {
            try
            {
                var test = await _adminController.GetTestByIdAsync(testId);
                if (test != null)
                {
                    SetTextValue(txtTitle, test.Title);
                    SetTextValue(txtTimeLimit, test.TimeLimitSec.HasValue ? (test.TimeLimitSec.Value / 60).ToString() : "");
                    SetTextValue(txtDescription, test.Description);
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
                // Validate required fields
                if (!ValidateFormFields()) return;
                
                int timeLimit = 0;
                var timeLimitText = GetFormValue("txtTimeLimit");
                if (!string.IsNullOrWhiteSpace(timeLimitText) && !int.TryParse(timeLimitText, out timeLimit))
                {
                    ShowFieldError("txtTimeLimit", "Thời gian không hợp lệ");
                    return;
                }

                if (timeLimit < 0)
                {
                    ShowFieldError("txtTimeLimit", "Thời gian phải lớn hơn hoặc bằng 0");
                    return;
                }

                var test = new Test
                {
                    Title = GetFormValue("txtTitle").Trim(),
                    Description = GetFormValue("txtDescription")?.Trim(),
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
                    ToastHelper.Show(this.FindForm(), "✅ Lưu thành công!");
                    await LoadTestsAsync();
                    HideInputForm();
                    isEditing = false;
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
        
        private bool ValidateFormFields()
        {
            bool isValid = true;
            
            // Clear all errors first
            ClearFormErrors();
            
            // Validate title
            var title = GetFormValue("txtTitle").Trim();
            if (string.IsNullOrEmpty(title))
            {
                ShowFieldError("txtTitle", "Tiêu đề không được để trống");
                isValid = false;
            }
            else if (title.Length < 3)
            {
                ShowFieldError("txtTitle", "Tiêu đề phải có ít nhất 3 ký tự");
                isValid = false;
            }
            
            return isValid;
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
            else
            {
                ToastHelper.Show(this.FindForm(), "Vui lòng chọn bài kiểm tra để xóa!");
            }
        }
        




        protected override void ValidateField(string fieldName, bool required, bool isPassword)
        {
            var value = GetFormValue(fieldName).Trim();
            
            // Clear previous error
            HideFieldError(fieldName);
            
            // Required field validation
            if (required && string.IsNullOrEmpty(value))
            {
                ShowFieldError(fieldName, GetRequiredErrorMessage(fieldName));
                return;
            }
            
            // Skip validation if field is empty and not required
            if (string.IsNullOrEmpty(value)) return;
            
            // Title validation
            if (fieldName == "txtTitle" && value.Length < 3)
            {
                ShowFieldError(fieldName, "Tiêu đề phải có ít nhất 3 ký tự");
                return;
            }
            
            // Time limit validation
            if (fieldName == "txtTimeLimit")
            {
                if (!int.TryParse(value, out int timeLimit))
                {
                    ShowFieldError(fieldName, "Thời gian không hợp lệ");
                    return;
                }
                if (timeLimit < 0)
                {
                    ShowFieldError(fieldName, "Thời gian phải lớn hơn hoặc bằng 0");
                    return;
                }
            }
        }

        private void SetupQuestionsPanel()
        {
            questionsPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 300,
                Visible = false,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Margin = new Padding(0, 10, 0, 0)
            };
            
            // Thêm border
            questionsPanel.Paint += (s, e) =>
            {
                var rect = questionsPanel.ClientRectangle;
                using (var pen = new Pen(Color.FromArgb(229, 231, 235), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, rect.Width - 1, rect.Height - 1);
                }
            };

            var headerPanel = new Panel { Dock = DockStyle.Top, Height = 40, BackColor = Color.FromArgb(248, 249, 250) };
            var lblQuestions = new Label 
            { 
                Text = "📝 Câu hỏi trong bài kiểm tra", 
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblQuestions);

            var buttonPanel = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.FromArgb(248, 249, 250) };
            btnAddQuestion = CreateQuestionButton("➕ Thêm câu hỏi", Color.FromArgb(40, 167, 69));
            btnEditQuestion = CreateQuestionButton("✏️ Sửa", Color.FromArgb(255, 193, 7));
            btnDeleteQuestion = CreateQuestionButton("🗑️ Xóa", Color.FromArgb(220, 53, 69));
            
            btnAddQuestion.Location = new Point(15, 5);
            btnEditQuestion.Location = new Point(145, 5);
            btnDeleteQuestion.Location = new Point(275, 5);
            
            btnAddQuestion.Click += BtnAddQuestion_Click;
            btnEditQuestion.Click += BtnEditQuestion_Click;
            btnDeleteQuestion.Click += BtnDeleteQuestion_Click;
            
            buttonPanel.Controls.AddRange(new Control[] { btnAddQuestion, btnEditQuestion, btnDeleteQuestion });

            questionsGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackgroundColor = Color.White,
                GridColor = Color.FromArgb(229, 231, 235),
                Font = new Font("Segoe UI", 10),
                RowTemplate = { Height = 35 },
                ColumnHeadersHeight = 40
            };
            
            // Áp dụng styling cho questions grid
            questionsGrid.EnableHeadersVisualStyles = false;
            questionsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(75, 85, 99);
            questionsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            questionsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            questionsGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(59, 130, 246);
            questionsGrid.DefaultCellStyle.SelectionForeColor = Color.White;
            questionsGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            SetupQuestionsGrid();

            questionsPanel.Controls.AddRange(new Control[] { questionsGrid, buttonPanel, headerPanel });
            this.Controls.Add(questionsPanel);
        }

        private Button CreateQuestionButton(string text, Color backColor)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(120, 35),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(
                Math.Max(0, backColor.R - 20),
                Math.Max(0, backColor.G - 20),
                Math.Max(0, backColor.B - 20)
            );
            return btn;
        }

        private void SetupQuestionsGrid()
        {
            questionsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuestionId", HeaderText = "ID", Width = 50, Visible = false });
            questionsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderIndex", HeaderText = "STT", Width = 60, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            questionsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "StemText", HeaderText = "Câu hỏi", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            questionsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Type", HeaderText = "Loại", Width = 120 });
            questionsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Points", HeaderText = "Điểm", Width = 70, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            questionsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "OptionsCount", HeaderText = "Số đáp án", Width = 100, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
        }


    }
}