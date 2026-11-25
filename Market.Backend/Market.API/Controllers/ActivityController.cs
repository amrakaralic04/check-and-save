using Market.Application.Modules.Identity.Activity.Commands.Create;
using Market.Application.Modules.Identity.Activity.Commands.Delete;
using Market.Application.Modules.Identity.Activity.Commands.Update;
using Market.Application.Modules.Identity.Activity.Queries.GetById;
using Market.Application.Modules.Identity.Activity.Queries.List;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivityController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> CreateActivity(
        [FromBody] CreateActivityCommand command,
        CancellationToken ct)
    {
        int id = await sender.Send(command, ct);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(
        int id,
        [FromBody] UpdateActivityCommand command,
        CancellationToken ct)
    {
        command.Id = id;

        await sender.Send(command, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteActivityCommand { Id = id }, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<GetActivityByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        var activity = await sender.Send(new GetActivityByIdQuery { Id = id }, ct);
        return activity;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<PageResult<ListActivitiesQueryDto>> List(
        [FromQuery] ListActivitiesQuery query,
        CancellationToken ct)
    {
        var result = await sender.Send(query, ct);
        return result;
    }
}