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
    public class GenericRepository<T> : IGenericRepository<T>, IProductRepository where T : class 
    {
        internal Store3DBContext context;
        internal DbSet<T> dbSet;
        private readonly IWebHostEnvironment _environment;

        public GenericRepository(Store3DBContext context, IWebHostEnvironment environment)
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
        
        public string SaveImageAsync(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');

            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            var imagePath = Path.Combine("C:/DistributedProject/IlisStoreSln/StoreAdminMVC/wwwroot/","Images/", imageName);

            var imagePathStore = Path.Combine("C:/DistributedProject/IlisStoreSln/StoreMVC/wwwroot/", "Images/", imageName);


            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
               imageFile.CopyTo(fileStream);
            }

            using (var fileStream = new FileStream(imagePathStore, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }


            return imageName;
        }
    }
}
