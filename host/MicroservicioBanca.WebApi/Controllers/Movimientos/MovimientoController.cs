using MicroservicioBanca.Application.Contracts.Movimientos;
using MicroservicioBanca.Domain.Shared.Response.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.WebApi.Controllers.Movimientos
{
    [ApiController]
    [Route("api/movimientos")]
    public class MovimientoController : ControllerBase, IMovimientoAppService
    {
        private readonly IMovimientoAppService _movimientoAppService;

        public MovimientoController(IMovimientoAppService movimientoAppService)
        {
            _movimientoAppService = movimientoAppService;
        }

        [HttpPost]
        [Route("nuevo")]
        public async Task<Response<MovimientoDto>> CreateAsync(AddMovementDto input)
        {
            return await _movimientoAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<Response<MovimientoDto>> DeleteAsync(DeleteMovementDto input)
        {
            return await _movimientoAppService.DeleteAsync(input);
        }

        [HttpGet]
        public async Task<Response<List<MovimientoDto>>> GetByAccountNumberAsync(string numeroCuenta)
        {
            return await _movimientoAppService.GetByAccountNumberAsync(numeroCuenta);
        }

        [HttpPatch]
        [Route("editar")]
        public async Task<Response<MovimientoDto>> UpdateAsync(UpdateMovementDto input)
        {
            return await _movimientoAppService.UpdateAsync(input);
        }
    }
}
