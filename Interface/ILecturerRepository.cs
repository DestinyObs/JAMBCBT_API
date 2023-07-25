// ILecturerRepository.cs
using System.Threading.Tasks;
using JAMBAPI.Models;
using JAMBAPI.ViewModels.DTOs;

namespace JAMBAPI.Interface
{
    public interface ILecturerRepository
    {
        Task<Lecturer> GetByIdAsync(int id);
        Task<Lecturer> GetByEmailAsync(string email);
        Task<IEnumerable<Lecturer>> GetAllAsync();
        Task RegisterLecturerAsync(LecturerDto lecturerDto);
        Task<bool> EmailExistsAsync(string email);
        //Task<bool> LoginAsync(string email, string password);
        Task<bool> IsEmailUsedByStudentAsync(string email);
        Task<bool> IsApprovedAsync(int id);
        Task ApproveLecturerAsync(int id);
        Task RejectLecturerAsync(int id);
        Task<bool> SaveChangesAsync();
        Task SendApprovalEmail(string email);
        Task SendRejectionEmail(string email);
        Task<bool> DoesLecturerExistAsync(int lecturerId);
    }
}
