using Data.Context;
using Data.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T>, IProductRepository where T : class 
    {
        internal Store3DBContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(Store3DBContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual void Create(T entity)
        {
            context.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public ICollection<T> Find(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual ICollection<T> Get()
        {
           return context.Set<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public Product GetProductById(int id)
        {
            return context.Products.Where(p => p.Id == id).Include(x => x.Category).FirstOrDefault();
        }

        public IEnumerable<Product> GetProducts()
        {
            return context.Products.Include(x => x.Category).ToList();
        }

        public virtual void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }
    }
}
