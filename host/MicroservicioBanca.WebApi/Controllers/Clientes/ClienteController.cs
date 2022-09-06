using MicroservicioBanca.Application.Contracts.Clientes;
using MicroservicioBanca.Domain.Shared.Response.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.WebApi.Controllers.Clientes
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase, IClienteAppService
    {
        private readonly IClienteAppService _clienteAppService;

        public ClienteController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<Response<string>> DeleteAsync(string identification)
        {
            return await _clienteAppService.DeleteAsync(identification);
        }

        [HttpGet]
        [Route("reportes")]
        public async Task<Response<ClienteCompletoDto>> GetAccountsStatementAsync(string identificacionCliente, DateTime fechaInicial, DateTime? fechaFinal = null)
        {
            return await _clienteAppService.GetAccountsStatementAsync(
                identificacionCliente,
                fechaInicial,
                fechaFinal); 
        }

        [HttpGet]
        [Route("todos")]
        public async Task<Response<List<ClienteDto>>> GetAllAsync()
        {
            return await _clienteAppService.GetAllAsync();
        }

        [HttpGet]
        public async Task<Response<ClienteDto>> GetByIdentificationAsync(string identification)
        {
            return await _clienteAppService.GetByIdentificationAsync(identification);
        }

        [HttpPost]
        [Route("nuevo")]
        public async Task<Response<ClienteDto>> InsertAsync(AddClienteDto input)
        {
            return await _clienteAppService.InsertAsync(input);
        }

        [HttpPatch]
        [Route("editar")]
        public async Task<Response<ClienteDto>> UpdateAsync(UpdateClienteDto input)
        {
            return await _clienteAppService.UpdateAsync(input);
        }
    }
}
