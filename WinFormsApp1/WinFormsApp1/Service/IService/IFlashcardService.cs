using System.Collections.Generic;
using System.Threading.Tasks;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.Service.IService
{
    public interface IFlashcardService
    {
        /// <summary>
        /// Lấy danh sách tất cả FlashcardSet công khai
        /// </summary>
        Task<List<FlashcardSet>> GetAllPublicFlashcardSetsAsync();

        /// <summary>
        /// Lấy danh sách FlashcardSet của người dùng
        /// </summary>
        Task<List<FlashcardSet>> GetUserFlashcardSetsAsync(int userId);

        /// <summary>
        /// Lấy chi tiết FlashcardSet theo ID (bao gồm Owner và Flashcards)
        /// </summary>
        Task<FlashcardSet> GetFlashcardSetByIdAsync(int setId);

        /// <summary>
        /// Tạo FlashcardSet mới
        /// </summary>
        Task<bool> CreateFlashcardSetAsync(FlashcardSet flashcardSet, List<Flashcard> flashcards);

        /// <summary>
        /// Cập nhật FlashcardSet
        /// </summary>
        Task<bool> UpdateFlashcardSetAsync(FlashcardSet flashcardSet);

        /// <summary>
        /// Xóa FlashcardSet (soft delete)
        /// </summary>
        Task<bool> DeleteFlashcardSetAsync(int setId);

        /// <summary>
        /// Lấy tất cả Flashcard trong một Set
        /// </summary>
        Task<List<Flashcard>> GetFlashcardsInSetAsync(int setId);

        /// <summary>
        /// Thêm Flashcard mới vào Set
        /// </summary>
        Task<bool> AddFlashcardToSetAsync(Flashcard flashcard);

        /// <summary>
        /// Cập nhật Flashcard
        /// </summary>
        Task<bool> UpdateFlashcardAsync(Flashcard flashcard);

        /// <summary>
        /// Xóa Flashcard
        /// </summary>
        Task<bool> DeleteFlashcardAsync(int cardId);

        /// <summary>
        /// Lấy số lượng Flashcard trong Set
        /// </summary>
        Task<int> GetFlashcardCountInSetAsync(int setId);

        /// <summary>
        /// Tìm kiếm FlashcardSet theo từ khóa
        /// </summary>
        Task<List<FlashcardSet>> SearchFlashcardSetsAsync(string keyword);

        /// <summary>
        /// Lấy FlashcardSet phổ biến nhất (theo số lượng thẻ)
        /// </summary>
        Task<List<FlashcardSet>> GetPopularFlashcardSetsAsync(int count = 4);

        /// <summary>
        /// Kiểm tra quyền sở hữu FlashcardSet
        /// </summary>
        Task<bool> IsOwnerAsync(int setId, int userId);

        /// <summary>
        /// Lưu FlashcardSet vào Library (SavedItems)
        /// </summary>
        Task<bool> SaveFlashcardSetToLibraryAsync(int setId, int userId, int? folderId = null, string note = null);

        /// <summary>
        /// Lấy danh sách FlashcardSet đã lưu trong Library
        /// </summary>
        Task<List<SavedItem>> GetSavedFlashcardSetsAsync(int userId);

        /// <summary>
        /// Xóa FlashcardSet khỏi Library
        /// </summary>
        Task<bool> RemoveFlashcardSetFromLibraryAsync(int savedItemId);

        /// <summary>
        /// Ghi nhận log thực hành (FlashcardPracticeLog)
        /// </summary>
        Task<bool> LogPracticeSessionAsync(FlashcardPracticeLog log);

        /// <summary>
        /// Lấy lịch sử thực hành của người dùng cho một Set
        /// </summary>
        Task<List<FlashcardPracticeLog>> GetPracticeLogsAsync(int userId, int setId);
    }
}
