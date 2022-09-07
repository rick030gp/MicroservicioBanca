using System;
using System.Threading.Tasks;

namespace MicroservicioBanca.Clientes
{
    public interface IClienteRepository : IGenericRepository<Cliente, Guid>
    {
        Task<Cliente> GetByIdentificationAsync(string identification);
        Task<Cliente> GetWithAccountsByIdentificationAndDatesAsync(
            string identification, DateTime fechaInicial, DateTime? fechaFinal = null);
    }
}
