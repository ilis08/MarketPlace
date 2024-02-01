using FluentValidation.Results;

namespace MarketPlace.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(ValidationResult validationResult)
    {
        ValidationErrors = new List<string>();

        foreach (var error in validationResult.Errors)
        {
            ValidationErrors.Add(error.ErrorMessage);
        }

    }

    public List<string> ValidationErrors { get; set; }

}
