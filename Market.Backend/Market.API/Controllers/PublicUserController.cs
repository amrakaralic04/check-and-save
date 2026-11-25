using Market.Application.Modules.Identity.PublicUser.Commands.Create;
using Market.Application.Modules.Identity.PublicUser.Commands.Delete;
using Market.Application.Modules.Identity.PublicUser.Commands.Update;
using Market.Application.Modules.Identity.PublicUser.Queries.GetById;
using Market.Application.Modules.Identity.PublicUser.Queries.List;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PublicUserController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(
        [FromBody] CreatePublicUserCommand command,
        CancellationToken ct)
    {
        int id = await sender.Send(command, ct);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(
        int id,
        [FromBody] UpdatePublicUserCommand command,
        CancellationToken ct)
    {
        command.Id = id;

        await sender.Send(command, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeletePublicUserCommand { Id = id }, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<GetPublicUserByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        return await sender.Send(new GetPublicUserByIdQuery { Id = id }, ct);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<PageResult<ListPublicUsersQueryDto>> List(
        [FromQuery] ListPublicUsersQuery query,
        CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }
}