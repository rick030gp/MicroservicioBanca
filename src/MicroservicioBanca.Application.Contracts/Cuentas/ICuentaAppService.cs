using MicroservicioBanca.Domain.Shared.Response.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.Application.Contracts.Cuentas
{
    public interface ICuentaAppService
    {
        Task<Response<List<CuentaDto>>> GetAllAsync();
        Task<Response<CuentaDto>> GetByAccountNumberAsync(string numeroCuenta);
        Task<Response<CuentaDto>> InsertAsync(AddCuentaDto input);
        Task<Response<CuentaDto>> UpdateAsync(UpdateCuentaDto input);
        Task<Response<string>> DeleteAsync(string numeroCuenta);
        Task<Response<CuentaDto>> AddMovementAsync(AddMovementDto input);
        Task<Response<CuentaDto>> UpdateMovementAsync(UpdateMovementDto input);
        Task<Response<CuentaDto>> DeleteMovementAsync(DeleteMovementDto input);
    }
}
