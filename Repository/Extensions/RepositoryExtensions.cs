using Microsoft.Extensions.DependencyInjection;
using Repository.Implementations.BaseRepo;
using Repository.Implementations.OrderRepo;
using Repository.Implementations.ProductRepo;

namespace Repository.Extensions
{
    public static class RepositoryExtensions
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Implementations.BaseRepo.Repository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
