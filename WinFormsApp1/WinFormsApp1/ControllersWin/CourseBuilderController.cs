using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.ViewModels;
using System.Diagnostics;

namespace WinFormsApp1.ControllersWin
{
    // Controller for WinForms - handles data access and business logic only
    public class CourseBuilderController
    {
		public async Task<CourseBuilderViewModel> LoadCourseAsync(int courseId)
		{
			using var context = new LearningPlatformContext();
			var course = await context.Courses
				.Include(c => c.CourseChapters.OrderBy(ch => ch.OrderIndex))
					.ThenInclude(ch => ch.Lessons.OrderBy(l => l.OrderIndex))
						.ThenInclude(l => l.LessonContents.OrderBy(lc => lc.OrderIndex))
				.FirstOrDefaultAsync(c => c.CourseId == courseId);

			if (course == null) return null!;

			// Debug: Check raw data from database
			Debug.WriteLine($"Course loaded: {course.Title}");
			Debug.WriteLine($"Chapters count: {course.CourseChapters?.Count ?? 0}");
			foreach (var ch in course.CourseChapters ?? new List<CourseChapter>())
			{
				Debug.WriteLine($"  Chapter: {ch.Title}, Lessons: {ch.Lessons?.Count ?? 0}");
				foreach (var ls in ch.Lessons ?? new List<Lesson>())
				{
					Debug.WriteLine($"    Lesson: {ls.Title}, Contents: {ls.LessonContents?.Count ?? 0}");
					foreach (var ct in ls.LessonContents ?? new List<LessonContent>())
					{
						Debug.WriteLine($"      Content: {ct.ContentType} - {ct.Title}");
					}
				}
			}

			var vm = new CourseBuilderViewModel
			{
				CourseId = course.CourseId,
				Title = course.Title,
				Slug = course.Slug,
				Summary = course.Summary,
				Price = course.Price,
				CoverUrl = course.CoverUrl,
				CategoryId = course.CategoryId
			};

			// ✅ CHỈ DÙNG 1 BLOCK NÀY ĐỂ ĐỔ CHAPTERS / LESSONS / CONTENTS
			foreach (var ch in course.CourseChapters.OrderBy(c => c.OrderIndex))
			{
				var chDto = new ChapterBuilderViewModel
				{
					ChapterId = ch.ChapterId,
					Title = ch.Title,
					Description = ch.Description,
					OrderIndex = ch.OrderIndex
				};

				foreach (var ls in ch.Lessons.OrderBy(l => l.OrderIndex))
				{
					var lsDto = new LessonBuilderViewModel
					{
						LessonId = ls.LessonId,
						Title = ls.Title,
						Visibility = ls.Visibility,
						OrderIndex = ls.OrderIndex
					};

					foreach (var ct in ls.LessonContents.OrderBy(cn => cn.OrderIndex))
					{
						var contentVm = new LessonContentBuilderViewModel
						{
							ContentId = ct.ContentId,
							ContentType = string.IsNullOrEmpty(ct.ContentType) ? "Theory" : ct.ContentType,
							RefId = ct.RefId,
							Title = ct.Title,
							Body = ct.Body,
							VideoUrl = ct.VideoUrl,
							OrderIndex = ct.OrderIndex
						};
						
						Debug.WriteLine($"LoadCourseAsync: Loading content - Type: {contentVm.ContentType}, Title: '{contentVm.Title}', Body length: {contentVm.Body?.Length ?? 0}");
						lsDto.Contents.Add(contentVm);
						Debug.WriteLine($"LoadCourseAsync: Added content to lesson. Lesson now has {lsDto.Contents.Count} contents");
					}

					chDto.Lessons.Add(lsDto);
				}

				vm.Chapters.Add(chDto);
			}

			// Final debug check
			Debug.WriteLine($"LoadCourseAsync: Final VM has {vm.Chapters.Count} chapters");
			foreach (var ch in vm.Chapters)
			{
				Debug.WriteLine($"  VM Chapter: {ch.Title}, Lessons: {ch.Lessons.Count}");
				foreach (var ls in ch.Lessons)
				{
					Debug.WriteLine($"    VM Lesson: {ls.Title}, Contents: {ls.Contents.Count}");
				}
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
                course.Price = (decimal)vm.Price;
                course.IsPublished = vm.IsPublished;
                course.UpdatedAt = DateTime.Now;
                // ensure category is persisted
                course.CategoryId = vm.CategoryId;

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
                            // Log incoming content for diagnosis
                            Debug.WriteLine($"SaveCourseAsync: Saving content for LessonId={lesson.LessonId} Type={c.ContentType} Title='{c.Title}' BodyLen={(c.Body?.Length ?? 0)} RefId={(c.RefId.HasValue ? c.RefId.Value.ToString() : "NULL")} VideoUrl='{c.VideoUrl}'");

                            // If content references another entity but RefId missing, create it now
                            if (string.Equals(c.ContentType, "FlashcardSet", StringComparison.OrdinalIgnoreCase) && !c.RefId.HasValue)
                            {
                                if (!string.IsNullOrWhiteSpace(c.FlashcardSetTitle) || (c.Flashcards != null && c.Flashcards.Count > 0))
                                {
                                    var set = new FlashcardSet
                                    {
                                        OwnerId = course.OwnerId,
                                        Title = string.IsNullOrWhiteSpace(c.FlashcardSetTitle) ? c.Title ?? "Untitled Flashcard Set" : c.FlashcardSetTitle,
                                        Description = c.FlashcardSetDesc,
                                        Visibility = "Course",
                                        CreatedAt = DateTime.Now
                                    };
                                    context.FlashcardSets.Add(set);
                                    await context.SaveChangesAsync();

                                    if (c.Flashcards != null)
                                    {
                                        int cardIdx = 0;
                                        foreach (var fc in c.Flashcards)
                                        {
                                            var card = new Flashcard
                                            {
                                                SetId = set.SetId,
                                                FrontText = fc.FrontText ?? string.Empty,
                                                BackText = fc.BackText ?? string.Empty,
                                                Hint = fc.Hint,
                                                OrderIndex = cardIdx++,
                                                CreatedAt = DateTime.Now
                                            };
                                            context.Flashcards.Add(card);
                                        }
                                        await context.SaveChangesAsync();
                                    }

                                    c.RefId = set.SetId;
                                    Debug.WriteLine($"SaveCourseAsync: Created FlashcardSet Id={set.SetId} for lesson {lesson.LessonId}");
                                }
                            }

                            if (string.Equals(c.ContentType, "Test", StringComparison.OrdinalIgnoreCase) && !c.RefId.HasValue)
                            {
                                if (!string.IsNullOrWhiteSpace(c.TestTitle) || (c.Questions != null && c.Questions.Count > 0))
                                {
                                    var test = new Test
                                    {
                                        OwnerId = course.OwnerId,
                                        Title = string.IsNullOrWhiteSpace(c.TestTitle) ? c.Title ?? "Untitled Test" : c.TestTitle,
                                        Description = c.TestDesc,
                                        Visibility = "Course",
                                        CreatedAt = DateTime.Now
                                    };
                                    context.Tests.Add(test);
                                    await context.SaveChangesAsync();

                                    if (c.Questions != null)
                                    {
                                        int qIdx = 0;
                                        foreach (var q in c.Questions)
                                        {
                                            var question = new Question
                                            {
                                                TestId = test.TestId,
                                                Type = q.Type ?? "MCQ_Single",
                                                StemText = q.StemText ?? string.Empty,
                                                Points = (decimal)(q.Points == 0 ? 1 : q.Points),
                                                OrderIndex = qIdx++
                                            };
                                            context.Questions.Add(question);
                                            await context.SaveChangesAsync();

                                            if (q.Options != null)
                                            {
                                                int oIdx = 0;
                                                foreach (var opt in q.Options)
                                                {
                                                    var option = new QuestionOption
                                                    {
                                                        QuestionId = question.QuestionId,
                                                        OptionText = opt.OptionText ?? string.Empty,
                                                        IsCorrect = opt.IsCorrect,
                                                        OrderIndex = oIdx++
                                                    };
                                                    context.QuestionOptions.Add(option);
                                                }
                                                await context.SaveChangesAsync();
                                            }
                                        }
                                    }

                                    c.RefId = test.TestId;
                                    Debug.WriteLine($"SaveCourseAsync: Created Test Id={test.TestId} for lesson {lesson.LessonId}");
                                }
                            }

                            // Ensure RefId is set before creating LessonContent
                            if (!c.RefId.HasValue)
                            {
                                Debug.WriteLine($"SaveCourseAsync: Warning - content RefId is null after processing. LessonId={lesson.LessonId} ContentType={c.ContentType} Title='{c.Title}'");
                            }

                            var lc = new LessonContent
                            {
                                LessonId = lesson.LessonId,
                                ContentType = c.ContentType ?? "Theory",
                                Title = c.Title,
                                Body = c.Body,
                                VideoUrl = c.VideoUrl,
                                RefId = c.RefId,
                                OrderIndex = contentIdx++,
                                CreatedAt = DateTime.Now
                            };

                            // If this is a Theory content and body is empty, warn in log
                            if (string.Equals(lc.ContentType, "Theory", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(lc.Body))
                            {
                                Debug.WriteLine($"SaveCourseAsync: Warning - saving Theory content with empty body. Lesson={lesson.LessonId} Title={lc.Title}");
                            }

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

        public async Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(slug)) return false;
            using var context = new LearningPlatformContext();
            var q = context.Courses.AsQueryable().Where(c => c.Slug == slug);
            if (excludeId.HasValue) q = q.Where(c => c.CourseId != excludeId.Value);
            var exists = await q.AnyAsync();
            return !exists;
        }

        // CRUD for chapters/lessons/contents can be added here if needed
    }
}