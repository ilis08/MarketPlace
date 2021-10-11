using ApplicationService.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        /// <summary>
        /// Service for CRUD of Category class
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCategoryService(this IServiceCollection services)
        {
            services.AddTransient<CategoryManagementService>();
        }

        /// <summary>
        /// Service for CRUD of Product class 
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureProductService(this IServiceCollection services)
        {
            services.AddTransient<ProductManagementService>();
        }

        /// <summary>
        /// Service for CRUD of Order class
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureOrderService(this IServiceCollection services)
        {
            services.AddTransient<OrderManagementService>();
        }
    }
}
