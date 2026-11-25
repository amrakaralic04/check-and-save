using MediatR;

namespace Market.Application.Modules.Stores.Branches.Queries.GetById;

public class GetBranchByIdQuery : IRequest<GetBranchByIdQueryDto>
{
    public int Id { get; set; }
}