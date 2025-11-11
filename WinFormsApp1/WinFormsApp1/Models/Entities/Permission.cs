using System;
using System.Collections.Generic;

namespace WinFormsApp1.Models.Entities;

public partial class Permission
{
    public int PermissionId { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
    public string Category { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
