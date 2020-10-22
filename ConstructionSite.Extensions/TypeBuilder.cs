using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ConstructionSite.Extensions
{
    public static class TypeBuilder
    {
        public static void builder<T>(EntityTypeBuilder<T> typeBuilder) where T : class
        {
            Type myType = typeBuilder.GetType();
            foreach (var item in myType.GetProperties())
            {
                if (item.PropertyType == typeof(bool))
                {
                    item.SetValue(item, false);
                }
            }
        }
    }
}