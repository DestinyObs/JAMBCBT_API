using System.Collections.Generic;
using System.Threading.Tasks;
using JAMBAPI.Models;
using JAMBAPI.ViewModels.DTOs;

namespace JAMBAPI.Interface
{
    public interface ISuperAdminRepository
    {
        Task<Admin> GetSuperAdminByEmailAsync(string email);
        Task<AdminDto> CreateAdminAsync(AdminDto admin); // Create a new admin
        Task<Admin> UpdateAdminAsync(string adminSpecialId, Admin admin); // Update an existing admin
        Task<bool> DeleteAdminAsync(string adminSpecialId); // Delete an admin
        Task<Admin> GetAdminBySpecialIdAsync(string adminSpecialId); // Get admin details by special ID
        Task<bool> AdminExistsAsync(string adminSpecialId); // Check if an admin exists by special ID

        Task<User> GetUserByIdAsync(int userId); // Get user details by ID
        Task<bool> UserExistsAsync(int userId); // Check if a user exists by ID
        Task<UserProgress> GetUserProgressByIdAsync(int userId); // Get user progress by ID
        Task<bool> UserProgressExistsAsync(int userId); // Check if user progress exists by ID
        Task<QuizReport> GenerateQuizReportAsync(int quizId); // Generate a quiz report

        Task<IEnumerable<Admin>> GetAllAdminsAsync(); // Get details of all admins
        Task<IEnumerable<User>> GetAllUsersAsync(); // Get details of all users
        Task<IEnumerable<UserProgress>> GetAllUserProgressAsync(); // Get details of all user progress
        Task<IEnumerable<QuizReport>> GetAllQuizReportsAsync(); // Get details of all quiz reports

        Task<bool> LockUserAccountAsync(int userId); // Lock a user's account
        Task<bool> UnlockUserAccountAsync(int userId); // Unlock a user's account
        Task<bool> SuspendUserAsync(int userId); // Suspend a user's account
        Task<bool> ReinstateUserAsync(int userId); // Reinstate a suspended user's account
        Task<bool> LockLecturerAccountAsync(int LecturerId); // Lock a user's account
        Task<bool> UnlockLecturerAccountAsync(int LecturerId); // Unlock a user's account
        Task<bool> SuspendLecturerAsync(int LecturerId); // Suspend a user's account
        Task<bool> ReinstateLecturerAsync(int LecturerId); // Reinstate a suspended user's account

        // Add other methods as needed for SuperAdmin management
    }
}
