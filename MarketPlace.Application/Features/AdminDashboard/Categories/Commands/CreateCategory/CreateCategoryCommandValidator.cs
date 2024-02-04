using FluentValidation;
using MarketPlace.Application.Contracts.Infrastructure.Validations;

namespace MarketPlace.Application.Features.AdminDashboard.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly IImageValidation imageValidation;
    public CreateCategoryCommandValidator(IImageValidation _imageValidation)
    {
        imageValidation = _imageValidation;

        RuleFor(c => c.Title)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull()
             .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(c => c.Description)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull()
             .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.");

        RuleFor(c => c.Image)
            .Must(imageValidation.IsValidImage).WithMessage("Invalid file extension.");
    }
}
