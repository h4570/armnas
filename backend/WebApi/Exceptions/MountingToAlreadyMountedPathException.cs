using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Exceptions
{
    /// <summary>
    /// Thrown when user will try mount partition to path which is already used as mount
    /// </summary>
    [Serializable]
    public class MountingToAlreadyMountedPathException : Exception
    {
        public MountingToAlreadyMountedPathException(string name) : base(name) { }
        public MountingToAlreadyMountedPathException(Exception innerEx) : base(innerEx.Message, innerEx) { }
    }
}
