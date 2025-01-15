using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoboFix.Data;
using MoboFix.Models;

namespace MoboFix.Areas.Seller.Controllers
{
    [Authorize(Roles = "Seller")]
    [Area("Seller")]
    public class AdvertisementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdvertisementController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = _userManager.GetUserId(User);
            var Advertisments = _context.Advertisments.Where(a => a.SellerId == user).Include(a => a.Seller).Include(a => a.Product).ToList();
            return View(Advertisments);
        }
    }
}
