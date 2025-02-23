using BanHangMVC.Models.EF;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BanHangMVC.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ads> Adss { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<New> New { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Products> Productss { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Subcribe> Subcribes { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
