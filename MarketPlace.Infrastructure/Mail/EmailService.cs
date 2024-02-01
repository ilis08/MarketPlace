using MarketPlace.Application.Contracts.Infrastructure;
using MarketPlace.Application.Models.Mail;

namespace MarketPlace.Infrastructure.Mail;

public class EmailService : IEmailService
{
    public Task<bool> SendEmail(Email email)
    {
        throw new NotImplementedException();
    }
}
