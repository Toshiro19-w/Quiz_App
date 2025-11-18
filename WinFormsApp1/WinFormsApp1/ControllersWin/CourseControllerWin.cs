using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.ControllersWin
{
    // Controller for WinForms - handles data access and business logic only
    public class CourseControllerWin
    {
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
