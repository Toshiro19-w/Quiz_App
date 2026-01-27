using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Services;

namespace WinFormsApp1.Service.PaymentService
{
    public class SubscriptionPaymentService
    {
        private readonly LearningPlatformContext _context;
        private readonly MoMoPaymentService _momoService;
        private readonly EmailService _emailService;

        public SubscriptionPaymentService()
        {
            _context = new LearningPlatformContext();
            _momoService = new MoMoPaymentService();
            _emailService = new EmailService();
        }

        /// <summary>
        /// Thanh toán subscription với MoMo
        /// </summary>
        /// <param name="userId">ID người dùng</param>
        /// <param name="durationMonths">Số tháng đăng ký (1, 6, hoặc 12)</param>
        /// <returns>PaymentResult chứa thông tin thanh toán</returns>
        public async Task<PaymentResult> PaySubscriptionWithMoMoAsync(int userId, int durationMonths)
        {
            try
            {
                // Tự động hủy giao dịch subscription pending cũ
                var pendingPayments = await _context.Payments
                    .Include(p => p.Order)
                    .Where(p => p.Order.BuyerId == userId && p.Status == "Pending" && p.Provider == "MoMo")
                    .ToListAsync();

                foreach (var p in pendingPayments)
                {
                    p.Status = "Failed";
                    if (p.Order != null) p.Order.Status = "Failed";
                }
                await _context.SaveChangesAsync();

                // Lấy thông tin gói subscription
                var plan = await _context.SubscriptionPlans
                    .FirstOrDefaultAsync(p => p.DurationMonths == durationMonths);

                if (plan == null)
                {
                    return new PaymentResult { Success = false, Message = "Gói subscription không tồn tại" };
                }

                // Kiểm tra subscription hiện tại
                var currentSubscription = await _context.UserSubscriptions
                    .Where(s => s.UserId == userId && s.Status == "Active" && s.ExpiresAt > DateTime.UtcNow)
                    .FirstOrDefaultAsync();

                // 1. Tạo Order cho subscription
                var order = new Order
                {
                    BuyerId = userId,
                    TotalAmount = plan.Price,
                    OriginalAmount = plan.Price,
                    Currency = "VND",
                    Status = "Pending",
                    CreatedAt = DateTime.Now
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // 2. Lưu thông tin subscription vào Order metadata (để xử lý sau)
                // Không tạo UserSubscription ở đây vì Status chỉ có: Active, Expired, Cancelled, Suspended (không có Pending)
                
                // 3. Tạo Payment và lưu thông tin duration
                var orderIdStr = $"SUB_{userId}_{durationMonths}_{DateTime.Now:yyyyMMddHHmmss}";
                var payment = new Payment
                {
                    OrderId = order.OrderId,
                    Provider = "MoMo",
                    Amount = plan.Price,
                    Currency = "VND",
                    Status = "Pending",
                    RawPayload = orderIdStr
                };

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                // 4. Gọi MoMo API
                var planName = durationMonths switch
                {
                    1 => "1 tháng",
                    6 => "6 tháng",
                    12 => "1 năm",
                    _ => $"{durationMonths} tháng"
                };

                var orderInfo = $"Đăng ký gói Premium {planName}";
                var momo = await _momoService.CreatePaymentAsync(plan.Price, orderInfo, orderIdStr);

                if (momo.resultCode == 0)
                {
                    payment.ProviderRef = momo.orderId;
                    await _context.SaveChangesAsync();

                    return new PaymentResult
                    {
                        Success = true,
                        PaymentUrl = momo.payUrl,
                        OrderId = orderIdStr,
                        Message = "Tạo thanh toán thành công",
                        InternalOrderId = order.OrderId
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

        /// <summary>
        /// Hoàn tất thanh toán subscription
        /// </summary>
        public async Task<bool> CompleteSubscriptionPaymentAsync(string orderId)
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
                payment.Order.PaidAt = DateTime.UtcNow;

                // 3. Lấy thông tin duration từ orderId (SUB_{userId}_{durationMonths}_{timestamp})
                var parts = orderId.Split('_');
                if (parts.Length < 3 || !int.TryParse(parts[1], out int userId) || !int.TryParse(parts[2], out int durationMonths))
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                // 4. Kiểm tra subscription hiện tại
                var currentSubscription = await _context.UserSubscriptions
                    .Where(s => s.UserId == userId && s.Status == "Active")
                    .OrderByDescending(s => s.ExpiresAt)
                    .FirstOrDefaultAsync();

                DateTime startDate;
                DateTime endDate;

                if (currentSubscription != null && currentSubscription.ExpiresAt > DateTime.UtcNow)
                {
                    // Đánh dấu subscription cũ là Expired
                    currentSubscription.Status = "Expired";
                    
                    // Gia hạn từ ngày hết hạn
                    startDate = currentSubscription.ExpiresAt;
                    endDate = startDate.AddMonths(durationMonths);
                }
                else
                {
                    // Subscription mới
                    startDate = DateTime.UtcNow;
                    endDate = startDate.AddMonths(durationMonths);
                }

                // 5. Tạo subscription mới với Status = "Active"
                var newSubscription = new UserSubscription
                {
                    UserId = userId,
                    Status = "Active",
                    SubscribedAt = startDate,
                    ExpiresAt = endDate
                };

                _context.UserSubscriptions.Add(newSubscription);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Gửi email thông báo thành công
                try
                {
                    var user = await _context.Users.FindAsync(userId);
                    if (user != null && !string.IsNullOrEmpty(user.Email))
                    {
                        // Fire-and-forget email sending (không chờ)
                        _ = _emailService.SendSubscriptionSuccessEmailAsync(
                            user.Email, 
                            user.FullName, 
                            durationMonths, 
                            endDate
                        );
                    }
                }
                catch (Exception emailEx)
                {
                    // Log lỗi gửi email nhưng không ảnh hưởng đến thanh toán
                    System.Diagnostics.Debug.WriteLine($"Email sending error: {emailEx.Message}");
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Complete subscription payment error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Hủy các subscription pending
        /// </summary>
        public async Task<bool> CancelPendingSubscriptionAsync(int userId)
        {
            try
            {
                var pending = await _context.Payments
                    .Include(p => p.Order)
                    .Where(p => p.Order.BuyerId == userId && p.Status == "Pending" && p.RawPayload.StartsWith("SUB_"))
                    .ToListAsync();

                foreach (var p in pending)
                {
                    p.Status = "Cancelled";
                    if (p.Order != null) p.Order.Status = "Cancelled";
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
