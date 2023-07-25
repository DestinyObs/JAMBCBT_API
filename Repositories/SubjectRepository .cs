using System.Collections.Generic;
using System.Threading.Tasks;
using JAMBAPI.Data;
using JAMBAPI.Interface;
using JAMBAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JAMBAPI.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly JambDbContext _dbContext;

        public SubjectRepository(JambDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _dbContext.Subjects.ToListAsync();
        }

        public async Task<Subject> GetSubjectByIdAsync(int id)
        {
            return await _dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<bool> DoesSubjectExistAsync(int subjectId)
        {
            return await _dbContext.Subjects.AnyAsync(s => s.Id == subjectId);
        }


        public async Task<Subject> CreateSubjectAsync(Subject subject)
        {
            _dbContext.Subjects.Add(subject);
            await _dbContext.SaveChangesAsync();
            return subject;
        }

        public async Task<Subject> UpdateSubjectAsync(Subject subject)
        {
            _dbContext.Subjects.Update(subject);
            await _dbContext.SaveChangesAsync();
            return subject;
        }

        public async Task DeleteSubjectAsync(int id)
        {
            var subject = await _dbContext.Subjects.FindAsync(id);
            if (subject != null)
            {
                _dbContext.Subjects.Remove(subject);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
