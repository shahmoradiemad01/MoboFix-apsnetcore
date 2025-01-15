using MoboFix.Models;
using MoboFix.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MoboFix.Services
{
    public interface IAdvertismentService
    {
        List<Advertisment> GetAllProducts();
    }

    public class AdvertismentService : IAdvertismentService
    {
        private readonly ApplicationDbContext _context;
        private List<Advertisment> _advertisment;
        public AdvertismentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Advertisment> GetAllProducts()
        {
            _advertisment = _context.Advertisments.Include(a => a.Product).ToList();
            foreach (var ad in _advertisment)
            {
                ad.Seller = _context.Users.Find(ad.SellerId);
            }

            if (_advertisment.Count == 0)
            {
                _advertisment.Add(new Advertisment()
                {
                    Id = -1,
                    ProductId = -1,
                    SellerId = "000",

                });
            }

            return _advertisment;
        }
    }
}


