using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoboFix.Data;
using MoboFix.Models;
using MoboFix.Services;
using System.Diagnostics;

namespace MoboFix.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IAdvertismentService _advertismentService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IAdvertismentService advertismentService)
        {
            _logger = logger;
            _context = context;
            _advertismentService = advertismentService;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAdvertisments(int page = 1, int pageSize = 10)
        {
            var advertisments = _advertismentService.GetAllProducts()
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
            int totalAdvertisment = _advertismentService.GetAllProducts().Count();

            var result = new
            {
                Advertisments = advertisments,
                TotalPages = (int)Math.Ceiling((double)totalAdvertisment / pageSize),
                CurrentPage = page
            };

            return PartialView("_AdvertismentListPartial", result);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}