namespace Market.Application.Modules.Products.Brand.Commands.Delete
{
    public sealed class DeleteBrandCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
