

namespace CustomeModule.Interfaces.HelperExtensions.Interfaces
{
    public interface IResponse
    {
        int StatusCode { get; set; }
        string Message { get; set; }
        string Success { get; set; }
    }
}
