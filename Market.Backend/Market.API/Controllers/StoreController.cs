using Market.Application.Modules.Store.Stores.Commands.Delete;
using Market.Application.Modules.Store.Stores.Queries.GetById;
using Market.Application.Modules.Store.Stores.Queries.List;
using Market.Application.Modules.Store.Stores.Queries.Map;
using Market.Application.Modules.Stores.Store.Commands.Create;
using Market.Application.Modules.Stores.Store.Commands.Update;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StoreController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateStoreCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdateStoreCommand command, CancellationToken ct)
    {
        command.Id = id;
        await sender.Send(command, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteStoreCommand { Id = id }, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<GetStoreByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        return await sender.Send(new GetStoreByIdQuery { Id = id }, ct);
    }

    [HttpGet]
    public async Task<PageResult<ListStoresQueryDto>> List([FromQuery] ListStoresQuery query, CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }

    /// <summary>
    /// Returns all active stores with coordinates for map rendering.
    /// Requires authenticated user (admin, manager or public user).
    /// GET /Store/map
    /// </summary>
    [HttpGet("map")]
    [Authorize(Policy = "AuthenticatedUser")]
    public async Task<List<StoreMapItemDto>> GetMap(CancellationToken ct)
    {
        return await sender.Send(new GetStoresMapQuery(), ct);
    }
}