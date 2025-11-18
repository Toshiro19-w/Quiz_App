# Teacher Dashboard - MyCoursesControl & MyFlashcardsControl

## T?ng quan h? th?ng

H? th?ng qu?n lý n?i dung gi?ng viên bao g?m 2 modules chính:
1. **MyCoursesControl** - Qu?n lý khóa h?c
2. **MyFlashcardsControl** - Qu?n lý b? flashcard

## Navigation Flow

```
MainContainer
    ?
    ??? MyCoursesControl
    ?       ?
    ?       ??? btnCreateCourse ? (TODO: Create Course Dialog)
    ?       ??? btnRevenue ? (TODO: Revenue Statistics)
    ?       ??? btnFlashcards ? MyFlashcardsControl ?
    ?       ?
    ?       ??? Per Course Actions:
    ?               ??? View Course ? (TODO: Course Detail)
    ?               ??? Edit Course ? (TODO: Edit Dialog)
    ?               ??? Delete Course ? Confirm & Delete ?
    ?
    ??? MyFlashcardsControl
            ?
            ??? btnCreateFlashcard ? (TODO: Create Flashcard Dialog)
            ??? btnBack ? MyCoursesControl ?
            ?
            ??? Per Flashcard Set Actions:
                    ??? View ? (TODO: Flashcard Set Detail)
                    ??? Study ? (TODO: Study Mode)
                    ??? Edit ? (TODO: Edit Dialog)
                    ??? Delete ? Confirm & Soft Delete ?
```

## So sánh tính n?ng

| Tính n?ng | MyCoursesControl | MyFlashcardsControl |
|-----------|------------------|---------------------|
| **Entity** | Course | FlashcardSet |
| **Query Include** | None | Include(fs => fs.Flashcards) |
| **Soft Delete** | ? Hard Delete | ? Soft Delete (IsDeleted) |
| **Badge 1** | IsPublished (?ã xu?t b?n/Nháp) | Card Count (X th?) |
| **Badge 2** | - | Visibility (Công khai/Riêng t?) |
| **Additional Field 1** | Price (VN?) | Language (vi, en, etc.) |
| **Additional Field 2** | CreatedAt | CreatedAt |
| **Action Buttons** | 3 (View, Edit, Delete) | 4 (View, Study, Edit, Delete) |
| **Header Actions** | 3 buttons | 2 buttons |
| **Back Button** | ? | ? |

## Layout Comparison

### MyCoursesControl Row Layout
```
ID(20) | Title(175-625, 450px) | Status(629) | Price(792) | Date(1017) | Actions(1346-1466)
        ?? 1 label               ?? Badge     ?? 120px    ?? 155px    ?? 3 buttons (View, Edit, Delete)
```

### MyFlashcardsControl Row Layout
```
ID(20) | Title(175-625, 450px) | Count(629) | Visibility(729) | Lang(824) | Date(1017) | Actions(1346-1481)
        ?? 1 label               ?? Badge    ?? Badge         ?? 80px    ?? 155px    ?? 4 buttons (View, Study, Edit, Delete)
```

## Color Schemes

### MyCoursesControl
| Element | Color | Usage |
|---------|-------|-------|
| ?ã xu?t b?n | `#28A745` Green | Status badge |
| Nháp | `#6C757D` Gray | Status badge |
| Create Button | `#3490DC` Blue | Header action |
| Revenue Button | `#28A745` Green | Header action |
| Flashcards Button | `#17A2B8` Cyan | Header action |
| View Button | `ColorPalette.Primary` | Row action |
| Edit Button | `#FFC107` Yellow | Row action |
| Delete Button | `#DC3545` Red | Row action |

