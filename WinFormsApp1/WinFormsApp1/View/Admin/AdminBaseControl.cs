using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
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
            if (dataGridView == null) return;

            // Modern DataGridView styling
            dataGridView.BorderStyle = BorderStyle.FixedSingle;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.GridColor = Color.FromArgb(224, 224, 224);
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            
            // Header styling
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersHeight = 35;
            
            // Row styling
            dataGridView.DefaultCellStyle.BackColor = Color.White;
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridView.RowTemplate.Height = 35;
            
            // Selection styling
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 122, 183);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            
            // Behavior
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ReadOnly = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Format headers
            dataGridView.DataBindingComplete += (s, e) =>
            {
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    column.HeaderText = column.HeaderText.Replace("_", " ");
                }
            };

            if (formPanel != null)
            {
                formPanel.BorderStyle = BorderStyle.None;
                formPanel.BackColor = Color.White;
                formPanel.Padding = new Padding(20);
            }
        }

        protected Panel CreateTopPanel(string title)
        {
            var topPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            topPanel.Controls.Add(titleLabel);

            return topPanel;
        }

        protected Panel CreateCrudButtonPanel()
        {
            var buttonPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };

            var addBtn = new Button
            {
                Text = "Thêm mới",
                Size = new Size(100, 35),
                Location = new Point(20, 12),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Name = "btnAdd"
            };
            addBtn.FlatAppearance.BorderSize = 0;

            var editBtn = new Button
            {
                Text = "Sửa",
                Size = new Size(80, 35),
                Location = new Point(130, 12),
                BackColor = Color.FromArgb(52, 144, 220),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Name = "btnEdit"
            };
            editBtn.FlatAppearance.BorderSize = 0;

            var deleteBtn = new Button
            {
                Text = "Xóa",
                Size = new Size(80, 35),
                Location = new Point(220, 12),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Name = "btnDelete"
            };
            deleteBtn.FlatAppearance.BorderSize = 0;

            var refreshBtn = new Button
            {
                Text = "Làm mới",
                Size = new Size(90, 35),
                Location = new Point(310, 12),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Name = "btnRefresh"
            };
            refreshBtn.FlatAppearance.BorderSize = 0;

            buttonPanel.Controls.AddRange(new Control[] { addBtn, editBtn, deleteBtn, refreshBtn });
            return buttonPanel;
        }

        protected Panel CreateFilterPanel()
        {
            var filterPanel = new Panel
            {
                Height = 50,
                Dock = DockStyle.Top,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };

            var showLabel = new Label
            {
                Text = "Hiển thị",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(20, 15)
            };
            filterPanel.Controls.Add(showLabel);

            var entriesCombo = new ComboBox
            {
                Items = { "10", "25", "50", "100" },
                SelectedIndex = 0,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                Size = new Size(60, 25),
                Location = new Point(70, 12)
            };
            filterPanel.Controls.Add(entriesCombo);

            var entriesLabel = new Label
            {
                Text = "dữ liệu",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(140, 15)
            };
            filterPanel.Controls.Add(entriesLabel);

            var searchLabel = new Label
            {
                Text = "Tìm kiếm:",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(filterPanel.Width - 300, 15)
            };
            filterPanel.Controls.Add(searchLabel);

            searchBox = new TextBox
            {
                Font = new Font("Segoe UI", 9),
                Size = new Size(200, 25),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(filterPanel.Width - 220, 12),
                BorderStyle = BorderStyle.FixedSingle
            };
            filterPanel.Controls.Add(searchBox);

            return filterPanel;
        }

        protected void SetupSearchFunctionality(DataGridView dataGridView, params string[] searchColumns)
        {
            if (searchBox != null)
            {
                searchBox.TextChanged += (s, e) => PerformAdvancedSearch(dataGridView, searchBox.Text, searchColumns);
            }
        }



        protected void SetupLayout(string title, DataGridView dataGridView)
        {
            this.SuspendLayout();
            this.Controls.Clear();
            
            var topPanel = CreateTopPanel(title);
            var buttonPanel = CreateCrudButtonPanel();
            var filterPanel = CreateFilterPanel();
            var paginationPanel = CreatePaginationPanel();
            
            dataGridView.Dock = DockStyle.Fill;
            
            this.Controls.Add(dataGridView);
            this.Controls.Add(paginationPanel);
            this.Controls.Add(filterPanel);
            this.Controls.Add(buttonPanel);
            this.Controls.Add(topPanel);
            
            this.ResumeLayout();
        }

        protected void SetupLayoutWithForm(string title, DataGridView dataGridView, Panel formPanel)
        {
            this.SuspendLayout();
            this.Controls.Clear();
            
            var topPanel = CreateTopPanel(title);
            var buttonPanel = CreateCrudButtonPanel();
            var filterPanel = CreateFilterPanel();
            var paginationPanel = CreatePaginationPanel();
            
            dataGridView.Dock = DockStyle.Fill;
            
            this.Controls.Add(dataGridView);
            this.Controls.Add(paginationPanel);
            this.Controls.Add(filterPanel);
            this.Controls.Add(buttonPanel);
            this.Controls.Add(topPanel);
            this.Controls.Add(formPanel);
            
            this.ResumeLayout();
        }
        
        protected Panel inputFormPanel;
        protected bool isFormVisible = false;

        protected virtual void OnAddButtonClick(object sender, EventArgs e) { }
        protected virtual void OnEditButtonClick(object sender, EventArgs e) { }
        protected virtual void OnDeleteButtonClick(object sender, EventArgs e) { }
        protected virtual void OnRefreshButtonClick(object sender, EventArgs e) { }

        protected void WireCrudEvents()
        {
            var addBtn = this.Controls.Find("btnAdd", true).FirstOrDefault() as Button;
            var editBtn = this.Controls.Find("btnEdit", true).FirstOrDefault() as Button;
            var deleteBtn = this.Controls.Find("btnDelete", true).FirstOrDefault() as Button;
            var refreshBtn = this.Controls.Find("btnRefresh", true).FirstOrDefault() as Button;

            if (addBtn != null) addBtn.Click += OnAddButtonClick;
            if (editBtn != null) editBtn.Click += OnEditButtonClick;
            if (deleteBtn != null) deleteBtn.Click += OnDeleteButtonClick;
            if (refreshBtn != null) refreshBtn.Click += OnRefreshButtonClick;
        }

        protected Panel CreateInputForm(string title, params (string label, string name, string placeholder, bool required, bool isPassword)[] fields)
        {
            inputFormPanel = new Panel
            {
                Width = 350,
                Dock = DockStyle.Left,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            var headerPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(248, 249, 250)
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(20, 18),
                AutoSize = true
            };

            var closeBtn = new Button
            {
                Text = "×",
                Size = new Size(30, 30),
                Location = new Point(310, 15),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.Gray,
                Name = "btnCloseForm"
            };
            closeBtn.FlatAppearance.BorderSize = 0;
            closeBtn.Click += (s, e) => HideInputForm();

            headerPanel.Controls.AddRange(new Control[] { titleLabel, closeBtn });

            var scrollPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20)
            };

            int yPos = 20;
            foreach (var field in fields)
            {
                var fieldLabel = new Label
                {
                    Text = field.label + (field.required ? " *" : ""),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new Point(0, yPos),
                    Size = new Size(300, 20),
                    ForeColor = field.required ? Color.FromArgb(220, 53, 69) : Color.Black
                };

                var textBox = new TextBox
                {
                    Name = field.name,
                    Font = new Font("Segoe UI", 10),
                    Size = new Size(300, 30),
                    Location = new Point(0, yPos + 25),
                    BorderStyle = BorderStyle.FixedSingle,
                    UseSystemPasswordChar = field.isPassword
                };
                
                // Set placeholder using TextBoxHelper
                TextBoxHelper.SetPlaceholder(textBox, field.placeholder, true);
                
                // Add real-time validation
                textBox.TextChanged += (s, e) => ValidateField(field.name, field.required, field.isPassword);
                textBox.Leave += (s, e) => ValidateField(field.name, field.required, field.isPassword);

                var errorLabel = new Label
                {
                    Name = field.name + "Error",
                    Font = new Font("Segoe UI", 9),
                    Size = new Size(300, 30),
                    Location = new Point(0, yPos + 60),
                    ForeColor = Color.FromArgb(220, 53, 69),
                    Visible = false,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                scrollPanel.Controls.AddRange(new Control[] { fieldLabel, textBox, errorLabel });
                yPos += 95;
            }

            var buttonPanel = new Panel
            {
                Height = 70,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20, 15, 20, 15)
            };

            var saveBtn = new Button
            {
                Text = "Lưu",
                Size = new Size(80, 35),
                Location = new Point(20, 15),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Name = "btnSave"
            };
            saveBtn.FlatAppearance.BorderSize = 0;

            var cancelBtn = new Button
            {
                Text = "Hủy",
                Size = new Size(80, 35),
                Location = new Point(110, 15),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Name = "btnCancel"
            };
            cancelBtn.FlatAppearance.BorderSize = 0;
            cancelBtn.Click += (s, e) => HideInputForm();

            buttonPanel.Controls.AddRange(new Control[] { saveBtn, cancelBtn });

            inputFormPanel.Controls.AddRange(new Control[] { scrollPanel, buttonPanel, headerPanel });
            return inputFormPanel;
        }

        protected void ShowInputForm()
        {
            if (inputFormPanel != null)
            {
                inputFormPanel.Visible = true;
                isFormVisible = true;
                inputFormPanel.BringToFront();
            }
        }

        protected void HideInputForm()
        {
            if (inputFormPanel != null)
            {
                inputFormPanel.Visible = false;
                isFormVisible = false;
                ClearFormInputs();
                ClearFormErrors();
            }
        }

        protected void ClearFormInputs()
        {
            if (inputFormPanel == null) return;
            
            // Get all TextBox controls recursively
            var textBoxes = GetAllControls(inputFormPanel).OfType<TextBox>();
            
            foreach (var textBox in textBoxes)
            {
                textBox.Text = "";
            }
        }
        
        protected IEnumerable<Control> GetAllControls(Control container)
        {
            var controls = new List<Control>();
            foreach (Control control in container.Controls)
            {
                controls.Add(control);
                if (control.HasChildren)
                {
                    controls.AddRange(GetAllControls(control));
                }
            }
            return controls;
        }

        protected void ClearFormErrors()
        {
            if (inputFormPanel == null) return;
            
            // Get all Label controls that are error labels
            var errorLabels = GetAllControls(inputFormPanel).OfType<Label>()
                .Where(l => l.Name != null && l.Name.EndsWith("Error"));
            
            foreach (var errorLabel in errorLabels)
            {
                errorLabel.Visible = false;
                errorLabel.Text = "";
            }
        }

        protected void ShowFieldError(string fieldName, string errorMessage)
        {
            if (inputFormPanel == null) return;
            var errorLabel = inputFormPanel.Controls.Find(fieldName + "Error", true).FirstOrDefault() as Label;
            if (errorLabel != null)
            {
                errorLabel.Text = errorMessage;
                errorLabel.Visible = true;
            }
        }
        
        protected void HideFieldError(string fieldName)
        {
            if (inputFormPanel == null) return;
            var errorLabel = inputFormPanel.Controls.Find(fieldName + "Error", true).FirstOrDefault() as Label;
            if (errorLabel != null)
            {
                errorLabel.Visible = false;
                errorLabel.Text = "";
            }
        }
        
        protected virtual void ValidateField(string fieldName, bool required, bool isPassword)
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
            
            // Email validation
            if (fieldName.ToLower().Contains("email") && !value.Contains("@"))
            {
                ShowFieldError(fieldName, "Email không hợp lệ");
                return;
            }
            
            // Password validation
            if (isPassword && value.Length < 6)
            {
                ShowFieldError(fieldName, "Mật khẩu phải có ít nhất 6 ký tự");
                return;
            }
        }
        
        protected virtual string GetRequiredErrorMessage(string fieldName)
        {
            return fieldName.ToLower() switch
            {
                var name when name.Contains("email") => "Email không được để trống",
                var name when name.Contains("fullname") => "Họ tên không được để trống",
                var name when name.Contains("username") => "Tên đăng nhập không được để trống",
                var name when name.Contains("password") => "Mật khẩu không được để trống",
                var name when name.Contains("name") => "Tên không được để trống",
                var name when name.Contains("title") => "Tiêu đề không được để trống",
                _ => "Trường này không được để trống"
            };
        }

        protected string GetFormValue(string fieldName)
        {
            if (inputFormPanel == null) return "";
            var textBox = inputFormPanel.Controls.Find(fieldName, true).FirstOrDefault() as TextBox;
            return textBox?.Text ?? "";
        }

        protected void SetFormValue(string fieldName, string value)
        {
            if (inputFormPanel == null) return;
            var textBox = inputFormPanel.Controls.Find(fieldName, true).FirstOrDefault() as TextBox;
            if (textBox != null)
            {
                textBox.Text = value ?? "";
            }
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

        protected Panel CreatePaginationPanel()
        {
            var paginationPanel = new Panel
            {
                Height = 50,
                Dock = DockStyle.Bottom,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };

            var infoLabel = new Label
            {
                Text = "Hiển thị 1 tới 1 của 1 dữ liệu",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(20, 15)
            };
            paginationPanel.Controls.Add(infoLabel);

            var buttonPanel = new Panel
            {
                Size = new Size(300, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(paginationPanel.Width - 320, 10)
            };

            var firstBtn = CreatePaginationButton("Đầu tiên", 0);
            var prevBtn = CreatePaginationButton("Trước", 80);
            var currentBtn = CreatePaginationButton("1", 140, true);
            var nextBtn = CreatePaginationButton("Sau", 170);
            var lastBtn = CreatePaginationButton("Cuối cùng", 220);

            buttonPanel.Controls.AddRange(new Control[] { firstBtn, prevBtn, currentBtn, nextBtn, lastBtn });
            paginationPanel.Controls.Add(buttonPanel);

            return paginationPanel;
        }

        private Button CreatePaginationButton(string text, int x, bool isActive = false)
        {
            return new Button
            {
                Text = text,
                Size = new Size(text == "1" ? 25 : 70, 25),
                Location = new Point(x, 0),
                BackColor = isActive ? Color.FromArgb(51, 122, 183) : Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8),
                Enabled = !isActive
            };
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



        protected string GetTextValue(TextBox textBox)
        {
            return textBox?.Text ?? "";
        }

        protected void SetTextValue(TextBox textBox, string value)
        {
            if (textBox != null)
                textBox.Text = value ?? "";
        }

        protected void AdjustResponsiveLayout(DataGridView dataGridView, Panel formPanel, int breakpoint = 1100, int rightOffset = 420)
        {
            // Layout responsive - có thể để trống hoặc implement sau
        }

        protected void AdjustBottomPanelLayout(Panel bottomPanel, int minHeight = 300)
        {
            if (bottomPanel != null)
            {
                bottomPanel.Height = Math.Max(minHeight, (int)(this.Height * 0.35));
            }
        }

        protected void UpdateDataGridHeaders(DataGridView dataGridView, Dictionary<string, string> columnHeaders)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (columnHeaders.ContainsKey(column.Name))
                {
                    column.HeaderText = columnHeaders[column.Name];
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _adminController?.Dispose();
                searchBox?.Dispose();
                searchPanel?.Dispose();
                paginationPanel?.Dispose();
                inputFormPanel?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}