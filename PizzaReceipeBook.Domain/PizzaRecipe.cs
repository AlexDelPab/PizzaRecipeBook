namespace PizzaReceipeBook.Domain;

public class PizzaRecipe
{
    // Parameterless constructor required by EF Core
    public PizzaRecipe()
    {
    }

    public PizzaRecipe(Guid id,
        string name,
        string ingredients,
        string instructions,
        TimeSpan preparationTime,
        TimeSpan cookingTime,
        Guid authorId,
        Author author)
    {
        Id = id;
        Name = name;
        Ingredients = ingredients;
        Instructions = instructions;
        PreparationTime = preparationTime;
        CookingTime = cookingTime;
        AuthorId = authorId;
        Author = author;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public TimeSpan PreparationTime { get; set; }
    public TimeSpan CookingTime { get; set; }
    public Guid AuthorId { get; set; }
    public Author Author { get; set; }
}