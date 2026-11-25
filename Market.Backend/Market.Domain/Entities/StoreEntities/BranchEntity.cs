using Market.Domain.Common;
using Market.Domain.Entities.ProductEntities;

namespace Market.Domain.Entities.StoreEntities;

public class BranchEntity : BaseEntity
{
    public int StoreEntityId { get; set; }
    public StoreEntity? StoreEntity { get; set; }

    public int CityEntityId { get; set; }
    public CityEntity? CityEntity { get; set; }

    public string Address { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();

    public static class Constraints
    {
        public const int AddressMaxLength = 200;
        public const int ContactMaxLength = 100;
        public const int EmailMaxLength = 200;
    }
}