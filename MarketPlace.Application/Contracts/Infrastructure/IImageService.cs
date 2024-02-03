using Microsoft.AspNetCore.Http;

namespace MarketPlace.Application.Contracts.Infrastructure
{
    public interface IImageService
    {
        Task<string> UploadImage(IFormFile image);
        Task<bool> DeleteImage(Guid id);
    }
}
