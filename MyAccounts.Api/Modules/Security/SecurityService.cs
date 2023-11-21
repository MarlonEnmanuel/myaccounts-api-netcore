using Microsoft.IdentityModel.Tokens;
using MyAccounts.Api.AppConfig;
using MyAccounts.Api.Database;
using MyAccounts.Api.Database.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyAccounts.Api.Modules.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly IAppSettings _settings;
        private readonly MyAccountsContext _context;

        public SecurityService(IAppSettings settings, MyAccountsContext context)
        {
            _settings = settings;
            _context = context;
        }

        public User? FindUser (string userKey)
        {
            return _context.Users.FirstOrDefault(u => u.Password == userKey);
        }

        public string BuildJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username ?? ""),
            };

            var token = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.UtcNow.AddDays(1),
                                signingCredentials: credentials
                            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
