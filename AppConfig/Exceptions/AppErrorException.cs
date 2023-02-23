namespace MyAccounts.AppConfig.Exceptions
{
    public class AppErrorException : Exception
    {
        public AppErrorException(string message) : base(message)
        {
        }
    }
}
