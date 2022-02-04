using Data.Context;
using Data.Entitites;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await context.Categories.FindAsync(id);
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
