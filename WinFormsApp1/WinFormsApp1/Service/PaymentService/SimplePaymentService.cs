using System;
using System.Threading.Tasks;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.Service.PaymentService
{
    public static class SimplePaymentService
    {
        /// <summary>
        /// Xử lý thanh toán khóa học đơn giản
        /// </summary>
        public static async Task<bool> ProcessCoursePaymentAsync(int courseId, int buyerId, decimal amount)
        {
            try
            {
                using var context = new LearningPlatformContext();
                
                // Kiểm tra đã mua chưa
                var existingPurchase = await context.CoursePurchases
                    .FirstOrDefaultAsync(cp => cp.CourseId == courseId && cp.BuyerId == buyerId);
                
                if (existingPurchase != null)
                    throw new Exception("Bạn đã sở hữu khóa học này");

                // Tạo bản ghi mua khóa học
                var coursePurchase = new CoursePurchase
                {
                    CourseId = courseId,
                    BuyerId = buyerId,
                    PricePaid = amount,
                    Currency = "VND",
                    Status = "Paid",
                    PurchasedAt = DateTime.Now
                };
                
                context.CoursePurchases.Add(coursePurchase);
                await context.SaveChangesAsync();
                
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Xử lý thanh toán nhiều khóa học từ giỏ hàng
        /// </summary>
        public static async Task<bool> ProcessCartPaymentAsync(int userId)
        {
            try
            {
                using var context = new LearningPlatformContext();
                
                var cart = await context.ShoppingCarts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Course)
                    .FirstOrDefaultAsync(c => c.UserId == userId);
                
                if (cart == null || !cart.CartItems.Any())
                    throw new Exception("Giỏ hàng trống");

                foreach (var item in cart.CartItems)
                {
                    var existingPurchase = await context.CoursePurchases
                        .FirstOrDefaultAsync(cp => cp.CourseId == item.CourseId && cp.BuyerId == userId);
                    
                    if (existingPurchase == null)
                    {
                        var coursePurchase = new CoursePurchase
                        {
                            CourseId = item.CourseId,
                            BuyerId = userId,
                            PricePaid = item.Course.Price,
                            Currency = "VND",
                            Status = "Paid",
                            PurchasedAt = DateTime.Now
                        };
                        context.CoursePurchases.Add(coursePurchase);
                    }
                }
                
                context.CartItems.RemoveRange(cart.CartItems);
                await context.SaveChangesAsync();
                
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Kiểm tra người dùng đã mua khóa học chưa
        /// </summary>
        public static async Task<bool> HasUserPurchasedCourseAsync(int courseId, int userId)
        {
            try
            {
                using var context = new LearningPlatformContext();
                return await context.CoursePurchases
                    .AnyAsync(cp => cp.CourseId == courseId && 
                                   cp.BuyerId == userId && 
                                   cp.Status == "Paid");
            }
            catch
            {
                return false;
            }
        }
    }
}