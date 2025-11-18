# RevenueControl - Th?ng kê doanh thu cho Gi?ng viên

## T?ng quan
`RevenueControl` là trang th?ng kê doanh thu chi ti?t cho gi?ng viên, hi?n th? thu nh?p t? các khóa h?c ?ã bán v?i phân chia 60% cho gi?ng viên và 40% phí n?n t?ng.

## C?u trúc giao di?n

### 1. **Header (pnlHeader)**
- Tiêu ??: ?? Th?ng kê doanh thu
- Nút **Quay l?i** - Navigate v? MyCoursesControl

### 2. **T?ng quan (pnlOverview)** - 4 Card th?ng kê
```
?????????????????????????????????????????????????????????????????????
?  ??           ?  ??           ?  ??           ?  ??           ?
?  T?ng s? l??t  ?  T?ng doanh thu?  Thu nh?p (60%)?  Phí n?n (40%) ?
?  mua          ?                ?                ?                ?
?  0            ?  0 VN?         ?  0 VN?         ?  0 VN?         ?
?????????????????????????????????????????????????????????????????????
```

**Chi ti?t 4 cards:**
| Card | Icon | Màu ch? | D? li?u | Mô t? |
|------|------|---------|---------|-------|
| L??t mua | ?? | Blue #3490DC | TotalPurchases | T?ng s? l??t mua khóa h?c |
| T?ng doanh thu | ?? | Cyan #17A2B8 | TotalGrossRevenue | 100% doanh thu |
| Thu nh?p 60% | ?? | Green #28A745 | TotalInstructorRevenue | Ph?n c?a gi?ng viên |
| Phí n?n t?ng 40% | ?? | Yellow #FFC107 | TotalPlatformFee | Ph?n c?a n?n t?ng |

### 3. **B? l?c (pnlFilters)**
- Ch?n s? l??ng hi?n th?: 10, 25, 50, 100
- Thanh tìm ki?m theo tên khóa h?c

### 4. **B?ng chi ti?t doanh thu theo khóa h?c**

| C?t | Position | Width | Màu | Mô t? |
|-----|----------|-------|-----|-------|
| Khóa h?c | 20 | 400px | TextPrimary | Tên khóa h?c + ID |
| Giá | 440 | 150px | TextPrimary | Giá niêm y?t |
| S? l??t mua | 620 | 60px | Blue #3490DC | Badge s? l??ng |
| T?ng doanh thu | 710 | 180px | Cyan #17A2B8 | 100% |
| Thu nh?p (60%) | 920 | 180px | Green #28A745 | Ph?n gi?ng viên |
| Phí n?n t?ng (40%) | 1130 | 180px | Yellow #FFC107 | Ph?n n?n t?ng |

### 5. **Phân trang (pnlFooter)**
- Thông tin hi?n th?: "Hi?n th? X t?i Y c?a Z d? li?u"
- ?i?u h??ng: ??u tiên | Tr??c | [Trang] | Sau | Cu?i cùng

## Database Queries

### Query T?ng quan
```csharp
var overview = await context.CoursePurchases
    .Include(cp => cp.Course)
    .Where(cp => cp.Course.OwnerId == userId.Value && cp.Status == "Paid")
    .GroupBy(cp => 1)
    .Select(g => new RevenueOverviewViewModel
    {
        TotalPurchases = g.Count(),
        TotalGrossRevenue = g.Sum(cp => cp.PricePaid),
        TotalInstructorRevenue = g.Sum(cp => cp.PricePaid * 0.6m),
        TotalPlatformFee = g.Sum(cp => cp.PricePaid * 0.4m)
    })
    .FirstOrDefaultAsync();
```

### Query Chi ti?t theo khóa h?c
```csharp
var revenues = await context.Courses
    .Where(c => c.OwnerId == userId.Value)
    .Select(c => new CourseRevenueViewModel
    {
        CourseId = c.CourseId,
        CourseTitle = c.Title,
        CoursePrice = c.Price,
        TotalPurchases = c.CoursePurchases.Count(cp => cp.Status == "Paid"),
        GrossRevenue = c.CoursePurchases
            .Where(cp => cp.Status == "Paid")
            .Sum(cp => (decimal?)cp.PricePaid) ?? 0,
        InstructorRevenue = c.CoursePurchases
            .Where(cp => cp.Status == "Paid")
            .Sum(cp => (decimal?)(cp.PricePaid * 0.6m)) ?? 0,
        PlatformFee = c.CoursePurchases
            .Where(cp => cp.Status == "Paid")
            .Sum(cp => (decimal?)(cp.PricePaid * 0.4m)) ?? 0
    })
    .OrderByDescending(r => r.InstructorRevenue)
    .ToListAsync();
```

