using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MoboFix.Data;
using MoboFix.Models;
using Microsoft.AspNetCore.Authorization;

namespace MoboFix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Seller")]
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
            return View(user);
        }
    }
}
