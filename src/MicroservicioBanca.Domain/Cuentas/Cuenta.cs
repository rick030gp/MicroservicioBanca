using MicroservicioBanca.Domain.Shared;
using MicroservicioBanca.Domain.Shared.Cuentas;
using System;
using System.Collections.Generic;

namespace MicroservicioBanca.Domain.Cuentas
{
    public class Cuenta
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string NumeroCuenta { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
        public float SaldoInicial { get; private set; }
        public float Saldo { get; private set; }
        public bool Estado { get; set; } = true;
        public virtual List<Movimiento> Movimientos { get; set; }

        internal Cuenta(
            Guid id,
            string numeroCuenta,
            TipoCuenta tipoCuenta,
            float saldoInicial,
            bool estado = true)
        {
            Id = id;
            NumeroCuenta = numeroCuenta;
            TipoCuenta = tipoCuenta;
            SaldoInicial = saldoInicial;
            Saldo = saldoInicial;
            Estado = estado;
        }

        internal void AgregarMovimiento(
            Guid id,
            Guid cuentaId,
            TipoMovimiento tipo,
            float valor)
        {
            Movimientos ??= new List<Movimiento>();
            if (!Estado)
                throw new Exception(MicroservicioBancaErrors.InactiveAccountError);
            if (tipo == TipoMovimiento.Debito && Saldo < valor)
                throw new Exception(MicroservicioBancaErrors.InsufficientBalanceError);

            switch(tipo)
            {
                case TipoMovimiento.Debito:
                    Saldo -= valor;
                    break;
                case TipoMovimiento.Credito:
                    Saldo += valor;
                    break;
                default:
                    throw new Exception(MicroservicioBancaErrors.MovementTypeDoesNotExistError);
            }

            Movimientos.Add(new Movimiento(id, cuentaId, tipo, valor, Saldo));
        }
    }
}
