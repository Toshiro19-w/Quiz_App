using System;
using System.Threading.Tasks;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.Helpers
{
    public static class CartHelper
    {
        /// <summary>
        /// Add a course to the current user's shopping cart. Returns a tuple (success, message).
        /// success = true -> newly added
        /// success = false -> already in cart or failure
        /// </summary>
        public static async Task<(bool Success, string Message)> AddCourseToCartAsync(int courseId, Form ownerForm)
        {
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue)
            {
                return (false, "Vui lòng ??ng nh?p ?? thêm vào gi? hàng!");
            }

            try
            {
                using var context = new LearningPlatformContext();

                var cart = await context.ShoppingCarts
                    .FirstOrDefaultAsync(c => c.UserId == userId.Value);

                if (cart == null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId.Value,
                        CreatedAt = DateTime.Now
                    };
                    context.ShoppingCarts.Add(cart);
                    await context.SaveChangesAsync();
                }

                var existingItem = await context.CartItems
                    .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.CourseId == courseId);

                if (existingItem == null)
                {
                    var cartItem = new CartItem
                    {
                        CartId = cart.CartId,
                        CourseId = courseId,
                        AddedAt = DateTime.Now
                    };
                    context.CartItems.Add(cartItem);
                    await context.SaveChangesAsync();

                    return (true, "?ã thêm khóa h?c vào gi? hàng!");
                }

                return (false, "Khóa h?c ?ã có trong gi? hàng!");
            }
            catch (Exception ex)
            {
                // Log if necessary
                return (false, "L?i khi thêm vào gi? hàng: " + ex.Message);
            }
        }
    }
}