## ViewModels

### CourseRevenueViewModel
```csharp
public class CourseRevenueViewModel
{
    public int CourseId { get; set; }
    public string CourseTitle { get; set; }
    public decimal CoursePrice { get; set; }
    public int TotalPurchases { get; set; }
    public decimal GrossRevenue { get; set; }        // 100%
    public decimal InstructorRevenue { get; set; }   // 60%
    public decimal PlatformFee { get; set; }         // 40%
}
```

### RevenueOverviewViewModel
```csharp
public class RevenueOverviewViewModel
{
    public int TotalPurchases { get; set; }
    public decimal TotalGrossRevenue { get; set; }
    public decimal TotalInstructorRevenue { get; set; }
    public decimal TotalPlatformFee { get; set; }
}
```

## Phân chia doanh thu

```
Giá khóa h?c: 299,000 VN?
?? Thu nh?p gi?ng viên (60%): 179,400 VN?
?? Phí n?n t?ng (40%): 119,600 VN?
```

### Công th?c tính
```csharp
GrossRevenue = SUM(PricePaid)
InstructorRevenue = SUM(PricePaid * 0.6)
PlatformFee = SUM(PricePaid * 0.4)
```

## Navigation

### Vào RevenueControl t? MyCoursesControl
```csharp
private void BtnRevenue_Click(object sender, EventArgs e)
{
    var form = this.FindForm();
    if (form is MainContainer mainContainer)
    {
        var mainPanel = FindControlRecursive(mainContainer, "mainContentPanel") as Panel;
        if (mainPanel != null)
        {
            mainPanel.Controls.Clear();
            var revenueControl = new RevenueControl();
            revenueControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(revenueControl);
        }
    }
}
```

### Quay l?i MyCoursesControl
```csharp
private void BtnBack_Click(object sender, EventArgs e)
{
    // Navigate back to MyCoursesControl
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

## Màu s?c

### Overview Cards
- **Total Purchases**: #3490DC (Blue)
- **Gross Revenue**: #17A2B8 (Cyan)
- **Instructor Revenue**: #28A745 (Green)
- **Platform Fee**: #FFC107 (Yellow)

### Table Columns
- **Purchases Badge**: #3490DC (Blue), White text
- **Gross Revenue**: #17A2B8 (Cyan), Bold
- **Instructor Revenue**: #28A745 (Green), Bold
- **Platform Fee**: #FFC107 (Yellow)

### Buttons
- **Back Button**: #6C757D (Gray)
- **Pagination Buttons**: #6C757D (Gray)
- **Current Page**: #3490DC (Blue)

## Tính n?ng

### ? ?ã implement
1. **Th?ng kê t?ng quan**
   - T?ng l??t mua
   - T?ng doanh thu (100%)
   - Thu nh?p gi?ng viên (60%)
   - Phí n?n t?ng (40%)

2. **B?ng chi ti?t theo khóa h?c**
   - Hi?n th? t?t c? khóa h?c c?a gi?ng viên
   - S?p x?p theo thu nh?p gi?ng viên (gi?m d?n)
   - Hi?n th? ??y ?? thông tin doanh thu

3. **Search & Filter**
   - Tìm ki?m theo tên khóa h?c
   - Ch?n s? l??ng hi?n th?/trang
   - Real-time search

4. **Pagination**
   - Phân trang ??y ??
   - Hi?n th? thông tin trang hi?n t?i
   - Auto-disable buttons không kh? d?ng

5. **Data Loading**
   - Async loading
   - Error handling
   - Empty state

6. **Navigation**
   - Quay l?i MyCoursesControl
   - Navigation helper method

### ? Ch?a implement
1. Bi?u ?? doanh thu (Chart)
2. Filter theo kho?ng th?i gian
3. Export doanh thu (Excel/PDF)
4. Th?ng kê theo tháng/n?m
5. So sánh doanh thu gi?a các khóa h?c
6. Trend analysis
7. Revenue forecasting

## Database Tables s? d?ng

### Courses
```sql
CourseId, OwnerId, Title, Price, IsPublished, CreatedAt
```

### CoursePurchases
```sql
PurchaseId, CourseId, BuyerId, PricePaid, Currency, Status, PurchasedAt
```

### Users
```sql
UserId, Username, Email, FullName, RoleId
```

## Filter Conditions

```csharp
// Only count paid purchases
WHERE cp.Status == "Paid"

