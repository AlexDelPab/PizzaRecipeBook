namespace PizzaReceipeBook.Domain;

public class PizzaRecipe(
    Guid id,
    string name,
    string ingredients,
    string instructions,
    TimeSpan preparationTime,
    TimeSpan cookingTime,
    Guid authorId,
    Author author)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Ingredients { get; set; } = ingredients;
    public string Instructions { get; set; } = instructions;
    public TimeSpan PreparationTime { get; set; } = preparationTime;
    public TimeSpan CookingTime { get; set; } = cookingTime;
    public Guid AuthorId { get; set; } = authorId;
    public Author Author { get; set; } = author;
}