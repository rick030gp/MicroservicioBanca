using MicroservicioBanca.Domain.Shared.Cuentas;
using System.ComponentModel.DataAnnotations;

namespace MicroservicioBanca.Application.Contracts.Cuentas
{
    public class UpdateCuentaDto
    {
        [Required]
        public string NumeroCuenta { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
        public bool Estado { get; set; }
    }
}