using ECommerceAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.DbContext
{
    public class ShoppingDbContext : IdentityDbContext<User>
    {
        public ShoppingDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "1",
                    Name = "Admin",
                    ConcurrencyStamp = "1",
                    NormalizedName = "ADMIN"
                },
                 new IdentityRole()
                 {
                     Id = "2",
                     Name = "User",
                     ConcurrencyStamp = "2",
                     NormalizedName = "USER"
                 });
            builder.Entity<Category>().HasData(
                new Category()
                {
                    CategoryId = 1,
                    CategoryName = "Toys",
                    CreatedOn = DateTime.Now.ToString("yyyy-MM-dd"),
                    CreatedBy = "Admin",
                    UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd"),
                    UpdatedBy = "Admin"
                },
                 new Category()
                 {
                     CategoryId = 2,
                     CategoryName = "Electronics",
                     CreatedOn = DateTime.Now.ToString("yyyy-MM-dd"),
                     CreatedBy = "Admin",
                     UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd"),
                     UpdatedBy = "Admin"
                });
           
            builder.Entity<User>().HasData(
                new User()
                {
                    Id = "1",
                    Name = "Admin",
                    UserName = "Admin@gmail.com",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    Email = "Admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "Admin@123")
                });
            builder.Entity<IdentityUserRole<string>>().HasData(
               new IdentityUserRole<string>() { RoleId = "1", UserId = "1" }
               );
            base.OnModelCreating(builder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
