using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoboFix.Areas.Seller.Controllers
{
    [Authorize(Roles = "Seller")]
    [Area("Seller")]
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
