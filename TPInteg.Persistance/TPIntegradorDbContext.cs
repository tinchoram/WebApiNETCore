using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TPInteg.Shared;

namespace TPInteg.Persistance;
public class TPIntegradorDbContext : DbContext
{
    public TPIntegradorDbContext()
    {
            
    }
    public TPIntegradorDbContext(DbContextOptions<TPIntegradorDbContext> options) : base(options)
    {        
    }
    public DbSet<Producto> Producto { get; set; }
    public DbSet<Proveedor> Proveedor { get; set; }
    public DbSet<Localidad> Localidad { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
