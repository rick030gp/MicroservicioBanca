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
            Guid clienteId,
            string numeroCuenta,
            TipoCuenta tipoCuenta,
            float saldoInicial,
            bool estado = true)
        {
            Id = id;
            ClientId = clienteId;
            NumeroCuenta = numeroCuenta;
            TipoCuenta = tipoCuenta;
            SaldoInicial = saldoInicial;
            Saldo = saldoInicial;
            Estado = estado;
        }

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

        private void SetSaldo(float saldo)
        {
            Saldo = saldo;
        }

        internal void AgregarMovimiento(
            Guid id,
            float valor)
        {
            Movimientos ??= new List<Movimiento>();
            if (valor == 0)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.MovementZeroError);

            TipoMovimiento tipoMovimiento = valor < 0 ?
                TipoMovimiento.Debito : TipoMovimiento.Credito;
            
            if (tipoMovimiento == TipoMovimiento.Debito && Saldo < Math.Abs(valor))
                throw new MicroservicioBancaException(MicroservicioBancaErrors.InsufficientBalanceError);

            Movimientos.Add(new Movimiento(id, this.Id, tipoMovimiento, Saldo, valor));
            SetSaldo(Saldo + valor);
        }
    }
}