### MyFlashcardsControl
| Element | Color | Usage |
|---------|-------|-------|
| Card Count Badge | `#17A2B8` Cyan | Shows number of cards |
| Công khai | `#28A745` Green | Visibility badge |
| Riêng t? | `#6C757D` Gray | Visibility badge |
| Create Button | `#17A2B8` Cyan | Header action |
| Back Button | `#6C757D` Gray | Header action |
| View Button | `ColorPalette.Primary` | Row action |
| Study Button | `#3490DC` Blue | Row action |
| Edit Button | `#FFC107` Yellow | Row action |
| Delete Button | `#DC3545` Red | Row action |

## Code Patterns

### Navigation Pattern
```csharp
private void NavigateToControl(UserControl control)
{
    var form = this.FindForm();
    if (form is MainContainer mainContainer)
    {
        var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
        if (mainPanel != null)
        {
            mainPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(control);
        }
    }
}

private Control FindControlRecursive(Control parent, string name)
{
    foreach (Control c in parent.Controls)
    {
        if (string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase)) 
            return c;
        var found = FindControlRecursive(c, name);
        if (found != null) return found;
    }
    return null;
}
```

### Delete Pattern

**MyCoursesControl - Hard Delete:**
```csharp
private async void DeleteCourse(Course course)
{
    var result = MessageBox.Show(
        $"B?n có ch?c ch?n mu?n xóa khóa h?c '{course.Title}'?...",
        "Xác nh?n xóa",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

    if (result == DialogResult.Yes)
    {
        using var context = new LearningPlatformContext();
        var courseToDelete = await context.Courses.FindAsync(course.CourseId);
        if (courseToDelete != null)
        {
            context.Courses.Remove(courseToDelete); // Hard delete
            await context.SaveChangesAsync();
            ToastHelper.Show(this.FindForm(), "?ã xóa khóa h?c thành công!");
            LoadCourses();
        }
    }
}
```

**MyFlashcardsControl - Soft Delete:**
```csharp
private async void DeleteFlashcardSet(FlashcardSet flashcardSet)
{
    var result = MessageBox.Show(
        $"B?n có ch?c ch?n mu?n xóa b? flashcard '{flashcardSet.Title}'?...",
        "Xác nh?n xóa",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

    if (result == DialogResult.Yes)
    {
        using var context = new LearningPlatformContext();
        var setToDelete = await context.FlashcardSets.FindAsync(flashcardSet.SetId);
        if (setToDelete != null)
        {
            setToDelete.IsDeleted = true; // Soft delete
            await context.SaveChangesAsync();
            ToastHelper.Show(this.FindForm(), "?ã xóa b? flashcard thành công!");
            LoadFlashcardSets();
        }
    }
}
```

### Pagination Pattern (Same for both)
```csharp
private void ApplyFiltersAndLoadPage()
{
    var filtered = _allItems.AsEnumerable();
    
    // Apply search filter
    string searchText = txtSearch.Text.Trim().ToLower();
    if (!string.IsNullOrEmpty(searchText))
    {
        filtered = filtered.Where(item => /* filter logic */);
    }
    
    _totalRecords = filtered.Count();
    
    // Calculate pagination
    int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
    if (_currentPage > totalPages && totalPages > 0)
        _currentPage = totalPages;
    
    var paged = filtered
        .Skip((_currentPage - 1) * _pageSize)
        .Take(_pageSize)
        .ToList();
    
    LoadDataToGrid(paged);
    UpdatePaginationUI(totalPages);
}
```

## Database Queries

### MyCoursesControl
```csharp
_allCourses = await context.Courses
    .Where(c => c.OwnerId == userId.Value)
    .OrderByDescending(c => c.CreatedAt)
    .ToListAsync();
```

### MyFlashcardsControl
```csharp
_allFlashcardSets = await context.FlashcardSets
    .Include(fs => fs.Flashcards)  // ? Include for count
    .Where(fs => fs.OwnerId == userId.Value && !fs.IsDeleted)  // ? Soft delete filter
    .OrderByDescending(fs => fs.CreatedAt)
    .ToListAsync();
```

## Common Features

### ? ?ã implement
1. **Pagination**
   - Page size selector (10, 25, 50, 100)
   - First/Prev/Current/Next/Last buttons
   - Page info display
   - Auto-disable unavailable buttons

