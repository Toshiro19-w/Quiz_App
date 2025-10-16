using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class Class
{
    public int ClassId { get; set; }

    public int TeacherId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public string? Term { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }
}
