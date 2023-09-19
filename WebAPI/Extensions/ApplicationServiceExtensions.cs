using ApplicationService.Contracts;
using ApplicationService.Implementations;

namespace WebAPI.Extensions;

public static class ApplicationServiceExtensions
{
    /// <summary>
    /// Service for CRUD of Category class
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureCategoryService(this IServiceCollection services)
    {
        services.AddScoped<ICategoryManagementService,CategoryManagementService>();
    }

    /// <summary>
    /// Service for CRUD of Product class 
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureProductService(this IServiceCollection services)
    {
        services.AddScoped<IProductManagementService, ProductManagementService>();
    }

    /// <summary>
    /// Service for CRUD of Order class
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureOrderService(this IServiceCollection services)
    {
        services.AddScoped<IOrderManagementService, OrderManagementService>();
    }

    /// <summary>
    /// Service for CRUD of Seller class
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureSellerService(this IServiceCollection services)
    {
        services.AddScoped<ISellerManagementService, SellerManagementService>();
    }

    public static void ConfigureTokenService(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
    }
}
