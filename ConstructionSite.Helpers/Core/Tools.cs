﻿using System;
using System.Data.Entity.Validation;

namespace ConstructionSite.Helpers.Core
{
    public class Tools
    {
        public static string WriteEntityValidationException(DbEntityValidationException ex)
        {
            string message = string.Empty;

            foreach (var validationErrors in ex.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    message += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                }
            }

            return message;
        }
    }
}