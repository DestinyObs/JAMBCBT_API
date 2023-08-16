using System;

namespace JAMBAPI.Models
{
    public class QuizReport
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public DateTime DateTaken { get; set; }
    }
}
