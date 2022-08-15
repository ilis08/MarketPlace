using Azure.Storage.Blobs;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.Contracts;
using Repository.Implementations;
using WebAPI.Filters;

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
                c.EnableAnnotations();
            });
        }

        public static void ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("IlisStore"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                    sqlOptions.MigrationsAssembly("WebAPI");
                });
            });
        }

        public static void ConfigureProductImageService(this IServiceCollection services)
        {
            services.AddScoped<IProductImage, ProductImage>();
        }

        public static void ConfigureValidationFilterAttribute(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
        }

        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(opts =>
            {
                opts.AddPolicy("CorsPolicy", builder =>
                    builder.WithOrigins("https://www.ilisstoreclient.somee.com/")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
            });

        public static void ConfigureResponseCaching(this IServiceCollection services) =>
            services.AddResponseCaching();

        public static void ConfigureHttpCacheHeaders(this IServiceCollection services) =>
            services.AddHttpCacheHeaders(
                (expiration) =>
                {
                    expiration.MaxAge = 180;
                    expiration.CacheLocation = Marvin.Cache.Headers.CacheLocation.Private;
                },
                (validationOpt) =>
                {
                    validationOpt.MustRevalidate = true;
                }
        );

        public static void ConfigureBlobService(this IServiceCollection services, IConfiguration configuration) {
            services.AddSingleton(x => new BlobServiceClient(configuration.GetConnectionString("AzureBlobStorage")));

            services.AddScoped<IBlobService, BlobService>();
        }
    }
}
