// QuizDto.cs
using System;
using System.Collections.Generic;

namespace JAMBAPI.ViewModels.DTOs
{
    public class QuizDto
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        //public int LecturerId { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime DateCreated { get; set; }
        public List<QuizQuestionDto> QuizQuestions { get; set; } // List of quiz questions
    }
}
