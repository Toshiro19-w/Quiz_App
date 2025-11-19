using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.Controllers
{
    public class CourseController
    {
        public async Task<Course> GetCourseDetailAsync(int courseId)
        {
            using (var context = new LearningPlatformContext())
            {
                var course = await context.Courses
                    .Include(c => c.Owner)
                    .Include(c => c.Category)
                    .Include(c => c.CourseChapters)
                        .ThenInclude(ch => ch.Lessons)
                    .Include(c => c.CourseReviews)
                        .ThenInclude(r => r.User)
                    .Include(c => c.CoursePurchases)
                    .FirstOrDefaultAsync(c => c.CourseId == courseId);

                if (course != null)
                {
                    // Sort chapters in memory
                    course.CourseChapters = course.CourseChapters
                        .OrderBy(ch => ch.OrderIndex)
                        .ToList();

                    // Sort lessons for each chapter
                    foreach (var chapter in course.CourseChapters)
                    {
                        chapter.Lessons = chapter.Lessons
                            .OrderBy(l => l.OrderIndex)
                            .ToList();
                    }

                    // Filter and sort reviews
                    course.CourseReviews = course.CourseReviews
                        .Where(r => r.IsApproved)
                        .OrderByDescending(r => r.CreatedAt)
                        .ToList();
                }

                return course;
            }
        }

        public async Task<Dictionary<int, int>> GetRatingDistributionAsync(int courseId)
        {
            using (var context = new LearningPlatformContext())
            {
                var reviews = await context.CourseReviews
                    .Where(r => r.CourseId == courseId && r.IsApproved)
                    .ToListAsync();

                return new Dictionary<int, int>
                {
                    { 5, reviews.Count(r => r.Rating == 5) },
                    { 4, reviews.Count(r => r.Rating == 4) },
                    { 3, reviews.Count(r => r.Rating == 3) },
                    { 2, reviews.Count(r => r.Rating == 2) },
                    { 1, reviews.Count(r => r.Rating == 1) }
                };
            }
        }
		public async Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null)
		{
			if (string.IsNullOrWhiteSpace(slug)) return false;
			using var context = new LearningPlatformContext();
			var query = context.Courses.AsQueryable().Where(c => c.Slug == slug);
			if (excludeId.HasValue) query = query.Where(c => c.CourseId != excludeId.Value);
			return !await query.AnyAsync();
		}

		public async Task<int> SaveCourseAsync(CourseBuilderViewModel vm, int? courseId = null)
		{
			using var context = new LearningPlatformContext();
			Models.Entities.Course course;
			if (courseId.HasValue)
			{
				course = await context.Courses.FindAsync(courseId.Value);
				if (course == null) throw new InvalidOperationException("Course not found");
			}
			else
			{
				course = new Models.Entities.Course
				{
					CreatedAt = DateTime.Now,
					OwnerId = vm.OwnerId ?? 0 // caller should set OwnerId
				};
				context.Courses.Add(course);
			}

			course.Title = vm.Title ?? string.Empty;
			course.Slug = vm.Slug ?? string.Empty;
			course.Summary = vm.Summary;
			course.CoverUrl = vm.CoverUrl;
			course.Price = (decimal)vm.Price;
			course.IsPublished = vm.IsPublished;
			course.UpdatedAt = DateTime.Now;

			await context.SaveChangesAsync();
			return course.CourseId;
		}

		public async Task AutosaveDraftAsync(CourseBuilderViewModel vm, int? courseId = null)
		{
			// Simple wrapper that saves course basic info. Complex nested data (chapters/lessons/contents)
			// can be implemented later. For now persist main Course fields.
			await SaveCourseAsync(vm, courseId);
		}
	}
}
