using Market.Application.Modules.Notifications.NotificationType.Commands.Create;
using Market.Application.Modules.Notifications.NotificationType.Commands.Delete;
using Market.Application.Modules.Notifications.NotificationType.Commands.Update;
using Market.Application.Modules.Notifications.NotificationType.Queries.GetById;
using Market.Application.Modules.Notifications.NotificationType.Queries.List;


namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationTypesController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateNotifTypeCmd command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdateNotifTypeCmd command, CancellationToken ct)
    {
       
        command.Id = id;
        await sender.Send(command, ct);
        
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteNotifTypeCmd { Id = id }, ct);
     
    }

    [HttpGet("{id:int}")]
    public async Task<GetNotifTypeByIdQryDto> GetById(int id, CancellationToken ct)
    {
        var type = await sender.Send(new GetNotifTypeByIdQry { Id = id }, ct);
        return type; 
    }

    [HttpGet]
    public async Task<PageResult<ListNotifTypesQryDto>> List([FromQuery] ListNotifTypesQry query, CancellationToken ct)
    {
        var result = await sender.Send(query, ct);
        return result;
    }
}