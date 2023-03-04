namespace MyAccounts.AppConfig
{
    public interface IAppSettings
    {
        public string JwtKey { get; }
    }

    public class AppSettings: IAppSettings
    {
        public string JwtKey => GetString("Jwt:Key");

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        private string GetString(string section)
        {
            return _configuration[section] ?? throw new InvalidOperationException($"Setting '{section}' not found.");
        }
    }
}
