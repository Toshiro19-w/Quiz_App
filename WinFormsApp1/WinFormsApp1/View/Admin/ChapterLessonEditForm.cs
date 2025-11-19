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
    public partial class ChapterLessonEditForm : Form
    {
        private readonly AdminController _controller;
        private readonly int _courseId;
        private CourseChapter _chapter;
        private Lesson _lesson;
        private readonly bool _isChapter;

        private TextBox txtTitle, txtDescription;
        private ComboBox cmbVisibility;
        private Button btnSave, btnCancel;

        public CourseChapter Chapter => _chapter;
        public Lesson Lesson => _lesson;

        // Constructor for Chapter
        public ChapterLessonEditForm(int courseId, CourseChapter chapter = null)
        {
            _controller = new AdminController();
            _courseId = courseId;
            _chapter = chapter ?? new CourseChapter { CourseId = courseId };
            _isChapter = true;
            
            InitializeComponent();
            SetupControls();
            LoadData();
        }

        // Constructor for Lesson - use bool to differentiate
        public ChapterLessonEditForm(int chapterId, Lesson lesson = null, bool isLesson = true)
        {
            _controller = new AdminController();
            _lesson = lesson ?? new Lesson { ChapterId = chapterId, Visibility = "public" };
            _isChapter = false;
            
            InitializeComponent();
            SetupControls();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = _isChapter ? 
                (_chapter.ChapterId == 0 ? "Thêm chương" : "Sửa chương") :
                (_lesson.LessonId == 0 ? "Thêm bài học" : "Sửa bài học");
            this.Size = new Size(500, 400);
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
                RowCount = _isChapter ? 4 : 5,
                Padding = new Padding(20)
            };
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            int row = 0;

            // Title
            mainPanel.Controls.Add(new Label { Text = "Tiêu đề:", Anchor = AnchorStyles.Right }, 0, row);
            txtTitle = new TextBox { Dock = DockStyle.Fill };
            mainPanel.Controls.Add(txtTitle, 1, row++);

            // Description
            mainPanel.Controls.Add(new Label { Text = "Mô tả:", Anchor = AnchorStyles.Right | AnchorStyles.Top }, 0, row);
            txtDescription = new TextBox 
            { 
                Multiline = true, 
                Height = 100, 
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical
            };
            mainPanel.Controls.Add(txtDescription, 1, row++);

            // Visibility (only for lessons)
            if (!_isChapter)
            {
                mainPanel.Controls.Add(new Label { Text = "Hiển thị:", Anchor = AnchorStyles.Right }, 0, row);
                cmbVisibility = new ComboBox 
                { 
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Dock = DockStyle.Fill
                };
                cmbVisibility.Items.AddRange(new[] { "public", "private", "draft" });
                cmbVisibility.SelectedItem = "public";
                mainPanel.Controls.Add(cmbVisibility, 1, row++);
            }

            // Buttons
            var buttonPanel = new Panel { Dock = DockStyle.Fill };
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
            
            buttonPanel.Controls.AddRange(new Control[] { btnSave, btnCancel });
            mainPanel.Controls.Add(buttonPanel, 1, row);

            this.Controls.Add(mainPanel);
        }

        private void LoadData()
        {
            if (_isChapter)
            {
                txtTitle.Text = _chapter.Title ?? "";
                txtDescription.Text = _chapter.Description ?? "";
            }
            else
            {
                txtTitle.Text = _lesson.Title ?? "";
                txtDescription.Text = _lesson.Description ?? "";
                if (cmbVisibility != null)
                    cmbVisibility.SelectedItem = _lesson.Visibility ?? "public";
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate title
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("Tiêu đề không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTitle.Focus();
                    return;
                }
                
                if (txtTitle.Text.Trim().Length < 3)
                {
                    MessageBox.Show("Tiêu đề phải có ít nhất 3 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTitle.Focus();
                    return;
                }

                bool success;
                if (_isChapter)
                {
                    _chapter.Title = txtTitle.Text.Trim();
                    _chapter.Description = txtDescription.Text?.Trim();

                    if (_chapter.ChapterId == 0)
                    {
                        success = await _controller.CreateChapterAsync(_chapter);
                    }
                    else
                    {
                        success = await _controller.UpdateChapterAsync(_chapter);
                    }
                }
                else
                {
                    _lesson.Title = txtTitle.Text.Trim();
                    _lesson.Description = txtDescription.Text?.Trim();
                    _lesson.Visibility = cmbVisibility?.SelectedItem?.ToString() ?? "public";

                    if (_lesson.LessonId == 0)
                    {
                        success = await _controller.CreateLessonAsync(_lesson);
                    }
                    else
                    {
                        success = await _controller.UpdateLessonAsync(_lesson);
                    }
                }

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