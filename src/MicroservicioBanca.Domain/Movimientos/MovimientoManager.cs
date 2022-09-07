using MicroservicioBanca.Cuentas;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MicroservicioBanca.Movimientos
{
    public class MovimientoManager
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IMovimientoRepository _movimientoRepository;

        public MovimientoManager(
            ICuentaRepository cuentaRepository,
            IMovimientoRepository movimientoRepository)
        {
            _cuentaRepository = cuentaRepository;
            _movimientoRepository = movimientoRepository;
        }

        public async Task<Movimiento> CreateAsync(
            string numeroCuenta,
            float valor
            )
        {
            var cuenta = await _cuentaRepository.GetWithMovementsByAccountNumberAsync(numeroCuenta);
            if (cuenta == null)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.AccountDoesNotExistError);

            if (!cuenta.Estado)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.InactiveAccountError);

            var movimiento = await CommitMovementUpdatingAccountAsync(cuenta, valor);
            return movimiento;
        }

        public async Task<Movimiento> UpdateAsync(
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

            if (valor - movimiento.Valor == 0)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.UpdateSameMovementError);

            movimiento = await CommitMovementUpdatingAccountAsync(cuenta, valor - movimiento.Valor);

            return movimiento;
        }

        public async Task<Movimiento> DeleteAsync(
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

            movimiento = await CommitMovementUpdatingAccountAsync(cuenta, movimiento.Valor * -1);

            return movimiento;
        }

        private async Task<Movimiento> CommitMovementUpdatingAccountAsync(Cuenta cuenta, float valor)
        {
            if (valor == 0)
                throw new MicroservicioBancaException(MicroservicioBancaErrors.MovementZeroError);

            TipoMovimiento tipoMovimiento = valor < 0 ?
                TipoMovimiento.Debito : TipoMovimiento.Credito;

            if (tipoMovimiento == TipoMovimiento.Debito && cuenta.Saldo < Math.Abs(valor))
                throw new MicroservicioBancaException(MicroservicioBancaErrors.InsufficientBalanceError);

            var movimiento = new Movimiento(
                Guid.NewGuid(),
                cuenta.Id,
                tipoMovimiento,
                cuenta.Saldo,
                valor);

            cuenta.UpdateSaldo(cuenta.Saldo + valor);

            await _movimientoRepository.InsertAsync(movimiento);
            await _cuentaRepository.UpdateAsync(cuenta);
            return movimiento;
        }
    }
}
