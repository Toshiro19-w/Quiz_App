# LessonDetailControl - H??ng d?n s? d?ng

## Mô t?
Component `LessonDetailControl` là m?t UserControl hoàn ch?nh ?? hi?n th? và qu?n lý vi?c h?c m?t bài h?c trong khóa h?c.

## Tính n?ng chính

### 1. **Hi?n th? N?i dung Bài H?c**
- **Video**: H? tr? phát video (c?n c?u hình Windows Media Player)
- **Theory**: Hi?n th? n?i dung lý thuy?t HTML
- **Flashcard**: Luy?n t?p flashcard v?i flip animation
- **Test**: Làm bài ki?m tra tr?c tuy?n

### 2. **Theo dõi Ti?n ??**
- T? ??ng l?u ti?n ?? xem video (m?i 5 giây)
- ?ánh d?u n?i dung ?ã xem/hoàn thành
- Hi?n th? progress bar t?ng th?
- C?p nh?t tr?ng thái realtime

### 3. **Navigation**
- Sidebar hi?n th? danh sách t?t c? bài h?c trong khóa h?c
- Nút ?i?u h??ng: Bài tr??c, Bài sau
- Click vào bài h?c trong sidebar ?? chuy?n

### 4. **Qu?n lý Test**
- Hi?n th? câu h?i MCQ_Single, MCQ_Multi, TrueFalse
- Tính toán ?i?m s? t? ??ng
- L?u k?t qu? vào database
- Hi?n th? th?ng kê sau khi n?p bài

## Cách s? d?ng

### 1. Thêm vào Form/Container

```csharp
// Trong form ho?c control cha
using WinFormsApp1.View.User.Controls;

public partial class MainForm : Form
{
    private LessonDetailControl lessonControl;
    
    public MainForm()
    {
        InitializeComponent();
        
        // T?o instance
        lessonControl = new LessonDetailControl();
        lessonControl.Dock = DockStyle.Fill;
        
        // Thêm vào panel ho?c form
        this.Controls.Add(lessonControl);
    }
    
    // G?i khi ng??i dùng ch?n m?t bài h?c
    public async void OpenLesson(string courseSlug, int lessonId)
    {
        await lessonControl.LoadLessonAsync(courseSlug, lessonId);
    }
}
```

### 2. Load bài h?c

```csharp
// Ví d?: Load bài h?c ??u tiên c?a khóa "sql-co-ban"
await lessonControl.LoadLessonAsync("sql-co-ban", 1);
```

### 3. Integration v?i Navigation

```csharp
// Trong CourseControl ho?c n?i hi?n th? danh sách khóa h?c
private void btnStartLesson_Click(object sender, EventArgs e)
{
    var mainContainer = this.FindForm() as MainContainer;
    if (mainContainer != null)
    {
        var lessonControl = new LessonDetailControl();
        lessonControl.Dock = DockStyle.Fill;
        
        mainContainer.LoadControl(lessonControl);
        lessonControl.LoadLessonAsync("sql-co-ban", 1);
    }
}
```

## C?u trúc Database

### CourseProgress Table
```sql
CREATE TABLE CourseProgress (
    ProgressId INT PRIMARY KEY IDENTITY,
    UserId INT NOT NULL,
    CourseId INT NOT NULL,
    LessonId INT NULL,
    ContentType VARCHAR(20) NOT NULL, -- 'Video', 'Theory', 'FlashcardSet', 'Test'
    ContentId INT NOT NULL,
    IsCompleted BIT NOT NULL DEFAULT 0,
    CompletionAt DATETIME2 NULL,
    LastViewedAt DATETIME2 DEFAULT SYSUTCDATETIME(),
    Score DECIMAL(6,2) NULL,
    DurationSec INT NULL  -- Cho video
);
```

### Lu?ng d? li?u

1. **Video**:
   - Khi phát: Timer t? ??ng l?u progress m?i 5s
   - Khi xem ?? 90%: T? ??ng ?ánh d?u hoàn thành
   - L?u `DurationSec` ?? ti?p t?c t? v? trí c?

