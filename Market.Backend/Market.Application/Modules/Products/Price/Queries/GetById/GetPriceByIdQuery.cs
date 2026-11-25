namespace Market.Application.Modules.Products.Price.Queries.GetById
{
    public sealed class GetPriceByIdQuery : IRequest<GetPriceByIdQueryDto>
    {
        public int Id { get; set; }
    }
}
