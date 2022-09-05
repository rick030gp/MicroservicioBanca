using MicroservicioBanca.Domain.Shared.Response.Models;

namespace MicroservicioBanca.Domain.Shared.Response.Contracts
{
    public interface IResponseManager<T>
    {
        Response<T> OnError(Error error, bool UnAuthorizedRequest = false, string targetUrl = "");
        Response<T> OnSuccess(T response, bool UnAuthorizedRequest = true, string targetUrl = "");
    }
}
