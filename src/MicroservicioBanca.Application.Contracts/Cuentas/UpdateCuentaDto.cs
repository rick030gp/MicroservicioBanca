using System.ComponentModel.DataAnnotations;

namespace MicroservicioBanca.Cuentas
{
    public class UpdateCuentaDto
    {
        [Required]
        public string NumeroCuenta { get; set; }
        public TipoCuenta? TipoCuenta { get; set; }
        public bool? Estado { get; set; }
    }
}