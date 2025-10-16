using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class CourseContent
{
    public int CourseContentId { get; set; }

    public int CourseId { get; set; }

    public int? SectionId { get; set; }

    public string ContentType { get; set; } = null!;

    public int ContentId { get; set; }

    public string? TitleOverride { get; set; }

    public int OrderIndex { get; set; }

    public bool IsPreview { get; set; }
}
