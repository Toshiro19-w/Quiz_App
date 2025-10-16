using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class ClassAnnouncement
{
    public int AnnouncementId { get; set; }

    public int ClassId { get; set; }

    public int AuthorId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? PinUntil { get; set; }
}
