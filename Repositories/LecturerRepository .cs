using JAMBAPI.Data;
using JAMBAPI.Interface;
using JAMBAPI.Models;
using BCrypt.Net;
using JAMBAPI.ViewModels;
using JAMBAPI.ViewModels.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace JAMBAPI.Repositories
{
    public class LecturerRepository : ILecturerRepository
    {
        private readonly JambDbContext _dbContext;
        private readonly SmtpSettings _smtpSettings;

        public LecturerRepository(JambDbContext dbContext, SmtpSettings smtpSettings)
        {
            _dbContext = dbContext;
            _smtpSettings = smtpSettings;
        }

        public async Task<Lecturer> GetByIdAsync(int id)
        {
            return await _dbContext.Lecturers.FindAsync(id);
        }

        public async Task<Lecturer> GetByEmailAsync(string email)
        {
            return await _dbContext.Lecturers.FirstOrDefaultAsync(l => l.Email == email);
        }

        public async Task<IEnumerable<Lecturer>> GetAllAsync()
        {
            return await _dbContext.Lecturers.ToListAsync();
        }
        public async Task RegisterLecturerAsync(LecturerDto lecturerDto)
        {
            var lecturer = new Lecturer
            {
                FullName = lecturerDto.FullName,
                Email = lecturerDto.Email,
                Department = lecturerDto.Department,
                IsApproved = false, 
            Password = BCrypt.Net.BCrypt.HashPassword(lecturerDto.Password)
            };


            _dbContext.Lecturers.Add(lecturer);
            await SaveChangesAsync();
        }


        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbContext.Lecturers.AnyAsync(l => l.Email == email);
        }

        public async Task<bool> IsApprovedAsync(int id)
        {
            var lecturer = await GetByIdAsync(id);
            return lecturer?.IsApproved ?? false;
        }

        public async Task ApproveLecturerAsync(int id)
        {
            var lecturer = await GetByIdAsync(id);
            if (lecturer != null)
            {
                lecturer.IsApproved = true;
                await SaveChangesAsync();
            }
        }

        public async Task RejectLecturerAsync(int id)
        {
            var lecturer = await GetByIdAsync(id);
            if (lecturer != null)
            {
                _dbContext.Lecturers.Remove(lecturer);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> DoesLecturerExistAsync(int lecturerId)
        {
            return await _dbContext.Lecturers.AnyAsync(l => l.Id == lecturerId);
        }

        public async Task SendApprovalEmail(string email)
        {
            var mail = new MailMessage();
            var smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = true
            };

            mail.From = new MailAddress(_smtpSettings.Username);
            mail.To.Add(email);
            mail.Subject = "Lecturer Account Approval";
            mail.Body = "Congratulations! Your lecturer account has been approved.";

            await smtpClient.SendMailAsync(mail);
        }

        public async Task SendRejectionEmail(string email)
        {
            var mail = new MailMessage();
            var smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = true
            };

            mail.From = new MailAddress(_smtpSettings.Username);
            mail.To.Add(email);
            mail.Subject = "Lecturer Account Rejection";
            mail.Body = "We regret to inform you that your lecturer account has been rejected.";

            await smtpClient.SendMailAsync(mail);
        }
        public async Task<bool> IsEmailUsedByStudentAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
