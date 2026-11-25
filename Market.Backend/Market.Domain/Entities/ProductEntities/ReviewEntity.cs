using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.ProductEntities
{
    public class ReviewEntity : BaseEntity
    {
        public int PublicUserEntityId { get; set; }
        public PublicUserEntity? PublicUserEntity { get; set;}
        public int ProductEntityId { get; set; }
        public ProductEntity? ProductEntity { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime Date {  get; set; }

        public static class Constraints
        {
            public const int CommentMaxLength = 2000;
            public const int RatingMin = 1;
            public const int RatingMax = 5;
        }

    }
}
