using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.Controllers
{
    /// <summary>
    /// Partial class containing User Management methods for AdminController
    /// </summary>
    public partial class AdminController
    {
        // User Management
        public async Task<List<User>> GetUsersAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Users.Include(u => u.UserProfile).ToListAsync();
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Users.Include(u => u.UserProfile).FirstOrDefaultAsync(u => u.UserId == id);
            }
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tạo người dùng: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbUser = await context.Users.FindAsync(user.UserId);
                    if (dbUser == null) throw new Exception("Người dùng không tồn tại.");

                    dbUser.Email = user.Email;
                    dbUser.Username = user.Username;
                    dbUser.FullName = user.FullName;
                    dbUser.RoleId = user.RoleId;
                    dbUser.Status = user.Status;
                    // User entity does not define UpdatedAt; do not set it here

                    context.Users.Update(dbUser);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật người dùng: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var user = await context.Users.FindAsync(id);
                    if (user == null) throw new Exception("Người dùng không tồn tại.");

                    context.Users.Remove(user);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa người dùng: {ex.Message}");
                }
            }
        }
    }
}
