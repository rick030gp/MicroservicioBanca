using System;
using System.ComponentModel.DataAnnotations;

namespace MicroservicioBanca.Application.Contracts.Cuentas
{
    public class DeleteMovementDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string NumeroCuenta { get; set; }
    }
}