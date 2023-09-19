using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : class
{
    Task<List<T>> FindAllAsync();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
    Task<T> FindByIdAsync<T>(long id) where T : class;
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task SaveChangesAsync();
}
