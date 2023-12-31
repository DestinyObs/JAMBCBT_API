﻿using JAMBAPI.Data;
using JAMBAPI.Interface;
using JAMBAPI.Models;
using JAMBAPI.ViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace JAMBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerRepository _lecturerRepository;
        private readonly JambDbContext _jambDbContext;

        public LecturerController(ILecturerRepository lecturerRepository, JambDbContext jambDbContext)
        {
            _lecturerRepository = lecturerRepository;
            _jambDbContext = jambDbContext;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LecturerDto lecturerDto)
        {
            try
            {
                // Check if the model is valid based on data annotations
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if email already exists
                if (await _lecturerRepository.EmailExistsAsync(lecturerDto.Email))
                {
                    return BadRequest("Email already exists.");
                }

                // Check if email already exists as a student
                if (await _lecturerRepository.IsEmailUsedByStudentAsync(lecturerDto.Email))
                {
                    return BadRequest("Email already exists as a student.");
                }

                // Check if password and confirm password match
                if (lecturerDto.Password != lecturerDto.ConfirmPassword)
                {
                    return BadRequest("Password and Confirm Password do not match.");
                }

                await _lecturerRepository.RegisterLecturerAsync(lecturerDto);

                // Send approval email to lecturer
                await _lecturerRepository.SendApprovalEmail(lecturerDto.Email);

                return Ok("Lecturer registration successful. Account is pending approval.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lecturer = await _lecturerRepository.GetByIdAsync(id);
            if (lecturer == null)
            {
                return NotFound("Lecturer not found.");
            }

            return Ok(lecturer);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var lecturer = await _lecturerRepository.GetByEmailAsync(email);
            if (lecturer == null)
            {
                return NotFound("Lecturer not found.");
            }

            return Ok(lecturer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lecturers = await _lecturerRepository.GetAllAsync();
            return Ok(lecturers);
        }

       

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userDto)
        {
            var lecturer = _jambDbContext.Lecturers.SingleOrDefault(u => u.Email == userDto.Email);

            if (lecturer == null)
            {
                return Unauthorized("Lecturer Email Not Found");
            }


            if (!BCrypt.Net.BCrypt.Verify(userDto.Password, lecturer.Password))
            {
                return Unauthorized("Incorrect password.");
            }

            if (!lecturer.IsApproved)
            {
                return BadRequest("Account is still PENDING approval. Please be patient if you are approved or declined we'd let you know!.");
            }
            return Ok("Login successful.");
        }
       
    }
}
