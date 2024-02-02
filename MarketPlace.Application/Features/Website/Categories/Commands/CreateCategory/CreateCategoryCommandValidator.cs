using FluentValidation;

namespace MarketPlace.Application.Features.Website.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Title)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull()
             .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
    }
}
