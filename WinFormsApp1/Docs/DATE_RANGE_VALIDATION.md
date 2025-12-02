# Date Range Validation for OverviewDashboard

## Tính năng mới: Validation ngày tháng

### Yêu cầu
Khi admin chọn "Tùy chọn" để nhập ngày thủ công, cần đảm bảo:
- ❌ Ngày bắt đầu **KHÔNG được** lớn hơn hoặc bằng ngày kết thúc
- ✅ Ngày bắt đầu **PHẢI** nhỏ hơn ngày kết thúc

## Implementation

### 1. Wire up date validation events

```csharp
private void OverviewDashboard_Load(object sender, EventArgs e)
{
    // ... existing code ...
    
    // ✅ NEW: Add date validation events
    startDatePicker.ValueChanged += DatePicker_ValueChanged;
    endDatePicker.ValueChanged += DatePicker_ValueChanged;
    
    // ... existing code ...
}
```

### 2. Date picker change handler

```csharp
/// <summary>
/// Validate date range when date pickers change
/// </summary>
private void DatePicker_ValueChanged(object sender, EventArgs e)
{
    // Only validate when custom mode is active
    if (!startDatePicker.Visible) return;

    // Disable apply button and show validation
    ValidateDateRange();
}
```

### 3. Validation logic

```csharp
/// <summary>
/// Validate that start date is before end date
/// </summary>
private bool ValidateDateRange()
{
    if (startDatePicker.Value.Date >= endDatePicker.Value.Date)
    {
        // Show error state
        applyButton.Enabled = false;
        applyButton.BackColor = Color.Gray;
        
        // Show tooltip with error message
        var tooltip = new ToolTip();
        tooltip.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!", 
            applyButton, 
            0, 
            -30, 
            3000);
        
        return false;
    }
    else
    {
        // Reset to normal state
        applyButton.Enabled = true;
        applyButton.BackColor = Color.FromArgb(56, 178, 172);
        return true;
    }
}
```

### 4. Validate when switching to custom mode

```csharp
private void FilterCombo_SelectedIndexChanged(object sender, EventArgs e)
{
    bool isCustom = filterCombo.SelectedIndex == 3;
    startDatePicker.Visible = isCustom;
    endDatePicker.Visible = isCustom;
    applyButton.Visible = isCustom;

    AdjustFilterPosition();

    // ✅ NEW: Validate dates when switching to custom mode
    if (isCustom)
    {
        ValidateDateRange();
    }
    else
    {
        _ = ApplyFilter();
    }
}
```

### 5. Validate before applying filter

```csharp
private async Task ApplyFilter()
{
    DateTime? start = null;
    DateTime? end = null;
    var now = DateTime.Now;

    switch (filterCombo.SelectedIndex)
    {
        // ... other cases ...
        
        case 3: // Custom
            // ✅ NEW: Validate date range before applying
            if (!ValidateDateRange())
            {
                MessageBox.Show(
                    "Ngày bắt đầu phải nhỏ hơn ngày kết thúc!",
                    "Lỗi khoảng thời gian",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            start = startDatePicker.Value.Date;
            end = endDatePicker.Value.Date.AddDays(1).AddTicks(-1);
            break;
    }

    await LoadData(start, end);
}
```

## UI/UX Behavior

### Normal State (Valid Range)
```
┌────────────────────────────────────────────────────────────────┐
│ [Tùy chọn ▼] [01/01/2024] [31/01/2024] [Áp dụng (Enabled)]   │
│                                          ↑ Green color         │
└────────────────────────────────────────────────────────────────┘
```

### Error State (Invalid Range)
```
┌────────────────────────────────────────────────────────────────┐
│ [Tùy chọn ▼] [31/01/2024] [01/01/2024] [Áp dụng (Disabled)]  │
│                                          ↑ Gray color          │
│                                     "Ngày bắt đầu phải..."     │
│                                     ← Tooltip hiện 3 giây      │
└────────────────────────────────────────────────────────────────┘
```

## Validation Triggers

