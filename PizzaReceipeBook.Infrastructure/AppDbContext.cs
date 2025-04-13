using Microsoft.EntityFrameworkCore;
using PizzaReceipeBook.Domain;

namespace PizzaReceipeBook.Infrastructure;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<PizzaRecipe> PizzaRecipes { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
}