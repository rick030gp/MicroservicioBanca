using System;
using System.ComponentModel.DataAnnotations;

namespace MicroservicioBanca.Movimientos
{
    public class UpdateMovementDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string NumeroCuenta { get; set; }
        [Required]
        public float Valor { get; set; }
    }
}