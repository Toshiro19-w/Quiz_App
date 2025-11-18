# Hướng dẫn Thanh toán VietQR

## Tổng quan
Hệ thống đã được cập nhật để hỗ trợ thanh toán trực tiếp bằng mã QR VietQR, thay thế cho luồng giỏ hàng truyền thống.

## Các thành phần chính

### 1. VietQRService
- **File**: `Service/PaymentService/VietQRService.cs`
- **Chức năng**: Tạo mã QR theo chuẩn VietQR của Việt Nam
- **Phương thức chính**:
  - `GenerateVietQRCode()`: Tạo mã QR thanh toán
  - `GeneratePaymentQRCode()`: Tạo QR cho khóa học cụ thể

### 2. VietQRConfig
- **File**: `Service/PaymentService/VietQRConfig.cs`
- **Chức năng**: Cấu hình thông tin ngân hàng
- **Cài đặt**:
  - Mã ngân hàng: 970415 (Vietcombank)
  - Số tài khoản: 1025839148
  - Tên tài khoản: QUIZ APP

### 3. PaymentQRDialog
- **File**: `View/Dialogs/PaymentQRDialog.cs`
- **Chức năng**: Hiển thị mã QR và thông tin thanh toán
- **Tính năng**:
  - Hiển thị mã QR VietQR
  - Đếm ngược thời gian (5 phút)
  - Thông tin tài khoản ngân hàng
  - Xác nhận thanh toán

### 4. PaymentForm
- **File**: `View/User/Forms/PaymentForm.cs`
- **Chức năng**: Form thanh toán chính thay thế giỏ hàng
- **Tính năng**:
  - Hiển thị thông tin khóa học
  - Tổng kết thanh toán
  - Mở PaymentQRDialog

### 5. PaymentProcessingService
- **File**: `Service/PaymentService/PaymentProcessingService.cs`
- **Chức năng**: Xử lý logic thanh toán và cập nhật database
- **Phương thức chính**:
  - `ProcessCoursePaymentAsync()`: Xử lý thanh toán khóa học
  - `HasUserPurchasedCourseAsync()`: Kiểm tra trạng thái mua

## Luồng thanh toán mới

### 1. Người dùng xem khóa học
- Truy cập trang chi tiết khóa học
- Thấy 2 nút: "Mua ngay" và "♥ Mua khóa học"

### 2. Bấm nút mua
- Kiểm tra đăng nhập
- Mở PaymentForm trực tiếp (không qua giỏ hàng)

### 3. Thanh toán
- Hiển thị thông tin khóa học và giá
- Bấm "Thanh toán bằng VietQR"
- Mở PaymentQRDialog với mã QR

### 4. Quét mã QR
- Người dùng quét mã QR bằng app ngân hàng
- Thực hiện chuyển khoản
- Bấm "Đã thanh toán" trong dialog

### 5. Xử lý thanh toán
- Hệ thống tạo Order, OrderItem, CoursePurchase, Payment
- Cập nhật trạng thái trong database
- Hiển thị thông báo thành công

## Cấu hình VietQR

### Thay đổi thông tin ngân hàng
Chỉnh sửa file `VietQRConfig.cs`:

```csharp
public static string BankBinCode { get; set; } = "970415"; // Mã ngân hàng
public static string AccountNumber { get; set; } = "1025839148"; // Số tài khoản
public static string AccountName { get; set; } = "QUIZ APP"; // Tên tài khoản
public static string BankName { get; set; } = "Vietcombank"; // Tên ngân hàng
```

### Danh sách mã ngân hàng phổ biến
- Vietcombank: 970415
- VietinBank: 970415
- BIDV: 970418
- Agribank: 970405
- Techcombank: 970407
- MBBank: 970422
- VPBank: 970432

## Cơ sở dữ liệu

### Bảng được cập nhật
1. **Orders**: Đơn hàng
2. **OrderItems**: Chi tiết đơn hàng
3. **CoursePurchases**: Lịch sử mua khóa học
4. **Payments**: Thông tin thanh toán

### Trạng thái thanh toán
- `Status = "Paid"`: Đã thanh toán thành công
- `PaymentMethod = "VietQR"`: Phương thức thanh toán

## Lưu ý kỹ thuật

### 1. Transaction Safety
- Sử dụng database transaction để đảm bảo tính nhất quán
- Rollback nếu có lỗi trong quá trình xử lý

### 2. Error Handling
- Xử lý lỗi khi tạo mã QR
- Hiển thị thông tin fallback nếu không tạo được QR
- Timeout thanh toán sau 5 phút

### 3. Security
- Validate thông tin khóa học và người dùng
- Kiểm tra trùng lặp mua khóa học
- Tạo mã giao dịch unique

## Kiểm thử

### 1. Test Cases
- Thanh toán thành công
- Timeout thanh toán
- Hủy thanh toán
- Lỗi tạo QR
- Trùng lặp mua khóa học

### 2. UI Testing
- Hiển thị đúng thông tin khóa học
- Mã QR hiển thị rõ ràng
- Đếm ngược thời gian chính xác
- Cập nhật trạng thái nút sau thanh toán

## Tương lai

### Tính năng có thể mở rộng
1. Tích hợp webhook từ ngân hàng để tự động xác nhận
2. Lưu lịch sử giao dịch chi tiết
3. Hỗ trợ nhiều phương thức thanh toán khác
4. Tính năng hoàn tiền
5. Báo cáo doanh thu theo thời gian thực