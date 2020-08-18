﻿using System;
using System.Linq;
using System.Reflection;

namespace ConstructionSite.Extensions.Mapping
{
    public static class MapExtension
    {
        public static T Mapped<T>(this object query)
        {
            Type TargetType = typeof(T);
            Type SoruceType = query.GetType();
            T soruces = Activator.CreateInstance<T>();
            PropertyInfo[] propertyInfo = TargetType.GetProperties();
            foreach (var item in SoruceType.GetProperties())
            {
                var target = TargetType.GetProperties()
                       .FirstOrDefault(x => x.Name.ToUpper() == item.Name.ToUpper());
                if (target != null)
                {
                    object data = item.GetValue(query);
                    target.SetValue(soruces, data);
                }
            }
            return soruces;
        }
    }
}