using System;

namespace WinFormsApp1.Service.PaymentService
{
    /// <summary>
    /// Configuration for VietQR Payment
    /// Dễ dàng thay đổi thông tin ngân hàng mà không cần sửa code
    /// </summary>
    public class VietQRConfig
    {
        /// <summary>
        /// Bank BIN Code - Mã định danh ngân hàng
        /// 970415 = Vietcombank
        /// Danh sách các ngân hàng: https://api.vietqr.io/v2/banks
        /// </summary>
        public static string BankBinCode { get; set; } = "970415";

        /// <summary>
        /// Account Number - Số tài khoản nhận tiền
        /// </summary>
        public static string AccountNumber { get; set; } = "1025839148";

        /// <summary>
        /// Account Holder Name - Tên chủ tài khoản
        /// </summary>
        public static string AccountName { get; set; } = "QUIZ APP";

        /// <summary>
        /// Bank Display Name - Tên ngân hàng hiển thị
        /// </summary>
        public static string BankName { get; set; } = "Vietcombank";

        /// <summary>
        /// Payment Timeout in seconds - Thời gian hết hạn thanh toán
        /// </summary>
        public static int PaymentTimeoutSeconds { get; set; } = 300; // 5 minutes

        /// <summary>
        /// Default Payment Description - Mô tả thanh toán mặc định
        /// </summary>
        public static string DefaultDescription { get; set; } = "Thanh toan khoa hoc";

        /// <summary>
        /// QR Code Size multiplier
        /// </summary>
        public static int QRCodePixelPerModule { get; set; } = 8;

        /// <summary>
        /// Validate configuration
        /// </summary>
        public static void Validate()
        {
            if (string.IsNullOrEmpty(BankBinCode))
                throw new InvalidOperationException("BankBinCode không được để trống");

            if (string.IsNullOrEmpty(AccountNumber))
                throw new InvalidOperationException("AccountNumber không được để trống");

            if (string.IsNullOrEmpty(AccountName))
                throw new InvalidOperationException("AccountName không được để trống");

            if (PaymentTimeoutSeconds <= 0)
                throw new InvalidOperationException("PaymentTimeoutSeconds phải > 0");

            if (BankBinCode.Length != 6)
                throw new InvalidOperationException("BankBinCode phải có 6 ký tự");
        }

        /// <summary>
        /// Get bank info for display
        /// </summary>
        public static (string BankCode, string BankName, string AccountNo, string AccountHolderName) GetBankInfo()
        {
            return (BankBinCode, BankName, AccountNumber, AccountName);
        }
    }
}