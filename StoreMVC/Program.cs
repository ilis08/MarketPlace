using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoreMVC.Models.Order.SessionCart;
using StoreMVC.Service;
using System;
using System.Configuration;
using System.Net.Http.Headers;

namespace WebAPI;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(opts =>
        {
            opts.IdleTimeout = TimeSpan.FromDays(120);
            opts.Cookie.IsEssential = true;
        });

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddTransient(c => CartSession.GetCart(c));
        builder.Services.AddScoped<IProductService, ProductService>();

        builder.Services.AddHttpClient("myapi", c =>
        {
            c.BaseAddress = new Uri(builder.Configuration.GetSection("apiPath").Value);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseSession();


        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            endpoints.MapDefaultControllerRoute();

            endpoints.MapRazorPages();
        });

        app.Run();
    }
}
