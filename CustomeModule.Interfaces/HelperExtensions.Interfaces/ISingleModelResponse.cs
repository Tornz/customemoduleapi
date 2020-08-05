

namespace CustomeModule.Interfaces.HelperExtensions.Interfaces
{
    public interface ISingleModelResponse<T> : IResponse
    {
        T Data { get; set; }
    }
}
