using MarketPlace.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MarketPlace.Persistence.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    protected readonly MarketPlaceDbContext dbContext;

    public BaseRepository(MarketPlaceDbContext _dbContext)
        => dbContext = _dbContext;

    public async Task<List<T>> FindAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<T> FindByIdAsync(long id)
    {
        return await dbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}
