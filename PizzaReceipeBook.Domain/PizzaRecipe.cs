namespace PizzaReceipeBook.Domain;

public class PizzaRecipe
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public TimeSpan PreparationTime { get; set; }
    public TimeSpan CookingTime { get; set; }
    public Guid AuthorId { get; set; }
    public Author Author { get; set; }
}