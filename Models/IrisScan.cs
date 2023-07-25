namespace JAMBAPI.Models
{
    public class IrisScan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] IrisScanData { get; set; }
        public User User { get; set; }
    }

}
