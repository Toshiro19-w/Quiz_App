using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Service.IService;

namespace WinFormsApp1.Service
{
    public class FlashcardService : IFlashcardService
    {
        /// <summary>
        /// Lấy danh sách tất cả FlashcardSet công khai
        /// </summary>
        public async Task<List<FlashcardSet>> GetAllPublicFlashcardSetsAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.FlashcardSets
                    .Include(fs => fs.Owner)
                    .Include(fs => fs.Flashcards)
                    .Where(fs => fs.Visibility == "Public" && !fs.IsDeleted)
                    .OrderByDescending(fs => fs.CreatedAt)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Lấy danh sách FlashcardSet của người dùng
        /// </summary>
        public async Task<List<FlashcardSet>> GetUserFlashcardSetsAsync(int userId)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.FlashcardSets
                    .Include(fs => fs.Owner)
                    .Include(fs => fs.Flashcards)
                    .Where(fs => fs.OwnerId == userId && !fs.IsDeleted)
                    .OrderByDescending(fs => fs.CreatedAt)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Lấy chi tiết FlashcardSet theo ID (bao gồm Owner và Flashcards)
        /// </summary>
        public async Task<FlashcardSet> GetFlashcardSetByIdAsync(int setId)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.FlashcardSets
                    .Include(fs => fs.Owner)
                    .Include(fs => fs.Flashcards)
                    .FirstOrDefaultAsync(fs => fs.SetId == setId && !fs.IsDeleted);
            }
        }

        /// <summary>
        /// Tạo FlashcardSet mới
        /// </summary>
        public async Task<bool> CreateFlashcardSetAsync(FlashcardSet flashcardSet, List<Flashcard> flashcards)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    // Validation
                    if (string.IsNullOrWhiteSpace(flashcardSet.Title))
                        throw new ArgumentException("Tiêu đề không được để trống.");

                    if (flashcardSet.Title.Length > 200)
                        throw new ArgumentException("Tiêu đề quá dài (tối đa 200 ký tự).");

                    if (flashcards == null || flashcards.Count == 0)
                        throw new ArgumentException("Phải có ít nhất 1 thẻ flashcard.");

                    // Đặt các giá trị mặc định
                    flashcardSet.CreatedAt = DateTime.Now;
                    flashcardSet.IsDeleted = false;

                    // Thêm FlashcardSet
                    context.FlashcardSets.Add(flashcardSet);
                    await context.SaveChangesAsync();

                    // Thêm các Flashcard vào Set vừa tạo
                    int orderIndex = 1;
                    foreach (var flashcard in flashcards)
                    {
                        if (!string.IsNullOrWhiteSpace(flashcard.FrontText) && 
                            !string.IsNullOrWhiteSpace(flashcard.BackText))
                        {
                            flashcard.SetId = flashcardSet.SetId;
                            flashcard.OrderIndex = orderIndex++;
                            flashcard.CreatedAt = DateTime.Now;
                            context.Flashcards.Add(flashcard);
                        }
                    }

                    await context.SaveChangesAsync();
                    return true;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tạo flashcard set: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Cập nhật FlashcardSet
        /// </summary>
        public async Task<bool> UpdateFlashcardSetAsync(FlashcardSet flashcardSet)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbSet = await context.FlashcardSets.FindAsync(flashcardSet.SetId);
                    if (dbSet == null)
                        throw new Exception("FlashcardSet không tồn tại.");

                    if (string.IsNullOrWhiteSpace(flashcardSet.Title))
                        throw new ArgumentException("Tiêu đề không được để trống.");

                    if (flashcardSet.Title.Length > 200)
                        throw new ArgumentException("Tiêu đề quá dài (tối đa 200 ký tự).");

                    // Cập nhật các trường được phép
                    dbSet.Title = flashcardSet.Title;
                    dbSet.Description = flashcardSet.Description;
                    dbSet.Visibility = flashcardSet.Visibility;
                    dbSet.Language = flashcardSet.Language;
                    dbSet.TagsText = flashcardSet.TagsText;
                    dbSet.CoverUrl = flashcardSet.CoverUrl;
                    dbSet.UpdatedAt = DateTime.Now;

                    context.FlashcardSets.Update(dbSet);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật flashcard set: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Xóa FlashcardSet (soft delete)
        /// </summary>
        public async Task<bool> DeleteFlashcardSetAsync(int setId)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var flashcardSet = await context.FlashcardSets.FindAsync(setId);
                    if (flashcardSet == null)
                        throw new Exception("FlashcardSet không tồn tại.");

                    // Soft delete
                    flashcardSet.IsDeleted = true;
                    flashcardSet.UpdatedAt = DateTime.Now;

                    context.FlashcardSets.Update(flashcardSet);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa flashcard set: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Lấy tất cả Flashcard trong một Set
        /// </summary>
        public async Task<List<Flashcard>> GetFlashcardsInSetAsync(int setId)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Flashcards
                    .Where(f => f.SetId == setId)
                    .OrderBy(f => f.OrderIndex)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Thêm Flashcard mới vào Set
        /// </summary>
        public async Task<bool> AddFlashcardToSetAsync(Flashcard flashcard)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(flashcard.FrontText))
                        throw new ArgumentException("Mặt trước không được để trống.");

                    if (string.IsNullOrWhiteSpace(flashcard.BackText))
                        throw new ArgumentException("Mặt sau không được để trống.");

                    flashcard.CreatedAt = DateTime.Now;

                    context.Flashcards.Add(flashcard);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi thêm flashcard: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Cập nhật Flashcard
        /// </summary>
        public async Task<bool> UpdateFlashcardAsync(Flashcard flashcard)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbCard = await context.Flashcards.FindAsync(flashcard.CardId);
                    if (dbCard == null)
                        throw new Exception("Flashcard không tồn tại.");

                    if (string.IsNullOrWhiteSpace(flashcard.FrontText))
                        throw new ArgumentException("Mặt trước không được để trống.");

                    if (string.IsNullOrWhiteSpace(flashcard.BackText))
                        throw new ArgumentException("Mặt sau không được để trống.");

                    dbCard.FrontText = flashcard.FrontText;
                    dbCard.BackText = flashcard.BackText;
                    dbCard.Hint = flashcard.Hint;
                    dbCard.OrderIndex = flashcard.OrderIndex;
                    dbCard.UpdatedAt = DateTime.Now;

                    context.Flashcards.Update(dbCard);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật flashcard: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Xóa Flashcard
        /// </summary>
        public async Task<bool> DeleteFlashcardAsync(int cardId)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var flashcard = await context.Flashcards.FindAsync(cardId);
                    if (flashcard == null)
                        throw new Exception("Flashcard không tồn tại.");

                    context.Flashcards.Remove(flashcard);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa flashcard: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Lấy số lượng Flashcard trong Set
        /// </summary>
        public async Task<int> GetFlashcardCountInSetAsync(int setId)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Flashcards.CountAsync(f => f.SetId == setId);
            }
        }

        /// <summary>
        /// Tìm kiếm FlashcardSet theo từ khóa
        /// </summary>
        public async Task<List<FlashcardSet>> SearchFlashcardSetsAsync(string keyword)
        {
            using (var context = new LearningPlatformContext())
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return await GetAllPublicFlashcardSetsAsync();

                return await context.FlashcardSets
                    .Include(fs => fs.Owner)
                    .Include(fs => fs.Flashcards)
                    .Where(fs => fs.Visibility == "Public" && 
                                !fs.IsDeleted &&
                                (fs.Title.Contains(keyword) || 
                                 fs.Description.Contains(keyword) ||
                                 fs.TagsText.Contains(keyword)))
                    .OrderByDescending(fs => fs.CreatedAt)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Lấy FlashcardSet phổ biến nhất (theo số lượng thẻ)
        /// </summary>
        public async Task<List<FlashcardSet>> GetPopularFlashcardSetsAsync(int count = 4)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.FlashcardSets
                    .Include(fs => fs.Owner)
                    .Include(fs => fs.Flashcards)
                    .Where(fs => fs.Visibility == "Public" && !fs.IsDeleted)
                    .OrderByDescending(fs => fs.Flashcards.Count)
                    .Take(count)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Kiểm tra quyền sở hữu FlashcardSet
        /// </summary>
        public async Task<bool> IsOwnerAsync(int setId, int userId)
        {
            using (var context = new LearningPlatformContext())
            {
                var flashcardSet = await context.FlashcardSets.FindAsync(setId);
                return flashcardSet != null && flashcardSet.OwnerId == userId;
            }
        }

        /// <summary>
        /// Lưu FlashcardSet vào Library (SavedItems)
        /// </summary>
        public async Task<bool> SaveFlashcardSetToLibraryAsync(int setId, int userId, int? folderId = null, string note = null)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    // Kiểm tra xem FlashcardSet có tồn tại không
                    var flashcardSet = await context.FlashcardSets.FindAsync(setId);
                    if (flashcardSet == null)
                        throw new Exception("FlashcardSet không tồn tại.");

                    // Lấy hoặc tạo Library mặc định của user
                    var library = await context.Libraries.FirstOrDefaultAsync(l => l.OwnerId == userId);
                    if (library == null)
                    {
                        library = new Library
                        {
                            OwnerId = userId,
                            Name = "My Library",
                            CreatedAt = DateTime.Now
                        };
                        context.Libraries.Add(library);
                        await context.SaveChangesAsync();
                    }

                    // Kiểm tra xem đã lưu chưa
                    var existingSaved = await context.SavedItems
                        .FirstOrDefaultAsync(si => si.LibraryId == library.LibraryId && 
                                                   si.ContentType == "FlashcardSet" && 
                                                   si.ContentId == setId);

                    if (existingSaved != null)
                        throw new Exception("FlashcardSet đã được lưu trước đó.");

                    // Tạo SavedItem mới
                    var savedItem = new SavedItem
                    {
                        LibraryId = library.LibraryId,
                        FolderId = folderId,
                        ContentType = "FlashcardSet",
                        ContentId = setId,
                        Note = note,
                        AddedAt = DateTime.Now
                    };

                    context.SavedItems.Add(savedItem);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lưu flashcard set: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Lấy danh sách FlashcardSet đã lưu trong Library
        /// </summary>
        public async Task<List<SavedItem>> GetSavedFlashcardSetsAsync(int userId)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.SavedItems
                    .Include(si => si.Library)
                    .Where(si => si.Library.OwnerId == userId && si.ContentType == "FlashcardSet")
                    .OrderByDescending(si => si.AddedAt)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Xóa FlashcardSet khỏi Library
        /// </summary>
        public async Task<bool> RemoveFlashcardSetFromLibraryAsync(int savedItemId)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var savedItem = await context.SavedItems.FindAsync(savedItemId);
                    if (savedItem == null)
                        throw new Exception("SavedItem không tồn tại.");

                    context.SavedItems.Remove(savedItem);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa flashcard set khỏi library: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Ghi nhận log thực hành (FlashcardPracticeLog)
        /// </summary>
        public async Task<bool> LogPracticeSessionAsync(FlashcardPracticeLog log)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    log.ReviewedAt = DateTime.Now;
                    context.FlashcardPracticeLogs.Add(log);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi ghi log thực hành: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Lấy lịch sử thực hành của người dùng cho một Set
        /// </summary>
        public async Task<List<FlashcardPracticeLog>> GetPracticeLogsAsync(int userId, int setId)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.FlashcardPracticeLogs
                    .Where(log => log.UserId == userId && log.SetId == setId)
                    .OrderByDescending(log => log.ReviewedAt)
                    .ToListAsync();
            }
        }
    }
}
