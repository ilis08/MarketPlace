using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ApplicationService.Attributes;

public class AllowedFileFormatsForImageAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedFileFormatsForImageAttribute(string[] extensions) => _extensions = extensions;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;

        if (file is not null)
        {
            var extension = Path.GetExtension(file.FileName);

            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(GetErrorMessage());
            }
        }

        return ValidationResult.Success;
    }

    public static string GetErrorMessage()
    {
        return $"This file format is not allowed";
    }
}
