namespace Market.Application.Modules.Products.Price.Commands.Create
{
    public sealed class CreatePriceCommand : IRequest<int>
    {
        public int ProductEntityId { get; set; }
        public int Amount { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
