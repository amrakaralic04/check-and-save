using Market.Domain.Entities.ProductEntities;

namespace Market.Application.Modules.Products.Comparison.Commands.Create
{
    public sealed class CreateComparisonCommandHandler(IAppDbContext ctx) : IRequestHandler<CreateComparisonCommand, int>
    {
        public async Task<int> Handle(CreateComparisonCommand request, CancellationToken ct)
        {
            var entity = new ComparisonEntity
            {
                CustomerEntityId = request.CustomerEntityId,
                Date = request.Date
            };

            await ctx.Comparisons.AddAsync(entity, ct);
            await ctx.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}
