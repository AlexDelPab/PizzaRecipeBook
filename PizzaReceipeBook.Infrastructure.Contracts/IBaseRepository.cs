using System.Linq.Expressions;

namespace PizzaReceipeBook.Infrastructure.Contracts;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetSingleByAsync(Expression<Func<TEntity, bool>> predicate);
    void AddAsync(TEntity entity);
    void Update(TEntity entity);
    Task SaveChangesAsync();
}