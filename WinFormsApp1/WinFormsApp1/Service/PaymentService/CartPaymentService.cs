using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.Service.PaymentService
{
    public class CartPaymentService
    {
        private readonly LearningPlatformContext _context;
        private readonly MoMoPaymentService _momoService;

        public CartPaymentService()
        {
            _context = new LearningPlatformContext();
            _momoService = new MoMoPaymentService();
        }

        public async Task<PaymentResult> PayCartWithMoMoAsync(int userId)
        {
            try
            {
                // Tự động hủy giao dịch pending cũ
                var pendingPayments = await _context.Payments
                    .Include(p => p.Order)
                    .Where(p => p.Order.BuyerId == userId && p.Status == "Pending" && p.Provider == "MoMo")
                    .ToListAsync();

                foreach (var p in pendingPayments)
                {
                    p.Status = "Failed";
                    if (p.Order != null) p.Order.Status = "Failed";
                }

                var pendingPurchases = await _context.CoursePurchases
                    .Where(cp => cp.BuyerId == userId && cp.Status == "Pending")
                    .ToListAsync();
                _context.CoursePurchases.RemoveRange(pendingPurchases);
                await _context.SaveChangesAsync();

                var cart = await _context.ShoppingCarts
                    .Include(sc => sc.CartItems)
                        .ThenInclude(ci => ci.Course)
                    .FirstOrDefaultAsync(sc => sc.UserId == userId);

                if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
                {
                    return new PaymentResult { Success = false, Message = "Giỏ hàng trống" };
                }

                var cartItems = cart.CartItems.ToList();
                var total = cartItems.Sum(x => x.Course.Price);

                // 2. Tạo Order
                var order = new Order
                {
                    BuyerId = userId,
                    TotalAmount = total,
                    Status = "Pending",
                    CreatedAt = DateTime.Now
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // 3. Tạo OrderItems
                foreach (var item in cartItems)
                {
                    _context.OrderItems.Add(new OrderItem
                    {
                        OrderId = order.OrderId,
                        CourseId = item.CourseId,
                        Price = item.Course.Price
                    });
                }
                await _context.SaveChangesAsync();

                // 4. Tạo Purchase (Pending)
                foreach (var item in cartItems)
                {
                    _context.CoursePurchases.Add(new CoursePurchase
                    {
                        BuyerId = userId,
                        CourseId = item.CourseId,
                        PricePaid = item.Course.Price,
                        Currency = "VND",
                        Status = "Pending",
                        PurchasedAt = DateTime.UtcNow
                    });
                }
                await _context.SaveChangesAsync();

                // 5. Tạo Payment
                var orderIdStr = $"CART_{order.OrderId}_{DateTime.Now:yyyyMMddHHmmss}";
                var payment = new Payment
                {
                    OrderId = order.OrderId,
                    Provider = "MoMo",
                    Amount = total,
                    Currency = "VND",
                    Status = "Pending",
                    RawPayload = orderIdStr
                };

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                // 6. Gọi MoMo API
                var momo = await _momoService.CreatePaymentAsync(total, "Thanh toán khóa học", orderIdStr);

                if (momo.resultCode == 0)
                {
                    payment.ProviderRef = momo.orderId;
                    await _context.SaveChangesAsync();

                    return new PaymentResult
                    {
                        Success = true,
                        PaymentUrl = momo.payUrl,
                        OrderId = orderIdStr,
                        Message = "Tạo thanh toán thành công"
                    };
                }

                return new PaymentResult { Success = false, Message = momo.message };
            }
            catch (DbUpdateException dbEx)
            {
                var baseMsg = dbEx.GetBaseException()?.Message ?? dbEx.Message;
                return new PaymentResult { Success = false, Message = "Lỗi khi lưu dữ liệu: " + baseMsg };
            }
            catch (Exception ex)
            {
                return new PaymentResult { Success = false, Message = $"Lỗi: {ex.Message}" };
            }
        }

        public async Task<PaymentResult> PaySingleCourseWithMoMoAsync(int userId, int courseId)
        {
            try
            {
                // Tự động hủy giao dịch pending cũ
                var pendingPayments = await _context.Payments
                    .Include(p => p.Order)
                    .Where(p => p.Order.BuyerId == userId && p.Status == "Pending" && p.Provider == "MoMo")
                    .ToListAsync();

                foreach (var p in pendingPayments)
                {
                    p.Status = "Failed";
                    if (p.Order != null) p.Order.Status = "Failed";
                }

                var pendingPurchases = await _context.CoursePurchases
                    .Where(cp => cp.BuyerId == userId && cp.Status == "Pending")
                    .ToListAsync();
                _context.CoursePurchases.RemoveRange(pendingPurchases);
                await _context.SaveChangesAsync();

                var course = await _context.Courses.FindAsync(courseId);
                if (course == null)
                {
                    return new PaymentResult { Success = false, Message = "Khóa học không tồn tại" };
                }

                var hasPurchased = await _context.CoursePurchases
                    .AnyAsync(p => p.BuyerId == userId && p.CourseId == courseId && p.Status == "Paid");

                if (hasPurchased)
                {
                    return new PaymentResult { Success = false, Message = "Bạn đã sở hữu khóa học này" };
                }

                // 1. Tạo Order
                var order = new Order
                {
                    BuyerId = userId,
                    TotalAmount = course.Price,
                    Status = "Pending",
                    CreatedAt = DateTime.Now
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // 2. Tạo OrderItem
                _context.OrderItems.Add(new OrderItem
                {
                    OrderId = order.OrderId,
                    CourseId = courseId,
                    Price = course.Price
                });
                await _context.SaveChangesAsync();

                // 3. Tạo Purchase (Pending)
                _context.CoursePurchases.Add(new CoursePurchase
                {
                    BuyerId = userId,
                    CourseId = courseId,
                    PricePaid = course.Price,
                    Currency = "VND",
                    Status = "Pending",
                    PurchasedAt = DateTime.UtcNow
                });
                await _context.SaveChangesAsync();

                // 4. Tạo Payment
                var orderIdStr = $"COURSE_{courseId}_{DateTime.Now:yyyyMMddHHmmss}";
                var payment = new Payment
                {
                    OrderId = order.OrderId,
                    Provider = "MoMo",
                    Amount = course.Price,
                    Currency = "VND",
                    Status = "Pending",
                    RawPayload = orderIdStr
                };

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                // 5. Gọi MoMo API
                var momo = await _momoService.CreatePaymentAsync(course.Price, $"Mua khóa học: {course.Title}", orderIdStr);

                if (momo.resultCode == 0)
                {
                    payment.ProviderRef = momo.orderId;
                    await _context.SaveChangesAsync();

                    return new PaymentResult
                    {
                        Success = true,
                        PaymentUrl = momo.payUrl,
                        OrderId = orderIdStr,
                        Message = "Tạo thanh toán thành công"
                    };
                }

                return new PaymentResult { Success = false, Message = momo.message };
            }
            catch (DbUpdateException dbEx)
            {
                var baseMsg = dbEx.GetBaseException()?.Message ?? dbEx.Message;
                return new PaymentResult { Success = false, Message = "Lỗi khi lưu dữ liệu: " + baseMsg };
            }
            catch (Exception ex)
            {
                return new PaymentResult { Success = false, Message = $"Lỗi: {ex.Message}" };
            }
        }

        public async Task<bool> CompletePaymentAsync(string orderId)
        {
            try
            {
                var payment = await _context.Payments
                    .Include(p => p.Order)
                    .FirstOrDefaultAsync(p => p.RawPayload == orderId);

                if (payment == null || payment.Status != "Pending")
                    return false;

                using var transaction = await _context.Database.BeginTransactionAsync();

                // 1. Cập nhật Payment
                payment.Status = "Paid";
                payment.PaidAt = DateTime.UtcNow;

                // 2. Cập nhật Order
                payment.Order.Status = "Paid";

                // 3. Cập nhật Purchases
                var orderItems = await _context.OrderItems
                    .Where(i => i.OrderId == payment.Order.OrderId)
                    .ToListAsync();

                foreach (var item in orderItems)
                {
                    var purchase = await _context.CoursePurchases
                        .FirstOrDefaultAsync(x =>
                            x.BuyerId == payment.Order.BuyerId &&
                            x.CourseId == item.CourseId &&
                            x.Status == "Pending");

                    if (purchase != null)
                    {
                        purchase.Status = "Paid";
                        purchase.PurchasedAt = DateTime.UtcNow;
                    }
                }

                // 4. Xóa giỏ hàng: tìm ShoppingCart của người mua và xóa CartItems
                var cart = await _context.ShoppingCarts
                    .Include(sc => sc.CartItems)
                    .FirstOrDefaultAsync(sc => sc.UserId == payment.Order.BuyerId);

                if (cart != null && cart.CartItems != null && cart.CartItems.Any())
                {
                    _context.CartItems.RemoveRange(cart.CartItems);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CancelPendingPaymentAsync(int userId)
        {
            try
            {
                var pending = await _context.Payments
                    .Include(p => p.Order)
                    .Where(p => p.Order.BuyerId == userId && p.Status == "Pending")
                    .ToListAsync();

                foreach (var p in pending)
                {
                    p.Status = "Cancelled";
                    if (p.Order != null) p.Order.Status = "Cancelled";
                }

                var pendingPurchases = await _context.CoursePurchases
                    .Where(cp => cp.BuyerId == userId && cp.Status == "Pending")
                    .ToListAsync();

                _context.CoursePurchases.RemoveRange(pendingPurchases);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class PaymentResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string PaymentUrl { get; set; } = "";
        public string OrderId { get; set; } = "";
    }
}