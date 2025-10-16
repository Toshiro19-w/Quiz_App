using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class Course
{
    public int CourseId { get; set; }

    public int OwnerId { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Summary { get; set; }

    public string? CoverUrl { get; set; }

    public decimal Price { get; set; }

    public string Currency { get; set; } = null!;

    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
