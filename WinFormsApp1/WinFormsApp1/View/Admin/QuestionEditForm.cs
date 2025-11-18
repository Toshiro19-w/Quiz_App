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
    public partial class QuestionEditForm : Form
    {
        private readonly AdminController _controller;
        private Question _question;
        private readonly int _testId;
        private List<QuestionOption> _options = new List<QuestionOption>();

        private TextBox txtStemText;
        private ComboBox cmbType;
        private NumericUpDown numPoints;
        private DataGridView dgvOptions;
        private Button btnAddOption, btnRemoveOption, btnSave, btnCancel;

        public Question Question => _question;

        public QuestionEditForm(int testId, Question question = null)
        {
            _controller = new AdminController();
            _testId = testId;
            _question = question ?? new Question { TestId = testId, Type = "multiple_choice", Points = 1 };
            
            InitializeComponent();
            SetupControls();
            LoadQuestion();
        }

        private void InitializeComponent()
        {
            this.Text = _question.QuestionId == 0 ? "Thêm câu hỏi" : "Sửa câu hỏi";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void SetupControls()
        {
            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6,
                Padding = new Padding(20)
            };
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // Question Text
            mainPanel.Controls.Add(new Label { Text = "Câu hỏi:", Anchor = AnchorStyles.Right }, 0, 0);
            txtStemText = new TextBox 
            { 
                Multiline = true, 
                Height = 80, 
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical
            };
            mainPanel.Controls.Add(txtStemText, 1, 0);

            // Question Type
            mainPanel.Controls.Add(new Label { Text = "Loại:", Anchor = AnchorStyles.Right }, 0, 1);
            cmbType = new ComboBox 
            { 
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill
            };
            cmbType.Items.AddRange(new[] { "multiple_choice", "single_choice", "true_false", "short_answer" });
            cmbType.SelectedIndexChanged += CmbType_SelectedIndexChanged;
            mainPanel.Controls.Add(cmbType, 1, 1);

            // Points
            mainPanel.Controls.Add(new Label { Text = "Điểm:", Anchor = AnchorStyles.Right }, 0, 2);
            numPoints = new NumericUpDown 
            { 
                Minimum = 0.1m, 
                Maximum = 100, 
                DecimalPlaces = 1,
                Value = 1,
                Dock = DockStyle.Fill
            };
            mainPanel.Controls.Add(numPoints, 1, 2);

            // Options section
            var optionsPanel = new Panel { Dock = DockStyle.Fill };
            var optionsLabel = new Label { Text = "Đáp án:", Dock = DockStyle.Top, Height = 25 };
            
            dgvOptions = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            SetupOptionsGrid();

            var buttonPanel = new Panel { Dock = DockStyle.Bottom, Height = 35 };
            btnAddOption = new Button { Text = "➕ Thêm", Size = new Size(80, 30), Location = new Point(0, 2) };
            btnRemoveOption = new Button { Text = "➖ Xóa", Size = new Size(80, 30), Location = new Point(85, 2) };
            btnAddOption.Click += BtnAddOption_Click;
            btnRemoveOption.Click += BtnRemoveOption_Click;
            buttonPanel.Controls.AddRange(new Control[] { btnAddOption, btnRemoveOption });

            optionsPanel.Controls.AddRange(new Control[] { dgvOptions, buttonPanel, optionsLabel });
            mainPanel.Controls.Add(optionsPanel, 1, 3);
            mainPanel.SetRowSpan(optionsPanel, 2);

            // Action buttons
            var actionPanel = new Panel { Dock = DockStyle.Fill };
            btnSave = new Button 
            { 
                Text = "💾 Lưu", 
                Size = new Size(80, 35),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnCancel = new Button 
            { 
                Text = "❌ Hủy", 
                Size = new Size(80, 35),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            
            btnSave.Location = new Point(0, 0);
            btnCancel.Location = new Point(90, 0);
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            
            actionPanel.Controls.AddRange(new Control[] { btnSave, btnCancel });
            mainPanel.Controls.Add(actionPanel, 1, 5);

            this.Controls.Add(mainPanel);
        }

        private void SetupOptionsGrid()
        {
            dgvOptions.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "OptionText", 
                HeaderText = "Nội dung đáp án", 
                Width = 400 
            });
            dgvOptions.Columns.Add(new DataGridViewCheckBoxColumn 
            { 
                Name = "IsCorrect", 
                HeaderText = "Đúng", 
                Width = 60 
            });
        }

        private void LoadQuestion()
        {
            txtStemText.Text = _question.StemText ?? "";
            cmbType.SelectedItem = _question.Type ?? "multiple_choice";
            numPoints.Value = _question.Points;

            if (_question.QuestionOptions?.Any() == true)
            {
                _options = _question.QuestionOptions.OrderBy(o => o.OrderIndex).ToList();
            }
            else
            {
                // Default options for new questions
                _options = new List<QuestionOption>
                {
                    new QuestionOption { OptionText = "", IsCorrect = false, OrderIndex = 1 },
                    new QuestionOption { OptionText = "", IsCorrect = false, OrderIndex = 2 }
                };
            }
            
            RefreshOptionsGrid();
        }

        private void RefreshOptionsGrid()
        {
            dgvOptions.Rows.Clear();
            foreach (var option in _options)
            {
                dgvOptions.Rows.Add(option.OptionText, option.IsCorrect);
            }
        }

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool showOptions = cmbType.SelectedItem?.ToString() != "short_answer";
            dgvOptions.Visible = showOptions;
            btnAddOption.Visible = showOptions;
            btnRemoveOption.Visible = showOptions;

            if (cmbType.SelectedItem?.ToString() == "true_false" && _options.Count != 2)
            {
                _options.Clear();
                _options.Add(new QuestionOption { OptionText = "Đúng", IsCorrect = true, OrderIndex = 1 });
                _options.Add(new QuestionOption { OptionText = "Sai", IsCorrect = false, OrderIndex = 2 });
                RefreshOptionsGrid();
            }
        }

        private void BtnAddOption_Click(object sender, EventArgs e)
        {
            if (cmbType.SelectedItem?.ToString() == "true_false") return;

            _options.Add(new QuestionOption 
            { 
                OptionText = "", 
                IsCorrect = false, 
                OrderIndex = _options.Count + 1 
            });
            RefreshOptionsGrid();
        }

        private void BtnRemoveOption_Click(object sender, EventArgs e)
        {
            if (dgvOptions.SelectedRows.Count > 0 && _options.Count > 1)
            {
                int index = dgvOptions.SelectedRows[0].Index;
                if (index < _options.Count)
                {
                    _options.RemoveAt(index);
                    // Reorder
                    for (int i = 0; i < _options.Count; i++)
                        _options[i].OrderIndex = i + 1;
                    RefreshOptionsGrid();
                }
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate
                var validation = ValidationHelper.ValidateTitle(txtStemText.Text);
                if (!string.IsNullOrEmpty(validation))
                {
                    ValidationHelper.ShowValidationError(this, validation);
                    return;
                }

                // Update question from form
                _question.StemText = txtStemText.Text.Trim();
                _question.Type = cmbType.SelectedItem.ToString();
                _question.Points = numPoints.Value;

                // Update options from grid
                if (dgvOptions.Visible)
                {
                    _options.Clear();
                    for (int i = 0; i < dgvOptions.Rows.Count; i++)
                    {
                        var row = dgvOptions.Rows[i];
                        var optionText = row.Cells["OptionText"].Value?.ToString()?.Trim();
                        if (!string.IsNullOrEmpty(optionText))
                        {
                            _options.Add(new QuestionOption
                            {
                                OptionText = optionText,
                                IsCorrect = Convert.ToBoolean(row.Cells["IsCorrect"].Value ?? false),
                                OrderIndex = i + 1
                            });
                        }
                    }

                    if (_options.Count == 0)
                    {
                        ValidationHelper.ShowValidationError(this, "Phải có ít nhất một đáp án");
                        return;
                    }

                    if (_question.Type != "short_answer" && !_options.Any(o => o.IsCorrect))
                    {
                        ValidationHelper.ShowValidationError(this, "Phải có ít nhất một đáp án đúng");
                        return;
                    }
                }

                _question.QuestionOptions = _options;

                // Save to database
                bool success;
                if (_question.QuestionId == 0)
                {
                    success = await _controller.CreateQuestionAsync(_question);
                }
                else
                {
                    success = await _controller.UpdateQuestionAsync(_question);
                }

                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    ValidationHelper.ShowValidationError(this, "Lưu thất bại");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError(this, ex.Message);
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