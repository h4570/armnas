using System;

namespace OSCommander.Exceptions
{
    /// <summary>
    /// Wrapper exception for JSON parsing fail.
    /// </summary>
    [Serializable]
    public class JsonParsingException : Exception
    {
        public JsonParsingException(string name) : base(name) { }
        public JsonParsingException(Exception innerEx) : base(innerEx.Message, innerEx) { }
    }
}