using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;

namespace WinFormsApp1.View.Admin
{
    public abstract class AdminBaseControl : UserControl
    {
        protected readonly AdminController _adminController;
        protected DataGridView dataGridView;
        protected TextBox searchBox;
        protected PaginationHelper paginationHelper;
        protected Panel inputFormPanel;
        protected bool isFormVisible = false;
        
        // Filter panel components
        protected Panel filterPanel;
        protected FlowLayoutPanel filterLeftPanel;
        protected FlowLayoutPanel filterCenterPanel;
        protected FlowLayoutPanel filterRightPanel;

        /// <summary>
        /// Shorthand for LanguageHelper.GetString
        /// </summary>
        protected static string Lang(string key) => LanguageHelper.GetString(key);
        protected static string Lang(string key, params object[] args) => LanguageHelper.GetString(key, args);

        protected AdminBaseControl(AdminController controller = null)
        {
            _adminController = controller ?? new AdminController();
            paginationHelper = new PaginationHelper(50);
            
            // Handle DPI changes
            this.Font = SystemFonts.MessageBoxFont;
            this.AutoScaleMode = AutoScaleMode.Dpi;
            
            // Subscribe to DPI changed events
            this.HandleCreated += OnHandleCreated;
        }

        private void OnHandleCreated(object sender, EventArgs e)
        {
            // Hook into Windows message to detect DPI changes
            this.HandleDestroyed += (s, args) => { };
        }

        /// <summary>
        /// Tạo một DataGridView hiện đại với styling đẹp
        /// </summary>
        protected DataGridView CreateModernDataGridView()
        {
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 10),
                GridColor = Color.FromArgb(224, 224, 224)
            };

