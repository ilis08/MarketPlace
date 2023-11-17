using System.Linq.Expressions;

namespace MarketPlace.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : class
{
    Task<List<T>> FindAllAsync();
    Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
    Task<T> FindByIdAsync(long id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
