using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;
using WinFormsApp1.View.User.Forms;

namespace WinFormsApp1.View.Admin
{
    public partial class CourseManagementControl : AdminBaseControl
    {
        private bool isEditing = false;
        private int editingCourseId = 0;

        public CourseManagementControl() : base()
        {
            InitializeComponent();
            SetupCourseContentPanel();
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
            _ = LoadCoursesAsync();
        }

        private async void CourseManagementControl_Load(object sender, EventArgs e)
        {
            var formPanel = CreateInputForm("Thông tin khóa học",
                ("Tiêu đề", "txtTitle", "Nhập tiêu đề khóa học...", true, false),
                ("Mô tả", "txtDescription", "Nhập mô tả...", false, false),
                ("Giá (VND)", "txtPrice", "Nhập giá...", true, false)
            );
            
            SetupLayoutWithForm("Quản lý khóa học", dataGridView, formPanel);
            WireCrudEvents();
            WireFormEvents();
            SetupSearchFunctionality(dataGridView, "Tên", "Danh_mục", "Mô_tả", "Trạng_thái");
            
            dataGridView.CellClick += DataGridView_CellClick;
            await LoadCoursesAsync();
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
                var courseId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                await LoadCourseContentAsync(courseId);
                courseContentPanel.Visible = true;
            }
        }

        private void CourseManagementControl_Resize(object sender, EventArgs e)
        {
            AdjustResponsiveLayout(dataGridView, formPanel, breakpoint: 1150, rightOffset: 450);
            AdjustBottomPanelLayout(courseContentPanel, 350);
        }

