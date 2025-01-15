using Microsoft.AspNetCore.Mvc;
using MoboFix.Models;
using MoboFix.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MoboFix.Utilities.Program.Status;
using MoboFix.Utilities.Program.Messages;
using MoboFix.Utilities.Program.Paths;
using Microsoft.AspNetCore.Authorization;

namespace MoboFix.Areas.Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _he;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(ApplicationDbContext context, IHostingEnvironment he, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _he = he;
            _userManager = userManager;
        }
        [HttpPost]
        public JsonResult AddToCart(int id)
        {
            var user = _userManager.GetUserId(User);
            var cart = _context.Carts.SingleOrDefault(c => c.CustomerId == user && c.HasFinalised == false);
            if(cart == null)
            {
                cart = new Cart()
                {
                    CustomerId = user,
                    HasFinalised = false,
                    IsConfirmed = false,
                    CartItems = new List<CartItem>()
                  
                    
                };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }
            var cartItems = _context.CartItems.Where(c => c.CartId == cart.Id).ToList();
            cart.CartItems = (cartItems != null) ? cartItems : new List<CartItem>();
            var item = cart.addCartItem(id);
            if(_context.CartItems.Find(item.Id) == null)
                _context.CartItems.Add(item);
            else
                _context.CartItems.Update(item);

            var response = new{success = true,message = "default message"};
            try
            {
                _context.SaveChanges();
                response = new
                {
                    success = true,
                    message = "محصول با موفقیت به سبد اضافه شد"
                };
            }
            catch (Exception ex)
            {
                response = new
                {
                    success = false,
                    message = ex.Message
                };
            }

            return Json(response);
        }
        public IActionResult Cart(int? id)
        {
            var user = _userManager.GetUserId(User);
            Cart cart = new();
            if (id == null)
                cart = _context.Carts.SingleOrDefault(c => c.CustomerId == user && c.HasFinalised == false);
            else
                cart = _context.Carts.Find(id);
            var items = _context.CartItems.Where(i => i.CartId == cart.Id).Include(i => i.Product).ThenInclude(p => p.Seller) .ToList();
            cart.CartItems = items;


            return View(cart);
        }
        public IActionResult Carts()
        {
            var user = _userManager.GetUserId(User);
            var carts = new List<Cart>();
            carts = _context.Carts.Where(c => c.CustomerId == user).ToList();
            foreach (var cart in carts)
            {
                var items = _context.CartItems.Where(i => i.CartId == cart.Id).Include(i => i.Product).ThenInclude(p => p.Seller).ToList();
                cart.CartItems = items;
            }

            return View(carts);
        }
        [HttpPost]
        public JsonResult Checkout(int id)
        {
            var cart = _context.Carts.SingleOrDefault(c => c.Id == id);
            cart.HasFinalised = true;
            cart.FinalisedDate = DateTime.Now;
            _context.Carts.Update(cart);
            _context.SaveChanges();

            var response = new { success = true, message = "default message" };
            try
            {
                _context.SaveChanges();
                response = new
                {
                    success = true,
                    message = "ثبت شد"
                };
            }
            catch (Exception ex)
            {
                response = new
                {
                    success = false,
                    message = ex.Message
                };
            }

            return Json(response);
        }
    
    }
}
