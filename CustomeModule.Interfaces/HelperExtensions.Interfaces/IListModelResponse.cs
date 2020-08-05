using System.Collections.Generic;

namespace CustomeModule.Interfaces.HelperExtensions.Interfaces
{
    public interface IListModelResponse<T> : IResponse
    {
        int Count { get; set; }
        int PageSize { get; set; }
        int PageNumber { get; set; }
        IEnumerable<T> Data { get; set; }
    }
}
