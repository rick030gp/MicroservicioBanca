namespace MicroservicioBanca.Domain.Shared
{
    public static class MicroservicioBancaErrors
    {
        public const string AccountAlreadyExistsError = "";
        public const string InsufficientBalanceError = "Saldo no disponible";
        public const string MovementTypeDoesNotExistError = "El tipo de movimiento {0} no existe";
        public const string InactiveAccountError = "La cuenta {0} no está activa";
    }
}
