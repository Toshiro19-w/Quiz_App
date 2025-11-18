using System;
using System.Drawing;
using QRCoder;
using System.Text;

namespace WinFormsApp1.Service.PaymentService
{
    /// <summary>
    /// Service to generate VietQR code for payment
    /// VietQR is Vietnam's QR code standard for banking
    /// </summary>
    public class VietQRService
    {
        /// <summary>
        /// Generate VietQR code for payment
        /// </summary>
        public static Image GenerateVietQRCode(decimal amount, string description = null)
        {
            try
            {
                VietQRConfig.Validate();
                description ??= VietQRConfig.DefaultDescription;

                string qrContent = GenerateVietQRContent(amount, description);

                using (var qrGenerator = new QRCodeGenerator())
                {
                    var qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.M);
                    using (var qrCode = new QRCode(qrCodeData))
                    {
                        var qrImage = qrCode.GetGraphic(VietQRConfig.QRCodePixelPerModule, Color.Black, Color.White, true);
                        return new Bitmap(qrImage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tạo mã VietQR: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Generate VietQR content according to Vietnam's QR standard
        /// </summary>
        private static string GenerateVietQRContent(decimal amount, string description)
        {
            // VietQR format theo chuẩn EMVCo
            var payload = new StringBuilder();
            
            // Payload Format Indicator
            payload.Append("000201");
            
            // Point of Initiation Method
            payload.Append("010212");
            
            // Merchant Account Information
            var merchantInfo = $"0010A000000727012{VietQRConfig.BankBinCode.PadLeft(6, '0')}{VietQRConfig.AccountNumber}";
            payload.Append($"38{merchantInfo.Length:D2}{merchantInfo}");
            
            // Transaction Currency (VND = 704)
            payload.Append("5303704");
            
            // Transaction Amount
            var amountStr = amount.ToString("F0");
            payload.Append($"54{amountStr.Length:D2}{amountStr}");
            
            // Country Code
            payload.Append("5802VN");
            
            // Merchant Name
            var merchantName = VietQRConfig.AccountName;
            payload.Append($"59{merchantName.Length:D2}{merchantName}");
            
            // Additional Data Field
            if (!string.IsNullOrEmpty(description))
            {
                payload.Append($"62{description.Length + 4:D2}08{description.Length:D2}{description}");
            }
            
            // CRC (sẽ được tính sau)
            payload.Append("6304");
            
            // Tính CRC16
            var crc = CalculateCRC16(payload.ToString());
            payload.Append(crc.ToString("X4"));
            
            return payload.ToString();
        }
        
        /// <summary>
        /// Calculate CRC16 for VietQR
        /// </summary>
        private static ushort CalculateCRC16(string data)
        {
            ushort crc = 0xFFFF;
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            
            foreach (byte b in bytes)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0)
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    else
                        crc <<= 1;
                }
            }
            
            return crc;
        }

        /// <summary>
        /// Generate QR code with payment information
        /// </summary>
        public static Image GeneratePaymentQRCode(decimal amount, string courseName, int courseId)
        {
            string description = $"Thanh toan khoa hoc {courseId}";
            return GenerateVietQRCode(amount, description);
        }

        /// <summary>
        /// Get payment information for display
        /// </summary>
        public static PaymentInfo GetPaymentInfo(decimal amount)
        {
            return new PaymentInfo
            {
                BankName = VietQRConfig.BankName,
                AccountName = VietQRConfig.AccountName,
                AccountNumber = VietQRConfig.AccountNumber,
                Amount = amount,
                Description = VietQRConfig.DefaultDescription,
                QRImage = GenerateVietQRCode(amount)
            };
        }

        public class PaymentInfo
        {
            public string BankName { get; set; }
            public string AccountName { get; set; }
            public string AccountNumber { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public Image QRImage { get; set; }
        }
    }
}