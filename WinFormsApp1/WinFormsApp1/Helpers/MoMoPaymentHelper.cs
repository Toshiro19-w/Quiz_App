using WinFormsApp1.View.Dialogs;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.Helpers
{
    public static class MoMoPaymentHelper
    {
        /// <summary>
        /// Thanh toán toàn bộ giỏ hàng bằng MoMo
        /// </summary>
        public static async Task<bool> PayCartAsync(int userId, Form parentForm = null)
        {
            try
            {
                var paymentForm = new MoMoPaymentForm(userId);

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
        /// Thanh toán khóa học đơn lẻ bằng MoMo
        /// </summary>
        public static async Task<bool> PaySingleCourseAsync(int userId, int courseId, Form parentForm = null)
        {
            try
            {
                var paymentForm = new MoMoPaymentForm(userId, courseId);

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