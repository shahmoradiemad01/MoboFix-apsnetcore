using System.ComponentModel.DataAnnotations;

namespace MoboFix.Areas.Seller.ViewModels
{
    public class ProductViewModel
    {
        private int Id { get; set; }
        public int id { 
            get{
                return Id; 
            }
            set{
                Id = value;
            } 
        }
        
        public ProductViewModel()
        {
            Image = null;
        }

        [Required(ErrorMessage = "این فیلد لازم است")]
        [Display(Name = "نام محصول")]
        public string Name { get; set; }
        [Required(ErrorMessage = "این فیلد لازم است")]
        [Display(Name = "قیمت")]

        public int Price { get; set; }
        [Required(ErrorMessage = "این فیلد لازم است")]
        [Display(Name = "بارکدکالا")]
        public string CodeNumber { get; set; }
        [Required(ErrorMessage = "این فیلد لازم است")]
        [Display(Name = "کشورتولیدکننده")]
        public string Country { get; set; }
        [Required(ErrorMessage = "این فیلد لازم است")]
        [Display(Name = "برند")]
        public string Brand { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "تصویر محصول")]
        public IFormFile? Image { get; set; }
    }
}
