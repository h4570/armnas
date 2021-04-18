using System;

namespace OSCommander.Exceptions
{
    /// <summary>
    /// Wrapper exception for Cron parsing fail.
    /// </summary>
    [Serializable]
    public class CronParseException : Exception
    {
        public CronParseException(string name) : base(name) { }
        public CronParseException(Exception innerEx) : base(innerEx.Message, innerEx) { }
    }
}