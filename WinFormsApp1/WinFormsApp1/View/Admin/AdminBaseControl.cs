using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    /// <summary>
    /// Base control for admin UserControls to centralize styling, layout and controller lifetime.
    /// NOTE: AdjustResponsiveLayout has been changed to use a fixed large layout (no responsive behavior)
    /// to match the user UI size and maximize admin controls.
    /// </summary>
    public abstract class AdminBaseControl : UserControl
    {
        protected readonly AdminController _adminController;
        protected TextBox searchBox;
        protected Panel searchPanel;
        protected PaginationHelper paginationHelper;
        protected Panel paginationPanel;

        protected AdminBaseControl(AdminController controller = null)
        {
            _adminController = controller ?? new AdminController();
            paginationHelper = new PaginationHelper(50);
        }

        protected void ApplyModernStyling(DataGridView dataGridView, Panel formPanel)
        {
            if (dataGridView == null || formPanel == null) return;

            // Cải thiện DataGridView
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(59, 130, 246);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.DefaultCellStyle.BackColor = Color.White;
            dataGridView.DefaultCellStyle.ForeColor = Color.FromArgb(55, 65, 81);
            dataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView.DefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            dataGridView.BackgroundColor = Color.White;
            dataGridView.GridColor = Color.FromArgb(229, 231, 235);
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(75, 85, 99);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 10, 8, 10);
            dataGridView.ColumnHeadersHeight = 45;
            dataGridView.RowTemplate.Height = 40;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ReadOnly = true;

            // Format headers to remove underscores
            dataGridView.DataBindingComplete += (s, e) =>
            {
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    column.HeaderText = column.HeaderText.Replace("_", " ");
                }
            };

            // Cải thiện Form Panel
            formPanel.BorderStyle = BorderStyle.None;
            formPanel.BackColor = Color.White;
            formPanel.Padding = new Padding(20);
            
            // Thêm border và shadow effect
            formPanel.Paint += (s, e) =>
            {
                var rect = formPanel.ClientRectangle;
                using (var pen = new Pen(Color.FromArgb(229, 231, 235), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, rect.Width - 1, rect.Height - 1);
                }
            };
        }

        protected void SetupSearchFunctionality(DataGridView dataGridView, params string[] searchColumns)
        {
            var exportBtn = ExportImportHelper.CreateExportButton(dataGridView, this.GetType().Name.Replace("Control", ""));
            
            searchBox = SearchHelper.CreateSearchBox(this, (s, e) => 
            {
                PerformAdvancedSearch(dataGridView, searchBox.Text, searchColumns);
            });
            
            // Áp dụng placeholder cho search box
            string searchPlaceholder = GetSearchPlaceholder(this.GetType().Name);
            if (!string.IsNullOrEmpty(searchPlaceholder))
            {
                if (!searchBox.IsHandleCreated)
                {
                    searchBox.HandleCreated += (s, e) => TextBoxHelper.SetPlaceholder(searchBox, searchPlaceholder, true);
                }
                else
                {
                    TextBoxHelper.SetPlaceholder(searchBox, searchPlaceholder, true);
                }
            }
            
            searchPanel = SearchHelper.CreateSearchPanel(searchBox, exportBtn);
            this.Controls.Add(searchPanel);
        }
        
        private string GetSearchPlaceholder(string controlName)
        {
            if (controlName?.Contains("User") == true) return "🔍 Tìm kiếm theo tên, email, username...";
            if (controlName?.Contains("Course") == true) return "🔍 Tìm kiếm theo tên khóa học, mô tả, giá...";
            if (controlName?.Contains("Test") == true) return "🔍 Tìm kiếm theo tên bài kiểm tra, mô tả...";
            if (controlName?.Contains("Category") == true) return "🔍 Tìm kiếm theo tên danh mục, mô tả...";
            return "🔍 Tìm kiếm...";
        }
        
        private void PerformAdvancedSearch(DataGridView dataGridView, string searchText, string[] searchColumns)
        {
            if (dataGridView.DataSource == null) return;
            
            var bindingSource = dataGridView.DataSource as BindingSource;
            if (bindingSource != null)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(searchText))
                    {
                        bindingSource.RemoveFilter();
                        return;
                    }
                    
                    var filters = new List<string>();
                    string normalizedSearch = searchText.Trim().Replace("'", "''");
                    
                    foreach (string column in searchColumns)
                    {
                        filters.Add($"CONVERT([{column}], System.String) LIKE '%{normalizedSearch}%'");
                    }
                    
                    if (filters.Count > 0)
                    {
                        bindingSource.Filter = string.Join(" OR ", filters);
                    }
                }
                catch
                {
                    // Nếu lỗi, sử dụng SearchHelper
                    SearchHelper.FilterDataGridView(dataGridView, searchText, searchColumns);
                }
            }
            else
            {
                // Không có BindingSource, sử dụng SearchHelper
                SearchHelper.FilterDataGridView(dataGridView, searchText, searchColumns);
            }
        }

        protected void SetupPagination(DataGridView dataGridView, Func<int, Task> loadDataCallback)
        {
            paginationPanel = paginationHelper.CreatePaginationPanel(async (page) => 
            {
                await loadDataCallback(page);
            });
            this.Controls.Add(paginationPanel);
        }

        protected async Task LogAdminActionAsync(string action, string entityType, int? entityId = null, string details = null)
        {
            try
            {
                AuditHelper.CheckPermission(action, entityType);
                await AuditHelper.LogActionAsync(action, entityType, entityId, details);
            }
            catch (UnauthorizedAccessException ex)
            {
                ValidationHelper.ShowValidationError(this.FindForm(), ex.Message);
                throw;
            }
        }

        protected bool ValidateInput(string validationResult)
        {
            if (!string.IsNullOrEmpty(validationResult))
            {
                ValidationHelper.ShowValidationError(this.FindForm(), validationResult);
                return false;
            }
            return true;
        }

        protected void ApplyModernFormStyling(Panel formPanel)
        {
            if (formPanel == null) return;

            foreach (Control control in formPanel.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Font = new Font("Segoe UI", 11);
                    textBox.Height = 35;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.BackColor = Color.White;
                    textBox.ForeColor = Color.FromArgb(55, 65, 81);
                    
                    SetupRealTimeValidation(textBox);
                    
                    // Áp dụng placeholder sau khi styling
                    string placeholder = GetPlaceholderText(textBox.Name);
                    if (!string.IsNullOrEmpty(placeholder))
                    {
                        if (!textBox.IsHandleCreated)
                        {
                            textBox.HandleCreated += (s, e) => TextBoxHelper.SetPlaceholder(textBox, placeholder, true);
                        }
                        else
                        {
                            TextBoxHelper.SetPlaceholder(textBox, placeholder, true);
                        }
                    }
                }
                else if (control is Button button)
                {
                    button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    button.Height = 40;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.Cursor = Cursors.Hand;
                    
                    // Màu sắc theo loại button
                    if (button.Name?.Contains("Save") == true || button.Name?.Contains("Add") == true)
                    {
                        button.BackColor = Color.FromArgb(34, 197, 94);
                        button.ForeColor = Color.White;
                    }
                    else if (button.Name?.Contains("Delete") == true)
                    {
                        button.BackColor = Color.FromArgb(239, 68, 68);
                        button.ForeColor = Color.White;
                    }
                    else if (button.Name?.Contains("Edit") == true)
                    {
                        button.BackColor = Color.FromArgb(59, 130, 246);
                        button.ForeColor = Color.White;
                    }
                    else if (button.Name?.Contains("Cancel") == true)
                    {
                        button.BackColor = Color.FromArgb(156, 163, 175);
                        button.ForeColor = Color.White;
                    }
                    else
                    {
                        button.BackColor = Color.FromArgb(75, 85, 99);
                        button.ForeColor = Color.White;
                    }
                }
                else if (control is Label label && !label.Name?.Contains("lbl") == true)
                {
                    label.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    label.ForeColor = Color.FromArgb(55, 65, 81);
                }
                else if (control is CheckBox checkBox)
                {
                    checkBox.Font = new Font("Segoe UI", 10);
                    checkBox.ForeColor = Color.FromArgb(55, 65, 81);
                }
            }
        }

        protected void AdjustResponsiveLayout(DataGridView dataGridView, Panel formPanel, int breakpoint = 1100, int rightOffset = 420)
        {
            if (dataGridView == null || formPanel == null) return;

            int padding = 20;
            int topOffset = searchPanel != null ? 80 : 60; // Giảm header height
            int availableWidth = Math.Max(1200, this.Width - padding * 2);
            int availableHeight = Math.Max(700, this.Height - topOffset - padding - 30);

            // Tăng tỷ lệ cho DataGridView
            int dataGridWidth = (int)(availableWidth * 0.72);
            int formWidth = availableWidth - dataGridWidth - 20;

            dataGridView.Location = new Point(padding, topOffset);
            dataGridView.Size = new Size(Math.Max(900, dataGridWidth), Math.Max(500, availableHeight));

            formPanel.Location = new Point(dataGridView.Right + 20, topOffset);
            formPanel.Size = new Size(Math.Max(350, formWidth), Math.Max(600, availableHeight));
        }

        protected void ApplyPlaceholdersToAllTextBoxes(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is TextBox textBox)
                {
                    string placeholder = GetPlaceholderText(textBox.Name);
                    if (!string.IsNullOrEmpty(placeholder))
                    {
                        // Đảm bảo TextBox đã có handle trước khi áp dụng placeholder
                        if (!textBox.IsHandleCreated)
                        {
                            textBox.HandleCreated += (s, e) => TextBoxHelper.SetPlaceholder(textBox, placeholder, true);
                        }
                        else
                        {
                            TextBoxHelper.SetPlaceholder(textBox, placeholder, true);
                        }
                    }
                }
            }
        }
        
        private string GetPlaceholderText(string textBoxName)
        {
            // Email fields
            if (textBoxName?.Contains("Email") == true) return "📧 Nhập địa chỉ email hợp lệ (vd: user@example.com)";
            
            // User fields
            if (textBoxName?.Contains("Username") == true) return "👤 Nhập tên đăng nhập (chỉ chữ, số và dấu gạch dưới, tối thiểu 3 ký tự)";
            if (textBoxName?.Contains("FullName") == true) return "🏷️ Nhập họ và tên đầy đủ (tối thiểu 3 ký tự)";
            if (textBoxName?.Contains("FirstName") == true) return "👤 Nhập tên (tối thiểu 2 ký tự)";
            if (textBoxName?.Contains("LastName") == true) return "👤 Nhập họ (tối thiểu 2 ký tự)";
            
            // Course/Test fields
            if (textBoxName?.Contains("Title") == true) return "📝 Nhập tiêu đề (3-200 ký tự)";
            if (textBoxName?.Contains("Description") == true) return "📄 Nhập mô tả chi tiết (tùy chọn)";
            if (textBoxName?.Contains("Price") == true) return "💰 Nhập giá: 0 (miễn phí) hoặc 10,000-2,000,000 VND";
            if (textBoxName?.Contains("TimeLimit") == true) return "⏱️ Thời gian làm bài: 15-120 phút (bỏ trống = không giới hạn)";
            
            // Category/General name fields
            if (textBoxName?.Contains("Name") == true) return "🏷️ Nhập tên (3-200 ký tự)";
            
            // Contact fields
            if (textBoxName?.Contains("Phone") == true) return "📞 Nhập số điện thoại (vd: 0901234567)";
            if (textBoxName?.Contains("Address") == true) return "🏠 Nhập địa chỉ (tùy chọn)";
            
            // Password fields
            if (textBoxName?.Contains("Password") == true) return "🔒 Nhập mật khẩu (tối thiểu 6 ký tự)";
            if (textBoxName?.Contains("ConfirmPassword") == true) return "🔒 Xác nhận lại mật khẩu";
            
            // Search fields
            if (textBoxName?.Contains("Search") == true) return "🔍 Tìm kiếm...";
            
            // Numeric fields
            if (textBoxName?.Contains("Score") == true) return "🎯 Nhập điểm số (0-100)";
            if (textBoxName?.Contains("Duration") == true) return "⏱️ Nhập thời lượng (phút)";
            if (textBoxName?.Contains("Order") == true) return "🔢 Nhập thứ tự sắp xếp";
            
            // URL/Link fields
            if (textBoxName?.Contains("Url") == true || textBoxName?.Contains("Link") == true) return "🔗 Nhập đường dẫn (vd: https://example.com)";
            
            // Date fields (if using TextBox for dates)
            if (textBoxName?.Contains("Date") == true) return "📅 Nhập ngày (dd/mm/yyyy)";
            
            // Default fallback
            return "✏️ Nhập thông tin...";
        }
        
        protected void SetupRealTimeValidation(TextBox textBox)
        {
            var errorLabel = new Label
            {
                ForeColor = Color.FromArgb(239, 68, 68),
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Visible = false,
                Location = new Point(textBox.Left, textBox.Bottom + 2)
            };
            
            textBox.Parent.Controls.Add(errorLabel);
            
            textBox.TextChanged += (s, e) =>
            {
                var error = ValidateTextBox(textBox);
                if (!string.IsNullOrEmpty(error))
                {
                    errorLabel.Text = error;
                    errorLabel.Visible = true;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    errorLabel.Visible = false;
                }
            };
        }
        
        protected string ValidateTextBox(TextBox textBox)
        {
            var text = textBox.Text;
            var placeholder = textBox.PlaceholderText;
            
            if (text == placeholder || string.IsNullOrWhiteSpace(text))
                return "";
                
            if (textBox.Name?.Contains("Email") == true)
            {
                if (!text.Contains("@") || !text.Contains("."))
                    return "Email không hợp lệ";
            }
            else if (textBox.Name?.Contains("Price") == true)
            {
                if (!decimal.TryParse(text, out var price) || price < 0)
                    return "Giá không hợp lệ";
                if (price > 0 && (price < 10000 || price > 2000000))
                    return "Giá phải từ 10,000 - 2,000,000 VND hoặc 0 (miễn phí)";
            }
            else if (textBox.Name?.Contains("TimeLimit") == true)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    if (!int.TryParse(text, out var time) || time < 15 || time > 120)
                        return "Thời gian phải từ 15-120 phút";
                }
            }
            else if (textBox.Name?.Contains("Title") == true || textBox.Name?.Contains("Name") == true)
            {
                if (text.Length < 3)
                    return "Tên phải ít nhất 3 ký tự";
                if (text.Length > 200)
                    return "Tên không quá 200 ký tự";
            }
            else if (textBox.Name?.Contains("Username") == true)
            {
                if (text.Length < 3)
                    return "Username phải ít nhất 3 ký tự";
                if (!System.Text.RegularExpressions.Regex.IsMatch(text, "^[a-zA-Z0-9_]+$"))
                    return "Username chỉ chứa chữ, số và dấu gạch dưới";
            }
            
            return "";
        }

        protected string GetTextValue(TextBox textBox)
        {
            return textBox.Text;
        }
        
        protected void SetTextValue(TextBox textBox, string value)
        {
            textBox.Text = value ?? "";
        }

        protected void AdjustBottomPanelLayout(Panel bottomPanel, int minHeight = 300)
        {
            if (bottomPanel == null) return;
            
            int availableHeight = Math.Max(minHeight, (int)(this.Height * 0.35));
            bottomPanel.Height = Math.Min(availableHeight, 400);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _adminController?.Dispose();
                searchBox?.Dispose();
                searchPanel?.Dispose();
                paginationPanel?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
