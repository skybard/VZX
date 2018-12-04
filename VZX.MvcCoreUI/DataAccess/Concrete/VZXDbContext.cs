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
    }
}
