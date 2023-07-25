namespace JAMBAPI.Models
{
    public class UserProgress
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public DateTime CompletionDate { get; set; }
        public int StudentId { get; set; }
        public int QuizId { get; set; }
        public User Student { get; set; }
        public Quiz Quiz { get; set; }
    }

}
