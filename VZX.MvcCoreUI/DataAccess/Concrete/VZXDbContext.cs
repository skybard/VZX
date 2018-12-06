using Microsoft.EntityFrameworkCore;
using VZX.MvcCoreUI.Entities.Concrete;

namespace VZX.MvcCoreUI.DataAccess.Concrete
{
    public class VZXDbContext : DbContext
    {
        public VZXDbContext(DbContextOptions<VZXDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne<Brand>(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);
        }
    }
}
