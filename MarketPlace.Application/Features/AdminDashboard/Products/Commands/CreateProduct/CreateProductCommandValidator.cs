using FluentValidation;
using MarketPlace.Application.Contracts.Infrastructure.Validations;

namespace MarketPlace.Application.Features.AdminDashboard.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IImageValidation imageValidation;
    public CreateProductCommandValidator(IImageValidation imageValidation)
    {
        this.imageValidation = imageValidation;

        RuleFor(c => c.ProductName)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull()
             .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

        RuleFor(c => c.Description)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull()
             .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.");

        RuleFor(c => c.Price)
             .NotNull().WithMessage("{PropertyName} is required.")
             .GreaterThan(0).WithMessage("{PropertyName} should be greater that 0.");

        RuleFor(c => c.Quantity)
             .NotNull().WithMessage("{PropertyName} is required.")
             .GreaterThan(0).WithMessage("{PropertyName} should be greater that 0.");

        RuleFor(c => c.LogoImage)
           .Must(imageValidation.IsValidImage).WithMessage("Invalid file extension.");
    }
}