        private async Task LoadCoursesAsync()
        {
            try
            {
                var courses = await _adminController.GetCoursesAsync();
                var courseData = courses.Select(c => new
                {
                    ID = c.CourseId,
                    Tên = c.Title,
                    Danh_mục = c.Category?.Name ?? "Chưa phân loại",
                    Mô_tả = c.Summary?.Length > 50 ? c.Summary.Substring(0, 50) + "..." : c.Summary,
                    Giá = c.Price.ToString("N0") + " VND",
                    Trạng_thái = c.IsPublished ? "Đã xuất bản" : "Nháp",
                    Ngày_tạo = c.CreatedAt.ToString("dd/MM/yyyy")
                }).ToList();
                
                dataGridView.DataSource = courseData;
                ApplyModernStyling(dataGridView, null);
                
                UpdateDataGridHeaders(dataGridView, new Dictionary<string, string>
                {
                    { "ID", "Mã" },
                    { "Tên", "Tiêu đề" },
                    { "Danh_mục", "Danh mục" },
                    { "Mô_tả", "Mô tả" },
                    { "Giá", "Giá" },
                    { "Trạng_thái", "Trạng thái" },
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
            // Open CourseBuilderForm for creating a new course
            using var form = new CourseBuilderForm();
            form.StartPosition = FormStartPosition.CenterParent;
            var result = form.ShowDialog(this.FindForm());
            if (result == DialogResult.OK)
            {
                _ = LoadCoursesAsync();
                ToastHelper.Show(this.FindForm(), "✅ Thêm khóa học thành công!");
            }

            ClearFormInputs();
            ClearFormErrors();
            ShowInputForm();
            isEditing = false;
        }

        private async void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var courseId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;

                // Open CourseBuilderForm in edit mode
                using var form = new CourseBuilderForm(courseId);
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this.FindForm());

                if (result == DialogResult.OK)
                {
                    await LoadCoursesAsync();
                    ToastHelper.Show(this.FindForm(), "✅ Cập nhật thành công!");
                }
            }
            else
            {
                ToastHelper.Show(this.FindForm(), "Vui lòng chọn khóa học để sửa!");
            }
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                var categories = await _adminController.GetCategoriesAsync();
                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "Name";
                cmbCategory.ValueMember = "CategoryId";
                cmbCategory.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), $"Lỗi tải danh mục: {ex.Message}");
            }
        }

        private async Task LoadCourseForEditAsync(int courseId)
        {
            try
            {
                var course = await _adminController.GetCourseByIdAsync(courseId);
                if (course != null)
                {
                    SetTextValue(txtTitle, course.Title);
                    SetTextValue(txtDescription, course.Summary);
                    // display price without trailing .00
                    SetTextValue(txtPrice, course.Price.ToString("F0"));
                    chkPublished.Checked = course.IsPublished;
                    cmbCategory.SelectedValue = course.CategoryId;
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), $"Lỗi tải thông tin khóa học: {ex.Message}");
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (!ValidateFormFields()) return;
                
                decimal price = 0;
                var priceText = GetFormValue("txtPrice");
                if (!string.IsNullOrWhiteSpace(priceText) && !decimal.TryParse(priceText, out price))
                {
                    ShowFieldError("txtPrice", "Giá không hợp lệ");
                    return;
                }
                
                if (price < 0)
                {
                    ShowFieldError("txtPrice", "Giá phải lớn hơn hoặc bằng 0");
                    return;
                }

                var course = new Course
                {
                    Title = GetFormValue("txtTitle").Trim(),
                    Summary = GetFormValue("txtDescription")?.Trim(),
                    Slug = GetFormValue("txtTitle").Trim().ToLower().Replace(" ", "-"),
                    Price = price,
                    IsPublished = false,
                    CategoryId = null,
                    OwnerId = 1,
                    AverageRating = 0,
                    TotalReviews = 0,
                    CreatedAt = DateTime.UtcNow
                };

                bool success;
                if (isEditing)
                {
                    course.CourseId = editingCourseId;
                    success = await _adminController.UpdateCourseAsync(course);
                }
                else
                {
                    success = await _adminController.CreateCourseAsync(course);
                }

                if (success)
                {
                    ToastHelper.Show(this.FindForm(), "✅ Lưu thành công!");
                    await LoadCoursesAsync();
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
                var courseId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                var result = MessageBox.Show("Bạn có chắc muốn xóa khóa học này?", "Xác nhận", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var success = await _adminController.DeleteCourseAsync(courseId);
                        if (success)
                        {
                            ToastHelper.Show(this.FindForm(), "Xóa thành công!");
                            await LoadCoursesAsync();
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
                ToastHelper.Show(this.FindForm(), "Vui lòng chọn khóa học để xóa!");
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
            
            // Price validation
            if (fieldName == "txtPrice")
            {
                if (!decimal.TryParse(value, out decimal price))
                {
                    ShowFieldError(fieldName, "Giá không hợp lệ");
                    return;
                }
                if (price < 0)
                {
                    ShowFieldError(fieldName, "Giá phải lớn hơn hoặc bằng 0");
                    return;
                }
            }
        }

        private TreeView courseContentTree;
        private Panel courseContentPanel;
        private Button btnAddChapter, btnAddLesson, btnEditContent, btnDeleteContent;

        private void SetupCourseContentPanel()
        {
            courseContentPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 350,
                Visible = false,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Margin = new Padding(0, 10, 0, 0)
            };
            
            // Thêm border
            courseContentPanel.Paint += (s, e) =>
            {
                var rect = courseContentPanel.ClientRectangle;
                using (var pen = new Pen(Color.FromArgb(229, 231, 235), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, rect.Width - 1, rect.Height - 1);
                }
            };

            var headerPanel = new Panel { Dock = DockStyle.Top, Height = 40, BackColor = Color.FromArgb(248, 249, 250) };
            var lblContent = new Label 
            { 
                Text = "📚 Nội dung khóa học", 
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblContent);

            var buttonPanel = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.FromArgb(248, 249, 250) };
            btnAddChapter = CreateContentButton("➕ Thêm chương", Color.FromArgb(40, 167, 69));
            btnAddLesson = CreateContentButton("📝 Thêm bài", Color.FromArgb(23, 162, 184));
            btnEditContent = CreateContentButton("✏️ Sửa", Color.FromArgb(255, 193, 7));
            btnDeleteContent = CreateContentButton("🗑️ Xóa", Color.FromArgb(220, 53, 69));
            
            btnAddChapter.Location = new Point(15, 5);
            btnAddLesson.Location = new Point(145, 5);
            btnEditContent.Location = new Point(275, 5);
            btnDeleteContent.Location = new Point(405, 5);
            
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
                ShowRootLines = true,
                FullRowSelect = true,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                ItemHeight = 25
            };

            courseContentPanel.Controls.AddRange(new Control[] { courseContentTree, buttonPanel, headerPanel });
            this.Controls.Add(courseContentPanel);
        }

        private Button CreateContentButton(string text, Color backColor)
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

        private async Task LoadCourseContentAsync(int courseId)
        {
            try
            {
                var chapters = await _adminController.GetChaptersByCourseIdAsync(courseId);
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
                courseContentPanel.Visible = true;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError(this.FindForm(), $"Lỗi tải nội dung: {ex.Message}");
            }
        }

        private async void BtnAddChapter_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;
            
            var courseId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
            using (var form = new ChapterLessonEditForm(courseId, null))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await LogAdminActionAsync("CREATE", "Chapter", null, $"Thêm chương vào khóa học {courseId}");
                    await LoadCourseContentAsync(courseId);
                    ToastHelper.Show(this.FindForm(), "✅ Thêm chương thành công!");    
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
                    var courseId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
                    await LogAdminActionAsync("CREATE", "Lesson", null, $"Thêm bài học vào chương {chapterId}");
                    await LoadCourseContentAsync(courseId);
                    ToastHelper.Show(this.FindForm(), "✅ Thêm bài học thành công!");
                }
            }
        }

        private async void BtnEditContent_Click(object sender, EventArgs e)
        {
            if (courseContentTree.SelectedNode?.Tag == null) return;
            
            var nodeData = (dynamic)courseContentTree.SelectedNode.Tag;
            var courseId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
            
            if (nodeData.Type == "Chapter")
            {
                var chapter = (CourseChapter)nodeData.Data;
                using (var form = new ChapterLessonEditForm(courseId, chapter))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        await LogAdminActionAsync("UPDATE", "Chapter", chapter.ChapterId, $"Sửa chương {chapter.ChapterId}");
                        await LoadCourseContentAsync(courseId);
                        ToastHelper.Show(this.FindForm(), "✅ Cập nhật chương thành công!");
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
                        await LogAdminActionAsync("UPDATE", "Lesson", lesson.LessonId, $"Sửa bài học {lesson.LessonId}");
                        await LoadCourseContentAsync(courseId);
                        ToastHelper.Show(this.FindForm(), "✅ Cập nhật bài học thành công!");
                    }
                }
            }
        }

        private async void BtnDeleteContent_Click(object sender, EventArgs e)
        {
            if (courseContentTree.SelectedNode?.Tag == null) return;
            
            var nodeData = (dynamic)courseContentTree.SelectedNode.Tag;
            var courseId = (int)dataGridView.SelectedRows[0].Cells["ID"].Value;
            
            var result = MessageBox.Show($"Bạn có chắc muốn xóa {nodeData.Type.ToLower()} này?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = false;
                    if (nodeData.Type == "Chapter")
                    {
                        success = await _adminController.DeleteChapterAsync(nodeData.Id);
                        if (success)
                            await LogAdminActionAsync("DELETE", "Chapter", nodeData.Id, $"Xóa chương {nodeData.Id}");
                    }
                    else if (nodeData.Type == "Lesson")
                    {
                        success = await _adminController.DeleteLessonAsync(nodeData.Id);
                        if (success)
                            await LogAdminActionAsync("DELETE", "Lesson", nodeData.Id, $"Xóa bài học {nodeData.Id}");
                    }
                    
                    if (success)
                    {
                        await LoadCourseContentAsync(courseId);
                        ToastHelper.Show(this.FindForm(), "✅ Xóa thành công!");
                    }
                }
                catch (Exception ex)
                {
                    ValidationHelper.ShowValidationError(this.FindForm(), ex.Message);
                }
            }
        }


    }
}