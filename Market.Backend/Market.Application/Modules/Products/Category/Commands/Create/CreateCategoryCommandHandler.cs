namespace Market.Application.Modules.Products.Category.Commands.Create
{
    public class CreateCategoryCommandHandler(IAppDbContext context) : IRequestHandler<CreateCategoryCommand, int>
    {
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Market.Domain.Entities.ProductEntities.CategoryEntity
            {
                Name = request.Name,
                Description = request.Description,
            };
            await context.Categories.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return category.Id;
        }
    }
}