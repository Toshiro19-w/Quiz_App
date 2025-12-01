# Admin Filter Pattern Guide

## Tổng quan

Pattern mới này tách biệt logic tạo custom filters khỏi base class, giúp code dễ maintain và tránh được các vấn đề về initialization order.

## Pattern cũ (Deprecated)

```csharp
// ❌ KHÔNG SỬ DỤNG CÁCH NÀY NỮA
protected override Panel CreateFilterPanel()
{
    var panel = base.CreateFilterPanel();
    
    // Add filters here...
    var combo = new ComboBox { ... };
    AddFilterControl(combo);
    
    // Add labels...
    var label = new Label { ... };
    panel.Controls.Add(label);
    
    return panel;
}
```

**Vấn đề:**
- Phụ thuộc vào thứ tự initialization
- Labels và ComboBoxes được thêm riêng rẽ, dễ nhầm lẫn
- Override method khiến code phức tạp

## Pattern mới (Recommended)

### 1. Tạo method `SetupCustomFilters()`

```csharp
/// <summary>
/// Setup custom filters cho control
/// </summary>
private void SetupCustomFilters()
{
    // Tạo ComboBox với config đầy đủ
    var filterCombo = new ComboBox
    {
        Name = "cboMyFilter",
        DropDownStyle = ComboBoxStyle.DropDownList
    };
    filterCombo.Items.AddRange(new object[] { "Option 1", "Option 2", "Option 3" });
    filterCombo.SelectedIndex = 0;
    filterCombo.SelectedIndexChanged += (s, e) => OnFilterChanged();

    // Sử dụng helper method để add filter
    AddCustomFilter("Label Text:", filterCombo);
}
```

### 2. Gọi trong `_Load` event handler

```csharp
private async void MyControl_Load(object sender, EventArgs e)
{
    // ... setup layout first ...
    SetupLayout("Title", dataGridView);
    
    // ✅ Add custom filters AFTER layout is setup
    SetupCustomFilters();
    
    // ... wire events and load data ...
}
```

### 3. Sử dụng trong filter logic

```csharp
private void OnFilterChanged()
{
    // Tìm ComboBox bằng Name
    var filterCombo = this.Controls.Find("cboMyFilter", true).FirstOrDefault() as ComboBox;
    var selectedValue = filterCombo?.SelectedItem?.ToString() ?? "";
    
    // Apply filter logic...
}
```

## Helper Methods

### `AddCustomFilter(string labelText, ComboBox comboBox)`

Thêm một filter với label và ComboBox.

**Parameters:**
- `labelText`: Text của label (VD: "Danh mục:")
- `comboBox`: ComboBox đã được config sẵn

**Example:**
```csharp
var categoryCombo = new ComboBox
{
    Name = "cboCategory",
    DropDownStyle = ComboBoxStyle.DropDownList
};
categoryCombo.Items.AddRange(new object[] { "All", "Category 1", "Category 2" });
categoryCombo.SelectedIndex = 0;

AddCustomFilter("Danh mục:", categoryCombo);
```

### `AddCustomFilters(params (string label, ComboBox combo)[] filters)`

Thêm nhiều filters cùng lúc.

**Example:**
```csharp
AddCustomFilters(
    ("Danh mục:", categoryCombo),
    ("Trạng thái:", statusCombo),
    ("Loại:", typeCombo)
);
```

## Ví dụ thực tế

### CourseManagementControl

```csharp
private void SetupCustomFilters()
{
    // Category Filter
    var categoryCombo = new ComboBox
    {
        Name = "cboCategory",
        DropDownStyle = ComboBoxStyle.DropDownList
    };
    categoryCombo.Items.Add("Tất cả danh mục");
    categoryCombo.SelectedIndex = 0;
    categoryCombo.SelectedIndexChanged += (s, e) => FilterCoursesLocally();

    // Status Filter
    var statusCombo = new ComboBox
    {
        Name = "cboStatus",
        DropDownStyle = ComboBoxStyle.DropDownList
    };
    statusCombo.Items.AddRange(new object[] { "Tất cả", "Đã xuất bản", "Nháp" });
    statusCombo.SelectedIndex = 0;
    statusCombo.SelectedIndexChanged += (s, e) => FilterCoursesLocally();

    // Add both filters at once
    AddCustomFilters(
        ("Danh mục:", categoryCombo),
        ("Trạng thái:", statusCombo)
    );
}

private void FilterCoursesLocally()
{
    var categoryCombo = this.Controls.Find("cboCategory", true).FirstOrDefault() as ComboBox;
    var statusCombo = this.Controls.Find("cboStatus", true).FirstOrDefault() as ComboBox;

    string categoryFilter = categoryCombo?.SelectedIndex > 0 ? categoryCombo.Text : "";
    string statusFilter = statusCombo?.SelectedIndex > 0 ? statusCombo.Text : "";
    
    // Apply filters...
}
```

### UserManagementControl

