using System;
using System.Reflection;

namespace ConstructionSite.Helpers.Core
{
    public class Tools
    {
        public static string WriteExeptions(Exception ex)
        {
            string message = string.Empty;
           
            return message;
        }
        //public static string WriteExeptions(DbEntityValidationException ex)
        //{
        //    string message = string.Empty;
        //    foreach (var validationErrors in ex.EntityValidationErrors)
        //    {
        //        foreach (var validationError in validationErrors.ValidationErrors)
        //        {
        //            message = Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //        }
        //    }
        //    return message;
        //}


    }
}