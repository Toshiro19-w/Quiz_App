# MyFlashcardsControl - Qu?n lý Flashcard Sets c?a Gi?ng viên

## T?ng quan
`MyFlashcardsControl` là m?t UserControl ?? qu?n lý các b? flashcard mà ng??i dùng ?ã t?o, v?i ??y ?? ch?c n?ng CRUD và phân trang.

## Tính n?ng chính

### 1. **Hi?n th? danh sách Flashcard Sets**
- Hi?n th? t?t c? b? flashcard c?a ng??i dùng hi?n t?i (theo `OwnerId`)
- S?p x?p theo ngày t?o (m?i nh?t tr??c)
- L?c b? các b? ?ã xóa (`IsDeleted = false`)
- Hi?n th? thông tin: ID, Tiêu ??, S? th?, Hi?n th?, Ngôn ng?, Ngày t?o

### 2. **Thanh công c? (Header)**
- **T?o b? Flashcard** (?): Nút ?? t?o flashcard set m?i (ch?a implement)
- **Quay l?i** (??): Quay v? MyCoursesControl

### 3. **B? l?c và tìm ki?m**
- Thanh tìm ki?m theo tiêu ?? ho?c mô t?
- Ch?n s? l??ng hi?n th?: 10, 25, 50, 100 dòng/trang
- Tìm ki?m real-time (c?p nh?t khi gõ)

### 4. **B?ng d? li?u**

| C?t | Mô t? |
|-----|-------|
| ID | S? th? t? trong danh sách |
| Tiêu ?? | Tên b? flashcard |
| S? th? | Badge hi?n th? s? l??ng th? (màu xanh d??ng nh?t) |
| Hi?n th? | Badge: "Công khai" (xanh) ho?c "Riêng t?" (xám) |
| Ngôn ng? | Ngôn ng? c?a flashcard (m?c ??nh: vi) |
| T?o lúc | Ngày t?o b? flashcard |
| Hành ??ng | 4 nút: Xem, H?c, S?a, Xóa |

### 5. **Hành ??ng trên t?ng Flashcard Set**
- **Xem** (???): Xem chi ti?t b? flashcard (màu xanh d??ng)
- **H?c** (??): B?t ??u h?c flashcard (màu xanh d??ng ??m)
- **S?a** (??): Ch?nh s?a b? flashcard (màu vàng)
- **Xóa** (???): Xóa b? flashcard v?i xác nh?n (màu ??)
  - Hi?n th? popup xác nh?n tr??c khi xóa
  - Soft delete (set `IsDeleted = true`)
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
??? MyFlashcardsControl.cs           # Logic chính
??? MyFlashcardsControl.Designer.cs   # UI Designer code
??? MyFlashcardsControl.resx          # Resources
```

## ?i?u h??ng

### T? MyCoursesControl ? MyFlashcardsControl
```csharp
private void BtnFlashcards_Click(object sender, EventArgs e)
{
    var form = this.FindForm();
    if (form is MainContainer mainContainer)
    {
        var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
        if (mainPanel != null)
        {
            mainPanel.Controls.Clear();
            var myFlashcardsControl = new MyFlashcardsControl();
            myFlashcardsControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(myFlashcardsControl);
        }
    }
}
```

### T? MyFlashcardsControl ? MyCoursesControl
```csharp
private void BtnBack_Click(object sender, EventArgs e)
{
    var form = this.FindForm();
    if (form is MainContainer mainContainer)
    {
        var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
        if (mainPanel != null)
        {
            mainPanel.Controls.Clear();
            var myCoursesControl = new MyCoursesControl();
            myCoursesControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(myCoursesControl);
        }
    }
}
```

## Database Query

```csharp
_allFlashcardSets = await context.FlashcardSets
    .Include(fs => fs.Flashcards)
    .Where(fs => fs.OwnerId == userId.Value && !fs.IsDeleted)
    .OrderByDescending(fs => fs.CreatedAt)
    .ToListAsync();
```

## State Management

```csharp
private int _currentPage = 1;              // Trang hi?n t?i
private int _pageSize = 10;                // S? dòng/trang
private int _totalRecords = 0;             // T?ng s? flashcard sets
private List<FlashcardSet> _allFlashcardSets; // Cache d? li?u
```

## UI Layout (Positions)

### Row Layout (Width: ~1540px)
```
ID (20)  |  Title (175-625)  |  CardCount (629)  |  Visibility (729)  |  Language (824)  |  Date (1017)  |  Actions (1346-1481)
   40px  |      450px        |      80px         |      80px          |     80px         |     155px     |     145px (4 buttons)
```

### Action Buttons
- **Xem**: Position 1346, Color: Primary (Blue)
- **H?c**: Position 1391, Color: #3490DC
- **S?a**: Position 1436, Color: #FFC107 (Yellow)
- **Xóa**: Position 1481, Color: #DC3545 (Red)
- Spacing: 45px gi?a các nút
- Size: 36 x 34 px

## Màu s?c

### Badges
- **S? th?**: `#17A2B8` (Cyan/Teal)
- **Công khai**: `#28A745` (Green)
- **Riêng t?**: `#6C757D` (Gray)

