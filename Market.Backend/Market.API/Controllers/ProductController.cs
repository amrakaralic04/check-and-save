using Market.Application.Modules.Products.Product.Commands.Create;
using Market.Application.Modules.Products.Product.Commands.Update;
using Market.Application.Modules.Products.Product.Commands.Delete;
using Market.Application.Modules.Products.Product.Commands.UploadImage;
using Market.Application.Modules.Products.Product.Queries.GetById;
using Market.Application.Modules.Products.Product.Queries.List;
using Market.Application.Modules.Products.Product.Queries.Compare;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
[Route("api/products")]
public class ProductController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateProductCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdateProductCommand command, CancellationToken ct)
    {
        command.Id = id;
        await sender.Send(command, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteProductCommand { Id = id }, ct);
    }

    [HttpGet("compare")]
    [AllowAnonymous]
    public async Task<CompareProductsQueryDto> Compare([FromQuery] CompareProductsQuery query, CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<GetProductByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        return await sender.Send(new GetProductByIdQuery { Id = id }, ct);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<PageResult<ListProductsQueryDto>> List(
        [FromQuery] ListProductsQuery query,
        CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }

    /// <summary>
    /// Uploads, compresses and stores product image in Supabase Storage.
    /// POST /Product/{id}/images
    /// </summary>
    [HttpPost("{id:int}/images")]
    [Consumes("multipart/form-data")]
    public async Task<UploadProductImageCommandDto> UploadImage(
        int id,
        IFormFile image,
        CancellationToken ct)
    {
        await using var stream = image.OpenReadStream();

        var command = new UploadProductImageCommand
        {
            ProductId = id,
            ImageStream = stream,
            FileName = image.FileName,
            FileSize = image.Length
        };

        return await sender.Send(command, ct);
    }
}