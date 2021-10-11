
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using System.IO;
using ApplicationService.Implementations;
using Repository.Implementations;
using WebAPI.Messages;
using Data.Context;
using WebAPI.Extensions;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Swagger

            services.ConfigureSwagger();

            #endregion

            #region Database

            services.ConfigureDatabaseContext(Configuration);

            #endregion

            services.AddTransient<UnitOfWork>();

            #region ApplicationService
            services.ConfigureCategoryService();
            services.ConfigureProductService();
            services.ConfigureOrderService();
            #endregion

            #region Repository

            services.ConfigureUnitOfWork();

            #endregion

            services.AddHealthChecks()
                .AddSqlServer(Configuration.GetConnectionString("IlisStoreDB"),
                  name: "ilisDb-check",
                  failureStatus: HealthStatus.Unhealthy,
                  tags: new string[] { "api", "SqlDb" });



            services.AddControllers().AddNewtonsoftJson(x =>
                  x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IlisStore V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
