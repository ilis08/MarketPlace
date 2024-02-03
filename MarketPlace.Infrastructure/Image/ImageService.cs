using MarketPlace.Application.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace MarketPlace.Infrastructure.Image
{
    public class ImageService : IImageService
    {
        public Task<bool> DeleteImage(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadImage(IFormFile image)
        {
            throw new NotImplementedException();
        }
    }
}
