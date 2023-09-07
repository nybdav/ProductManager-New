using Microsoft.EntityFrameworkCore;
using ProductManager_New.Domain;

namespace ProductManager_New.Data;

public class ApplicationContext : DbContext
{
    private string connectionString = "Server=.;Database=ProductManager;Integrated Security=true;Encrypt=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    public DbSet<Product> Product { get; set; }
}
