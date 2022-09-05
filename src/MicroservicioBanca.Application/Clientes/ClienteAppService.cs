using AutoMapper;
using MicroservicioBanca.Application.Contracts.Clientes;
using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Domain.Shared;
using MicroservicioBanca.Domain.Shared.Response;
using MicroservicioBanca.Domain.Shared.Response.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.Application.Clientes
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ClienteManager _clienteManager;
        private readonly IMapper _mapper;

        public ClienteAppService(
            IClienteRepository clientRepository,
            ClienteManager clienteManager,
            IMapper mapper)
        {
            _clienteRepository = clientRepository;
            _clienteManager = clienteManager;
            _mapper = mapper;
        }

        public async Task<Response<string>> DeleteAsync(string identification)
        {
            ResponseManager<string> response = new();
            try
            {
                var cliente = await _clienteManager.DeleteAsync(identification);
                await _clienteRepository.RemoveAsync(cliente);
                return response.OnSuccess("Eliminado exitosamente");
            }
            catch (MicroservicioBancaException ex)
            {
                return response.OnError(new Error(ex));
            }
            catch (Exception)
            {
                return response.OnError(MicroservicioBancaErrors.GeneralError);
            }

        }

        public async Task<Response<List<ClienteDto>>> GetAllAsync()
        {
            ResponseManager<List<ClienteDto>> response = new();
            try
            {
                var clients = await _clienteRepository.GetAllAsync();
                return response.OnSuccess(_mapper.Map<List<ClienteDto>>(clients));
            }
            catch (MicroservicioBancaException ex)
            {
                return response.OnError(new Error(ex));
            }
            catch (Exception)
            {
                return response.OnError(MicroservicioBancaErrors.GeneralError);
            }
        }

        public async Task<Response<ClienteDto>> GetByIdentificationAsync(string identification)
        {
            ResponseManager<ClienteDto> response = new();
            try
            {
                var cliente = await _clienteRepository.GetByIdentificationAsync(identification);
                if (cliente == null)
                    return response.OnError(MicroservicioBancaErrors.ClientNotFoundError);

                return response.OnSuccess(_mapper.Map<ClienteDto>(cliente));
            }
            catch (MicroservicioBancaException ex)
            {
                return response.OnError(new Error(ex));
            }
            catch (Exception)
            {
                return response.OnError(MicroservicioBancaErrors.GeneralError);
            }
        }

        public async Task<Response<ClienteDto>> InsertAsync(AddClienteDto input)
        {
            ResponseManager<ClienteDto> response = new();
            try
            {
                var client = await _clienteManager.CreateAsync(
                    input.Nombre,
                    input.Genero,
                    input.Edad,
                    input.Identificacion,
                    input.Direccion,
                    input.Telefono,
                    input.Contrasenia);

                await _clienteRepository.InsertAsync(client);
                return response.OnSuccess(_mapper.Map<ClienteDto>(client));
            }
            catch (MicroservicioBancaException ex)
            {
                return response.OnError(new Error(ex));
            }
            catch (Exception)
            {
                return response.OnError(MicroservicioBancaErrors.GeneralError);
            }
        }

        public async Task<Response<ClienteDto>> UpdateAsync(UpdateClienteDto input)
        {
            ResponseManager<ClienteDto> response = new();
            try
            {
                var cliente = await _clienteManager.UpdateAsync(
                    input.Identificacion,
                    input.Nombre,
                    input.Genero,
                    input.Edad,
                    input.Direccion,
                    input.Telefono,
                    input.Contrasenia,
                    input.Estado);

                await _clienteRepository.UpdateAsync(cliente);
                return response.OnSuccess(_mapper.Map<ClienteDto>(cliente));
            }
            catch (MicroservicioBancaException ex)
            {
                return response.OnError(new Error(ex));
            }
            catch (Exception)
            {
                return response.OnError(MicroservicioBancaErrors.GeneralError);
            }
        }
    }
}
