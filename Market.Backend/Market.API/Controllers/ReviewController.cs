using Market.Application.Modules.Products.Review.Commands.Create;
using Market.Application.Modules.Products.Review.Commands.Update;
using Market.Application.Modules.Products.Review.Commands.Delete;
using Market.Application.Modules.Products.Review.Queries.GetById;
using Market.Application.Modules.Products.Review.Queries.List;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateReviewCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdateReviewCommand command, CancellationToken ct)
    {
        command.Id = id;
        await sender.Send(command, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteReviewCommand { Id = id }, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<GetReviewByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        return await sender.Send(new GetReviewByIdQuery { Id = id }, ct);
    }

    [HttpGet]
    public async Task<PageResult<ListReviewsQueryDto>> List([FromQuery] ListReviewsQuery query, CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }
}
