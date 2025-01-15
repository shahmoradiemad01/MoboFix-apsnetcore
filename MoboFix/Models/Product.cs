using System.ComponentModel.DataAnnotations;

namespace MoboFix.Models
{
    public class Product
    {
        public Product()
        {
            Name = String.Empty;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string CodeNumber { get; set; }
        public ApplicationUser Seller { get; set; }
        public string SellerId { get; set; }
        public string ImagePath { get; set; }
        public string Country { get; set; }
        public string Brand { get; set; }


    }
}
