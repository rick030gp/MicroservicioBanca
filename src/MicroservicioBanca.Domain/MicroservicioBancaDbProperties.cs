namespace MicroservicioBanca.Domain
{
    public static class MicroservicioBancaDbProperties
    {
        public static string DbTablePrefix { get; set; } = "";

        public static string DbSchema { get; set; } = "banca";

        public const string ConnectionStringName = "Banca";

        #region Table names
        public const string TableNameClient = "Cliente";
        public const string TableNameAccount = "Cuenta";
        public const string TableNameMovement = "Movimiento";
        #endregion
    }
}
