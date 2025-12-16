using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
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

        public DiscountManagementControl() : base()
        {
            InitializeComponent();
        }

        private async void DiscountManagementControl_Load(object sender, EventArgs e)
        {
            dataGridView = CreateModernDataGridView();
            dataGridView.CellFormatting += DataGridView_CellFormatting;
            
            SetupDataGridColumns();
            SetupLayout("Quản lý mã giảm giá", dataGridView);
            SetupCustomFilters();
            SetupSearchFunctionality();
            WireCrudEvents();

            await LoadDataAsync();
        }

        private void SetupDataGridColumns()
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.Columns.Clear();

            dataGridView.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "DiscountId", HeaderText = "ID", Width = 50, DataPropertyName = "DiscountId" },
                new DataGridViewTextBoxColumn { Name = "Code", HeaderText = "Mã", Width = 100, DataPropertyName = "Code" },
                new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "Tên", Width = 180, DataPropertyName = "Name" },
                new DataGridViewTextBoxColumn { Name = "DisplayValue", HeaderText = "Giảm", Width = 80, DataPropertyName = "DisplayValue" },
                new DataGridViewTextBoxColumn { Name = "TypeDisplay", HeaderText = "Loại", Width = 100, DataPropertyName = "TypeDisplay" },
                new DataGridViewTextBoxColumn { Name = "MinOrderAmount", HeaderText = "Đơn tối thiểu", Width = 100, DataPropertyName = "MinOrderAmount" },
                new DataGridViewTextBoxColumn { Name = "RemainingUsage", HeaderText = "Còn lại", Width = 90, DataPropertyName = "RemainingUsage" },
                new DataGridViewTextBoxColumn { Name = "StartDate", HeaderText = "Bắt đầu", Width = 90, DataPropertyName = "StartDate" },
                new DataGridViewTextBoxColumn { Name = "EndDate", HeaderText = "Kết thúc", Width = 90, DataPropertyName = "EndDate" },
                new DataGridViewTextBoxColumn { Name = "StatusDisplay", HeaderText = "Trạng thái", Width = 90, DataPropertyName = "StatusDisplay" }
            });
        }

        private void SetupCustomFilters()
        {
            // Status filter
            _cboStatus = new ComboBox
            {
                Name = "cboStatus",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 120
            };
            _cboStatus.Items.AddRange(new object[] { "Tất cả", "Hoạt động", "Tạm dừng", "Hết hạn" });
            _cboStatus.SelectedIndex = 0;
            _cboStatus.SelectedIndexChanged += (s, e) => FilterData();
            AddCustomFilter("Trạng thái:", _cboStatus);

            // Type filter
            _cboType = new ComboBox
            {
                Name = "cboType",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 130
            };
            _cboType.Items.AddRange(new object[] { "Tất cả", "Phần trăm", "Số tiền cố định" });
            _cboType.SelectedIndex = 0;
            _cboType.SelectedIndexChanged += (s, e) => FilterData();
            AddCustomFilter("Loại:", _cboType);
        }

        private void SetupSearchFunctionality()
        {
            if (searchBox != null)
            {
                TextBoxHelper.SetPlaceholder(searchBox, "Tìm theo mã, tên...", true);
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

            // Filter by status
            if (_cboStatus?.SelectedIndex > 0)
            {
                var status = _cboStatus.SelectedIndex switch
                {
                    1 => "Active",
                    2 => "Inactive",
                    3 => "Expired",
                    _ => null
                };
                if (status != null)
                    query = query.Where(d => d.Status == status);
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

            // Color status
            if (dataGridView.Columns[e.ColumnIndex].Name == "StatusDisplay")
            {
                switch (discount.Status)
                {
                    case "Active":
                        e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129);
                        break;
                    case "Inactive":
                        e.CellStyle.ForeColor = Color.FromArgb(245, 158, 11);
                        break;
                    case "Expired":
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                        break;
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
                ToastHelper.Show(this.FindForm(), "Vui lòng chọn mã giảm giá cần sửa");
            }
        }

        protected override async void OnDeleteButtonClick(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow?.DataBoundItem is DiscountViewModel discount)
            {
                var result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa mã giảm giá '{discount.Code}'?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var success = await DiscountService.DeleteDiscountAsync(discount.DiscountId);
                    if (success)
                    {
                        ToastHelper.Show(this.FindForm(), "Đã xóa mã giảm giá!");
                        await LoadDataAsync();
                    }
                    else
                    {
                        ToastHelper.Show(this.FindForm(), "Không thể xóa mã giảm giá!");
                    }
                }
            }
            else
            {
                ToastHelper.Show(this.FindForm(), "Vui lòng chọn mã giảm giá cần xóa");
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

        public DiscountEditForm(DiscountViewModel? existing = null)
        {
            _existing = existing;
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = _existing == null ? "Tạo mã giảm giá" : "Sửa mã giảm giá";
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
            AddLabel(panel, "Mã giảm giá *", y);
            txtCode = AddTextBox(panel, y + 25);
            txtCode.CharacterCasing = CharacterCasing.Upper;
            txtCode.Enabled = _existing == null;
            y += 70;

            // Name
            AddLabel(panel, "Tên *", y);
            txtName = AddTextBox(panel, y + 25);
            y += 70;

            // Description
            AddLabel(panel, "Mô tả", y);
            txtDescription = AddTextBox(panel, y + 25);
            y += 70;

            // Type and Value
            AddLabel(panel, "Loại giảm giá", y);
            cboType = new ComboBox
            {
                Location = new Point(20, y + 25),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboType.Items.AddRange(new object[] { "Phần trăm (%)", "Số tiền cố định (VNĐ)" });
            cboType.SelectedIndex = 0;
            panel.Controls.Add(cboType);

            var lblValue = new Label
            {
                Text = "Giá trị *",
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
            AddLabel(panel, "Đơn hàng tối thiểu (VNĐ)", y);
            nudMinOrder = AddNumericUpDown(panel, y + 25);
            y += 70;

            // Max Discount (for percentage)
            AddLabel(panel, "Giảm tối đa (VNĐ) - cho loại %", y);
            nudMaxDiscount = AddNumericUpDown(panel, y + 25);
            y += 70;

            // Max Usage
            AddLabel(panel, "Tổng lượt sử dụng (0 = không giới hạn)", y);
            nudMaxUsage = AddNumericUpDown(panel, y + 25, 0, 999999);
            y += 70;

            // Max Per User
            AddLabel(panel, "Lượt/người (0 = không giới hạn)", y);
            nudMaxPerUser = AddNumericUpDown(panel, y + 25, 0, 100);
            y += 70;

            // Date Range
            AddLabel(panel, "Ngày bắt đầu", y);
            dtpStart = new DateTimePicker
            {
                Location = new Point(20, y + 25),
                Size = new Size(200, 30),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm"
            };
            panel.Controls.Add(dtpStart);

            var lblEnd = new Label
            {
                Text = "Ngày kết thúc",
                Location = new Point(230, y),
                AutoSize = true
            };
            panel.Controls.Add(lblEnd);

            dtpEnd = new DateTimePicker
            {
                Location = new Point(230, y + 25),
                Size = new Size(200, 30),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm",
                Value = DateTime.Now.AddMonths(1)
            };
            panel.Controls.Add(dtpEnd);
            y += 70;

            // Status
            AddLabel(panel, "Trạng thái", y);
            cboStatus = new ComboBox
            {
                Location = new Point(20, y + 25),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboStatus.Items.AddRange(new object[] { "Hoạt động", "Tạm dừng" });
            cboStatus.SelectedIndex = 0;
            panel.Controls.Add(cboStatus);

            // Apply to all
            chkAllCourses = new CheckBox
            {
                Text = "Áp dụng cho tất cả khóa học",
                Location = new Point(230, y + 25),
                Size = new Size(200, 30),
                Checked = true
            };
            panel.Controls.Add(chkAllCourses);
            y += 70;

            // Buttons
            var btnSave = new Button
            {
                Text = "Lưu",
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
                Text = "Hủy",
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
                    IsActive = cboStatus.SelectedIndex == 0,
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
