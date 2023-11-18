using MarketPlace.Application.Models.Mail;

namespace MarketPlace.Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}
