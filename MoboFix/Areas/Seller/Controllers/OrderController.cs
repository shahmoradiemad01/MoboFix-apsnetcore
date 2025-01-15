using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoboFix.Areas.Seller.ViewModels;
using MoboFix.Data;
using MoboFix.Models;
using MoboFix.Utilities.Program.Status;

namespace MoboFix.Areas.Seller.Controllers
{
    [Authorize(Roles = "Seller")]
    [Area("Seller")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var list = _context.CartItems.Include(i=> i.Product).Where(p => p.Product.SellerId == userId && p.Status == ProgramStatusCodes.Pending).ToList();
            var listOrder = new List<OrderItemViewModel>();
            foreach(var i in list)
            {
                listOrder.Add(new OrderItemViewModel()
                {
                    Id = i.Id,
                    Customer = _context.Users.Find(_context.Carts.Find(i.CartId).CustomerId).UserName,
                    Quantity = i.Quantity,
                    Name = i.Product.Name,
                    Status = i.Status
                });
            }
            return View(listOrder);
        }

        [HttpPost]
        public JsonResult SetOrder(int id, int status)
        {
            var order = _context.CartItems.Find(id);
            var s_msg = new String("");
            if (status == ProgramStatusCodes.Accepted)
                s_msg = "Accepted";
            if (status == ProgramStatusCodes.Rejected)
                s_msg = "Rejected";
            order.Confirmation(status, s_msg);
            _context.Update(order);

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
