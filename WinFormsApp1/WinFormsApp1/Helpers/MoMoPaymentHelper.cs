using WinFormsApp1.View.Dialogs;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.Helpers
{
    public static class MoMoPaymentHelper
    {
        /// <summary>
        /// Thanh toán toàn bộ giỏ hàng bằng MoMo (có hỗ trợ mã giảm giá)
        /// </summary>
        /// <param name="userId">ID người dùng</param>
        /// <param name="parentForm">Form cha</param>
        /// <param name="discountId">ID mã giảm giá (nếu có)</param>
        /// <param name="discountAmount">Số tiền giảm giá</param>
        public static async Task<bool> PayCartAsync(int userId, Form parentForm = null, int? discountId = null, decimal discountAmount = 0)
        {
            try
            {
                var paymentForm = new MoMoPaymentForm(userId, null, discountId, discountAmount);

                if (parentForm != null)
                {
                    var result = paymentForm.ShowDialog(parentForm);
                    return result == DialogResult.OK && paymentForm.PaymentCompleted;
                }
                else
                {
                    var result = paymentForm.ShowDialog();
                    return result == DialogResult.OK && paymentForm.PaymentCompleted;
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(null, $"Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Thanh toán khóa học đơn lẻ bằng MoMo (có hỗ trợ mã giảm giá)
        /// </summary>
        /// <param name="userId">ID người dùng</param>
        /// <param name="courseId">ID khóa học</param>
        /// <param name="parentForm">Form cha</param>
        /// <param name="discountId">ID mã giảm giá (nếu có)</param>
        /// <param name="discountAmount">Số tiền giảm giá</param>
        public static async Task<bool> PaySingleCourseAsync(int userId, int courseId, Form parentForm = null, int? discountId = null, decimal discountAmount = 0)
        {
            try
            {
                var paymentForm = new MoMoPaymentForm(userId, courseId, discountId, discountAmount);

                if (parentForm != null)
                {
                    var result = paymentForm.ShowDialog(parentForm);
                    return result == DialogResult.OK && paymentForm.PaymentCompleted;
                }
                else
                {
                    var result = paymentForm.ShowDialog();
                    return result == DialogResult.OK && paymentForm.PaymentCompleted;
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(null, $"Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Thanh toán subscription bằng MoMo
        /// </summary>
        /// <param name="userId">ID người dùng</param>
        /// <param name="durationMonths">Số tháng đăng ký (1, 6, hoặc 12)</param>
        /// <param name="parentForm">Form cha</param>
        public static async Task<bool> PaySubscriptionAsync(int userId, int durationMonths, Form parentForm = null)
        {
            try
            {
                var paymentForm = new MoMoSubscriptionPaymentForm(userId, durationMonths);

                if (parentForm != null)
                {
                    var result = paymentForm.ShowDialog(parentForm);
                    return result == DialogResult.OK && paymentForm.PaymentCompleted;
                }
                else
                {
                    var result = paymentForm.ShowDialog();
                    return result == DialogResult.OK && paymentForm.PaymentCompleted;
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Show(null, $"Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Hiển thị dialog xác nhận thanh toán
        /// </summary>
        public static bool ConfirmPayment(decimal amount, string itemName)
        {
            var message = $"Bạn có chắc muốn thanh toán {amount:N0} VND cho {itemName}?";
            var result = MessageBox.Show(
                message,
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            return result == DialogResult.Yes;
        }
    }
}