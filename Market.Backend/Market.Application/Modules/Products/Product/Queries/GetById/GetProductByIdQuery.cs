namespace Market.Application.Modules.Products.Product.Queries.GetById
{
    public sealed class GetProductByIdQuery : IRequest<GetProductByIdQueryDto>
    {
        public int Id { get; set; }
    }
}
