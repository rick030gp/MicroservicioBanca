using MicroservicioBanca.Domain.Shared.Cuentas;
using System.ComponentModel.DataAnnotations;

namespace MicroservicioBanca.Application.Contracts.Cuentas
{
    public class AddCuentaDto
    {
        [Required]
        public string IdentificacionCliente { get; set; }
        [Required]
        public string NumeroCuenta { get; set; }
        [Required]
        public TipoCuenta TipoCuenta { get; set; }
        public float SaldoInicial { get; set; }
        public bool Estado { get; set; }
    }
}