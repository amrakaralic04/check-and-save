namespace Market.Application.Modules.Products.ItemComparison.Queries.GetById
{
    public sealed class GetItemComparisonByIdQuery : IRequest<GetItemComparisonByIdQueryDto>
    {
        public int Id { get; set; }
    }
}