```csharp
private void SetupCustomFilters()
{
    // Role Filter
    var roleCombo = new ComboBox
    {
        Name = "cboRoleFilter", // ⚠️ Tránh trùng tên với controls khác
        DropDownStyle = ComboBoxStyle.DropDownList
    };
    roleCombo.Items.AddRange(new object[] { "Tất cả vai trò", "Admin", "User" });
    roleCombo.SelectedIndex = 0;
    roleCombo.SelectedIndexChanged += (s, e) => FilterUsersLocally();

    // Status Filter
    var statusCombo = new ComboBox
    {
        Name = "cboStatusFilter",
        DropDownStyle = ComboBoxStyle.DropDownList
    };
    statusCombo.Items.AddRange(new object[] { "Tất cả trạng thái", "Hoạt động", "Không hoạt động" });
    statusCombo.SelectedIndex = 0;
    statusCombo.SelectedIndexChanged += (s, e) => FilterUsersLocally();

    AddCustomFilters(
        ("Vai trò:", roleCombo),
        ("Trạng thái:", statusCombo)
    );
}
```

## Best Practices

### ✅ DO

1. **Đặt tên ComboBox rõ ràng và unique**
   ```csharp
   Name = "cboCategory"  // Good
   Name = "cboRoleFilter" // Good - tránh conflict với cboRole trong form
   ```

2. **Config đầy đủ trước khi add**
   ```csharp
   var combo = new ComboBox
   {
       Name = "cboStatus",
       DropDownStyle = ComboBoxStyle.DropDownList // Always set this
   };
   combo.Items.AddRange(...);
   combo.SelectedIndex = 0; // Set default
   combo.SelectedIndexChanged += ...; // Wire event
   ```

3. **Gọi `SetupCustomFilters()` SAU `SetupLayout()`**
   ```csharp
   SetupLayout("Title", dataGridView);
   SetupCustomFilters(); // ✅ After layout
   ```

### ❌ DON'T

1. **Không override `CreateFilterPanel()` nữa**
   ```csharp
   // ❌ Deprecated pattern
   protected override Panel CreateFilterPanel() { ... }
   ```

2. **Không add filters trước khi layout ready**
   ```csharp
   // ❌ Wrong order
   SetupCustomFilters();
   SetupLayout("Title", dataGridView); // Too late!
   ```

3. **Không dùng tên trùng**
   ```csharp
   Name = "cboStatus" // ❌ Might conflict
   Name = "cboRole"   // ❌ Might conflict with form controls
   ```

## Migration Guide

Nếu bạn có code cũ sử dụng `CreateFilterPanel()` override:

### Bước 1: Xóa override method

```csharp
// ❌ Delete this
protected override Panel CreateFilterPanel()
{
    var panel = base.CreateFilterPanel();
    // ... filter code ...
    return panel;
}
```

### Bước 2: Tạo `SetupCustomFilters()`

```csharp
// ✅ Create this instead
private void SetupCustomFilters()
{
    // Move filter creation code here
    AddCustomFilters(...);
}
```

### Bước 3: Gọi trong `_Load`

```csharp
private async void MyControl_Load(object sender, EventArgs e)
{
    SetupLayout("Title", dataGridView);
    SetupCustomFilters(); // ✅ Add this line
    // ... rest of code ...
}
```

### Bước 4: Update filter logic

```csharp
// Update ComboBox names if needed
var combo = this.Controls.Find("cboNewName", true).FirstOrDefault() as ComboBox;
```

## Troubleshooting

### Filters không hiển thị?

1. **Kiểm tra thứ tự gọi**
   ```csharp
   // Must be in this order:
   SetupLayout(...);        // 1. Layout first
   SetupCustomFilters();    // 2. Then filters
   ```

2. **Check debug output**
   ```csharp
   System.Diagnostics.Debug.WriteLine($"Added filter: {labelText}");
   ```

3. **Verify filterPanel exists**
   ```csharp
   var filterPanel = this.Controls.Find("filterPanel", true).FirstOrDefault();
   if (filterPanel == null)
   {
       System.Diagnostics.Debug.WriteLine("filterPanel not found!");
   }
   ```

### ComboBox không tìm thấy?

```csharp
// Use recursive search
var combo = this.Controls.Find("cboMyFilter", true).FirstOrDefault() as ComboBox;
if (combo == null)
{
    System.Diagnostics.Debug.WriteLine("ComboBox not found!");
}
```

## Summary

- ✅ **Sử dụng** `SetupCustomFilters()` method
- ✅ **Gọi SAU** `SetupLayout()`
- ✅ **Sử dụng** `AddCustomFilter()` và `AddCustomFilters()`
- ❌ **Không override** `CreateFilterPanel()` nữa
- ❌ **Không thêm** filters trước khi layout ready

Pattern này giúp code:
- 📦 **Modular**: Tách biệt concerns
- 🔧 **Maintainable**: Dễ bảo trì
- 🐛 **Bug-free**: Tránh initialization issues
- 📖 **Readable**: Code rõ ràng hơn
