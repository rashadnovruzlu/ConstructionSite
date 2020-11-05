using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionSite.Extensions.SelectListItems
{
    public static class SelectListItemExtension
    {
        public static IQueryable<SelectListItem> BaseSelectList<T>(this IQueryable<T> list, string idPropertyName, string namePropertyName = "Name")
           where T : class, new()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (var item in list)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = item.GetType().GetProperty(namePropertyName).GetValue(item).ToString(),
                    Value = item.GetType().GetProperty(idPropertyName).GetValue(item).ToString()
                });
            }

            return selectListItems.AsQueryable();
        }
    }
}