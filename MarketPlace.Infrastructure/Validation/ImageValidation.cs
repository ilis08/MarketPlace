using MarketPlace.Application.Contracts.Infrastructure.Validations;
using Microsoft.AspNetCore.Http;

namespace MarketPlace.Infrastructure.Validation
{
    public class ImageValidation : IImageValidation
    {
        public bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            return allowedExtensions.Contains(fileExtension);
        }
    }
}
