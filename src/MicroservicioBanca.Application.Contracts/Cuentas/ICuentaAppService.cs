using MicroservicioBanca.Response.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.Cuentas
{
    public interface ICuentaAppService
    {
        Task<Response<List<CuentaDto>>> GetAllAsync();
        Task<Response<CuentaDto>> GetByAccountNumberAsync(string numeroCuenta);
        Task<Response<CuentaDto>> InsertAsync(AddCuentaDto input);
        Task<Response<CuentaDto>> UpdateAsync(UpdateCuentaDto input);
        Task<Response<string>> DeleteAsync(string numeroCuenta);
    }
}
