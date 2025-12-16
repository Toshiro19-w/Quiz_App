using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers.Admin;
using WinFormsApp1.Helpers;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.Admin
{
    /// <summary>
    /// Control quản lý và xem lịch sử hoạt động (Audit Logs) của hệ thống
    /// </summary>
    public partial class AuditLogManagementControl : AdminBaseControl
    {
        private readonly AuditLogController _auditController;
        
        // Stats panel
        private FlowLayoutPanel _statsFlowPanel;
        
        // Filter controls
        private DateTimePicker _dtpStartDate;
        private DateTimePicker _dtpEndDate;
        private ComboBox _cboAction;
        private ComboBox _cboEntityType;
        
        // Pagination
        private Label _lblPageInfo;
        private Button _btnPrevPage;
        private Button _btnNextPage;
        private ComboBox _cboPageSize;
        
        // State
        private AuditLogFilter _currentFilter;
        private AuditLogPagedResult _currentResult;
        private AuditLogStatistics _statistics;
        private List<AuditLogViewModel> _allLogs = new();
        private System.Windows.Forms.Timer _searchTimer;

        public AuditLogManagementControl() : base()
        {
            _auditController = new AuditLogController();
            _currentFilter = new AuditLogFilter { PageNumber = 1, PageSize = 50 };
            
            // Setup search debounce timer
            _searchTimer = new System.Windows.Forms.Timer { Interval = 300 };
            _searchTimer.Tick += async (s, e) =>
            {
                _searchTimer.Stop();
                await SearchAsync();
            };
            
            InitializeComponent();
        }

        private async void AuditLogManagementControl_Load(object sender, EventArgs e)
        {
            // Create modern DataGridView
            dataGridView = CreateModernDataGridView();
            dataGridView.CellClick += DgvLogs_CellClick;
            dataGridView.CellFormatting += DgvLogs_CellFormatting;
            
            // Setup columns
            SetupDataGridColumns();
            
            // Setup layout without form (read-only view)
            SetupLayout("Lịch sử hoạt động hệ thống", dataGridView);
            
            // Hide CRUD buttons and customize for audit log
            SetupButtons();
            
            // Add stats panel
            AddStatsPanel();
            
            // Add custom date filters
            SetupCustomFilters();
            
            // Setup search
            SetupSearchBox();
            
            // Setup pagination
            SetupPaginationPanel();
            
            // Handle resize for responsive stats
            this.Resize += AuditLogManagementControl_Resize;
            
            // Load data
            await LoadDataAsync();
        }

        private void AuditLogManagementControl_Resize(object sender, EventArgs e)
        {
            UpdateStatsCardWidths();
        }

        private void UpdateStatsCardWidths()
        {
            if (_statsFlowPanel == null || _statsFlowPanel.Controls.Count == 0) return;

            int availableWidth = _statsFlowPanel.ClientSize.Width - _statsFlowPanel.Padding.Horizontal - 20;
            int cardCount = _statsFlowPanel.Controls.Count;
            int cardWidth = Math.Max(160, (availableWidth - (cardCount - 1) * 15) / cardCount);

            foreach (Control card in _statsFlowPanel.Controls)
            {
                card.Width = cardWidth;
            }
        }

        private void SetupDataGridColumns()
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.Columns.Clear();
            
            dataGridView.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "AuditId", HeaderText = "ID", Width = 60, DataPropertyName = "AuditId" },
                new DataGridViewTextBoxColumn { Name = "CreatedAt", HeaderText = "Thời gian", Width = 130, DataPropertyName = "CreatedAt" },
                new DataGridViewTextBoxColumn { Name = "Username", HeaderText = "Người dùng", Width = 110, DataPropertyName = "Username" },
                new DataGridViewTextBoxColumn { Name = "ActionDisplay", HeaderText = "Hành động", Width = 140, DataPropertyName = "ActionDisplay" },
                new DataGridViewTextBoxColumn { Name = "EntityTypeDisplay", HeaderText = "Loại", Width = 100, DataPropertyName = "EntityTypeDisplay" },
                new DataGridViewTextBoxColumn { Name = "EntityId", HeaderText = "ID", Width = 60, DataPropertyName = "EntityId" },
                new DataGridViewTextBoxColumn { Name = "After", HeaderText = "Chi tiết", Width = 250, DataPropertyName = "After", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill },
                new DataGridViewTextBoxColumn { Name = "IpAddress", HeaderText = "IP", Width = 100, DataPropertyName = "IpAddress" },
                new DataGridViewButtonColumn { Name = "Detail", HeaderText = "", Width = 50, Text = "👁", UseColumnTextForButtonValue = true }
            });
        }

        private void SetupButtons()
        {
            // Hide CRUD buttons
            var btnAdd = this.Controls.Find("btnAdd", true).FirstOrDefault();
            var btnEdit = this.Controls.Find("btnEdit", true).FirstOrDefault();
            var btnDelete = this.Controls.Find("btnDelete", true).FirstOrDefault();
            
            if (btnAdd != null) btnAdd.Visible = false;
            if (btnEdit != null) btnEdit.Visible = false;
            if (btnDelete != null) btnDelete.Visible = false;
            
            // Update Refresh button
            var btnRefresh = this.Controls.Find("btnRefresh", true).FirstOrDefault() as Button;
            if (btnRefresh != null)
            {
                btnRefresh.Text = "🔄 Làm mới";
                btnRefresh.Width = 110;
                btnRefresh.Location = new Point(20, 12);
            }
            
            // Add Export button
            var buttonPanel = btnRefresh?.Parent as Panel;
            if (buttonPanel != null)
            {
                var btnExport = new Button
                {
                    Text = "📤 Xuất file",
                    Size = new Size(110, 35),
                    Location = new Point(140, 12),
                    BackColor = Color.FromArgb(40, 167, 69),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Name = "btnExport"
                };
                btnExport.FlatAppearance.BorderSize = 0;
                btnExport.Click += async (s, e) => await ExportLogsAsync();
                buttonPanel.Controls.Add(btnExport);
            }
        }

        private void AddStatsPanel()
        {
            _statsFlowPanel = new FlowLayoutPanel
            {
                Height = 95,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20, 8, 20, 8),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };
            
            // Add after filter panel
            var fp = this.Controls.Find("filterPanel", true).FirstOrDefault();
            if (fp != null)
            {
                int index = this.Controls.GetChildIndex(fp);
                this.Controls.Add(_statsFlowPanel);
                this.Controls.SetChildIndex(_statsFlowPanel, index);
            }
            else
            {
                this.Controls.Add(_statsFlowPanel);
            }
        }

        private void SetupCustomFilters()
        {
            // Start Date with Vietnamese format
            _dtpStartDate = new DateTimePicker
            {
                Name = "dtpStartDate",
                Value = DateTime.Now.AddDays(-7),
                Width = 115,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };
            _dtpStartDate.ValueChanged += (s, e) => _searchTimer.Start();
            AddDateFilter("Từ ngày:", _dtpStartDate);

            // End Date with Vietnamese format
            _dtpEndDate = new DateTimePicker
            {
                Name = "dtpEndDate",
                Value = DateTime.Now,
                Width = 115,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };
            _dtpEndDate.ValueChanged += (s, e) => _searchTimer.Start();
            AddDateFilter("Đến:", _dtpEndDate);

            // Action filter ComboBox
            _cboAction = new ComboBox
            {
                Name = "cboAction",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 150
            };
            _cboAction.Items.Add("Tất cả hành động");
            _cboAction.SelectedIndex = 0;
            _cboAction.SelectedIndexChanged += (s, e) => _searchTimer.Start();
            AddCustomFilter("Hành động:", _cboAction);

            // Entity Type filter ComboBox
            _cboEntityType = new ComboBox
            {
                Name = "cboEntityType",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 130
            };
            _cboEntityType.Items.Add("Tất cả loại");
            _cboEntityType.SelectedIndex = 0;
            _cboEntityType.SelectedIndexChanged += (s, e) => _searchTimer.Start();
            AddCustomFilter("Loại:", _cboEntityType);
        }

        private void SetupSearchBox()
        {
            if (searchBox != null)
            {
                TextBoxHelper.SetPlaceholder(searchBox, "Tìm người dùng, hành động...", true);
                searchBox.Width = 200;
                searchBox.TextChanged += (s, e) => _searchTimer.Start();
            }
        }

        private void SetupPaginationPanel()
        {
            var paginationPanel = new Panel
            {
                Height = 45,
                Dock = DockStyle.Bottom,
                BackColor = Color.White,
                Padding = new Padding(20, 8, 20, 8)
            };

            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            _lblPageInfo = new Label
            {
                Text = "Trang 1 / 1 (0 bản ghi)",
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                Margin = new Padding(0, 8, 20, 0)
            };

            _btnPrevPage = new Button
            {
                Text = "◀ Trước",
                Size = new Size(75, 28),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Enabled = false,
                Margin = new Padding(0, 2, 5, 0)
            };
            _btnPrevPage.FlatAppearance.BorderSize = 0;
            _btnPrevPage.Click += async (s, e) => await GoToPreviousPageAsync();

            _btnNextPage = new Button
            {
                Text = "Sau ▶",
                Size = new Size(75, 28),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Enabled = false,
                Margin = new Padding(0, 2, 20, 0)
            };
            _btnNextPage.FlatAppearance.BorderSize = 0;
            _btnNextPage.Click += async (s, e) => await GoToNextPageAsync();

            var lblPageSize = new Label
            {
                Text = "Hiển thị:",
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                Margin = new Padding(0, 8, 5, 0)
            };

            _cboPageSize = new ComboBox
            {
                Width = 60,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                Margin = new Padding(0, 2, 0, 0)
            };
            _cboPageSize.Items.AddRange(new object[] { "25", "50", "100", "200" });
            _cboPageSize.SelectedItem = "50";
            _cboPageSize.SelectedIndexChanged += async (s, e) => await ChangePageSizeAsync();

            flowPanel.Controls.AddRange(new Control[] { _lblPageInfo, _btnPrevPage, _btnNextPage, lblPageSize, _cboPageSize });
            paginationPanel.Controls.Add(flowPanel);
            this.Controls.Add(paginationPanel);
        }

        protected override void OnRefreshButtonClick(object sender, EventArgs e)
        {
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                await LoadFilterOptionsAsync();
                await LoadStatisticsAsync();
                await LoadLogsAsync();
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), $"Lỗi tải dữ liệu: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task LoadFilterOptionsAsync()
        {
            try
            {
                var actions = await _auditController.GetDistinctActionsAsync();
                var entityTypes = await _auditController.GetDistinctEntityTypesAsync();

                _cboAction.Items.Clear();
                _cboAction.Items.Add("Tất cả hành động");
                foreach (var action in actions)
                {
                    _cboAction.Items.Add(new ComboBoxItem(action, AuditActions.GetDisplayName(action)));
                }
                _cboAction.SelectedIndex = 0;

                _cboEntityType.Items.Clear();
                _cboEntityType.Items.Add("Tất cả loại");
                foreach (var entityType in entityTypes)
                {
                    _cboEntityType.Items.Add(new ComboBoxItem(entityType, AuditEntityTypes.GetDisplayName(entityType)));
                }
                _cboEntityType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading filter options: {ex.Message}");
            }
        }

        private async Task LoadStatisticsAsync()
        {
            try
            {
                _statistics = await _auditController.GetStatisticsAsync(_dtpStartDate.Value, _dtpEndDate.Value);
                UpdateStatsPanel();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading statistics: {ex.Message}");
            }
        }

        private void UpdateStatsPanel()
        {
            if (_statistics == null || _statsFlowPanel == null) return;

            _statsFlowPanel.Controls.Clear();

            var cards = new[]
            {
                ("📊 Tổng Log", _statistics.TotalLogs.ToString("N0"), Color.FromArgb(59, 130, 246)),
                ("📅 Hôm nay", _statistics.LogsToday.ToString("N0"), Color.FromArgb(16, 185, 129)),
                ("📆 Tuần này", _statistics.LogsThisWeek.ToString("N0"), Color.FromArgb(245, 158, 11)),
                ("📈 Tháng này", _statistics.LogsThisMonth.ToString("N0"), Color.FromArgb(139, 92, 246))
            };

            // Calculate initial card width
            int availableWidth = _statsFlowPanel.ClientSize.Width - _statsFlowPanel.Padding.Horizontal - 20;
            int cardWidth = Math.Max(160, (availableWidth - (cards.Length - 1) * 15) / cards.Length);

            foreach (var (title, value, color) in cards)
            {
                var card = CreateStatsCard(title, value, color, cardWidth);
                _statsFlowPanel.Controls.Add(card);
            }
        }

        private Panel CreateStatsCard(string title, string value, Color color, int width)
        {
            var card = new Panel
            {
                Width = width,
                Height = 75,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 15, 0)
            };
            
            card.Paint += (s, e) =>
            {
                using var pen = new Pen(color, 2);
                e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Location = new Point(12, 10),
                AutoSize = true
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = color,
                Location = new Point(12, 35),
                AutoSize = true
            };

            card.Controls.AddRange(new Control[] { lblTitle, lblValue });
            return card;
        }

        private async Task LoadLogsAsync()
        {
            try
            {
                _currentResult = await _auditController.GetLogsAsync(_currentFilter);
                _allLogs = _currentResult?.Items ?? new List<AuditLogViewModel>();
                
                DisplayLogs(_allLogs);
                UpdatePaginationInfo();
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), $"Lỗi tải log: {ex.Message}");
            }
        }

        private void DisplayLogs(List<AuditLogViewModel> logs)
        {
            dataGridView.DataSource = null;
            dataGridView.DataSource = new BindingSource { DataSource = logs };
        }

        private void UpdatePaginationInfo()
        {
            if (_currentResult == null) return;

            _lblPageInfo.Text = $"Trang {_currentResult.PageNumber} / {Math.Max(1, _currentResult.TotalPages)} ({_currentResult.TotalCount:N0} bản ghi)";
            _btnPrevPage.Enabled = _currentResult.HasPreviousPage;
            _btnNextPage.Enabled = _currentResult.HasNextPage;
        }

        private async Task SearchAsync()
        {
            _currentFilter.StartDate = _dtpStartDate.Value.Date;
            _currentFilter.EndDate = _dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1);
            _currentFilter.SearchKeyword = string.IsNullOrWhiteSpace(searchBox?.Text) ? null : searchBox.Text.Trim();
            _currentFilter.PageNumber = 1;

            if (_cboAction.SelectedIndex > 0 && _cboAction.SelectedItem is ComboBoxItem actionItem)
            {
                _currentFilter.Action = actionItem.Value;
            }
            else
            {
                _currentFilter.Action = null;
            }

            if (_cboEntityType.SelectedIndex > 0 && _cboEntityType.SelectedItem is ComboBoxItem entityItem)
            {
                _currentFilter.EntityType = entityItem.Value;
            }
            else
            {
                _currentFilter.EntityType = null;
            }

            await LoadLogsAsync();
            await LoadStatisticsAsync();
        }

        private async Task ExportLogsAsync()
        {
            try
            {
                using var saveDialog = new SaveFileDialog
                {
                    Filter = "CSV Files|*.csv|JSON Files|*.json",
                    Title = "Xuất lịch sử hoạt động",
                    FileName = $"AuditLogs_{DateTime.Now:yyyyMMdd_HHmmss}"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var format = saveDialog.FileName.EndsWith(".json") ? "json" : "csv";
                    var data = await _auditController.ExportLogsAsync(_currentFilter, format);
                    
                    System.IO.File.WriteAllBytes(saveDialog.FileName, data);
                    ToastHelper.Show(this.FindForm(), "✅ Xuất file thành công!");

                    // Log action
                    await AuditHelper.LogDataExportAsync("AuditLogs", _currentResult?.TotalCount ?? 0);
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(this.FindForm(), $"Lỗi xuất file: {ex.Message}");
            }
        }

        private async Task GoToPreviousPageAsync()
        {
            if (_currentResult?.HasPreviousPage == true)
            {
                _currentFilter.PageNumber--;
                await LoadLogsAsync();
            }
        }

        private async Task GoToNextPageAsync()
        {
            if (_currentResult?.HasNextPage == true)
            {
                _currentFilter.PageNumber++;
                await LoadLogsAsync();
            }
        }

        private async Task ChangePageSizeAsync()
        {
            if (int.TryParse(_cboPageSize.SelectedItem?.ToString(), out int pageSize))
            {
                _currentFilter.PageSize = pageSize;
                _currentFilter.PageNumber = 1;
                await LoadLogsAsync();
            }
        }

        private void DgvLogs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _allLogs == null) return;

            if (dataGridView.Columns[e.ColumnIndex].Name == "Detail")
            {
                if (e.RowIndex < _allLogs.Count)
                {
                    var log = _allLogs[e.RowIndex];
                    if (log != null)
                    {
                        ShowLogDetail(log);
                    }
                }
            }
        }

        private void DgvLogs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || _allLogs == null || e.RowIndex >= _allLogs.Count) return;

            if (dataGridView.Columns[e.ColumnIndex].Name == "CreatedAt" && e.Value is DateTime dt)
            {
                e.Value = dt.ToString("dd/MM/yy HH:mm");
                e.FormattingApplied = true;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "After" && e.Value is string text)
            {
                if (text.Length > 60)
                {
                    e.Value = text.Substring(0, 57) + "...";
                    e.FormattingApplied = true;
                }
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "ActionDisplay")
            {
                var log = _allLogs[e.RowIndex];
                if (log != null)
                {
                    var action = log.Action.ToUpper();
                    if (action.Contains("DELETE") || action.Contains("REJECT") || action.Contains("FAILED"))
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    else if (action.Contains("CREATE") || action.Contains("APPROVE") || action.Contains("SUCCESS") || action.Contains("LOGIN"))
                        e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129);
                    else if (action.Contains("UPDATE") || action.Contains("CHANGE"))
                        e.CellStyle.ForeColor = Color.FromArgb(245, 158, 11);
                }
            }
        }

        private void ShowLogDetail(AuditLogViewModel log)
        {
            var detailForm = new Form
            {
                Text = $"Chi tiết Log #{log.AuditId}",
                Size = new Size(600, 500),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White,
                FormBorderStyle = FormBorderStyle.Sizable,
                MinimumSize = new Size(500, 400),
                MaximizeBox = true,
                MinimizeBox = false
            };

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true
            };

            int y = 10;

            void AddField(string label, string value, bool multiline = false)
            {
                var lblLabel = new Label
                {
                    Text = label,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.Gray,
                    Location = new Point(10, y),
                    AutoSize = true
                };
                panel.Controls.Add(lblLabel);
                y += 22;

                if (multiline)
                {
                    var txtValue = new TextBox
                    {
                        Text = FormatJson(value),
                        Font = new Font("Consolas", 9),
                        Location = new Point(10, y),
                        Size = new Size(540, 100),
                        Multiline = true,
                        ReadOnly = true,
                        ScrollBars = ScrollBars.Both,
                        BackColor = Color.FromArgb(248, 249, 250)
                    };
                    panel.Controls.Add(txtValue);
                    y += 110;
                }
                else
                {
                    var lblValue = new Label
                    {
                        Text = value ?? "-",
                        Font = new Font("Segoe UI", 10),
                        ForeColor = Color.Black,
                        Location = new Point(10, y),
                        AutoSize = true,
                        MaximumSize = new Size(540, 0)
                    };
                    panel.Controls.Add(lblValue);
                    y += lblValue.Height + 12;
                }
            }

            AddField("ID:", log.AuditId.ToString());
            AddField("Thời gian:", log.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"));
            AddField("Người dùng:", $"{log.FullName} ({log.Username})");
            AddField("Hành động:", log.ActionDisplay);
            AddField("Loại đối tượng:", log.EntityTypeDisplay);
            AddField("ID Đối tượng:", log.EntityId?.ToString() ?? "-");
            AddField("Địa chỉ IP:", log.IpAddress);
            
            if (!string.IsNullOrEmpty(log.Before))
                AddField("Dữ liệu trước:", log.Before, true);
            
            if (!string.IsNullOrEmpty(log.After))
                AddField("Chi tiết / Dữ liệu sau:", log.After, true);

            var btnClose = new Button
            {
                Text = "Đóng",
                Size = new Size(100, 35),
                Location = new Point(250, y + 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(52, 144, 220),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => detailForm.Close();
            panel.Controls.Add(btnClose);

            detailForm.Controls.Add(panel);
            detailForm.ShowDialog();
        }

        private string FormatJson(string json)
        {
            if (string.IsNullOrEmpty(json)) return json;

            try
            {
                var doc = JsonDocument.Parse(json);
                return JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true });
            }
            catch
            {
                return json;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _searchTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        private class ComboBoxItem
        {
            public string Value { get; }
            public string Display { get; }

            public ComboBoxItem(string value, string display)
            {
                Value = value;
                Display = display;
            }

            public override string ToString() => Display;
        }
    }
}
