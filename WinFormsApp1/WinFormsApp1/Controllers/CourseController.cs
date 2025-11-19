using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

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
    }
}
