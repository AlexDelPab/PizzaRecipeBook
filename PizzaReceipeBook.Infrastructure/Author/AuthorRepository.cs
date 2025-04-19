using PizzaReceipeBook.Infrastructure.Contracts.Author;

namespace PizzaReceipeBook.Infrastructure.Author;

public class AuthorRepository(AppDbContext context) : BaseRepository<Domain.Author>(context), IAuthorRepository<Domain.Author>;