// Only instructor's courses
WHERE c.OwnerId == userId.Value

// Order by instructor revenue
ORDER BY InstructorRevenue DESC
```

## State Management

```csharp
private int _currentPage = 1;
private int _pageSize = 10;
private int _totalRecords = 0;
private List<CourseRevenueViewModel> _allRevenues;
private RevenueOverviewViewModel _overview;
```

## File Structure

```
WinFormsApp1/
??? Models/
?   ??? ViewModels/
?       ??? CourseRevenueViewModel.cs
??? View/
    ??? User/
        ??? Controls/
            ??? RevenueControl.cs
            ??? RevenueControl.Designer.cs
            ??? RevenueControl.resx
```

## Dependencies

- `Microsoft.EntityFrameworkCore`
- `WinFormsApp1.Helpers.AuthHelper`
- `WinFormsApp1.Helpers.ColorPalette`
- `WinFormsApp1.Models.EF.LearningPlatformContext`
- `WinFormsApp1.Models.ViewModels.CourseRevenueViewModel`
- `WinFormsApp1.Models.Entities.Course`
- `WinFormsApp1.Models.Entities.CoursePurchase`

## Build Status
? Build successful
? No compilation errors
? Navigation working
? Data loading from database

## Layout Dimensions

### Overview Section
- Height: 140px
- 4 cards: 310px width each
- Spacing: 30px between cards
- Total width: 1340px

### Table Section
- Header height: 60px
- Row height: 70px
- Total width: 1340px

### Footer Section
- Height: 80px
- Pagination width: 420px

## Usage Example

```csharp
// From MyCoursesControl
BtnRevenue_Click ? new RevenueControl()

// From RevenueControl
BtnBack_Click ? new MyCoursesControl()
```

## Screenshots Layout

```
???????????????????????????????????????????????????????????????????????
?  ?? Th?ng kê doanh thu                               [?? Quay l?i]  ?
???????????????????????????????????????????????????????????????????????
?  ?? 0          ?? 0 VN?        ?? 0 VN?        ?? 0 VN?              ?
?  T?ng l??t mua  T?ng doanh thu  Thu nh?p (60%)  Phí n?n t?ng (40%) ?
???????????????????????????????????????????????????????????????????????
?  Hi?n th? [10 ?] d? li?u                  Tìm ki?m: [____________]?
??????????????????????????????????????????????????????????????????????
?Khóa h?c  ? Giá   ?L??t  ?T?ng doanh ?Thu nh?p    ?Phí n?n t?ng     ?
?          ?       ?mua   ?thu        ?(60%)       ?(40%)            ?
??????????????????????????????????????????????????????????????????????
?SQL C? b?n?199,000?  0   ?0 VN?      ?0 VN?       ?0 VN?            ?
?ID: 1     ?       ?      ?           ?            ?                 ?
??????????????????????????????????????????????????????????????????????
  Hi?n th? 1 t?i 4 c?a 4 d? li?u    [??u][Tr??c][1][Sau][Cu?i]
```

## Testing Checklist

- [ ] Load revenue overview correctly
- [ ] Load detailed revenue per course
- [ ] Calculate 60/40 split correctly
- [ ] Search by course title
- [ ] Pagination works
- [ ] Navigate back to MyCoursesControl
- [ ] Handle zero purchases
- [ ] Handle no courses
- [ ] Error handling
- [ ] Empty state display

## Business Logic

### Revenue Calculation
```
For each purchase:
- Gross Revenue = PricePaid
- Instructor gets 60% = PricePaid * 0.6
- Platform gets 40% = PricePaid * 0.4

Total:
- Sum all purchases where Status = "Paid"
- Group by Course to get per-course statistics
- Aggregate all for overview statistics
```

### Example Calculation
```
Khóa h?c SQL: 199,000 VN?
- S? l??t mua: 3
- T?ng doanh thu: 597,000 VN?
- Thu nh?p gi?ng viên: 358,200 VN? (60%)
- Phí n?n t?ng: 238,800 VN? (40%)
```
