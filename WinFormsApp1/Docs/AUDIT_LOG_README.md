# Hệ Thống Ghi Log Hoạt Động (Audit Log System)

## Tổng Quan

Hệ thống Audit Log giúp Admin theo dõi và quản lý tất cả các hoạt động trong hệ thống. Mỗi hành động quan trọng đều được ghi nhận với đầy đủ thông tin về người thực hiện, thời gian, và chi tiết thay đổi.

## Các Tính Năng Chính

### 1. Ghi Log Tự Động

- ✅ **Người dùng**: Đăng nhập, đăng xuất, tạo/sửa/xóa tài khoản
- ✅ **Khóa học**: Tạo, cập nhật, xóa, xuất bản, phê duyệt
- ✅ **Bài kiểm tra**: Tạo, cập nhật, xóa, làm bài, nộp bài
- ✅ **Flashcard**: Tạo, cập nhật, xóa bộ flashcard
- ✅ **Danh mục**: Tạo, cập nhật, xóa
- ✅ **Thanh toán**: Tạo, hoàn tất, hoàn tiền
- ✅ **Hệ thống**: Thay đổi cài đặt, xuất/nhập dữ liệu

### 2. Xem Lịch Sử Hoạt Động

Admin có thể:
- Xem danh sách log với phân trang
- Lọc theo thời gian, người dùng, loại hành động
- Tìm kiếm theo từ khóa
- Xem chi tiết từng log (dữ liệu trước/sau thay đổi)
- Xuất báo cáo (CSV/JSON)

### 3. Thống Kê

- Tổng số log
- Log hôm nay / tuần này / tháng này
- Top người dùng hoạt động
- Biểu đồ log theo ngày/giờ
- Thống kê theo loại hành động

## Cấu Trúc Dữ Liệu

### Bảng AuditLogs

| Cột | Kiểu | Mô tả |
|-----|------|-------|
| AuditId | INT | ID tự tăng |
| UserId | INT | ID người thực hiện (NULL nếu hệ thống) |
| Action | NVARCHAR(100) | Mã hành động |
| EntityType | NVARCHAR(50) | Loại đối tượng |
| EntityId | INT | ID đối tượng |
| Before | NVARCHAR(MAX) | Dữ liệu trước thay đổi (JSON) |
| After | NVARCHAR(MAX) | Dữ liệu sau thay đổi (JSON) |
| CreatedAt | DATETIME2 | Thời gian thực hiện |
| IpAddress | VARCHAR(45) | Địa chỉ IP |

## Các Loại Hành Động (Actions)

### User Actions
- `USER_LOGIN` - Đăng nhập
- `USER_LOGOUT` - Đăng xuất
- `USER_CREATE` - Tạo người dùng
- `USER_UPDATE` - Cập nhật người dùng
- `USER_DELETE` - Xóa người dùng
- `USER_PASSWORD_CHANGE` - Đổi mật khẩu
- `USER_PASSWORD_RESET` - Đặt lại mật khẩu
- `USER_STATUS_CHANGE` - Thay đổi trạng thái
- `USER_ROLE_CHANGE` - Thay đổi vai trò

### Course Actions
- `COURSE_CREATE` - Tạo khóa học
- `COURSE_UPDATE` - Cập nhật khóa học
- `COURSE_DELETE` - Xóa khóa học
- `COURSE_PUBLISH` - Xuất bản khóa học
- `COURSE_APPROVE` - Phê duyệt khóa học
- `COURSE_REJECT` - Từ chối khóa học

### Test Actions
- `TEST_CREATE` - Tạo bài kiểm tra
- `TEST_UPDATE` - Cập nhật bài kiểm tra
- `TEST_DELETE` - Xóa bài kiểm tra
- `TEST_ATTEMPT_START` - Bắt đầu làm bài
- `TEST_ATTEMPT_SUBMIT` - Nộp bài

### Flashcard Actions
- `FLASHCARD_SET_CREATE` - Tạo bộ flashcard
- `FLASHCARD_SET_UPDATE` - Cập nhật bộ flashcard
- `FLASHCARD_SET_DELETE` - Xóa bộ flashcard

### Category Actions
- `CATEGORY_CREATE` - Tạo danh mục
- `CATEGORY_UPDATE` - Cập nhật danh mục
- `CATEGORY_DELETE` - Xóa danh mục

### Payment Actions
- `PAYMENT_CREATE` - Tạo thanh toán
- `PAYMENT_COMPLETE` - Hoàn tất thanh toán
- `PAYMENT_REFUND` - Hoàn tiền

### System Actions
- `SYSTEM_SETTING_UPDATE` - Cập nhật cài đặt
- `DATA_EXPORT` - Xuất dữ liệu
- `DATA_IMPORT` - Nhập dữ liệu

## Các Loại Entity

- `User` - Người dùng
- `Course` - Khóa học
- `CourseChapter` - Chương học
- `Lesson` - Bài học
- `Test` - Bài kiểm tra
- `TestAttempt` - Lượt làm bài
- `FlashcardSet` - Bộ flashcard
- `Category` - Danh mục
- `Payment` - Thanh toán
- `System` - Hệ thống

