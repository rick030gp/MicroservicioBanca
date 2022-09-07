using System.ComponentModel.DataAnnotations;

namespace MicroservicioBanca.Clientes
{
    public class AddClienteDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public Genero Genero { get; set; }
        [Required]
        public short Edad { get; set; }
        [Required]
        public string Identificacion { get; set; }

        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; } = true;

        [Required]
        public string Contrasenia { get; set; }
    }
}
