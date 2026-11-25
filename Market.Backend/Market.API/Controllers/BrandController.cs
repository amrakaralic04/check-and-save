using Market.Application.Modules.Products.Brand.Commands.Create;
using Market.Application.Modules.Products.Brand.Commands.Update;
using Market.Application.Modules.Products.Brand.Commands.Delete;
using Market.Application.Modules.Products.Brand.Queries.GetById;
using Market.Application.Modules.Products.Brand.Queries.List;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateBrandCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdateBrandCommand command, CancellationToken ct)
    {
        command.Id = id;
        await sender.Send(command, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteBrandCommand { Id = id }, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<GetBrandByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        return await sender.Send(new GetBrandByIdQuery { Id = id }, ct);
    }

    [HttpGet]
    public async Task<PageResult<ListBrandsQueryDto>> List([FromQuery] ListBrandsQuery query, CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }
}
