using AutoMapper;
using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Application.Exceptions;
using MarketPlace.Domain.Entitites;
using MediatR;

namespace MarketPlace.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler
    {
        private readonly IAsyncRepository<Category> categoryRepository;
        private readonly IMapper mapper;

        public UpdateCategoryCommandHandler(IMapper _mapper, IAsyncRepository<Category> _categoryRepository)
        {
            mapper = _mapper;
            categoryRepository = _categoryRepository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToUpdate = await categoryRepository.FindByIdAsync(request.Id);

            if (categoryToUpdate is null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            var validator = new UpdateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            mapper.Map(request, categoryToUpdate);

            await categoryRepository.UpdateAsync(categoryToUpdate);

            return Unit.Value;
        }
    }
}
