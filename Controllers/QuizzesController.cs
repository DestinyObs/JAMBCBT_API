// QuizzesController.cs
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JAMBAPI.Models;
using JAMBAPI.ViewModels.DTOs;
using JAMBAPI.Interface;

namespace JAMBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizRepository _quizRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILecturerRepository _lecturerRepository;
        private readonly IQuestionRepository _questionRepository;

        public QuizzesController(
            IQuizRepository quizRepository,
            ISubjectRepository subjectRepository,
            ILecturerRepository lecturerRepository,
            IQuestionRepository questionRepository)
        {
            _quizRepository = quizRepository;
            _subjectRepository = subjectRepository;
            _lecturerRepository = lecturerRepository;
            _questionRepository = questionRepository;
        }


        // Create a mapping function to convert QuestionOptionDto to Option
        public Option MapToOption(QuestionOptionDto questionOptionDto)
        {
            return new Option
            {
                Text = questionOptionDto.Text,
                IsCorrect = questionOptionDto.IsCorrect
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz(QuizCreateDto quizCreateDto)
        {
            try
            {
                // Check if the subject and lecturer exist
                var subjectExists = await _subjectRepository.DoesSubjectExistAsync(quizCreateDto.SubjectId);
                //var lecturerExists = await _lecturerRepository.DoesLecturerExistAsync(quizCreateDto.LecturerId);

                if (!subjectExists)
                {
                    return BadRequest("Invalid subject");
                }

                // Create a new quiz entity from the DTO
                var quiz = new Quiz
                {
                    SubjectId = quizCreateDto.SubjectId,
                    //LecturerId = quizCreateDto.LecturerId,
                    DurationInMinutes = quizCreateDto.DurationInMinutes,
                    DateCreated = DateTime.UtcNow
                };

                // Validate the question IDs
                var questions = await _questionRepository.GetQuestionsByIdsAsync(quizCreateDto.QuestionIds);
                if (questions.Count != quizCreateDto.QuestionIds.Count)
                {
                    return BadRequest("Invalid question ID(s).");
                }
                // Create quiz question entities and assign the order
                var quizQuestions = quizCreateDto.QuestionIds.Select((questionId, index) =>
                {
                    var question = questions.FirstOrDefault(q => q.Id == questionId);
                    if (question == null)
                    {
                        throw new ArgumentException($"Question with ID {questionId} not found.");
                    }

                    // Ensure the number of options matches the number of provided options for each question
                    //if (quizCreateDto.Options[index].Count != question.Options.Count)
                    //{
                    //    throw new ArgumentException($"Invalid number of options for question with ID {questionId}.");
                    //}

                    var questionOptions = question.Options.Select((option, optionIndex) => MapToOption(new QuestionOptionDto
                    {
                        Text = option.Text,
                        IsCorrect = quizCreateDto.Options[index].IsCorrect
                    })).ToList();

                    // Validate that at least one option is marked as correct
                    if (!questionOptions.Any(o => o.IsCorrect))
                    {
                        throw new ArgumentException($"At least one option must be marked as correct for question with ID {questionId}.");
                    }

                    return new QuizQuestion
                    {
                        QuestionId = questionId,
                        Order = index + 1, // Adding 1 to start the order from 1
                        Options = questionOptions
                    };
                }).ToList();

                // Add the quiz questions to the quiz
                quiz.QuizQuestions = quizQuestions;

                // Save the new quiz to the database
                await _quizRepository.CreateQuizAsync(quiz);

                // Return the created quiz details
                var quizDto = new QuizDto
                {
                    Id = quiz.Id,
                    SubjectId = quiz.SubjectId,
                    DurationInMinutes = quiz.DurationInMinutes,
                    DateCreated = quiz.DateCreated,
                    QuizQuestions = quiz.QuizQuestions
                        .Select(qq => new QuizQuestionDto
                        {
                            Order = qq.Order,
                            Question = new QuestionDto
                            {
                                //LecturerId = qq.Question.LecturerId,
                                SubjectId = qq.Question.SubjectId,
                                Text = qq.Question.Text,
                                Options = qq.Options
                                    .Select(option => new QuestionOptionDto
                                    {
                                        Text = option.Text,
                                        IsCorrect = option.IsCorrect
                                    }).ToList()
                            }
                        }).ToList()
                };

                return CreatedAtAction(nameof(GetQuizById), new { id = quiz.Id }, quizDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            try
            {
                var quiz = await _quizRepository.GetQuizByIdAsync(id);

                if (quiz == null)
                {
                    return NotFound();
                }

                // Return the quiz details
                var quizDto = new QuizDto
                {
                    Id = quiz.Id,
                    SubjectId = quiz.SubjectId,
                    DurationInMinutes = quiz.DurationInMinutes,
                    DateCreated = quiz.DateCreated,
                    QuizQuestions = quiz.QuizQuestions
                        .Select(qq => new QuizQuestionDto
                        {
                            Order = qq.Order,
                            Question = new QuestionDto
                            {
                                SubjectId = qq.Question.SubjectId,
                                Text = qq.Question.Text,
                                Options = qq.Question.Options
                                    .Select(option => new QuestionOptionDto
                                    {
                                        Text = option.Text,
                                        IsCorrect = option.IsCorrect
                                    }).ToList()
                            }
                        }).ToList()
                };

                return Ok(quizDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Add other quiz-related endpoints as needed...
    }
}
