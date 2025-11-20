using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.Service.PaymentService
{
    public class MoMoPaymentService
    {
        private readonly string _partnerCode = "MOMO";
        private readonly string _accessKey = "F8BBA842ECF85";
        private readonly string _secretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";
        private readonly string _endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
        private readonly string _queryEndpoint = "https://test-payment.momo.vn/v2/gateway/api/query";
        private readonly string _redirectUrl = "http://localhost:5000/payment/momo-return";
        private readonly string _ipnUrl = "http://localhost:5000/payment/momo-callback";

        public async Task<MoMoResponse> CreatePaymentAsync(decimal amount, string orderInfo, string orderId)
        {
            try
            {
                var requestId = Guid.NewGuid().ToString();
                var extraData = "";
                var amountLong = (long)Math.Round(amount, 0);

                // Tạo signature
                var rawData = $"accessKey={_accessKey}&amount={amountLong}&extraData={extraData}&ipnUrl={_ipnUrl}&orderId={orderId}&orderInfo={orderInfo}&partnerCode={_partnerCode}&redirectUrl={_redirectUrl}&requestId={requestId}&requestType=captureWallet";
                var signature = GenerateSignature(rawData);

                var request = new
                {
                    partnerCode = _partnerCode,
                    accessKey = _accessKey,
                    requestId = requestId,
                    amount = amountLong,
                    orderId = orderId,
                    orderInfo = orderInfo,
                    redirectUrl = _redirectUrl,
                    ipnUrl = _ipnUrl,
                    extraData = extraData,
                    requestType = "captureWallet",
                    signature = signature,
                    lang = "vi"
                };

                using var httpClient = new HttpClient();
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(_endpoint, content);
                var responseJson = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<MoMoResponse>(responseJson) ?? new MoMoResponse();
            }
            catch (Exception ex)
            {
                return new MoMoResponse { resultCode = -1, message = ex.Message };
            }
        }

        /// <summary>
        /// Query payment status from MoMo gateway. This is used by the client polling logic.
        /// </summary>
        public async Task<MoMoQueryResponse> QueryPaymentStatusAsync(string orderId)
        {
            try
            {
                var requestId = Guid.NewGuid().ToString();
                var rawData = $"accessKey={_accessKey}&orderId={orderId}&partnerCode={_partnerCode}&requestId={requestId}";
                var signature = GenerateSignature(rawData);

                var request = new
                {
                    partnerCode = _partnerCode,
                    accessKey = _accessKey,
                    requestId = requestId,
                    orderId = orderId,
                    signature = signature,
                    lang = "vi"
                };

                using var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(30);
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(_queryEndpoint, content);
                var responseJson = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine($"MoMo Query Response: {responseJson}");

                var resp = JsonSerializer.Deserialize<MoMoQueryResponse>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (resp != null) return resp;

                var alt = JsonSerializer.Deserialize<MoMoResponse>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (alt != null)
                {
                    return new MoMoQueryResponse
                    {
                        resultCode = alt.resultCode,
                        message = alt.message,
                        status = alt.resultCode == 0 ? "SUCCESS" : "PENDING",
                        orderId = alt.orderId
                    };
                }

                return new MoMoQueryResponse { resultCode = -1, message = "Không thể phân tích phản hồi" };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"MoMo Query Error: {ex.Message}");
                return new MoMoQueryResponse { resultCode = -1, message = ex.Message };
            }
        }

        private string GenerateSignature(string rawData)
        {
            var keyBytes = Encoding.UTF8.GetBytes(_secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(rawData);

            using var hmac = new HMACSHA256(keyBytes);
            var hashBytes = hmac.ComputeHash(messageBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    public class MoMoResponse
    {
        public string partnerCode { get; set; } = "";
        public string orderId { get; set; } = "";
        public string requestId { get; set; } = "";
        public long amount { get; set; }
        public long responseTime { get; set; }
        public string message { get; set; } = "";
        public int resultCode { get; set; }
        public string payUrl { get; set; } = "";
        public string deeplink { get; set; } = "";
        public string qrCodeUrl { get; set; } = "";
    }

    public class MoMoQueryResponse
    {
        public int resultCode { get; set; }
        public string message { get; set; } = "";
        public string status { get; set; } = "";
        public string orderId { get; set; } = "";
        public long transId { get; set; }
        public string payType { get; set; } = "";
    }
}