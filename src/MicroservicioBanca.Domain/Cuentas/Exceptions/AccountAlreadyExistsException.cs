using System;
using System.Runtime.Serialization;

namespace MicroservicioBanca.Domain.Cuentas.Exceptions
{
    public class AccountAlreadyExistsException : Exception
    {
        public AccountAlreadyExistsException(string name)
            : base()
        {
        }

        protected AccountAlreadyExistsException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        
    }
}
