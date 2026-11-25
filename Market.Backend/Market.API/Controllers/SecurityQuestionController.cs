using Market.Application.Modules.Identity.SecurityQuestion.Command.Create;
using Market.Application.Modules.Identity.SecurityQuestion.Command.Delete;
using Market.Application.Modules.Identity.SecurityQuestion.Command.Update;
using Market.Application.Modules.Identity.SecurityQuestion.Queries.GetById;
using Market.Application.Modules.Identity.SecurityQuestion.Queries.List;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SecurityQuestionsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(
        [FromBody] CreateSecQCommand command,
        CancellationToken ct)
    {
        int id = await sender.Send(command, ct);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(
        int id,
        [FromBody] UpdateSecQCommand command,
        CancellationToken ct)
    {
        command.Id = id;
        await sender.Send(command, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteSecQCommand { Id = id }, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<GetSecQByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        return await sender.Send(new GetSecQByIdQuery { Id = id }, ct);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<PageResult<ListSecQQueryDto>> List(
        [FromQuery] ListSecQQuery query,
        CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }
}