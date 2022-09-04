using MicroservicioBanca.Domain.Shared.Cuentas;
using System;

namespace MicroservicioBanca.Domain.Cuentas
{
    public class Movimiento
    {
        public Guid Id { get; set; }
        public Guid CuentaId { get; set; }
        public DateTime Fecha { get; set; }
        public TipoMovimiento Tipo { get; private set; }
        public float Valor { get; set; }
        public float Saldo { get; private set; }

        internal Movimiento(
            Guid id,
            Guid cuentaId,
            TipoMovimiento tipo,
            float valor,
            float saldo)
        {
            Id = id;
            CuentaId = cuentaId;
            Tipo = tipo;
            Valor = valor;
            Saldo = saldo;
        }
    }
}