## Hướng Dẫn Sử Dụng

### 1. Truy Cập Giao Diện

1. Đăng nhập với tài khoản Admin
2. Vào **Admin Dashboard**
3. Menu **Quản lý** → **Lịch sử hoạt động**

### 2. Lọc Log

- **Từ ngày / Đến ngày**: Chọn khoảng thời gian
- **Hành động**: Lọc theo loại hành động cụ thể
- **Loại**: Lọc theo loại đối tượng
- **Tìm kiếm**: Nhập từ khóa bất kỳ

### 3. Xem Chi Tiết Log

Click nút **👁** ở cuối mỗi dòng để xem:
- Thông tin đầy đủ về log
- Dữ liệu trước thay đổi (nếu có)
- Dữ liệu sau thay đổi (nếu có)

### 4. Xuất Báo Cáo

1. Thiết lập bộ lọc (tùy chọn)
2. Click nút **📤 Xuất**
3. Chọn định dạng (CSV hoặc JSON)
4. Lưu file

## Tích Hợp Vào Code

### Sử Dụng AuditHelper

```csharp
using WinFormsApp1.Helpers;

// Ghi log đơn giản
await AuditHelper.LogActionAsync("ACTION_NAME", "EntityType", entityId, "Chi tiết");

// Ghi log với dữ liệu trước/sau
await AuditHelper.LogChangeAsync("ACTION_NAME", "EntityType", entityId, beforeData, afterData);

// Ghi log cho User
await AuditHelper.LogUserCreateAsync(user);
await AuditHelper.LogUserUpdateAsync(beforeUser, afterUser);
await AuditHelper.LogUserDeleteAsync(user);

// Ghi log cho Course
await AuditHelper.LogCourseCreateAsync(course);
await AuditHelper.LogCourseUpdateAsync(beforeCourse, afterCourse);
await AuditHelper.LogCourseDeleteAsync(course);

// Ghi log lỗi
await AuditHelper.LogErrorAsync("ACTION_NAME", "EntityType", entityId, exception);

// Ghi log đăng nhập
await AuditHelper.LogUserLoginAsync(userId, success: true, "Đăng nhập thành công");
```

### Sử Dụng AuditLogService Trực Tiếp

```csharp
using WinFormsApp1.Service;
using WinFormsApp1.Service.IService;

IAuditLogService auditService = new AuditLogService();

// Ghi log
await auditService.LogAsync("ACTION", "EntityType", entityId, before, after, severity, userId);

// Truy vấn log
var filter = new AuditLogFilter
{
    StartDate = DateTime.Now.AddDays(-7),
    EndDate = DateTime.Now,
    Action = "USER_LOGIN",
    PageNumber = 1,
    PageSize = 50
};
var result = await auditService.GetLogsAsync(filter);

// Lấy thống kê
var stats = await auditService.GetStatisticsAsync();
```

## Bảo Mật

- ✅ Chỉ Admin mới truy cập được giao diện Audit Log
- ✅ Mật khẩu không bao giờ được ghi vào log
- ✅ Dữ liệu nhạy cảm được mã hóa (nếu cần)
- ✅ IP Address được ghi nhận để truy vết

## Quản Lý Dữ Liệu

### Xóa Log Cũ

```csharp
// Xóa log cũ hơn 90 ngày
int deletedCount = await auditService.DeleteOldLogsAsync(daysToKeep: 90);
```

### Backup

Nên backup bảng `AuditLogs` định kỳ trước khi xóa log cũ.

## Cấu Trúc File

```
WinFormsApp1/
├── ViewModels/
│   └── AuditLogViewModels.cs          # ViewModels cho Audit Log
├── Service/
│   ├── IService/
│   │   └── IAuditLogService.cs        # Interface
│   └── AuditLogService.cs             # Implementation
├── Controllers/
│   └── Admin/
│       └── AuditLogController.cs      # Controller
├── Helpers/
│   └── AuditHelper.cs                 # Helper class
└── View/
    └── Admin/
        └── AuditLogManagementControl.cs  # UI Control
```

## FAQ

### Q: Log có ảnh hưởng đến hiệu suất không?
A: Việc ghi log được thực hiện bất đồng bộ (async) nên không ảnh hưởng đáng kể đến hiệu suất của ứng dụng.

### Q: Có thể tắt ghi log không?
A: Không nên tắt hoàn toàn. Có thể cấu hình để chỉ ghi log các hành động quan trọng.

### Q: Log chiếm bao nhiêu dung lượng?
A: Mỗi log ~500 bytes. Với 10,000 hành động/ngày → ~5MB/ngày → ~150MB/tháng.

### Q: Làm sao để thêm loại action mới?
A: Thêm constant vào `AuditActions` class trong `AuditLogViewModels.cs` và cập nhật method `GetDisplayName()`.

## Changelog

### v1.0.0 (2024)
- Khởi tạo hệ thống Audit Log
- Ghi log cho User, Course, Test, Flashcard, Category, Payment
- Giao diện quản lý cho Admin
- Xuất báo cáo CSV/JSON
- Thống kê và biểu đồ

## Support

- **Tác giả**: [Tên]
- **Email**: support@example.com
