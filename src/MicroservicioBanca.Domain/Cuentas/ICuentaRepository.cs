using System;
using System.Threading.Tasks;

namespace MicroservicioBanca.Domain.Cuentas
{
    public interface ICuentaRepository : IGenericRepository<Cuenta, Guid>
    {
        Task<Cuenta> GetByAccountNumberAsync(string accountNumber);
        Task<Cuenta> GetWithMovementsByAccountNumberAsync(string accountNumber);
    }
}
