using MyAccounts.Api.Database.Models;

namespace MyAccounts.Api.Modules.Security
{
    public interface ISecurityService
    {
        public User? FindUser(string userKey);
        public string BuildJwtToken(User user);
    }
}
