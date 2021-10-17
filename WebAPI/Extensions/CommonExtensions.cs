using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Repository.Implementations;
using Repository.Implementations.ProductRepo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Messages;

namespace WebAPI.Extensions
{
    public static class CommonExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IlisStoreAPI", Version = "v1" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "WebAPI.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public static void ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opts =>
              opts.UseSqlServer(configuration.GetConnectionString("IlisStore"),
              b => b.MigrationsAssembly("WebAPI")));
        }

        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<UnitOfWork>();
        }

        public static void ConfigureResponseMessage(this IServiceCollection services)
        {
            services.AddTransient<ResponseMessage>();
        }

        public static void ConfigureProductImageService(this IServiceCollection services)
        {
            services.AddTransient<ProductImage>();
        }
    }
}
