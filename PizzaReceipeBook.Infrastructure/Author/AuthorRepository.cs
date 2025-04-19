namespace PizzaReceipeBook.Infrastructure.Author;

public class AuthorRepository(AppDbContext context) : BaseRepository<Domain.Author>(context);