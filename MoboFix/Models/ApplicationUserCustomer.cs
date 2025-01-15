using Microsoft.EntityFrameworkCore;
using MoboFix.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoboFix.Models
{
    public class ApplicationUserCustomer : ApplicationUser
    {

        [InverseProperty("Customer")]
        public List<Cart>? Carts { get; set; }

/*        public int addToCart(int ProductId)
        {
            var activeCart = _context.Carts.Where(x => x.CustomerId == Id && x.HasFinalised == false).Include(y => y.CartItems).SingleOrDefault();
            if(activeCart == null)
            {
                activeCart = new Cart
                {
                    CustomerId = Id,
                    HasFinalised = false,
                    IsConfirmed = false,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(activeCart);
            }
            activeCart.addCartItem(ProductId);
            _context.Update(activeCart);
            return _context.SaveChanges();

            
        }*/
    }
}
