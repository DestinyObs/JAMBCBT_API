namespace JAMBAPI.Models
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; } 
        public string Email { get; set; }
        public string Department { get; set; }
        public string About { get; set; }
        public bool IsApproved { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }
}
