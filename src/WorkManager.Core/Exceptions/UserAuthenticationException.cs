using System;

namespace WorkManager.Core.Exceptions
{
    public class UserAuthenticationException : ApplicationException
    {
        public UserAuthenticationException(string message) : base(message)
        {
        }
    }
}
