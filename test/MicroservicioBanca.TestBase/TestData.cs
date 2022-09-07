using MicroservicioBanca.Cuentas;

namespace MicroservicioBanca
{
    public static class TestData
    {
        public const string IdentificacionClienteDataSeed = "1212121212";
        public const string NombreClienteDataSeed = "RICHARD TARUPI";

        public const string NumeroCuentaDataSeed = "0001";
        public const TipoCuenta TipoCuentaDataSeed = TipoCuenta.Ahorros;

        public const string NonExistentAccountNumber = "XXXXX";
        public const string NumeroCuentaInactivaDataSeed = "1002";
    }
}
