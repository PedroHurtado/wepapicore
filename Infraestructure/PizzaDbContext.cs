using Microsoft.EntityFrameworkCore;

namespace webapinet.Controllers;

public class PizzaDbContext : DbContext
{

    public DbSet<Pizza> Pizzas => Set<Pizza>();
    public PizzaDbContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PizzaConfiguration());
    }
}