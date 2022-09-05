﻿using AutoMapper;
using MicroservicioBanca.Application.Contracts.Cuentas;
using MicroservicioBanca.Domain.Cuentas;
using MicroservicioBanca.Domain.Shared;
using MicroservicioBanca.Domain.Shared.Response;
using MicroservicioBanca.Domain.Shared.Response.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioBanca.Application.Cuentas
{
    public class CuentaAppService : ICuentaAppService
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly CuentaManager _cuentaManager;
        private readonly IMapper _mapper;

        public CuentaAppService(
            ICuentaRepository cuentaRepository,
            CuentaManager cuentaManager,
            IMapper mapper)
        {
            _cuentaRepository = cuentaRepository;
            _cuentaManager = cuentaManager;
            _mapper = mapper;
        }

        public async Task<Response<CuentaDto>> AddMovementAsync(AddMovementDto input)
        {
            ResponseManager<CuentaDto> response = new();
            try
            {
                var cuenta = await _cuentaManager.AddMovementAsync(
                    input.NumeroCuenta, input.Valor);

                cuenta = await _cuentaRepository.UpdateAsync(cuenta);
                return response.OnSuccess(_mapper.Map<CuentaDto>(cuenta));
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

        public async Task<Response<string>> DeleteAsync(string numeroCuenta)
        {
            ResponseManager<string> response = new();
            try
            {
                var cuenta = await _cuentaManager.DeleteAsync(numeroCuenta);
                await _cuentaRepository.RemoveAsync(cuenta);
                return response.OnSuccess("Cuenta eliminada exitosamente");
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

        public async Task<Response<CuentaDto>> DeleteMovementAsync(DeleteMovementDto input)
        {
            ResponseManager<CuentaDto> response = new();
            try
            {
                var cuenta = await _cuentaManager.DeleteMovementAsync(
                    input.Id, input.NumeroCuenta);

                cuenta = await _cuentaRepository.UpdateAsync(cuenta);
                return response.OnSuccess(_mapper.Map<CuentaDto>(cuenta));
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

        public async Task<Response<List<CuentaDto>>> GetAllAsync()
        {
            ResponseManager<List<CuentaDto>> response = new();
            try
            {
                var cuentas = await _cuentaRepository.GetAllAsync();
                return response.OnSuccess(_mapper.Map<List<CuentaDto>>(cuentas));
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

        public async Task<Response<CuentaDto>> GetByAccountNumberAsync(string numeroCuenta)
        {
            ResponseManager<CuentaDto> response = new();
            try
            {
                var cuenta = await _cuentaRepository.GetByAccountNumberAsync(numeroCuenta);
                return response.OnSuccess(_mapper.Map<CuentaDto>(cuenta));
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

        public async Task<Response<CuentaDto>> InsertAsync(AddCuentaDto input)
        {
            ResponseManager<CuentaDto> response = new();
            try
            {
                var cuenta = await _cuentaManager.CreateAsync(
                    input.IdentificacionCliente,
                    input.NumeroCuenta,
                    input.TipoCuenta,
                    input.SaldoInicial,
                    input.Estado);

                cuenta = await _cuentaRepository.InsertAsync(cuenta);
                return response.OnSuccess(_mapper.Map<CuentaDto>(cuenta));
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

        public async Task<Response<CuentaDto>> UpdateAsync(UpdateCuentaDto input)
        {
            ResponseManager<CuentaDto> response = new();
            try
            {
                var cuenta = await _cuentaManager.UpdateAsync(
                    input.NumeroCuenta,
                    input.TipoCuenta,
                    input.Estado);

                cuenta = await _cuentaRepository.UpdateAsync(cuenta);
                return response.OnSuccess(_mapper.Map<CuentaDto>(cuenta));
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

        public async Task<Response<CuentaDto>> UpdateMovementAsync(UpdateMovementDto input)
        {
            ResponseManager<CuentaDto> response = new();
            try
            {
                var cuenta = await _cuentaManager.UpdateMovementAsync(
                    input.Id,
                    input.NumeroCuenta,
                    input.Valor);

                cuenta = await _cuentaRepository.UpdateAsync(cuenta);
                return response.OnSuccess(_mapper.Map<CuentaDto>(cuenta));
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