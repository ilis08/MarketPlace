using System.Linq.Expressions;

namespace Repository.Implementations.BaseRepo
{
    public interface IRepository : IDisposable
    {
        IQueryable<T> FindAll<T>() where T : class;
        IQueryable<T> FindByCondition <T>(Expression<Func<T, bool>> expression) where T : class;
        Task<T> FindByIdAsync <T>(int id) where T : class;
        Task CreateAsync<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
