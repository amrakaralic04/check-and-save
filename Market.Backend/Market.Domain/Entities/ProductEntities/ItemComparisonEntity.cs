using Market.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.ProductEntities
{
    public class ItemComparisonEntity : BaseEntity
    {
        public int ComparisonEntityId { get; set; }
        public ComparisonEntity? ComparisonEntity { get; set; }
        public int ProductId { get; set; }
        public ProductEntity? ProductEntity { get; set; }

    }
}
