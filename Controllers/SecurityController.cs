using Microsoft.AspNetCore.Mvc;
using MyAccounts.Modules.Security;

namespace MyAccounts.Controllers
{
    [Route("api/security")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        public ISecurityService _securityService;

        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] string userKey)
        {
            var user = _securityService.FindUser(userKey);

            if (user == null) return Unauthorized();

            var token = _securityService.BuildJwtToken(user);

            return Ok(token);
        }
    }
}
