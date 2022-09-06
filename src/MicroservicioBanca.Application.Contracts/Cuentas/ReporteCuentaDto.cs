using MicroservicioBanca.Application.Contracts.Movimientos;
using MicroservicioBanca.Domain.Shared.Cuentas;
using System.Collections.Generic;

namespace MicroservicioBanca.Application.Contracts.Cuentas
{
    public class ReporteCuentaDto
    {
        public string NumeroCuenta { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
        public float SaldoInicial { get; set; }
        public float SaldoDisponible { get; set; }
        public float Movimiento { get; set; }
        public bool Estado { get; set; }
        public List<MovimientoDto> Movimientos { get; set; }
    }
}