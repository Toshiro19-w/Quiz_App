using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.Service
{
    public class CourseRecommendationService
    {
        private readonly LearningPlatformContext _context;

        public CourseRecommendationService(LearningPlatformContext context)
        {
            _context = context;
        }

        public async Task<List<RecommendedCourse>> GetRecommendedCoursesAsync(int? userId, int count = 6)
        {
            var allCourses = await _context.Courses
                .Include(c => c.Owner)
                .Include(c => c.Category)
                .Include(c => c.CoursePurchases)
                .Include(c => c.CourseReviews)
                .Where(c => c.IsPublished)
                .ToListAsync();

            if (userId.HasValue)
            {
                var purchasedCourseIds = await _context.CoursePurchases
                    .Where(p => p.BuyerId == userId.Value && p.Status == "Paid")
                    .Select(p => p.CourseId)
                    .ToListAsync();

                allCourses = allCourses.Where(c => !purchasedCourseIds.Contains(c.CourseId)).ToList();
            }

            var scoredCourses = new List<RecommendedCourse>();

            foreach (var course in allCourses)
            {
                var score = await CalculateCourseScoreAsync(course, userId);
                var reasons = GenerateRecommendationReasons(course, userId, score);

                scoredCourses.Add(new RecommendedCourse
                {
                    Course = course,
                    TotalScore = score.TotalScore,
                    ReasonTags = reasons,
                    ConfidenceLevel = CalculateConfidenceLevel(score.TotalScore)
                });
            }

            return scoredCourses
                .OrderByDescending(r => r.TotalScore)
                .Take(count)
                .ToList();
        }

        private async Task<CourseScore> CalculateCourseScoreAsync(Course course, int? userId)
        {
            var score = new CourseScore();

            if (userId.HasValue)
            {
                score.HistoryScore = await CalculateHistoryScoreAsync(course, userId.Value);
                score.BehaviorScore = await CalculateBehaviorScoreAsync(course, userId.Value);
            }
            else
            {
                score.HistoryScore = 0;
                score.BehaviorScore = 0;
            }

            score.PopularityScore = CalculatePopularityScore(course);
            score.TimePriceScore = CalculateTimePriceScore(course);

            // Điều chỉnh trọng số sau khi bỏ RelevanceScore (15%)
            const decimal HISTORY_WEIGHT = 0.40m;      // Tăng từ 35% → 40%
            const decimal BEHAVIOR_WEIGHT = 0.30m;     // Tăng từ 25% → 30%
            const decimal POPULARITY_WEIGHT = 0.25m;   // Tăng từ 20% → 25%
            const decimal TIME_PRICE_WEIGHT = 0.05m;   // Giữ nguyên 5%

            score.TotalScore = 
                (score.HistoryScore * HISTORY_WEIGHT) +
                (score.BehaviorScore * BEHAVIOR_WEIGHT) +
                (score.PopularityScore * POPULARITY_WEIGHT) +
                (score.TimePriceScore * TIME_PRICE_WEIGHT);

            return score;
        }

        private async Task<decimal> CalculateHistoryScoreAsync(Course course, int userId)
        {
            decimal score = 0;

            var purchasedCourses = await _context.CoursePurchases
                .Include(p => p.Course)
                    .ThenInclude(c => c.Category)
                .Where(p => p.BuyerId == userId && p.Status == "Paid")
                .Select(p => p.Course)
                .ToListAsync();

            if (!purchasedCourses.Any())
                return 0;

            if (course.CategoryId.HasValue && purchasedCourses.Any(p => p.CategoryId == course.CategoryId))
            {
                score += 60;
            }

            var sameInstructor = purchasedCourses.Any(p => p.OwnerId == course.OwnerId);
            if (sameInstructor)
            {
                score += 40;
            }

            return Math.Min(score, 100);
        }

        private async Task<decimal> CalculateBehaviorScoreAsync(Course course, int userId)
        {
            decimal score = 0;

            var cartItems = await _context.CartItems
                .Include(ci => ci.Cart)
                .Where(ci => ci.Cart.UserId == userId && ci.CourseId == course.CourseId)
                .ToListAsync();

            if (cartItems.Any())
            {
                score += 100;
            }

            return Math.Min(score, 100);
        }

        private decimal CalculatePopularityScore(Course course)
        {
            decimal score = 0;

            var purchaseCount = course.CoursePurchases?.Count ?? 0;
            if (purchaseCount > 100)
                score += 40;
            else if (purchaseCount > 50)
                score += 30;
            else if (purchaseCount > 20)
                score += 20;
            else if (purchaseCount > 0)
                score += 10;

            if (course.AverageRating >= 4.5m)
                score += 40;
            else if (course.AverageRating >= 4.0m)
                score += 30;
            else if (course.AverageRating >= 3.5m)
                score += 20;
            else if (course.AverageRating >= 3.0m)
                score += 10;

            if (course.TotalReviews > 50)
                score += 20;
            else if (course.TotalReviews > 20)
                score += 15;
            else if (course.TotalReviews > 10)
                score += 10;
            else if (course.TotalReviews > 0)
                score += 5;

            return Math.Min(score, 100);
        }

        private decimal CalculateTimePriceScore(Course course)
        {
            decimal score = 50;

            var daysSinceCreated = (DateTime.Now - course.CreatedAt).Days;
            if (daysSinceCreated <= 7)
            {
                score += 50;
            }
            else if (daysSinceCreated <= 30)
            {
                score += 30;
            }

            return Math.Min(score, 100);
        }

        private List<string> GenerateRecommendationReasons(Course course, int? userId, CourseScore score)
        {
            var reasons = new List<string>();

            if (score.HistoryScore > 50)
            {
                reasons.Add("Matches your learning history");
            }

            if (score.BehaviorScore > 80)
            {
                reasons.Add("Added to your cart");
            }

            if (course.AverageRating >= 4.5m)
            {
                reasons.Add("Highly rated");
            }

            if (course.CoursePurchases?.Count > 50)
            {
                reasons.Add("Popular");
            }

            var daysSinceCreated = (DateTime.Now - course.CreatedAt).Days;
            if (daysSinceCreated <= 7)
            {
                reasons.Add("New release");
            }

            if (course.Price == 0)
            {
                reasons.Add("Free");
            }

            return reasons;
        }

        private string CalculateConfidenceLevel(decimal totalScore)
        {
            if (totalScore >= 70)
                return "High";
            else if (totalScore >= 40)
                return "Medium";
            else
                return "Low";
        }
    }

    public class RecommendedCourse
    {
        public Course Course { get; set; }
        public decimal TotalScore { get; set; }
        public List<string> ReasonTags { get; set; }
        public string ConfidenceLevel { get; set; }
    }

    public class CourseScore
    {
        public decimal HistoryScore { get; set; }
        public decimal BehaviorScore { get; set; }
        public decimal PopularityScore { get; set; }
        public decimal TimePriceScore { get; set; }
        public decimal TotalScore { get; set; }
    }
}
