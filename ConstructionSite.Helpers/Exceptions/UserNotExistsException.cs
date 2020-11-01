using System;

namespace ConstructionSite.Helpers.Exceptions
{
    public class UserNotExistsException : ApplicationException
    {
        public UserNotExistsException(string Name) : base(Name)
        {
        }
    }
}