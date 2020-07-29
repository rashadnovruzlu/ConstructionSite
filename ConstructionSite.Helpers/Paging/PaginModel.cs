using System.Collections.Generic;

namespace ConstructionSite.Helpers.Paging
{
    public class PaginModel<T>
    {
        public IEnumerable<T>  Paging { get; set; }
        public PagingInfo  PagingInfo { get; set; }
    }
}
