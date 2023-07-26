using System.Collections.Generic;
using System.Threading.Tasks;
using JAMBAPI.Models;

namespace JAMBAPI.Interface
{
    public interface IQuizRepository
    {
        Task<Quiz> GetQuizByIdAsync(int quizId);
        Task<IEnumerable<Quiz>> GetQuizzesByLecturerIdAsync(int lecturerId);
        Task<IEnumerable<Quiz>> GetQuizzesBySubjectIdAsync(int subjectId);
        Task CreateQuizAsync(Quiz quiz);
        Task UpdateQuizAsync(Quiz quiz);
        Task DeleteQuizAsync(Quiz quiz);
        Task<bool> QuizExistsAsync(int quizId);
    }
}
