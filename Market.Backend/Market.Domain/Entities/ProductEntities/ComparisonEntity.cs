using Market.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.ProductEntities
{
    public class ComparisonEntity : BaseEntity
    {
        public int CustomerEntityId { get; set; }
        public DateTime Date {  get; set; }

        public ICollection<ItemComparisonEntity> Items { get; set; } = new List<ItemComparisonEntity>();
    }
}
