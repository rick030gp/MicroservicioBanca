using MicroservicioBanca.Domain.Shared;
using MicroservicioBanca.Domain.Shared.Clientes;
using System;
using System.Threading.Tasks;

namespace MicroservicioBanca.Domain.Clientes
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

            await _clienteRepository.InsertAsync(cliente);
            return cliente;
        }

        public async Task<Cliente> UpdateAsync(
            string identificacion,
            string nombre,
            Genero genero,
            short edad,
            string direccion,
            string telefono,
            string contrasenia,
            bool estado)
        {
            var cliente = await _clienteRepository.GetByIdentificationAsync(identificacion);
            if (cliente == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.ClientNotFoundError);

            cliente.Nombre = nombre;
            cliente.Genero = genero;
            cliente.Edad = edad;
            cliente.Direccion = direccion;
            cliente.Telefono = telefono;
            cliente.CambiarContrasenia(contrasenia);
            cliente.Estado = estado;

            await _clienteRepository.UpdateAsync(cliente);
            return cliente;
        }

        public async Task DeleteAsync(string identificacion)
        {
            var cliente = await _clienteRepository.GetByIdentificationAsync(identificacion);
            if (cliente == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.ClientNotFoundError);

            await _clienteRepository.RemoveAsync(cliente);
        }
    }
}
