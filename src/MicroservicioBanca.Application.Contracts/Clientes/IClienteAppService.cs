using MicroservicioBanca.Domain.Shared.Response.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.Application.Contracts.Clientes
{
    public interface IClienteAppService
    {
        Task<Response<List<ClienteDto>>> GetAllAsync();
        Task<Response<ClienteDto>> GetByIdentificationAsync(string identification);
        Task<Response<ClienteDto>> InsertAsync(AddClienteDto input);
        Task<Response<ClienteDto>> UpdateAsync(UpdateClienteDto input);
        Task<Response<string>> DeleteAsync(string identification);
    }
}
