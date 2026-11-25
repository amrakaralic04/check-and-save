using Market.Application.Modules.Analiytics.SaleStatistic.Commands.Create;
using Market.Application.Modules.Analiytics.SaleStatistic.Commands.Delete;
using Market.Application.Modules.Analiytics.SaleStatistic.Commands.Update;
using Market.Application.Modules.Analiytics.SaleStatistic.Queries.GetById;
using Market.Application.Modules.Analiytics.SaleStatistic.Queries.List;

namespace Market.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class SalesStatisticController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSalesStatisticCommand command, CancellationToken ct)
        {
            int id = await sender.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id:int}")]
        public async Task Update(int id, UpdateSalesStatisticCommand command, CancellationToken ct)
        {
            command.Id = id;
            await sender.Send(command, ct);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await sender.Send(new DeleteSalesStatisticCommand { Id = id }, ct);
        }

        [HttpGet("{id:int}")]
        public async Task<GetSalesStatisticByIdQueryDto> GetById(int id, CancellationToken ct)
        {
            return await sender.Send(new GetSalesStatisticByIdQuery { Id = id }, ct);
        }

        [HttpGet]
        public async Task<PageResult<ListSalesStatisticsQueryDto>> List([FromQuery] ListSalesStatisticsQuery query, CancellationToken ct)
        {
            return await sender.Send(query, ct);
        }
    }
}