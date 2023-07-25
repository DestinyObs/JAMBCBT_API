namespace JAMBAPI.ViewModels.DTOs
{
    public class UserRegistrationDto
    {
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now.Date.ToUniversalTime();
        public string State { get; set; }
        public string LGA { get; set; }
        public string Phone { get; set; }
        public string ResidentialAddress { get; set; }
        public ICollection<OLevelGradeDto> OLevelGrades { get; set; }
    }
}
