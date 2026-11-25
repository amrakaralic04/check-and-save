using Market.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Identity
{
    public class PublicUserEntity : BaseEntity
    {
        public int MarketUserEntityId { get; set; }
        public MarketUserEntity? MarketUserEntity { get; set; }
        public int Points { get; set; }
        public int AvatarLevel { get; set; }

        public static class Constraints
        {
            public const int MinPoints = 0;
            public const int MinAvatarLevel = 0;
            public const int MaxAvatarLevel = 100;
        }
    }
}