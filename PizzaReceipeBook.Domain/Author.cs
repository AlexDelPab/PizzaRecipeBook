namespace PizzaReceipeBook.Domain;

public class Author(Guid id, string firstName, string lastName, string bio)
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
}