using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConstructionSite.Extensions.Mapping
{
   public static  class Map
    {
        public static T Mapped<T>(this object query)
        {
            Type hedefTip=typeof(T);
            Type kaynakTip=query.GetType();
            T soruces=Activator.CreateInstance<T>();
            PropertyInfo [] propertyInfo= hedefTip.GetProperties();
            foreach (var item in kaynakTip.GetProperties())
            {
             var target=   hedefTip.GetProperties()
                    .FirstOrDefault(x=>x.Name.ToUpper()==item.Name.ToUpper());
                if (target!=null)
                {
                    object data=item.GetValue(query);
                    target.SetValue(soruces,data);
                }
            }
            return soruces;
        }
    }
}