            // Modern header styling
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 144, 220);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 45;

            // Modern row styling
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(59, 130, 246);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Padding = new Padding(5);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.RowTemplate.Height = 35;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);

            return dgv;
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
                Text = Lang("Add"),
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
                Text = Lang("Edit"),
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
                Text = Lang("Delete"),
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
                Text = Lang("Refresh"),
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

        protected virtual Panel CreateFilterPanel()
        {
            filterPanel = new Panel
            {
                Height = 50,
                Dock = DockStyle.Top,
                BackColor = Color.White,
                Name = "filterPanel"
            };

            // Use TableLayoutPanel for responsive layout
            var tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = Color.White,
                Padding = new Padding(15, 5, 15, 5)
            };
            
            // Left: 15%, Center: 55%, Right: 30%
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Left panel - "Hiển thị X dữ liệu"
            filterLeftPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = false,
                Margin = new Padding(0)
            };

            var showLabel = new Label
            {
                Text = Lang("Show"),
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Margin = new Padding(0, 8, 5, 0)
            };

            var entriesCombo = new ComboBox
            {
                Name = "cboEntries",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                Width = 55,
                Margin = new Padding(0, 4, 5, 0)
            };
            entriesCombo.Items.AddRange(new object[] { "10", "25", "50", "100" });
            entriesCombo.SelectedIndex = 2; // Default 50

            var entriesLabel = new Label
            {
                Text = Lang("Entries"),
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Margin = new Padding(0, 8, 0, 0)
            };

            filterLeftPanel.Controls.AddRange(new Control[] { showLabel, entriesCombo, entriesLabel });

            // Center panel - Custom filters will be added here
            filterCenterPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoSize = false,
                Margin = new Padding(0),
                Name = "filterCenterPanel"
            };

            // Right panel - Search box
            filterRightPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoSize = false,
                Margin = new Padding(0)
            };

            searchBox = new TextBox
            {
                Name = "txtSearch",
                Font = new Font("Segoe UI", 9),
                Width = 220,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 4, 0, 0)
            };
            TextBoxHelper.SetPlaceholder(searchBox, $"{Lang("Search")}...", true);

            var searchLabel = new Label
            {
                Text = $"{Lang("Search")}:",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Margin = new Padding(0, 8, 8, 0)
            };

            // Add in reverse order for RightToLeft flow
            filterRightPanel.Controls.Add(searchBox);
            filterRightPanel.Controls.Add(searchLabel);

            // Add panels to table layout
            tableLayout.Controls.Add(filterLeftPanel, 0, 0);
            tableLayout.Controls.Add(filterCenterPanel, 1, 0);
            tableLayout.Controls.Add(filterRightPanel, 2, 0);

            filterPanel.Controls.Add(tableLayout);

            return filterPanel;
        }
        
        /// <summary>
        /// Helper method để thêm custom filter controls vào filter panel.
        /// Gọi method này SAU KHI SetupLayout() đã chạy xong.
        /// </summary>
        protected void AddCustomFilter(string labelText, ComboBox comboBox)
        {
            if (filterCenterPanel == null)
            {
                var fp = this.Controls.Find("filterPanel", true).FirstOrDefault() as Panel;
                if (fp == null)
                {
                    System.Diagnostics.Debug.WriteLine("WARNING: filterPanel not found. Call this after SetupLayout()");
                    return;
                }
                
                filterCenterPanel = fp.Controls.Find("filterCenterPanel", true).FirstOrDefault() as FlowLayoutPanel;
                if (filterCenterPanel == null)
                {
                    System.Diagnostics.Debug.WriteLine("WARNING: filterCenterPanel not found");
                    return;
                }
            }

            var label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Margin = new Padding(5, 8, 3, 0),
                Name = $"lbl{comboBox.Name}"
            };

            comboBox.Font = new Font("Segoe UI", 9);
            comboBox.Margin = new Padding(0, 4, 10, 0);
            
            filterCenterPanel.Controls.Add(label);
            filterCenterPanel.Controls.Add(comboBox);
            
            System.Diagnostics.Debug.WriteLine($"Added filter: {labelText}");
        }
        
        /// <summary>
        /// Helper method để thêm nhiều custom filters cùng lúc
        /// </summary>
        protected void AddCustomFilters(params (string label, ComboBox combo)[] filters)
        {
            foreach (var filter in filters)
            {
                AddCustomFilter(filter.label, filter.combo);
            }
        }
        
        /// <summary>
        /// Thêm DateTimePicker vào filter panel
        /// </summary>
        protected void AddDateFilter(string labelText, DateTimePicker datePicker)
        {
            if (filterCenterPanel == null)
            {
                var fp = this.Controls.Find("filterPanel", true).FirstOrDefault() as Panel;
                filterCenterPanel = fp?.Controls.Find("filterCenterPanel", true).FirstOrDefault() as FlowLayoutPanel;
                if (filterCenterPanel == null) return;
            }

            var label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Margin = new Padding(5, 8, 3, 0)
            };

            datePicker.Font = new Font("Segoe UI", 9);
            datePicker.Format = DateTimePickerFormat.Short;
            datePicker.Margin = new Padding(0, 4, 10, 0);

            filterCenterPanel.Controls.Add(label);
            filterCenterPanel.Controls.Add(datePicker);
        }
        
        protected void AddFilterControl(Control control)
        {
            if (filterCenterPanel == null)
            {
                var fp = this.Controls.Find("filterPanel", true).FirstOrDefault() as Panel;
                filterCenterPanel = fp?.Controls.Find("filterCenterPanel", true).FirstOrDefault() as FlowLayoutPanel;
                if (filterCenterPanel == null) return;
            }

            control.Margin = new Padding(5, 4, 10, 0);
            filterCenterPanel.Controls.Add(control);
        }

        protected virtual void SetupSearchFunctionality(DataGridView dataGridView, params string[] searchColumns)
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
            var filterPnl = CreateFilterPanel();
            
            dataGridView.Dock = DockStyle.Fill;
            
            this.Controls.Add(dataGridView);
            this.Controls.Add(filterPnl);
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
            var filterPnl = CreateFilterPanel();
            
            dataGridView.Dock = DockStyle.Fill;
            
            this.Controls.Add(dataGridView);
            this.Controls.Add(filterPnl);
            this.Controls.Add(buttonPanel);
            this.Controls.Add(topPanel);
            this.Controls.Add(formPanel);
            
            this.ResumeLayout();
        }

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
                
                TextBoxHelper.SetPlaceholder(textBox, field.placeholder, true);
                
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
                Text = Lang("Save"),
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
                Text = Lang("Cancel"),
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
            
            HideFieldError(fieldName);
            
            if (required && string.IsNullOrEmpty(value))
            {
                ShowFieldError(fieldName, GetRequiredErrorMessage(fieldName));
                return;
            }
            
            if (string.IsNullOrEmpty(value)) return;
            
            if (fieldName.ToLower().Contains("email") && !value.Contains("@"))
            {
                ShowFieldError(fieldName, Lang("InvalidEmail"));
                return;
            }
            
            if (isPassword && value.Length < 6)
            {
                ShowFieldError(fieldName, Lang("PasswordTooShort"));
                return;
            }
        }
        
        protected virtual string GetRequiredErrorMessage(string fieldName)
        {
            return fieldName.ToLower() switch
            {
                var name when name.Contains("email") => Lang("EmailRequired"),
                var name when name.Contains("fullname") => Lang("FullNameRequired"),
                var name when name.Contains("username") => Lang("UsernameRequired"),
                var name when name.Contains("password") => Lang("PasswordRequired"),
                var name when name.Contains("name") => Lang("NameRequired"),
                var name when name.Contains("title") => Lang("TitleRequired"),
                _ => Lang("FieldRequired")
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
                    SearchHelper.FilterDataGridView(dataGridView, searchText, searchColumns);
                }
            }
            else
            {
                SearchHelper.FilterDataGridView(dataGridView, searchText, searchColumns);
            }
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
                inputFormPanel?.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_DPICHANGED = 0x02E0;
            
            if (m.Msg == WM_DPICHANGED)
            {
                int newDpi = (int)(m.WParam.ToInt64() & 0xFFFF);
                float dpiScale = newDpi / 96f;
                
                UpdateUiForDpi(dpiScale);
            }
            
            base.WndProc(ref m);
        }

        private void UpdateUiForDpi(float dpiScale)
        {
            if (dataGridView != null)
            {
                try
                {
                    dataGridView.Font = new Font("Segoe UI", 10 * dpiScale, FontStyle.Regular, GraphicsUnit.Point);
                    dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11 * dpiScale, FontStyle.Bold, GraphicsUnit.Point);
                    dataGridView.ColumnHeadersHeight = (int)(45 * dpiScale);
                    dataGridView.RowTemplate.Height = (int)(40 * dpiScale);
                }
                catch { }
            }

            if (searchBox != null)
            {
                try
                {
                    searchBox.Font = new Font("Segoe UI", 9 * dpiScale, FontStyle.Regular, GraphicsUnit.Point);
                }
                catch { }
            }

            UpdateButtonsDpi(dpiScale);

            if (inputFormPanel != null && inputFormPanel.Visible)
            {
                UpdateInputFormDpi(dpiScale);
            }

            try
            {
                this.PerformLayout();
            }
            catch { }
        }

        private void UpdateButtonsDpi(float dpiScale)
        {
            try
            {
                var buttons = this.Controls.Find("btnAdd", true)
                    .Concat(this.Controls.Find("btnEdit", true))
                    .Concat(this.Controls.Find("btnDelete", true))
                    .Concat(this.Controls.Find("btnRefresh", true))
                    .OfType<Button>();

                foreach (var btn in buttons)
                {
                    btn.Font = new Font("Segoe UI", 9 * dpiScale, FontStyle.Bold, GraphicsUnit.Point);
                    btn.Height = (int)(35 * dpiScale);
                }

                if (filterPanel != null)
                {
                    foreach (var control in GetAllControls(filterPanel))
                    {
                        if (control is Label label)
                        {
                            label.Font = new Font("Segoe UI", 9 * dpiScale, FontStyle.Regular, GraphicsUnit.Point);
                        }
                        else if (control is ComboBox combo)
                        {
                            combo.Font = new Font("Segoe UI", 9 * dpiScale, FontStyle.Regular, GraphicsUnit.Point);
                        }
                        else if (control is TextBox textBox)
                        {
                            textBox.Font = new Font("Segoe UI", 9 * dpiScale, FontStyle.Regular, GraphicsUnit.Point);
                        }
                    }
                }
            }
            catch { }
        }

        private void UpdateInputFormDpi(float dpiScale)
        {
            if (inputFormPanel == null) return;

            try
            {
                inputFormPanel.Width = (int)(350 * dpiScale);

                foreach (var control in GetAllControls(inputFormPanel))
                {
                    if (control is Label label)
                    {
                        if (label.Font.Bold)
                            label.Font = new Font("Segoe UI", 10 * dpiScale, FontStyle.Bold, GraphicsUnit.Point);
                        else
                            label.Font = new Font("Segoe UI", 9 * dpiScale, FontStyle.Regular, GraphicsUnit.Point);
                    }
                    else if (control is TextBox textBox)
                    {
                        textBox.Font = new Font("Segoe UI", 10 * dpiScale, FontStyle.Regular, GraphicsUnit.Point);
                    }
                    else if (control is Button btn)
                    {
                        btn.Font = new Font("Segoe UI", 10 * dpiScale, FontStyle.Bold, GraphicsUnit.Point);
                        btn.Height = (int)(35 * dpiScale);
                    }
                }
            }
            catch { }
        }
    }
}