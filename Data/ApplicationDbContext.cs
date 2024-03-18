using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QRMenu_TabGida.Models;

namespace QRMenu_TabGida.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandUser> BrandUsers { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantUser> RestaurantUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasOne(au => au.Status).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Restaurant>().HasOne(r => r.Status).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RestaurantUser>().HasOne(ru => ru.Status).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Brand>().HasOne(b => b.Status).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BrandUser>().HasOne(bu => bu.Status).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Category>().HasOne(c => c.Status).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Food>().HasOne(f => f.Status).WithMany().OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}