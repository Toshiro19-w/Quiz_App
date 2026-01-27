# HƯỚNG DẪN TÍCH HỢP TÍNH NĂNG GỢI Ý KHÓA HỌC CÓ TRỌNG SỐ

## Tổng quan

Hệ thống gợi ý khóa học thông minh đã được tạo với các thành phần sau:
- ✅ `CourseRecommendationService.cs` - Logic tính toán điểm và gợi ý
- ✅ `CourseRecommendationHelper.cs` - Helper tạo UI card cho khóa học gợi ý
- ✅ `CourseController.cs` - Đã thêm method `GetRecommendedCoursesAsync()`
- ✅ `HomeControl.Designer.cs` - Đã thêm UI components

## Các file đã tạo

### 1. CourseRecommendationService.cs
**Location**: `WinFormsApp1\Services\CourseRecommendationService.cs`

**Chức năng**:
- Tính toán điểm gợi ý cho từng khóa học dựa trên 5 yếu tố có trọng số
- Trả về danh sách khóa học gợi ý đã được sắp xếp theo điểm

**Công thức tính điểm**:
```
TotalScore = 
    (HistoryScore × 35%) +      // Lịch sử học tập
    (BehaviorScore × 25%) +     // Hành vi người dùng
    (PopularityScore × 20%) +   // Độ phổ biến
    (RelevanceScore × 15%) +    // Mức độ phù hợp
    (TimePriceScore × 5%)       // Thời gian & giá
```

### 2. CourseRecommendationHelper.cs
**Location**: `WinFormsApp1\Helpers\CourseRecommendationHelper.cs`

**Chức năng**:
- Tạo UI card cho khóa học gợi ý
- Hiển thị tags lý do gợi ý (VD: "Phù hợp sở thích", "Đánh giá cao")

### 3. Cập nhật CourseController
**File**: `WinFormsApp1\Controllers\CourseController.cs`

**Method mới**:
```csharp
public async Task<List<RecommendedCourse>> GetRecommendedCoursesAsync(int? userId, int count = 6)
```

## HƯỚNG DẪN TÍCH HỢP VÀO HOMECONTROL

### Bước 1: Mở file HomeControl.cs

File location: `WinFormsApp1\View\User\Controls\HomeControl.cs`

### Bước 2: Thêm vào đầu class

```csharp
public partial class HomeControl : UserControl
{
    private readonly CourseController _courseController;

    public HomeControl()
    {
        InitializeComponent();
        _courseController = new CourseController();  // ← THÊM DÒNG NÀY
    }
```

### Bước 3: Cập nhật HomeControl_Load

Thêm dòng gọi `LoadRecommendedCourses()`:

```csharp
private void HomeControl_Load(object sender, EventArgs e)
{
    SetupWelcomeBanner();
    LoadMotivationImage();
    LoadRecommendedCourses();  // ← THÊM DÒNG NÀY
    LoadData();
    LoadFlashcardSets();
}
```

### Bước 4: Thêm method LoadRecommendedCourses

**COPY ĐOẠN CODE SAU VÀO CUỐI FILE** (trước dấu `}` cuối cùng):

