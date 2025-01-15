using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MoboFix.Data;
using MoboFix.Models;
using Microsoft.AspNetCore.Authorization;
using MoboFix.Areas.Admin.ViewModels;

namespace MoboFix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var users = _context.Users.Where(u => u.EmailConfirmed == false).ToList();
            return View(users);
        }
        public IActionResult Details(string id)
        {
            var user = _context.Users.Find(id);
            var shop = _context.Shops.Where(s => s.UserId == id).SingleOrDefault();
            var VM = new UserViewModel()
            {
                UserId = id,
                UserName = user.UserName,
                UserEmail = user.Email,
                CertificationCode = shop.CertificationCode,
                PhoneNumber = shop.PhoneNumber,
                ShopName = shop.Name
            };
            return View(VM);
        }
        [HttpPost]
        public IActionResult ConfirmUser(string userId)
        {
            var user = _context.Users.Find(userId);
            var shop = _context.Shops.Where(s => s.UserId == userId).SingleOrDefault();
            user.EmailConfirmed = true;
            user.UserName = shop.Name;
            _context.Users.Update(user);
            var response = new { status = true, message = "Default Message" };
            try
            {
                _context.SaveChanges();
                response = new{ status = true, message = "User Confirmed" };
            }catch(Exception e)
            {
                response = new{ status = false, message = e.Message };

            }
            return Json(response);
        }
        [HttpPost]
        public IActionResult DeleteUser(string userId)
        {
            var user = _context.Users.Find(userId);
            _context.Users.Remove(user);
            var shop = _context.Shops.Where(s => s.UserId == userId).FirstOrDefault();
            _context.Shops.Remove(shop);
            var response = new { status = true, message = "Default Message" };
            try
            {
                _context.SaveChanges();
                response = new { status = true, message = "User Deleted" };
            }
            catch (Exception e)
            {
                response = new { status = false, message = e.Message };

            }
            return Json(response);

        }
    }
}
