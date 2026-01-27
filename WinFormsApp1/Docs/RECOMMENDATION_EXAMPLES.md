# DEMO & EXAMPLES - COURSE RECOMMENDATION SYSTEM

## Ví dụ 1: Kịch bản User Mới

### User Profile
- Tên: Nguyễn Văn A
- Chưa mua khóa học nào
- Chưa có sở thích
- Chưa thêm gì vào giỏ hàng

### Kết quả gợi ý
```
🎯 Gợi ý dành cho bạn

┌────────────────────────┐  ┌────────────────────────┐
│ Khóa học SQL Cơ bản    │  │ Lập trình C# cơ bản    │
│ Trần Minh Khoa         │  │ Trần Minh Khoa         │
│ ★★★★★ 5.0 (2)         │  │ ☆☆☆☆☆ Chưa có đánh giá│
│ 199,000 đ              │  │ 299,000 đ              │
│ [Đánh giá cao]         │  │ [Phổ biến]             │
└────────────────────────┘  └────────────────────────┘
```

**Giải thích**: 
- User mới → Hệ thống gợi ý dựa trên **Popularity** (rating cao, nhiều học viên)
- Score = Popularity (20%) + TimePrice (5%) = ~25-40 điểm

---

## Ví dụ 2: User Đã Mua Khóa Học SQL

### User Profile
- Tên: Nguyễn Thị B
- Đã mua: "Khóa học SQL Cơ bản" (CategoryId: 2 - Cơ sở dữ liệu)
- Chưa có sở thích đăng ký
- Giỏ hàng trống

### Kết quả gợi ý
```
🎯 Gợi ý dành cho bạn

┌────────────────────────────────┐  
│ SQL Server từ cơ bản đến nâng cao│
│ Trần Minh Khoa                 │
│ ★★★★☆ 4.6 (4)                 │
│ 299,000 đ                      │
│ [Phù hợp lịch sử] [Đánh giá cao]│
└────────────────────────────────┘  
```

**Giải thích**:
- User đã học SQL → Gợi ý khóa học khác về **Database** (cùng category)
- Score = History (35%) + Popularity (20%) = ~55-75 điểm
- Tag hiển thị: "Phù hợp với lịch sử học tập"

---

## Ví dụ 3: User Có Khóa Học Trong Giỏ Hàng

### User Profile
- Tên: Trần Văn C
- Đã mua: "Lập trình C# cơ bản"
- Giỏ hàng: "Phân tích dữ liệu với Excel"
- Sở thích: Phân tích dữ liệu (CategoryId: 3)

### Kết quả gợi ý
```
🎯 Gợi ý dành cho bạn

┌────────────────────────────────┐  ┌────────────────────────┐
│ Phân tích dữ liệu với Excel    │  │ SQL Cơ bản             │
│ Trần Minh Khoa                 │  │ Trần Minh Khoa         │
│ ☆☆☆☆☆ Chưa có đánh giá        │  │ ★★★★★ 5.0 (2)         │
│ 149,000 đ                      │  │ 199,000 đ              │
│ [Trong giỏ hàng][Phù hợp sở thích]│[Đánh giá cao][Phổ biến]│
└────────────────────────────────┘  └────────────────────────┘
```

**Giải thích**:
- Khóa học trong giỏ hàng được **ưu tiên cực cao**
- Score = Behavior (25%) + Relevance (15%) + History (~10%) = ~50-80 điểm
- Tags: "Bạn đã thêm vào giỏ hàng", "Phù hợp sở thích"

---

## Ví dụ 4: User "Power User"

### User Profile
- Tên: Lê Thị D
- Đã mua: 5 khóa học (3 về Lập trình, 2 về Database)
- Sở thích: Lập trình, AI, Data
- Giỏ hàng trống
- Giảng viên yêu thích: Trần Minh Khoa

