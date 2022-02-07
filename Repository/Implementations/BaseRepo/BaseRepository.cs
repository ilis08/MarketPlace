using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations.BaseRepo
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
		protected RepositoryContext repositoryContext;

		public BaseRepository(RepositoryContext _repositoryContext)
			=> repositoryContext = _repositoryContext;

		public IQueryable<T> FindAll() => repositoryContext.Set<T>();

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => repositoryContext.Set<T>()
																										.Where(expression);
		public void Create(T entity) => repositoryContext.Set<T>().Add(entity);

		public void Update(T entity) => repositoryContext.Set<T>().Update(entity);

		public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);
	}
}
