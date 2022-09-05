using AutoMapper;
using MicroservicioBanca.Application.Contracts.Movimientos;
using MicroservicioBanca.Domain.Cuentas;
using MicroservicioBanca.Domain.Movimientos;
using MicroservicioBanca.Domain.Shared;
using MicroservicioBanca.Domain.Shared.Response;
using MicroservicioBanca.Domain.Shared.Response.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.Application
{
    public class MovimientoAppService : IMovimientoAppService
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly MovimientoManager _movimientoManager;
        private readonly IMapper _mapper;

        public MovimientoAppService(
            ICuentaRepository cuentaRepository,
            MovimientoManager movimientoManager,
            IMapper mapper)
        {
            _cuentaRepository = cuentaRepository;
            _movimientoManager = movimientoManager;
            _mapper = mapper;
        }

        public async Task<Response<MovimientoDto>> CreateAsync(AddMovementDto input)
        {
            ResponseManager<MovimientoDto> response = new();
            try
            {
                var movimiento = await _movimientoManager.CreateAsync(
                    input.NumeroCuenta,
                    input.Valor);

                return response.OnSuccess(_mapper.Map<MovimientoDto>(movimiento));
            }
            catch (MicroservicioBancaException ex)
            {
                return response.OnError(new Error(ex));
            }
            catch (Exception)
            {
                return response.OnError(MicroservicioBancaErrors.GeneralError);
            }
        }

        public async Task<Response<MovimientoDto>> DeleteAsync(DeleteMovementDto input)
        {
            ResponseManager<MovimientoDto> response = new();
            try
            {
                var movimiento = await _movimientoManager.DeleteAsync(
                    input.Id,
                    input.NumeroCuenta);

                return response.OnSuccess(_mapper.Map<MovimientoDto>(movimiento));
            }
            catch (MicroservicioBancaException ex)
            {
                return response.OnError(new Error(ex));
            }
            catch (Exception)
            {
                return response.OnError(MicroservicioBancaErrors.GeneralError);
            }
        }

        public async Task<Response<List<MovimientoDto>>> GetByAccountNumberAsync(string numeroCuenta)
        {
            ResponseManager<List<MovimientoDto>> response = new();
            try
            {
                var cuenta = await _cuentaRepository.GetWithMovementsByAccountNumberAsync(numeroCuenta);
                if (cuenta == null)
                    return response.OnError(MicroservicioBancaErrors.AccountDoesNotExistError);
                return response.OnSuccess(_mapper.Map<List<MovimientoDto>>(cuenta.Movimientos));
            }
            catch (MicroservicioBancaException ex)
            {
                return response.OnError(new Error(ex));
            }
            catch (Exception)
            {
                return response.OnError(MicroservicioBancaErrors.GeneralError);
            }
        }

        public async Task<Response<MovimientoDto>> UpdateAsync(UpdateMovementDto input)
        {
            ResponseManager<MovimientoDto> response = new();
            try
            {
                var movimiento = await _movimientoManager.UpdateAsync(
                    input.Id,
                    input.NumeroCuenta,
                    input.Valor);

                return response.OnSuccess(_mapper.Map<MovimientoDto>(movimiento));
            }
            catch (MicroservicioBancaException ex)
            {
                return response.OnError(new Error(ex));
            }
            catch (Exception)
            {
                return response.OnError(MicroservicioBancaErrors.GeneralError);
            }
        }
    }
}
