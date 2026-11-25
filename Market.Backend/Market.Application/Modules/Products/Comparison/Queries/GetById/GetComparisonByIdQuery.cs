namespace Market.Application.Modules.Products.Comparison.Queries.GetById
{
    public sealed class GetComparisonByIdQuery : IRequest<GetComparisonByIdQueryDto>
    {
            public int Id { get; set; }
    }
}