```csharp
private async void LoadRecommendedCourses()
{
    try
    {
        flowRecommended.Controls.Clear();

        var userId = AuthHelper.CurrentUser?.UserId;
        var recommendedCourses = await _courseController.GetRecommendedCoursesAsync(userId, 6);

        if (recommendedCourses == null || !recommendedCourses.Any())
        {
            // Ẩn section nếu không có gợi ý
            flowRecommended.Visible = false;
            lblRecommended.Visible = false;
            lblRecommendedDesc.Visible = false;
            btnViewAllRecommended.Visible = false;
            return;
        }

        // Hiển thị các khóa học gợi ý
        foreach (var recommended in recommendedCourses)
        {
            var card = CourseRecommendationHelper.CreateRecommendedCourseCard(recommended);
            card.Margin = new Padding(10);
            
            // Gắn sự kiện click
            var btnView = card.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "Xem chi tiết");
            if (btnView != null)
            {
                btnView.Click += (s, e) => ShowCourseDetail((int)btnView.Tag);
            }

            var btnAddToCart = card.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "🛒");
            if (btnAddToCart != null)
            {
                btnAddToCart.Click += async (s, e) => await AddToCartAsync((int)btnAddToCart.Tag);
            }

            // Click vào panel để xem chi tiết
            card.Click += (s, e) => ShowCourseDetail(recommended.Course.CourseId);
            var picCover = card.Controls.OfType<PictureBox>().FirstOrDefault();
            if (picCover != null)
            {
                picCover.Click += (s, e) => ShowCourseDetail(recommended.Course.CourseId);
            }

            flowRecommended.Controls.Add(card);
        }
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine($"Error loading recommended courses: {ex.Message}");
        // Ẩn section nếu có lỗi
        flowRecommended.Visible = false;
        lblRecommended.Visible = false;
        lblRecommendedDesc.Visible = false;
        btnViewAllRecommended.Visible = false;
    }
}

private async System.Threading.Tasks.Task AddToCartAsync(int courseId)
{
    var userId = AuthHelper.CurrentUser?.UserId;
    if (!userId.HasValue)
    {
        ToastHelper.Show(this.FindForm(), "Vui lòng đăng nhập để thêm vào giỏ hàng!");
        return;
    }

    try
    {
        using var context = new LearningPlatformContext();
        var cart = await context.ShoppingCarts.FirstOrDefaultAsync(c => c.UserId == userId.Value);
        if (cart == null)
        {
            cart = new Models.Entities.ShoppingCart { UserId = userId.Value, CreatedAt = DateTime.Now };
            context.ShoppingCarts.Add(cart);
            await context.SaveChangesAsync();
        }

        var existing = await context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.CourseId == courseId);
        if (existing == null)
        {
            var item = new Models.Entities.CartItem { CartId = cart.CartId, CourseId = courseId, AddedAt = DateTime.Now };
            context.CartItems.Add(item);
            await context.SaveChangesAsync();
            ToastHelper.Show(this.FindForm(), "Đã thêm khóa học vào giỏ hàng!");
        }
        else
        {
            ToastHelper.Show(this.FindForm(), "Khóa học đã có trong giỏ hàng!");
        }
    }
    catch (Exception ex)
    {
        ToastHelper.Show(this.FindForm(), $"Lỗi: {ex.Message}");
    }
}
```

### Bước 5: Thêm using statements

Thêm vào đầu file HomeControl.cs:

```csharp
using WinFormsApp1.Controllers;
```

## KẾT QUẢ SAU KHI TÍCH HỢP

Trang chủ sẽ hiển thị theo thứ tự:
1. **Welcome Banner** (có sẵn)
2. **Motivation Section** (có sẵn)
3. **🎯 Gợi ý dành cho bạn** (MỚI) - 6 khóa học được chọn riêng
4. **🔥 Khóa học phổ biến** (có sẵn) - 4 khóa học rating cao nhất
5. **📚 Bộ flashcard nên học** (có sẵn)

## CHI TIẾT TÍNH NĂNG GỢI Ý

### Khi người dùng CHƯA đăng nhập
- Hiển thị khóa học dựa trên **Popularity Score** (rating cao, nhiều học viên)
- Ưu tiên khóa học mới ra mắt
- Hiển thị tags: "Phổ biến", "Đánh giá cao", "Mới ra mắt"

### Khi người dùng ĐÃ đăng nhập
Hệ thống tính điểm dựa trên:

#### 1. **Lịch sử Học tập (35%)**
- Ưu tiên khóa học cùng danh mục với khóa học đã mua (+60 điểm)
- Ưu tiên khóa học cùng giảng viên đã học (+40 điểm)
- Tag: "Phù hợp với lịch sử học tập"

#### 2. **Hành vi Người dùng (25%)**
- Khóa học trong giỏ hàng (+100 điểm)
- Tag: "Bạn đã thêm vào giỏ hàng"

#### 3. **Độ phổ biến (20%)**
- Số lượng học viên: >100 (+40), >50 (+30), >20 (+20)
- Rating: ≥4.5 (+40), ≥4.0 (+30), ≥3.5 (+20)
- Số đánh giá: >50 (+20), >20 (+15), >10 (+10)
- Tags: "Phổ biến", "Đánh giá cao"

#### 4. **Mức độ Phù hợp (15%)**
- Khóa học thuộc danh mục sở thích của user (+100 điểm)
- Tag: "Phù hợp sở thích"

#### 5. **Thời gian & Giá (5%)**
- Khóa học mới (<7 ngày: +50, <30 ngày: +30)
- Tags: "Mới ra mắt", "Miễn phí"

## TÙY CHỈNH

