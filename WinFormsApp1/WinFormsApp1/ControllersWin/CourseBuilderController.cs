using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.ControllersWin
{
    // Controller for WinForms - handles data access and business logic only
    public class CourseBuilderController
    {
        public async Task<CourseBuilderViewModel> LoadCourseAsync(int courseId)
        {
            using var context = new LearningPlatformContext();
            var course = await context.Courses
                .Include(c => c.CourseChapters)
                    .ThenInclude(ch => ch.Lessons)
                        .ThenInclude(l => l.LessonContents)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);

            if (course == null) return null!;

            var vm = new CourseBuilderViewModel
            {
                OwnerId = course.OwnerId,
                Title = course.Title,
                Slug = course.Slug,
                Summary = course.Summary,
                CoverUrl = course.CoverUrl,
                Price = course.Price,
                IsPublished = course.IsPublished
            };

            foreach (var ch in course.CourseChapters.OrderBy(c => c.OrderIndex))
            {
                var chDto = new ChapterDto { Title = ch.Title, Description = ch.Description };
                foreach (var ls in ch.Lessons.OrderBy(l => l.OrderIndex))
                {
                    var lsDto = new LessonDto { Title = ls.Title, Visibility = ls.Visibility };
                    foreach (var ct in ls.LessonContents.OrderBy(cn => cn.OrderIndex))
                    {
                        lsDto.Contents.Add(new ContentDto
                        {
                            ContentType = ct.ContentType,
                            Title = ct.Title,
                            Body = ct.Body,
                            VideoUrl = ct.VideoUrl
                        });
                    }
                    chDto.Lessons.Add(lsDto);
                }
                vm.Chapters.Add(chDto);
            }

            return vm;
        }

        public async Task<int> SaveCourseAsync(CourseBuilderViewModel vm, int? courseId = null)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));
            using var context = new LearningPlatformContext();
            using var tx = await context.Database.BeginTransactionAsync();
            try
            {
                // slug uniqueness check
                if (string.IsNullOrWhiteSpace(vm.Slug)) throw new InvalidOperationException("Slug is required");
                var exists = await context.Courses.AnyAsync(c => c.Slug == vm.Slug && (!courseId.HasValue || c.CourseId != courseId.Value));
                if (exists) throw new InvalidOperationException("Slug already exists");

                Course course;
                if (courseId.HasValue)
                {
                    course = await context.Courses.FindAsync(courseId.Value);
                    if (course == null) throw new InvalidOperationException("Course not found");
                }
                else
                {
                    course = new Course
                    {
                        CreatedAt = DateTime.Now,
                        OwnerId = vm.OwnerId ?? throw new InvalidOperationException("OwnerId required")
                    };
                    context.Courses.Add(course);
                }

                course.Title = vm.Title ?? string.Empty;
                course.Slug = vm.Slug ?? string.Empty;
                course.Summary = vm.Summary;
                course.CoverUrl = vm.CoverUrl;
                course.Price = vm.Price;
                course.IsPublished = vm.IsPublished;
                course.UpdatedAt = DateTime.Now;

                await context.SaveChangesAsync();

                // handle nested Chapters/Lessons/Contents by removing existing and recreating
                var existingChapters = await context.CourseChapters
                    .Where(ch => ch.CourseId == course.CourseId)
                    .Include(ch => ch.Lessons)
                        .ThenInclude(l => l.LessonContents)
                    .ToListAsync();

                if (existingChapters.Any())
                {
                    context.LessonContents.RemoveRange(existingChapters.SelectMany(c => c.Lessons).SelectMany(l => l.LessonContents));
                    context.Lessons.RemoveRange(existingChapters.SelectMany(c => c.Lessons));
                    context.CourseChapters.RemoveRange(existingChapters);
                    await context.SaveChangesAsync();
                }

                // recreate from vm
                int chapterIndex = 0;
                foreach (var chDto in vm.Chapters)
                {
                    var ch = new CourseChapter
                    {
                        CourseId = course.CourseId,
                        Title = chDto.Title ?? string.Empty,
                        Description = chDto.Description,
                        OrderIndex = chapterIndex++
                    };
                    context.CourseChapters.Add(ch);
                    await context.SaveChangesAsync(); // ensure ChapterId available for lessons

                    int lessonIdx = 0;
                    foreach (var lsDto in chDto.Lessons)
                    {
                        var lesson = new Lesson
                        {
                            ChapterId = ch.ChapterId,
                            Title = lsDto.Title ?? string.Empty,
                            Description = lsDto.Description,
                            OrderIndex = lessonIdx++,
                            Visibility = lsDto.Visibility ?? "Course",
                            CreatedAt = DateTime.Now
                        };
                        context.Lessons.Add(lesson);
                        await context.SaveChangesAsync(); // ensure LessonId

                        int contentIdx = 0;
                        foreach (var c in lsDto.Contents)
                        {
                            var lc = new LessonContent
                            {
                                LessonId = lesson.LessonId,
                                ContentType = c.ContentType ?? "Theory",
                                Title = c.Title,
                                Body = c.Body,
                                VideoUrl = c.VideoUrl,
                                OrderIndex = contentIdx++,
                                CreatedAt = DateTime.Now
                            };
                            context.LessonContents.Add(lc);
                        }
                        await context.SaveChangesAsync();
                    }
                }

                await tx.CommitAsync();
                return course.CourseId;
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(slug)) return Task.FromResult(false);
            using var context = new LearningPlatformContext();
            var q = context.Courses.AsQueryable().Where(c => c.Slug == slug);
            if (excludeId.HasValue) q = q.Where(c => c.CourseId != excludeId.Value);
            return q.AnyAsync().ContinueWith(t => !t.Result);
        }

        // CRUD for chapters/lessons/contents can be added here if needed
    }
}