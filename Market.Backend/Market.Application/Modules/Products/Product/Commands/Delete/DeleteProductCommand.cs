namespace Market.Application.Modules.Products.Product.Commands.Delete
{
    public sealed class DeleteProductCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