2. **Search & Filter**
   - Real-time text search
   - Search by title
   - Search by secondary field (Slug for Courses, Description for Flashcards)

3. **CRUD Operations**
   - **C**reate: Button ready (implementation pending)
   - **R**ead: Full list with pagination ?
   - **U**pdate: Button ready (implementation pending)
   - **D**elete: Full implementation with confirmation ?

4. **UI/UX**
   - Hover effects on rows
   - Color-coded badges
   - Tooltips on action buttons
   - Empty state handling
   - Error handling with try-catch
   - Toast notifications
   - Responsive layout

### ? Ch?a implement
1. Create dialogs/forms
2. Edit dialogs/forms
3. Detail view pages
4. Study mode (Flashcards only)
5. Revenue statistics (Courses only)
6. Export/Import
7. Bulk operations
8. Advanced filtering
9. Sorting options

## File Structure

```
WinFormsApp1/
??? View/
    ??? User/
        ??? Controls/
            ??? MyCoursesControl.cs
            ??? MyCoursesControl.Designer.cs
            ??? MyCoursesControl.resx
            ??? MyFlashcardsControl.cs
            ??? MyFlashcardsControl.Designer.cs
            ??? MyFlashcardsControl.resx
```

## Dependencies

### Shared Dependencies
- `Microsoft.EntityFrameworkCore`
- `WinFormsApp1.Helpers.AuthHelper`
- `WinFormsApp1.Helpers.ColorPalette`
- `WinFormsApp1.Helpers.ToastHelper`
- `WinFormsApp1.Models.EF.LearningPlatformContext`

### Specific Dependencies
**MyCoursesControl:**
- `WinFormsApp1.Models.Entities.Course`

**MyFlashcardsControl:**
- `WinFormsApp1.Models.Entities.FlashcardSet`
- `WinFormsApp1.Models.Entities.Flashcard`

## Build Status
? **MyCoursesControl**: Build successful
? **MyFlashcardsControl**: Build successful
? **Navigation**: Ho?t ??ng hai chi?u
? **No compilation errors**

## Usage

### Accessing from MainContainer
```csharp
// Access MyCoursesControl
private void btnGiangVien_Click(object sender, EventArgs e)
{
    NavigateToControl(new MyCoursesControl());
}
```

### Internal Navigation
```csharp
// From MyCoursesControl to MyFlashcardsControl
BtnFlashcards_Click ? new MyFlashcardsControl()

// From MyFlashcardsControl back to MyCoursesControl
BtnBack_Click ? new MyCoursesControl()
```

## Testing Checklist

### MyCoursesControl
- [ ] Hi?n th? danh sách khóa h?c
- [ ] Tìm ki?m theo title/slug
- [ ] Phân trang ho?t ??ng
- [ ] Delete course v?i confirmation
- [ ] Navigate to MyFlashcardsControl
- [ ] Empty state khi không có data
- [ ] Error handling

### MyFlashcardsControl
- [ ] Hi?n th? danh sách flashcard sets
- [ ] Hi?n th? ?úng s? th?
- [ ] Tìm ki?m theo title/description
- [ ] Phân trang ho?t ??ng
- [ ] Soft delete flashcard set
- [ ] Navigate back to MyCoursesControl
- [ ] Empty state khi không có data
- [ ] Error handling
- [ ] Filter IsDeleted = false

## Next Steps

### High Priority
1. ? Implement MyFlashcardsControl
2. ? Add navigation between controls
3. Implement Create Course dialog
4. Implement Create Flashcard Set dialog
5. Implement Edit dialogs for both

### Medium Priority
6. Implement Course Detail view
7. Implement Flashcard Set Detail view
8. Implement Study Mode for Flashcards
9. Implement Revenue Statistics

### Low Priority
10. Export/Import functionality
11. Bulk operations
12. Advanced filtering
13. Custom sorting
14. Analytics dashboard
