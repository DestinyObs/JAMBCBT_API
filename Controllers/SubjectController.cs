using JAMBAPI.Data;
using JAMBAPI.Models;
using JAMBAPI.ViewModels.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JAMBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize("LecturerOnly")]
    public class SubjectController : ControllerBase
    {
        private readonly JambDbContext _dbContext;

        public SubjectController(JambDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Endpoint to get a list of all subjects
        [HttpGet]
        public IActionResult GetAllSubjects()
        {
            try
            {
                // Retrieve all subjects from the database
                var subjects = _dbContext.Subjects.ToList();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Endpoint to get details of a specific subject by ID
        [HttpGet("{id}")]
        public IActionResult GetSubjectById(int id)
        {
            try
            {
                // Retrieve the subject from the database by ID
                var subject = _dbContext.Subjects.FirstOrDefault(s => s.Id == id);

                if (subject == null)
                {
                    return NotFound("Subject not found.");
                }

                return Ok(subject);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Endpoint to create a new subject
        [HttpPost]
        public async Task<IActionResult> CreateSubject(SubjectDto subjectDto)
        {
            try
            {
                // Create a new subject entity from the DTO
                var subject = new Subject
                {
                    Name = subjectDto.Name,
                    Description = subjectDto.Description
                };

                // Save the new subject to the database
                _dbContext.Subjects.Add(subject);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetSubjectById), new { id = subject.Id }, subject);
            }
             catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Endpoint to update an existing subject by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, SubjectDto subjectDto)
        {
            try
            {
                // Retrieve the subject from the database by ID
                var subject = _dbContext.Subjects.FirstOrDefault(s => s.Id == id);

                if (subject == null)
                {
                    return NotFound("Subject not found.");
                }

                // Update the subject entity with the new values from the DTO
                subject.Name = subjectDto.Name;
                subject.Description = subjectDto.Description;

                // Save the updated subject to the database
                await _dbContext.SaveChangesAsync();

                return Ok(subject);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Endpoint to delete a subject by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                // Retrieve the subject from the database by ID
                var subject = _dbContext.Subjects.FirstOrDefault(s => s.Id == id);

                if (subject == null)
                {
                    return NotFound("Subject not found.");
                }

                // Delete the subject from the database
                _dbContext.Subjects.Remove(subject);
                await _dbContext.SaveChangesAsync();

                return Ok("Subject deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