2. **Theory**:
   - Khi m?: T? ??ng ?ánh d?u ?ã xem
   - `IsCompleted = true`, `CompletionAt = NOW()`

3. **Flashcard**:
   - Khi xem h?t và click "Hoàn thành": L?u progress
   - `IsCompleted = true`

4. **Test**:
   - Khi n?p bài: T?o `TestAttempt` và `AttemptAnswers`
   - Tính ?i?m t? ??ng (GradingMode = 'Auto')
   - L?u `Score` vào CourseProgress

## Customization

### Thay ??i màu s?c

```csharp
// Trong InitializeComponent ho?c constructor
pnlVideo.BackColor = Color.Black;
btnFlipCard.BackColor = Color.FromArgb(52, 144, 220);
```

### Thay ??i timer interval

```csharp
private void SetupVideoProgressTimer()
{
    _videoProgressTimer = new System.Windows.Forms.Timer();
    _videoProgressTimer.Interval = 10000; // 10 giây thay vì 5
    _videoProgressTimer.Tick += VideoProgressTimer_Tick;
}
```

### Disable auto-complete cho Theory

```csharp
private async Task LoadTheoryContentAsync(LessonContent content)
{
    pnlTheory.Visible = true;
    // ... HTML code ...
    webBrowser.DocumentText = html;
    
    // Comment dòng này ?? không t? ??ng ?ánh d?u
    // await MarkContentViewedAsync(content.ContentId);
}
```

## Troubleshooting

### 1. Windows Media Player không ho?t ??ng

**Gi?i pháp t?m th?i**: Component hi?n s? d?ng Label placeholder
```csharp
// Thay th? b?ng video player khác (VLC, LibVLC, ho?c HTML5)
```

### 2. L?i "User not logged in"

```csharp
// ??m b?o AuthHelper.CurrentUser ?ã ???c set
if (AuthHelper.CurrentUser == null)
{
    MessageBox.Show("Vui lòng ??ng nh?p!");
    return;
}
```

### 3. Progress không c?p nh?t

```csharp
// Ki?m tra connection string trong LearningPlatformContext
// ??m b?o CourseProgresses table t?n t?i
```

## Ví d? hoàn ch?nh

```csharp
// MainContainer.cs
public partial class MainContainer : Form
{
    private LessonDetailControl currentLessonControl;
    
    public void NavigateToLesson(string courseSlug, int lessonId)
    {
        // Xóa control c?
        pnlMainContent.Controls.Clear();
        
        // T?o m?i
        currentLessonControl = new LessonDetailControl
        {
            Dock = DockStyle.Fill
        };
        
        // Thêm vào panel
        pnlMainContent.Controls.Add(currentLessonControl);
        
        // Load lesson
        _ = currentLessonControl.LoadLessonAsync(courseSlug, lessonId);
    }
    
    // G?i t? CourseControl
    public void OnStartCourseClick(Course course)
    {
        using var context = new LearningPlatformContext();
        
        // L?y bài h?c ??u tiên
        var firstLesson = context.Lessons
            .Where(l => l.Chapter.CourseId == course.CourseId)
            .OrderBy(l => l.Chapter.OrderIndex)
            .ThenBy(l => l.OrderIndex)
            .FirstOrDefault();
            
        if (firstLesson != null)
        {
            NavigateToLesson(course.Slug, firstLesson.LessonId);
        }
    }
}
```

## Notes

- C?n cài ??t Windows Media Player ho?c thay th? b?ng video player khác
- ??m b?o user ?ã ??ng nh?p tr??c khi load lesson
- Progress ???c l?u realtime, không c?n click "Hoàn thành" cho video
- Test results ???c tính t? ??ng d?a trên QuestionOptions.IsCorrect

## Future Enhancements

1. Thêm h? tr? video player HTML5
2. Offline mode v?i local storage
3. Discussion/Comments cho m?i bài h?c
4. Note-taking feature
5. Bookmarks/Highlights
6. Speed control cho video
7. Keyboard shortcuts