### Thay đổi số lượng khóa học hiển thị

File: `HomeControl.cs` → method `LoadRecommendedCourses()`

```csharp
var recommendedCourses = await _courseController.GetRecommendedCoursesAsync(userId, 6);
//                                                                                 ↑
//                                                                        Thay đổi số này
```

### Điều chỉnh trọng số

File: `CourseRecommendationService.cs` → method `CalculateCourseScoreAsync()`

```csharp
const decimal HISTORY_WEIGHT = 0.35m;      // 35%
const decimal BEHAVIOR_WEIGHT = 0.25m;     // 25%
const decimal POPULARITY_WEIGHT = 0.20m;   // 20%
const decimal RELEVANCE_WEIGHT = 0.15m;    // 15%
const decimal TIME_PRICE_WEIGHT = 0.05m;   // 5%
```

### Thay đổi tiêu chí chấm điểm

File: `CourseRecommendationService.cs` 

Chỉnh sửa các methods:
- `CalculateHistoryScoreAsync()` - Điểm lịch sử
- `CalculateBehaviorScoreAsync()` - Điểm hành vi
- `CalculatePopularityScore()` - Điểm phổ biến
- `CalculateRelevanceScoreAsync()` - Điểm phù hợp
- `CalculateTimePriceScore()` - Điểm thời gian/giá

### Thêm/Sửa tags hiển thị

File: `CourseRecommendationService.cs` → method `GenerateRecommendationReasons()`

## TESTING

### Test Case 1: User chưa đăng nhập
- Kết quả mong đợi: Hiển thị khóa học rating cao, phổ biến

### Test Case 2: User mới đăng ký (chưa mua khóa học)
- Kết quả mong đợi: Hiển thị khóa học theo sở thích đã chọn (nếu có) + phổ biến

### Test Case 3: User đã mua 1 khóa học C#
- Kết quả mong đợi: Gợi ý khóa học lập trình khác, khóa học cùng giảng viên

### Test Case 4: User có khóa học trong giỏ hàng
- Kết quả mong đợi: Khóa học đó được ưu tiên cao với tag "Bạn đã thêm vào giỏ hàng"

## NÂNG CAP SAU NÀY (PHASE 2)

### 1. Tracking User Interactions
Tạo bảng `UserCourseInteractions` để lưu:
- ViewedAt: Thời điểm xem khóa học
- Duration: Thời gian xem
- InteractionType: View, Click, AddToCart, Search

```sql
CREATE TABLE UserCourseInteractions (
    InteractionId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    CourseId INT NOT NULL,
    InteractionType VARCHAR(20) NOT NULL,
    DurationSeconds INT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);
```

### 2. Collaborative Filtering
- Tìm user có pattern học tập tương tự
- Gợi ý khóa học họ đã mua mà user chưa có

### 3. Content-Based Filtering
- Phân tích tags, keywords trong mô tả khóa học
- So sánh với khóa học user đã thích

### 4. A/B Testing
- Test nhiều công thức trọng số
- Đo lường CTR và conversion rate

## TROUBLESHOOTING

### Lỗi "flowRecommended does not exist"
- **Nguyên nhân**: Chưa rebuild sau khi sửa Designer.cs
- **Giải pháp**: Build → Rebuild Solution

### Không hiển thị khóa học gợi ý
- **Nguyên nhân**: Không có khóa học published trong DB
- **Giải pháp**: Thêm dữ liệu mẫu vào database

### Tags không hiển thị
- **Nguyên nhân**: ReasonTags rỗng
- **Giải pháp**: Kiểm tra logic trong `GenerateRecommendationReasons()`

## METRICS ĐỂ ĐÁNH GIÁ

Sau khi triển khai, bạn có thể đo lường:
1. **Click-Through Rate (CTR)**: % user click vào khóa học gợi ý
2. **Conversion Rate**: % user mua khóa học từ section gợi ý
3. **Engagement Time**: Thời gian user dừng lại ở card gợi ý

Để tracking, thêm code vào event Click của card:
```csharp
// Log khi user click vào khóa học gợi ý
await LogInteractionAsync(userId, courseId, "RECOMMENDED_CLICK");
```

## HỖ TRỢ

Nếu gặp vấn đề, kiểm tra:
1. Build errors trong Output window
2. Debug console cho exception messages
3. Database có dữ liệu khóa học đã published

---

**Created**: 2024
**Version**: 1.0.0
