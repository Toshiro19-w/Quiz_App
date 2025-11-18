# ?? H??ng d?n Tính n?ng Thanh toán VietQR

## ?? T?ng quan

?ng d?ng Quiz App WinForms ?ã ???c tích h?p tính n?ng thanh toán b?ng **Mã QR chu?n VietQR**. Ng??i dùng có th? mua khóa h?c tr?c ti?p b?ng cách quét mã QR b?ng ?ng d?ng ngân hàng h? tr? VietQR ho?c các ?ng d?ng h? tr? quét VietQR.

## ?? Tính n?ng chính

### 1. **Nút Mua (?? Mua) trên C?a hàng**
- Nút "?? Mua" ???c ??t ? **bên trái** nút "Xem chi ti?t" 
- Cho phép mua khóa h?c **ngay l?p t?c** mà không c?n thêm vào gi? hàng
- H? tr?:
  - **Khóa h?c mi?n phí**: ??ng ký ngay t?c thì
  - **Khóa h?c tr? phí**: Hi?n th? dialog thanh toán VietQR

### 2. **Dialog Thanh toán VietQR**
- Hi?n th? **Mã QR** cho thanh toán
- Thông tin tài kho?n:
  - **Ch? tài kho?n**: QUIZ APP
  - **S? tài kho?n**: 1025839148
  - **Ngân hàng**: Vietcombank (970415)
- **H?t h?n sau 5 phút**
- Nút "? ?ã thanh toán" ?? xác nh?n

### 3. **X? lý ??n hàng**
Sau khi xác nh?n thanh toán:
- T?o b?n ghi **Order** (??n hàng)
- T?o **OrderItem** (M?c trong ??n hàng)
- T?o b?n ghi **Payment** (Thanh toán)
- T?o **CoursePurchase** (Mua khóa h?c)

## ?? C?u trúc Th? m?c

```
WinFormsApp1/
??? Service/
?   ??? PaymentService/
?       ??? VietQRService.cs          # Service t?o mã QR
??? View/
?   ??? Dialogs/
?   ?   ??? PaymentQRDialog.cs        # Dialog thanh toán
?   ??? User/
?       ??? Controls/
?           ??? CourseControl.cs      # Hi?u ch?nh: thêm nút mua
?           ??? ShopControl.cs        # Hi?u ch?nh: thêm nút mua
??? Models/
    ??? Entities/
        ??? Order.cs
        ??? OrderItem.cs
        ??? Payment.cs
        ??? CoursePurchase.cs
```

## ?? Cách s? d?ng

### T? phía Ng??i dùng

1. **Vào C?a hàng** ? Xem danh sách khóa h?c
2. **Nh?n nút "?? Mua"** trên khóa h?c b?t k?
3. N?u **khóa h?c mi?n phí** ? ??ng ký ngay
4. N?u **khóa h?c tr? phí**:
   - Dialog thanh toán hi?n th?
   - Quét mã QR b?ng app ngân hàng
   - Th?c hi?n chuy?n kho?n
   - Nh?n "? ?ã thanh toán" khi xong

### T? phía L?p trình viên

#### T?o mã QR:
```csharp
// Trong VietQRService
var qrImage = VietQRService.GeneratePaymentQRCode(
    amount: 299000,
    courseName: "SQL C? b?n",
    courseId: 1
);
picBox.Image = qrImage;
```

#### Hi?n th? Dialog:
```csharp
using (var paymentDialog = new PaymentQRDialog(
    courseTitle: "SQL C? b?n",
    amount: 299000,
    courseId: 1))
{
    var result = paymentDialog.ShowDialog();
    if (result == DialogResult.OK)
    {
        // X? lý thanh toán thành công
    }
}
```

## ?? Thông tin VietQR

**VietQR** là tiêu chu?n QR code cho các giao d?ch ngân hàng t?i Vi?t Nam:
- ???c h? tr? b?i t?t c? các ngân hàng Vi?t Nam
- H? tr? trong các ?ng d?ng ngân hàng và d?ch v? thanh toán
- Format chu?n cho chuy?n kho?n t?c thì (Instant Transfer)

