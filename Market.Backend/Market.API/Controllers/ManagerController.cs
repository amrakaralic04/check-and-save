using Market.Application.Modules.Identity.Manager.Commands.Create;
using Market.Application.Modules.Identity.Manager.Commands.Delete;
using Market.Application.Modules.Identity.Manager.Commands.Update;
using Market.Application.Modules.Identity.Manager.Queries.GetById;
using Market.Application.Modules.Identity.Manager.Queries.List;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ManagerController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(
        [FromBody] CreateManagerCommand command,
        CancellationToken ct)
    {
        int id = await sender.Send(command, ct);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(
        int id,
        [FromBody] UpdateManagerCommand command,
        CancellationToken ct)
    {
        command.Id = id;

        await sender.Send(command, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteManagerCommand { Id = id }, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<GetManagerByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        return await sender.Send(new GetManagerByIdQuery { Id = id }, ct);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<PageResult<ListManagersQueryDto>> List(
        [FromQuery] ListManagersQuery query,
        CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }
}