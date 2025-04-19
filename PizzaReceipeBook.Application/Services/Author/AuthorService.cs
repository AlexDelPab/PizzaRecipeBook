using PizzaReceipeBook.Application.Responses.Author;
using PizzaReceipeBook.Infrastructure.Contracts.Author;

namespace PizzaReceipeBook.Application.Services.Author;

public class AuthorService(IAuthorRepository<Domain.Author> authorRepository)
{
    public async Task<Guid> CreateAuthorAsync(string firstName, string lastName, string bio)
    {
        var author = new Domain.Author(Guid.NewGuid(), firstName, lastName, bio);
        authorRepository.AddAsync(author);
        await authorRepository.SaveChangesAsync();

        return author.Id;
    }

    public async Task<GetAuthorByNameResponse?> GetAuthorByNameAsync(string name)
    {
        var author = await authorRepository.GetSingleByAsync(
            a => a.FirstName.ToLower().Contains(name.ToLower()) || a.LastName.ToLower().Contains(name.ToLower()));

        return author is null ? null : new GetAuthorByNameResponse(author.Id, author.FirstName, author.LastName);
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsForDropdownSelectAsync()
    {
        return (await authorRepository.GetAllAsync())
            .Select(a => new AuthorDto(a.Id, $"{a.FirstName} {a.LastName}"));
    }
}