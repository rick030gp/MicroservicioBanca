using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Domain.Shared;
using MicroservicioBanca.Domain.Shared.Cuentas;
using System;
using System.Linq;
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

            return cuenta;
        }

        public async Task<Cuenta> DeleteAsync(string numeroCuenta)
        {
            var cuenta = await _cuentaRepository.GetByAccountNumberAsync(numeroCuenta);
            if (cuenta == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.AccountDoesNotExistError);

            return cuenta;
        }

        public async Task<Cuenta> AddMovementAsync(
            string numeroCuenta,
            float valor
            )
        {
            var cuenta = await _cuentaRepository.GetWithMovementsByAccountNumberAsync(numeroCuenta);
            if (cuenta == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.AccountDoesNotExistError);

            if (!cuenta.Estado)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.InactiveAccountError);

            cuenta.AgregarMovimiento(Guid.NewGuid(), valor);

            return cuenta;
        }

        public async Task<Cuenta> UpdateMovementAsync(
            Guid id,
            string numeroCuenta,
            float valor
            )
        {
            var cuenta = await _cuentaRepository.GetWithMovementsByAccountNumberAsync(numeroCuenta);
            if (cuenta == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.AccountDoesNotExistError);

            if (!cuenta.Estado)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.InactiveAccountError);

            var movimiento = cuenta.Movimientos.FirstOrDefault(m => m.Id == id);
            if (movimiento == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.MovementDoesNotExistError);

            cuenta.AgregarMovimiento(Guid.NewGuid(), movimiento.Valor + valor);

            return cuenta;
        }

        public async Task<Cuenta> DeleteMovementAsync(
            Guid id,
            string numeroCuenta
            )
        {
            var cuenta = await _cuentaRepository.GetWithMovementsByAccountNumberAsync(numeroCuenta);
            if (cuenta == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.AccountDoesNotExistError);

            if (!cuenta.Estado)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.InactiveAccountError);

            var movimiento = cuenta.Movimientos.FirstOrDefault(m => m.Id == id);
            if (movimiento == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.MovementDoesNotExistError);

            cuenta.AgregarMovimiento(Guid.NewGuid(), movimiento.Valor * -1);

            return cuenta;
        }
    }
}
