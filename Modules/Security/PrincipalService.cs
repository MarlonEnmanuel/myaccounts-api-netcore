using System.Security.Claims;
using System.Security.Principal;

namespace MyAccounts.Modules.Security
{
    public interface IPrincipalService
    {
        public int UserId { get; }
        public DateTime RequestDate { get; }
    }

    public class PrincipalService : IPrincipalService
    {
        private readonly ClaimsPrincipal? _claimsPrincipal;
        private int? userId;
        private DateTime? requestDate;

        public int UserId => userId ??= GetUserId();

        public DateTime RequestDate => requestDate ??= DateTime.Now;

        public PrincipalService(IPrincipal principal)
        {
            _claimsPrincipal = (ClaimsPrincipal?)principal;
        }

        private int GetUserId()
        {
            var claim = _claimsPrincipal?.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? "0";
            return int.TryParse(claim, out int userId) ? userId : 0;
        }
    }
}
