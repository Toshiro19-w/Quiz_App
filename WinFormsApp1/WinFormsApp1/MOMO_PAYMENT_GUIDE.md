# Hướng dẫn Thanh toán MoMo với Polling

## Luồng hoạt động

### 1. Người dùng nhấn nút "Thanh toán MoMo" trong giỏ hàng
- File: `frmCheckout.cs` → `btnThanhToanMoMo_Click`
- Gọi: `MoMoPaymentHelper.PayCartAsync(userId, this)`

### 2. Tạo đơn hàng và gọi MoMo API
- File: `CartPaymentService.cs` → `PayCartWithMoMoAsync`
- Tạo Order, OrderItems, CoursePurchases (status: Pending)
- Gọi MoMo API để lấy URL thanh toán
- Trả về PaymentUrl và OrderId

### 3. Hiển thị form thanh toán
- File: `MoMoPaymentForm.cs`
- Mở trình duyệt với URL thanh toán MoMo
- Bắt đầu polling để kiểm tra trạng thái

### 4. Polling kiểm tra trạng thái (mỗi 10 giây)
- File: `MoMoPaymentForm.cs` → `StartPaymentStatusCheckAsync`
- Gọi: `MoMoPaymentService.QueryPaymentStatusAsync(orderId)`
- Kiểm tra 30 lần (5 phút)
- Nếu SUCCESS → Cập nhật DB và đóng form
- Nếu FAILED → Hiển thị lỗi
- Nếu hết thời gian → Thông báo timeout

### 5. Hoàn tất thanh toán
- File: `CartPaymentService.cs` → `CompletePaymentAsync`
- Cập nhật Payment status → "Paid"
- Cập nhật Order status → "Paid"
- Cập nhật CoursePurchases status → "Paid"
- Xóa giỏ hàng

## Các file liên quan

1. **MoMoPaymentHelper.cs** - Helper để gọi thanh toán
2. **MoMoPaymentService.cs** - Service gọi MoMo API
3. **CartPaymentService.cs** - Service xử lý đơn hàng
4. **MoMoPaymentForm.cs** - Form hiển thị và polling
5. **frmCheckout.cs** - Form giỏ hàng với nút MoMo

## Cấu hình MoMo Test

```csharp
PartnerCode: "MOMO"
AccessKey: "F8BBA842ECF85"
SecretKey: "K951B6PE1waDMi640xX08PD3vg6EkVlz"
Endpoint: "https://test-payment.momo.vn/v2/gateway/api/create"
QueryEndpoint: "https://test-payment.momo.vn/v2/gateway/api/query"
```

## Polling Configuration

- Interval: 5 giây
- Max attempts: 30 lần
- Total timeout: 2.5 phút
- ResultCode check: 0 (SUCCESS) / 1006, 1000 (FAILED)

## Xử lý khi MoMo Test không trả về kết quả

### Cách 1: Nút "Đã thanh toán" trong form
- Sau khi thanh toán thành công trên MoMo
- Nhấn nút "Đã thanh toán" để xác nhận thủ công
- Hệ thống sẽ cập nhật trạng thái ngay lập tức

### Cách 2: Sử dụng MoMoTestHelper
```csharp
// Test query trạng thái
var result = await MoMoTestHelper.TestQueryPaymentAsync("CART_123_...");
MessageBox.Show(result);

// Hoàn tất thủ công
var success = await MoMoTestHelper.ManualCompletePaymentAsync("CART_123_...");
```

## Debug Logging

- Mở Output window trong Visual Studio (View > Output)
- Chọn "Debug" trong dropdown
- Xem log: "MoMo Query Response", "Poll X: resultCode=..."
