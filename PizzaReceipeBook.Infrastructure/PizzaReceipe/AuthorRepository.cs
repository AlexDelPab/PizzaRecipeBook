namespace PizzaReceipeBook.Infrastructure.PizzaReceipe;

public class PizzaRecipeRepository(AppDbContext context) : BaseRepository<Domain.PizzaRecipe>(context);