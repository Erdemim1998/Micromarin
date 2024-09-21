using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace DynamicData.Concrete
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Table> Tables => Set<Table>();

        public DbSet<Field> Fields => Set<Field>();

        public DbSet<Product> Products => Set<Product>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<SubCategory> SubCategories => Set<SubCategory>();

        public DbSet<Brand> Brands => Set<Brand>();

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();
    }
}
