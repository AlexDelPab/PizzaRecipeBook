using PizzaReceipeBook.Infrastructure.Contracts.PizzaReceipe;

namespace PizzaReceipeBook.Infrastructure.PizzaReceipe;

public class PizzaRecipeRepository(AppDbContext context) : BaseRepository<Domain.PizzaRecipe>(context), IPizzaReceipeRepository<Domain.PizzaRecipe>;