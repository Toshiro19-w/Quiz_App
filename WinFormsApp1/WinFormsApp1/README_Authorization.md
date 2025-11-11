# Hệ thống Phân quyền - Authorization System

## Tổng quan
Hệ thống phân quyền đơn giản dựa trên Role (vai trò) của người dùng.

## Các Role hỗ trợ
1. **Admin** - Quản trị viên (toàn quyền)
2. **Teacher** - Giáo viên (quản lý khóa học, bài kiểm tra)
3. **Student** - Học viên (học tập, làm bài kiểm tra)

## Cách sử dụng

### 1. Đăng nhập
```csharp
// Trong form đăng nhập
if (AuthHelper.Login(email, password))
{
    if (AuthHelper.IsAdmin())
    {
        // Mở AdminDashboard
    }
    else if (AuthHelper.IsTeacher())
    {
        // Mở TeacherDashboard
    }
    else
    {
        // Mở StudentDashboard
    }
}
```

### 2. Kiểm tra quyền
```csharp
// Kiểm tra role
if (AuthHelper.IsAdmin())
{
    // Chỉ admin mới thực hiện được
}

if (AuthHelper.IsTeacher())
{
    // Admin và Teacher đều thực hiện được
}

// Lấy thông tin user hiện tại
var currentUser = AuthHelper.CurrentUser;
var roleName = AuthHelper.GetRoleName();
```

### 3. Bảo vệ Form
```csharp
public partial class AdminDashboard : Form
{
    public AdminDashboard()
    {
        InitializeComponent();
        PermissionHelper.CheckAdminAccess(this); // Chỉ admin mới vào được
    }
}
```

### 4. Kiểm tra quyền chức năng
```csharp
// Kiểm tra quyền quản lý user
if (PermissionHelper.CanManageUsers())
{
    // Cho phép quản lý user
}

// Vô hiệu hóa control nếu không có quyền
PermissionHelper.DisableControlIfNoPermission(btnDelete, PermissionHelper.CanManageUsers());
```

### 5. Đăng xuất
```csharp
AuthHelper.Logout();
// Quay về form đăng nhập
```

## Demo Account
- **Admin**: admin@admin.com / admin123
- **Teacher**: teacher@test.com / teacher123
- **Student**: student@test.com / student123

## Quyền hạn theo Role

| Chức năng | Admin | Teacher | Student |
|-----------|-------|---------|---------|
| Xem Dashboard | ✓ | ✓ | ✓ |
| Quản lý User | ✓ | ✗ | ✗ |
| Quản lý Khóa học | ✓ | ✓ | ✗ |
| Quản lý Bài kiểm tra | ✓ | ✓ | ✗ |
| Xem Báo cáo | ✓ | ✓ | ✗ |
| Làm bài kiểm tra | ✗ | ✗ | ✓ |
| Học khóa học | ✗ | ✗ | ✓ |

## Mở rộng
Để thêm quyền mới, chỉnh sửa file `PermissionHelper.cs`:
```csharp
public static bool CanDoSomething()
{
    return AuthHelper.IsAdmin(); // hoặc logic phức tạp hơn
}
```
