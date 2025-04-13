namespace PizzaReceipeBook.Domain;

public class PizzaRecipe
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public TimeSpan PreparationTime { get; set; }
    public TimeSpan CookingTime { get; set; }
    public bool IsVegetarian { get; set; }
    public bool IsVegan { get; set; }
    public int Servings { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid AuthorId { get; set; }
    public Author Author { get; set; }
}