using MicroservicioBanca.Cuentas;
using System.Collections.Generic;

namespace MicroservicioBanca.Clientes
{
    public class ClienteCompletoDto
    {
        public string FechaInicial { get; set; }
        public string FechaFinal { get; set; }
        public string Cliente { get; set; }
        public List<ReporteCuentaDto> Cuentas { get; set; }
    }
}
