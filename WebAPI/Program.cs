using Microsoft.Extensions.Diagnostics.HealthChecks;
using Repository.Implementations;
using WebAPI.Extensions;

public class Program
{
    public static void Main(string [] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.ConfigureLogging(logging =>
        {
            logging.AddConsole();
            logging.AddDebug();
        });

        #region Swagger

        builder.Services.ConfigureSwagger();

        #endregion

        #region Database

        builder.Services.ConfigureDatabaseContext(builder.Configuration);

        #endregion

        builder.Services.AddTransient<UnitOfWork>();

        #region ApplicationService
        builder.Services.ConfigureCategoryService();
        builder.Services.ConfigureProductService();
        builder.Services.ConfigureOrderService();
        #endregion

        #region Repository

        builder.Services.ConfigureUnitOfWork();

        #endregion

        builder.Services.ConfigureCors();

       /* builder.Services.ConfigureResponseCaching();

        builder.Services.ConfigureHttpCacheHeaders();*/

        builder.Services.ConfigureResponseMessage();

        builder.Services.AddHealthChecks()
            .AddSqlServer(builder.Configuration.GetConnectionString("IlisStoreDB"),
              name: "ilisDb-check",
              failureStatus: HealthStatus.Unhealthy,
              tags: new string[] { "api", "SqlDb" });

        builder.Services.ConfigureValidationFilterAttribute();

        builder.Services.AddControllers().AddNewtonsoftJson(x =>
              x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        var logger = app.Services.GetRequiredService<ILogger<Program>>();

        app.ConfigureExceptionHandler(logger);

        app.UseRouting();

        app.UseCors("CorsPolicy");
      /*  app.UseResponseCaching();
        app.UseHttpCacheHeaders();*/

        app.UseAuthorization();

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "IlisStore V1");
        });


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}

