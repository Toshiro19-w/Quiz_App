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
    /// Partial class containing Test and Question Management methods for AdminController
    /// </summary>
    public partial class AdminController
    {
        // Test Management
        public async Task<List<Test>> GetTestsAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Tests.Include(t => t.Questions).ToListAsync();
            }
        }

        public async Task<Test> GetTestByIdAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Tests.Include(t => t.Questions).FirstOrDefaultAsync(t => t.TestId == id);
            }
        }

        public async Task<bool> CreateTestAsync(Test test)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(test.Title)) throw new ArgumentException("Tiêu đề bài kiểm tra không được để trống.");
                    if (test.Title.Length > 200) throw new ArgumentException("Tiêu đề quá dài (tối đa 200 ký tự).");

                    context.Tests.Add(test);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tạo bài kiểm tra: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateTestAsync(Test test)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbTest = await context.Tests.FindAsync(test.TestId);
                    if (dbTest == null) throw new Exception("Bài kiểm tra không tồn tại.");

                    if (string.IsNullOrWhiteSpace(test.Title)) throw new ArgumentException("Tiêu đề bài kiểm tra không được để trống.");
                    if (test.Title.Length > 200) throw new ArgumentException("Tiêu đề quá dài (tối đa 200 ký tự).");

                    dbTest.Title = test.Title;
                    dbTest.Description = test.Description;
                    dbTest.TimeLimitSec = test.TimeLimitSec;
                    dbTest.UpdatedAt = DateTime.UtcNow;

                    context.Tests.Update(dbTest);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật bài kiểm tra: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteTestAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var test = await context.Tests.FindAsync(id);
                    if (test == null) throw new Exception("Bài kiểm tra không tồn tại.");

                    context.Tests.Remove(test);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa bài kiểm tra: {ex.Message}");
                }
            }
        }

        // Question Management
        public async Task<List<Question>> GetQuestionsByTestIdAsync(int testId)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Questions
                    .Include(q => q.QuestionOptions)
                    .Where(q => q.TestId == testId)
                    .OrderBy(q => q.OrderIndex)
                    .ToListAsync();
            }
        }

        public async Task<Question> GetQuestionByIdAsync(int questionId)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Questions
                    .Include(q => q.QuestionOptions)
                    .FirstOrDefaultAsync(q => q.QuestionId == questionId);
            }
        }

        public async Task<bool> CreateQuestionAsync(Question question)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(question.StemText))
                        throw new ArgumentException("Nội dung câu hỏi không được trống");
                    if (question.Points <= 0)
                        throw new ArgumentException("Điểm số phải lớn hơn 0");

                    // Set order index if not provided
                    if (question.OrderIndex == 0)
                    {
                        var maxOrder = await context.Questions
                            .Where(q => q.TestId == question.TestId)
                            .MaxAsync(q => (int?)q.OrderIndex) ?? 0;
                        question.OrderIndex = maxOrder + 1;
                    }

                    context.Questions.Add(question);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tạo câu hỏi: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateQuestionAsync(Question question)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbQuestion = await context.Questions
                        .Include(q => q.QuestionOptions)
                        .FirstOrDefaultAsync(q => q.QuestionId == question.QuestionId);
                    if (dbQuestion == null) throw new Exception("Câu hỏi không tồn tại");

                    if (string.IsNullOrWhiteSpace(question.StemText))
                        throw new ArgumentException("Nội dung câu hỏi không được trống");
                    if (question.Points <= 0)
                        throw new ArgumentException("Điểm số phải lớn hơn 0");

                    dbQuestion.StemText = question.StemText;
                    dbQuestion.Type = question.Type;
                    dbQuestion.Points = question.Points;
                    dbQuestion.OrderIndex = question.OrderIndex;
                    dbQuestion.Metadata = question.Metadata;

                    // Update options
                    context.QuestionOptions.RemoveRange(dbQuestion.QuestionOptions);
                    if (question.QuestionOptions?.Any() == true)
                    {
                        foreach (var option in question.QuestionOptions)
                        {
                            option.QuestionId = question.QuestionId;
                            context.QuestionOptions.Add(option);
                        }
                    }

                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật câu hỏi: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteQuestionAsync(int questionId)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var question = await context.Questions
                        .Include(q => q.QuestionOptions)
                        .FirstOrDefaultAsync(q => q.QuestionId == questionId);
                    if (question == null) throw new Exception("Câu hỏi không tồn tại");

                    context.QuestionOptions.RemoveRange(question.QuestionOptions);
                    context.Questions.Remove(question);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa câu hỏi: {ex.Message}");
                }
            }
        }
    }
}
