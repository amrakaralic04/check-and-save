using System.Text.Json.Serialization;

namespace Market.Application.Modules.Products.Product.Commands.UploadImage;

public sealed class UploadProductImageCommand : IRequest<UploadProductImageCommandDto>
{
    [JsonIgnore]
    public int ProductId { get; set; }

    public Stream ImageStream { get; set; } = null!;
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
}

public sealed class UploadProductImageCommandDto
{
    public string ImageUrl { get; init; } = string.Empty;
}