using Repository.Extensions;
using WebAPI.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebAPI;
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

        #region ApplicationService
        builder.Services.ConfigureCategoryService();
        builder.Services.ConfigureProductService();
        builder.Services.ConfigureOrderService();
        #endregion

        #region Repository
        builder.Services.ConfigureRepository();

        builder.Services.ConfigureProductImageService();
        #endregion

        builder.Services.AddIdentity();

        builder.Services.AddAuthentication(builder.Configuration);

        builder.Services.ConfigureCors();

        builder.Services.AddHealthChecks();

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "IlisStore V1");
        });


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/healthcheck");
            endpoints.MapControllers();
        });

        app.Run();
    }
}

