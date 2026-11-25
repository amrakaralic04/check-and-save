using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.ProductEntities
{
    public class FavoritesEntity : BaseEntity
    {
        public int PublicUserEntityId { get; set; }
        public PublicUserEntity? PublicUserEntity { get; set;}
        public int ProductEntityId { get; set; }
        public ProductEntity? ProductEntity { get; set; }   
        public DateTime DateAdded { get; set; } 

    }
}
