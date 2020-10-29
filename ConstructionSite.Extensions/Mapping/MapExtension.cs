using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Mapping
{
    public static class MapExtension
    {
        public static T Mapped<T>(this object query)
        {
            if (query != null)
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
            return default(T);
        }

        public static Task<T> MappedAsync<T>(this object query)
        {
            if (query != null)
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
                return Task.FromResult(soruces);
            }
            return Task.FromResult(default(T)); ;
        }

        public static T Mapped<T>(this IEnumerable<T> query)
        {
            if (query != null)
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
            return default(T);
        }

        public static Task<T> MappedAsync<T>(this IEnumerable<T> query)
        {
            if (query != null)
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
                return Task.FromResult(soruces);
            }
            return Task.FromResult(default(T));
        }

        public async static Task<T> MappedAsync<T>(this IQueryable<T> query)
        {
            if (query != null)
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
                return await Task.FromResult(soruces);
            }
            return default(T);
        }
    }
}