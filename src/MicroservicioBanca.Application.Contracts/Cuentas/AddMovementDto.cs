using System.ComponentModel.DataAnnotations;

namespace MicroservicioBanca.Application.Contracts.Cuentas
{
    public class AddMovementDto
    {
        [Required]
        public string NumeroCuenta { get; set; }
        [Required]
        public float Valor { get; set; }
    }
}