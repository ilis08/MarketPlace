using FluentValidation;
using MarketPlace.Application.Contracts.Infrastructure.Validations;

namespace MarketPlace.Application.Features.AdminDashboard.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    private readonly IImageValidation imageValidation;
    public UpdateCategoryCommandValidator(IImageValidation imageValidation)
    {
        this.imageValidation = imageValidation;

        RuleFor(c => c.Title)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull()
             .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(c => c.Description)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull()
             .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.");

        RuleFor(c => c.Image).Must(image =>
        {
            if (image is not null)
            {
                return imageValidation.IsValidImage(image);
            }

            return true;
        }).WithMessage("Invalid file extension.");
    }
}
