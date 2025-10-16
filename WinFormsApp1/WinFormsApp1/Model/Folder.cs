using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class Folder
{
    public int FolderId { get; set; }

    public int LibraryId { get; set; }

    public int? ParentFolderId { get; set; }

    public string Name { get; set; } = null!;

    public int OrderIndex { get; set; }

    public DateTime CreatedAt { get; set; }
}
