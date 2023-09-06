using Microsoft.EntityFrameworkCore;
using ProductManager_New.Domain;

namespace ProductManager_New.Data;

public class ApplicationContext : DbContext
{
    public readonly string connectionString;

    public ApplicationContext(string connectionString)
    {
        this.connectionString = connectionString;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    public DbSet<Product> Product { get; set;}

}