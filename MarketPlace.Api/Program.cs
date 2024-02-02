using MarketPlace.Api;
using Serilog;

namespace WebAPI;
public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

        Log.Information("MarketPlace API starting.");

        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
            .WriteTo.Console()
            .ReadFrom.Configuration(context.Configuration));

        var app = builder
            .ConfigureServices()
            .ConfigurePipeline();

        app.UseSerilogRequestLogging();

        await app.ResetDatabaseAsync();

        app.Run();
    }
}

