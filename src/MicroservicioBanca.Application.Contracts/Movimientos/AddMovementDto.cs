using System.ComponentModel.DataAnnotations;

namespace MicroservicioBanca.Application.Contracts.Movimientos
{
    public class AddMovementDto
    {
        [Required]
        public string NumeroCuenta { get; set; }
        [Required]
        public float Valor { get; set; }
    }
}