# ?? H? TH?NG G?I Ý KHÓA H?C CÓ TR?NG S?

## ?? Tóm t?t

?ã t?o h? th?ng g?i ý khóa h?c thông minh cho trang ch?, s? d?ng thu?t toán weighted scoring ?? cá nhân hóa tr?i nghi?m h?c t?p.

## ? ?ã hoàn thành

### 1. Backend - Logic Recommendation
- ? **CourseRecommendationService.cs** - Service tính toán ?i?m g?i ý
- ? **CourseController.cs** - Method `GetRecommendedCoursesAsync()`

### 2. UI Components
- ? **CourseRecommendationHelper.cs** - Helper t?o card UI
- ? **HomeControl.Designer.cs** - Thêm section "G?i ý dành cho b?n"

### 3. Documentation
- ? **COURSE_RECOMMENDATION_GUIDE.md** - H??ng d?n t?ng quan
- ? **HOMECONTROL_INTEGRATION_STEPS.txt** - B??c tích h?p chi ti?t
- ? **RECOMMENDATION_EXAMPLES.md** - Ví d? và test cases

## ?? CÁCH S? D?NG

### B??c 1: M? file HomeControl.cs
Location: `WinFormsApp1\View\User\Controls\HomeControl.cs`

### B??c 2: Làm theo file h??ng d?n
??c file: `Docs\HOMECONTROL_INTEGRATION_STEPS.txt`

Tóm t?t các thay ??i c?n làm:
1. Thêm using: `using WinFormsApp1.Controllers;`
2. Thêm field: `private readonly CourseController _courseController;`
3. Kh?i t?o trong constructor: `_courseController = new CourseController();`
4. G?i trong Load: `LoadRecommendedCourses();`
5. Thêm 2 methods: `LoadRecommendedCourses()` và `AddToCartAsync()`

### B??c 3: Build & Run
```
Build ? Rebuild Solution
F5 ?? ch?y
```

## ?? Giao di?n

### Hi?n th? trên Trang ch?

```
???????????????????????????????????????????
?  ?? G?i ý dành cho b?n         [Xem t?t c?]?
?  Khóa h?c ???c ch?n riêng d?a trên s? thích?
???????????????????????????????????????????
?  [Card 1]    [Card 2]    [Card 3]       ?
?  • ?nh bìa                               ?
?  • Tags: [Phù h?p l?ch s?] [?ánh giá cao]?
?  • Tiêu ??                               ?
?  • Gi?ng viên                            ?
?  • Rating ?????                         ?
?  • Giá: 299,000?                         ?
?  • [Xem chi ti?t] [??]                   ?
???????????????????????????????????????????
```

## ?? Thu?t toán Scoring

### Công th?c t?ng
```
TotalScore = 
    (HistoryScore × 35%) +      // L?ch s? h?c t?p
    (BehaviorScore × 25%) +     // Hành vi (gi? hàng, xem)
    (PopularityScore × 20%) +   // Rating, s? h?c viên
    (RelevanceScore × 15%) +    // S? thích user
    (TimePriceScore × 5%)       // M?i, giá
```

### Chi ti?t t?ng y?u t?

#### 1?? History Score (35%)
- Cùng danh m?c v?i khóa h?c ?ã mua: **+60 ?i?m**
- Cùng gi?ng viên ?ã h?c: **+40 ?i?m**

#### 2?? Behavior Score (25%)
- ?ã thêm vào gi? hàng: **+100 ?i?m**

#### 3?? Popularity Score (20%)
**S? h?c viên**:
- \>100: +40 | >50: +30 | >20: +20 | >0: +10

**Rating**:
- ?4.5: +40 | ?4.0: +30 | ?3.5: +20 | ?3.0: +10

**S? ?ánh giá**:
- \>50: +20 | >20: +15 | >10: +10 | >0: +5

#### 4?? Relevance Score (15%)
- Thu?c danh m?c s? thích: **+100 ?i?m**

#### 5?? Time & Price Score (5%)
- Khóa h?c m?i ?7 ngày: **+50 ?i?m**
- Khóa h?c m?i ?30 ngày: **+30 ?i?m**

## ??? Recommendation Tags

Các tags hi?n th? trên card g?i ý:

| Tag | ?i?u ki?n |
|-----|-----------|
| ?? **Phù h?p v?i l?ch s? h?c t?p** | HistoryScore > 50 |
| ?? **B?n ?ã thêm vào gi? hàng** | BehaviorScore > 80 |
| ? **?ánh giá cao** | Rating ? 4.5 |
| ?? **Ph? bi?n** | >50 h?c viên |
| ? **M?i ra m?t** | T?o ?7 ngày |
| ?? **Mi?n phí** | Price = 0 |
| ?? **Phù h?p s? thích** | RelevanceScore > 80 |

## ?? C?u trúc Files

```
WinFormsApp1/
??? Services/
?   ??? CourseRecommendationService.cs      ? Service tính ?i?m
??? Helpers/
?   ??? CourseRecommendationHelper.cs       ? Helper t?o UI
??? Controllers/
?   ??? CourseController.cs                 ? ?ã thêm method
??? View/User/Controls/
?   ??? HomeControl.cs                      ? C?N CH?NH S?A
?   ??? HomeControl.Designer.cs             ? ?ã c?p nh?t
??? Docs/
    ??? COURSE_RECOMMENDATION_GUIDE.md      ? H??ng d?n t?ng quan
    ??? HOMECONTROL_INTEGRATION_STEPS.txt   ? Các b??c tích h?p
    ??? RECOMMENDATION_EXAMPLES.md          ? Ví d? & demos
    ??? RECOMMENDATION_README.md            ? File này
```

