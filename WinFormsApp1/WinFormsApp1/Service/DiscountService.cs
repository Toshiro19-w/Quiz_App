using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.Service
{
    /// <summary>
    /// Service xử lý logic mã giảm giá
    /// </summary>
    public class DiscountService
    {
        /// <summary>
        /// Kết quả validate mã giảm giá
        /// </summary>
        public class DiscountValidationResult
        {
            public bool IsValid { get; set; }
            public string Message { get; set; } = string.Empty;
            public Discount? Discount { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal FinalAmount { get; set; }
        }

        /// <summary>
        /// Validate và tính toán giảm giá
        /// </summary>
        public static async Task<DiscountValidationResult> ValidateDiscountAsync(
            string code, 
            int userId, 
            decimal orderAmount, 
            List<int>? courseIds = null)
        {
            using var context = new LearningPlatformContext();
            
            var result = new DiscountValidationResult
            {
                FinalAmount = orderAmount
            };

            // 1. Tìm mã giảm giá
            var discount = await context.Discounts
                .Include(d => d.DiscountCourses)
                .FirstOrDefaultAsync(d => d.Code.ToUpper() == code.ToUpper().Trim());

            if (discount == null)
            {
                result.Message = "Mã giảm giá không tồn tại";
                return result;
            }

            // 2. Kiểm tra trạng thái - use Status instead of IsActive
            if (discount.Status != "Active")
            {
                result.Message = "Mã giảm giá đã ngừng hoạt động";
                return result;
            }

            // 3. Kiểm tra thời gian hiệu lực
            var now = DateTime.UtcNow;
            if (now < discount.StartDate)
            {
                result.Message = $"Mã giảm giá chưa có hiệu lực (bắt đầu từ {discount.StartDate:dd/MM/yyyy})";
                return result;
            }

            if (now > discount.EndDate)
            {
                result.Message = "Mã giảm giá đã hết hạn";
                return result;
            }

            // 4. Kiểm tra số lần sử dụng tối đa
            if (discount.UsageLimit.HasValue && discount.UsageCount >= discount.UsageLimit.Value)
            {
                result.Message = "Mã giảm giá đã hết lượt sử dụng";
                return result;
            }

            // 5. Kiểm tra số lần sử dụng của user
            if (discount.UsageLimitPerUser.HasValue)
            {
                var userUsageCount = await context.DiscountUsages
                    .CountAsync(du => du.DiscountId == discount.DiscountId && du.UserId == userId);

                if (userUsageCount >= discount.UsageLimitPerUser.Value)
                {
                    result.Message = "Bạn đã sử dụng hết lượt cho mã giảm giá này";
                    return result;
                }
            }

            // 6. Kiểm tra giá trị đơn hàng tối thiểu
            if (discount.MinOrderAmount.HasValue && orderAmount < discount.MinOrderAmount.Value)
            {
                result.Message = $"Đơn hàng tối thiểu {discount.MinOrderAmount.Value:N0} VNĐ để áp dụng mã này";
                return result;
            }

            // 7. Kiểm tra áp dụng cho khóa học cụ thể
            if (!discount.ApplyToAllCourses && courseIds != null && courseIds.Any())
            {
                var applicableCourseIds = discount.DiscountCourses.Select(dc => dc.CourseId).ToList();
                var validCourseIds = courseIds.Intersect(applicableCourseIds).ToList();

                if (!validCourseIds.Any())
                {
                    result.Message = "Mã giảm giá không áp dụng cho các khóa học trong đơn hàng";
                    return result;
                }
            }

            // 8. Tính toán số tiền giảm
            decimal discountAmount = 0;
            if (discount.DiscountType == "Percentage")
            {
                discountAmount = orderAmount * discount.DiscountValue / 100;
                
                // Áp dụng giới hạn giảm tối đa
                if (discount.MaxDiscountAmount.HasValue && discountAmount > discount.MaxDiscountAmount.Value)
                {
                    discountAmount = discount.MaxDiscountAmount.Value;
                }
            }
            else // FixedAmount
            {
                discountAmount = discount.DiscountValue;
            }

            // Đảm bảo không giảm quá giá trị đơn hàng
            if (discountAmount > orderAmount)
            {
                discountAmount = orderAmount;
            }

            result.IsValid = true;
            result.Discount = discount;
            result.DiscountAmount = discountAmount;
            result.FinalAmount = orderAmount - discountAmount;
            result.Message = $"Áp dụng thành công! Giảm {discountAmount:N0} VNĐ";

            return result;
        }

        /// <summary>
        /// Ghi nhận sử dụng mã giảm giá
        /// </summary>
        public static async Task RecordUsageAsync(int discountId, int userId, int orderId, decimal discountAmount)
        {
            using var context = new LearningPlatformContext();

            // Tạo bản ghi sử dụng
            var usage = new DiscountUsage
            {
                DiscountId = discountId,
                UserId = userId,
                OrderId = orderId,
                DiscountAmount = discountAmount,
                UsedAt = DateTime.UtcNow
            };
            context.DiscountUsages.Add(usage);

            // Tăng số lần sử dụng
            var discount = await context.Discounts.FindAsync(discountId);
            if (discount != null)
            {
                discount.UsageCount++;
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Lấy danh sách mã giảm giá đang hoạt động
        /// </summary>
        public static async Task<List<Discount>> GetActiveDiscountsAsync()
        {
            using var context = new LearningPlatformContext();
            var now = DateTime.UtcNow;

            return await context.Discounts
                .Where(d => d.Status == "Active" 
                    && d.StartDate <= now 
                    && d.EndDate >= now
                    && (!d.UsageLimit.HasValue || d.UsageCount < d.UsageLimit.Value))
                .OrderByDescending(d => d.DiscountValue)
                .ToListAsync();
        }

        /// <summary>
        /// Lấy tất cả mã giảm giá (cho admin)
        /// </summary>
        public static async Task<List<Discount>> GetAllDiscountsAsync()
        {
            using var context = new LearningPlatformContext();

            return await context.Discounts
                .Include(d => d.Creator)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// Tạo mã giảm giá mới
        /// </summary>
        public static async Task<Discount> CreateDiscountAsync(Discount discount)
        {
            using var context = new LearningPlatformContext();

            // Kiểm tra mã đã tồn tại chưa
            var exists = await context.Discounts
                .AnyAsync(d => d.Code.ToUpper() == discount.Code.ToUpper());

            if (exists)
            {
                throw new InvalidOperationException("Mã giảm giá đã tồn tại");
            }

            discount.Code = discount.Code.ToUpper().Trim();
            discount.CreatedAt = DateTime.UtcNow;
            discount.UsageCount = 0;

            context.Discounts.Add(discount);
            await context.SaveChangesAsync();

            return discount;
        }

        /// <summary>
        /// Cập nhật mã giảm giá
        /// </summary>
        public static async Task<bool> UpdateDiscountAsync(Discount discount)
        {
            using var context = new LearningPlatformContext();

            var existing = await context.Discounts.FindAsync(discount.DiscountId);
            if (existing == null) return false;

            existing.Name = discount.Name;
            existing.Description = discount.Description;
            existing.DiscountType = discount.DiscountType;
            existing.DiscountValue = discount.DiscountValue;
            existing.MinOrderAmount = discount.MinOrderAmount;
            existing.MaxDiscountAmount = discount.MaxDiscountAmount;
            existing.UsageLimit = discount.UsageLimit;
            existing.UsageLimitPerUser = discount.UsageLimitPerUser;
            existing.StartDate = discount.StartDate;
            existing.EndDate = discount.EndDate;
            existing.Status = discount.Status;
            existing.ApplyToAllCourses = discount.ApplyToAllCourses;
            existing.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Xóa mã giảm giá
        /// </summary>
        public static async Task<bool> DeleteDiscountAsync(int discountId)
        {
            using var context = new LearningPlatformContext();

            var discount = await context.Discounts.FindAsync(discountId);
            if (discount == null) return false;

            // Kiểm tra đã có ai sử dụng chưa
            var hasUsage = await context.DiscountUsages.AnyAsync(du => du.DiscountId == discountId);
            if (hasUsage)
            {
                // Đánh dấu inactive thay vì xóa
                discount.Status = "Inactive";
                discount.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                context.Discounts.Remove(discount);
            }

            await context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Lấy thống kê mã giảm giá
        /// </summary>
        public static async Task<DiscountStatistics> GetStatisticsAsync(int discountId)
        {
            using var context = new LearningPlatformContext();

            var discount = await context.Discounts.FindAsync(discountId);
            if (discount == null) return new DiscountStatistics();

            var usages = await context.DiscountUsages
                .Where(du => du.DiscountId == discountId)
                .ToListAsync();

            return new DiscountStatistics
            {
                TotalUsages = usages.Count,
                TotalDiscountAmount = usages.Sum(u => u.DiscountAmount),
                UniqueUsers = usages.Select(u => u.UserId).Distinct().Count(),
                LastUsedAt = usages.OrderByDescending(u => u.UsedAt).FirstOrDefault()?.UsedAt
            };
        }

        public class DiscountStatistics
        {
            public int TotalUsages { get; set; }
            public decimal TotalDiscountAmount { get; set; }
            public int UniqueUsers { get; set; }
            public DateTime? LastUsedAt { get; set; }
        }
    }
}
