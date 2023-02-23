namespace MyAccounts.Modules.Shared
{
    public class Errors
    {
        public const string DTO_ID_ERROR = "El ID del registro no coincide con la petición";

        public static string NOT_FOUND(string entity, int id) => $"No existe {entity} con ID {id}";
    }
}
