using MicroservicioBanca.Cuentas;
using MicroservicioBanca.Response.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.Controllers.Cuentas
{
    [ApiController]
    [Route("api/cuentas")]
    public class CuentaController : ControllerBase, ICuentaAppService
    {
        private readonly ICuentaAppService _cuentaAppService;

        public CuentaController(ICuentaAppService cuentaAppService)
        {
            _cuentaAppService = cuentaAppService;
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<Response<string>> DeleteAsync(string numeroCuenta)
        {
            return await _cuentaAppService.DeleteAsync(numeroCuenta);
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
    }
}
