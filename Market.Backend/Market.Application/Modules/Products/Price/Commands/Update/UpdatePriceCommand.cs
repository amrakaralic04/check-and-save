namespace Market.Application.Modules.Products.Price.Commands.Update
{
    public sealed class UpdatePriceCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int Amount { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
