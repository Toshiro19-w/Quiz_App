using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Service.IService;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.Service
{
	internal class CourseService : ICourseService
	{
		private readonly LearningPlatformContext _context;
		private LearningPlatformContext learningPlatformContext;

		public CourseService(LearningPlatformContext context, object value)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public CourseService(LearningPlatformContext learningPlatformContext)
		{
			this.learningPlatformContext = learningPlatformContext;
		}

		public List<Course> GetAllPublishedCourses()
		{
			try
			{
				return _context.Courses
					.Include(c => c.Owner)
					.Where(c => c.IsPublished)
					.OrderByDescending(c => c.CreatedAt)
					.ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetAllPublishedCourses: {ex.Message}");
				return new List<Course>();
			}
		}

		public Course GetCourseById(int id)
		{
			try
			{
				return _context.Courses
					.Include(c => c.Owner)
					.FirstOrDefault(c => c.CourseId == id && c.IsPublished);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetCourseById: {ex.Message}");
				return null;
			}
		}

		public Course GetCourseBySlug(string slug)
		{
			try
			{
				return _context.Courses
					.Include(c => c.Owner)
					.FirstOrDefault(c => c.Slug == slug && c.IsPublished);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetCourseBySlug: {ex.Message}");
				return null;
			}
		}

		public Course GetCourseBySlugWithFullDetails(string slug)
		{
			try
			{
				var course = _context.Courses
					.Include(c => c.Owner)
					.Include(c => c.Category)
					.Include(c => c.CourseChapters)
						.ThenInclude(ch => ch.Lessons)
							.ThenInclude(l => l.LessonContents)
					.Include(c => c.CoursePurchases)
					.Include(c => c.CourseReviews)
						.ThenInclude(r => r.User)
					.FirstOrDefault(c => c.Slug == slug);

				if (course != null)
				{
					// Sort in memory after loading
					course.CourseChapters = course.CourseChapters
						.OrderBy(ch => ch.OrderIndex)
						.ToList();

					foreach (var chapter in course.CourseChapters)
					{
						chapter.Lessons = chapter.Lessons
							.OrderBy(l => l.OrderIndex)
							.ToList();

						foreach (var lesson in chapter.Lessons)
						{
							lesson.LessonContents = lesson.LessonContents
								.OrderBy(lc => lc.OrderIndex)
								.ToList();
						}
					}

					course.CourseReviews = course.CourseReviews
						.Where(r => r.IsApproved)
						.OrderByDescending(r => r.CreatedAt)
						.ToList();
				}

				return course;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetCourseBySlugWithFullDetails: {ex.Message}");
				return null;
			}
		}

		public List<Course> GetCoursesByCategory(string categorySlug)
		{
			try
			{
				return _context.Courses
					.Include(c => c.Owner)
					.Include(c => c.Category)
					.Where(c => c.IsPublished && c.Category != null && c.Category.Slug == categorySlug)
					.OrderByDescending(c => c.CreatedAt)
					.ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetCoursesByCategory: {ex.Message}");
				return new List<Course>();
			}
		}

		public List<Course> SearchCourses(string keyword)
		{
			try
			{
				return _context.Courses
					.Include(c => c.Owner)
					.Where(c => c.IsPublished &&
						(c.Title.Contains(keyword) || (c.Summary != null && c.Summary.Contains(keyword))))
					.OrderByDescending(c => c.CreatedAt)
					.ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in SearchCourses: {ex.Message}");
				return new List<Course>();
			}
		}

		public Course CreateCourse(CreateCourseViewModel model, int ownerId)
		{
			try
			{
				var course = new Course
				{
					OwnerId = ownerId,
					Title = model.Title,
					Slug = model.Slug,
					Summary = model.Description,
					CoverUrl = model.CoverUrl,
					Price = model.Price ?? 0,
					IsPublished = model.IsPublished,
					CreatedAt = DateTime.Now
				};

				_context.Courses.Add(course);
				_context.SaveChanges();

				return course;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in CreateCourse: {ex.Message}");
				return null;
			}
		}

		public bool IsSlugUnique(string slug)
		{
			try
			{
				return !_context.Courses.Any(c => c.Slug == slug);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in IsSlugUnique: {ex.Message}");
				return false;
			}
		}

		public bool UpdateCourse(EditCourseViewModel model)
		{
			try
			{
				var course = _context.Courses.Find(model.CourseId);
				if (course == null) return false;

				course.Title = model.Title;
				course.Slug = model.Slug;
				course.Summary = model.Description;
				course.Price = model.Price ?? 0;
				course.IsPublished = model.IsPublished;
				course.CoverUrl = model.CoverUrl;
				course.UpdatedAt = DateTime.Now;

				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in UpdateCourse: {ex.Message}");
				return false;
			}
		}

		public bool DeleteCourse(int courseId)
		{
			try
			{
				var course = _context.Courses.Find(courseId);
				if (course == null) return false;

				_context.Courses.Remove(course);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in DeleteCourse: {ex.Message}");
				return false;
			}
		}

		public List<Course> GetRecommendedCoursesForUser(int userId, int count = 6)
		{
			try
			{
				// Logic khuyến nghị đơn giản: dựa vao rating cao
				return _context.Courses
					.Include(c => c.Owner)
					.Include(c => c.Category)
					.Where(c => c.IsPublished)
					.OrderByDescending(c => c.AverageRating)
					.ThenByDescending(c => c.TotalReviews)
					.ThenByDescending(c => c.CreatedAt)
					.Take(count)
					.ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetRecommendedCoursesForUser: {ex.Message}");
				return new List<Course>();
			}
		}

		public List<Course> GetFilteredAndSortedCourses(string searchKeyword = null, string categorySlug = null, decimal? minRating = null, decimal? maxRating = null, bool? isFree = null, string sortBy = null)
		{
			try
			{
				var query = _context.Courses
					.Include(c => c.Owner)
					.Include(c => c.Category)
					.Where(c => c.IsPublished)
					.AsQueryable();

				if (!string.IsNullOrWhiteSpace(searchKeyword))
				{
					query = query.Where(c => c.Title.Contains(searchKeyword) || (c.Summary != null && c.Summary.Contains(searchKeyword)));
				}

				if (!string.IsNullOrWhiteSpace(categorySlug))
				{
					query = query.Where(c => c.Category != null && c.Category.Slug == categorySlug);
				}

				if (minRating.HasValue)
				{
					query = query.Where(c => c.AverageRating >= minRating.Value);
				}
				if (maxRating.HasValue)
				{
					query = query.Where(c => c.AverageRating < maxRating.Value);
				}

				if (isFree.HasValue)
				{
					query = query.Where(c => isFree.Value ? c.Price == 0 : c.Price > 0);
				}

				query = sortBy?.ToLower() switch
				{
					"rating" => query.OrderByDescending(c => c.AverageRating).ThenByDescending(c => c.TotalReviews),
					"price_asc" => query.OrderBy(c => c.Price),
					"price_desc" => query.OrderByDescending(c => c.Price),
					"newest" => query.OrderByDescending(c => c.CreatedAt),
					_ => query.OrderByDescending(c => c.CreatedAt)
				};

				return query.ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetFilteredAndSortedCourses: {ex.Message}");
				return new List<Course>();
			}
		}

		public List<Course> GetTopRatedCourses(int count = 6)
		{
			try
			{
				return _context.Courses
					.Include(c => c.Owner)
					.Include(c => c.Category)
					.Where(c => c.IsPublished && c.TotalReviews > 0)
					.OrderByDescending(c => c.AverageRating)
					.ThenByDescending(c => c.TotalReviews)
					.ThenByDescending(c => c.CreatedAt)
					.Take(count)
					.ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetTopRatedCourses: {ex.Message}");
				return new List<Course>();
			}
		}
	}
}
