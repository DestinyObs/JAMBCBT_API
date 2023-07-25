using System.Collections.Generic;
using System.Threading.Tasks;
using JAMBAPI.Models;

namespace JAMBAPI.Interface
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();
        Task<Subject> GetSubjectByIdAsync(int id);
        Task<Subject> CreateSubjectAsync(Subject subject);
        Task<Subject> UpdateSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(int id);
        Task<bool> DoesSubjectExistAsync(int subjectId);
    }
}
