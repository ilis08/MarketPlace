using Microsoft.AspNetCore.Http;

namespace MarketPlace.Application.Contracts.Infrastructure.Validations
{
    public interface IImageValidation
    {
        bool IsValidImage(IFormFile file);
    }
}
