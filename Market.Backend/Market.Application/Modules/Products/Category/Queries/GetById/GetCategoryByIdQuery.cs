namespace Market.Application.Modules.Products.Category.Queries.GetById
{
    public sealed class GetCategoryByIdQuery : IRequest<GetCategoryByIdQueryDto>
    {
        public int Id { get; set; }
    }
}
