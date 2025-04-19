namespace PizzaReceipeBook.Infrastructure.Contracts.Author;

public interface IAuthorRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class;