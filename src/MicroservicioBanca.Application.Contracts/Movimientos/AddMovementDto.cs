using System.ComponentModel.DataAnnotations;

namespace MicroservicioBanca.Movimientos
{
    public class AddMovementDto
    {
        [Required]
        public string NumeroCuenta { get; set; }
        [Required]
        public float Valor { get; set; }
    }
}