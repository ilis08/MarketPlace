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
    public class Repository : IRepository
    {
		protected RepositoryContext repositoryContext;

		public Repository(RepositoryContext _repositoryContext)
			=> repositoryContext = _repositoryContext;

        public DbSet<T> DbSet<T>() where T : class
        {
            return this.repositoryContext.Set<T>();
        }

		public IQueryable<T> FindAll<T>() where T : class => DbSet<T>().AsQueryable();

		public IQueryable<T> FindByCondition<T>(Expression<Func<T, bool>> expression) where T : class => DbSet<T>()
																										.Where(expression);
        public async Task<T> FindByIdAsync<T>(int id) where T : class => await DbSet<T>().FindAsync(id);
		public async Task CreateAsync<T>(T entity) where T : class => await DbSet<T>().AddAsync(entity);

		public void Update<T>(T entity) where T : class => DbSet<T>().Update(entity);

		public void Delete<T>(T entity) where T : class => DbSet<T>().Remove(entity);

        public async Task SaveChangesAsync()
        {
            await repositoryContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            repositoryContext.SaveChanges();
        }

        public void Dispose()
        {
            repositoryContext.Dispose();
        }
    }
}
