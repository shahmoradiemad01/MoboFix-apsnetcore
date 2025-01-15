using MoboFix.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoboFix.Models
{
    public class Advertisment
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public ApplicationUser Seller { get; set; }
        [ForeignKey("Seller")]
        public string? SellerId { get; set; }
        public int Status{ get; set; }
        public int SetStatus(int Code) { Status = Code; return Status; }
        public int GetStatus(int Code) { return Status; }
    }
}
