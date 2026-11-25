namespace Market.Application.Modules.Products.Product.Commands.UploadImage;

public sealed class UploadProductImageCommandValidator : AbstractValidator<UploadProductImageCommand>
{
    public UploadProductImageCommandValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.ImageStream).NotNull().WithMessage("Image stream is required.");
        RuleFor(x => x.FileName).NotEmpty().WithMessage("File name is required.");
        RuleFor(x => x.FileSize).GreaterThan(0).WithMessage("File size must be greater than 0.");
    }
}