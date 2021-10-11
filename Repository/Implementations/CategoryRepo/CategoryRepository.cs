using Data.Context;
using Data.Entitites;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.OrderRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations.CategoryRepo
{
    public class CategoryRepository : ICategoryRepository
    {
        internal RepositoryContext context;

        public CategoryRepository(RepositoryContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategorytByIdAsync(int id)
        {
            return await context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public void Create(Category entity)
        {
            context.Add(entity);
        }

        public void Update(Category entity)
        {
            context.Update(entity);
        }

        public void Delete(Category entity)
        {
            context.Remove(entity);
        }
    }
}
