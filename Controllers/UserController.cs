using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using JAMBAPI.Models;
using Microsoft.Extensions.Options;
using System.Web;
using JAMBAPI.Data;
using Microsoft.EntityFrameworkCore;
using JAMBAPI.ViewModels.DTOs;
using JAMBAPI.ViewModels;

namespace JAMBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly JambDbContext _dbContext;
        private readonly SmtpSettings _smtpSettings;

        public UserController(JambDbContext dbContext, IOptions<SmtpSettings> smtpSettings)
        {
            _dbContext = dbContext;
            _smtpSettings = smtpSettings.Value;
        }




        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto userDto)
        {
            try
            {
                // Check if email already exists
                if (_dbContext.Users.Any(u => u.Email == userDto.Email))
                {
                    return BadRequest("Email already exists.");
                }

                // Generate OTP
                string otp = GenerateOTP();

                // Convert OLevelGradeDto list to OLevelGrade entities
                var oLevelGrades = userDto.OLevelGrades.Select(ol => new OLevelGrade
                {
                    Subject = ol.Subject,
                    Grade = ol.Grade
                }).ToList();

                // Hash the user's password using BCrypt
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

                // Save the user to the database
                var user = new User
                {
                    Email = userDto.Email,
                    FullName = userDto.FullName,
                    DateOfBirth = userDto.DateOfBirth.Date,
                    State = userDto.State,
                    LGA = userDto.LGA,
                    Phone = userDto.Phone,
                    ResidentialAddress = userDto.ResidentialAddress,
                    OLevelGrades = oLevelGrades,
                    Password = hashedPassword
                };

                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                // Send OTP to user's email
                bool otpSent = SendOTP(userDto.Email, otp);

                if (!otpSent)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to send OTP. Please try again later.");
                }

                return Ok("Check your email for verification."); // Redirect to the verification page
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(UserVerificationDto verificationDto)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == verificationDto.Email && u.VerificationCode == verificationDto.VerificationCode);

            if (user == null)
            {
                return BadRequest("Invalid verification code.");
            }

            user.IsVerified = true;
            await _dbContext.SaveChangesAsync();
            return Ok("Verification successful. You can now log in."); // Redirect to the login page
        }

        [HttpPost("resend-verification")]
        public async Task<IActionResult> ResendVerificationCode(string email)
        {
            try
            {
                // Check if a user with the given email exists
                var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                // Check if the user is already verified
                if (user.IsVerified)
                {
                    return BadRequest("User is already verified.");
                }

                // Generate a new verification code
                string newVerificationCode = GenerateOTP();

                // Update the user's verification code and its expiration time
                user.VerificationCode = newVerificationCode;

                await _dbContext.SaveChangesAsync();

                // Send the new verification code to the user's email
                bool otpSent = SendOTP(user.Email, newVerificationCode);

                if (!otpSent)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to send verification code. Please try again later.");
                }

                return Ok("Verification code resent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userDto)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == userDto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            if (!BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
            {
                return Unauthorized("Incorrect password.");
            }

            if (!user.IsVerified)
            {
                return BadRequest("Account is not yet verified. Please verify your email.");
            }


            // Redirect to the home page or return a token for authentication
            return Ok("Login successful.");
        }

        private bool SendOTP(string email, string otp)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
                smtpClient.EnableSsl = true;

                mail.From = new MailAddress(_smtpSettings.Username);
                mail.To.Add(email);
                mail.Subject = "OTP Verification";
                mail.Body = $"Your OTP is: {otp}";

                smtpClient.Send(mail);

                // Set the verification code in the database
                TimeSpan VerificationCodeExpiration = TimeSpan.FromMinutes(5);
                var user = _dbContext.Users.SingleOrDefault(u => u.Email == email);
                if (user != null)
                {
                    user.VerificationCode = otp;
                    user.VerificationCodeExpiration = DateTime.UtcNow.Add(VerificationCodeExpiration);
                    _dbContext.SaveChanges();
                }


                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Handle the exception
                return false;
            }
        }

        private string GenerateOTP()
        {
            // Generate a 6-digit OTP
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp.ToString();
        }
    }
}