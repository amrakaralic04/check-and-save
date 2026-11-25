using Market.Application.Modules.Products.Category.Commands.Create;
using Market.Application.Modules.Products.Category.Commands.Update;
using Market.Application.Modules.Products.Category.Commands.Delete;
using Market.Application.Modules.Products.Category.Queries.GetById;
using Market.Application.Modules.Products.Category.Queries.List;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCategoryCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdateCategoryCommand command, CancellationToken ct)
    {
        command.Id = id;
        await sender.Send(command, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteCategoryCommand { Id = id }, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<GetCategoryByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        return await sender.Send(new GetCategoryByIdQuery { Id = id }, ct);
    }

    [HttpGet]
    public async Task<PageResult<ListCategoriesQueryDto>> List([FromQuery] ListCategoriesQuery query, CancellationToken ct)
    {
        return await sender.Send(query, ct);
    }
}
