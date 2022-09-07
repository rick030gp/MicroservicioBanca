using System;
using System.Threading.Tasks;

namespace MicroservicioBanca.Clientes
{
    public class ClienteManager
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteManager(
            IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Cliente> CreateAsync(
            string nombre,
            Genero genero,
            short edad,
            string identificacion,
            string direccion,
            string telefono,
            string contrasenia,
            bool estado = true)
        {
            var existingClient = await _clienteRepository.GetByIdentificationAsync(identificacion);
            if (existingClient != null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.ClientAlreadyExistsError);
            
            var cliente = new Cliente(
                Guid.NewGuid(),
                nombre,
                genero,
                edad,
                identificacion,
                direccion,
                telefono,
                contrasenia,
                estado);

            return cliente;
        }

        public async Task<Cliente> UpdateAsync(
            string identificacion,
            string nombre,
            Genero? genero,
            short? edad,
            string direccion,
            string telefono,
            string contrasenia,
            bool? estado)
        {
            var cliente = await _clienteRepository.GetByIdentificationAsync(identificacion);
            if (cliente == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.ClientNotFoundError);

            cliente.Nombre = nombre ?? cliente.Nombre;
            cliente.Genero = genero ?? cliente.Genero;
            cliente.Edad = edad ?? cliente.Edad;
            cliente.Direccion = direccion ?? cliente.Direccion;
            cliente.Telefono = telefono ?? cliente.Telefono;
            cliente.CambiarContrasenia(contrasenia ?? cliente.Contrasenia);
            cliente.Estado = estado ?? cliente.Estado;

            return cliente;
        }
    }
}
