using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.StoreEntities
{
    public class StoreEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int CityEntityId { get; set; }
        public CityEntity? CityEntity { get; set; }
        public ICollection<BranchEntity> Branches { get; set; } = new List<BranchEntity>();
        public ICollection<ManagerEntity> Managers { get; set; } = new List<ManagerEntity>();
        public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();

        public static class Constraints
        {
            public const int NameMaxLength = 100;
            public const int ContactMaxLength = 100;
            public const int EmailMaxLength = 200;
        }
    }
}