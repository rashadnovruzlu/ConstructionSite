using ConstructionSite.Helpers.Page;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Pageinations
{
   public static class PageExtensions
    {
      
        public static  PagedResult<T> Pagination<T>(this IQueryable<T> query,
            int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount =  query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Results= query.Skip(skip).Take(pageSize).ToList();
            return result;
           
        }
    }
}
