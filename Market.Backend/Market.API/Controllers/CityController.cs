
using Market.Application.Modules.Stores.City.Commands.Delete;
using Market.Application.Modules.Stores.City.Commands.Update;
using Market.Application.Modules.Stores.City.Queries.GetById;
using Market.Application.Modules.Stores.City.Queries.List;
using Market.Application.Modules.Stores.City.Commands.Create;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CityController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCityCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdateCityCommand command, CancellationToken ct)
    {
        command.Id = id;
        await sender.Send(command, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteCityCommand { Id = id }, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<GetCityByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        return await sender.Send(new GetCityByIdQuery { Id = id }, ct);
    }

    [HttpGet]
    public async Task<PageResult<ListCitiesQueryDto>> List([FromQuery] ListCitiesQuery query, CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }
}
