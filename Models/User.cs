namespace JAMBAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now.Date.ToUniversalTime();
        public string State { get; set; }
        public string LGA { get; set; }
        public string Phone { get; set; }
        public string ResidentialAddress { get; set; }
        public ICollection<OLevelGrade> OLevelGrades { get; set; }
        public ICollection<UserProgress> UserProgresses { get; set; }
        public bool IsVerified { get; set; } // Add 'IsVerified' property
        public string? VerificationCode { get; set; } // Add 'VerificationCode' property
        public DateTime VerificationCodeExpiration { get; set; }
    }
}
