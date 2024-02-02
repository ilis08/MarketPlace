using FluentValidation;

namespace MarketPlace.Application.Features.Website.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.Title)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull()
             .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
    }
}
