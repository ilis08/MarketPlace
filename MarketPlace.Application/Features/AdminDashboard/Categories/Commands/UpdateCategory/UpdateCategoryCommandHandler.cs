using AutoMapper;
using MarketPlace.Application.Contracts.Infrastructure.Validations;
using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Application.Exceptions;
using MarketPlace.Domain.Entitites;
using MediatR;

namespace MarketPlace.Application.Features.AdminDashboard.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IAsyncRepository<Category> categoryRepository;
    private readonly IImageValidation imageValidation;
    private readonly IMapper mapper;

    public UpdateCategoryCommandHandler(
        IMapper mapper,
        IImageValidation imageValidation,
        IAsyncRepository<Category> categoryRepository)
    {
        this.mapper = mapper;
        this.imageValidation = imageValidation;
        this.categoryRepository = categoryRepository;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryToUpdate = await categoryRepository.FindByIdAsync(request.Id);

        if (categoryToUpdate is null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        var validator = new UpdateCategoryCommandValidator(imageValidation);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            throw new ValidationException(validationResult);
        }

        mapper.Map(request, categoryToUpdate);

        await categoryRepository.UpdateAsync(categoryToUpdate);
    }
}
