using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyAccounts.AppConfig;
using MyAccounts.Database.Context;
using MyAccounts.Database.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyAccounts.Modules.Security
{
    public interface ISecurityService
    {
        public User? FindUser(string userKey);
        public string BuildJwtToken(User user);
    }

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
            return _context.Users
                            .Include(u => u.Persons)
                            .FirstOrDefault(u => u.Key == userKey);
        }

        public string BuildJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Person?.Name ?? ""),
            };

            var token = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.UtcNow.AddMonths(24),
                                signingCredentials: credentials
                            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
