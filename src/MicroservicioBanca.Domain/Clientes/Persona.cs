using MicroservicioBanca.Domain.Shared.Clientes;

namespace MicroservicioBanca.Domain.Clientes
{
    public class Persona
    {
        public string Nombre { get; set; }
        public Genero Genero { get; set; }
        public short Edad { get; set; }
        public string Identificacion { get; private set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public Persona()
        {
        }

        internal Persona(
            string nombre,
            Genero genero,
            short edad,
            string identificacion,
            string direccion,
            string telefono)
        {
            Nombre = nombre;
            Genero = genero;
            Edad = edad;
            Identificacion = identificacion;
            Direccion = direccion;
            Telefono = telefono;
        }
    }
}
