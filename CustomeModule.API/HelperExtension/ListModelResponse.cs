
using CustomeModule.Interfaces.HelperExtensions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomeModule.API.HelperExtension
{
    public class ListModelResponse<T> : IListModelResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
        public string Success { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
