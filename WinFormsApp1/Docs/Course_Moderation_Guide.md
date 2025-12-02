# HỆ THỐNG KIỂM DUYỆT KHÓA HỌC

## Tổng quan

Hệ thống kiểm duyệt khóa học cho phép Admin kiểm soát chất lượng các khóa học trước khi xuất bản công khai. Hệ thống bao gồm:

1. **Kiểm tra tự động**: Tự động đánh giá các tiêu chí cơ bản của khóa học
2. **Kiểm duyệt thủ công**: Admin xem xét và quyết định cuối cùng
3. **Workflow rõ ràng**: Trạng thái rõ ràng cho từng giai đoạn kiểm duyệt

## Workflow Kiểm Duyệt

```
[Giảng viên tạo khóa học] 
    ↓
[Hoàn thiện nội dung]
    ↓
[Gửi kiểm duyệt] → [Kiểm tra tự động]
    ↓
[Chờ duyệt (Pending)]
    ↓
┌───────────────────────┬──────────────────┐
│                       │                  │
[Phê duyệt]    [Yêu cầu sửa]    [Từ chối]
    ↓                   ↓                  ↓
[Đã xuất bản]    [Giảng viên sửa]    [Kết thúc]
                        ↓
                [Gửi lại kiểm duyệt]
```

## Các Trạng Thái

### 1. Chưa gửi (Default)
- **Mô tả**: Khóa học vừa tạo, chưa gửi kiểm duyệt
- **Hành động**: Giảng viên có thể chỉnh sửa tự do

### 2. Chờ duyệt (Pending)
- **Mô tả**: Khóa học đã gửi, đang chờ admin xem xét
- **Hành động**: Admin có thể xem và quyết định

### 3. Đã duyệt (Approved)
- **Mô tả**: Admin đã phê duyệt, khóa học được xuất bản
- **Hành động**: Hiển thị công khai cho học viên

### 4. Từ chối (Rejected)
- **Mô tả**: Admin từ chối khóa học
- **Hành động**: Giảng viên xem lý do và có thể tạo khóa học mới

### 5. Cần sửa (NeedsRevision)
- **Mô tả**: Admin yêu cầu giảng viên sửa đổi
- **Hành động**: Giảng viên sửa và gửi lại

## Hướng Dẫn Cho Giảng Viên

### Gửi Khóa Học Để Kiểm Duyệt

1. **Hoàn thiện khóa học**:
   - Đảm bảo có đủ tiêu đề, mô tả
   - Thêm ít nhất 3 chương
   - Mỗi chương có ít nhất 5 bài học
   - Mỗi bài học có nội dung (video, lý thuyết, flashcard, hoặc test)

2. **Kiểm tra trước khi gửi**:
   - Vào "Khóa học của tôi"
   - Nhấn nút "📤 Gửi duyệt" bên cạnh khóa học
   - Xem kết quả kiểm tra tự động

3. **Xem kết quả kiểm tra tự động**:
   - ✅ **Điểm 80-100**: Tốt, sẵn sàng gửi
   - ⚠️ **Điểm 60-79**: Có cảnh báo, nên sửa trước khi gửi
   - ❌ **Điểm dưới 60**: Có lỗi, phải sửa mới gửi được

4. **Xử lý kết quả**:
   - **Lỗi (Error)**: BẮT BUỘC phải sửa mới gửi được
   - **Cảnh báo (Warning)**: Nên sửa nhưng vẫn có thể gửi

### Xem Trạng Thái Kiểm Duyệt

Trong màn hình "Khóa học của tôi", cột "Trạng thái kiểm duyệt" hiển thị:
- 🟡 **Chờ duyệt**: Đang chờ admin xem xét
- 🟢 **Đã duyệt**: Khóa học đã được phê duyệt và xuất bản
- 🔴 **Từ chối**: Khóa học bị từ chối (xem lý do)
- 🟠 **Cần sửa**: Admin yêu cầu sửa đổi (xem yêu cầu)

### Xử Lý Khi Bị Từ Chối hoặc Yêu Cầu Sửa

1. Nhận thông báo qua email (nếu có)
2. Vào "Khóa học của tôi"
3. Xem lý do từ chối hoặc yêu cầu sửa đổi
4. Chỉnh sửa khóa học theo yêu cầu
5. Gửi lại kiểm duyệt

## Hướng Dẫn Cho Admin

### Truy Cập Trang Kiểm Duyệt

1. Đăng nhập với tài khoản Admin
2. Vào **Admin Dashboard**
3. Chọn menu **"Quản lý" → "Kiểm duyệt"**

### Xem Danh Sách Khóa Học Chờ Duyệt

**Bộ lọc**:
- **Trạng thái**: Chọn trạng thái cần xem (mặc định: Chờ duyệt)
- **Tìm kiếm**: Tìm theo tên khóa học, giảng viên

**Thông tin hiển thị**:
- Tên khóa học
- Giảng viên
- Danh mục
- Số chương / Số bài học
- Giá
- **Điểm tự động**: Điểm đánh giá tự động (0-100)
  - 🟢 80-100: Tốt
  - 🟡 60-79: Cảnh báo
  - 🔴 <60: Kém
- Ngày gửi

### Xem Chi Tiết và Kiểm Duyệt

1. **Mở chi tiết**:
   - Double-click vào khóa học
   - Hoặc chọn khóa học và nhấn "Xem chi tiết"

2. **Xem thông tin**:
   - Thông tin cơ bản: Tên, giảng viên, giá, số chương/bài
   - Mô tả đầy đủ
   - **Kết quả kiểm tra tự động**:
     - Điểm tổng
     - Chi tiết từng tiêu chí:
       - ✅ Đạt / ❌ Không đạt
       - Mức độ: 🟢 Info / 🟡 Warning / 🔴 Error

