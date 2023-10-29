namespace MyAccounts.Api.AppConfig.Exceptions
{
    public class ApiErrorException : Exception
    {
        public ApiErrorException(string message) : base(message)
        {
        }
    }
}
