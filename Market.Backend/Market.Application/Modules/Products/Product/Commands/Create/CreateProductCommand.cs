namespace Market.Application.Modules.Products.Product.Commands.Create
{
    public sealed class CreateProductCommand : IRequest<int>
    {
        public int StoreEntityId { get; set; }
        public int BranchEntityId { get; set; }
        public int CategoryEntityId { get; set; }
        public int BrandEntityId { get; set; }

        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ImageURL { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
