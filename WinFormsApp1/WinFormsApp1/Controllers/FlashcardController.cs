using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Service.IService;
using WinFormsApp1.Service;

namespace WinFormsApp1.Controllers
{
    public class FlashcardController
    {
        private readonly IFlashcardService _flashcardService;

        public FlashcardController()
        {
            _flashcardService = new FlashcardService();
        }

        public FlashcardController(IFlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }

        #region FlashcardSet Management

        /// <summary>
        /// Lấy danh sách tất cả FlashcardSet công khai
        /// </summary>
        public async Task<List<FlashcardSet>> GetAllPublicFlashcardSetsAsync()
        {
            try
            {
                return await _flashcardService.GetAllPublicFlashcardSetsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách flashcard set: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy danh sách FlashcardSet của người dùng
        /// </summary>
        public async Task<List<FlashcardSet>> GetUserFlashcardSetsAsync(int userId)
        {
            try
            {
                return await _flashcardService.GetUserFlashcardSetsAsync(userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy flashcard set của người dùng: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy chi tiết FlashcardSet theo ID
        /// </summary>
        public async Task<FlashcardSet> GetFlashcardSetByIdAsync(int setId)
        {
            try
            {
                var flashcardSet = await _flashcardService.GetFlashcardSetByIdAsync(setId);
                if (flashcardSet == null)
                    throw new Exception("Không tìm thấy FlashcardSet.");

                return flashcardSet;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy chi tiết flashcard set: {ex.Message}");
            }
        }

        /// <summary>
        /// Tạo FlashcardSet mới
        /// </summary>
        public async Task<bool> CreateFlashcardSetAsync(FlashcardSet flashcardSet, List<Flashcard> flashcards)
        {
            try
            {
                return await _flashcardService.CreateFlashcardSetAsync(flashcardSet, flashcards);
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

        /// <summary>
        /// Cập nhật FlashcardSet
        /// </summary>
        public async Task<bool> UpdateFlashcardSetAsync(FlashcardSet flashcardSet)
        {
            try
            {
                return await _flashcardService.UpdateFlashcardSetAsync(flashcardSet);
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

        /// <summary>
        /// Xóa FlashcardSet (soft delete)
        /// </summary>
        public async Task<bool> DeleteFlashcardSetAsync(int setId)
        {
            try
            {
                return await _flashcardService.DeleteFlashcardSetAsync(setId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa flashcard set: {ex.Message}");
            }
        }

        #endregion

        #region Flashcard Management

        /// <summary>
        /// Lấy tất cả Flashcard trong một Set
        /// </summary>
        public async Task<List<Flashcard>> GetFlashcardsInSetAsync(int setId)
        {
            try
            {
                return await _flashcardService.GetFlashcardsInSetAsync(setId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách flashcard: {ex.Message}");
            }
        }

        /// <summary>
        /// Thêm Flashcard mới vào Set
        /// </summary>
        public async Task<bool> AddFlashcardToSetAsync(Flashcard flashcard)
        {
            try
            {
                return await _flashcardService.AddFlashcardToSetAsync(flashcard);
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

        /// <summary>
        /// Cập nhật Flashcard
        /// </summary>
        public async Task<bool> UpdateFlashcardAsync(Flashcard flashcard)
        {
            try
            {
                return await _flashcardService.UpdateFlashcardAsync(flashcard);
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

        /// <summary>
        /// Xóa Flashcard
        /// </summary>
        public async Task<bool> DeleteFlashcardAsync(int cardId)
        {
            try
            {
                return await _flashcardService.DeleteFlashcardAsync(cardId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa flashcard: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy số lượng Flashcard trong Set
        /// </summary>
        public async Task<int> GetFlashcardCountInSetAsync(int setId)
        {
            try
            {
                return await _flashcardService.GetFlashcardCountInSetAsync(setId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi đếm flashcard: {ex.Message}");
            }
        }

        #endregion

        #region Search & Popular

        /// <summary>
        /// Tìm kiếm FlashcardSet theo từ khóa
        /// </summary>
        public async Task<List<FlashcardSet>> SearchFlashcardSetsAsync(string keyword)
        {
            try
            {
                return await _flashcardService.SearchFlashcardSetsAsync(keyword);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tìm kiếm flashcard set: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy FlashcardSet phổ biến nhất
        /// </summary>
        public async Task<List<FlashcardSet>> GetPopularFlashcardSetsAsync(int count = 4)
        {
            try
            {
                return await _flashcardService.GetPopularFlashcardSetsAsync(count);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy flashcard set phổ biến: {ex.Message}");
            }
        }

        #endregion

        #region Authorization

        /// <summary>
        /// Kiểm tra quyền sở hữu FlashcardSet
        /// </summary>
        public async Task<bool> IsOwnerAsync(int setId, int userId)
        {
            try
            {
                return await _flashcardService.IsOwnerAsync(setId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi kiểm tra quyền sở hữu: {ex.Message}");
            }
        }

        #endregion

        #region Library Management

        /// <summary>
        /// Lưu FlashcardSet vào Library
        /// </summary>
        public async Task<bool> SaveFlashcardSetToLibraryAsync(int setId, int userId, int? folderId = null, string note = null)
        {
            try
            {
                return await _flashcardService.SaveFlashcardSetToLibraryAsync(setId, userId, folderId, note);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu flashcard set: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy danh sách FlashcardSet đã lưu
        /// </summary>
        public async Task<List<SavedItem>> GetSavedFlashcardSetsAsync(int userId)
        {
            try
            {
                return await _flashcardService.GetSavedFlashcardSetsAsync(userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy flashcard set đã lưu: {ex.Message}");
            }
        }

        /// <summary>
        /// Xóa FlashcardSet khỏi Library
        /// </summary>
        public async Task<bool> RemoveFlashcardSetFromLibraryAsync(int savedItemId)
        {
            try
            {
                return await _flashcardService.RemoveFlashcardSetFromLibraryAsync(savedItemId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa flashcard set khỏi library: {ex.Message}");
            }
        }

        #endregion

        #region Practice Logs

        /// <summary>
        /// Ghi nhận log thực hành
        /// </summary>
        public async Task<bool> LogPracticeSessionAsync(FlashcardPracticeLog log)
        {
            try
            {
                return await _flashcardService.LogPracticeSessionAsync(log);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi ghi log thực hành: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy lịch sử thực hành
        /// </summary>
        public async Task<List<FlashcardPracticeLog>> GetPracticeLogsAsync(int userId, int setId)
        {
            try
            {
                return await _flashcardService.GetPracticeLogsAsync(userId, setId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy lịch sử thực hành: {ex.Message}");
            }
        }

        #endregion
    }
}
