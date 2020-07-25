using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Helpers.Paging
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemPrePage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage()
        {
            return (int) Math.Ceiling((decimal)TotalItems/ItemPrePage);
        }
    }
    public class PaginModel<T>
    {
        public IEnumerable<T>  Paging { get; set; }
        public PagingInfo  PagingInfo { get; set; }
    }
}
