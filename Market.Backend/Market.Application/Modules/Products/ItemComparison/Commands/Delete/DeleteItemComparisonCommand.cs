namespace Market.Application.Modules.Products.ItemComparison.Commands.Delete
{
    public sealed class DeleteItemComparisonCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
