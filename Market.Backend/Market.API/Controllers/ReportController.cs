using Market.Application.Modules.Analiytics.Report.Commands.Create;
using Market.Application.Modules.Analiytics.Report.Commands.Delete;
using Market.Application.Modules.Analiytics.Report.Commands.Update;
using Market.Application.Modules.Analiytics.Report.Queries.GetById;
using Market.Application.Modules.Analiytics.Report.Queries.List;

namespace Market.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ReportController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateReportCommand command, CancellationToken ct)
        {
            int id = await sender.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id:int}")]
        public async Task Update(int id, UpdateReportCommand command, CancellationToken ct)
        {
            command.Id = id;
            await sender.Send(command, ct);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await sender.Send(new DeleteReportCommand { Id = id }, ct);
        }

        [HttpGet("{id:int}")]
        public async Task<GetReportByIdQueryDto> GetById(int id, CancellationToken ct)
        {
            return await sender.Send(new GetReportByIdQuery { Id = id }, ct);
        }

        [HttpGet]
        public async Task<PageResult<ListReportsQueryDto>> List([FromQuery] ListReportsQuery query, CancellationToken ct)
        {
            return await sender.Send(query, ct);
        }
    }
}