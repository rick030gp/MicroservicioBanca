using MicroservicioBanca.Clientes;
using MicroservicioBanca.Cuentas;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MicroservicioBanca.Movimientos
{
    public class MovimientoManager_Tests
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly MovimientoManager _movimientoManager;
        private readonly Cuenta _cuentaSeed, _cuentaInactivaSeed;

        public MovimientoManager_Tests()
        {
            _cuentaRepository = Substitute.For<ICuentaRepository>();
            _movimientoRepository = Substitute.For<IMovimientoRepository>();
            var clienteRepository = Substitute.For<IClienteRepository>();
            var clienteManager = new ClienteManager(clienteRepository);
            var cliente = clienteManager.CreateAsync(
                TestData.NombreClienteDataSeed,
                Genero.Masculino,
                29,
                TestData.IdentificacionClienteDataSeed,
                "El Angel",
                "0984905901",
                "12345678").Result;

            clienteRepository.GetByIdentificationAsync(TestData.IdentificacionClienteDataSeed)
               .Returns(cliente);
            var cuentaManager = new CuentaManager(_cuentaRepository, clienteRepository);
            _cuentaSeed = cuentaManager.CreateAsync(
                TestData.IdentificacionClienteDataSeed,
                TestData.NumeroCuentaDataSeed,
                TipoCuenta.Ahorros).Result;

            _cuentaInactivaSeed = cuentaManager.CreateAsync(
                TestData.IdentificacionClienteDataSeed,
                TestData.NumeroCuentaInactivaDataSeed,
                TipoCuenta.Ahorros,
                estado: false).Result;

            _movimientoManager = new MovimientoManager(_cuentaRepository, _movimientoRepository);
        }

        #region CreateAsync
        [Fact]
        public async Task Should_CreateMovement_Ok()
        {
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NumeroCuentaDataSeed)
                .Returns(_cuentaSeed);

            var movimiento = await _movimientoManager.CreateAsync(
                TestData.NumeroCuentaDataSeed,
                100);

            Assert.NotNull(movimiento);
            Assert.Equal(100, _cuentaSeed.Saldo);
        }

        [Fact]
        public async Task Should_Throw_AccountDoesNotExistError_When_CreateMovement()
        {
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NonExistentAccountNumber)
                .ReturnsNull();

            Task act() => _movimientoManager.CreateAsync(
                    TestData.NonExistentAccountNumber,
                    50);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.AccountDoesNotExistError.Code);
        }

        [Fact]
        public async Task Should_Throw_InactiveAccountError_When_CreateMovement()
        {
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NumeroCuentaInactivaDataSeed)
                .Returns(_cuentaInactivaSeed);

            Task act() => _movimientoManager.CreateAsync(
                    TestData.NumeroCuentaInactivaDataSeed,
                    100);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.InactiveAccountError.Code);
        }
        #endregion

        #region UpdateAsync
        [Fact]
        public async Task Should_UpdateMovement_Ok()
        {
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NumeroCuentaDataSeed)
                .Returns(_cuentaSeed);

            var movimiento = await _movimientoManager.CreateAsync(
                TestData.NumeroCuentaDataSeed,
                50);

            _cuentaSeed.Movimientos.Add(movimiento);

            movimiento = await _movimientoManager.UpdateAsync(
                movimiento.Id,
                TestData.NumeroCuentaDataSeed,
                90);

            Assert.NotNull(movimiento);
            Assert.Equal(40, movimiento.Valor);
            Assert.Equal(90, _cuentaSeed.Saldo);
        }

        [Fact]
        public async Task Should_Throw_AccountDoesNotExistError_When_UpdateMovement()
        {
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NonExistentAccountNumber)
                .ReturnsNull();

            Task act() => _movimientoManager.UpdateAsync(
                    Guid.NewGuid(),
                    TestData.NonExistentAccountNumber,
                    50);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.AccountDoesNotExistError.Code);
        }

        [Fact]
        public async Task Should_Throw_InactiveAccountError_When_UpdateMovement()
        {
            _cuentaInactivaSeed.Estado = true;
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NumeroCuentaInactivaDataSeed)
                .Returns(_cuentaInactivaSeed);

            var movimiento = await _movimientoManager.CreateAsync(
                TestData.NumeroCuentaInactivaDataSeed,
                100);

            _cuentaInactivaSeed.Movimientos.Add(movimiento);
            _cuentaInactivaSeed.Estado = false;

            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NumeroCuentaInactivaDataSeed)
                .Returns(_cuentaInactivaSeed);

            Task act() => _movimientoManager.UpdateAsync(
                    _cuentaInactivaSeed.Movimientos.FirstOrDefault().Id,
                    TestData.NumeroCuentaInactivaDataSeed,
                    100);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.InactiveAccountError.Code);
        }

        [Fact]
        public async Task Should_Throw_MovementDoesNotExistError_When_UpdateMovement()
        {
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NumeroCuentaDataSeed)
                .Returns(_cuentaSeed);

            Task act() => _movimientoManager.UpdateAsync(
                    Guid.NewGuid(),
                    TestData.NumeroCuentaDataSeed,
                    100);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.MovementDoesNotExistError.Code);
        }

        [Fact]
        public async Task Should_Throw_UpdateSameMovementError_When_UpdateMovement()
        {
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NumeroCuentaDataSeed)
                .Returns(_cuentaSeed);

            var movimiento = await _movimientoManager.CreateAsync(
                TestData.NumeroCuentaDataSeed,
                50);

            _cuentaSeed.Movimientos.Add(movimiento);

            Task act() => _movimientoManager.UpdateAsync(
                movimiento.Id,
                TestData.NumeroCuentaDataSeed,
                50);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.UpdateSameMovementError.Code);
        }
        #endregion

        #region DeleteAsync
        [Fact]
        public async Task Should_DeleteMovement_Ok()
        {
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NumeroCuentaDataSeed)
                .Returns(_cuentaSeed);

            var movimiento = await _movimientoManager.CreateAsync(
                TestData.NumeroCuentaDataSeed,
                50);

            _cuentaSeed.Movimientos.Add(movimiento);

            movimiento = await _movimientoManager.DeleteAsync(
                movimiento.Id,
                TestData.NumeroCuentaDataSeed);

            Assert.NotNull(movimiento);
            Assert.Equal(-50, movimiento.Valor);
            Assert.Equal(0, _cuentaSeed.Saldo);
        }

        [Fact]
        public async Task Should_Throw_AccountDoesNotExistError_When_DeleteMovement()
        {
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NonExistentAccountNumber)
                .ReturnsNull();

            Task act() => _movimientoManager.DeleteAsync(
                    Guid.NewGuid(),
                    TestData.NonExistentAccountNumber);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.AccountDoesNotExistError.Code);
        }

        [Fact]
        public async Task Should_Throw_InactiveAccountError_When_DeleteMovement()
        {
            _cuentaInactivaSeed.Estado = true;
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NumeroCuentaInactivaDataSeed)
                .Returns(_cuentaInactivaSeed);

            var movimiento = await _movimientoManager.CreateAsync(
                TestData.NumeroCuentaInactivaDataSeed,
                100);

            _cuentaInactivaSeed.Movimientos.Add(movimiento);
            _cuentaInactivaSeed.Estado = false;

            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NumeroCuentaInactivaDataSeed)
                .Returns(_cuentaInactivaSeed);

            Task act() => _movimientoManager.DeleteAsync(
                    _cuentaInactivaSeed.Movimientos.FirstOrDefault().Id,
                    TestData.NumeroCuentaInactivaDataSeed);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.InactiveAccountError.Code);
        }

        [Fact]
        public async Task Should_Throw_MovementDoesNotExistError_When_DeleteMovement()
        {
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NumeroCuentaDataSeed)
                .Returns(_cuentaSeed);

            Task act() => _movimientoManager.DeleteAsync(
                    Guid.NewGuid(),
                    TestData.NumeroCuentaDataSeed);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.MovementDoesNotExistError.Code);
        }
        #endregion

        #region CommitMovementUpdatingAccountAsync
        [Fact]
        public async Task Should_Throw_MovementZeroError_When_Try_CommitMovement()
        {
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NumeroCuentaDataSeed)
                .Returns(_cuentaSeed);

            Task act() => _movimientoManager.CreateAsync(
                TestData.NumeroCuentaDataSeed,
                0);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.MovementZeroError.Code);
        }

        [Fact]
        public async Task Should_Throw_InsufficientBalanceError_When_Try_CommitMovement()
        {
            _cuentaRepository.GetWithMovementsByAccountNumberAsync(TestData.NumeroCuentaDataSeed)
                .Returns(_cuentaSeed);

            Task act() => _movimientoManager.CreateAsync(
                TestData.NumeroCuentaDataSeed,
                -100);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.InsufficientBalanceError.Code);
        }
        #endregion
    }
}
