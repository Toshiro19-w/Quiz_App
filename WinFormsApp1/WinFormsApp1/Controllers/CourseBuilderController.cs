using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.ViewModels;
using System.Diagnostics;

namespace WinFormsApp1.Controllers
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

						// If this content references a FlashcardSet or Test, load details into VM so editor can display them
						if (string.Equals(contentVm.ContentType, "FlashcardSet", StringComparison.OrdinalIgnoreCase) && contentVm.RefId.HasValue)
						{
							var set = await context.FlashcardSets
								.Include(s => s.Flashcards)
								.FirstOrDefaultAsync(s => s.SetId == contentVm.RefId.Value);
							if (set != null)
							{
								contentVm.FlashcardSetTitle = set.Title;
								contentVm.FlashcardSetDesc = set.Description;
								contentVm.Flashcards = set.Flashcards
									.OrderBy(f => f.OrderIndex)
									.Select(f => new FlashcardBuilderViewModel
									{
										FrontText = f.FrontText,
										BackText = f.BackText,
										Hint = f.Hint,
										OrderIndex = f.OrderIndex
									})
									.ToList();
								Debug.WriteLine($"LoadCourseAsync: Loaded FlashcardSet Id={set.SetId} with {contentVm.Flashcards.Count} cards");
							}
						}

						if (string.Equals(contentVm.ContentType, "Test", StringComparison.OrdinalIgnoreCase) && contentVm.RefId.HasValue)
						{
							var test = await context.Tests
								.Include(t => t.Questions)
									.ThenInclude(q => q.QuestionOptions)
								.FirstOrDefaultAsync(t => t.TestId == contentVm.RefId.Value);
							if (test != null)
							{
								contentVm.TestTitle = test.Title;
								contentVm.TestDesc = test.Description;
								contentVm.Questions = test.Questions
									.OrderBy(q => q.OrderIndex)
									.Select(q => new TestQuestionBuilderViewModel
									{
										Type = q.Type,
										StemText = q.StemText,
										Points = q.Points,
										OrderIndex = q.OrderIndex,
										Options = q.QuestionOptions.OrderBy(o => o.OrderIndex).Select(o => new TestQuestionOptionBuilderViewModel
										{
											OptionText = o.OptionText,
											IsCorrect = o.IsCorrect,
											OrderIndex = o.OrderIndex
										}).ToList()
									}).ToList();
								Debug.WriteLine($"LoadCourseAsync: Loaded Test Id={test.TestId} with {contentVm.Questions.Count} questions");
							}
						}

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
                            Debug.WriteLine($"SaveCourseAsync: Saving content for LessonId={lesson.LessonId} Type={c.ContentType} Title='{c.Title}' BodyLen={c.Body?.Length ?? 0} RefId={(c.RefId.HasValue ? c.RefId.Value.ToString() : "NULL")} VideoUrl='{c.VideoUrl}'");

                            // If content references another entity but RefId missing, create it now
                            if (string.Equals(c.ContentType, "FlashcardSet", StringComparison.OrdinalIgnoreCase) && !c.RefId.HasValue)
                            {
                                if (!string.IsNullOrWhiteSpace(c.FlashcardSetTitle) || c.Flashcards != null && c.Flashcards.Count > 0)
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

                            if (string.Equals(c.ContentType, "Test", StringComparison.OrdinalIgnoreCase))
                            {
                                Test? test = null;
                                if (c.RefId.HasValue)
                                {
                                    test = await context.Tests
                                        .Include(t => t.Questions)
                                            .ThenInclude(q => q.QuestionOptions)
                                        .FirstOrDefaultAsync(t => t.TestId == c.RefId.Value);
                                    
                                    if (test != null)
                                    {
                                        Debug.WriteLine($"[Controller] Updating Test Id={test.TestId}. Questions count: {c.Questions?.Count ?? 0}");
                                        
                                        var questionIds = test.Questions.Select(q => q.QuestionId).ToList();
                                        var options = await context.QuestionOptions.Where(o => questionIds.Contains(o.QuestionId)).ToListAsync();
                                        context.QuestionOptions.RemoveRange(options);
                                        await context.SaveChangesAsync();
                                        
                                        // Remove any AttemptAnswers that reference these questions to avoid FK conflicts
                                        var attemptAnswers = await context.AttemptAnswers.Where(a => questionIds.Contains(a.QuestionId)).ToListAsync();
                                        if (attemptAnswers.Any())
                                        {
                                            Debug.WriteLine($"[Controller] Removing {attemptAnswers.Count} AttemptAnswers referencing old questions for TestId={test.TestId}");
                                            context.AttemptAnswers.RemoveRange(attemptAnswers);
                                            await context.SaveChangesAsync();
                                        }

                                        var questions = await context.Questions.Where(q => q.TestId == test.TestId).ToListAsync();
                                        context.Questions.RemoveRange(questions);
                                        await context.SaveChangesAsync();
                                        
                                        test.Title = string.IsNullOrWhiteSpace(c.TestTitle) ? c.Title ?? "Untitled Test" : c.TestTitle;
                                        test.Description = c.TestDesc;
                                    }
                                    else
                                    {
                                        c.RefId = null;
                                    }
                                }
                                
                                if (test == null)
                                {
                                    Debug.WriteLine($"[Controller] Creating Test for lesson {lesson.LessonId}. Questions count: {c.Questions?.Count ?? 0}");
                                    
                                    test = new Test
                                    {
                                        OwnerId = course.OwnerId,
                                        Title = string.IsNullOrWhiteSpace(c.TestTitle) ? c.Title ?? "Untitled Test" : c.TestTitle,
                                        Description = c.TestDesc,
                                        Visibility = "Course",
                                        GradingMode = "Auto",
                                        CreatedAt = DateTime.Now
                                    };
                                    context.Tests.Add(test);
                                    await context.SaveChangesAsync();
                                    c.RefId = test.TestId;
                                }
                                
                                decimal totalPoints = 0;
                                if (c.Questions != null && c.Questions.Count > 0)
                                {
                                    int qIdx = 0;
                                    foreach (var q in c.Questions)
                                    {
                                        var question = new Question
                                        {
                                            TestId = test.TestId,
                                            Type = q.Type ?? "MCQ_Single",
                                            StemText = q.StemText ?? string.Empty,
                                            Points = q.Points == 0 ? 1 : q.Points,
                                            OrderIndex = qIdx++
                                        };
                                        context.Questions.Add(question);
                                        await context.SaveChangesAsync();
                                        totalPoints += question.Points;

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
                                
                                test.MaxScore = totalPoints == 0 ? null : (decimal?)totalPoints;
                                await context.SaveChangesAsync();
                                Debug.WriteLine($"[Controller] Test Id={test.TestId} saved with {c.Questions?.Count ?? 0} questions");
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