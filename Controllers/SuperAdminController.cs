using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JAMBAPI.Data;
using JAMBAPI.Interface;
using JAMBAPI.Models;
using JAMBAPI.Repositories;
using JAMBAPI.ViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JAMBAPI.Controllers
{
    [Route("api/superadmin")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        private readonly ISuperAdminRepository _superAdminRepository;
        private readonly ILecturerRepository _lecturerRepository;

        public SuperAdminController(ILecturerRepository lecturerRepository, ISuperAdminRepository superAdminRepository)
        {
            _lecturerRepository = lecturerRepository;
            _superAdminRepository = superAdminRepository;
        }

        [HttpPost("Create-Admins")]
        public async Task<ActionResult<AdminDto>> CreateAdmin([FromBody] AdminDto adminDto)
        {
            try
            {
                if (adminDto == null)
                {
                    return BadRequest("Admin data is null.");
                }

                // Create the admin using the repository
                var createdAdmin = await _superAdminRepository.CreateAdminAsync(adminDto);

                return Ok(createdAdmin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Admins")]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAllAdmins()
        {
            var admins = await _superAdminRepository.GetAllAdminsAsync();
            return Ok(admins);
        }

        [HttpGet("Get All Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _superAdminRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("userprogress")]
        public async Task<ActionResult<IEnumerable<UserProgress>>> GetAllUserProgress()
        {
            var userProgresses = await _superAdminRepository.GetAllUserProgressAsync();
            return Ok(userProgresses);
        }

        [HttpGet("quizreports")]
        public async Task<ActionResult<IEnumerable<QuizReport>>> GetAllQuizReports()
        {
            var quizReports = await _superAdminRepository.GetAllQuizReportsAsync();
            return Ok(quizReports);
        }

        [HttpPut("lockuser/{userId}")]
        public async Task<IActionResult> LockUserAccount(int userId)
        {
            var success = await _superAdminRepository.LockUserAccountAsync(userId);
            if (success)
                return Ok();

            return NotFound();
        }
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveLecturer(int id)
        {
            try
            {
                var lecturer = await _lecturerRepository.GetByIdAsync(id);
                if (lecturer == null)
                {
                    return NotFound("Lecturer not found.");
                }

                if (lecturer.IsApproved)
                {
                    return BadRequest("Account as Already been Approved!!!");
                }

                await _lecturerRepository.ApproveLecturerAsync(id);

                // Send approval email to lecturer
                await _lecturerRepository.SendApprovalEmail(lecturer.Email);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok("Lecturer approved successfully. An approval email has been sent.");
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectLecturer(int id)
        {
            var lecturer = await _lecturerRepository.GetByIdAsync(id);
            if (lecturer == null)
            {
                return NotFound("Lecturer not found.");
            }

            if (lecturer.IsApproved)
            {
                return BadRequest("Account as Already been Approved!!!");
            }

            await _lecturerRepository.RejectLecturerAsync(id);

            // Send rejection email to lecturer
            await _lecturerRepository.SendRejectionEmail(lecturer.Email);




            return Ok("Lecturer rejected successfully. A rejection email has been sent.");
        }

        [HttpPut("unlockuser/{userId}")]
        public async Task<IActionResult> UnlockUserAccount(int userId)
        {
            var success = await _superAdminRepository.UnlockUserAccountAsync(userId);
            if (success)
                return Ok();

            return NotFound();
        }

        [HttpPut("suspenduser/{userId}")]
        public async Task<IActionResult> SuspendUser(int userId)
        {
            var success = await _superAdminRepository.SuspendUserAsync(userId);
            if (success)
                return Ok();

            return NotFound();
        }

        [HttpPut("reinstateuser/{userId}")]
        public async Task<IActionResult> ReinstateUser(int userId)
        {
            var success = await _superAdminRepository.ReinstateUserAsync(userId);
            if (success)
                return Ok();

            return NotFound();
        }
    }
}
