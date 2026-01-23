# S?A L?I CONTEXT NAVIGATION

## Thay ??i trong file `MyCoursesControl.cs` (dòng 293-311):

### ? Code C? (GÂY L?I):
```csharp
private async void ViewCourse(Course course)
{
    try
    {
        using var context = new LearningPlatformContext();
        
        var courseWithDetails = await context.Courses
            .Include(c => c.CourseChapters)          // ? L?I ? ?ÂY
                .ThenInclude(ch => ch.Lessons)        // ? Include quá sâu
            .FirstOrDefaultAsync(c => c.CourseId == course.CourseId);

        // ... r?i g?i LoadLessonAsync ? T?o context m?i
        await lessonDetailControl.LoadLessonAsync(courseWithDetails.Slug, firstLesson.LessonId);
    }
}
```

### ? Code M?I (?Ã S?A):
```csharp
private async void ViewCourse(Course course)
{
    try
    {
        // S?A: Ch? l?y lesson ??u tiên, không load toàn b? course structure
        using var context = new LearningPlatformContext();
        
        var firstLesson = await context.Lessons
            .AsNoTracking() // QUAN TR?NG: Tránh tracking
            .Include(l => l.Chapter)
            .Where(l => l.Chapter.CourseId == course.CourseId)
            .OrderBy(l => l.Chapter.OrderIndex)
            .ThenBy(l => l.OrderIndex)
            .FirstOrDefaultAsync();

        if (firstLesson == null)
        {
            MessageBox.Show("Khóa h?c ch?a có bài h?c nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var form = this.FindForm();
        if (form is MainContainer mainContainer)
        {
            var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
            if (mainPanel != null)
            {
                mainPanel.Controls.Clear();

                var lessonDetailControl = new LessonDetailControl();
                lessonDetailControl.Dock = DockStyle.Fill;
                mainPanel.Controls.Add(lessonDetailControl);

                // Truy?n course.Slug thay vì courseWithDetails.Slug
                await lessonDetailControl.LoadLessonAsync(course.Slug, firstLesson.LessonId);
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"L?i khi m? khóa h?c: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

## LÝ DO S?A:

1. **Lo?i b? `Include().ThenInclude()`** ph?c t?p
2. **Thêm `AsNoTracking()`** ?? EF Core không track entity
3. **Ch? l?y lesson ??u tiên** thay vì load toàn b? course structure
4. **Truy?n `course.Slug`** (có s?n t? tham s?) thay vì `courseWithDetails.Slug`

## K?T QU?:
? Không còn xung ??t context  
? T?c ?? nhanh h?n (ch? 1 query ??n gi?n)  
? LoadLessonAsync s? t? load l?i toàn b? c?u trúc course trong context riêng
