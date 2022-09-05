using MicroservicioBanca.Application.Contracts.Cuentas;
using System.Collections.Generic;

namespace MicroservicioBanca.Application.Contracts.Clientes
{
    public class ClienteCompletoDto : ClienteDto
    {
        public List<CuentaDto> Cuentas { get; set; }
    }
}
