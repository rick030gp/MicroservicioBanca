using AutoMapper;
using MicroservicioBanca.Application.Contracts.Clientes;
using MicroservicioBanca.Domain.Clientes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.Application.Clientes
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteAppService(IClienteRepository clientRepository, IMapper mapper)
        {
            _clienteRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<List<ClienteDto>> GetAllClients()
        {
            var clients = await _clienteRepository.GetAllAsync();
            return _mapper.Map<List<ClienteDto>>(clients);
        }
    }
}
