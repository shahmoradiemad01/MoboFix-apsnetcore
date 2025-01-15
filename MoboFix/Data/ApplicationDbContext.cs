using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoboFix.Models;

namespace MoboFix.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

/*        public DbSet<ApplicationUserSeller> ApplicationUserSellers { get; set; }
        public DbSet<ApplicationUserCustomer> ApplicationUserCustomers{ get; set; }*/

        public DbSet<Advertisment> Advertisments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Shop> Shops { get; set; }



    }
}