using Microsoft.AspNetCore.Http;

namespace MarketPlace.Application.Contracts.Infrastructure
{
    public interface IImageService
    {
        Task<string> UploadImage(IFormFile image);
        Task<IEnumerable<string>> UploadImages(IEnumerable<IFormFile> images);
        Task<bool> DeleteImage(Guid id);
    }
}
