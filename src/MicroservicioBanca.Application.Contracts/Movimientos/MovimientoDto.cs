using System;

namespace MicroservicioBanca.Movimientos
{
    public class MovimientoDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public TipoMovimiento Tipo { get; set; }
        public float Valor { get; set; }
        public float SaldoInicial { get; set; }
        public float Saldo { get; set; }
    }
}