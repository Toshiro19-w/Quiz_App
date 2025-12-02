# Dialog Responsive Fix & Course Moderation Draft Filter

## Tổng quan

Đã thực hiện 2 improvements quan trọng:
1. ✅ Sửa tất cả dialogs của Flashcard Management để responsive
2. ✅ Loại bỏ khóa học "Nháp" khỏi hệ thống kiểm duyệt

## 1. Flashcard Dialogs - Responsive Fix

### Vấn đề trước đây
- ❌ `FormBorderStyle = FixedDialog` → Không thể resize
- ❌ `MaximizeBox = false` → Không thể maximize
- ❌ No minimum size → UI có thể bị vỡ khi window nhỏ
- ❌ Fixed size → Không flexible cho màn hình khác nhau

### Giải pháp

#### Create Flashcard Dialog
```csharp
var dialogForm = new Form
{
    Text = "Tạo Flashcard Set mới",
    Size = new Size(1000, 750),           // ✅ Larger default size
    MinimumSize = new Size(800, 600),     // ✅ Set minimum
    StartPosition = FormStartPosition.CenterParent,
    FormBorderStyle = FormBorderStyle.Sizable, // ✅ Allow resize
    MaximizeBox = true,                    // ✅ Allow maximize
    MinimizeBox = false
};
```

## 2. Course Moderation - Draft Filter

### Vấn đề trước đây
Khóa học "Nháp" (IsPublished = false) xuất hiện trong hệ thống kiểm duyệt, gây confusion.

### Giải pháp

```csharp
var query = context.Courses
    .Include(c => c.Owner)
    // ✅ Chỉ load khóa học đã xuất bản
    .Where(c => c.IsPublished == true)
    .AsQueryable();
```

### Logic Flow

```
Nháp (Draft) → IsPublished = false → ❌ NOT in moderation
    ↓
Submit for Review → IsPublished = true → ✅ IN moderation
```

## Summary

✅ **Flashcard Dialogs**: All 3 dialogs responsive
✅ **Course Moderation**: Draft courses filtered
✅ **Build**: Successful
✅ **UX**: Major improvement