3. **Các hành động**:

   **a) Phê duyệt**:
   - Nhấn nút "Phê duyệt"
   - Xác nhận
   - Khóa học sẽ được xuất bản ngay lập tức

   **b) Yêu cầu sửa đổi**:
   - Nhấn nút "Yêu cầu sửa"
   - Nhập chi tiết yêu cầu sửa đổi
   - Giảng viên sẽ nhận được thông báo

   **c) Từ chối**:
   - Nhấn nút "Từ chối"
   - Nhập lý do từ chối rõ ràng
   - Khóa học sẽ không được xuất bản

### Tiêu Chí Đánh Giá Khóa Học

#### Kiểm Tra Tự Động

1. **Tiêu đề** (Tự động):
   - ✅ Có tiêu đề
   - ✅ Tiêu đề ít nhất 10 ký tự
   - ❌ Lỗi nếu thiếu hoặc quá ngắn

2. **Mô tả** (Tự động):
   - ✅ Có mô tả
   - ✅ Mô tả ít nhất 50 ký tự
   - ❌ Lỗi nếu thiếu hoặc quá ngắn

3. **Ảnh bìa** (Tự động):
   - ⚠️ Cảnh báo nếu thiếu ảnh bìa

4. **Danh mục** (Tự động):
   - ⚠️ Cảnh báo nếu chưa phân loại

5. **Giá** (Tự động):
   - ❌ Lỗi nếu giá âm
   - ✅ OK nếu giá >= 0

6. **Nội dung** (Tự động):
   - ❌ Lỗi nếu không có chương
   - ⚠️ Cảnh báo nếu < 3 chương
   - ❌ Lỗi nếu không có bài học
   - ⚠️ Cảnh báo nếu < 5 bài học
   - ❌ Lỗi nếu bài học không có nội dung

7. **Từ khóa nhạy cảm** (Tự động):
   - ❌ Lỗi nếu phát hiện: "scam", "lừa đảo", "hack", "crack", "cheat"

#### Đánh Giá Thủ Công (Admin quyết định)

8. **Chất lượng nội dung**:
   - Nội dung có chính xác?
   - Ngôn ngữ có phù hợp?
   - Có vi phạm bản quyền không?

9. **Cấu trúc**:
   - Khóa học có logic rõ ràng?
   - Tiến trình học tập hợp lý?

10. **Giá trị**:
    - Giá có phù hợp với nội dung?
    - Có đủ giá trị cho học viên?

## Các Quy Tắc Quan Trọng

### Cho Giảng Viên

1. **KHÔNG** được gửi khóa học khi còn lỗi Error
2. **NÊN** sửa tất cả Warning trước khi gửi
3. **PHẢI** đọc kỹ lý do từ chối/yêu cầu sửa
4. **KHÔNG** spam gửi liên tục (chỉ gửi khi đã sửa xong)

### Cho Admin

1. **PHẢI** xem đầy đủ kết quả kiểm tra tự động
2. **PHẢI** kiểm tra nội dung thực tế (không chỉ dựa vào điểm tự động)
3. **PHẢI** viết rõ lý do khi từ chối hoặc yêu cầu sửa
4. **NÊN** xử lý các khóa học theo thứ tự gửi (ngày cũ trước)
5. **KHÔNG** phê duyệt khóa học có nội dung vi phạm

## Cấu Hình và Tùy Chỉnh

### Thêm Từ Khóa Nhạy Cảm

File: `WinFormsApp1\Services\CourseModerationService.cs`

```csharp
// Dòng ~179
var bannedWords = new[] { "scam", "lừa đảo", "hack", "crack", "cheat" };
```

Thêm từ khóa mới vào mảng `bannedWords`.

### Thay Đổi Điểm Số

File: `WinFormsApp1\Services\CourseModerationService.cs`

```csharp
// Dòng ~195
var score = 100 - (errorCount * 15) - (warningCount * 5);
```

Thay đổi công thức tính điểm:
- Hiện tại: Mỗi Error -15 điểm, mỗi Warning -5 điểm
- Có thể điều chỉnh các hệ số này

### Thay Đổi Tiêu Chí

File: `WinFormsApp1\Services\CourseModerationService.cs`

Method `RunAutoChecks()` (dòng ~34)

Thêm hoặc sửa tiêu chí kiểm tra tại đây.

## Troubleshooting

### Lỗi "Không thể gửi khóa học"

**Nguyên nhân**: Khóa học còn lỗi Error

**Giải pháp**:
1. Xem kết quả kiểm tra tự động
2. Sửa tất cả lỗi Error
3. Thử gửi lại

### Điểm tự động thấp

**Nguyên nhân**: Nhiều Warning hoặc Error

**Giải pháp**:
1. Xem chi tiết từng tiêu chí
2. Sửa theo hướng dẫn
3. Chạy kiểm tra lại

### Admin không thấy khóa học

**Nguyên nhân**: 
- Khóa học chưa gửi kiểm duyệt
- Bộ lọc trạng thái không đúng

**Giải pháp**:
1. Giảng viên: Đảm bảo đã nhấn "Gửi duyệt"
2. Admin: Kiểm tra bộ lọc "Trạng thái" = "Chờ duyệt"

## Liên Hệ và Hỗ Trợ

- Email hỗ trợ: support@example.com
- Tài liệu kỹ thuật: [Link]
- Báo lỗi: [GitHub Issues]

## Changelog

### Version 1.0.0 (2024)
- Phát hành tính năng kiểm duyệt khóa học
- Kiểm tra tự động 9 tiêu chí cơ bản
- Workflow phê duyệt/từ chối/yêu cầu sửa
- Giao diện Admin và Giảng viên
