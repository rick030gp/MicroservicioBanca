using NSubstitute;
using System.Threading.Tasks;
using Xunit;
using NSubstitute.ReturnsExtensions;

namespace MicroservicioBanca.Clientes
{
    public class ClienteManager_Tests
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ClienteManager _clienteManager;
        private readonly Cliente _clienteSeed;

        public ClienteManager_Tests()
        {
            _clienteRepository = Substitute.For<IClienteRepository>();
            _clienteManager = new ClienteManager(_clienteRepository);
            _clienteSeed =_clienteManager.CreateAsync(
                TestData.NombreClienteDataSeed,
                Genero.Masculino,
                29,
                TestData.IdentificacionClienteDataSeed,
                "El Angel",
                "0984905901",
                "12345678").Result;
        }


        #region CreateAsync
        [Fact]
        public async Task Should_CreateClient_Ok()
        {
            _clienteRepository.GetByIdentificationAsync(_clienteSeed.Identificacion)
                .ReturnsNull();

            var cliente = await _clienteManager.CreateAsync(
                "RICHARD",
                Genero.Masculino,
                29,
                TestData.IdentificacionClienteDataSeed,
                "El Angel",
                "0984905901",
                "12345678");

            // ASSERT
            Assert.NotNull(cliente);
            Assert.Equal(TestData.IdentificacionClienteDataSeed, cliente.Identificacion);
        }

        [Fact]
        public async Task Should_Throw_ClientAlreadyExistsError_When_CreateClient()
        {
            // ARRANGE
            _clienteRepository.GetByIdentificationAsync(_clienteSeed.Identificacion)
                .Returns(_clienteSeed);

            // ACT
            Task act() => _clienteManager.CreateAsync(
                    "JUAN",
                    Genero.Masculino,
                    29,
                    _clienteSeed.Identificacion,
                    "Ibarra",
                    "0984905905",
                    "12345678");

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);
            
            // ASSERT
            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.ClientAlreadyExistsError.Code);
        }
        #endregion

        #region UpdateAsync
        [Fact]
        public async Task Should_UpdateClient_Ok()
        {
            _clienteRepository.GetByIdentificationAsync(_clienteSeed.Identificacion)
                .Returns(_clienteSeed);

            var clienteActualizado = await _clienteManager.UpdateAsync(
                _clienteSeed.Identificacion,
                "Sara",
                Genero.Femenino,
                15,
                "Quito",
                "0999999999",
                "123132121",
                true);

            Assert.NotNull(clienteActualizado);
            Assert.NotEqual(TestData.NombreClienteDataSeed, clienteActualizado.Nombre);
        }

        [Fact]
        public async Task Should_Throw_ClientDoesNotExistError_When_UpdateClient()
        {
            _clienteRepository.GetByIdentificationAsync("0401734041")
                .ReturnsNull();

            Task act() => _clienteManager.UpdateAsync(
                    "0000000000",
                    "Sara",
                    Genero.Femenino,
                    15,
                    "Quito",
                    "0999999999",
                    "123132121",
                    true);

            var exception = await Assert.ThrowsAsync<MicroservicioBancaException>(act);

            Assert.NotNull(exception);
            Assert.Equal(exception.Code, MicroservicioBancaErrors.ClientNotFoundError.Code);
        }
        #endregion
    }
}