## ?? Quick Start

### Cho ng??i dùng cu?i

1. ??ng nh?p vào h? th?ng
2. Vào **Trang ch?**
3. Xem section **"?? G?i ý dành cho b?n"**
4. Click vào khóa h?c ?? xem chi ti?t ho?c thêm vào gi?

### Cho developer

**Tích h?p nhanh** (5 phút):
1. M? file `HomeControl.cs`
2. Copy code t? `HOMECONTROL_INTEGRATION_STEPS.txt`
3. Rebuild Solution
4. Run & Test

**Chi ti?t ??y ??**:
- ??c `COURSE_RECOMMENDATION_GUIDE.md`

## ?? Ví d? Th?c t?

### Scenario 1: User h?c l?p trình
```
User ?ã mua:
- "C# C? b?n"
- "SQL C? b?n"

G?i ý:
1. "C# Nâng cao" (Score: 75) - [Phù h?p l?ch s?] [?ánh giá cao]
2. "ASP.NET Core" (Score: 68) - [Phù h?p l?ch s?]
3. "Python cho Data" (Score: 45) - [Ph? bi?n]
```

### Scenario 2: User trong gi? có khóa h?c
```
User ?ã mua: (r?ng)
Gi? hàng: "Excel C? b?n"

G?i ý:
1. "Excel C? b?n" (Score: 85) - [Trong gi? hàng] [Ph? bi?n]
2. "SQL C? b?n" (Score: 55) - [?ánh giá cao] [Ph? bi?n]
```

## ?? Customization

### Thay ??i tr?ng s?

File: `CourseRecommendationService.cs` (line ~64)

```csharp
const decimal HISTORY_WEIGHT = 0.35m;
const decimal BEHAVIOR_WEIGHT = 0.25m;
const decimal POPULARITY_WEIGHT = 0.20m;
const decimal RELEVANCE_WEIGHT = 0.15m;
const decimal TIME_PRICE_WEIGHT = 0.05m;
```

**Ví d?**: T?ng t?m quan tr?ng c?a Popularity

```csharp
const decimal HISTORY_WEIGHT = 0.30m;      // 35% ? 30%
const decimal POPULARITY_WEIGHT = 0.25m;   // 20% ? 25%
```

### Thêm tiêu chí m?i

File: `CourseRecommendationService.cs`

```csharp
// Thêm vào class CourseScore
public decimal NewFactorScore { get; set; }

// Thêm method tính ?i?m m?i
private decimal CalculateNewFactorScore(Course course)
{
    // Logic c?a b?n
    return score;
}

// C?p nh?t CalculateCourseScoreAsync
score.NewFactorScore = CalculateNewFactorScore(course);
score.TotalScore = ... + (score.NewFactorScore * 0.10m);
```

## ?? Metrics & Analytics (Phase 2)

### Tracking User Clicks
```csharp
private async void OnRecommendedCourseClick(int courseId, int userId)
{
    // Log to database
    await _analyticsService.LogInteractionAsync(
        userId, 
        courseId, 
        "RECOMMENDED_CLICK"
    );
}
```

### Measuring Success
- **CTR** = (Clicks on recommended) / (Total impressions)
- **Conversion Rate** = (Purchases from recommended) / (Clicks on recommended)
- **Avg Score** = Average TotalScore of displayed courses

## ?? Troubleshooting

### Problem: Section không hi?n th?
**Solution**: 
- Ki?m tra `flowRecommended.Visible = true` trong Designer
- Check database có khóa h?c published

### Problem: Tags b? ch?ng lên nhau
**Solution**:
```csharp
// ?i?u ch?nh size c?a pnlReasonTags
Size = new Size(310, 50),  // T?ng height t? 35 ? 50
```

### Problem: Loading quá ch?m
**Solution**:
- Enable caching
- Reduce s? l??ng courses t? 6 ? 4
- Optimize database queries v?i indexes

## ?? Tài li?u Tham kh?o

1. **COURSE_RECOMMENDATION_GUIDE.md** - H??ng d?n chi ti?t
2. **HOMECONTROL_INTEGRATION_STEPS.txt** - Các b??c tích h?p
3. **RECOMMENDATION_EXAMPLES.md** - Ví d? và test cases

## ?? Contributors

- AI Assistant (H? th?ng recommendation)
- Developer (Integration & Testing)

## ?? Changelog

### Version 1.0.0 (2024)
- ? T?o CourseRecommendationService
- ? T?o CourseRecommendationHelper
- ? C?p nh?t HomeControl.Designer.cs
- ?? T?o documentation ??y ??

### Version 1.1.0 (Future)
- [ ] Thêm UserCourseInteractions tracking
- [ ] Collaborative filtering
- [ ] A/B testing framework
- [ ] ML-based recommendations

---

**Status**: ? Ready for Integration
**Next Action**: Ch?nh s?a `HomeControl.cs` theo h??ng d?n
