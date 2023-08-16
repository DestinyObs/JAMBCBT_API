using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JAMBAPI.Data;
using JAMBAPI.Interface;
using JAMBAPI.Models;
using JAMBAPI.ViewModels.DTOs;
using Microsoft.EntityFrameworkCore;

namespace JAMBAPI.Repositories
{
    public class SuperAdminRepository : ISuperAdminRepository
    {
        private readonly JambDbContext _dbContext;

        public SuperAdminRepository(JambDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Admin> GetSuperAdminByEmailAsync(string email)
        {
            return await _dbContext.Admins.SingleOrDefaultAsync(a => a.Email == email && a.Role == "SuperAdmin");
        }

        public async Task<AdminDto> CreateAdminAsync(AdminDto adminDto)
        {
            var admin = new Admin
            {
                Password = BCrypt.Net.BCrypt.HashPassword(adminDto.Password),
                Email = adminDto.Email,
                Role = "Admin", // Set the role to "Admin" as SuperAdmin is hard-coded
                AdminSpecialId = GenerateSpecialId(), // Generate the special ID
                IsLocked = false,
                IsSuspended = false,
            };

            _dbContext.Admins.Add(admin);
            await _dbContext.SaveChangesAsync();

            return new AdminDto
            {
                Password = admin.Password,
                Email = admin.Email,
            };
        }
        public async Task<bool> LockAdminAccountAsync(string specialId)
        {
            var admin = await _dbContext.Admins.SingleOrDefaultAsync(a => a.AdminSpecialId == specialId);
            if (admin == null)
                return false;

            admin.IsLocked = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnlockAdminAccountAsync(string specialId)
        {
            var admin = await _dbContext.Admins.SingleOrDefaultAsync(a => a.AdminSpecialId == specialId);
            if (admin == null)
                return false;

            admin.IsLocked = false;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SuspendAdminAsync(string specialId)
        {
            var admin = await _dbContext.Admins.SingleOrDefaultAsync(a => a.AdminSpecialId == specialId);
            if (admin == null)
                return false;

            admin.IsSuspended = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReinstateAdminAsync(string specialId)
        {
            var admin = await _dbContext.Admins.SingleOrDefaultAsync(a => a.AdminSpecialId == specialId);
            if (admin == null)
                return false;

            admin.IsSuspended = false;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private string GenerateSpecialId()
        {
            var currentYear = DateTime.UtcNow.Year;
            var adminsCount = _dbContext.Admins.Count() + 1; // Add 1 to ensure uniqueness
            var specialId = $"Jmb{currentYear}Adm{adminsCount:D3}"; // D3 ensures 3-digit format

            return specialId;
        }

        public async Task<Admin> UpdateAdminAsync(int adminId, Admin admin)
        {
            var existingAdmin = await _dbContext.Admins.FindAsync(adminId);
            if (existingAdmin == null)
                return null;

            existingAdmin.Password = admin.Password;
            existingAdmin.Email = admin.Email;

            await _dbContext.SaveChangesAsync();
            return existingAdmin;
        }

        public async Task<bool> DeleteAdminAsync(int adminId)
        {
            var admin = await _dbContext.Admins.FindAsync(adminId);
            if (admin == null)
                return false;

            _dbContext.Admins.Remove(admin);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Admin> GetAdminByIdAsync(int adminId)
        {
            return await _dbContext.Admins.FindAsync(adminId);
        }

        public async Task<bool> AdminExistsAsync(int adminId)
        {
            return await _dbContext.Admins.AnyAsync(a => a.Id == adminId);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _dbContext.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task<UserProgress> GetUserProgressByIdAsync(int userProgressId)
        {
            return await _dbContext.UserProgresses.FindAsync(userProgressId);
        }

        public async Task<bool> UserProgressExistsAsync(int userProgressId)
        {
            return await _dbContext.UserProgresses.AnyAsync(up => up.Id == userProgressId);
        }


        public async Task<List<Admin>> GetAllAdminsAsync()
        {
            return await _dbContext.Admins.ToListAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<List<UserProgress>> GetAllUserProgressAsync()
        {
            return await _dbContext.UserProgresses.ToListAsync();
        }

        public async Task<List<QuizReport>> GetAllQuizReportsAsync()
        {
            return await _dbContext.QuizReports.ToListAsync();
        }

        public async Task<bool> LockUserAccountAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
                return false;

            user.IsLocked = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnlockUserAccountAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
                return false;

            user.IsLocked = false;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SuspendUserAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
                return false;

            user.IsSuspended = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReinstateUserAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
                return false;

            user.IsSuspended = false;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        //public async Task<QuizReport> GenerateQuizReportAsync(int quizId)
        //{
        //    //// Implement logic to generate a quiz report based on the provided quizId
        //    //// Retrieve quiz details from the database
        //    //var quiz = await _dbContext.Quizzes.FindAsync(quizId);
        //    //if (quiz == null)
        //    //    return null;

        //    //// Retrieve user quiz submissions
        //    //var submissions = await _dbContext.UserQuizSubmissions
        //    //    .Where(submission => submission.QuizId == quizId)
        //    //    .ToListAsync();

        //    //// Calculate quiz statistics
        //    //int totalSubmissions = submissions.Count;
        //    //int totalCorrectAnswers = submissions.Sum(submission => submission.CorrectAnswers);

        //    //// Create a new QuizReport object
        //    //var quizReport = new QuizReport
        //    //{
        //    //    QuizId = quizId,
        //    //    QuizTitle = quiz.Title,
        //    //    TotalSubmissions = totalSubmissions,
        //    //    TotalCorrectAnswers = totalCorrectAnswers
        //    //};

        //    //// Save the quiz report to the database
        //    //_dbContext.QuizReports.Add(quizReport);
        //    //await _dbContext.SaveChangesAsync();

        //    //return quizReport;
        //}

        async Task<IEnumerable<Admin>> ISuperAdminRepository.GetAllAdminsAsync()
        {
            // Implement logic to retrieve details of all admins
            var admins = await _dbContext.Admins.ToListAsync();
            return  admins;
        }


        async Task<IEnumerable<User>> ISuperAdminRepository.GetAllUsersAsync()
        {
            // Implement logic to retrieve details of all users
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        async Task<IEnumerable<UserProgress>> ISuperAdminRepository.GetAllUserProgressAsync()
        {
            // Implement logic to retrieve details of all user progress records
            var userProgresses = await _dbContext.UserProgresses.ToListAsync();
            return userProgresses;
        }

        async Task<IEnumerable<QuizReport>> ISuperAdminRepository.GetAllQuizReportsAsync()
        {
            // Implement logic to retrieve details of all quiz reports
            var quizReports = await _dbContext.QuizReports.ToListAsync();
            return quizReports;
        }

        public Task<QuizReport> GenerateQuizReportAsync(int quizId)
        {
            throw new NotImplementedException();
        }
    }
}
