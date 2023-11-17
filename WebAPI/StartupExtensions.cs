using MarketPlace.Api.Middleware;
using MarketPlace.Application;
using MarketPlace.Infrastructure;
using MarketPlace.Persistence;
using Microsoft.OpenApi.Models;
using WebAPI;

namespace MarketPlace.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureSwagger();

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddControllers().AddNewtonsoftJson(x =>
              x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Market Share API");
                });
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            var logger = app.Services.GetRequiredService<ILogger<Program>>();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCustomExceptionHandler();

            app.UseCors("Open");

            app.MapControllers();

            return app;
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Market Place API", Version = "v1" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "WebAPI.xml");
                c.IncludeXmlComments(filePath);
            });
        }
    }
}
