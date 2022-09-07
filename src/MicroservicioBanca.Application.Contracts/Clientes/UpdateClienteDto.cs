using System.ComponentModel.DataAnnotations;

namespace MicroservicioBanca.Clientes
{
    public class UpdateClienteDto
    {
        [Required]
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public Genero? Genero { get; set; }
        public short? Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool? Estado { get; set; }
        public string Contrasenia { get; set; }
    }
}
