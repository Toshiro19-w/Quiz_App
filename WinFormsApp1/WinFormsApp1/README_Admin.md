# Admin Dashboard - Quiz Web Application

## Mô tả
Hệ thống quản trị admin cho ứng dụng Quiz Web với giao diện tương tự như trong ảnh thiết kế.

## Tính năng chính

### 1. Dashboard Tổng quan
- Hiển thị thống kê tổng quan hệ thống
- 4 thẻ thống kê chính:
  - Tổng người dùng
  - Tổng khóa học  
  - Tổng bài kiểm tra
  - Tổng doanh thu

### 2. Quản lý Người dùng
- Xem danh sách người dùng
- Thêm người dùng mới
- Sửa thông tin người dùng
- Xóa người dùng

### 3. Quản lý Khóa học
- Xem danh sách khóa học
- Thêm khóa học mới
- Sửa thông tin khóa học
- Xóa khóa học
- Quản lý trạng thái xuất bản

### 4. Quản lý Bài kiểm tra
- Xem danh sách bài kiểm tra
- Thêm bài kiểm tra mới
- Sửa thông tin bài kiểm tra
- Xóa bài kiểm tra

## Cấu trúc Files

### Controllers
- `AdminController.cs` - Controller chính xử lý logic CRUD

### Views
- `AdminDashboard.cs` - Form dashboard chính
- `UserManagementForm.cs` - Form quản lý người dùng
- `CourseManagementForm.cs` - Form quản lý khóa học
- `TestManagementForm.cs` - Form quản lý bài kiểm tra
- `AdminTestForm.cs` - Form test để mở admin dashboard

### Helpers
- `FormLayoutHelper.cs` - Helper tạo stats cards và layout

## Cách sử dụng

1. Chạy ứng dụng
2. Click "Mở Admin Dashboard" trong AdminTestForm
3. Sử dụng sidebar để điều hướng giữa các chức năng:
   - Dashboard: Xem tổng quan
   - Người dùng: Quản lý users
   - Khóa học: Quản lý courses
   - Bài kiểm tra: Quản lý tests

## Giao diện

### Layout chính
- **Top Panel**: Logo "Valt" và thông tin admin
- **Sidebar**: Menu điều hướng với các chức năng
- **Content Area**: Hiển thị nội dung tương ứng

### Màu sắc
- Top Panel: #3490DC (xanh dương)
- Sidebar: #2D3748 (xám đen)
- Background: #F8F9FA (xám nhạt)
- Stats Cards: Màu khác nhau cho từng loại thống kê

### Stats Cards
- Tổng người dùng: #38B2AC (xanh lục)
- Tổng khóa học: #22C55E (xanh lá)
- Tổng bài kiểm tra: #FBBF24 (vàng)
- Tổng doanh thu: #0EA5E9 (xanh dương)

## Lưu ý kỹ thuật

- Sử dụng Entity Framework Core để kết nối database
- Async/await pattern cho các thao tác database
- Form riêng biệt cho từng chức năng quản lý
- Helper class để tạo UI components
- Error handling cơ bản với try-catch

## Yêu cầu hệ thống

- .NET 8.0 Windows Forms
- SQL Server database
- Entity Framework Core
- Connection string trong LearningPlatformContext.cs