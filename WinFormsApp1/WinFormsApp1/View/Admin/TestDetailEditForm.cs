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
    public partial class TestDetailEditForm : Form
    {
        private readonly AdminController _controller;
        private Test _test;
        
        private TextBox txtTitle, txtDescription, txtTimeLimit;
        private DataGridView questionsGrid;
        private Button btnSave, btnCancel;
        private Button btnAddQuestion, btnEditQuestion, btnDeleteQuestion;

        public Test Test => _test;

        public TestDetailEditForm(Test test)
        {
            _controller = new AdminController();
            _test = test;
            
            InitializeComponent();
            SetupControls();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Chi tiết bài kiểm tra - " + _test.Title;
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void SetupControls()
        {
            // Top info panel
            var infoPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 180,
                Padding = new Padding(20)
            };

            var infoTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3
            };
            infoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            infoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // Test info fields
            infoTable.Controls.Add(new Label { Text = "Tiêu đề:", Anchor = AnchorStyles.Right }, 0, 0);
            txtTitle = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };
            infoTable.Controls.Add(txtTitle, 1, 0);

            infoTable.Controls.Add(new Label { Text = "Mô tả:", Anchor = AnchorStyles.Right | AnchorStyles.Top }, 0, 1);
            txtDescription = new TextBox { Multiline = true, Height = 60, Dock = DockStyle.Fill, ScrollBars = ScrollBars.Vertical, Font = new Font("Segoe UI", 10) };
            infoTable.Controls.Add(txtDescription, 1, 1);

            infoTable.Controls.Add(new Label { Text = "Thời gian (phút):", Anchor = AnchorStyles.Right }, 0, 2);
            txtTimeLimit = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };
            infoTable.Controls.Add(txtTimeLimit, 1, 2);

            infoPanel.Controls.Add(infoTable);

            // Questions section - takes remaining space
            var questionsPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 0, 20, 20) };
            var questionsLabel = new Label 
            { 
                Text = "Câu hỏi:", 
                Dock = DockStyle.Top, 
                Height = 30, 
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(75, 85, 99)
            };
            
            var buttonPanel = new Panel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(0, 5, 0, 5) };
            btnAddQuestion = CreateButton("➕ Thêm câu hỏi", Color.FromArgb(40, 167, 69), 0);
            btnEditQuestion = CreateButton("✏️ Sửa", Color.FromArgb(255, 193, 7), 140);
            btnDeleteQuestion = CreateButton("🗑️ Xóa", Color.FromArgb(220, 53, 69), 280);
            
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
                RowTemplate = { Height = 40 },
                ColumnHeadersHeight = 45
            };
            SetupQuestionsGrid();

            questionsPanel.Controls.AddRange(new Control[] { questionsGrid, buttonPanel, questionsLabel });

            // Bottom action panel
            var actionPanel = new Panel { Dock = DockStyle.Bottom, Height = 70, Padding = new Padding(20) };
            btnSave = CreateButton("💾 Lưu", Color.FromArgb(40, 167, 69), 0);
            btnCancel = CreateButton("❌ Hủy", Color.FromArgb(220, 53, 69), 110);
            
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            
            actionPanel.Controls.AddRange(new Control[] { btnSave, btnCancel });

            this.Controls.AddRange(new Control[] { questionsPanel, actionPanel, infoPanel });
        }

        private Button CreateButton(string text, Color backColor, int x)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(130, 35),
                Location = new Point(x, 5),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
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
            // Apply modern styling
            questionsGrid.EnableHeadersVisualStyles = false;
            questionsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(75, 85, 99);
            questionsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            questionsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            questionsGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            questionsGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(59, 130, 246);
            questionsGrid.DefaultCellStyle.SelectionForeColor = Color.White;
            questionsGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            
            questionsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuestionId", HeaderText = "ID", Width = 50, Visible = false });
            questionsGrid.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "OrderIndex", 
                HeaderText = "STT", 
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            questionsGrid.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "StemText", 
                HeaderText = "Câu hỏi", 
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 300
            });
            questionsGrid.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Type", 
                HeaderText = "Loại", 
                Width = 140,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            questionsGrid.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Points", 
                HeaderText = "Điểm", 
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            questionsGrid.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "OptionsCount", 
                HeaderText = "Số đáp án", 
                Width = 110,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
        }

        private async void LoadData()
        {
            txtTitle.Text = _test.Title ?? "";
            txtDescription.Text = _test.Description ?? "";
            txtTimeLimit.Text = _test.TimeLimitSec.HasValue ? (_test.TimeLimitSec.Value / 60).ToString() : "";
            
            await LoadQuestionsAsync();
        }

        private async Task LoadQuestionsAsync()
        {
            try
            {
                var questions = await _controller.GetQuestionsByTestIdAsync(_test.TestId);
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
                MessageBox.Show($"Lỗi tải câu hỏi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            using (var form = new QuestionEditForm(_test.TestId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await LoadQuestionsAsync();
                    MessageBox.Show("✅ Thêm câu hỏi thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void BtnEditQuestion_Click(object sender, EventArgs e)
        {
            if (questionsGrid.SelectedRows.Count == 0) return;
            
            var questionId = (int)questionsGrid.SelectedRows[0].Cells["QuestionId"].Value;
            
            try
            {
                var question = await _controller.GetQuestionByIdAsync(questionId);
                if (question != null)
                {
                    using (var form = new QuestionEditForm(_test.TestId, question))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            await LoadQuestionsAsync();
                            MessageBox.Show("✅ Cập nhật câu hỏi thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnDeleteQuestion_Click(object sender, EventArgs e)
        {
            if (questionsGrid.SelectedRows.Count == 0) return;
            
            var questionId = (int)questionsGrid.SelectedRows[0].Cells["QuestionId"].Value;
            var result = MessageBox.Show("Bạn có chắc muốn xóa câu hỏi này?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    var success = await _controller.DeleteQuestionAsync(questionId);
                    if (success)
                    {
                        await LoadQuestionsAsync();
                        MessageBox.Show("✅ Xóa câu hỏi thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("Tiêu đề không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int timeLimit = 0;
                if (!string.IsNullOrWhiteSpace(txtTimeLimit.Text) && 
                    (!int.TryParse(txtTimeLimit.Text, out timeLimit) || timeLimit < 0))
                {
                    MessageBox.Show("Thời gian không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _test.Title = txtTitle.Text.Trim();
                _test.Description = txtDescription.Text?.Trim();
                _test.TimeLimitSec = timeLimit > 0 ? timeLimit * 60 : null;

                var success = await _controller.UpdateTestAsync(_test);
                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Lưu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _controller?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}