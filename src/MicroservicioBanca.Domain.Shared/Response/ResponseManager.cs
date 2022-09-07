using MicroservicioBanca.Response.Contracts;
using MicroservicioBanca.Response.Models;

namespace MicroservicioBanca.Response
{
    public class ResponseManager<T> : IResponseManager<T>
    {
        public Response<T> OnError(Error error, bool UnAuthorizedRequest = false, string targetUrl = "")
        {
            Response<T> result = new Response<T>()
            {
                Error = error,
                Result = default,
                Success = false,
                TargetUrl = targetUrl,
                UnAuthorizedRequest = UnAuthorizedRequest
            };

            return result;
        }

        public Response<T> OnSuccess(T response, bool UnAuthorizedRequest = true, string targetUrl = "")
        {
            Response<T> result = new Response<T>()
            {
                Error = null,
                Result = response,
                Success = true,
                TargetUrl = targetUrl,
                UnAuthorizedRequest = UnAuthorizedRequest
            };

            return result;
        }
    }
}
