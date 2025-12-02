# User Report - Role Column Fix

## Vấn đề

Cột "Vai trò" trong User Report hiển thị **"N/A"** thay vì tên vai trò thực tế (Admin, User).

## Nguyên nhân

Trong file `ReportHelper.cs`, code đang sử dụng navigation property:

```csharp
// ❌ BAD - Navigation property không được load
user.Role?.Name ?? "N/A"
```

Navigation property `Role` không được load (chưa Include trong query), nên luôn là `null` → hiển thị "N/A".

## Giải pháp

Thay vì dùng navigation property, map `RoleId` trực tiếp ra tên vai trò:

```csharp
// ✅ GOOD - Map RoleId directly
string roleName = user.RoleId switch
{
    1 => "Admin",
    2 => "User",
    _ => "Unknown"
};

dataTable.Rows.Add(
    user.UserId,
    user.Username,
    user.Email,
    user.FullName,
    roleName,  // ✅ Use mapped role name
    user.CreatedAt
);
```

## So sánh

### Before ❌
```csharp
dataTable.Rows.Add(
    user.UserId,
    user.Username,
    user.Email,
    user.FullName,
    user.Role?.Name ?? "N/A",  // ❌ Always "N/A"
    user.CreatedAt
);
```

**Result**: Cột "Vai trò" luôn hiển thị "N/A"

### After ✅
```csharp
string roleName = user.RoleId switch
{
    1 => "Admin",
    2 => "User",
    _ => "Unknown"
};

dataTable.Rows.Add(
    user.UserId,
    user.Username,
    user.Email,
    user.FullName,
    roleName,  // ✅ "Admin" or "User"
    user.CreatedAt
);
```

**Result**: Cột "Vai trò" hiển thị đúng "Admin" hoặc "User"

## Code Changes

### File: `WinFormsApp1\Helpers\ReportHelper.cs`

**Method**: `GenerateUserReport()`

```csharp
public static void GenerateUserReport(ReportViewer reportViewer, List<User> users)
{
    // ... setup code ...
    
    foreach (var user in users)
    {
        // ✅ NEW: Map RoleId to role name
        string roleName = user.RoleId switch
        {
            1 => "Admin",
            2 => "User",
            _ => "Unknown"
        };
        
        dataTable.Rows.Add(
            user.UserId,
            user.Username,
            user.Email,
            user.FullName,
            roleName,  // ✅ Use mapped name
            user.CreatedAt
        );
    }
    
    // ... rest of code ...
}
```

## Lý do không Include Role

### Option 1: Include Role (không khuyến khích)
```csharp
// ❌ Requires modifying AdminController.GetUsersAsync()
var users = await context.Users
    .Include(u => u.Role)  // Additional query overhead
    .ToListAsync();
```

**Nhược điểm:**
- Cần sửa nhiều chỗ (Controller query)
- Thêm overhead cho database query
- Phức tạp hơn không cần thiết

### Option 2: Map RoleId (✅ Recommended)
```csharp
// ✅ Simple, fast, no additional queries
string roleName = user.RoleId switch
{
    1 => "Admin",
    2 => "User",
    _ => "Unknown"
};
```

**Ưu điểm:**
- ✅ Không cần sửa query
- ✅ Không overhead
- ✅ Simple và clear
- ✅ Dễ maintain

## Testing

### Test Case 1: Admin User
```
Input: User with RoleId = 1
Expected: Vai trò = "Admin"
Result: ✅ Pass
```

### Test Case 2: Regular User
```
Input: User with RoleId = 2
Expected: Vai trò = "User"
Result: ✅ Pass
```

### Test Case 3: Unknown Role
```
Input: User with RoleId = 99 (không tồn tại)
Expected: Vai trò = "Unknown"
Result: ✅ Pass
```

## Role ID Mapping

Theo database schema (`LearningPlatform.sql`):

```sql
INSERT INTO dbo.Roles (Name) VALUES (N'Admin'), (N'User');
```

**Mapping:**
| RoleId | Role Name |
|--------|-----------|
| 1 | Admin |
| 2 | User |
| Other | Unknown |

## Build Status

✅ **Build successful** - No errors

## Files Modified

1. ✅ `WinFormsApp1\Helpers\ReportHelper.cs`
   - Method: `GenerateUserReport()`
   - Lines: ~20-40

## Benefits

### For Users 👥
- ✅ **Correct display**: Vai trò hiển thị đúng
- ✅ **Clear information**: Biết user là Admin hay User
- ✅ **Better reports**: Report chất lượng hơn

### For Developers 💻
- ✅ **Simple fix**: Không cần sửa query
- ✅ **No overhead**: Không impact performance
- ✅ **Easy maintain**: Logic rõ ràng
- ✅ **Type safe**: Switch expression với pattern matching

### For System 🎯
- ✅ **Performance**: Không additional query
- ✅ **Reliability**: Không depend on navigation property
- ✅ **Scalability**: Dễ thêm roles mới

## Future Improvements

Nếu cần dynamic role mapping (không hardcode):

```csharp
// Option 1: Cache roles in memory
private static Dictionary<int, string> _roleCache;

static ReportHelper()
{
    using var context = new LearningPlatformContext();
    _roleCache = context.Roles
        .ToDictionary(r => r.RoleId, r => r.Name);
}

// Then use:
string roleName = _roleCache.TryGetValue(user.RoleId, out var name) 
    ? name 
    : "Unknown";
```

**Hoặc:**

```csharp
// Option 2: Load roles once per report
public static void GenerateUserReport(ReportViewer reportViewer, List<User> users)
{
    using var context = new LearningPlatformContext();
    var roles = context.Roles.ToDictionary(r => r.RoleId, r => r.Name);
    
    foreach (var user in users)
    {
        string roleName = roles.TryGetValue(user.RoleId, out var name)
            ? name
            : "Unknown";
        // ...
    }
}
```

**Nhưng với 2 roles fixed, hardcode switch expression là tốt nhất!**

## Summary

✅ **Fixed**: Cột "Vai trò" giờ hiển thị đúng
✅ **Simple**: Map RoleId trực tiếp
✅ **Fast**: Không overhead
✅ **Reliable**: Không depend on navigation property
✅ **Build**: Successful

**Impact**: 🎯 Critical bug fix - Report giờ hiển thị đúng thông tin!
