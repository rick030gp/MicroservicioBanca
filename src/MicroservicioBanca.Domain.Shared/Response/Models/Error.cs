namespace MicroservicioBanca.Domain.Shared.Response.Models
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

        public Error() { }

        public Error(MicroservicioBancaException exception)
        {
            Code = exception.Code;
            Message = exception.Message;
            Details = exception.Details;
        }
    }
}
