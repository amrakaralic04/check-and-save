namespace Market.Application.Modules.Products.ItemComparison.Commands.Update
{
    public sealed class UpdateItemComparisonCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int ProductId { get; set; }
    }
}
