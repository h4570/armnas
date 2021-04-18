using System;

namespace WebApi.Exceptions
{
    /// <summary>
    /// Thrown when exception occur during login operation
    /// </summary>
    [Serializable]
    public class LoginOperationException : Exception
    {
        public LoginOperationException(string name) : base(name) { }
        public LoginOperationException(Exception innerEx) : base(innerEx.Message, innerEx) { }
    }
}
