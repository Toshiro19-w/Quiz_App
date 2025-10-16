using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class Library
{
    public int LibraryId { get; set; }

    public int OwnerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
}
