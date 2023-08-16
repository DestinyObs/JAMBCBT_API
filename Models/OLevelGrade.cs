using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JAMBAPI.Models
{
    public class OLevelGrade
    {
        [Key]
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Grade { get; set; }

        //public int UserdId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
