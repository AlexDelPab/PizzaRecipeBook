using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PizzaReceipeBook.Infrastructure;

public abstract class BaseRepository<TEntity>(AppDbContext context) where TEntity : class
{
    private readonly AppDbContext _context = context;

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await this._context.Set<TEntity>().FindAsync(id);
    }
    
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await this._context.Set<TEntity>().ToListAsync();
    }
    
    public async Task<IEnumerable<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await this._context.Set<TEntity>().Where(predicate).ToListAsync();
    }
    
    public async Task<TEntity?> GetSingleByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await this._context.Set<TEntity>().SingleOrDefaultAsync(predicate);
    }
    
    public async void AddAsync(TEntity entity)
    {
        await this._context.Set<TEntity>().AddAsync(entity);
    }
    
    public void Update(TEntity entity)
    {
        this._context.Set<TEntity>().Update(entity);
    }
    
    public async Task SaveChangesAsync()
    {
        await this._context.SaveChangesAsync();
    }
}