namespace JAMBAPI.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }

}
