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
    public partial class CourseDetailEditForm : Form
    {
        private readonly AdminController _controller;
        private Course _course;
        
        private TextBox txtTitle, txtDescription, txtPrice;
        private TreeView courseContentTree;
        private Button btnSave, btnCancel;
        private Button btnAddChapter, btnAddLesson, btnEditContent, btnDeleteContent;

        public Course Course => _course;

        public CourseDetailEditForm(Course course)
        {
            _controller = new AdminController();
            _course = course;
            
            InitializeComponent();
            SetupControls();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Chi tiết khóa học - " + _course.Title;
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

            // Course info fields
            infoTable.Controls.Add(new Label { Text = "Tiêu đề:", Anchor = AnchorStyles.Right }, 0, 0);
            txtTitle = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };
            infoTable.Controls.Add(txtTitle, 1, 0);

            infoTable.Controls.Add(new Label { Text = "Mô tả:", Anchor = AnchorStyles.Right | AnchorStyles.Top }, 0, 1);
            txtDescription = new TextBox { Multiline = true, Height = 60, Dock = DockStyle.Fill, ScrollBars = ScrollBars.Vertical, Font = new Font("Segoe UI", 10) };
            infoTable.Controls.Add(txtDescription, 1, 1);

            infoTable.Controls.Add(new Label { Text = "Giá (VND):", Anchor = AnchorStyles.Right }, 0, 2);
            txtPrice = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };
            infoTable.Controls.Add(txtPrice, 1, 2);

            infoPanel.Controls.Add(infoTable);

            // Content section - takes remaining space
            var contentPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 0, 20, 20) };
            var contentLabel = new Label 
            { 
                Text = "Nội dung khóa học:", 
                Dock = DockStyle.Top, 
                Height = 30, 
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(75, 85, 99)
            };
            
            var buttonPanel = new Panel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(0, 5, 0, 5) };
            btnAddChapter = CreateButton("➕ Thêm chương", Color.FromArgb(40, 167, 69), 0);
            btnAddLesson = CreateButton("📝 Thêm bài", Color.FromArgb(23, 162, 184), 140);
            btnEditContent = CreateButton("✏️ Sửa", Color.FromArgb(255, 193, 7), 280);
            btnDeleteContent = CreateButton("🗑️ Xóa", Color.FromArgb(220, 53, 69), 420);
            
            btnAddChapter.Click += BtnAddChapter_Click;
            btnAddLesson.Click += BtnAddLesson_Click;
            btnEditContent.Click += BtnEditContent_Click;
            btnDeleteContent.Click += BtnDeleteContent_Click;
            
            buttonPanel.Controls.AddRange(new Control[] { btnAddChapter, btnAddLesson, btnEditContent, btnDeleteContent });

            courseContentTree = new TreeView
            {
                Dock = DockStyle.Fill,
                ShowLines = true,
                ShowPlusMinus = true,
                FullRowSelect = true,
                Font = new Font("Segoe UI", 11),
                ItemHeight = 30,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White
            };

            contentPanel.Controls.AddRange(new Control[] { courseContentTree, buttonPanel, contentLabel });

            // Bottom action panel
            var actionPanel = new Panel { Dock = DockStyle.Bottom, Height = 70, Padding = new Padding(20) };
            btnSave = CreateButton("💾 Lưu", Color.FromArgb(40, 167, 69), 0);
            btnCancel = CreateButton("❌ Hủy", Color.FromArgb(220, 53, 69), 110);
            
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            
            actionPanel.Controls.AddRange(new Control[] { btnSave, btnCancel });

            this.Controls.AddRange(new Control[] { contentPanel, actionPanel, infoPanel });
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

        private async void LoadData()
        {
            txtTitle.Text = _course.Title ?? "";
            txtDescription.Text = _course.Summary ?? "";
            txtPrice.Text = _course.Price.ToString();
            
            await LoadCourseContentAsync();
        }

        private async Task LoadCourseContentAsync()
        {
            try
            {
                var chapters = await _controller.GetChaptersByCourseIdAsync(_course.CourseId);
                courseContentTree.Nodes.Clear();
                
                foreach (var chapter in chapters)
                {
                    var chapterNode = new TreeNode($"📚 {chapter.Title}")
                    {
                        Tag = new { Type = "Chapter", Id = chapter.ChapterId, Data = chapter }
                    };
                    
                    foreach (var lesson in chapter.Lessons.OrderBy(l => l.OrderIndex))
                    {
                        var lessonNode = new TreeNode($"📝 {lesson.Title}")
                        {
                            Tag = new { Type = "Lesson", Id = lesson.LessonId, Data = lesson }
                        };
                        chapterNode.Nodes.Add(lessonNode);
                    }
                    
                    courseContentTree.Nodes.Add(chapterNode);
                }
                
                courseContentTree.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải nội dung: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnAddChapter_Click(object sender, EventArgs e)
        {
            using (var form = new ChapterLessonEditForm(_course.CourseId, null))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await LoadCourseContentAsync();
                    MessageBox.Show("✅ Thêm chương thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void BtnAddLesson_Click(object sender, EventArgs e)
        {
            if (courseContentTree.SelectedNode?.Tag == null) return;
            
            var nodeData = (dynamic)courseContentTree.SelectedNode.Tag;
            int chapterId;
            
            if (nodeData.Type == "Chapter")
            {
                chapterId = nodeData.Id;
            }
            else if (nodeData.Type == "Lesson")
            {
                var lessonData = (Lesson)nodeData.Data;
                chapterId = lessonData.ChapterId;
            }
            else return;
            
            using (var form = new ChapterLessonEditForm(chapterId, null, true))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await LoadCourseContentAsync();
                    MessageBox.Show("✅ Thêm bài học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void BtnEditContent_Click(object sender, EventArgs e)
        {
            if (courseContentTree.SelectedNode?.Tag == null) return;
            
            var nodeData = (dynamic)courseContentTree.SelectedNode.Tag;
            
            if (nodeData.Type == "Chapter")
            {
                var chapter = (CourseChapter)nodeData.Data;
                using (var form = new ChapterLessonEditForm(_course.CourseId, chapter))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        await LoadCourseContentAsync();
                        MessageBox.Show("✅ Cập nhật chương thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (nodeData.Type == "Lesson")
            {
                var lesson = (Lesson)nodeData.Data;
                using (var form = new ChapterLessonEditForm(lesson.ChapterId, lesson, true))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        await LoadCourseContentAsync();
                        MessageBox.Show("✅ Cập nhật bài học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private async void BtnDeleteContent_Click(object sender, EventArgs e)
        {
            if (courseContentTree.SelectedNode?.Tag == null) return;
            
            var nodeData = (dynamic)courseContentTree.SelectedNode.Tag;
            var result = MessageBox.Show($"Bạn có chắc muốn xóa {nodeData.Type.ToLower()} này?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = false;
                    if (nodeData.Type == "Chapter")
                    {
                        success = await _controller.DeleteChapterAsync(nodeData.Id);
                    }
                    else if (nodeData.Type == "Lesson")
                    {
                        success = await _controller.DeleteLessonAsync(nodeData.Id);
                    }
                    
                    if (success)
                    {
                        await LoadCourseContentAsync();
                        MessageBox.Show("✅ Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
                {
                    MessageBox.Show("Giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _course.Title = txtTitle.Text.Trim();
                _course.Summary = txtDescription.Text?.Trim();
                _course.Price = price;

                var success = await _controller.UpdateCourseAsync(_course);
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