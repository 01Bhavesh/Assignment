using Microsoft.EntityFrameworkCore;

namespace AssignmentDotNet.Models
{
    public class ProductDb : DbContext
    {
        public ProductDb(DbContextOptions<ProductDb> o) : base(o)
        {
            
        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }

    }
}
