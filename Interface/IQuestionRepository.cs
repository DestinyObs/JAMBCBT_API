using JAMBAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JAMBAPI.Interface
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int id);
        Task<Question> CreateQuestionAsync(Question question);
        Task<Question> UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int id);
    }
}
