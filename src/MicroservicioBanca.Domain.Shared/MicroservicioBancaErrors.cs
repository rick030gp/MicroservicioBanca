using MicroservicioBanca.Response.Models;

namespace MicroservicioBanca
{
    public static class MicroservicioBancaErrors
    {
        public static readonly Error ClientAlreadyExistsError = new()
        {
            Code = "ECL001",
            Message = "El cliente ya existe"
        };
        public static readonly Error ClientNotFoundError = new()
        {
            Code = "ECL002",
            Message = "El cliente no fue encontrado"
        };
        public static readonly Error InsufficientBalanceError = new()
        {
            Code = "ECT001",
            Message = "Saldo no disponible"
        };
        public static readonly Error MovementTypeDoesNotExistError = new()
        {
            Code = "ECT002",
            Message = "El tipo de movimiento no existe"
        };
        public static readonly Error InactiveAccountError = new()
        {
            Code = "ECT003",
            Message = "La cuenta no está activa"
        };
        public static readonly Error AccountAlreadyExistsError = new()
        {
            Code = "ECT004",
            Message = "La cuenta ya existe"
        };
        public static readonly Error AccountDoesNotExistError = new()
        {
            Code = "ECT005",
            Message = "La cuenta no existe"
        };
        public static readonly Error MovementDoesNotExistError = new()
        {
            Code = "ECT006",
            Message = "La transacción no existe"
        };
        public static readonly Error MovementZeroError = new()
        {
            Code = "ECT007",
            Message = "No se puede agregar una transacción de 0"
        };
        public static readonly Error ReportDatesError = new()
        {
            Code = "ECT008",
            Message = "Rango no válido entre las fechas"
        };
        public static readonly Error UpdateSameMovementError = new()
        {
            Code = "ECT009",
            Message = "La transacción a actualizar no tiene cambios en su valor"
        };
        public static readonly Error GeneralError = new()
        {
            Code = "ERR000",
            Message = "Un error sin controlar"
        };
    }
}
