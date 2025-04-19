namespace PizzaReceipeBook.Application.Requests.Author;

public record CreateAuthorRequest(string FirstName, string LastName, string Bio);