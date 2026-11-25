using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.Modules.Products.Comparison.Queries.List
{
    public sealed class ListComparisonsQuery : BasePagedQuery<ListComparisonsQueryDto>
    {
        public int? CustomerEntityId { get; set; }
        public PageRequest Page { get; internal set; }
    }
}