### Kết quả gợi ý
```
🎯 Gợi ý dành cho bạn

┌────────────────────────────────┐  ┌────────────────────────┐
│ Trí tuệ nhân tạo Cơ bản        │  │ Python cho Data        │
│ Trần Minh Khoa                 │  │ Trần Minh Khoa         │
│ ★★★★★ 5.0 (10)                │  │ ★★★★☆ 4.5 (8)         │
│ 399,000 đ                      │  │ 349,000 đ              │
│ [Phù hợp sở thích][Đánh giá cao]│  │ [Phù hợp lịch sử]     │
└────────────────────────────────┘  └────────────────────────┘
```

**Giải thích**:
- User có nhiều dữ liệu → Gợi ý **cực kỳ cá nhân hóa**
- Score = History (35%) + Relevance (15%) + Popularity (20%) = ~70-90 điểm
- Ưu tiên khóa học cùng giảng viên đã theo dõi
- Gợi ý theo sở thích đã đăng ký

---

## Ví dụ 5: So Sánh Score Chi Tiết

### Course A: "SQL Nâng Cao"
**Thông tin**:
- Rating: 4.8/5.0 (50 reviews)
- Học viên: 120 người
- Giảng viên: Trần Minh Khoa
- Category: Database
- Giá: 299,000đ
- Ngày tạo: 2 tháng trước

**User Context** (Nguyễn Thị B):
- Đã mua: "SQL Cơ bản" (cùng category)
- Không có trong giỏ
- Sở thích: Database

**Tính điểm**:
```
HistoryScore    = 60 (cùng category) = 60
BehaviorScore   = 0  (không trong giỏ) = 0
PopularityScore = 40 (>100 học viên) + 40 (rating ≥4.5) + 20 (>50 reviews) = 100
RelevanceScore  = 100 (match sở thích) = 100
TimePriceScore  = 30 (tạo <30 ngày) = 30

TotalScore = (60 × 0.35) + (0 × 0.25) + (100 × 0.20) + (100 × 0.15) + (30 × 0.05)
           = 21 + 0 + 20 + 15 + 1.5
           = 57.5 điểm
```

**Confidence**: Medium
**Tags**: "Phù hợp lịch sử", "Đánh giá cao", "Phổ biến", "Phù hợp sở thích"

---

### Course B: "Excel Cơ bản"
**Thông tin**:
- Rating: 3.5/5.0 (5 reviews)
- Học viên: 15 người
- Giảng viên: Nguyễn Văn X
- Category: Phân tích dữ liệu
- Giá: 99,000đ
- Ngày tạo: 4 tháng trước

**User Context** (cùng Nguyễn Thị B):
- Đã mua: "SQL Cơ bản" (khác category)
- Không có trong giỏ
- Sở thích: Database (không match)

**Tính điểm**:
```
HistoryScore    = 0  (khác category, khác GV) = 0
BehaviorScore   = 0  (không trong giỏ) = 0
PopularityScore = 10 (>0 học viên) + 20 (rating ≥3.5) + 0 (≤10 reviews) = 30
RelevanceScore  = 0  (không match sở thích) = 0
TimePriceScore  = 50 (base score) = 50

TotalScore = (0 × 0.35) + (0 × 0.25) + (30 × 0.20) + (0 × 0.15) + (50 × 0.05)
           = 0 + 0 + 6 + 0 + 2.5
           = 8.5 điểm
```

**Confidence**: Low
**Tags**: Không có tags nổi bật

**Kết luận**: Course A (57.5 điểm) được ưu tiên cao hơn Course B (8.5 điểm)

---

## Ví dụ 6: Khóa Học Trong Giỏ Hàng vs Khóa Học Rating Cao

### Scenario
User có:
- Giỏ hàng: Course X (rating 3.0, 10 học viên)
- Không có lịch sử mua

**Course X** (Trong giỏ hàng):
```
BehaviorScore = 100 (trong giỏ hàng)
PopularityScore = 30
TotalScore = (0 × 0.35) + (100 × 0.25) + (30 × 0.20) + ...
           = 0 + 25 + 6 + ...
           = ~31-35 điểm
```

