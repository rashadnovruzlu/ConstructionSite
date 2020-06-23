using System;

namespace ConstructionSite.Helpers.Exceptions
{
    public class DbContextCommitException : ApplicationException
    {
        public DbContextCommitException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}