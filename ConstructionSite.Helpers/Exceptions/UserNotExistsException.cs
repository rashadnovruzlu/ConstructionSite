using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Helpers.Exceptions
{
    public class UserNotExistsException : ApplicationException
    {
        public UserNotExistsException(string Name):base(Name)
        {
        }
    }
}
