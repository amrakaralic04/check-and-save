using Market.Application.Modules.Products.Comparison.Commands.Create;
using Market.Application.Modules.Products.Comparison.Commands.Update;
using Market.Application.Modules.Products.Comparison.Commands.Delete;
using Market.Application.Modules.Products.Comparison.Queries.GetById;
using Market.Application.Modules.Products.Comparison.Queries.List;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ComparisonController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateComparisonCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdateComparisonCommand command, CancellationToken ct)
    {
        command.Id = id;
        await sender.Send(command, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteComparisonCommand { Id = id }, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<GetComparisonByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        return await sender.Send(new GetComparisonByIdQuery { Id = id }, ct);
    }

    [HttpGet]
    public async Task<PageResult<ListComparisonsQueryDto>> List([FromQuery] ListComparisonsQuery query, CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }
}