### 1. **Khi thay đổi ngày bắt đầu**
- ✅ Check ngay lập tức
- ✅ Disable/Enable button "Áp dụng"
- ✅ Show/Hide tooltip

### 2. **Khi thay đổi ngày kết thúc**
- ✅ Check ngay lập tức
- ✅ Disable/Enable button "Áp dụng"
- ✅ Show/Hide tooltip

### 3. **Khi chuyển sang chế độ "Tùy chọn"**
- ✅ Validate ngay lập tức
- ✅ Đảm bảo state đúng từ đầu

### 4. **Khi click button "Áp dụng"**
- ✅ Double-check validation
- ✅ Show MessageBox nếu invalid
- ✅ Không load data nếu invalid

## Visual Feedback

### Valid State (✅)
- Button "Áp dụng": **Enabled**
- Button color: **Green** `Color.FromArgb(56, 178, 172)`
- Cursor: **Hand** (clickable)

### Invalid State (❌)
- Button "Áp dụng": **Disabled**
- Button color: **Gray** `Color.Gray`
- Cursor: **Default** (not clickable)
- Tooltip: **Visible** for 3 seconds
- MessageBox: **Show** when trying to apply

## Example Scenarios

### Scenario 1: Valid Range
```
Ngày bắt đầu: 01/01/2024
Ngày kết thúc: 31/01/2024
Result: ✅ Valid (01/01 < 31/01)
Button: Enabled (Green)
```

### Scenario 2: Invalid Range (End before Start)
```
Ngày bắt đầu: 31/01/2024
Ngày kết thúc: 01/01/2024
Result: ❌ Invalid (31/01 >= 01/01)
Button: Disabled (Gray)
Tooltip: "Ngày bắt đầu phải nhỏ hơn ngày kết thúc!"
```

### Scenario 3: Same Day
```
Ngày bắt đầu: 15/01/2024
Ngày kết thúc: 15/01/2024
Result: ❌ Invalid (15/01 >= 15/01)
Button: Disabled (Gray)
Tooltip: "Ngày bắt đầu phải nhỏ hơn ngày kết thúc!"
```

### Scenario 4: One Day Apart
```
Ngày bắt đầu: 15/01/2024
Ngày kết thúc: 16/01/2024
Result: ✅ Valid (15/01 < 16/01)
Button: Enabled (Green)
```

## Code Flow

```
User changes date picker
    ↓
DatePicker_ValueChanged() triggered
    ↓
Check if custom mode active
    ↓ (Yes)
ValidateDateRange()
    ↓
Compare start vs end date
    ↓
    ├─ Invalid (start >= end)
    │   ↓
    │   - Disable button
    │   - Gray button color
    │   - Show tooltip
    │   
    └─ Valid (start < end)
        ↓
        - Enable button
        - Green button color
        - Hide tooltip

User clicks "Áp dụng"
    ↓
ApplyFilter() called
    ↓
ValidateDateRange() (double-check)
    ↓
    ├─ Invalid
    │   ↓
    │   - Show MessageBox
    │   - Return (don't load data)
    │   
    └─ Valid
        ↓
        - Load data with date range
```

## Benefits

✅ **Better UX**: Real-time feedback khi user chọn ngày
✅ **Prevent Errors**: Không thể apply invalid range
✅ **Clear Messaging**: Tooltip + MessageBox giải thích rõ
✅ **Visual Indication**: Button color change thấy ngay
✅ **No Data Waste**: Không query database với invalid range

## Testing Checklist

- [x] Build successful
- [ ] Valid range: Button enabled và green
- [ ] Invalid range: Button disabled và gray
- [ ] Same day: Button disabled
- [ ] Tooltip hiển thị khi invalid
- [ ] Tooltip tự động ẩn sau 3 giây
- [ ] MessageBox show khi click "Áp dụng" với invalid range
- [ ] Data không load khi invalid
- [ ] Validation chạy real-time khi thay đổi date picker
- [ ] Validation chạy khi switch sang custom mode
- [ ] Button color reset khi valid lại
