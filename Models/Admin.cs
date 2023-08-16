namespace JAMBAPI.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string AdminSpecialId { get; set; } // Special ID in the format "Jmb{CurrentYear}Adm{001}"
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // SuperAdmin or Admin
        public bool CanManageAdmins { get; set; } = false;
        public bool IsLocked { get; set; }
        public bool IsSuspended { get; set; }
    }
}
