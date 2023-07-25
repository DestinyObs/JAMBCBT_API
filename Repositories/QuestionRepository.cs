using JAMBAPI.Data;
using JAMBAPI.Interface;
using JAMBAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAMBAPI.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly JambDbContext _dbContext;

        public QuestionRepository(JambDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _dbContext.Questions.ToListAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            return await _dbContext.Questions.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Question> CreateQuestionAsync(Question question)
        {
            _dbContext.Questions.Add(question);
            await _dbContext.SaveChangesAsync();
            return question;
        }

        public async Task<Question> UpdateQuestionAsync(Question question)
        {
            _dbContext.Questions.Update(question);
            await _dbContext.SaveChangesAsync();
            return question;
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _dbContext.Questions.FindAsync(id);
            if (question != null)
            {
                _dbContext.Questions.Remove(question);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
