using WinFormsApp1.Service.PaymentService;

namespace WinFormsApp1.Helpers
{
    public static class MoMoTestHelper
    {
        /// <summary>
        /// Test query trạng thái thanh toán MoMo
        /// </summary>
        public static async Task<string> TestQueryPaymentAsync(string orderId)
        {
            var service = new MoMoPaymentService();
            var result = await service.QueryPaymentStatusAsync(orderId);
            
            return $"ResultCode: {result.resultCode}\n" +
                   $"Message: {result.message}\n" +
                   $"Status: {result.status}\n" +
                   $"OrderId: {result.orderId}\n" +
                   $"TransId: {result.transId}";
        }

        /// <summary>
        /// Hoàn tất thanh toán thủ công (dùng khi MoMo test không trả về kết quả)
        /// </summary>
        public static async Task<bool> ManualCompletePaymentAsync(string orderId)
        {
            var service = new CartPaymentService();
            return await service.CompletePaymentAsync(orderId);
        }
    }
}
