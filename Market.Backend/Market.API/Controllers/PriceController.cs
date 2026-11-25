using Market.Application.Modules.Products.Price.Commands.Create;
using Market.Application.Modules.Products.Price.Commands.Update;
using Market.Application.Modules.Products.Price.Commands.Delete;
using Market.Application.Modules.Products.Price.Queries.GetById;
using Market.Application.Modules.Products.Price.Queries.List;


namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PriceController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreatePriceCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdatePriceCommand command, CancellationToken ct)
    {
        
        command.Id = id;
        await sender.Send(command, ct);
        
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeletePriceCommand { Id = id }, ct);
       
    }

    [HttpGet("{id:int}")]
    public async Task<GetPriceByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        return await sender.Send(new GetPriceByIdQuery { Id = id }, ct);
    }

    [HttpGet]
    public async Task<PageResult<ListPricesQueryDto>> List(
        [FromQuery] ListPricesQuery query,
        CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }
}
