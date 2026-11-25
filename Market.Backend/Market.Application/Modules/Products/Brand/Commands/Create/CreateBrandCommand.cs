namespace Market.Application.Modules.Products.Brand.Commands.Create
{
    public sealed class CreateBrandCommand : IRequest<int>

    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
