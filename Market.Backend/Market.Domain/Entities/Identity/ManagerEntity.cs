using Market.Domain.Common;
using Market.Domain.Entities.Analytics;
using Market.Domain.Entities.StoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Identity
{
    public class ManagerEntity : BaseEntity
    {
        public int MarketUserEntityId { get; set; }
        public MarketUserEntity? MarketUserEntity { get; set; }

        public int StoreEntityId { get; set; }
        public StoreEntity? StoreEntity { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<SalesStatisticEntity> SalesStatistics { get; set; } = new List<SalesStatisticEntity>();
    }
}