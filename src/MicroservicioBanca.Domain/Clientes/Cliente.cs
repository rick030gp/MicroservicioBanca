using MicroservicioBanca.Cuentas;
using System;
using System.Collections.Generic;

namespace MicroservicioBanca.Clientes
{
    public class Cliente : Persona
    {
        public Guid Id { get; set; }
        public string Contrasenia { get; private set; }
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
            bool estado) : base(
                nombre,
                genero,
                edad,
                identificacion,
                direccion,
                telefono)
        {
            Id = id;
            Contrasenia = contrasenia;
            Estado = estado;
            Cuentas = new List<Cuenta>();
        }

        internal Cliente CambiarContrasenia(string contrasenia)
        {
            SetContrasenia(contrasenia);
            return this;
        }

        private void SetContrasenia(string contrasenia)
        {
            Contrasenia = contrasenia;
        }
    }
}
