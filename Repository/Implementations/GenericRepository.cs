using Data.Context;
using Data.Entitites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        internal Store4DBContext context;
        internal DbSet<T> dbSet;
        private readonly IWebHostEnvironment _environment;

        public GenericRepository(Store4DBContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            this._environment = environment;
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

        public virtual void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }      
    }
}
