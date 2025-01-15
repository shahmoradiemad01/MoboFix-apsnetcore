using Microsoft.AspNetCore.Mvc;
using MoboFix.Areas.Seller.ViewModels;
using MoboFix.Models;
using MoboFix.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MoboFix.Utilities.Program.Status;
using MoboFix.Utilities.Program.Messages;
using MoboFix.Utilities.Program.Paths;
using Microsoft.AspNetCore.Authorization;

namespace MoboFix.Areas.Seller.Controllers
{
    [Authorize(Roles = "Seller")]
    [Area("Seller")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _he;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProductController(ApplicationDbContext context, IHostingEnvironment he, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _he = he;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var products = _context.Products.Where(p => p.SellerId == userId).ToList();
            List<Tuple<Product, int>> list = new();
            foreach(var item in products)
            {
                var ad = _context.Advertisments.SingleOrDefault(a => a.ProductId == item.Id);
                var status = ((ad != null) ? ad.Status : ProgramStatusCodes.NA);
                Tuple<Product, int> i = new(item, status);
                list.Add(i);
            }
            return View(list);
        }

        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductViewModel model, IFormFile image)
        {
            var user = _userManager.GetUserId(User);
            var userName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                var searchProduct = _context.Products.FirstOrDefault(c => c.CodeNumber == model.CodeNumber);
                if (searchProduct != null && searchProduct.SellerId != user)
                {
                    ViewBag.message = Messages.RepeteadProductName;
                    return View(model);
                }
                else
                {
                    var NewProduct = new Product { Name = model.Name,
                    Price = model.Price,
                    CodeNumber = model.CodeNumber,
                    Brand = model.Brand,
                    Country = model.Country,
                    SellerId = user };

                    _context.Products.Add(NewProduct);
                    if (image == null)
                    {
                        NewProduct.ImagePath = ProgramPaths.DefaultProductImagePath;
                    }
                    _context.SaveChanges();
                    if (image != null)
                    {
                        var imageName = userName + "--" + NewProduct.Id + Path.GetExtension(image.FileName);
                        var name = Path.Combine(_he.WebRootPath + "/" + ProgramPaths.ProductsImagesPath, imageName);
                        image.CopyToAsync(new FileStream(name, FileMode.Create));
                        NewProduct.ImagePath = ProgramPaths.ProductsImagesPath + "/" + imageName;
                        _context.Update(NewProduct);
                        _context.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
                return View(model);
        }

        public IActionResult EditProduct(int id)
        {
            Product product = _context.Products.FirstOrDefault(c => c.Id == id);
            if (product == null)
                return NotFound();
            ProductViewModel model = new ProductViewModel()
            {
                Brand = product.Brand,
                CodeNumber = product.CodeNumber,
                Country = product.Country,
                Price = (int)product.Price,
                Name = product.Name,
                id = product.Id
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult EditProduct(ProductViewModel model, IFormFile image)
        {
            
            if (ModelState.IsValid)
            {
                var searchProduct = _context.Products.FirstOrDefault(c => c.Name == model.Name);
                if (searchProduct != null && searchProduct.Id != model.id)
                {
                    ViewBag.message = Messages.RepeteadProductName;
                    return View(model);
                }
                var UpdatingProduct = _context.Products.SingleOrDefault(p => p.Id == model.id);

                if (image != null)
                {
                    var userName = _context.Users.Find(UpdatingProduct.SellerId).UserName;
                    var NewImageName = userName + "--" + UpdatingProduct.Id + Path.GetExtension(image.FileName);
                    var name = Path.Combine(_he.WebRootPath + "/" + ProgramPaths.ProductsImagesPath, NewImageName);
                    image.CopyToAsync(new FileStream(name, FileMode.Create));
                    UpdatingProduct.ImagePath = ProgramPaths.ProductsImagesPath + "/" + NewImageName;
                }

                UpdatingProduct.CodeNumber = model.CodeNumber;
                UpdatingProduct.Country = model.Country;
                UpdatingProduct.Name = model.Name;
                UpdatingProduct.Price = model.Price;
                _context.Products.Update(UpdatingProduct);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult DeleteProduct(int id)
        {
            var model = _context.Products.Find(id);
            if (model == null)
                return NotFound();
            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteProduct(Product product)
        {
            var file = _he.WebRootPath + "/" + product.ImagePath;
            if (System.IO.File.Exists(file))
            {
                try
                { System.IO.File.Delete(file); }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("\n\n"+ex.Message+ "\n\n");
                }
            }
            _context.Products.Remove(product);
            if(_context.SaveChanges() != 0)
                return RedirectToAction("Index");
            return View(product);
        }
        public IActionResult CreateAdvertisement(int id)
        {
            ViewBag.Status = StatusCodes.Status102Processing;

            var user = _userManager.GetUserId(User);
            Product product = _context.Products.Find(id);

            if (user == null)
                ViewBag.Status = StatusCodes.Status401Unauthorized;
            else if (product == null)
                ViewBag.Status = StatusCodes.Status400BadRequest;
            else if (product.SellerId == user)
            {
                if (_context.Advertisments.Where(a => a.ProductId == id).ToList().Count == 0)
                {
                    Advertisment advertisment = new()
                    {
                        Status = ProgramStatusCodes.Pending,
                        SellerId = user,
                        ProductId = id,
                    };
                    _context.Add(advertisment);
                    _context.SaveChanges();
                    ViewBag.Status = StatusCodes.Status201Created;
                }
                ViewBag.Status = StatusCodes.Status208AlreadyReported;
            }
            else
                ViewBag.Status = StatusCodes.Status500InternalServerError;
            return RedirectToAction("Index");

        }
    }
}