**Course Y** (Rating cao, 4.9/5.0, 200 học viên):
```
BehaviorScore = 0
PopularityScore = 100
TotalScore = (0 × 0.35) + (0 × 0.25) + (100 × 0.20) + ...
           = 0 + 0 + 20 + ...
           = ~20-25 điểm
```

**Kết quả**: Course X (trong giỏ) được ưu tiên cao hơn nhờ **BehaviorScore**

---

## Test Cases

### Test 1: User Guest (chưa login)
```csharp
await _courseController.GetRecommendedCoursesAsync(null, 6);
// Expected: Top 6 khóa học có popularity score cao nhất
```

### Test 2: User có 1 khóa học
```csharp
// Setup: User #3 đã mua Course #1 (SQL)
await _courseController.GetRecommendedCoursesAsync(3, 6);
// Expected: Ưu tiên khóa học về Database, loại bỏ Course #1
```

### Test 3: User có sở thích
```csharp
// Setup: User #3 có sở thích CategoryId = 4 (AI)
await _courseController.GetRecommendedCoursesAsync(3, 6);
// Expected: Khóa học AI được boost điểm RelevanceScore
```

### Test 4: Khóa học trong giỏ hàng
```csharp
// Setup: User #3 có Course #2 trong giỏ
await _courseController.GetRecommendedCoursesAsync(3, 6);
// Expected: Course #2 xuất hiện đầu tiên với tag "Bạn đã thêm vào giỏ"
```

---

## UI Screenshots (Mô tả)

### Layout Trang Chủ Sau Khi Tích Hợp

```
┌────────────────────────────────────────────────────────┐
│  👤 Chào mừng Nguyễn Văn A trở lại!                    │
└────────────────────────────────────────────────────────┘

┌────────────────────────────────────────────────────────┐
│  🎨 Học những gì bạn có hứng thú                       │
│  Các kỹ năng cho hiện tại và tương lai...              │
│                                          [Ảnh minh họa]│
└────────────────────────────────────────────────────────┘

🎯 Gợi ý dành cho bạn                        [Xem tất cả]
Khóa học được chọn riêng dựa trên sở thích của bạn
┌──────────┐ ┌──────────┐ ┌──────────┐ ┌──────────┐
│ [Ảnh]    │ │ [Ảnh]    │ │ [Ảnh]    │ │ [Ảnh]    │
│ [Tags]   │ │ [Tags]   │ │ [Tags]   │ │ [Tags]   │
│ Title    │ │ Title    │ │ Title    │ │ Title    │
│ GV       │ │ GV       │ │ GV       │ │ GV       │
│ Rating   │ │ Rating   │ │ Rating   │ │ Rating   │
│ Price    │ │ Price    │ │ Price    │ │ Price    │
│[Xem][🛒]│ │[Xem][🛒]│ │[Xem][🛒]│ │[Xem][🛒]│
└──────────┘ └──────────┘ └──────────┘ └──────────┘

🔥 Khóa học phổ biến                         [Xem tất cả]
┌──────────┐ ┌──────────┐ ┌──────────┐ ┌──────────┐
│ [Card]   │ │ [Card]   │ │ [Card]   │ │ [Card]   │
└──────────┘ └──────────┘ └──────────┘ └──────────┘

📚 Bộ flashcard nên học                      [Xem tất cả]
┌──────────┐ ┌──────────┐ ┌──────────┐ ┌──────────┐
│ [Card]   │ │ [Card]   │ │ [Card]   │ │ [Card]   │
└──────────┘ └──────────┘ └──────────┘ └──────────┘
```

---

## Chi Tiết Tags Hiển Thị

### Tags có thể xuất hiện

