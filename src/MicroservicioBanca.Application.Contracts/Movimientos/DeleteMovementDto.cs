using System;
using System.ComponentModel.DataAnnotations;

namespace MicroservicioBanca.Movimientos
{
    public class DeleteMovementDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string NumeroCuenta { get; set; }
    }
}