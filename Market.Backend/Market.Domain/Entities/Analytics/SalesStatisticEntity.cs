using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Entities.ProductEntities;

namespace Market.Domain.Entities.Analytics
{
    public class SalesStatisticEntity : BaseEntity
    {
        public int ManagerEntityId { get; set; }
        public int ProductEntityId { get; set; }

        public int ViewsCount { get; set; }
        public int SalesCount { get; set; }
        public DateTime Date { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }

        public ManagerEntity ManagerEntity { get; set; } = default!;
        public ProductEntity ProductEntity { get; set; } = default!;
    }
}