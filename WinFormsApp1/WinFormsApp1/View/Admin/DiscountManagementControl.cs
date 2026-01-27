using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Service;
using WinFormsApp1.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.View.Admin
{
    public partial class DiscountManagementControl : AdminBaseControl
    {
        private List<DiscountViewModel> _discounts = new();
        private List<DiscountViewModel> _filteredDiscounts = new();
        private ComboBox _cboStatus;
        private ComboBox _cboType;
        private System.Windows.Forms.Timer _statusUpdateTimer;

        public DiscountManagementControl() : base()
        {
            InitializeComponent();
        }

        private async void DiscountManagementControl_Load(object sender, EventArgs e)
        {
            dataGridView = CreateModernDataGridView();
            dataGridView.CellFormatting += DataGridView_CellFormatting;
            
            SetupDataGridColumns();
            SetupLayout(Lang("DiscountManagement"), dataGridView);
            SetupCustomFilters();
            SetupSearchFunctionality();
            WireCrudEvents();
            SetupStatusUpdateTimer();

            await LoadDataAsync();
        }

        /// <summary>
        /// Thiết lập Timer để cập nhật trạng thái theo thời gian thực
        /// </summary>
        private void SetupStatusUpdateTimer()
        {
            _statusUpdateTimer = new System.Windows.Forms.Timer();
            _statusUpdateTimer.Interval = 30000; // Cập nhật mỗi 30 giây
            _statusUpdateTimer.Tick += StatusUpdateTimer_Tick;
            _statusUpdateTimer.Start();
        }

        private void StatusUpdateTimer_Tick(object sender, EventArgs e)
        {
            // Refresh DataGridView để cập nhật trạng thái
            if (dataGridView != null && _filteredDiscounts.Count > 0)
            {
                try
                {
                    // Chỉ refresh hiển thị, không load lại từ database
                    dataGridView.Invalidate();
                    dataGridView.Refresh();
                }
                catch
                {
                    // Ignore errors during refresh
                }
            }
        }

        private void SetupDataGridColumns()
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.Columns.Clear();

            dataGridView.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "DiscountId", HeaderText = "ID", Width = 50, DataPropertyName = "DiscountId" },
                new DataGridViewTextBoxColumn { Name = "Code", HeaderText = Lang("Code"), Width = 100, DataPropertyName = "Code" },
                new DataGridViewTextBoxColumn { Name = "Name", HeaderText = Lang("Name"), Width = 150, DataPropertyName = "Name" },
                new DataGridViewTextBoxColumn { Name = "DisplayValue", HeaderText = Lang("DiscountValue"), Width = 80, DataPropertyName = "DisplayValue" },
                new DataGridViewTextBoxColumn { Name = "TypeDisplay", HeaderText = Lang("DiscountType"), Width = 100, DataPropertyName = "TypeDisplay" },
                new DataGridViewTextBoxColumn { Name = "MinOrderAmount", HeaderText = Lang("MinOrderAmount"), Width = 100, DataPropertyName = "MinOrderAmount" },
                new DataGridViewTextBoxColumn { Name = "RemainingUsage", HeaderText = Lang("RemainingUsage"), Width = 90, DataPropertyName = "RemainingUsage" },
                new DataGridViewTextBoxColumn { Name = "StartDate", HeaderText = Lang("StartDate"), Width = 90, DataPropertyName = "StartDate" },
                new DataGridViewTextBoxColumn { Name = "EndDate", HeaderText = Lang("EndDate"), Width = 90, DataPropertyName = "EndDate" },
                new DataGridViewTextBoxColumn { Name = "StatusDisplay", HeaderText = Lang("Status"), Width = 100, DataPropertyName = "StatusDisplay" },
                new DataGridViewTextBoxColumn { Name = "TimeRemaining", HeaderText = Lang("TimeRemaining"), Width = 80, DataPropertyName = "TimeRemaining" }
            });
        }

        private void SetupCustomFilters()
        {
            // Status filter - cập nhật thêm các trạng thái mới
            _cboStatus = new ComboBox
            {
                Name = "cboStatus",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 120
            };
            _cboStatus.Items.AddRange(new object[] { Lang("All"), Lang("StatusActive"), Lang("StatusInactive"), Lang("StatusExpired"), Lang("StatusExhausted"), Lang("StatusNotStarted") });
            _cboStatus.SelectedIndex = 0;
            _cboStatus.SelectedIndexChanged += (s, e) => FilterData();
            AddCustomFilter(Lang("FilterStatus"), _cboStatus);

            // Type filter
            _cboType = new ComboBox
            {
                Name = "cboType",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 130
            };
            _cboType.Items.AddRange(new object[] { Lang("All"), Lang("TypePercentage"), Lang("TypeFixedAmount") });
            _cboType.SelectedIndex = 0;
            _cboType.SelectedIndexChanged += (s, e) => FilterData();
            AddCustomFilter(Lang("FilterType"), _cboType);
        }

        private void SetupSearchFunctionality()
        {
            if (searchBox != null)
            {
                TextBoxHelper.SetPlaceholder(searchBox, Lang("SearchCodeName"), true);
                searchBox.TextChanged += (s, e) => FilterData();
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                using var context = new LearningPlatformContext();
                var discounts = await context.Discounts
                    .Include(d => d.Creator)
                    .OrderByDescending(d => d.CreatedAt)
                    .ToListAsync();

                _discounts = discounts.Select(d => new DiscountViewModel
                {
                    DiscountId = d.DiscountId,
                    Code = d.Code,
                    Name = d.Name,
                    Description = d.Description,
                    DiscountType = d.DiscountType,
                    DiscountValue = d.DiscountValue,
                    MinOrderAmount = d.MinOrderAmount,
                    MaxDiscountAmount = d.MaxDiscountAmount,
                    UsageLimit = d.UsageLimit,
                    UsageCount = d.UsageCount,
                    UsageLimitPerUser = d.UsageLimitPerUser,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    Status = d.Status,
                    IsActive = d.IsActive,
                    ApplyToAllCourses = d.ApplyToAllCourses,
                    CreatorName = d.Creator?.FullName ?? "N/A",
                    CreatedAt = d.CreatedAt
                }).ToList();

                FilterData();
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

        private void FilterData()
        {
            var query = _discounts.AsEnumerable();

            // Filter by real-time status
            if (_cboStatus?.SelectedIndex > 0)
            {
                var status = _cboStatus.SelectedIndex switch
                {
                    1 => "Active",
                    2 => "Inactive",
                    3 => "Expired",
                    4 => "Exhausted",
                    5 => "Pending",
                    _ => null
                };
                if (status != null)
                    query = query.Where(d => d.RealTimeStatus == status);
            }

            // Filter by type
            if (_cboType?.SelectedIndex > 0)
            {
                var type = _cboType.SelectedIndex == 1 ? "Percentage" : "FixedAmount";
                query = query.Where(d => d.DiscountType == type);
            }

            // Filter by search
            if (!string.IsNullOrWhiteSpace(searchBox?.Text))
            {
                var search = searchBox.Text.ToLower().Trim();
                query = query.Where(d => 
                    d.Code.ToLower().Contains(search) ||
                    d.Name.ToLower().Contains(search) ||
                    (d.Description?.ToLower().Contains(search) ?? false));
            }

            _filteredDiscounts = query.ToList();
            dataGridView.DataSource = new BindingSource { DataSource = _filteredDiscounts };
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || _filteredDiscounts == null || e.RowIndex >= _filteredDiscounts.Count) return;

            var discount = _filteredDiscounts[e.RowIndex];

            // Format dates
            if (dataGridView.Columns[e.ColumnIndex].Name == "StartDate" || 
                dataGridView.Columns[e.ColumnIndex].Name == "EndDate")
            {
                if (e.Value is DateTime dt)
                {
                    e.Value = dt.ToString("dd/MM/yyyy");
                    e.FormattingApplied = true;
                }
            }

            // Format MinOrderAmount
            if (dataGridView.Columns[e.ColumnIndex].Name == "MinOrderAmount")
            {
                if (e.Value is decimal amount && amount > 0)
                {
                    e.Value = $"{amount:N0}đ";
                    e.FormattingApplied = true;
                }
                else
                {
                    e.Value = "-";
                    e.FormattingApplied = true;
                }
            }

            // Color status based on real-time status
            if (dataGridView.Columns[e.ColumnIndex].Name == "StatusDisplay")
            {
                // Cập nhật giá trị hiển thị từ RealTimeStatus
                e.Value = discount.StatusDisplay;
                e.FormattingApplied = true;
                
                switch (discount.RealTimeStatus)
                {
                    case "Active":
                        e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129); // Xanh lá
                        e.CellStyle.Font = new Font(dataGridView.Font, FontStyle.Bold);
                        break;
                    case "Inactive":
                        e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128); // Xám
                        break;
                    case "Expired":
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ
                        break;
                    case "Exhausted":
                        e.CellStyle.ForeColor = Color.FromArgb(245, 158, 11); // Cam
                        break;
                    case "Pending":
                        e.CellStyle.ForeColor = Color.FromArgb(59, 130, 246); // Xanh dương
                        break;
                }
            }

            // Color TimeRemaining column
            if (dataGridView.Columns[e.ColumnIndex].Name == "TimeRemaining")
            {
                e.Value = discount.TimeRemaining;
                e.FormattingApplied = true;
                
                if (discount.RealTimeStatus == "Active")
                {
                    var remaining = discount.EndDate - DateTime.Now;
                    if (remaining.TotalDays <= 1)
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ - sắp hết hạn
                    else if (remaining.TotalDays <= 7)
                        e.CellStyle.ForeColor = Color.FromArgb(245, 158, 11); // Cam - còn ít
                    else
                        e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129); // Xanh - còn nhiều
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Gray;
                }
            }
        }

        protected override void OnAddButtonClick(object sender, EventArgs e)
        {
            ShowDiscountForm(null);
        }

        protected override void OnEditButtonClick(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow?.DataBoundItem is DiscountViewModel discount)
            {
                ShowDiscountForm(discount);
            }
            else
            {
                ToastHelper.Show(this.FindForm(), Lang("PleaseSelectDiscountToEdit"));
            }
        }

        protected override async void OnDeleteButtonClick(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow?.DataBoundItem is DiscountViewModel discount)
            {
                var result = MessageBox.Show(
                    $"{Lang("ConfirmDeleteDiscount")}\n\n'{discount.Code}'",
                    Lang("Confirm"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var success = await DiscountService.DeleteDiscountAsync(discount.DiscountId);
                    if (success)
                    {
                        ToastHelper.Show(this.FindForm(), Lang("DiscountDeleteSuccess"));
                        await LoadDataAsync();
                    }
                    else
                    {
                        ToastHelper.Show(this.FindForm(), Lang("DiscountDeleteFailed"));
                    }
                }
            }
            else
            {
                ToastHelper.Show(this.FindForm(), Lang("PleaseSelectDiscountToDelete"));
            }
        }

        protected override void OnRefreshButtonClick(object sender, EventArgs e)
        {
            _ = LoadDataAsync();
        }

        private void ShowDiscountForm(DiscountViewModel? existing)
        {
            using var form = new DiscountEditForm(existing);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadDataAsync();
            }
        }
    }

    /// <summary>
    /// Form tạo/sửa mã giảm giá
    /// </summary>
    public class DiscountEditForm : Form
    {
        private DiscountViewModel? _existing;
        private TextBox txtCode;
        private TextBox txtName;
        private TextBox txtDescription;
        private ComboBox cboType;
        private NumericUpDown nudValue;
        private NumericUpDown nudMinOrder;
        private NumericUpDown nudMaxDiscount;
        private NumericUpDown nudMaxUsage;
        private NumericUpDown nudMaxPerUser;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private ComboBox cboStatus;
        private CheckBox chkAllCourses;
        
        private static string Lang(string key) => LanguageHelper.GetString(key);

        public DiscountEditForm(DiscountViewModel? existing = null)
        {
            _existing = existing;
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = _existing == null ? Lang("CreateDiscount") : Lang("EditDiscount");
            this.Size = new Size(500, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20)
            };

            int y = 20;

            // Code
            AddLabel(panel, $"{Lang("DiscountCode")} *", y);
            txtCode = AddTextBox(panel, y + 25);
            txtCode.CharacterCasing = CharacterCasing.Upper;
            txtCode.Enabled = _existing == null;
            y += 70;

            // Name
            AddLabel(panel, $"{Lang("Name")} *", y);
            txtName = AddTextBox(panel, y + 25);
            y += 70;

            // Description
            AddLabel(panel, Lang("Description"), y);
            txtDescription = AddTextBox(panel, y + 25);
            y += 70;

            // Type and Value
            AddLabel(panel, Lang("DiscountType"), y);
            cboType = new ComboBox
            {
                Location = new Point(20, y + 25),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboType.Items.AddRange(new object[] { Lang("PercentageType"), Lang("FixedAmountType") });
            cboType.SelectedIndex = 0;
            panel.Controls.Add(cboType);

            var lblValue = new Label
            {
                Text = $"{Lang("Value")} *",
                Location = new Point(230, y),
                AutoSize = true
            };
            panel.Controls.Add(lblValue);

            nudValue = new NumericUpDown
            {
                Location = new Point(230, y + 25),
                Size = new Size(200, 30),
                Maximum = 999999999,
                DecimalPlaces = 0
            };
            panel.Controls.Add(nudValue);
            y += 70;

            // Min Order Amount
            AddLabel(panel, $"{Lang("MinOrderAmount")} (VNĐ)", y);
            nudMinOrder = AddNumericUpDown(panel, y + 25);
            y += 70;

            // Max Discount (for percentage)
            AddLabel(panel, $"{Lang("MaxDiscountAmount")} (VNĐ)", y);
            nudMaxDiscount = AddNumericUpDown(panel, y + 25);
            y += 70;

            // Max Usage
            AddLabel(panel, $"{Lang("UsageLimit")} (0 = ∞)", y);
            nudMaxUsage = AddNumericUpDown(panel, y + 25, 0, 999999);
            y += 70;

            // Max Per User
            AddLabel(panel, $"{Lang("UsageLimitPerUser")} (0 = ∞)", y);
            nudMaxPerUser = AddNumericUpDown(panel, y + 25, 0, 100);
            y += 70;

            // Date Range
            AddLabel(panel, Lang("StartDate"), y);
            dtpStart = new DateTimePicker
            {
                Location = new Point(20, y + 25),
                Size = new Size(200, 30),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = LanguageHelper.DateFormatPattern + " HH:mm"
            };
            panel.Controls.Add(dtpStart);

            var lblEnd = new Label
            {
                Text = Lang("EndDate"),
                Location = new Point(230, y),
                AutoSize = true
            };
            panel.Controls.Add(lblEnd);

            dtpEnd = new DateTimePicker
            {
                Location = new Point(230, y + 25),
                Size = new Size(200, 30),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = LanguageHelper.DateFormatPattern + " HH:mm",
                Value = DateTime.Now.AddMonths(1)
            };
            panel.Controls.Add(dtpEnd);
            y += 70;

            // Status
            AddLabel(panel, Lang("Status"), y);
            cboStatus = new ComboBox
            {
                Location = new Point(20, y + 25),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboStatus.Items.AddRange(new object[] { Lang("StatusActive"), Lang("StatusInactive") });
            cboStatus.SelectedIndex = 0;
            panel.Controls.Add(cboStatus);

            // Apply to all
            chkAllCourses = new CheckBox
            {
                Text = Lang("ApplyToAllCourses"),
                Location = new Point(230, y + 25),
                Size = new Size(200, 30),
                Checked = true
            };
            panel.Controls.Add(chkAllCourses);
            y += 70;

            // Buttons
            var btnSave = new Button
            {
                Text = Lang("Save"),
                Location = new Point(120, y + 20),
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += async (s, e) => await SaveAsync();
            panel.Controls.Add(btnSave);

            var btnCancel = new Button
            {
                Text = Lang("Cancel"),
                Location = new Point(230, y + 20),
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            panel.Controls.Add(btnCancel);

            this.Controls.Add(panel);

            // Load existing data
            if (_existing != null)
            {
                txtCode.Text = _existing.Code;
                txtName.Text = _existing.Name;
                txtDescription.Text = _existing.Description;
                cboType.SelectedIndex = _existing.DiscountType == "Percentage" ? 0 : 1;
                nudValue.Value = _existing.DiscountValue;
                nudMinOrder.Value = _existing.MinOrderAmount ?? 0;
                nudMaxDiscount.Value = _existing.MaxDiscountAmount ?? 0;
                nudMaxUsage.Value = _existing.UsageLimit ?? 0;
                nudMaxPerUser.Value = _existing.UsageLimitPerUser ?? 0;
                dtpStart.Value = _existing.StartDate;
                dtpEnd.Value = _existing.EndDate;
                cboStatus.SelectedIndex = _existing.Status == "Active" ? 0 : 1;
                chkAllCourses.Checked = _existing.ApplyToAllCourses;
            }
        }

        private void AddLabel(Panel panel, string text, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Location = new Point(20, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            panel.Controls.Add(lbl);
        }

        private TextBox AddTextBox(Panel panel, int y)
        {
            var txt = new TextBox
            {
                Location = new Point(20, y),
                Size = new Size(420, 30),
                Font = new Font("Segoe UI", 10)
            };
            panel.Controls.Add(txt);
            return txt;
        }

        private NumericUpDown AddNumericUpDown(Panel panel, int y, decimal min = 0, decimal max = 999999999)
        {
            var nud = new NumericUpDown
            {
                Location = new Point(20, y),
                Size = new Size(200, 30),
                Minimum = min,
                Maximum = max,
                DecimalPlaces = 0
            };
            panel.Controls.Add(nud);
            return nud;
        }

        private async Task SaveAsync()
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã giảm giá", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudValue.Value <= 0)
            {
                MessageBox.Show("Giá trị giảm phải lớn hơn 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpEnd.Value <= dtpStart.Value)
            {
                MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var discount = new Discount
                {
                    DiscountId = _existing?.DiscountId ?? 0,
                    Code = txtCode.Text.Trim().ToUpper(),
                    Name = txtName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    DiscountType = cboType.SelectedIndex == 0 ? "Percentage" : "FixedAmount",
                    DiscountValue = nudValue.Value,
                    MinOrderAmount = nudMinOrder.Value > 0 ? nudMinOrder.Value : null,
                    MaxDiscountAmount = nudMaxDiscount.Value > 0 ? nudMaxDiscount.Value : null,
                    UsageLimit = nudMaxUsage.Value > 0 ? (int)nudMaxUsage.Value : null,
                    UsageLimitPerUser = nudMaxPerUser.Value > 0 ? (int)nudMaxPerUser.Value : null,
                    StartDate = dtpStart.Value,
                    EndDate = dtpEnd.Value,
                    Status = cboStatus.SelectedIndex == 0 ? "Active" : "Inactive",
                    //IsActive = cboStatus.SelectedIndex == 0,
                    ApplyToAllCourses = chkAllCourses.Checked,
                    CreatedBy = AuthHelper.CurrentUser?.UserId ?? 1
                };

                if (_existing == null)
                {
                    await DiscountService.CreateDiscountAsync(discount);
                    ToastHelper.Show(this, "Tạo mã giảm giá thành công!");
                }
                else
                {
                    await DiscountService.UpdateDiscountAsync(discount);
                    ToastHelper.Show(this, "Cập nhật mã giảm giá thành công!");
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
