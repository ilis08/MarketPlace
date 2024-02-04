using AutoMapper;
using MarketPlace.Application.Contracts.Infrastructure;
using MarketPlace.Application.Contracts.Infrastructure.Validations;
using MarketPlace.Application.Contracts.Persistence;
using MarketPlace.Domain.Entitites;
using MediatR;

namespace MarketPlace.Application.Features.AdminDashboard.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand,
CreateProductCommandResponse>
{
    private readonly IProductRepository productRepository;
    private readonly IImageService imageService;
    private readonly IImageValidation imageValidation;
    private readonly IMapper mapper;

    public CreateProductCommandHandler(
        IMapper mapper,
        IImageService imageService,
        IProductRepository productRepository,
        IImageValidation imageValidation)
    {
        this.mapper = mapper;
        this.imageService = imageService;
        this.imageValidation = imageValidation;
        this.productRepository = productRepository;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var createProductCommandResponse = new CreateProductCommandResponse();

        var validator = new CreateProductCommandValidator(imageValidation);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            createProductCommandResponse.Success = false;
            createProductCommandResponse.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                createProductCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }
        }
        if (createProductCommandResponse.Success)
        {
            var logoImageUrl = await imageService.UploadImage(request.LogoImage);

            var imageUrls = await imageService.UploadImages(request.Images);

            var product = mapper.Map<Product>(request);

            product.Image = new Image
            {
                Url = logoImageUrl,
            };

            foreach (var imageUrl in imageUrls)
            {
                product.Images.Add(new Image
                {
                    Url = imageUrl
                });
            }

            product = await productRepository.AddAsync(product);

            createProductCommandResponse.Product = mapper.Map<CreateProductDto>(product);
        }

        return createProductCommandResponse;
    }
}

