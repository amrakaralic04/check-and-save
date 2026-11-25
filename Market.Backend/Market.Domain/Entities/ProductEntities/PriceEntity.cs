using Market.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.ProductEntities
{
    public class PriceEntity : BaseEntity
    {
        public int ProductEntityId { get; set; }
        public ProductEntity?ProductEntity { get; set; }
        public int Amount { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
