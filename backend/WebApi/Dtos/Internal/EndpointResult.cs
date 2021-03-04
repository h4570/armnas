using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos.Internal
{
    public class EndpointResult<T>
    {
        public T Result { get; set; }
        // Status: 200
        public bool Succeed { get; set; }
        // For "Succeed" == false
        public ushort StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
