# MyCoursesControl - Qu?n lý khóa h?c c?a gi?ng viên

## T?ng quan
`MyCoursesControl` là m?t UserControl hi?n th? danh sách các khóa h?c mà ng??i dùng hi?n t?i ?ã t?o, v?i ??y ?? ch?c n?ng qu?n lý và phân trang.

## Tính n?ng chính

### 1. **Hi?n th? danh sách khóa h?c**
- Hi?n th? t?t c? khóa h?c c?a ng??i dùng hi?n t?i (theo `OwnerId`)
- S?p x?p theo ngày t?o (m?i nh?t tr??c)
- Hi?n th? thông tin: ID, Tiêu ??, Slug, Tr?ng thái, Giá, Ngày t?o

### 2. **Thanh công c? (Header)**
- **T?o khóa h?c** (?): Nút ?? t?o khóa h?c m?i (ch?a implement)
- **Doanh thu** (??): Xem th?ng kê doanh thu (ch?a implement)
- **Flashcard c?a tôi** (???): Qu?n lý flashcard (ch?a implement)

### 3. **B? l?c và tìm ki?m**
- Thanh tìm ki?m theo tiêu ?? ho?c slug
- Ch?n s? l??ng hi?n th?: 10, 25, 50, 100 dòng/trang
- Tìm ki?m real-time (c?p nh?t khi gõ)

### 4. **B?ng d? li?u**
| C?t | Mô t? |
|-----|-------|
| ID | S? th? t? trong danh sách |
| Tiêu ?? | Tên khóa h?c và slug |
| Tr?ng thái | Badge màu: Xanh (?ã xu?t b?n), Xám (Nháp) |
| Giá | Giá khóa h?c (??nh d?ng VN?) |
| T?o lúc | Ngày t?o khóa h?c |
| Hành ??ng | 3 nút: Xem, S?a, Xóa |

### 5. **Hành ??ng trên t?ng khóa h?c**
- **Xem** (???): Xem chi ti?t khóa h?c (màu xanh d??ng)
- **S?a** (??): Ch?nh s?a khóa h?c (màu vàng)
- **Xóa** (???): Xóa khóa h?c v?i xác nh?n (màu ??)
  - Hi?n th? popup xác nh?n tr??c khi xóa
  - Xóa t? database v?i CASCADE
  - Hi?n th? toast thông báo thành công

### 6. **Phân trang**
- Hi?n th? t?ng s? b?n ghi và ph?m vi hi?n t?i
- Các nút ?i?u h??ng:
  - **??u tiên**: V? trang 1
  - **Tr??c**: Trang tr??c
  - **[S? trang]**: Trang hi?n t?i (highlight màu xanh)
  - **Sau**: Trang sau
  - **Cu?i cùng**: ??n trang cu?i
- T? ??ng disable các nút không kh? d?ng

## C?u trúc file

```
WinFormsApp1/View/User/Controls/
??? MyCoursesControl.cs           # Logic chính
??? MyCoursesControl.Designer.cs   # UI Designer code
??? MyCoursesControl.resx          # Resources
```

## S? d?ng

### ?i?u h??ng t? MainContainer
```csharp
private void btnGiangVien_Click(object sender, EventArgs e)
{
    NavigateToControl(new Controls.MyCoursesControl());
}
```

### Kh?i t?o
- T? ??ng load khóa h?c khi kh?i t?o
- Ki?m tra ??ng nh?p tr??c khi load
- M?c ??nh hi?n th? 10 khóa h?c/trang

## Database Query
```csharp
_allCourses = await context.Courses
    .Where(c => c.OwnerId == userId.Value)
    .OrderByDescending(c => c.CreatedAt)
    .ToListAsync();
```

## State Management
```csharp
private int _currentPage = 1;         // Trang hi?n t?i
private int _pageSize = 10;           // S? dòng/trang
private int _totalRecords = 0;        // T?ng s? khóa h?c
private List<Course> _allCourses;     // Cache d? li?u
```

## UI/UX Features

### Màu s?c
- **Background chính**: #F8F9FA (Light gray)
- **Tr?ng thái ?ã xu?t b?n**: #28A745 (Green)
- **Tr?ng thái Nháp**: #6C757D (Gray)
- **Nút T?o**: #3490DC (Blue)
- **Nút Doanh thu**: #28A745 (Green)
- **Nút Flashcard**: #17A2B8 (Cyan)
- **Nút Xem**: ColorPalette.Primary
- **Nút S?a**: #FFC107 (Yellow)
- **Nút Xóa**: #DC3545 (Red)

### Hover Effects
- Row hover: ??i n?n sang ColorPalette.Background
- Button hover: T? ??ng t? FlatStyle

### Responsive
- FlowLayoutPanel t? ??ng scroll
- Width ??ng theo parent container
- AutoEllipsis cho text dài

## Error Handling
- Try-catch cho t?t c? async operations
- MessageBox thông báo l?i chi ti?t
- Ki?m tra null/empty states
- Validate user login tr??c khi load

## T??ng lai (TODO)
- [ ] Implement ch?c n?ng t?o khóa h?c m?i
- [ ] Implement xem doanh thu
- [ ] Implement qu?n lý flashcard
- [ ] Navigate ??n course detail khi click Xem
- [ ] M? dialog Edit Course khi click S?a
- [ ] Export danh sách khóa h?c
- [ ] Bulk actions (xóa nhi?u, publish nhi?u)
- [ ] Sort theo các c?t khác nhau
- [ ] Filter theo tr?ng thái, giá, ngày t?o

## Dependencies
- `Microsoft.EntityFrameworkCore`
- `WinFormsApp1.Helpers.AuthHelper`
- `WinFormsApp1.Helpers.ColorPalette`
- `WinFormsApp1.Helpers.ToastHelper`
- `WinFormsApp1.Models.EF.LearningPlatformContext`
- `WinFormsApp1.Models.Entities.Course`

## Build Status
? Build successful - Không có l?i compilation
