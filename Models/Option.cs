using System.Text.Json.Serialization;

namespace JAMBAPI.Models
{
    public class Option
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public string OptionLetter { get; set; }
        [JsonIgnore]
        public Question Question { get; set; }
    }

}