| Tag | Điều kiện | Màu sắc |
|-----|-----------|---------|
| **Phù hợp với lịch sử học tập** | HistoryScore > 50 | Purple (#7C4DFF) |
| **Bạn đã thêm vào giỏ hàng** | BehaviorScore > 80 | Purple (#7C4DFF) |
| **Đánh giá cao** | AverageRating ≥ 4.5 | Purple (#7C4DFF) |
| **Phổ biến** | >50 học viên | Purple (#7C4DFF) |
| **Mới ra mắt** | Tạo ≤7 ngày | Purple (#7C4DFF) |
| **Miễn phí** | Price = 0 | Purple (#7C4DFF) |
| **Phù hợp sở thích** | RelevanceScore > 80 | Purple (#7C4DFF) |

**Lưu ý**: Mỗi card chỉ hiển thị tối đa **2 tags** để tránh lộn xộn

---

## Performance Metrics

### Kịch bản Load
- **Số khóa học trong DB**: 100
- **User đã mua**: 5 khóa học
- **Thời gian tính toán**: ~200-500ms
- **Memory usage**: ~10-20MB

### Optimization Tips
1. Cache kết quả gợi ý trong 1 ngày
2. Tính điểm offline cho khóa học phổ biến
3. Batch load dữ liệu với `Include()`

---

## Edge Cases & Xử lý

### Case 1: Không có khóa học nào
```csharp
if (recommendedCourses == null || !recommendedCourses.Any())
{
    // Ẩn section "Gợi ý dành cho bạn"
    flowRecommended.Visible = false;
}
```

### Case 2: User đã mua hết khóa học
```csharp
// Service tự động loại bỏ khóa học đã mua
var purchasedCourseIds = await _context.CoursePurchases
    .Where(p => p.BuyerId == userId.Value && p.Status == "Paid")
    .Select(p => p.CourseId)
    .ToListAsync();

allCourses = allCourses.Where(c => !purchasedCourseIds.Contains(c.CourseId)).ToList();
```

### Case 3: Khóa học không có ảnh bìa
```csharp
// Helper tự động thêm icon placeholder 📚
picCover.Controls.Add(new Label
{
    Text = "📚",
    Font = new Font("Segoe UI", 48),
    // ...
});
```

---

## Customization Examples

### Thay đổi màu tag
File: `CourseRecommendationHelper.cs`

```csharp
BackColor = Color.FromArgb(124, 77, 255),  // Purple
// Đổi thành:
BackColor = Color.FromArgb(0, 123, 255),   // Blue
```

### Hiển thị 3 tags thay vì 2
```csharp
foreach (var reason in recommended.ReasonTags.Take(2))  // ← Đổi 2 thành 3
```

### Thêm animation khi hover
```csharp
panel.MouseEnter += (s, e) => 
{
    panel.BackColor = Color.FromArgb(250, 250, 250);
    panel.Scale(new SizeF(1.02f, 1.02f));  // ← Thêm hiệu ứng phóng to
};
```

---

## Integration Checklist

- [x] Tạo `CourseRecommendationService.cs`
- [x] Tạo `CourseRecommendationHelper.cs`
- [x] Cập nhật `CourseController.cs`
- [x] Cập nhật `HomeControl.Designer.cs`
- [ ] **Cập nhật `HomeControl.cs`** ← BẠN CẦN LÀM BƯỚC NÀY
- [ ] Build & Test
- [ ] Deploy

---

## Next Steps (Phase 2)

### 1. Tracking Interactions
```sql
CREATE TABLE UserCourseInteractions (
    InteractionId INT PRIMARY KEY IDENTITY,
    UserId INT NOT NULL,
    CourseId INT NOT NULL,
    InteractionType VARCHAR(20), -- View/Click/AddToCart
    DurationSeconds INT,
    CreatedAt DATETIME2 DEFAULT SYSUTCDATETIME()
);
```

### 2. Machine Learning Integration
- Train model dựa trên interaction history
- Predict user preferences
- Real-time personalization

### 3. A/B Testing Framework
- Test nhiều công thức scoring
- So sánh CTR và conversion rate
- Tự động chọn công thức tốt nhất

---

**Created**: 2024
**Author**: AI Assistant
**Version**: 1.0.0