### Ví d? QR Content:
```
970415|1025839148|299000|Thanh toan khoa hoc 1 - SQL Co ban
```

## ?? B?o m?t & L?u ý

### Hi?n t?i
- Mã QR ???c t?o t? thông tin **t?nh** (account m?c ??nh)
- ?? **s?n xu?t (Production)**, c?n:
  1. L?y account s? t? **c? s? d? li?u** theo t?ng ng??i bán
  2. Tích h?p v?i **VietQR API** th?c t?
  3. Xác th?c thanh toán qua **Webhook** t? ngân hàng
  4. C?p nh?t tr?ng thái ??n hàng t? ??ng

### C?u hình cho Production
S?a file `VietQRService.cs`:
```csharp
// T? database ho?c config
private const string BankBinCode = "970415";      // L?y t? DB
private const string AccountNo = "1025839148";    // L?y t? DB
private const string AccountNameConst = "QUIZ APP"; // L?y t? DB
```

## ?? C? s? d? li?u

### Tables liên quan:
- `Orders` - ??n hàng
- `OrderItems` - Chi ti?t ??n hàng
- `Payments` - Thanh toán
- `CoursePurchases` - Mua khóa h?c (legacy)

### Truy v?n l?ch s? mua hàng:
```sql
SELECT o.*, p.*, oi.CourseId, c.Title
FROM Orders o
LEFT JOIN Payments p ON o.OrderId = p.OrderId
LEFT JOIN OrderItems oi ON o.OrderId = oi.OrderId
LEFT JOIN Courses c ON oi.CourseId = c.CourseId
WHERE o.BuyerId = @UserId
ORDER BY o.CreatedAt DESC;
```

## ?? Ki?m th?

### Test Case 1: Mua khóa h?c mi?n phí
```
1. Ch?n khóa h?c có Price = 0
2. Nh?n "?? Mua"
3. K? v?ng: ??ng ký ngay, không hi?n th? dialog
```

### Test Case 2: Mua khóa h?c tr? phí
```
1. Ch?n khóa h?c có Price > 0
2. Nh?n "?? Mua"
3. K? v?ng: Dialog hi?n th? mã QR
4. Quét QR b?ng app ngân hàng
5. Nh?n "? ?ã thanh toán"
6. K? v?ng: T?o Order, Payment, CoursePurchase
```

### Test Case 3: Khóa h?c ?ã mua
```
1. Mua khóa h?c (l?n ??u)
2. Nh?n "?? Mua" l?i
3. K? v?ng: Message "B?n ?ã mua khóa h?c này r?i!"
```

## ?? Troubleshooting

### L?i: "L?i khi t?o mã VietQR"
- **Nguyên nhân**: QRCoder library không ???c load ?úng
- **Gi?i pháp**: Ki?m tra package `QRCoder` version 1.7.0

### L?i: "Vui lòng ??ng nh?p"
- **Nguyên nhân**: `AuthHelper.CurrentUser` là null
- **Gi?i pháp**: ??m b?o ng??i dùng ?ã ??ng nh?p tr??c khi nh?n mua

### Dialog không hi?n th?
- **Nguyên nhân**: Form chính ch?a ???c kh?i t?o
- **Gi?i pháp**: Ch?c ch?n `this.FindForm()` tr? v? form h?p l?

## ?? H? tr? & Ti?p theo

### C?i ti?n t??ng lai:
1. [ ] Tích h?p VietQR API th?c t?
2. [ ] Webhook xác th?c thanh toán t? ngân hàng
3. [ ] Support nhi?u ngân hàng/account
4. [ ] QR code ??ng d?a trên Order ID
5. [ ] L?ch s? giao d?ch chi ti?t
6. [ ] Email xác nh?n thanh toán

---

**Phiên b?n**: 1.0.0  
**C?p nh?t l?n cu?i**: 2024  
**Tr?ng thái**: Hoàn thành c? b?n
