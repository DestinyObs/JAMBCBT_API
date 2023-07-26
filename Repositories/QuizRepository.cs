using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JAMBAPI.Data;
using JAMBAPI.Models;
using JAMBAPI.Interface;
using System.Globalization;

namespace JAMBAPI.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly JambDbContext _context;

        public QuizRepository(JambDbContext context)
        {
            _context = context;
        }

        public async Task<Quiz> GetQuizByIdAsync(int quizId)
        {
            return await _context.Quizzes
                .Include(q => q.QuizQuestions).ThenInclude(qq => qq.Question).ThenInclude(q => q.Options).FirstOrDefaultAsync(q => q.Id == quizId);
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesByLecturerIdAsync(int lecturerId)
        {
            return await _context.Quizzes
                //.Where(q => q.LecturerId == lecturerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesBySubjectIdAsync(int subjectId)
        {
            return await _context.Quizzes
                .Where(q => q.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task CreateQuizAsync(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuizAsync(Quiz quiz)
        {
            _context.Quizzes.Update(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuizAsync(Quiz quiz)
        {
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> QuizExistsAsync(int quizId)
        {
            return await _context.Quizzes.AnyAsync(q => q.Id == quizId);
        }
    }
}
