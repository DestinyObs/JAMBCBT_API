using JAMBAPI.Interface;
using JAMBAPI.Models;
using JAMBAPI.Repositories;
using JAMBAPI.ViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace JAMBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize("LecturerOnly")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILecturerRepository _lecturerRepository;
        private readonly ISubjectRepository _subjectRepository;

        public QuestionController(IQuestionRepository questionRepository, ILecturerRepository lecturerRepository, ISubjectRepository subjectRepository)
        {
            _questionRepository = questionRepository;
            _lecturerRepository = lecturerRepository;
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            try
            {
                // Retrieve all questions from the database
                var questions = await _questionRepository.GetAllQuestionsAsync();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            try
            {
                // Retrieve the question from the database by ID
                var question = await _questionRepository.GetQuestionByIdAsync(id);

                if (question == null)
                {
                    return NotFound("Question not found.");
                }

                return Ok(question);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(QuestionDto questionDto)
        {
            try
            {
                // Check if the LecturerId exists
                //var lecturerExists = await _lecturerRepository.DoesLecturerExistAsync(questionDto.LecturerId);
                //if (!lecturerExists)
                //{
                //    return BadRequest("Lecturer with the specified Id does not exist.");
                //}

                // Check if the SubjectId exists
                var subjectExists = await _subjectRepository.DoesSubjectExistAsync(questionDto.SubjectId);
                if (!subjectExists)
                {
                    return BadRequest("Subject with the specified Id does not exist.");
                }

                // Create a new question entity from the DTO
                var question = new Question
                {
                    //LecturerId = questionDto.LecturerId,
                    SubjectId = questionDto.SubjectId,
                    Text = questionDto.Text
                };

                // Create a list of options from the option texts provided in the DTO
                var options = questionDto.Options.Select((optionDto, index) => new Option
                {
                    Text = optionDto.Text,
                    IsCorrect = optionDto.IsCorrect,
                    OptionLetter = ((char)('A' + index)).ToString() // Set option letters as A, B, C, D, E...
                }).ToList();

                // Add the options to the question
                question.Options = options;

                // Save the new question to the database
                await _questionRepository.CreateQuestionAsync(question);

                return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, question);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, QuestionDto questionDto)
        {
            try
            {
                // Retrieve the question from the database by ID
                var question = await _questionRepository.GetQuestionByIdAsync(id);

                if (question == null)
                {
                    return NotFound("Question not found.");
                }

                // Update the question entity with the new values from the DTO

                //question.LecturerId = questionDto.LecturerId;
                question.SubjectId = questionDto.SubjectId;
                question.Text = questionDto.Text;

                // Save the updated question to the database
                await _questionRepository.UpdateQuestionAsync(question);

                return Ok(question);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                // Delete the question from the database
                await _questionRepository.DeleteQuestionAsync(id);

                return Ok("Question deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
