using MarketPlace.Application.Contracts.Infrastructure;
using MarketPlace.Application.Contracts.Infrastructure.Validations;
using MarketPlace.Infrastructure.Image;
using MarketPlace.Infrastructure.Mail;
using MarketPlace.Infrastructure.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketPlace.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IImageService, ImageService>();
        services.AddTransient<IImageValidation, ImageValidation>();

        return services;
    }
}
