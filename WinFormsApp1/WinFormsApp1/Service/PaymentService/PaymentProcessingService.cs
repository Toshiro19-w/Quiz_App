using System;
using System.Threading.Tasks;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.Service.PaymentService
{
    public class PaymentProcessingService : IDisposable
    {
        private readonly LearningPlatformContext _context;

        public PaymentProcessingService()
        {
            _context = new LearningPlatformContext();
        }

        /// <summary>
        /// Xử lý thanh toán khóa học
        /// </summary>
        public async Task<bool> ProcessCoursePaymentAsync(int courseId, int buyerId, decimal amount)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                // Kiểm tra khóa học tồn tại
                var course = await _context.Courses.FindAsync(courseId);
                if (course == null)
                    throw new Exception("Khóa học không tồn tại");

                // Kiểm tra người dùng đã mua chưa
                var existingPurchase = await _context.CoursePurchases
                    .FirstOrDefaultAsync(cp => cp.CourseId == courseId && cp.BuyerId == buyerId);
                
                if (existingPurchase != null)
                    throw new Exception("Bạn đã sở hữu khóa học này");

                // Tạo đơn hàng
                var order = new Order
                {
                    BuyerId = buyerId,
                    TotalAmount = amount,
                    Currency = "VND",
                    Status = "Paid",
                    CreatedAt = DateTime.Now,
                    PaidAt = DateTime.Now
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Tạo item đơn hàng
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    CourseId = courseId,
                    Price = amount
                };
                _context.OrderItems.Add(orderItem);

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
                _context.CoursePurchases.Add(coursePurchase);

                // Tạo bản ghi thanh toán
                var payment = new Payment
                {
                    OrderId = order.OrderId,
                    Provider = "VietQR",
                    ProviderRef = GenerateTransactionId(),
                    Amount = amount,
                    Currency = "VND",
                    Status = "Completed",
                    PaidAt = DateTime.Now
                };
                _context.Payments.Add(payment);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Kiểm tra người dùng đã mua khóa học chưa
        /// </summary>
        public async Task<bool> HasUserPurchasedCourseAsync(int courseId, int userId)
        {
            return await _context.CoursePurchases
                .AnyAsync(cp => cp.CourseId == courseId && 
                               cp.BuyerId == userId && 
                               cp.Status == "Paid");
        }

        /// <summary>
        /// Lấy thông tin mua khóa học
        /// </summary>
        public async Task<CoursePurchase> GetCoursePurchaseAsync(int courseId, int userId)
        {
            return await _context.CoursePurchases
                .Include(cp => cp.Course)
                .Include(cp => cp.Buyer)
                .FirstOrDefaultAsync(cp => cp.CourseId == courseId && 
                                          cp.BuyerId == userId && 
                                          cp.Status == "Paid");
        }

        /// <summary>
        /// Tạo mã giao dịch
        /// </summary>
        private string GenerateTransactionId()
        {
            return $"VQR{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(1000, 9999)}";
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}