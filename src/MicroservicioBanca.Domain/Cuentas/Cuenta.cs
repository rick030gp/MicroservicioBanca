using MicroservicioBanca.Movimientos;
using System;
using System.Collections.Generic;

namespace MicroservicioBanca.Cuentas
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

        private Cuenta()
        {
        }

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

            Movimientos = new List<Movimiento>();
        }

        internal Cuenta UpdateSaldo(float saldo)
        {
            Saldo = saldo;
            return this;
        }
    }
}
