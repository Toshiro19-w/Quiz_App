using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.Controllers
{
    /// <summary>
    /// Partial class containing Course, Chapter, Lesson, and Category Management methods for AdminController
    /// </summary>
    public partial class AdminController
    {
        // Course Management
        public async Task<List<Course>> GetCoursesAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Courses.Include(c => c.Category).ToListAsync();
            }
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Courses.Include(c => c.Category).FirstOrDefaultAsync(c => c.CourseId == id);
            }
        }

        public async Task<bool> CourseSlugExistsAsync(string slug, int? excludeId = null)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Courses.AnyAsync(c => c.Slug == slug && (!excludeId.HasValue || c.CourseId != excludeId.Value));
            }
        }

        public async Task<bool> CreateCourseAsync(Course course)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    if (course.Price < 0) throw new ArgumentException("Giá phải là số lớn hơn hoặc bằng 0.");
                    if (string.IsNullOrWhiteSpace(course.Title)) throw new ArgumentException("Tiêu đề không được để trống.");
                    if (course.Title.Length > 200) throw new ArgumentException("Tiêu đề quá dài (tối đa 200 ký tự).");

                    if (string.IsNullOrWhiteSpace(course.Slug)) course.Slug = course.Title.ToLower().Replace(" ", "-");
                    var slug = course.Slug;
                    if (await CourseSlugExistsAsync(slug)) throw new ArgumentException("Slug đã tồn tại. Vui lòng đổi tên tiêu đề.");

                    if (course.CreatedAt == default) course.CreatedAt = DateTime.UtcNow;

                    context.Courses.Add(course);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tạo khóa học: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbCourse = await context.Courses.FindAsync(course.CourseId);
                    if (dbCourse == null) throw new Exception("Khóa học không tồn tại.");

                    if (course.Price < 0) throw new ArgumentException("Giá phải là số lớn hơn hoặc bằng 0.");
                    if (string.IsNullOrWhiteSpace(course.Title)) throw new ArgumentException("Tiêu đề không được để trống.");
                    if (course.Title.Length > 200) throw new ArgumentException("Tiêu đề quá dài (tối đa 200 ký tự).");

                    if (string.IsNullOrWhiteSpace(course.Slug)) course.Slug = course.Title.ToLower().Replace(" ", "-");
                    if (await CourseSlugExistsAsync(course.Slug, course.CourseId)) throw new ArgumentException("Slug đã tồn tại cho khóa học khác.");

                    // update allowed fields
                    dbCourse.Title = course.Title;
                    dbCourse.Summary = course.Summary;
                    dbCourse.Slug = course.Slug;
                    dbCourse.Price = course.Price;
                    dbCourse.IsPublished = course.IsPublished;
                    dbCourse.OwnerId = course.OwnerId;
                    dbCourse.UpdatedAt = DateTime.UtcNow;

                    context.Courses.Update(dbCourse);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật khóa học: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var course = await context.Courses.FindAsync(id);
                    if (course == null) throw new Exception("Khóa học không tồn tại.");

                    context.Courses.Remove(course);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa khóa học: {ex.Message}");
                }
            }
        }

        // Chapter Management
        public async Task<List<CourseChapter>> GetChaptersByCourseIdAsync(int courseId)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.CourseChapters
                    .Include(c => c.Lessons)
                    .Where(c => c.CourseId == courseId)
                    .OrderBy(c => c.OrderIndex)
                    .ToListAsync();
            }
        }

        public async Task<bool> CreateChapterAsync(CourseChapter chapter)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(chapter.Title))
                        throw new ArgumentException("Tiêu đề chương không được trống");

                    if (chapter.OrderIndex == 0)
                    {
                        var maxOrder = await context.CourseChapters
                            .Where(c => c.CourseId == chapter.CourseId)
                            .MaxAsync(c => (int?)c.OrderIndex) ?? 0;
                        chapter.OrderIndex = maxOrder + 1;
                    }

                    context.CourseChapters.Add(chapter);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tạo chương: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateChapterAsync(CourseChapter chapter)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbChapter = await context.CourseChapters.FindAsync(chapter.ChapterId);
                    if (dbChapter == null) throw new Exception("Chương không tồn tại");

                    if (string.IsNullOrWhiteSpace(chapter.Title))
                        throw new ArgumentException("Tiêu đề chương không được trống");

                    dbChapter.Title = chapter.Title;
                    dbChapter.Description = chapter.Description;
                    dbChapter.OrderIndex = chapter.OrderIndex;

                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật chương: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteChapterAsync(int chapterId)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var chapter = await context.CourseChapters
                        .Include(c => c.Lessons)
                        .FirstOrDefaultAsync(c => c.ChapterId == chapterId);
                    if (chapter == null) throw new Exception("Chương không tồn tại");

                    context.Lessons.RemoveRange(chapter.Lessons);
                    context.CourseChapters.Remove(chapter);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa chương: {ex.Message}");
                }
            }
        }

        // Lesson Management
        public async Task<bool> CreateLessonAsync(Lesson lesson)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(lesson.Title))
                        throw new ArgumentException("Tiêu đề bài học không được trống");

                    if (lesson.OrderIndex == 0)
                    {
                        var maxOrder = await context.Lessons
                            .Where(l => l.ChapterId == lesson.ChapterId)
                            .MaxAsync(l => (int?)l.OrderIndex) ?? 0;
                        lesson.OrderIndex = maxOrder + 1;
                    }

                    lesson.CreatedAt = DateTime.UtcNow;
                    context.Lessons.Add(lesson);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tạo bài học: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateLessonAsync(Lesson lesson)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbLesson = await context.Lessons.FindAsync(lesson.LessonId);
                    if (dbLesson == null) throw new Exception("Bài học không tồn tại");

                    if (string.IsNullOrWhiteSpace(lesson.Title))
                        throw new ArgumentException("Tiêu đề bài học không được trống");

                    dbLesson.Title = lesson.Title;
                    dbLesson.Description = lesson.Description;
                    dbLesson.OrderIndex = lesson.OrderIndex;
                    dbLesson.Visibility = lesson.Visibility;
                    dbLesson.UpdatedAt = DateTime.UtcNow;

                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật bài học: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteLessonAsync(int lessonId)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var lesson = await context.Lessons.FindAsync(lessonId);
                    if (lesson == null) throw new Exception("Bài học không tồn tại");

                    context.Lessons.Remove(lesson);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa bài học: {ex.Message}");
                }
            }
        }

        // Category Management
        public async Task<List<CourseCategory>> GetCategoriesAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.CourseCategories.ToListAsync();
            }
        }

        public async Task<bool> CreateCategoryAsync(CourseCategory category)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(category.Name))
                        throw new ArgumentException("Tên danh mục không được trống");

                    context.CourseCategories.Add(category);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tạo danh mục: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateCategoryAsync(CourseCategory category)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbCategory = await context.CourseCategories.FindAsync(category.CategoryId);
                    if (dbCategory == null) throw new Exception("Danh mục không tồn tại");

                    if (string.IsNullOrWhiteSpace(category.Name))
                        throw new ArgumentException("Tên danh mục không được trống");

                    dbCategory.Name = category.Name;
                    dbCategory.Description = category.Description;

                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật danh mục: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var category = await context.CourseCategories.FindAsync(id);
                    if (category == null) throw new Exception("Danh mục không tồn tại");

                    // Kiểm tra xem có khóa học nào đang sử dụng danh mục này không
                    var coursesUsingCategory = await context.Courses.AnyAsync(c => c.CategoryId == id);
                    if (coursesUsingCategory)
                    {
                        throw new Exception("Không thể xóa danh mục vì có khóa học đang sử dụng");
                    }

                    context.CourseCategories.Remove(category);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa danh mục: {ex.Message}");
                }
            }
        }
    }
}
