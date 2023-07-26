// QuizQuestionDto.cs
namespace JAMBAPI.ViewModels.DTOs
{
    public class QuizQuestionDto
    {
        public int Order { get; set; }
        public QuestionDto Question { get; set; } // Using the QuestionDto we created earlier
    }
}
