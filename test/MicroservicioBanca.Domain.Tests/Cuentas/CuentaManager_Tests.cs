using MicroservicioBanca.Clientes;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Threading.Tasks;
using Xunit;

namespace MicroservicioBanca.Cuentas
{
    public class CuentaManager_Tests
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly CuentaManager _cuentaManager;
        private readonly Cuenta _cuentaSeed;
        private readonly Cliente _clienteSeed;

        public CuentaManager_Tests()
        {
            _cuentaRepository = Substitute.For<ICuentaRepository>();
            _clienteRepository = Substitute.For<IClienteRepository>();

            var clienteManager = new ClienteManager(_clienteRepository);
            _clienteSeed = clienteManager.CreateAsync(
                TestData.NombreClienteDataSeed,
                Genero.Masculino,
                29,
                TestData.IdentificacionClienteDataSeed,
                "El Angel",
                "0984905901",
                "12345678").Result;

            _clienteRepository.GetByIdentificationAsync(TestData.IdentificacionClienteDataSeed)
               .Returns(_clienteSeed);

            _cuentaManager = new CuentaManager(_cuentaRepository, _clienteRepository);
            _cuentaSeed = _cuentaManager.CreateAsync(
                TestData.IdentificacionClienteDataSeed,
                TestData.NumeroCuentaDataSeed,
                TipoCuenta.Ahorros).Result;
        }

        #region CreateAsync
        [Fact]
        public async Task Should_CreateAccount_Ok()
        {
            _cuentaRepository.GetByAccountNumberAsync(TestData.NombreClienteDataSeed)
                .ReturnsNull();

            _clienteRepository.GetByIdentificationAsync(TestData.IdentificacionClienteDataSeed)
               .Returns(_clienteSeed);

            var cuenta = await _cuentaManager.CreateAsync(
                TestData.IdentificacionClienteDataSeed,
                TestData.NumeroCuentaDataSeed,
                TestData.TipoCuentaDataSeed);

            Assert.NotNull(cuenta);
            Assert.Equal(TestData.NumeroCuentaDataSeed, cuenta.NumeroCuenta);
        }

        [Fact]
        public async Task Should_Throw_AccountAlreadyExistsError_When_CreateAccount()
        {
            _cuentaRepository.GetByAccountNumberAsync(_cuentaSeed.NumeroCuenta)
                .Returns(_cuentaSeed);

            _clienteRepository.GetByIdentificationAsync(TestData.IdentificacionClienteDataSeed)
               .Returns(_clienteSeed);

            Task act() => _cuentaManager.CreateAsync(
                    "1000102002",
                    TestData.NumeroCuentaDataSeed,
                    TipoCuenta.Corriente);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.AccountAlreadyExistsError.Code);
        }

        [Fact]
        public async Task Should_Throw_ClientNotFoundError_When_CreateAccount()
        {
            _cuentaRepository.GetByAccountNumberAsync("0002")
                .ReturnsNull();

            _clienteRepository.GetByIdentificationAsync(TestData.IdentificacionClienteDataSeed)
               .Returns(_clienteSeed);

            Task act() => _cuentaManager.CreateAsync(
                    "0002",
                    TestData.NumeroCuentaDataSeed,
                    TipoCuenta.Corriente);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.ClientNotFoundError.Code);
        }
        #endregion

        #region UpdateAsync
        [Fact]
        public async Task Should_UpdateAccount_Ok()
        {
            _cuentaRepository.GetByAccountNumberAsync(_cuentaSeed.NumeroCuenta)
                .Returns(_cuentaSeed);

            var cuentaActualizada = await _cuentaManager.UpdateAsync(
                _cuentaSeed.NumeroCuenta,
                TipoCuenta.Corriente,
                true);

            Assert.NotNull(cuentaActualizada);
            Assert.NotEqual(TestData.TipoCuentaDataSeed, cuentaActualizada.TipoCuenta);
        }

        [Fact]
        public async Task Should_Throw_AccountDoesNotExistError_When_UpdateClient()
        {
            _cuentaRepository.GetByAccountNumberAsync(TestData.NonExistentAccountNumber)
                .ReturnsNull();

            Task act() => _cuentaManager.UpdateAsync(
                    TestData.NonExistentAccountNumber,
                    TipoCuenta.Corriente,
                    true);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.AccountDoesNotExistError.Code);
        }
        #endregion
    }
}
