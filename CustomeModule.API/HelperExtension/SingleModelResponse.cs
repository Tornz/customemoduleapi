using CustomeModule.Interfaces.HelperExtensions.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomeModule.API.HelperExtension
{
    public class SingleModelResponse<T> : ISingleModelResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Success { get; set; }
        public T Data { get; set; }
    }
}
