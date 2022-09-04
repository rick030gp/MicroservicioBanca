using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.Application.Contracts.Clientes
{
    public interface IClienteAppService
    {
        Task<List<ClienteDto>> GetAllClients();
    }
}
