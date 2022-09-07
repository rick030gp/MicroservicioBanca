using MicroservicioBanca.Response.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.Movimientos
{
    public interface IMovimientoAppService
    {
        Task<Response<List<MovimientoDto>>> GetByAccountNumberAsync(string numeroCuenta);
        Task<Response<MovimientoDto>> CreateAsync(AddMovementDto input);
        Task<Response<MovimientoDto>> UpdateAsync(UpdateMovementDto input);
        Task<Response<MovimientoDto>> DeleteAsync(DeleteMovementDto input);
    }
}
