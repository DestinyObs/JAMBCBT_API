namespace JAMBAPI.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        //public int LecturerId { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime DateCreated { get; set; }
        public Subject Subject { get; set; }
        public Lecturer Lecturer { get; set; }
        public ICollection<QuizQuestion> QuizQuestions { get; set; }
        public ICollection<UserProgress> UserProgresses { get; set; }
    }

}
