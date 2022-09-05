using MicroservicioBanca.Application.Contracts.Cuentas;
using MicroservicioBanca.Domain.Shared.Response.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.WebApi.Controllers.Cuentas
{
    [ApiController]
    [Route("cuentas")]
    public class CuentaController : ControllerBase, ICuentaAppService
    {
        private readonly ICuentaAppService _cuentaAppService;

        public CuentaController(ICuentaAppService cuentaAppService)
        {
            _cuentaAppService = cuentaAppService;
        }

        [HttpPost]
        [Route("movimiento/agregar")]
        public async Task<Response<CuentaDto>> AddMovementAsync(AddMovementDto input)
        {
            return await _cuentaAppService.AddMovementAsync(input);
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<Response<string>> DeleteAsync(string numeroCuenta)
        {
            return await _cuentaAppService.DeleteAsync(numeroCuenta);
        }

        [HttpDelete]
        [Route("movimiento/eliminar")]
        public async Task<Response<CuentaDto>> DeleteMovementAsync(DeleteMovementDto input)
        {
            return await _cuentaAppService.DeleteMovementAsync(input);
        }

        [HttpGet]
        [Route("todas")]
        public async Task<Response<List<CuentaDto>>> GetAllAsync()
        {
            return await _cuentaAppService.GetAllAsync();
        }

        [HttpGet]
        public async Task<Response<CuentaDto>> GetByAccountNumberAsync(string numeroCuenta)
        {
            return await _cuentaAppService.GetByAccountNumberAsync(numeroCuenta);
        }

        [HttpPost]
        [Route("nueva")]
        public async Task<Response<CuentaDto>> InsertAsync(AddCuentaDto input)
        {
            return await _cuentaAppService.InsertAsync(input);
        }

        [HttpPatch]
        [Route("editar")]
        public async Task<Response<CuentaDto>> UpdateAsync(UpdateCuentaDto input)
        {
            return await _cuentaAppService.UpdateAsync(input);
        }

        [HttpPatch]
        [Route("movimiento/editar")]
        public async Task<Response<CuentaDto>> UpdateMovementAsync(UpdateMovementDto input)
        {
            return await _cuentaAppService.UpdateMovementAsync(input);
        }
    }
}
