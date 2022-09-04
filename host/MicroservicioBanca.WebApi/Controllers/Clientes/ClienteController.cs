using MicroservicioBanca.Application.Contracts.Clientes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.WebApi.Controllers.Clientes
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase, IClienteAppService
    {
        private readonly IClienteAppService _clienteAppService;

        public ClienteController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
        }

        [HttpGet]
        public async Task<List<ClienteDto>> GetAllClients()
        {
            return await _clienteAppService.GetAllClients();
        }
    }
}
