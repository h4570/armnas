using System;

namespace OSCommander.Exceptions
{
    /// <summary>
    /// Wrapper exception for Transmission update fail
    /// </summary>
    [Serializable]
    public class TransmissionUpdateException : Exception
    {
        public TransmissionUpdateException(string name) : base(name) { }
        public TransmissionUpdateException(Exception innerEx) : base(innerEx.Message, innerEx) { }
    }
}