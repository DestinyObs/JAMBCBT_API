using System.Collections.Generic;

namespace JAMBAPI.ViewModels.DTOs
{
   

    public class QuestionDto
    {
        public int LecturerId { get; set; }
        public int SubjectId { get; set; }
        public string Text { get; set; }
        public List<QuestionOptionDto> Options { get; set; }
    }
}
