namespace PizzaReceipeBook.Application.Responses.Author;

public record GetAuthorByNameResponse(Guid id, string firstName, string lastName);