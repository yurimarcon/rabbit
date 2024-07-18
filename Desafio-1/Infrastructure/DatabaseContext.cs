using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class DatabaseContext : DbContext
{
    public DatabaseContext() { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
}
