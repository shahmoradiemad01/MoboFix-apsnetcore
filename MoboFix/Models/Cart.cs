using System.ComponentModel.DataAnnotations.Schema;
using MoboFix.Data;
using MoboFix.Utilities.Program.Status;
namespace MoboFix.Models
{
    public class Cart
    {

        public int Id { get; set; }
        public ApplicationUserCustomer Customer { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public bool IsConfirmed { get; set; }
        public bool HasFinalised { get; set; }

        public void checkConfirmation()
        {
            bool status = true;
            foreach(var item in CartItems)
            {
                if (item.Status != ProgramStatusCodes.Accepted) 
                { status = false; break; }
            }
            IsConfirmed = status;
        }
        public CartItem addCartItem(int id)
        {
            var Item = CartItems.Find(x => x.ProductId == id);
            if(Item != null)
            {
                Item.Quantity++;
                return Item;
            }
            
            Item = new CartItem{
                CartId = Id,
                ProductId = id,
                Status = ProgramStatusCodes.Pending,
                Quantity = 1
            };
            CartItems.Add(Item);
            return Item;
        }
    }
}
