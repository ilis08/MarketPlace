using AutoMapper;
using MarketPlace.Application.Contracts.Infrastructure;
using MarketPlace.Application.Contracts.Infrastructure.Validations;
using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Application.Exceptions;
using MarketPlace.Domain.Entitites;
using MediatR;

namespace MarketPlace.Application.Features.AdminDashboard.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository productRepository;
    private readonly IImageService imageService;
    private readonly IImageValidation imageValidation;
    private readonly IMapper mapper;

    public UpdateProductCommandHandler(
        IMapper mapper,
        IImageService imageService,
        IImageValidation imageValidation,
        IProductRepository productRepository)
    {
        this.mapper = mapper;
        this.imageService = imageService;
        this.imageValidation = imageValidation;
        this.productRepository = productRepository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productToUpdate = await productRepository.FindByIdAsync(request.Id);

        if (productToUpdate is null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        var validator = new UpdateProductCommandValidator(imageValidation);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            throw new ValidationException(validationResult);
        }

        mapper.Map(request, productToUpdate);

        var logoImageUrl = await imageService.UploadImage(request.LogoImage);

        productToUpdate.Image.Url = logoImageUrl;

        await productRepository.UpdateAsync(productToUpdate);
    }
}
