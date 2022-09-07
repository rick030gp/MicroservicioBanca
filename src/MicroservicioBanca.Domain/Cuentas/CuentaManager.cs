using MicroservicioBanca.Clientes;
using System;
using System.Threading.Tasks;

namespace MicroservicioBanca.Cuentas
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

            return cuenta;
        }

        public async Task<Cuenta> UpdateAsync(
            string numeroCuenta,
            TipoCuenta? tipoCuenta,
            bool? estado)
        {
            var cuenta = await _cuentaRepository.GetByAccountNumberAsync(numeroCuenta);
            if (cuenta == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.AccountDoesNotExistError);

            cuenta.TipoCuenta = tipoCuenta ?? cuenta.TipoCuenta;
            cuenta.Estado = estado ?? cuenta.Estado;

            return cuenta;
        }
    }
}
