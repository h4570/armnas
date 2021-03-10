using System;

namespace OSCommander.Services
{
    /// <summary>
    /// Wrapper exception for smb.conf update fail.
    /// </summary>
    [Serializable]
    public class SambaUpdateException : Exception
    {
        public SambaUpdateException(string name) : base(name) { }
        public SambaUpdateException(Exception innerEx) : base(innerEx.Message, innerEx) { }
    }
}