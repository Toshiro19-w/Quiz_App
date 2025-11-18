# Flashcard Study Feature - README

## T?ng quan

Tính n?ng h?c flashcard cho phép ng??i dùng h?c các b? th? flashcard m?t cách t??ng tác v?i giao di?n ??p m?t và d? s? d?ng.

## C?u trúc

### 1. FlashcardStudyForm
- **Mô t?**: Form chính ?? h?c flashcard
- **Ch?c n?ng**:
  - Qu?n lý lu?ng h?c t?p
  - Chuy?n ??i gi?a StudyControl và FinishControl
  - T?i d? li?u flashcard t? database

### 2. StudyControl
- **Mô t?**: Control hi?n th? flashcard ?? h?c
- **Tính n?ng**:
  - Hi?n th? câu h?i và câu tr? l?i
  - L?t th? khi click ho?c nh?n Space
  - Thanh ti?n trình h?c t?p
  - ?i?u h??ng gi?a các th? (Previous/Next)
  - Nút "Hoàn thành" ? th? cu?i cùng
  
### 3. FinishControl  
- **Mô t?**: Control hi?n th? k?t qu? khi hoàn thành
- **Tính n?ng**:
  - Hi?n th? thông báo chúc m?ng
  - Animation celebration (icon l?p lánh)
  - 3 nút hành ??ng:
    - ?? B?t ??u l?i: H?c l?i t? ??u
    - ?? Xem thêm các flashcard khác: Quay v? danh sách
    - ?? Trang ch?: V? trang ch?

## Cách s? d?ng

### T? FlashcardDetailControl

```csharp
private void btnStartLearning_Click(object sender, EventArgs e)
{
    // Ki?m tra flashcard t?n t?i
    if (_flashcardSet == null || _flashcardSet.Flashcards.Count == 0)
    {
        MessageBox.Show("B? flashcard này ch?a có th? nào ?? h?c!");
        return;
    }

    // M? form h?c
    var studyForm = new FlashcardStudyForm(_setId);
    studyForm.ShowDialog();
}
```

### Phím t?t trong StudyControl

- **Space**: L?t th? flashcard
- **?** (Left Arrow): Quay v? th? tr??c
- **?** (Right Arrow): Chuy?n sang th? ti?p theo

## Màu s?c & Thi?t k?

### StudyControl
- Background: `Color.FromArgb(40, 20, 100)` - Purple gradient
- Card Panel: White v?i border orange `Color.FromArgb(255, 140, 0)`
- Progress Bar: Orange
- Buttons: White v?i border orange

### FinishControl
- Background: `Color.FromArgb(40, 20, 100)` - Purple gradient
- Center Panel: `Color.FromArgb(60, 40, 120)` - Lighter purple
- Celebration Icon: Gold v?i animation
- Buttons:
  - Restart: Orange `Color.FromArgb(255, 140, 0)`
  - View Other: Green `Color.FromArgb(76, 175, 80)`
  - Go Home: Blue `Color.FromArgb(0, 180, 216)`

## Lu?ng ho?t ??ng

1. **B?t ??u h?c**
   - User click nút "B?t ??u h?c" t? FlashcardDetailControl
   - FlashcardStudyForm ???c m? v?i full screen
   - T?i danh sách flashcards t? database
   
2. **H?c flashcard**
   - StudyControl hi?n th? th? ??u tiên (m?t tr??c - câu h?i)
   - User click ho?c nh?n Space ?? l?t th? (hi?n th? m?t sau - câu tr? l?i)
   - S? d?ng nút Previous/Next ho?c phím ? ? ?? ?i?u h??ng
   - Thanh ti?n trình c?p nh?t theo s? th? ?ã h?c

3. **Hoàn thành**
   - Khi ??n th? cu?i cùng, nút "Hoàn thành" xu?t hi?n
   - Click "Hoàn thành" ?? chuy?n sang FinishControl
   - FinishControl hi?n th? v?i animation celebration
   - User ch?n: H?c l?i, Xem flashcard khác, ho?c V? trang ch?

## Database Schema

```sql
-- FlashcardSets table
CREATE TABLE dbo.FlashcardSets (
    SetId INT IDENTITY(1,1) PRIMARY KEY,
    OwnerId INT NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    ...
);

-- Flashcards table  
CREATE TABLE dbo.Flashcards (
    CardId INT IDENTITY(1,1) PRIMARY KEY,
    SetId INT NOT NULL,
    FrontText NVARCHAR(MAX) NOT NULL,
    BackText NVARCHAR(MAX) NOT NULL,
    OrderIndex INT NOT NULL,
    ...
);
```

## Files Created

1. `FlashcardStudyForm.cs` - Form chính
2. `FlashcardStudyForm.Designer.cs` - Designer file
3. `FlashcardStudyForm.resx` - Resources
4. `StudyControl.cs` - Control h?c flashcard
5. `StudyControl.Designer.cs` - Designer file
6. `StudyControl.resx` - Resources
7. `FinishControl.cs` - Control hoàn thành
8. `FinishControl.Designer.cs` - Designer file
9. `FinishControl.resx` - Resources

## T??ng lai phát tri?n

- [ ] L?u ti?n trình h?c t?p vào database
- [ ] Thêm spaced repetition algorithm (SM-2)
- [ ] Th?ng kê s? l?n h?c m?i th?
- [ ] ?ánh d?u th? khó/d?
- [ ] Ch? ?? shuffle cards
- [ ] Hi?u ?ng 3D flip animation
- [ ] H? tr? hình ?nh và audio
- [ ] Ch? ?? thi ??u v?i timer
