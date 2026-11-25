using Market.Application.Abstractions;
using Market.Application.Common.Exceptions;

namespace Market.Application.Modules.Products.Product.Commands.UploadImage;

public sealed class UploadProductImageCommandHandler(
    IAppDbContext ctx,
    IStorageService storage)
    : IRequestHandler<UploadProductImageCommand, UploadProductImageCommandDto>
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxFileSizeBytes = 10 * 1024 * 1024;

    public async Task<UploadProductImageCommandDto> Handle(
        UploadProductImageCommand request,
        CancellationToken ct)
    {
        var product = await ctx.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, ct);

        if (product is null)
            throw new MarketNotFoundException($"Product (ID={request.ProductId}) not found.");

        if (request.FileSize > MaxFileSizeBytes)
            throw new MarketBusinessRuleException("image.too_large", "Image size must not exceed 10MB.");

        var ext = Path.GetExtension(request.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(ext))
            throw new MarketBusinessRuleException("image.invalid_format", $"Allowed formats: {string.Join(", ", AllowedExtensions)}");

        var imageUrl = await storage.UploadImageAsync(request.ImageStream, request.FileName, ct);

        product.ImageURL = imageUrl;
        await ctx.SaveChangesAsync(ct);

        return new UploadProductImageCommandDto { ImageUrl = imageUrl };
    }
}