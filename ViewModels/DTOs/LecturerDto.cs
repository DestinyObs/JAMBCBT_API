using System.ComponentModel.DataAnnotations;

namespace JAMBAPI.ViewModels.DTOs
{
    public class LecturerDto
    {
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        public string Department { get; set; }
        public string Password { get; set; }
        public string About { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
