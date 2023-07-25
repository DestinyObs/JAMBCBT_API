namespace JAMBAPI.Models
{
    public class Leaderboard
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int StudentId { get; set; }
        public int Score { get; set; }
        public Quiz Quiz { get; set; }
        public User Student { get; set; }
    }

}
