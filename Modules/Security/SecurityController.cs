using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyAccounts.Database;
using MyAccounts.Database.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyAccounts.Modules.Security
{
    [Route("security")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly MyAccountsContext _context;

        public SecurityController(IConfiguration config, MyAccountsContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] string userKey)
        {
            var user = _context.Users.FirstOrDefault(u => u.Key == userKey);

            if (user == null) return Unauthorized();

            var token = GetJwtToken(user);

            return Ok(token);
        }

        private string GetJwtToken (User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(24),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
