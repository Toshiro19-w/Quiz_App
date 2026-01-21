using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WinFormsApp1.Services
{
    public class EmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _fromEmail;
        private readonly string _fromPassword;
        private readonly string _fromName;

        public EmailService()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();

            _smtpHost = configuration["EmailSettings:SmtpHost"];
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            _fromEmail = configuration["EmailSettings:FromEmail"];
            _fromPassword = configuration["EmailSettings:FromPassword"];
            _fromName = configuration["EmailSettings:FromName"];
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body, string attachmentPath = null)
        {
            try
            {
                var fromAddress = new MailAddress(_fromEmail, _fromName);
                var toAddress = new MailAddress(toEmail);

                var smtp = new SmtpClient
                {
                    Host = _smtpHost,
                    Port = _smtpPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, _fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    // Thêm file đính kèm nếu có
                    if (!string.IsNullOrEmpty(attachmentPath) && File.Exists(attachmentPath))
                    {
                        var attachment = new Attachment(attachmentPath);
                        message.Attachments.Add(attachment);
                    }

                    await smtp.SendMailAsync(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi gửi email: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendCertificateEmailAsync(string toEmail, string studentName, string courseTitle, string pdfPath)
        {
            string subject = $"🎓 Chứng chỉ hoàn thành khóa học: {courseTitle}";
            
            string body = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                    <div style='background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); padding: 30px; text-align: center; border-radius: 10px 10px 0 0;'>
                        <h1 style='color: white; margin: 0;'>🎉 Chúc mừng!</h1>
                    </div>
                    
                    <div style='background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px;'>
                        <h2 style='color: #333;'>Xin chào {studentName},</h2>
                        
                        <p style='font-size: 16px; line-height: 1.6; color: #555;'>
                            Chúc mừng bạn đã hoàn thành xuất sắc khóa học:
                        </p>
                        
                        <div style='background: white; padding: 20px; border-left: 4px solid #667eea; margin: 20px 0;'>
                            <h3 style='color: #667eea; margin: 0;'>{courseTitle}</h3>
                        </div>
                        
                        <p style='font-size: 16px; line-height: 1.6; color: #555;'>
                            Chứng chỉ của bạn đã được đính kèm trong email này. Đây là minh chứng cho sự nỗ lực và thành tích của bạn trong suốt quá trình học tập.
                        </p>
                        
                        <div style='background: #fff3cd; border: 1px solid #ffc107; padding: 15px; border-radius: 5px; margin: 20px 0;'>
                            <p style='margin: 0; color: #856404;'>
                                <strong>💡 Lưu ý:</strong> Vui lòng lưu trữ chứng chỉ này cẩn thận. Bạn có thể in ra hoặc chia sẻ trên mạng xã hội để khẳng định thành tích của mình!
                            </p>
                        </div>
                        
                        <p style='font-size: 16px; line-height: 1.6; color: #555;'>
                            Chúng tôi hy vọng bạn sẽ tiếp tục hành trình học tập và phát triển kỹ năng cùng với nền tảng của chúng tôi.
                        </p>
                        
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='#' style='background: #667eea; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block;'>
                                Khám phá thêm khóa học
                            </a>
                        </div>
                        
                        <hr style='border: none; border-top: 1px solid #ddd; margin: 30px 0;'>
                        
                        <p style='font-size: 14px; color: #999; text-align: center; margin: 0;'>
                            Trân trọng,<br>
                            <strong style='color: #667eea;'>{_fromName}</strong><br>
                            <em>Nền tảng học trực tuyến hàng đầu</em>
                        </p>
                    </div>
                </div>
            ";

            return await SendEmailAsync(toEmail, subject, body, pdfPath);
        }
    }
}
