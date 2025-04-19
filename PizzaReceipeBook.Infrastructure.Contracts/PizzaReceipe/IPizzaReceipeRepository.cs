namespace PizzaReceipeBook.Infrastructure.Contracts.PizzaReceipe;

public interface IPizzaReceipeRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class;