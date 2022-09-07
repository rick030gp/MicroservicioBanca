namespace MicroservicioBanca.Response.Models
{
    public class Response<T>
    {
        public Response()
        {
            Success = true;
            UnAuthorizedRequest = false;
            TargetUrl = string.Empty;
            Error = null;
        }

        public bool Success { get; set; }
        public T Result { get; set; }
        public Error Error { get; set; }
        public string TargetUrl { get; set; }
        public bool UnAuthorizedRequest { get; set; }
    }
}
