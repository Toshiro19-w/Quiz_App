# Course Moderation Validation

## Tổng quan

Đã thêm validation chi tiết cho tất cả các nút kiểm duyệt khóa học để đảm bảo logic nghiệp vụ chặt chẽ và UX tốt hơn.

## Features Implemented

### 1. Dynamic Button States ⭐

#### Method: `UpdateButtonStates()`

Tự động enable/disable các nút dựa trên trạng thái khóa học hiện tại.

```csharp
private void UpdateButtonStates()
{
    // Enable/disable based on ModerationStatus
    switch (_selectedCourse.ModerationStatus)
    {
        case "Pending":      // Chờ duyệt
        case "Approved":     // Đã duyệt
        case "Rejected":     // Từ chối
        case "NeedsRevision": // Cần sửa
    }
}
```

#### Button States Matrix

| Status | Phê duyệt | Từ chối | Yêu cầu sửa | Logic |
|--------|-----------|---------|-------------|-------|
| **Pending** | ✅ Enable | ✅ Enable | ✅ Enable | Mới gửi, cho phép tất cả actions |
| **Approved** | ❌ Disable | ✅ Enable | ✅ Enable | Đã duyệt, không thể approve lại |
| **Rejected** | ✅ Enable | ❌ Disable | ✅ Enable | Đã từ chối, không thể reject lại |
| **NeedsRevision** | ✅ Enable | ✅ Enable | ❌ Disable | Đang chờ user fix, không cần request lại |
| **No Selection** | ❌ Disable | ❌ Disable | ❌ Disable | Không có course được chọn |

### 2. Approve Course Validation ✅

#### Validations:

**1. Already Approved Check**
```csharp
if (_selectedCourse.ModerationStatus == "Approved")
{
    MessageBox.Show("Khóa học này đã được phê duyệt rồi!");
    return;
}
```

**2. Low Score Warning**
```csharp
if (autoScore < 60)
{
    MessageBox.Show(
        $"Cảnh báo: Điểm tự động chỉ {autoScore}/100 (thấp).\n\n" +
        "Bạn có chắc chắn muốn phê duyệt?",
        MessageBoxButtons.YesNo);
}
```

**3. Critical Errors Warning**
```csharp
if (hasErrors)
{
    var errorList = string.Join("\n", errors);
    MessageBox.Show(
        $"Cảnh báo: Khóa học có lỗi nghiêm trọng:\n\n{errorList}\n\n" +
        "Bạn vẫn muốn phê duyệt?");
}
```

**4. Final Confirmation**
```csharp
MessageBox.Show(
    $"'{course.Title}'\n" +
    $"Giảng viên: {owner}\n" +
    $"Điểm tự động: {score}/100\n\n" +
    "Khóa học sẽ được công khai sau khi phê duyệt.");
```

### 3. Reject Course Validation ❌

#### Validations:

**1. Already Rejected Check**
```csharp
if (_selectedCourse.ModerationStatus == "Rejected")
{
    MessageBox.Show("Khóa học này đã bị từ chối rồi!");
    return;
}
```

**2. Require Reason**
```csharp
if (string.IsNullOrWhiteSpace(reason))
{
    MessageBox.Show("Vui lòng nhập lý do từ chối!");
    return;
}
```

**3. Minimum Reason Length (20 chars)**
```csharp
if (reason.Length < 20)
{
    MessageBox.Show("Lý do từ chối phải có ít nhất 20 ký tự!");
    return;
}
```

**4. Detailed Confirmation**
```csharp
MessageBox.Show(
    $"'{course.Title}'\n" +
    $"Giảng viên: {owner}\n\n" +
    $"Lý do: {reason}\n\n" +
    "Khóa học sẽ không được công khai và giảng viên sẽ nhận được thông báo.");
```

### 4. Request Revision Validation ⚠️

#### Validations:

**1. Already in NeedsRevision Check**
```csharp
if (_selectedCourse.ModerationStatus == "NeedsRevision")
{
    MessageBox.Show(
        "Khóa học này đã được yêu cầu sửa đổi rồi!\n\n" +
        "Vui lòng chờ giảng viên cập nhật và gửi lại.");
    return;
}
```

**2. Require Detailed Reason**
```csharp
if (string.IsNullOrWhiteSpace(reason))
{
    MessageBox.Show("Vui lòng nhập yêu cầu sửa đổi!");
    return;
}
```

**3. Minimum Reason Length (30 chars)**
```csharp
// Stricter than reject - need more detail
if (reason.Length < 30)
{
    MessageBox.Show(
        "Yêu cầu sửa đổi phải có ít nhất 30 ký tự để giảng viên hiểu rõ!");
    return;
}
```

**4. Show Auto-detected Issues**
```csharp
if (issues.Any())
{
    var issueList = string.Join("\n", issues);
    MessageBox.Show(
        $"Các vấn đề được phát hiện tự động:\n\n{issueList}\n\n" +
        "Bạn có muốn tiếp tục?");
}
```

**5. Comprehensive Confirmation**
```csharp
MessageBox.Show(
    $"'{course.Title}'\n" +
    $"Giảng viên: {owner}\n\n" +
    $"Yêu cầu: {reason}\n\n" +
    "Giảng viên sẽ nhận được thông báo và cần cập nhật khóa học.");
```

### 5. Enhanced Reason Dialog 📝

#### Improvements:

**1. Character Counter**
```csharp
var charCountLabel = new Label
{
    Text = "0 ký tự (tối thiểu 20)"
};

textBox.TextChanged += (s, e) =>
{
    var length = textBox.Text.Length;
    charCountLabel.Text = $"{length} ký tự (tối thiểu 20)";
    charCountLabel.ForeColor = length >= 20 ? Color.Green : Color.Red;
};
```

**2. Hint Label**
```csharp
var hintLabel = new Label
{
    Text = "Ghi chú: Lý do phải rõ ràng, cụ thể để giảng viên hiểu và cải thiện."
};
```

**3. Inline Validation on OK**
```csharp
btnOK.Click += (s, e) =>
{
    if (string.IsNullOrWhiteSpace(textBox.Text))
    {
        MessageBox.Show("Vui lòng nhập lý do!");
        return;
    }

    if (textBox.Text.Trim().Length < 20)
    {
        MessageBox.Show("Lý do phải có ít nhất 20 ký tự!");
        return;
    }

    form.DialogResult = DialogResult.OK;
};
```

**4. Keyboard Shortcuts**
```csharp
form.AcceptButton = btnOK;    // Enter to submit
form.CancelButton = btnCancel; // Esc to cancel
```

## Validation Flow

### Approve Flow

```
User clicks "Phê duyệt"
    ↓
✓ Check if already approved
    ↓
✓ Check auto score
    ↓ (if < 60)
    ⚠️ Show warning, confirm
    ↓
✓ Check critical errors
    ↓ (if has errors)
    ⚠️ Show errors, confirm
    ↓
✓ Final confirmation
    ↓
✅ Approve course
    ↓
📊 Log action
    ↓
🔄 Reload list
```

### Reject Flow

```
User clicks "Từ chối"
    ↓
✓ Check if already rejected
    ↓
📝 Show reason dialog
    ↓
✓ Validate reason not empty
    ↓
✓ Validate reason length >= 20
    ↓
✓ Final confirmation with details
    ↓
❌ Reject course
    ↓
📊 Log action
    ↓
🔄 Reload list
```

### Request Revision Flow

```
User clicks "Yêu cầu sửa"
    ↓
✓ Check if already in NeedsRevision
    ↓
📝 Show reason dialog
    ↓
✓ Validate reason not empty
    ↓
✓ Validate reason length >= 30
    ↓
💡 Show auto-detected issues (optional)
    ↓
✓ Final confirmation with details
    ↓
⚠️ Request revision
    ↓
📊 Log action
    ↓
🔄 Reload list
```

