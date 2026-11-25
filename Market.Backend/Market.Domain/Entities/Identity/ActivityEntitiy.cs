using Market.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Identity
{
    public class ActivityEntity : BaseEntity
    {
        public int MarketUserEntityId { get; set; }
        public MarketUserEntity?MarketUserEntity { get; set; }
        public string ActivityType { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date {  get; set; }

        public static class Constraints
        {
            public const int ActivityTypeMaxLength = 100;
            public const int DescriptionMaxLength = 1000;
        }
    }
}
