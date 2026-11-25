using Market.Application.Modules.Identity.RefreshToken.Commands.Create;
using Market.Application.Modules.Identity.RefreshToken.Commands.Delete;
using Market.Application.Modules.Identity.RefreshToken.Commands.Revoke;
using Market.Application.Modules.Identity.RefreshToken.Queries.GetById;
using Market.Application.Modules.Identity.RefreshToken.Queries.List;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RefreshTokensController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(
        [FromBody] CreateRefreshTokenCommand command,
        CancellationToken ct)
    {
        int id = await sender.Send(command, ct);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteRefreshTokenCommand { Id = id }, ct);
    }

    [HttpPut("{id:int}/revoke")]
    public async Task Revoke(int id, CancellationToken ct)
    {
        await sender.Send(new RevokeRefreshTokenCommand { Id = id }, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<GetRefreshTokenByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        return await sender.Send(new GetRefreshTokenByIdQuery { Id = id }, ct);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<PageResult<ListRefreshTokensQueryDto>> List(
        [FromQuery] ListRefreshTokensQuery query,
        CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }
}