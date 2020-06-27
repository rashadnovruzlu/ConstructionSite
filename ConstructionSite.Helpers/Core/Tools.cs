using System;
using System.Data.Entity.Validation;
using System.Reflection;


namespace ConstructionSite.Helpers.Core
{
    public class Tools
    {
      
        public static string WriteExeptions(DbEntityValidationException ex)
        {
            string message = string.Empty;
          
                  //  message = Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
            
            return message;
        }


    }
}