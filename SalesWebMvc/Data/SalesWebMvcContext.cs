using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Data
{
    public class SalesWebMvcContext : DbContext
    {
        public SalesWebMvcContext(DbContextOptions<SalesWebMvcContext> options)
            : base(options)
        {
        }

        public DbSet<SalesWebMvc.Models.Departament> Departament { get; set; } = default!;
        public DbSet<SalesWebMvc.Models.Seller> Seller { get; set; } = default!;
        public DbSet<SalesWebMvc.Models.SalesRecord> SalesRecord { get; set; } = default!;
    }
}
