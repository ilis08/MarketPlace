using Microsoft.Extensions.DependencyInjection;
using Repository.Contracts;
using Repository.Implementations;

namespace Repository.Extensions
{
    public static class RepositoryExtensions
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Implementations.Repository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
