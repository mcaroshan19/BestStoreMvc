using BestStoreMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace BestStoreMvc.Database
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<Product> Products { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Manager> Managers { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.City)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Cascade);  

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Manager>().ToTable("Managers");
        }

       




    }
}