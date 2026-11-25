namespace Market.Application.Modules.Products.Price.Commands.Delete
{
    public sealed class DeletePriceCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
