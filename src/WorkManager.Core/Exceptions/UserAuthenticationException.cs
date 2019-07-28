using System;

namespace WorkManager.Core.Exceptions
{
    public class UserAuthenticationException : Exception
    {
        public UserAuthenticationException()
        {
        }

        public UserAuthenticationException(string message)
            : base(message)
        {
        }

        public UserAuthenticationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
