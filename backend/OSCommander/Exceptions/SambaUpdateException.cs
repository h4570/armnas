using System;

namespace OSCommander.Exceptions
{
    /// <summary>
    /// Wrapper exception for Samba update fail
    /// </summary>
    [Serializable]
    public class SambaUpdateException : Exception
    {
        public SambaUpdateException(string name) : base(name) { }
        public SambaUpdateException(Exception innerEx) : base(innerEx.Message, innerEx) { }
    }
}