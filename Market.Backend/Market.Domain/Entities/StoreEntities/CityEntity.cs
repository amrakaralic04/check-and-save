using Market.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.StoreEntities
{
    public class CityEntity : BaseEntity
    {
        public string Name { get; set; }
        public int PostalCode { get; set; }
        public ICollection<StoreEntity> Stores { get; set; } = new List<StoreEntity>();
        public ICollection<BranchEntity> Branches { get; set; } = new List<BranchEntity>();

        public static class Constraints
        {
            public const int NameMaxLength = 100;
        }
    }
}