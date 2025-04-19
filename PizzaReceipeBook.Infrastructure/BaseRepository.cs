using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PizzaReceipeBook.Infrastructure;

public abstract class BaseRepository<TEntity>(AppDbContext context) : IBaseRepository<TEntity> where TEntity : class
{
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }
    
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
    }
    
    public async Task<IEnumerable<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await context.Set<TEntity>().Where(predicate).ToListAsync();
    }
    
    public async Task<TEntity?> GetSingleByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await context.Set<TEntity>().SingleOrDefaultAsync(predicate);
    }
    
    public async void AddAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
    }
    
    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}