using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.Service.IService
{
	internal interface ICourseService
	{
		List<Course> GetAllPublishedCourses();
		Course GetCourseById(int id);
		Course GetCourseBySlug(string slug);
		Course GetCourseBySlugWithFullDetails(string slug);
		List<Course> GetCoursesByCategory(string categorySlug);
		List<Course> SearchCourses(string keyword);
		Course CreateCourse(CreateCourseViewModel model, int ownerId);
		bool IsSlugUnique(string slug);
		bool UpdateCourse(EditCourseViewModel model);
		bool DeleteCourse(int courseId);
		List<Course> GetRecommendedCoursesForUser(int userId, int count = 6);
		List<Course> GetFilteredAndSortedCourses(string searchKeyword = null, string categorySlug = null, decimal? minRating = null, decimal? maxRating = null, bool? isFree = null, string sortBy = null);
		List<Course> GetTopRatedCourses(int count = 6);
	}
}
