namespace PizzaReceipeBook.Domain;

public class Author(Guid id, string firstName, string lastName, string bio)
{
    public Guid Id { get; set; } = id;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Bio { get; set; } = bio;
}