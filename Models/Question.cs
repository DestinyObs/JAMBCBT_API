using System.Collections.Generic;

namespace JAMBAPI.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string Text { get; set; }
        //public Subject Subject { get; set; }
        public ICollection<Option> Options { get; set; }
        public ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
