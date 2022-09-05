using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Domain.Shared;
using MicroservicioBanca.Domain.Shared.Cuentas;
using System;
using System.Threading.Tasks;

namespace MicroservicioBanca.Domain.Cuentas
{
    public class CuentaManager
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IClienteRepository _clienteRepository;

        public CuentaManager(
            ICuentaRepository cuentaRepository,
            IClienteRepository clienteRepository)
        {
            _cuentaRepository = cuentaRepository;
            _clienteRepository = clienteRepository;
        }
        
        public async Task<Cuenta> CreateAsync(
            string identificacion,
            string numeroCuenta,
            TipoCuenta tipoCuenta,
            float saldoInicial = 0,
            bool estado = true)
        {
            var cuenta = await _cuentaRepository.GetByAccountNumberAsync(numeroCuenta);
            if (cuenta != null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.AccountAlreadyExistsError);

            var cliente = await _clienteRepository.GetByIdentificationAsync(identificacion);
            if (cliente == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.ClientNotFoundError);

            cuenta = new Cuenta(
                Guid.NewGuid(),
                cliente.Id,
                numeroCuenta,
                tipoCuenta,
                saldoInicial,
                estado);

            await _cuentaRepository.InsertAsync(cuenta);

            return cuenta;
        }

        public async Task<Cuenta> UpdateAsync(
            string numeroCuenta,
            TipoCuenta tipoCuenta,
            bool estado)
        {
            var cuenta = await _cuentaRepository.GetByAccountNumberAsync(numeroCuenta);
            if (cuenta == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.AccountDoesNotExistError);

            cuenta.TipoCuenta = tipoCuenta;
            cuenta.Estado = estado;

            await _cuentaRepository.UpdateAsync(cuenta);
            return cuenta;
        }

        public async Task DeleteAsync(string numeroCuenta)
        {
            var cuenta = await _cuentaRepository.GetByAccountNumberAsync(numeroCuenta);
            if (cuenta == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.AccountDoesNotExistError);

            await _cuentaRepository.RemoveAsync(cuenta);
        }
    }
}
