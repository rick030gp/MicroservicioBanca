using MicroservicioBanca.Domain.Shared.Clientes;

namespace MicroservicioBanca.Application.Contracts.Clientes
{
    public class ClienteDto
    {
        public string Nombre { get; set; }
        public Genero Genero { get; set; }
        public short Edad { get; set; }
        public string Identificacion { get; private set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
    }
}
