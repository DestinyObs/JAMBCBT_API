using System;
using System.Collections.Generic;

namespace JAMBAPI.ViewModels.DTOs
{
    public class QuizCreateDto
    {
        public int SubjectId { get; set; }
        //public int LecturerId { get; set; }
        public int DurationInMinutes { get; set; }
        public List<int> QuestionIds { get; set; } // List of question IDs for the quiz
        public List<QuestionOptionDto> Options { get; set; } // List of options for each question
    }
}