### Action Buttons
- **Nút Xem**: `ColorPalette.Primary`
- **Nút H?c**: `#3490DC` (Blue)
- **Nút S?a**: `#FFC107` (Yellow)
- **Nút Xóa**: `#DC3545` (Red)

### Header Actions
- **Nút T?o**: `#17A2B8` (Cyan)
- **Nút Quay l?i**: `#6C757D` (Gray)

## Hover Effects
- Row hover: ??i n?n sang `ColorPalette.Background`
- Button hover: T? ??ng t? FlatStyle

## Error Handling
- Try-catch cho t?t c? async operations
- MessageBox thông báo l?i chi ti?t
- Ki?m tra null/empty states
- Validate user login tr??c khi load
- Soft delete thay vì hard delete

## T??ng lai (TODO)

### ?ã implement
- ? Hi?n th? danh sách flashcard sets
- ? Tìm ki?m và l?c
- ? Phân trang
- ? Xóa flashcard set (soft delete)
- ? Navigation qua l?i v?i MyCoursesControl

### Ch?a implement
- [ ] T?o b? flashcard m?i
- [ ] Xem chi ti?t flashcard set
- [ ] Ch? ?? h?c flashcard (study mode)
- [ ] Ch?nh s?a flashcard set
- [ ] Export/Import flashcard sets
- [ ] Th?ng kê h?c t?p
- [ ] Chia s? flashcard sets
- [ ] Duplicate flashcard set
- [ ] Filter theo visibility
- [ ] Sort theo nhi?u tiêu chí

## Dependencies
- `Microsoft.EntityFrameworkCore`
- `WinFormsApp1.Helpers.AuthHelper`
- `WinFormsApp1.Helpers.ColorPalette`
- `WinFormsApp1.Helpers.ToastHelper`
- `WinFormsApp1.Models.EF.LearningPlatformContext`
- `WinFormsApp1.Models.Entities.FlashcardSet`
- `WinFormsApp1.Models.Entities.Flashcard`

## Database Schema

### FlashcardSet
```sql
SetId           INT IDENTITY(1,1) PRIMARY KEY
OwnerId         INT NOT NULL
Title           NVARCHAR(200) NOT NULL
Description     NVARCHAR(MAX) NULL
Visibility      VARCHAR(20) NOT NULL  -- Private/Public/Course
CoverUrl        NVARCHAR(500) NULL
TagsText        NVARCHAR(500) NULL
Language        NVARCHAR(20) NULL
CreatedAt       DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME()
UpdatedAt       DATETIME2(7) NULL
IsDeleted       BIT NOT NULL DEFAULT (0)
```

### Flashcard
```sql
CardId          INT IDENTITY(1,1) PRIMARY KEY
SetId           INT NOT NULL
FrontText       NVARCHAR(MAX) NOT NULL
BackText        NVARCHAR(MAX) NOT NULL
FrontMediaId    INT NULL
BackMediaId     INT NULL
Hint            NVARCHAR(500) NULL
OrderIndex      INT NOT NULL DEFAULT (0)
CreatedAt       DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME()
UpdatedAt       DATETIME2(7) NULL
```

## Build Status
? Build successful - Không có l?i compilation
? Navigation ho?t ??ng hai chi?u
? Soft delete thay vì hard delete
? Include Flashcards ?? ??m s? th?

## Responsive Design
- FlowLayoutPanel t? ??ng scroll
- Width ??ng theo parent container
- AutoEllipsis cho text dài
- Badges có kích th??c c? ??nh

## Usage Example

```csharp
// T? b?t k? ?âu trong MainContainer
var myFlashcardsControl = new MyFlashcardsControl();
myFlashcardsControl.Dock = DockStyle.Fill;
mainContentPanel.Controls.Clear();
mainContentPanel.Controls.Add(myFlashcardsControl);
```

## Screenshots Layout

```
???????????????????????????????????????????????????????????????????????
?  B? Flashcard c?a tôi              [? T?o b? Flashcard] [?? Quay l?i] ?
???????????????????????????????????????????????????????????????????????
?  Hi?n th? [10 ?] d? li?u                    Tìm ki?m: [____________]?
??????????????????????????????????????????????????????????????????????
?ID? Tiêu ??    ?S? th??Hi?n th??Ngôn ng??  T?o lúc ?  Hành ??ng     ?
??????????????????????????????????????????????????????????????????????
?1 ?Flashcards..? 2 th??Công khai?   vi   ?18/11/2025???? ?? ?? ???   ?
??????????????????????????????????????????????????????????????????????
  Hi?n th? 1 t?i 1 c?a 1 d? li?u    [??u][Tr??c][1][Sau][Cu?i]
```
