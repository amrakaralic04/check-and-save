namespace Market.Application.Modules.Products.Category.Commands.Delete
{
    public sealed class DeleteCategoryCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
