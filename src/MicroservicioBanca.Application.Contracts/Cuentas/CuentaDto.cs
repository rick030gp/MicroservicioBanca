using MicroservicioBanca.Movimientos;
using System;
using System.Collections.Generic;

namespace MicroservicioBanca.Cuentas
{
    public class CuentaDto
    {
        public Guid ClientId { get; set; }
        public string NumeroCuenta { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
        public float SaldoInicial { get; set; }
        public float Saldo { get; set; }
        public bool Estado { get; set; }
        public List<MovimientoDto> Movimientos { get; set; }
    }
}
