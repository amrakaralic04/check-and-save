namespace Market.Application.Modules.Products.Brand.Queries.GetById
{
    public sealed class GetBrandByIdQuery : IRequest<GetBrandByIdQueryDto>
    {
        public int Id { get; set; }
    }
}
