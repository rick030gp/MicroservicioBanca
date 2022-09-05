using MicroservicioBanca.Domain.Shared.Cuentas;
using System;

namespace MicroservicioBanca.Domain.Cuentas
{
    public class Movimiento
    {
        public Guid Id { get; set; }
        public Guid CuentaId { get; private set; }
        public DateTime Fecha { get; private set; }
        public TipoMovimiento Tipo { get; private set; }
        public float Valor { get; private set; }
        public float SaldoInicial { get; private set; }
        public float Saldo { get; private set; }

        internal Movimiento(
            Guid id,
            Guid cuentaId,
            TipoMovimiento tipo,
            float saldoInicial,
            float valor)
        {
            Id = id;
            CuentaId = cuentaId;
            Tipo = tipo;
            Valor = valor;
            SaldoInicial = saldoInicial;
            Saldo = SaldoInicial + valor;
        }
    }
}
