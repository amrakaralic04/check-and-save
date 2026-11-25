using Market.Application.Modules.Notifications.Notification.Commands.Create;
using Market.Application.Modules.Notifications.Notification.Commands.Delete;
using Market.Application.Modules.Notifications.Notification.Commands.Update;
using Market.Application.Modules.Notifications.Notification.Queries.GetById;
using Market.Application.Modules.Notifications.Notification.Queries.List;

namespace Market.API.Controllers;
[ApiController]
[Route("[controller]")]
public class NotificationsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateNotificationCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdateNotificationCommand command, CancellationToken ct)
    {
        command.Id = id;
        await sender.Send(command, ct);
       
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteNotificationCommand { Id = id }, ct);
        
    }

    [HttpGet("{id:int}")]
    public async Task<GetNotificationByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        var notification = await sender.Send(new GetNotificationByIdQuery { Id = id }, ct);
        return notification; 
    }

    [HttpGet]
    public async Task<PageResult<ListNotificationsQueryDto>> List([FromQuery] ListNotificationsQuery query, CancellationToken ct)
    {
        var result = await sender.Send(query, ct);
        return result;
    }
}
