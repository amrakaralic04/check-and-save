namespace Market.Application.Modules.Products.Category.Commands.Update
{
    public sealed class UpdateCategoryCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
