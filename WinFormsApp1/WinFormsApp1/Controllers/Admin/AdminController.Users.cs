using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.ViewModels;

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
                    
                    // Log action
                    await AuditHelper.LogUserCreateAsync(user);
                    
                    return true;
                }
                catch (Exception ex)
                {
                    await AuditHelper.LogErrorAsync(AuditActions.UserCreate, AuditEntityTypes.User, null, ex);
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

                    // Capture old values for audit
                    var beforeData = new
                    {
                        dbUser.Username,
                        dbUser.Email,
                        dbUser.FullName,
                        dbUser.RoleId,
                        dbUser.Status
                    };

                    // Check for role change
                    var oldRoleId = dbUser.RoleId;
                    var oldStatus = dbUser.Status;

                    dbUser.Email = user.Email;
                    dbUser.Username = user.Username;
                    dbUser.FullName = user.FullName;
                    dbUser.RoleId = user.RoleId;
                    dbUser.Status = user.Status;

                    context.Users.Update(dbUser);
                    await context.SaveChangesAsync();

                    // Log specific changes
                    if (oldRoleId != user.RoleId)
                    {
                        await AuditHelper.LogUserRoleChangeAsync(user.UserId, oldRoleId, user.RoleId);
                    }
                    else if (oldStatus != user.Status)
                    {
                        await AuditHelper.LogUserStatusChangeAsync(user.UserId, oldStatus, user.Status);
                    }
                    else
                    {
                        // General update log
                        var afterData = new
                        {
                            user.Username,
                            user.Email,
                            user.FullName,
                            user.RoleId,
                            user.Status
                        };
                        await AuditHelper.LogChangeAsync(AuditActions.UserUpdate, AuditEntityTypes.User, user.UserId, beforeData, afterData);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    await AuditHelper.LogErrorAsync(AuditActions.UserUpdate, AuditEntityTypes.User, user.UserId, ex);
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

                    // Log before delete
                    await AuditHelper.LogUserDeleteAsync(user);

                    context.Users.Remove(user);
                    await context.SaveChangesAsync();
                    
                    return true;
                }
                catch (Exception ex)
                {
                    await AuditHelper.LogErrorAsync(AuditActions.UserDelete, AuditEntityTypes.User, id, ex);
                    throw new Exception($"Lỗi khi xóa người dùng: {ex.Message}");
                }
            }
        }
    }
}
