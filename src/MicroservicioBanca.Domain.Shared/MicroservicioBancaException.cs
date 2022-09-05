using MicroservicioBanca.Domain.Shared.Response.Models;
using System;
using System.Runtime.Serialization;

namespace MicroservicioBanca.Domain.Shared
{
    [Serializable]
    public class MicroservicioBancaException : Exception
    {
        public MicroservicioBancaException(SerializationInfo serializationInfo, StreamingContext context) { }
        public MicroservicioBancaException(Error error, Exception innerException = null) : base(error.Message, innerException)
        {
            Code = error.Code;
            Details = error.Details;
        }

        public string Code { get; set; }
        public string Details { get; set; }
    }
}
