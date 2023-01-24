using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MyAccounts.Controllers
{
    public class BaseController : ControllerBase
    {
        private int? userId = null;

        protected int UserId
        {
            get {
                userId ??= GetUserId();
                return userId.Value;
            }
        }

        private int GetUserId ()
        {
            var claim = User?.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? "0";
            return int.Parse(claim);
        }
    }
}
