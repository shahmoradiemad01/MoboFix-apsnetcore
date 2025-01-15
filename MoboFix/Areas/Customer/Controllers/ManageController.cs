using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoboFix.Areas.Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    [Area("Customer")]
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
