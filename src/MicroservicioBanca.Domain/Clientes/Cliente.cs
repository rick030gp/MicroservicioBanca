using MicroservicioBanca.Domain.Cuentas;
using MicroservicioBanca.Domain.Shared.Clientes;
using MicroservicioBanca.Domain.Shared.Cuentas;
using System;
using System.Collections.Generic;

namespace MicroservicioBanca.Domain.Clientes
{
    public class Cliente : Persona
    {
        public Guid Id { get; set; }
        public string Contrasenia { get; set; }
        public bool Estado { get; set; } = true;
        public List<Cuenta> Cuentas { get; set; }

        internal Cliente(
            Guid id,
            string nombre,
            Genero genero,
            short edad,
            string identificacion,
            string direccion,
            string telefono,
            string contrasenia,
            bool estado) : base(nombre, genero, edad, identificacion, direccion, telefono)
        {
            Id = id;
            Contrasenia = contrasenia;
            Estado = estado;
            Cuentas = new List<Cuenta>();
        }

        internal void AgregarCuenta(
            Guid id,
            string numeroCuenta,
            TipoCuenta tipoCuenta,
            float saldoInicial,
            bool estado = true)
        {
            Cuentas ??= new List<Cuenta>();
            Cuentas.Add(new Cuenta(id, numeroCuenta, tipoCuenta, saldoInicial, estado));
        }
    }
}