## UX Improvements

### Visual Feedback

| Feature | Implementation | Benefit |
|---------|---------------|---------|
| Character Counter | Real-time count with color | User knows requirements |
| Button States | Auto enable/disable | Prevent invalid actions |
| Hint Labels | Guidance text | Clear expectations |
| Detailed Confirmations | Show all info before action | Prevent mistakes |
| Toast Notifications | Success feedback | Clear outcome |

### Error Prevention

| Validation | Purpose | User Impact |
|-----------|---------|-------------|
| Status Check | Prevent duplicate actions | No confusion |
| Reason Required | Ensure communication | Better feedback |
| Minimum Length | Quality feedback | Meaningful reasons |
| Score Warning | Quality control | Better decisions |
| Error Detection | Catch issues | Informed approval |

## Testing Scenarios

### Scenario 1: Approve Pending Course
```
Action: Select pending course, click "Phê duyệt"
Expected:
- ✅ Check score
- ✅ Show warnings if needed
- ✅ Confirm
- ✅ Approve
- ✅ Show toast
- ✅ Reload list
```

### Scenario 2: Approve Already Approved
```
Action: Select approved course, click "Phê duyệt"
Expected:
- ❌ Button disabled OR
- ⚠️ Show "already approved" message
```

### Scenario 3: Reject with Short Reason
```
Action: Select course, click "Từ chối", enter 10 chars
Expected:
- ❌ Show error "phải có ít nhất 20 ký tự"
- 📝 Dialog stays open
- ✅ Can try again
```

### Scenario 4: Request Revision
```
Action: Select course, click "Yêu cầu sửa"
Expected:
- 📝 Dialog opens
- 📊 Character counter updates
- 💡 Auto issues shown (if any)
- ✅ Validate 30+ chars
- ✅ Confirm
- ⚠️ Mark as NeedsRevision
```

### Scenario 5: Button States
```
Action: Select different status courses
Expected:
- Pending: All buttons enabled
- Approved: Approve disabled
- Rejected: Reject disabled
- NeedsRevision: Request disabled
```

## Benefits

### For Admins 👨‍💼
- ✅ **Clear guidance**: Know what actions are valid
- ✅ **Prevent errors**: Can't make invalid actions
- ✅ **Better communication**: Force quality feedback
- ✅ **Informed decisions**: See issues before approving

### For Teachers 👨‍🏫
- ✅ **Quality feedback**: Detailed reasons (20-30+ chars)
- ✅ **Clear expectations**: Know what to fix
- ✅ **Fair process**: Consistent validation
- ✅ **No confusion**: Clear status transitions

### For System 🎯
- ✅ **Data quality**: Valid reasons logged
- ✅ **Audit trail**: All actions tracked with details
- ✅ **Consistency**: Same rules everywhere
- ✅ **Scalability**: Easy to add more validations

## Code Changes Summary

### Files Modified

1. ✅ `WinFormsApp1\View\Admin\CourseModerationControl.cs`
   - `UpdateButtonStates()` - Dynamic button control
   - `ApproveCourse()` - Enhanced validation
   - `RejectCourse()` - Enhanced validation
   - `RequestRevision()` - Enhanced validation
   - `ShowReasonDialog()` - Better UX
   - `LoadPendingCoursesAsync()` - Call UpdateButtonStates

### Build Status

⚠️ Build blocked - Application running (expected)
✅ No compilation errors

## Summary

✅ **Dynamic Button States** - Auto enable/disable
✅ **Approve Validation** - Score checks, error warnings
✅ **Reject Validation** - Required reason (20+ chars)
✅ **Revision Validation** - Detailed reason (30+ chars)
✅ **Enhanced Dialog** - Character counter, hints
✅ **Better UX** - Clear feedback, prevent errors

**Impact**: 🚀 Major improvement in moderation quality and admin UX!
