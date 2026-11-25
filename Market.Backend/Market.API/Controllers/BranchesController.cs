using Market.Application.Modules.Stores.Branches.Commands.Create;
using Market.Application.Modules.Stores.Branches.Commands.Delete;
using Market.Application.Modules.Stores.Branches.Commands.Update;
using Market.Application.Modules.Stores.Branches.Queries.GetById;
using Market.Application.Modules.Stores.Branches.Queries.List;
using Market.Application.Modules.Stores.Branches.Queries.Map;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BranchesController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateBranchCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdateBranchCommand command, CancellationToken ct)
    {
        command.Id = id;
        await sender.Send(command, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteBranchCommand { Id = id }, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<GetBranchByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        return await sender.Send(new GetBranchByIdQuery { Id = id }, ct);
    }

    [HttpGet]
    public async Task<PageResult<ListBranchesQueryDto>> List([FromQuery] ListBranchesQuery query, CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }

    /// <summary>
    /// Returns all active branches with coordinates for map rendering.
    /// GET /Branches/map
    /// </summary>
    [HttpGet("map")]
    public async Task<List<BranchMapItemDto>> GetMap(CancellationToken ct)
    {
        return await sender.Send(new GetBranchesMapQuery(), ct);
    }
}