using System.Collections.Generic;

namespace WinFormsApp1.ViewModels
{
    public class CourseBuilderViewModel
    {
        public int? OwnerId { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Summary { get; set; }
        public int? CategoryId { get; set; }
        public decimal Price { get; set; }
        public string? CoverUrl { get; set; }
        public bool IsPublished { get; set; }

        // Simplified nested structures to represent chapters/lessons/contents
        public List<ChapterDto> Chapters { get; set; } = new List<ChapterDto>();
    }

    public class ChapterDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<LessonDto> Lessons { get; set; } = new List<LessonDto>();
    }

    public class LessonDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Visibility { get; set; }
        public List<ContentDto> Contents { get; set; } = new List<ContentDto>();
    }

    public class ContentDto
    {
        public string? ContentType { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? VideoUrl { get; set; }
    }
}
