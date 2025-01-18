using System.ComponentModel.DataAnnotations.Schema;

namespace MoboFix.Models
{
    public class Certification
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ImagePath { get; set; }
        public ApplicationUser User { get; set; }
        [InverseProperty("User")]
        public string UserId { get; set; }
    }
}