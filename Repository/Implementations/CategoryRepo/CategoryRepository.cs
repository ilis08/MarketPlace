using Data.Context;
using Data.Entitites;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.BaseRepo;

namespace Repository.Implementations.CategoryRepo
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync() => await FindAll().ToListAsync();

        public async Task<Category> GetCategoryByIdAsync(int id) => await FindByCondition(x => x.Id == id).SingleOrDefaultAsync();
    }
}
