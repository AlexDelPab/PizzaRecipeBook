using Microsoft.EntityFrameworkCore;
using PizzaReceipeBook.Application.Responses.Author;
using PizzaReceipeBook.Infrastructure.Author;

namespace PizzaReceipeBook.Application.Services.Author;

public class AuthorService(AuthorRepository authorRepository)
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
            a => EF.Functions.Like(a.FirstName.ToLower(), $"%{name.ToLower()}%") ||
                 EF.Functions.Like(a.LastName.ToLower(), $"%{name.ToLower()}%"));

        return author is null ? null : new GetAuthorByNameResponse(author.Id, author.FirstName, author.LastName);
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsForDropdownSelectAsync()
    {
        return (await authorRepository.GetAllAsync())
            .Select(a => new AuthorDto(a.Id, $"{a.FirstName} {a.LastName}"));
    }
}