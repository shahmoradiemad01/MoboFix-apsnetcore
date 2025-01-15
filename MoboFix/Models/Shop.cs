namespace MoboFix.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public String CertificationCode { get; set; }
        public ApplicationUser User { get; set; }
        public String UserId { get; set; }
    }
}
