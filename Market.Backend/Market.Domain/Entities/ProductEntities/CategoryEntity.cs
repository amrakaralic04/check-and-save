using Market.Domain.Common;


namespace Market.Domain.Entities.ProductEntities
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();

        public static class Constraints
        {
            public const int NameMaxLength = 150;

            public const int DescriptionMaxLength = 1000;
        }
    }
}