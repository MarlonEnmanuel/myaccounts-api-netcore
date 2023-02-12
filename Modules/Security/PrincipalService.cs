using System.Security.Claims;
using System.Security.Principal;

namespace MyAccounts.Modules.Security
{
    public interface IPrincipalService
    {
        public int UserId { get; }
    }

    public class PrincipalService : IPrincipalService
    {
        private readonly ClaimsPrincipal? _claimsPrincipal;
        private int? userId;

        public PrincipalService(IPrincipal principal)
        {
            _claimsPrincipal = (ClaimsPrincipal?)principal;
        }

        public int UserId 
        {
            get
            {
                if (userId == null)
                {
                    var claim = _claimsPrincipal?.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? "0";
                    userId = int.Parse(claim);
                }
                return userId ?? 0;
            }
        }
    }
}
